using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateEndLevel : IDrawableState, IState
    {
        private static InGameStateEndLevel m_instance = new InGameStateEndLevel();
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;

        public static InGameStateEndLevel Instance => m_instance;

        public static InGameStateEndLevel GetInstance() => m_instance;

        public InGameStateEndLevel()
        {
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
            GameApplication.GetInstance().CheckKillsAchievements();
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            Scenario.GetInstance().RaiseEventEndLevel(currentWorld.LevelIndex);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (stateMachine.GetCurrentStateTime() < 2.0)
                return;
            GameStateInGame.GetInstance().NextLevelRequest = true;
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
