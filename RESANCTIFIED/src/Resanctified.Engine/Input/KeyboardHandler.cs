using Microsoft.Xna.Framework.Input;

namespace Resanctified.Engine.Input
{
    public class KeyboardHandler
    {
        private KeyboardState m_oldState;
        private KeyboardState m_curState;

        public void Initialize()
        {
            this.m_oldState = Keyboard.GetState();
            this.m_curState = this.m_oldState;
        }

        public void Cleanup()
        {
        }

        public void Update(float gameTime)
        {
            this.m_oldState = this.m_curState;
            this.m_curState = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key) => this.m_curState.IsKeyDown(key) && !this.m_oldState.IsKeyDown(key);

        public bool IsKeyReleased(Keys key) => this.m_oldState.IsKeyDown(key) && !this.m_curState.IsKeyDown(key);

        public bool IsKeyDown(Keys key) => this.m_curState.IsKeyDown(key);

        public bool IsKeyUp(Keys key) => this.m_curState.IsKeyUp(key);
    }
}
