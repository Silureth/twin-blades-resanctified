# HD textures

The game's art ships as `.xnb` textures, 256 to 1024 px each. These tools produce an HD pack at twice
that resolution. The game loads the pack when HD is on and falls back to the `.xnb` files when it is off.

| Folder | Contents | In git |
|---|---|---|
| `art/original/<key>.png` | The original textures as editable PNGs: straight alpha, same size as the `.xnb` | yes |
| `art/upscaled-x4/<key>.png` | Raw 4x Real-ESRGAN output | no (regenerable, about 1 GB) |
| `Content/hd/<key>.png` + `manifest.json` | The shipped pack: exactly 2x, straight alpha | yes |

A `<key>` is the lowercase content path without its extension, for example `textures/fx` or `fonts/brad`.
All 73 `Texture2D` assets (71 in `textures/`, 2 in `fonts/`) are covered. The `.xnb` files in `anims/` and
`levels/` hold mesh data, not textures.

## Setup (once)

You need Python 3.12 and an NVIDIA GPU with a recent driver (WSL works). The CUDA 12.8 wheels run on any
driver from 570 up.

```sh
python3 -m venv ~/hdvenv
~/hdvenv/bin/pip install torch torchvision --index-url https://download.pytorch.org/whl/cu128
~/hdvenv/bin/pip install spandrel pillow numpy
curl -LO https://github.com/xinntao/Real-ESRGAN/releases/download/v0.2.2.4/RealESRGAN_x4plus_anime_6B.pth
sha256sum RealESRGAN_x4plus_anime_6B.pth
# f872d837d3c90ed2e05227bed711af5671a6fd1c9f7d7e91c911a61f155e99da
```

Install torchvision from the same CUDA index as torch. The PyPI build fails to load with
`operator torchvision::nms does not exist`.

The pack was made with torch 2.11.0+cu128, spandrel 0.4.2 and an RTX 3080 Ti.

## Commands (run from `RESANCTIFIED/`)

```sh
~/hdvenv/bin/python tools/hd/export_textures.py        # Content/*.xnb -> art/original   (~1 min, no GPU)
~/hdvenv/bin/python tools/hd/upscale.py --model RealESRGAN_x4plus_anime_6B.pth   # -> art/upscaled-x4
~/hdvenv/bin/python tools/hd/build_pack.py             # -> Content/hd + manifest.json
~/hdvenv/bin/python tools/hd/compare.py textures/nun_template 100 0 70 50 /tmp/nun.png --zoom 2
```

- `upscale.py` skips keys whose output is newer than the original. Pass `--force` or list keys to redo them.
- `compare.py` writes an image with the original (nearest-neighbour 2x) beside the HD version, over a dark and a light background.

## Editing art

1. Edit or replace `art/original/<key>.png`. Keep the size and the layout, because the animation meshes
   address the texture with normalized coordinates.
2. Run `upscale.py <key>`, then `build_pack.py`.

Don't rerun `export_textures.py` after editing: it overwrites `art/original` from the `.xnb` files. The
`.xnb` files themselves don't change. If an edited original should also be used in non-HD mode, it has to
be converted back to `.xnb` separately.

## How the textures are made

**Decoding.** The `.xnb` files are uncompressed XNA 4.0 textures in three formats:

| Format | Count | Alpha |
|---|---|---|
| Dxt5 | 53 | straight |
| Color | 14 | premultiplied, except `fonts/brad`, which is straight |
| Dxt1 | 6 | opaque, or 1-bit alpha |

A texture counts as premultiplied when no pixel's colour exceeds its alpha. Those are un-premultiplied,
and the exporter checks that the round trip is exact. The game draws everything with straight-alpha
blending (SrcAlpha, InvSrcAlpha). The HD PNGs therefore must not be premultiplied when they are loaded.

**Model.** The model is `RealESRGAN_x4plus_anime_6B`, chosen for the flat, hand-painted cartoon art. It
also removes the DXT block artifacts. It runs in fp16 on 256 px tiles with 16 px of overlap and a
replicated image border.

**Colour.** Before upscaling, the colour of fully transparent pixels is filled from the nearest visible
pixels: 4 rings of neighbour averaging, then a push-pull pyramid. Otherwise the model blends sprite edges
with the black stored there, which gives dark halos.

The anime model also flattens low-contrast painted texture, such as the moss tint on gravestones or grass
shading. To counter that, the blurred difference (sigma of 1 original pixel) between the original and the
output reduced to 1x is added back. This restores the original colours at low frequency and keeps the
model's sharp detail.

**Alpha.** Two methods were compared on the sprite atlases:
- Running the model on alpha (replicated to 3 channels) gives crisp, clean sprite outlines. It also turns
  soft glows (the rail beam, the halo ring, lightning, motion blur) into hard bands.
- Lanczos keeps the glows but blurs every sprite outline.

The default, `--alpha hybrid`, picks between them per pixel. The selector is the share of partially
transparent pixels in a 5x5 window of the original:
- below 0.25 (antialiased hard edges), it uses the model;
- above 0.55 (glows), it uses Lanczos;
- in between, it blends the two smoothly.

`fonts/brad` always uses the model. Its strokes are so thin that the hybrid selector reads them as soft
alpha and blurs them.

Pixels that were transparent and at least 2 original pixels away from any visible pixel are forced back to
alpha 0. This keeps ringing from leaking into neighbouring atlas cells or font glyphs.

**Fonts.** Both glyph atlases go through the same model path. Glyph shapes stay intact and the edges come
out sharper than with Lanczos, so no separate method is needed.

**Final size.** The 4x output is reduced to exactly 2x with Lanczos, channel by channel, in straight alpha.
