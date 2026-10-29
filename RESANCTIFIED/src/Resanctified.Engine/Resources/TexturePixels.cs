using System;

namespace Resanctified.Engine.Resources
{
    /// <summary>CPU pixel operations on tightly packed 8-bit RGBA data, used when building textures from PNGs.</summary>
    public static class TexturePixels
    {
        private const int BYTES_PER_PIXEL = 4;

        /// <summary>Straight alpha -> premultiplied alpha, in place, rounded like the XNA content pipeline.</summary>
        public static void PremultiplyAlpha(byte[] rgba)
        {
            for (int index = 0; index < rgba.Length; index += BYTES_PER_PIXEL)
            {
                int alpha = rgba[index + 3];
                if (alpha == 255)
                    continue;
                rgba[index] = (byte)((rgba[index] * alpha + 127) / 255);
                rgba[index + 1] = (byte)((rgba[index + 1] * alpha + 127) / 255);
                rgba[index + 2] = (byte)((rgba[index + 2] * alpha + 127) / 255);
            }
        }

        /// <summary>Size of the next mip level, the same way the GPU counts it.</summary>
        public static int GetHalfSize(int size) => Math.Max(1, size / 2);

        /// <summary>
        /// Next mip level of straight-alpha data with a 2x2 box filter. Colours are weighted by alpha, so the
        /// arbitrary colour stored in fully transparent pixels (magenta in most original atlases) does not
        /// bleed into the edges of sprites; a block that is fully transparent keeps the plain average.
        /// </summary>
        public static byte[] DownsampleHalf(byte[] rgba, int width, int height)
        {
            int halfWidth = GetHalfSize(width);
            int halfHeight = GetHalfSize(height);
            byte[] result = new byte[halfWidth * halfHeight * BYTES_PER_PIXEL];
            int[] block = new int[4];
            for (int y = 0; y < halfHeight; ++y)
            {
                // Clamped so a dimension of 1 (e.g. the last levels of a non-square texture) still works.
                int row0 = Math.Min(y * 2, height - 1) * width;
                int row1 = Math.Min(y * 2 + 1, height - 1) * width;
                for (int x = 0; x < halfWidth; ++x)
                {
                    int column0 = Math.Min(x * 2, width - 1);
                    int column1 = Math.Min(x * 2 + 1, width - 1);
                    block[0] = (row0 + column0) * BYTES_PER_PIXEL;
                    block[1] = (row0 + column1) * BYTES_PER_PIXEL;
                    block[2] = (row1 + column0) * BYTES_PER_PIXEL;
                    block[3] = (row1 + column1) * BYTES_PER_PIXEL;
                    int alphaSum = rgba[block[0] + 3] + rgba[block[1] + 3] + rgba[block[2] + 3] + rgba[block[3] + 3];
                    int target = (y * halfWidth + x) * BYTES_PER_PIXEL;
                    for (int channel = 0; channel < 3; ++channel)
                    {
                        int sum = 0;
                        if (alphaSum > 0)
                        {
                            for (int index = 0; index < 4; ++index)
                                sum += rgba[block[index] + channel] * rgba[block[index] + 3];
                            result[target + channel] = (byte)((sum + alphaSum / 2) / alphaSum);
                        }
                        else
                        {
                            for (int index = 0; index < 4; ++index)
                                sum += rgba[block[index] + channel];
                            result[target + channel] = (byte)((sum + 2) / 4);
                        }
                    }
                    result[target + 3] = (byte)((alphaSum + 2) / 4);
                }
            }
            return result;
        }
    }
}
