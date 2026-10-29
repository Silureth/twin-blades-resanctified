"""Builds the shipped HD pack: Content/hd/<key>.png at exactly 2x the original size, plus manifest.json.

Each art/upscaled-x4/<key>.png is reduced to 2x with Lanczos, per channel in straight alpha (the colour of
transparent pixels was bled from the art by upscale.py, so no dark fringes are mixed in).

Usage: python tools/hd/build_pack.py
"""

import json
import shutil
import sys

import numpy as np
from PIL import Image

from common import HD_DIR, HD_SCALE, ORIGINAL_DIR, UPSCALED_DIR, keys_in, load_rgba, save_rgba

# Describes upscale.py's defaults; update it when the model or settings change.
GENERATOR = ("Real-ESRGAN RealESRGAN_x4plus_anime_6B x4 (fp16, 256px tiles), colour bled into transparent "
             "pixels, low-frequency colour restored (sigma 1), alpha: model on hard edges + Lanczos on soft "
             "alpha (fonts/brad: model only); Lanczos 4x->2x")


def resize_rgba(rgba, width, height):
    channels = []
    for index in range(4):
        image = Image.fromarray(np.ascontiguousarray(rgba[..., index]), "F")
        channels.append(np.asarray(image.resize((width, height), Image.Resampling.LANCZOS)))
    return np.clip(np.stack(channels, axis=2), 0, 1)


def main():
    keys = keys_in(ORIGINAL_DIR)
    missing = [key for key in keys if not (UPSCALED_DIR / f"{key}.png").exists()]
    if missing:
        sys.exit(f"missing upscaled textures (run upscale.py first): {', '.join(missing)}")

    if HD_DIR.exists():
        shutil.rmtree(HD_DIR)
    total = 0
    for key in keys:
        with Image.open(ORIGINAL_DIR / f"{key}.png") as original:
            width, height = original.size[0] * HD_SCALE, original.size[1] * HD_SCALE
        rgba = load_rgba(UPSCALED_DIR / f"{key}.png")
        if rgba.shape[1] < width or rgba.shape[0] < height:
            sys.exit(f"{key}: upscaled image is smaller than {width}x{height}; rerun upscale.py --force {key}")
        target = HD_DIR / f"{key}.png"
        save_rgba(target, resize_rgba(rgba, width, height))
        total += target.stat().st_size

    manifest = {"scale": HD_SCALE, "generator": GENERATOR, "textures": keys}
    (HD_DIR / "manifest.json").write_text(json.dumps(manifest, indent=2) + "\n", encoding="utf-8")
    print(f"{len(keys)} textures, {total / 1024 / 1024:.1f} MiB in {HD_DIR}")


if __name__ == "__main__":
    main()
