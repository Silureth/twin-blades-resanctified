using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Resanctified.Engine.Input;

namespace Resanctified.Desktop
{
    /// <summary>
    /// Plays the --key presses of <see cref="DesktopDebugOptions"/>: each key is held down through
    /// <see cref="InputManager.SetInjectedKeys"/> for its hold time, exactly as if it were pressed.
    /// </summary>
    public sealed class DesktopKeyScript : GameComponent
    {
        private readonly IReadOnlyList<DesktopDebugOptions.KeyPress> m_keyPresses;
        private readonly bool[] m_held;
        private readonly List<Keys> m_heldKeys = new List<Keys>();

        public DesktopKeyScript(Microsoft.Xna.Framework.Game game, IReadOnlyList<DesktopDebugOptions.KeyPress> keyPresses)
            : base(game)
        {
            this.m_keyPresses = keyPresses;
            this.m_held = new bool[keyPresses.Count];
        }

        public override void Update(GameTime gameTime)
        {
            double time = gameTime.TotalGameTime.TotalSeconds;
            this.m_heldKeys.Clear();
            for (int index = 0; index < this.m_keyPresses.Count; ++index)
            {
                DesktopDebugOptions.KeyPress press = this.m_keyPresses[index];
                bool held = time >= press.Seconds && time < press.Seconds + press.HoldSeconds;
                if (held != this.m_held[index])
                    Console.WriteLine($"[{time:0.00}s] key {press.Key} {(held ? "down" : "up")}");
                this.m_held[index] = held;
                if (held)
                    this.m_heldKeys.Add(press.Key);
            }
            InputManager.Instance.SetInjectedKeys(this.m_heldKeys);
            base.Update(gameTime);
        }
    }
}
