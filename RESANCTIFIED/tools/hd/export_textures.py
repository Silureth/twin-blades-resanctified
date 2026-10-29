"""Exports every Texture2D .xnb under Content/ to art/original/<key>.png with straight alpha.

The .xnb files are uncompressed XNA 4.0 Texture2D content in SurfaceFormat Color (RGBA8), Dxt1 or Dxt5.
Their alpha is not consistent: the Color textures are premultiplied (the XNA pipeline default) except
fonts/brad, while the DXT textures store straight alpha. A texture is treated as premultiplied when no
pixel's colour exceeds its alpha; those are un-premultiplied (exactly reversible, checked below), the rest
are written as stored. The game draws everything with straight-alpha blending (SrcAlpha, InvSrcAlpha).

Usage: python3 tools/hd/export_textures.py
"""

import struct
import sys

import numpy as np
from PIL import Image

from common import CONTENT_DIR, ORIGINAL_DIR, TEXTURE_FOLDERS

TEXTURE_READER = "Microsoft.Xna.Framework.Content.Texture2DReader"
SURFACE_FORMAT_COLOR = 0
SURFACE_FORMAT_DXT1 = 4
SURFACE_FORMAT_DXT5 = 6
FLAG_COMPRESSED_LZX = 0x80
FLAG_COMPRESSED_LZ4 = 0x40


class XnbError(Exception):
    pass


class Reader:
    def __init__(self, data):
        self.data = data
        self.pos = 0

    def bytes(self, count):
        if self.pos + count > len(self.data):
            raise XnbError("unexpected end of file")
        chunk = self.data[self.pos:self.pos + count]
        self.pos += count
        return chunk

    def u8(self):
        return self.bytes(1)[0]

    def u32(self):
        return struct.unpack("<I", self.bytes(4))[0]

    def i32(self):
        return struct.unpack("<i", self.bytes(4))[0]

    def var_int(self):
        """.NET BinaryReader.Read7BitEncodedInt."""
        value = 0
        shift = 0
        while True:
            byte = self.u8()
            value |= (byte & 0x7F) << shift
            shift += 7
            if byte < 0x80:
                return value

    def string(self):
        return self.bytes(self.var_int()).decode("utf-8")


def read_reader_names(reader):
    """Parses the XNB header up to the primary object; returns the type reader names."""
    if reader.bytes(3) != b"XNB":
        raise XnbError("not an XNB file")
    reader.u8()  # target platform ('m' = Windows Phone)
    version = reader.u8()
    if version != 5:
        raise XnbError(f"unsupported XNB version {version}")
    flags = reader.u8()
    if flags & (FLAG_COMPRESSED_LZX | FLAG_COMPRESSED_LZ4):
        raise XnbError(f"compressed XNB (flags 0x{flags:02x}) is not supported")
    file_size = reader.u32()
    if file_size != len(reader.data):
        raise XnbError(f"header size {file_size} != actual size {len(reader.data)}")
    names = []
    for _ in range(reader.var_int()):
        names.append(reader.string().split(",")[0])
        reader.i32()  # reader version
    return names


def expand_565(colour):
    """RGB565 uint16 array -> float RGB 0..255 (bit replication, as GPUs do)."""
    r = (colour >> 11) & 0x1F
    g = (colour >> 5) & 0x3F
    b = colour & 0x1F
    return np.stack([(r << 3) | (r >> 2), (g << 2) | (g >> 4), (b << 3) | (b >> 2)], axis=-1).astype(np.float64)


def decode_colour_blocks(blocks, allow_punch_through):
    """Decodes the 8-byte BC1 colour part of N blocks into N x 16 x 4 RGBA (alpha 255 or punch-through 0)."""
    c0 = blocks[:, 0].astype(np.uint16) | (blocks[:, 1].astype(np.uint16) << 8)
    c1 = blocks[:, 2].astype(np.uint16) | (blocks[:, 3].astype(np.uint16) << 8)
    e0, e1 = expand_565(c0), expand_565(c1)
    four_colour = (c0 > c1) | (not allow_punch_through)
    palette = np.empty((len(blocks), 4, 4))
    palette[:, 0, :3] = e0
    palette[:, 1, :3] = e1
    palette[:, 2, :3] = np.where(four_colour[:, None], (2 * e0 + e1) / 3, (e0 + e1) / 2)
    palette[:, 3, :3] = np.where(four_colour[:, None], (e0 + 2 * e1) / 3, 0)
    palette[:, :, 3] = 255
    palette[:, 3, 3] = np.where(four_colour, 255, 0)
    bits = blocks[:, 4:8].astype(np.uint32)
    indices = bits[:, 0] | (bits[:, 1] << 8) | (bits[:, 2] << 16) | (bits[:, 3] << 24)
    index = (indices[:, None] >> (2 * np.arange(16, dtype=np.uint32))) & 3
    return np.take_along_axis(palette, index[:, :, None].astype(np.int64), axis=1)


def decode_alpha_blocks(blocks):
    """Decodes the 8-byte BC3 alpha part of N blocks into N x 16 alpha values."""
    a0 = blocks[:, 0].astype(np.float64)
    a1 = blocks[:, 1].astype(np.float64)
    palette = np.empty((len(blocks), 8))
    palette[:, 0], palette[:, 1] = a0, a1
    eight = a0 > a1
    for i in range(1, 7):
        palette[:, i + 1] = np.where(eight, ((7 - i) * a0 + i * a1) / 7,
                                     ((5 - i) * a0 + i * a1) / 5 if i < 5 else (0 if i == 5 else 255))
    bits = np.zeros(len(blocks), dtype=np.uint64)
    for i in range(6):
        bits |= blocks[:, 2 + i].astype(np.uint64) << np.uint64(8 * i)
    index = (bits[:, None] >> (np.uint64(3) * np.arange(16, dtype=np.uint64))) & np.uint64(7)
    return np.take_along_axis(palette, index.astype(np.int64), axis=1)


