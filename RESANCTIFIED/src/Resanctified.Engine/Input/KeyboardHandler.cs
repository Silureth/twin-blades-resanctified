using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Resanctified.Engine.Input
{
    public class KeyboardHandler
    {
        private KeyboardState m_oldState;
        private KeyboardState m_curState;
        private List<Keys> m_injectedKeys = new List<Keys>();

        public void Initialize()
        {
            this.m_oldState = Keyboard.GetState();
            this.m_curState = this.m_oldState;
            this.m_injectedKeys.Clear();
        }

        public void Cleanup()
        {
        }

        public void Update(float gameTime)
        {
            this.m_oldState = this.m_curState;
            this.m_curState = Merge(Keyboard.GetState(), this.m_injectedKeys);
        }

        /// <summary>Keys held down by a script (automated checks) on top of the real keyboard.</summary>
        public void SetInjectedKeys(IEnumerable<Keys> keys)
        {
            this.m_injectedKeys.Clear();
            this.m_injectedKeys.AddRange(keys);
        }

        public static KeyboardState Merge(KeyboardState keyboard, IReadOnlyList<Keys> injectedKeys)
        {
            if (injectedKeys.Count == 0)
                return keyboard;
            List<Keys> keys = new List<Keys>(keyboard.GetPressedKeys());
            keys.AddRange(injectedKeys);
            return new KeyboardState(keys.ToArray(), keyboard.CapsLock, keyboard.NumLock);
        }

        public KeyboardState GetState() => this.m_curState;

        public bool IsKeyPressed(Keys key) => this.m_curState.IsKeyDown(key) && !this.m_oldState.IsKeyDown(key);

        public bool IsKeyReleased(Keys key) => this.m_oldState.IsKeyDown(key) && !this.m_curState.IsKeyDown(key);

        public bool IsKeyDown(Keys key) => this.m_curState.IsKeyDown(key);

        public bool IsKeyUp(Keys key) => this.m_curState.IsKeyUp(key);
    }
}
