using Microsoft.Xna.Framework.Input;
using Resanctified.Engine.Input;
using Xunit;

namespace Resanctified.Tests
{
    public class KeyBindingsTests
    {
        [Theory]
        [InlineData(Keys.A, PSSButton.ButtonDPadLeft)]
        [InlineData(Keys.Left, PSSButton.ButtonDPadLeft)]
        [InlineData(Keys.D, PSSButton.ButtonDPadRight)]
        [InlineData(Keys.Right, PSSButton.ButtonDPadRight)]
        [InlineData(Keys.W, PSSButton.ButtonDPadUp)]
        [InlineData(Keys.Up, PSSButton.ButtonDPadUp)]
        [InlineData(Keys.S, PSSButton.ButtonDPadDown)]
        [InlineData(Keys.Down, PSSButton.ButtonDPadDown)]
        [InlineData(Keys.LeftControl, PSSButton.ButtonB)]
        [InlineData(Keys.RightControl, PSSButton.ButtonB)]
        [InlineData(Keys.LeftShift, PSSButton.ButtonX)]
        [InlineData(Keys.RightShift, PSSButton.ButtonX)]
        [InlineData(Keys.Q, PSSButton.ButtonLeftShoulder)]
        [InlineData(Keys.E, PSSButton.ButtonRightShoulder)]
        [InlineData(Keys.Tab, PSSButton.ButtonRightShoulder)]
        public void GameplayKeyPressesTheSameButtonAsTheTouchControl(Keys key, PSSButton expected)
        {
            Assert.Equal(expected, KeyBindings.GetGameplayButtons(new KeyboardState(key)));
        }

        [Fact]
        public void HeldKeysCombine()
        {
            KeyboardState keyboard = new KeyboardState(Keys.D, Keys.W, Keys.LeftControl);
            Assert.Equal(PSSButton.ButtonDPadRight | PSSButton.ButtonDPadUp | PSSButton.ButtonB, KeyBindings.GetGameplayButtons(keyboard));
        }

        [Fact]
        public void NonGameplayKeysPressNoButton()
        {
            KeyboardState keyboard = new KeyboardState(KeyBindings.Back, KeyBindings.Pause, KeyBindings.Shake, KeyBindings.Validate, KeyBindings.Screenshot);
            Assert.Equal(PSSButton.ButtonNone, KeyBindings.GetGameplayButtons(keyboard));
        }

        [Fact]
        public void NavigationButtonsHaveKeyboardStandIns()
        {
            Assert.Equal(Keys.Escape, KeyBindings.GetKeyForButton(Buttons.Back));
            Assert.Equal(Keys.P, KeyBindings.GetKeyForButton(Buttons.Start));
            Assert.Equal(Keys.Enter, KeyBindings.GetKeyForButton(Buttons.A));
            Assert.Equal(Keys.None, KeyBindings.GetKeyForButton(Buttons.Y));
        }

        [Fact]
        public void InjectedKeysAddToTheRealKeyboard()
        {
            KeyboardState merged = KeyboardHandler.Merge(new KeyboardState(Keys.A), new[] { Keys.LeftShift });
            Assert.True(merged.IsKeyDown(Keys.A));
            Assert.True(merged.IsKeyDown(Keys.LeftShift));
            Assert.False(merged.IsKeyDown(Keys.D));
        }
    }
}
