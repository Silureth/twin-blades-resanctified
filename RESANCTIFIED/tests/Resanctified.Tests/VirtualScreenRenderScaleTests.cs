using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Xunit;

namespace Resanctified.Tests
{
    public class VirtualScreenRenderScaleTests
    {
        [Fact]
        public void IntegerScaleIsMeasuredAgainstTheRenderTargetSize()
        {
            // A 960x640 target (render scale 2) is shown 1:1 in a 960x640 window and 2:1 in 1920x1280.
            Assert.True(VirtualScreen.IsIntegerScale(VirtualScreen.ComputeDestinationRectangle(960, 640), 960, 640));
            Assert.True(VirtualScreen.IsIntegerScale(VirtualScreen.ComputeDestinationRectangle(1920, 1280), 960, 640));
            Assert.False(VirtualScreen.IsIntegerScale(VirtualScreen.ComputeDestinationRectangle(1440, 960), 960, 640));
        }

        [Fact]
        public void ShrinkingIsNeverPointSampled()
        {
            Assert.False(VirtualScreen.IsIntegerScale(VirtualScreen.ComputeDestinationRectangle(480, 320), 960, 640));
            Assert.False(VirtualScreen.IsIntegerScale(Rectangle.Empty, 960, 640));
        }

        [Fact]
        public void InputMappingDoesNotDependOnRenderScale()
        {
            // Window -> virtual mapping only uses the window rectangle; the target size never enters it.
            Rectangle destination = VirtualScreen.ComputeDestinationRectangle(1920, 1280);
            Assert.Equal(new Vector2(240, 160), VirtualScreen.WindowToVirtual(new Vector2(960, 640), destination));
        }

        [Fact]
        public void RenderScaleIsClampedToSupportedRange()
        {
            Assert.Equal(1, VirtualScreen.ClampRenderScale(-3));
            Assert.Equal(3, VirtualScreen.ClampRenderScale(3));
            Assert.Equal(VirtualScreen.MAX_RENDER_SCALE, VirtualScreen.ClampRenderScale(100));
        }
    }
}
