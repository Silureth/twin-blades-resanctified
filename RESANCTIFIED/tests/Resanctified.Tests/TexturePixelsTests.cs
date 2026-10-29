using Resanctified.Engine.Resources;
using Xunit;

namespace Resanctified.Tests
{
    public class TexturePixelsTests
    {
        [Fact]
        public void PremultiplyScalesColourByAlpha()
        {
            byte[] rgba =
            {
                255, 128, 0, 255,
                255, 128, 10, 128,
                200, 100, 50, 0,
            };
            TexturePixels.PremultiplyAlpha(rgba);
            Assert.Equal(new byte[]
            {
                255, 128, 0, 255,
                128, 64, 5, 128,
                0, 0, 0, 0,
            }, rgba);
        }

        [Fact]
        public void DownsampleAveragesEachTwoByTwoBlock()
        {
            // 4x2 image: left block alternates transparent black and grey 100 at alpha 100, right block is uniform.
            byte[] rgba =
            {
                0, 0, 0, 0,          100, 100, 100, 100,  40, 40, 40, 40,  40, 40, 40, 40,
                100, 100, 100, 100,  0, 0, 0, 0,          40, 40, 40, 40,  40, 40, 40, 40,
            };
            byte[] half = TexturePixels.DownsampleHalf(rgba, 4, 2);
            Assert.Equal(new byte[] { 100, 100, 100, 50, 40, 40, 40, 40 }, half);
        }

        [Fact]
        public void DownsampleOfASingleRowOrColumnClampsAtTheEdge()
        {
            byte[] rgba = { 10, 20, 30, 40, 30, 40, 50, 60 };
            Assert.Equal(new byte[] { 22, 32, 42, 50 }, TexturePixels.DownsampleHalf(rgba, 2, 1));
            Assert.Equal(new byte[] { 22, 32, 42, 50 }, TexturePixels.DownsampleHalf(rgba, 1, 2));
            Assert.Equal(new byte[] { 10, 20, 30, 40 }, TexturePixels.DownsampleHalf(new byte[] { 10, 20, 30, 40 }, 1, 1));
        }

        [Fact]
        public void DownsampleWeightsColourByAlpha()
        {
            // One opaque red pixel among three transparent magenta ones: the colour stays red,
            // only the coverage drops, so no magenta fringe appears at sprite edges in smaller mips.
            byte[] rgba =
            {
                255, 0, 0, 255,  255, 0, 255, 0,
                255, 0, 255, 0,  255, 0, 255, 0,
            };
            Assert.Equal(new byte[] { 255, 0, 0, 64 }, TexturePixels.DownsampleHalf(rgba, 2, 2));
        }

        [Fact]
        public void FullyTransparentBlockKeepsPlainAverageColour()
        {
            byte[] rgba =
            {
                100, 0, 0, 0,  200, 0, 0, 0,
                100, 0, 0, 0,  200, 0, 0, 0,
            };
            Assert.Equal(new byte[] { 150, 0, 0, 0 }, TexturePixels.DownsampleHalf(rgba, 2, 2));
        }

        [Fact]
        public void MipChainSizesMatchTheGpu()
        {
            Assert.Equal(512, TexturePixels.GetHalfSize(1024));
            Assert.Equal(1, TexturePixels.GetHalfSize(1));
            Assert.Equal(1, TexturePixels.GetHalfSize(3));
        }
    }
}
