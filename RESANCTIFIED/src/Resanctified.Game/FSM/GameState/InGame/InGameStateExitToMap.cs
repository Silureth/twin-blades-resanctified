using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateExitToMap : IDrawableState, IState
    {
        private static InGameStateExitToMap m_instance = new InGameStateExitToMap();
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;

        public static InGameStateExitToMap Instance => m_instance;

        public static InGameStateExitToMap GetInstance() => m_instance;

        public InGameStateExitToMap()
        {
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine) => QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (stateMachine.GetCurrentStateTime() < 2.0)
                return;
            GameStateInGame.Instance.BackToMap = true;
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            float num = byte.MaxValue;
            if (stateMachine.GetCurrentStateTime() < 2.0)
                num = (float)(stateMachine.GetCurrentStateTime() / 2.0) * byte.MaxValue;
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            this.m_fadeColor.A = (byte)num;
            instance.DrawQuad(this.m_quadFade, VertexFormat.Position3Color, null, null, this.m_fadeColor);
            instance.End2DMode();
        }
    }
}
