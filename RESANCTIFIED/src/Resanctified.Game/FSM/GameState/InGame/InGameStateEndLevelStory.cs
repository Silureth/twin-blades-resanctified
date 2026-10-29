using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateEndLevelStory : IDrawableState, IState
    {
        private static InGameStateEndLevelStory m_instance = new InGameStateEndLevelStory();
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;
        public float m_timer;
        private int m_state;

        public static InGameStateEndLevelStory Instance => m_instance;

        public static InGameStateEndLevelStory GetInstance() => m_instance;

        public InGameStateEndLevelStory()
        {
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
            GameApplication.GetInstance().CheckKillsAchievements();
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            this.m_timer = 0.0f;
            this.m_state = 0;
            currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(true);
            if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Normal)
                return;
            this.m_state = 1;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            currentWorld.Update(gameTime);
            if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
                return;
            switch (this.m_state)
            {
                case 0:
                    if (stateMachine.CurrentStateTime < 2.0)
                        break;
                    ++this.m_state;
                    break;
                case 1:
                    Scenario.GetInstance().RaiseEventEndLevel(currentWorld.LevelIndex);
                    ++this.m_state;
                    break;
                case 2:
                    this.m_timer += gameTime;
                    if (this.m_timer < 2.0)
                        break;
                    currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(false);
                    GameStateInGame.GetInstance().NextLevelRequest = true;
                    break;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            if (this.m_state != 2)
                return;
            float num = byte.MaxValue;
            if (this.m_timer < 2.0)
                num = this.m_timer / 2f * byte.MaxValue;
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            this.m_fadeColor.A = (byte)num;
            instance.DrawQuad(this.m_quadFade, VertexFormat.Position3Color, null, null, this.m_fadeColor);
            instance.End2DMode();
        }
    }
}
