using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateExit : IDrawableState, IState
    {
        private static InGameStateExit m_instance = new InGameStateExit();
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;

        public static InGameStateExit Instance => m_instance;

        public static InGameStateExit GetInstance() => m_instance;

        public InGameStateExit()
        {
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine) => QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (stateMachine.GetCurrentStateTime() < 2.0)
                return;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
                GameStateInGame.Instance.BackToMainMenuRequest = true;
            else if (GameFSM.GetInstance().GetCurrentState() == GameStateMap.GetInstance())
            {
                GameStateMap.Instance.BackToMainMenuRequest = true;
            }
            else
            {
                if (GameFSM.GetInstance().GetCurrentState() != GameStateEndOfNight.GetInstance())
                    return;
                GameStateEndOfNight.Instance.BackToMainMenuRequest = true;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
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
