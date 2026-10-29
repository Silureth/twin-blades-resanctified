using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateStartGame : IDrawableState, IState
    {
        private static MainMenuStateStartGame m_instance = new MainMenuStateStartGame();
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;

        public static MainMenuStateStartGame Instance => m_instance;

        public static MainMenuStateStartGame GetInstance() => m_instance;

        public MainMenuStateStartGame()
        {
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine) => QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
            {
                stateMachine.SetState(MainMenuStateMenu.GetInstance());
            }
            else
            {
                if (stateMachine.GetCurrentStateTime() < 2.0)
                    return;
                GameStateMainMenu.Instance.StartGameRequest = true;
                GameStateMainMenu.Instance.ContinueGameRequest = false;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            GameStateMainMenu.GetInstance().GetDifficultyView().Draw();
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
