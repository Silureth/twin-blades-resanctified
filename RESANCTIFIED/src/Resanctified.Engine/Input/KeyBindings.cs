using Microsoft.Xna.Framework.Input;

namespace Resanctified.Engine.Input
{
    /// <summary>
    /// The whole keyboard layout in one place. Gameplay keys press the same virtual pad buttons the
    /// on-screen controls press (see HudViewController.Action), so the hero logic sees no difference.
    /// Help &amp; Options &gt; Keyboard (KeyboardMenuViewController) shows this layout to the player; keep it in step.
    /// </summary>
    public static class KeyBindings
    {
        public readonly record struct Binding(Keys Key, PSSButton Button);

        public static readonly Binding[] Gameplay =
        {
            // Thumbstick directions: left/right walk, up jumps, down is unused by the hero but kept for symmetry.
            new Binding(Keys.A, PSSButton.ButtonDPadLeft),
            new Binding(Keys.Left, PSSButton.ButtonDPadLeft),
            new Binding(Keys.D, PSSButton.ButtonDPadRight),
            new Binding(Keys.Right, PSSButton.ButtonDPadRight),
            new Binding(Keys.W, PSSButton.ButtonDPadUp),
            new Binding(Keys.Up, PSSButton.ButtonDPadUp),
            new Binding(Keys.S, PSSButton.ButtonDPadDown),
            new Binding(Keys.Down, PSSButton.ButtonDPadDown),
            // Scythe button: slash. In the air it becomes the air slash, a direction held turns the hero first.
            new Binding(Keys.LeftControl, PSSButton.ButtonB),
            new Binding(Keys.RightControl, PSSButton.ButtonB),
            // Gun button: fire; holding it is autofire for the guns that have it.
            new Binding(Keys.LeftShift, PSSButton.ButtonX),
            new Binding(Keys.RightShift, PSSButton.ButtonX),
            // Weapon switch: swiping up selects the previous gun, swiping down the next one.
            new Binding(Keys.Q, PSSButton.ButtonLeftShoulder),
            new Binding(Keys.E, PSSButton.ButtonRightShoulder),
            new Binding(Keys.Tab, PSSButton.ButtonRightShoulder),
        };

        /// <summary>The phone's hardware Back key: leaves menus and pauses the game.</summary>
        public const Keys Back = Keys.Escape;

        /// <summary>Opens the in-game menu, like a gamepad's Start button.</summary>
        public const Keys Pause = Keys.P;

        /// <summary>Stands in for shaking the phone, which breaks free from a zombie's grab.</summary>
        public const Keys Shake = Keys.Space;

        /// <summary>Presses the OK or Next button of the screen on show, like a gamepad's A button (see Application).</summary>
        public const Keys Validate = Keys.Enter;

        /// <summary>The virtual pad buttons held down through the gameplay keys.</summary>
        public static PSSButton GetGameplayButtons(KeyboardState keyboard)
        {
            PSSButton buttons = PSSButton.ButtonNone;
            foreach (Binding binding in Gameplay)
            {
                if (keyboard.IsKeyDown(binding.Key))
                    buttons |= binding.Button;
            }
            return buttons;
        }

        /// <summary>Keyboard stand-ins for the pad buttons the standard navigation actions check.</summary>
        public static Keys GetKeyForButton(Buttons button)
        {
            switch (button)
            {
                case Buttons.Back:
                    return Back;
                case Buttons.Start:
                    return Pause;
                case Buttons.A:
                    return Validate;
                default:
                    return Keys.None;
            }
        }
    }
}
