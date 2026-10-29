using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateEndLevelEpilogSuicide : IDrawableState, IState
    {
        private static InGameStateEndLevelEpilogSuicide m_instance = new InGameStateEndLevelEpilogSuicide();
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;
        private float m_timer;
        private int m_state;

        public static InGameStateEndLevelEpilogSuicide Instance => m_instance;

        public static InGameStateEndLevelEpilogSuicide GetInstance() => m_instance;

        public InGameStateEndLevelEpilogSuicide()
        {
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            this.m_timer = 0.0f;
            this.m_state = 0;
            currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(true);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            currentWorld.Update(gameTime);
            switch (this.m_state)
            {
                case 0:
                    this.m_timer += gameTime;
                    if (this.m_timer < 2.0)
                        break;
                    ++this.m_state;
                    this.m_timer = 0.0f;
                    AudioManager.GetInstance().StopMusic();
                    break;
                case 1:
                    this.m_timer += gameTime;
                    if (this.m_timer < 1.0)
                        break;
                    currentWorld.GetHero().GetSound(PlayableCharacter.SoundFxId.SoundFx_Gun).Play();
                    ++this.m_state;
                    this.m_timer = 0.0f;
                    break;
                case 2:
                    this.m_timer += gameTime;
                    if (this.m_timer < 3.0)
                        break;
                    currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(false);
                    GameFSM.GetInstance().SetState(GameStateStoryEpilogSuicide.GetInstance());
                    break;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            float num = byte.MaxValue;
            if (this.m_state == 0)
            {
                if (this.m_timer < 2.0)
                    num = this.m_timer / 2f * byte.MaxValue;
            }
            else
                num = byte.MaxValue;
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            this.m_fadeColor.A = (byte)num;
            instance.DrawQuad(this.m_quadFade, VertexFormat.Position3Color, null, null, this.m_fadeColor);
            instance.End2DMode();
        }
    }
}