def decode_dxt(data, width, height, surface_format):
    block_size = 8 if surface_format == SURFACE_FORMAT_DXT1 else 16
    blocks_x, blocks_y = max(1, (width + 3) // 4), max(1, (height + 3) // 4)
    blocks = np.frombuffer(data, dtype=np.uint8).reshape(-1, block_size)
    if surface_format == SURFACE_FORMAT_DXT1:
        texels = decode_colour_blocks(blocks, allow_punch_through=True)
    else:
        texels = decode_colour_blocks(blocks[:, 8:], allow_punch_through=False)
        texels[:, :, 3] = decode_alpha_blocks(blocks[:, :8])
    image = texels.reshape(blocks_y, blocks_x, 4, 4, 4).transpose(0, 2, 1, 3, 4)
    image = image.reshape(blocks_y * 4, blocks_x * 4, 4)[:height, :width]
    return np.clip(np.rint(image), 0, 255).astype(np.uint8)


def decode_texture(data):
    """Returns the top mip of a Texture2D .xnb as an HxWx4 uint8 RGBA array (alpha as stored)."""
    reader = Reader(data)
    names = read_reader_names(reader)
    if reader.var_int() != 0:
        raise XnbError("shared resources are not expected in a texture")
    type_index = reader.var_int()
    if type_index == 0 or names[type_index - 1] != TEXTURE_READER:
        raise XnbError(f"not a Texture2D (readers: {names})")
    surface_format = reader.i32()
    width = reader.u32()
    height = reader.u32()
    if reader.u32() < 1:
        raise XnbError("texture has no mip levels")
    size = reader.u32()
    if surface_format == SURFACE_FORMAT_COLOR:
        expected = width * height * 4
    elif surface_format in (SURFACE_FORMAT_DXT1, SURFACE_FORMAT_DXT5):
        expected = ((width + 3) // 4) * ((height + 3) // 4) * (8 if surface_format == SURFACE_FORMAT_DXT1 else 16)
    else:
        raise XnbError(f"SurfaceFormat {surface_format} is not supported (only Color, Dxt1, Dxt5)")
    if size != expected:
        raise XnbError(f"mip 0 is {size} bytes, expected {expected}")
    if surface_format == SURFACE_FORMAT_COLOR:
        return np.frombuffer(reader.bytes(size), dtype=np.uint8).reshape(height, width, 4)
    return decode_dxt(reader.bytes(size), width, height, surface_format)


def unpremultiply(premultiplied):
    rgb = premultiplied[..., :3].astype(np.float64)
    alpha = premultiplied[..., 3:].astype(np.float64)
    straight = np.where(alpha > 0, rgb * 255.0 / np.maximum(alpha, 1), 0)
    straight = np.clip(np.rint(straight), 0, 255).astype(np.uint8)
    return np.concatenate([straight, premultiplied[..., 3:]], axis=-1)


def premultiply(straight):
    rgb = straight[..., :3].astype(np.float64)
    alpha = straight[..., 3:].astype(np.float64)
    return np.concatenate([np.rint(rgb * alpha / 255.0).astype(np.uint8), straight[..., 3:]], axis=-1)


def find_texture_files():
    files = []
    for path in sorted(CONTENT_DIR.rglob("*.xnb")):
        relative = path.relative_to(CONTENT_DIR)
        if relative.parts[0] == "hd":
            continue
        reader = Reader(path.read_bytes())
        is_texture = TEXTURE_READER in read_reader_names(reader)
        in_texture_folder = relative.parts[0] in TEXTURE_FOLDERS
        if is_texture and not in_texture_folder:
            raise XnbError(f"{relative}: Texture2D outside {TEXTURE_FOLDERS}; add its folder to TEXTURE_FOLDERS")
        if is_texture:
            files.append(path)
    return files


def is_premultiplied(pixels):
    return bool((pixels[..., 3] < 255).any() and (pixels[..., :3].max(axis=2) <= pixels[..., 3]).all())


def main():
    files = find_texture_files()
    for path in files:
        key = path.relative_to(CONTENT_DIR).with_suffix("").as_posix().lower()
        try:
            pixels = decode_texture(path.read_bytes())
        except XnbError as error:
            sys.exit(f"{key}: {error}")
        alpha_mode = "premultiplied" if is_premultiplied(pixels) else "straight"
        if alpha_mode == "premultiplied":
            straight = unpremultiply(pixels)
            if not np.array_equal(premultiply(straight), pixels):
                sys.exit(f"{key}: un-premultiplied pixels do not round-trip")
        else:
            straight = pixels
        out = ORIGINAL_DIR / f"{key}.png"
        out.parent.mkdir(parents=True, exist_ok=True)
        Image.fromarray(straight, "RGBA").save(out, optimize=True)
        print(f"{key}: {straight.shape[1]}x{straight.shape[0]}, stored {alpha_mode}")
    print(f"exported {len(files)} textures to {ORIGINAL_DIR}")


if __name__ == "__main__":
    main()
