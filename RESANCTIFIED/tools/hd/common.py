"""Paths and helpers shared by the HD texture tools (see README.md)."""

from pathlib import Path

import numpy as np
from PIL import Image

ROOT = Path(__file__).resolve().parents[2]
CONTENT_DIR = ROOT / "Content"
ORIGINAL_DIR = ROOT / "art" / "original"
UPSCALED_DIR = ROOT / "art" / "upscaled-x4"
HD_DIR = CONTENT_DIR / "hd"

# Only these content folders hold Texture2D .xnb files (anims/levels are PSS data, sounds are audio).
TEXTURE_FOLDERS = ("textures", "fonts")

HD_SCALE = 2
MODEL_SCALE = 4


def keys_in(directory, suffix=".png"):
    """Texture keys ("textures/fx") for every file below directory, sorted."""
    return sorted(
        p.relative_to(directory).with_suffix("").as_posix()
        for p in directory.rglob("*" + suffix)
    )


def load_rgba(path):
    """Loads a PNG as a float32 HxWx4 array in 0..1 (straight alpha)."""
    with Image.open(path) as image:
        return np.asarray(image.convert("RGBA"), dtype=np.float32) / 255.0


def save_rgba(path, rgba, **png_options):
    """Saves a float HxWx4 0..1 array as an 8-bit RGBA PNG, creating folders as needed."""
    path.parent.mkdir(parents=True, exist_ok=True)
    data = np.clip(np.rint(rgba * 255.0), 0, 255).astype(np.uint8)
    Image.fromarray(data, "RGBA").save(path, **(png_options or {"optimize": True}))
