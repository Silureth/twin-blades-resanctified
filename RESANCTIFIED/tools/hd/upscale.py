"""Upscales art/original/<key>.png 4x with Real-ESRGAN into art/upscaled-x4/<key>.png (straight alpha).

Needs the venv described in README.md (PyTorch with CUDA, spandrel) and the model file.

Usage: python tools/hd/upscale.py --model RealESRGAN_x4plus_anime_6B.pth [--alpha hybrid|model|lanczos] [--force] [key ...]

Alpha handling (sprite atlases are mostly transparent):
- Fully transparent pixels get the colour of the nearest opaque art before upscaling ("bleeding"), so the
  model never blends edges with the black stored in transparent pixels (that causes dark halos).
- Colour is upscaled by the model. Alpha is upscaled by the model (alpha replicated to 3 channels,
  Real-ESRGAN's own approach), which keeps sprite outlines crisp but flattens soft glows into hard bands,
  so "hybrid" (the default) uses Lanczos alpha wherever the original alpha is soft (glows, motion blur).
- Pixels that were transparent and at least EMPTY_MARGIN pixels from any visible pixel stay exactly
  transparent, so the model's ringing cannot leak into neighbouring atlas cells.
"""

import argparse
import sys
from pathlib import Path

import numpy as np
import torch
from PIL import Image
from spandrel import ImageModelDescriptor, ModelLoader

from common import MODEL_SCALE, ORIGINAL_DIR, UPSCALED_DIR, keys_in, load_rgba, save_rgba

TILE = 256
TILE_PAD = 16
EDGE_DILATION_STEPS = 4
EMPTY_MARGIN = 2
# Share of partially transparent pixels in a 5x5 window above which alpha counts as soft (an antialiased
# hard edge stays below about 0.3, a glow is close to 1).
SOFT_ALPHA_RANGE = (0.25, 0.55)
# The anime model flattens low-contrast painted texture (moss tint on stone, grass shading). Its output is
# pulled back towards the original colours at low frequency (blurred with this sigma, in original pixels).
COLOUR_RESTORE_SIGMA = 1.0
# Textures whose alpha is better upscaled another way than --alpha. brad is a thin white glyph font: its
# strokes are so narrow that "hybrid" mistakes them for soft alpha and blurs them; the model keeps them crisp.
ALPHA_MODE_OVERRIDES = {"fonts/brad": "model"}


def shifted_sum(values, radius=1):
    """Sum of each pixel's (2r+1)^2 neighbourhood (edge pixels see zeros outside the image)."""
    height, width = values.shape[:2]
    pad = [(radius, radius), (radius, radius)] + [(0, 0)] * (values.ndim - 2)
    padded = np.pad(values, pad)
    total = np.zeros_like(values)
    for dy in range(2 * radius + 1):
        for dx in range(2 * radius + 1):
            total += padded[dy:dy + height, dx:dx + width]
    return total


def shifted_max(values, radius):
    height, width = values.shape
    padded = np.pad(values, radius)
    result = np.zeros_like(values)
    for dy in range(2 * radius + 1):
        for dx in range(2 * radius + 1):
            result = np.maximum(result, padded[dy:dy + height, dx:dx + width])
    return result


def dilate(mask, radius):
    return shifted_sum(mask.astype(np.float32), radius) > 0


def soft_alpha_weight(alpha):
    """0 where the original alpha has hard (antialiased) edges, 1 in soft regions, smooth in between."""
    partial = ((alpha > 0.04) & (alpha < 0.96)).astype(np.float32)
    share = shifted_sum(partial, 2) / 25.0
    low, high = SOFT_ALPHA_RANGE
    weight = np.clip((share - low) / (high - low), 0, 1)
    weight = weight * weight * (3 - 2 * weight)
    # Grow over the outer fringe of glows (where the share drops again), then soften the transition.
    weight = shifted_sum(shifted_max(weight, 2), 2) / 25.0
    return resize(weight, MODEL_SCALE, Image.Resampling.BILINEAR)


