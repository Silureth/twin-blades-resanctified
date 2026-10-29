using System.Collections.Generic;
using Resanctified.Engine.Resources;
using Xunit;

namespace Resanctified.Tests
{
    public class HdTexturePackTests
    {
        [Fact]
        public void ManifestGivesScaleAndNormalizedKeys()
        {
            string json = "{\"scale\": 2, \"generator\": \"test\", \"textures\": [\"textures/fx\", \"Fonts\\\\Brad\"]}";
            Assert.True(HdTexturePack.TryParseManifest(json, out HdTexturePack.Manifest manifest));
            Assert.Equal(2, manifest.Scale);
            Assert.Equal(new HashSet<string> { "textures/fx", "fonts/brad" }, manifest.Textures);
            Assert.Empty(manifest.Premultiplied);
        }

        [Fact]
        public void ManifestReadsOptionalPremultipliedList()
        {
            string json = "{\"scale\": 2, \"textures\": [\"fonts/charlemagnestd50_0\"], \"premultiplied\": [\"Fonts/CharlemagneStd50_0\"]}";
            Assert.True(HdTexturePack.TryParseManifest(json, out HdTexturePack.Manifest manifest));
            Assert.Equal(new HashSet<string> { "fonts/charlemagnestd50_0" }, manifest.Premultiplied);
        }

        [Theory]
        [InlineData("")]
        [InlineData("not json")]
        [InlineData("[]")]
        [InlineData("{\"textures\": [\"textures/fx\"]}")]
        [InlineData("{\"scale\": 1, \"textures\": [\"textures/fx\"]}")]
        [InlineData("{\"scale\": \"2\", \"textures\": [\"textures/fx\"]}")]
        [InlineData("{\"scale\": 2.5, \"textures\": [\"textures/fx\"]}")]
        [InlineData("{\"scale\": 2}")]
        [InlineData("{\"scale\": 2, \"textures\": []}")]
        [InlineData("{\"scale\": 2, \"textures\": [3]}")]
        [InlineData("{\"scale\": 2, \"textures\": [\"\"]}")]
        [InlineData("{\"scale\": 2, \"textures\": [\"textures/fx\"], \"premultiplied\": \"textures/fx\"}")]
        public void InvalidManifestsAreRejected(string json)
        {
            Assert.False(HdTexturePack.TryParseManifest(json, out _));
        }

        [Fact]
        public void LogicalSizeIsHdSizeDividedByScale()
        {
            Assert.True(HdTexturePack.TryGetLogicalSize(2048, 1024, 2, out int width, out int height));
            Assert.Equal(1024, width);
            Assert.Equal(512, height);
        }

        [Theory]
        [InlineData(1025, 1024)]
        [InlineData(1024, 1023)]
        [InlineData(1, 2)]
        public void SizesThatAreNotAMultipleOfTheScaleAreRejected(int pixelWidth, int pixelHeight)
        {
            Assert.False(HdTexturePack.TryGetLogicalSize(pixelWidth, pixelHeight, 2, out _, out _));
        }
    }
}
