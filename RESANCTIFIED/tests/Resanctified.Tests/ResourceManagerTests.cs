using Resanctified.Engine.Resources;
using Xunit;

namespace Resanctified.Tests
{
    public class ResourceManagerTests
    {
        [Fact]
        public void ContentPathsAreLowercaseWithForwardSlashes()
        {
            // The decompiled game names assets like "Textures\\sky_day"; game code that compares resource
            // names (GameStateLoadLevel.RemapParallaxTextures) relies on this normalized form.
            Assert.Equal("textures/sky_day", ResourceManager.NormalizeContentPath("Textures\\sky_day"));
            Assert.StartsWith("textures/texture", ResourceManager.NormalizeContentPath("Textures\\Texture_Forest_Night"));
        }
    }
}
