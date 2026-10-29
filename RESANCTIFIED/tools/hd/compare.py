"""Writes a side-by-side PNG: the original (nearest 2x) next to one or more HD versions of a crop.

Each panel is composited (straight alpha) over a dark and a light background so halos show up.
HD sources may be 2x (Content/hd) or 4x (art/upscaled-x4); 4x ones are reduced to 2x with Lanczos.

Usage: python tools/hd/compare.py textures/nun_template 0 0 128 128 out.png [hd-dir ...]
       (crop x y width height in original pixels; hd-dir defaults to Content/hd)
"""

import argparse
from pathlib import Path

from PIL import Image

from common import HD_DIR, HD_SCALE, ORIGINAL_DIR

BACKGROUNDS = ((40, 44, 52), (200, 190, 170))
GAP = 4


def crop_at_2x(path, box, original_size):
    with Image.open(path) as image:
        image = image.convert("RGBA")
        scale = image.size[0] / original_size[0]
        if scale != HD_SCALE:
            image = image.resize((original_size[0] * HD_SCALE, original_size[1] * HD_SCALE),
                                 Image.Resampling.NEAREST if scale == 1 else Image.Resampling.LANCZOS)
        x, y, w, h = (v * HD_SCALE for v in box)
        return image.crop((x, y, x + w, y + h))


def on_background(image, colour):
    background = Image.new("RGBA", image.size, colour + (255,))
    return Image.alpha_composite(background, image)


def main():
    parser = argparse.ArgumentParser(description=__doc__.splitlines()[0])
    parser.add_argument("key")
    parser.add_argument("box", nargs=4, type=int, metavar="N")
    parser.add_argument("out")
    parser.add_argument("hd_dirs", nargs="*", default=[str(HD_DIR)])
    parser.add_argument("--zoom", type=int, default=1, help="extra nearest-neighbour magnification of the sheet")
    args = parser.parse_args()

    original = ORIGINAL_DIR / f"{args.key}.png"
    with Image.open(original) as image:
        size = image.size
    panels = [crop_at_2x(original, args.box, size)]
    panels += [crop_at_2x(Path(d) / f"{args.key}.png", args.box, size) for d in args.hd_dirs]
    width, height = panels[0].size
    sheet = Image.new("RGB", (len(panels) * (width + GAP), len(BACKGROUNDS) * (height + GAP)), (255, 0, 255))
    for row, colour in enumerate(BACKGROUNDS):
        for column, panel in enumerate(panels):
            sheet.paste(on_background(panel, colour).convert("RGB"), (column * (width + GAP), row * (height + GAP)))
    Path(args.out).parent.mkdir(parents=True, exist_ok=True)
    if args.zoom > 1:
        sheet = sheet.resize((sheet.size[0] * args.zoom, sheet.size[1] * args.zoom), Image.Resampling.NEAREST)
    sheet.save(args.out)
    print(f"{args.out}: {len(panels)} panels of {width}x{height}")


if __name__ == "__main__":
    main()
