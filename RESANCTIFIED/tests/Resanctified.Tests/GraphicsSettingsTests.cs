using Resanctified.Engine.Graphics;
using Resanctified.Engine.Resources;
using Xunit;

namespace Resanctified.Tests
{
    public class GraphicsSettingsTests
    {
        [Theory]
        [InlineData(TextureSet.Original, 1)]
        [InlineData(TextureSet.HD, 2)]
        [InlineData(TextureSet.HD, 4)]
        public void JsonRoundTrip(TextureSet textureSet, int renderScale)
        {
            GraphicsSettings.ParseJson(GraphicsSettings.ToJson(textureSet, renderScale), out TextureSet parsedTextureSet, out int parsedRenderScale);
            Assert.Equal(textureSet, parsedTextureSet);
            Assert.Equal(renderScale, parsedRenderScale);
        }

        [Theory]
        [InlineData("")]
        [InlineData("{\"textureSet\": \"Original\", ")]
        [InlineData("garbage")]
        [InlineData("[1, 2]")]
        [InlineData("{}")]
        [InlineData("{\"textureSet\": \"Ultra\", \"renderScale\": \"2\"}")]
        [InlineData("{\"textureSet\": \"7\", \"renderScale\": 2.5}")]
        public void CorruptOrInvalidValuesGiveDefaults(string json)
        {
            GraphicsSettings.ParseJson(json, out TextureSet textureSet, out int renderScale);
            Assert.Equal(GraphicsSettings.DEFAULT_TEXTURE_SET, textureSet);
            Assert.Equal(GraphicsSettings.DEFAULT_RENDER_SCALE, renderScale);
        }

        [Fact]
        public void ValidValuesSurviveNextToInvalidOnes()
        {
            GraphicsSettings.ParseJson("{\"textureSet\": \"original\", \"renderScale\": \"x\"}", out TextureSet textureSet, out int renderScale);
            Assert.Equal(TextureSet.Original, textureSet);
            Assert.Equal(GraphicsSettings.DEFAULT_RENDER_SCALE, renderScale);
        }

        [Fact]
        public void RenderScaleIsClamped()
        {
            GraphicsSettings.ParseJson("{\"renderScale\": 0}", out _, out int tooSmall);
            GraphicsSettings.ParseJson("{\"renderScale\": 99}", out _, out int tooLarge);
            Assert.Equal(VirtualScreen.MIN_RENDER_SCALE, tooSmall);
            Assert.Equal(VirtualScreen.MAX_RENDER_SCALE, tooLarge);
        }
    }
}
