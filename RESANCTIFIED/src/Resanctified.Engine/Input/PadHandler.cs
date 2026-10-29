using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Resanctified.Engine.Input
{
    public class PadHandler
    {
        private PlayerIndex m_playerIndex;
        private GamePadState m_padState;
        private GamePadState m_previousPadState;

        public PadHandler(PlayerIndex index) => this.m_playerIndex = index;

        public void Initialize() => this.InitializePadState();

        public void InitializePadState() => this.m_previousPadState = this.m_padState = GamePad.GetState(this.m_playerIndex, GamePadDeadZone.Circular);

        public void Cleanup()
        {
        }

        public void Update(float gameTime)
        {
            this.m_previousPadState = this.m_padState;
            this.m_padState = GamePad.GetState(this.m_playerIndex, GamePadDeadZone.Circular);
        }

        public bool IsButtonPressed(Buttons button) => this.m_previousPadState.IsButtonUp(button) && this.m_padState.IsButtonDown(button);

        public bool IsButtonReleased(Buttons button) => this.m_previousPadState.IsButtonDown(button) && this.m_padState.IsButtonUp(button);

        public bool IsButtonDown(Buttons button) => this.m_padState.IsButtonDown(button);

        public bool IsButtonUp(Buttons button) => this.m_padState.IsButtonUp(button);

        public Vector2 GetLeftThumbstickPosition() => this.m_padState.ThumbSticks.Left;

        public Vector2 GetRightThumbstickPosition() => this.m_padState.ThumbSticks.Right;

        public bool IsConnected => this.m_padState.IsConnected;
    }
}
