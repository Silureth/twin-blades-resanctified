using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Xunit;

namespace Resanctified.Tests
{
    public class VirtualScreenTests
    {
        [Fact]
        public void ExactAspectWindowFillsWholeBackBuffer()
        {
            Assert.Equal(new Rectangle(0, 0, 960, 640), VirtualScreen.ComputeDestinationRectangle(960, 640));
            Assert.Equal(new Rectangle(0, 0, 480, 320), VirtualScreen.ComputeDestinationRectangle(480, 320));
        }

        [Fact]
        public void WideWindowGetsBarsLeftAndRight()
        {
            // 1920x1080 is 16:9; a 3:2 picture 1080 high is 1620 wide, leaving 150 px on each side.
            Assert.Equal(new Rectangle(150, 0, 1620, 1080), VirtualScreen.ComputeDestinationRectangle(1920, 1080));
        }

        [Fact]
        public void TallWindowGetsBarsTopAndBottom()
        {
            // 800 wide -> 533.33 high, rounded to 533; (600 - 533) / 2 = 33.
            Assert.Equal(new Rectangle(0, 33, 800, 533), VirtualScreen.ComputeDestinationRectangle(800, 600));
        }

        [Fact]
        public void MinimizedWindowGivesEmptyRectangle()
        {
            Assert.Equal(Rectangle.Empty, VirtualScreen.ComputeDestinationRectangle(0, 0));
            Assert.Equal(Vector2.Zero, VirtualScreen.WindowToVirtual(new Vector2(10f, 10f), Rectangle.Empty));
            Assert.False(VirtualScreen.IsInside(new Vector2(0f, 0f), Rectangle.Empty));
        }

        [Fact]
        public void WindowToVirtualFlipsYAndScales()
        {
            Rectangle destination = VirtualScreen.ComputeDestinationRectangle(960, 640);
            // Window top-left is virtual top-left: (0, 320) because Y points up.
            Assert.Equal(new Vector2(0f, 320f), VirtualScreen.WindowToVirtual(new Vector2(0f, 0f), destination));
            // Window bottom-right edge is virtual (480, 0).
            Assert.Equal(new Vector2(480f, 0f), VirtualScreen.WindowToVirtual(new Vector2(960f, 640f), destination));
            Assert.Equal(new Vector2(240f, 160f), VirtualScreen.WindowToVirtual(new Vector2(480f, 320f), destination));
        }

        [Fact]
        public void WindowToVirtualAccountsForLetterboxOffset()
        {
            Rectangle destination = VirtualScreen.ComputeDestinationRectangle(1920, 1080);
            // Left edge of the picture (after the 150 px bar), vertical middle.
            Vector2 virtualPos = VirtualScreen.WindowToVirtual(new Vector2(150f, 540f), destination);
            Assert.Equal(0f, virtualPos.X, 3);
            Assert.Equal(160f, virtualPos.Y, 3);
        }

        [Fact]
        public void VirtualToWindowIsInverseOfWindowToVirtual()
        {
            Rectangle destination = VirtualScreen.ComputeDestinationRectangle(1366, 768);
            Vector2 virtualPos = new Vector2(150f, 270f);
            Vector2 roundTrip = VirtualScreen.WindowToVirtual(VirtualScreen.VirtualToWindow(virtualPos, destination), destination);
            Assert.Equal(virtualPos.X, roundTrip.X, 3);
            Assert.Equal(virtualPos.Y, roundTrip.Y, 3);
        }

        [Fact]
        public void PositionsInLetterboxBarsAreOutside()
        {
            Rectangle destination = VirtualScreen.ComputeDestinationRectangle(1920, 1080);
            Assert.False(VirtualScreen.IsInside(new Vector2(100f, 540f), destination));
            Assert.False(VirtualScreen.IsInside(new Vector2(1800f, 540f), destination));
            Assert.True(VirtualScreen.IsInside(new Vector2(150f, 540f), destination));
            Assert.True(VirtualScreen.IsInside(new Vector2(1769f, 1079f), destination));
            Assert.False(VirtualScreen.IsInside(new Vector2(1770f, 540f), destination));

            Rectangle tall = VirtualScreen.ComputeDestinationRectangle(800, 600);
            Assert.False(VirtualScreen.IsInside(new Vector2(400f, 10f), tall));
            Assert.False(VirtualScreen.IsInside(new Vector2(400f, 590f), tall));
            Assert.True(VirtualScreen.IsInside(new Vector2(400f, 300f), tall));
        }

        [Fact]
        public void ClampKeepsPositionsOnScreen()
        {
            Assert.Equal(new Vector2(0f, 320f), VirtualScreen.ClampToScreen(new Vector2(-50f, 400f)));
            Assert.Equal(new Vector2(480f, 0f), VirtualScreen.ClampToScreen(new Vector2(500f, -1f)));
            Assert.Equal(new Vector2(100f, 100f), VirtualScreen.ClampToScreen(new Vector2(100f, 100f)));
        }

        [Fact]
        public void IntegerScaleDetection()
        {
            Assert.True(VirtualScreen.IsIntegerScale(VirtualScreen.ComputeDestinationRectangle(960, 640)));
            Assert.True(VirtualScreen.IsIntegerScale(VirtualScreen.ComputeDestinationRectangle(1440, 1000)));
            Assert.False(VirtualScreen.IsIntegerScale(VirtualScreen.ComputeDestinationRectangle(1920, 1080)));
            Assert.False(VirtualScreen.IsIntegerScale(Rectangle.Empty));
        }
    }
}