def bleed_colour(rgb, alpha):
    """Fills the colour of fully transparent pixels from nearby visible pixels.

    The first few rings copy the average of their visible neighbours (exact edge colours, which is what the
    model sees around a sprite); the rest is filled from a coarse pyramid so large empty areas are smooth.
    """
    rgb = rgb.copy()
    known = alpha > 0
    if not known.any():
        return rgb
    for _ in range(EDGE_DILATION_STEPS):
        weight = known.astype(np.float32)
        count = shifted_sum(weight)
        colour_sum = shifted_sum(rgb * weight[..., None])
        grow = ~known & (count > 0)
        rgb[grow] = colour_sum[grow] / count[grow][:, None]
        known = known | grow
    if not known.all():
        rgb[~known] = pyramid_fill(rgb * known[..., None], known.astype(np.float32))[~known]
    return rgb


def pyramid_fill(colour_sum, weight):
    """Push-pull fill: every pixel gets the weighted average colour of the nearest region that has any."""
    height, width = weight.shape
    if height <= 1 and width <= 1:
        return colour_sum / max(weight[0, 0], 1e-6)
    padded_h = height + height % 2
    padded_w = width + width % 2
    c = np.pad(colour_sum, ((0, padded_h - height), (0, padded_w - width), (0, 0)))
    w = np.pad(weight, ((0, padded_h - height), (0, padded_w - width)))
    coarse_c = c.reshape(padded_h // 2, 2, padded_w // 2, 2, 3).sum(axis=(1, 3))
    coarse_w = w.reshape(padded_h // 2, 2, padded_w // 2, 2).sum(axis=(1, 3))
    coarse = pyramid_fill(coarse_c, coarse_w)
    upsampled = coarse.repeat(2, axis=0).repeat(2, axis=1)[:height, :width]
    has = weight > 1e-6
    return np.where(has[..., None], colour_sum / np.maximum(weight, 1e-6)[..., None], upsampled)


def load_model(path, device):
    descriptor = ModelLoader(device=device).load_from_file(path)
    if not isinstance(descriptor, ImageModelDescriptor) or descriptor.scale != MODEL_SCALE:
        sys.exit(f"{path}: expected a {MODEL_SCALE}x image model")
    descriptor.model.eval()
    if device.type == "cuda":
        descriptor.model.half()
    return descriptor.model


@torch.inference_mode()
def run_model(model, device, image):
    """Runs the model on an HxWx3 0..1 image in overlapping tiles; returns a (4H)x(4W)x3 image."""
    dtype = torch.float16 if device.type == "cuda" else torch.float32
    source = torch.from_numpy(np.ascontiguousarray(image.transpose(2, 0, 1)))[None].to(device, dtype)
    # Replicated border so the image edge gets the same context as the interior.
    source = torch.nn.functional.pad(source, (TILE_PAD,) * 4, mode="replicate")
    _, _, height, width = source.shape
    scale = MODEL_SCALE
    output = torch.zeros((1, 3, height * scale, width * scale), dtype=torch.float32, device=device)
    for y in range(0, height, TILE):
        for x in range(0, width, TILE):
            y0, x0 = max(y - TILE_PAD, 0), max(x - TILE_PAD, 0)
            y1, x1 = min(y + TILE + TILE_PAD, height), min(x + TILE + TILE_PAD, width)
            result = model(source[:, :, y0:y1, x0:x1]).float()
            inner_h, inner_w = min(TILE, height - y), min(TILE, width - x)
            oy, ox = (y - y0) * scale, (x - x0) * scale
            output[:, :, y * scale:(y + inner_h) * scale, x * scale:(x + inner_w) * scale] = \
                result[:, :, oy:oy + inner_h * scale, ox:ox + inner_w * scale]
    margin = TILE_PAD * scale
    output = output[:, :, margin:-margin, margin:-margin]
    return output[0].clamp(0, 1).permute(1, 2, 0).cpu().numpy()


def gaussian_blur(image, sigma):
    """Separable Gaussian blur of an HxWxC image with replicated borders."""
    radius = int(3 * sigma + 0.5)
    kernel = np.exp(-0.5 * (np.arange(-radius, radius + 1) / sigma) ** 2)
    kernel /= kernel.sum()
    for axis in (0, 1):
        pad = [(0, 0)] * image.ndim
        pad[axis] = (radius, radius)
        padded = np.pad(image, pad, mode="edge")
        length = image.shape[axis]
        image = sum(weight * np.take(padded, range(i, i + length), axis=axis) for i, weight in enumerate(kernel))
    return image


def restore_colour(rgb, rgb_x4):
    """Adds back the blurred difference between the original and the model output reduced to 1x."""
    height, width = rgb.shape[:2]
    reduced = np.stack([resize_to(rgb_x4[..., c], width, height, Image.Resampling.BOX) for c in range(3)], axis=2)
    error = gaussian_blur(rgb - reduced, COLOUR_RESTORE_SIGMA)
    correction = np.stack([resize_to(error[..., c], width * MODEL_SCALE, height * MODEL_SCALE,
                                     Image.Resampling.BICUBIC, clip=False) for c in range(3)], axis=2)
    return np.clip(rgb_x4 + correction, 0, 1)


def resize_to(channel, width, height, method, clip=True):
    image = Image.fromarray(channel.astype(np.float32), "F")
    result = np.asarray(image.resize((width, height), method))
    return result.clip(0, 1) if clip else result


def resize(channel, scale, method=Image.Resampling.LANCZOS):
    height, width = channel.shape
    return resize_to(channel, width * scale, height * scale, method)


def upscale_rgba(model, device, rgba, alpha_mode):
    rgb, alpha = rgba[..., :3], rgba[..., 3]
    bled = bleed_colour(rgb, alpha)
    rgb_x4 = restore_colour(bled, run_model(model, device, bled))
    if alpha_mode == "lanczos":
        alpha_x4 = resize(alpha, MODEL_SCALE)
    else:
        alpha_x4 = run_model(model, device, np.repeat(alpha[..., None], 3, axis=2)).mean(axis=2)
    if alpha_mode == "hybrid":
        weight = soft_alpha_weight(alpha)
        alpha_x4 = (1 - weight) * alpha_x4 + weight * resize(alpha, MODEL_SCALE)
    empty = ~dilate(alpha > 0, EMPTY_MARGIN)
    empty_x4 = empty.repeat(MODEL_SCALE, axis=0).repeat(MODEL_SCALE, axis=1)
    alpha_x4 = np.where(empty_x4, 0.0, alpha_x4)
    # Opaque originals (backgrounds) stay fully opaque instead of picking up model noise in alpha.
    if alpha.min() >= 1.0:
        alpha_x4[:] = 1.0
    return np.concatenate([rgb_x4, alpha_x4[..., None]], axis=2)


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("--model", required=True, help="path to RealESRGAN_x4plus_anime_6B.pth")
    parser.add_argument("--alpha", choices=("hybrid", "model", "lanczos"), default="hybrid", help="alpha upscaling method")
    parser.add_argument("--force", action="store_true", help="redo keys whose output is newer than the original")
    parser.add_argument("--out", default=str(UPSCALED_DIR), help="output folder (default art/upscaled-x4)")
    parser.add_argument("keys", nargs="*", help="texture keys such as textures/fx (default: all)")
    args = parser.parse_args()

    out_dir = Path(args.out)
    device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
    if device.type != "cuda":
        print("warning: CUDA is not available, running on the CPU (slow)")
    model = load_model(args.model, device)

    for key in args.keys or keys_in(ORIGINAL_DIR):
        source = ORIGINAL_DIR / f"{key}.png"
        target = out_dir / f"{key}.png"
        if not args.force and target.exists() and target.stat().st_mtime >= source.stat().st_mtime:
            continue
        rgba = load_rgba(source)
        alpha_mode = ALPHA_MODE_OVERRIDES.get(key, args.alpha)
        # Fast PNG compression: these files are large, local and regenerable.
        save_rgba(target, upscale_rgba(model, device, rgba, alpha_mode), compress_level=1)
        print(f"{key}: {rgba.shape[1]}x{rgba.shape[0]} -> {rgba.shape[1] * MODEL_SCALE}x{rgba.shape[0] * MODEL_SCALE}")


if __name__ == "__main__":
    main()
