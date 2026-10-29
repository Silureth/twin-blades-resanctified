using Microsoft.Xna.Framework.Input;

namespace Resanctified.Engine.Input
{
    public class MouseHandler
    {
        private MouseState m_previousMouseState;
        private int m_deltaX;
        private int m_deltaY;
        private int m_deltaZ;

        public void Initialize() => this.m_previousMouseState = Mouse.GetState();

        public void Cleanup()
        {
        }

        public void Update(float gameTime)
        {
            MouseState state = Mouse.GetState();
            this.m_deltaX = state.X - this.m_previousMouseState.X;
            this.m_deltaY = state.Y - this.m_previousMouseState.Y;
            this.m_deltaZ = state.ScrollWheelValue - this.m_previousMouseState.ScrollWheelValue;
            this.m_previousMouseState = state;
        }
    }
}
