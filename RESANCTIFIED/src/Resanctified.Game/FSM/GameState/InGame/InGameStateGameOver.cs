using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.World;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateGameOver : IDrawableState, IState
    {
        private static InGameStateGameOver m_instance = new InGameStateGameOver();
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;
        public bool DisableFade;

        public static InGameStateGameOver Instance => m_instance;

        public static InGameStateGameOver GetInstance() => m_instance;

        public InGameStateGameOver()
        {
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
            this.DisableFade = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            GameApplication.GetInstance().Achievement_OneLife = false;
            ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetMiscAnimation(GameWorld.MiscAnimId.GameOver).SetTime(0.0f);
            QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, Color.White);
            GameApplication.GetInstance().CheckKillsAchievements();
            this.DisableFade = false;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).Update(gameTime);
            if (!currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).IsEndOfAnimationReached())
                return;
            GameStateInGame.GetInstance().HighscoresRequest = true;
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            GameApplication instance = GameApplication.GetInstance();
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            int score = instance.Score;
            currentWorld.GetHero().HitPoints = currentWorld.GetHero().GetHitPointsMax();
            currentWorld.GetHero().ManaPoints = currentWorld.GetHero().GetManaPointsMax();
            instance.Score = 0;
            currentWorld.SaveGameData();
            instance.Score = score;
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float num = byte.MaxValue;
            if (!this.DisableFade && stateMachine.GetCurrentStateTime() < 2.0)
                num = (float)(stateMachine.GetCurrentStateTime() / 2.0) * byte.MaxValue;
            Renderer instance = Renderer.GetInstance();
            instance.Begin2DMode(false);
            this.m_fadeColor.A = (byte)num;
            instance.DrawQuad(this.m_quadFade, VertexFormat.Position3Color, null, null, this.m_fadeColor);
            instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).GetFrame(), null, BlendFunc.SrcAlpha_InvSrcAlpha);
            instance.End2DMode();
        }
    }
}
