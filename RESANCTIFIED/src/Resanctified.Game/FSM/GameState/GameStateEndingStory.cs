using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Game.UI;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateEndingStory : IDrawableState, IState
    {
        private const float ENDING_STORY_TIME_MIN = 2f;
        private const float ENDING_STORY_TIME_MAX = 8f;
        private const float ENDING_STORY_TIME_BACK = 180f;
        private static GameStateEndingStory m_instance = new GameStateEndingStory();
        private EndingStoryViewController m_endingViewController;
        private CreditsViewController m_creditsViewController;
        private float m_timer;
        private int m_endingState;
        public bool NextRequest;
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;

        public static GameStateEndingStory Instance => m_instance;

        public static GameStateEndingStory GetInstance() => m_instance;

        public int GetState() => this.m_endingState;

        public GameStateEndingStory()
        {
            this.m_endingViewController = new EndingStoryViewController();
            this.m_creditsViewController = new CreditsViewController();
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            this.m_endingState = 1;
            this.m_endingViewController.Initialize();
            this.m_creditsViewController.Initialize(true);
            Application.GetInstance().AddResponder(this.m_endingViewController);
            AudioManager.GetInstance().LoadMusic("Sounds\\music_credits");
            ResourceManager.GetInstance().LoadAllResources();
            GameApplication.GetInstance().SetSoundVolume();
            AudioManager.GetInstance().PlayMusic();
            this.m_timer = 0.0f;
            this.m_creditsViewController.SetPage(0);
            QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
            GameApplication.GetInstance().CheckEndingAchievements();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            this.m_endingViewController.Update(gameTime);
            switch (this.m_endingState)
            {
                case 0:
                    if (!this.NextRequest)
                        break;
                    this.NextRequest = false;
                    this.m_timer = 0.0f;
                    ++this.m_endingState;
                    break;
                case 1:
                    this.m_timer += gameTime;
                    if (this.m_timer < 8.0)
                    {
                        if (this.m_timer <= 2.0 && !this.NextRequest)
                            break;
                        this.NextRequest = false;
                        ++this.m_endingState;
                        break;
                    }
                    ++this.m_endingState;
                    break;
                case 2:
                    this.m_timer += gameTime;
                    if (this.m_timer < 4.0)
                        break;
                    this.m_timer = 0.0f;
                    if (this.m_creditsViewController.NextPage())
                        break;
                    ++this.m_endingState;
                    break;
                case 3:
                    this.m_timer += gameTime;
                    if (this.m_timer < 4.0)
                        break;
                    this.NextRequest = false;
                    this.m_timer = 0.0f;
                    ++this.m_endingState;
                    break;
                case 4:
                    this.m_timer += gameTime;
                    if (this.m_timer <= 180.0 && !this.NextRequest)
                        break;
                    this.m_timer = 2f;
                    ++this.m_endingState;
                    break;
                case 5:
                    if (this.m_timer > 0.0)
                    {
                        this.m_timer -= gameTime;
                        break;
                    }
                    stateMachine.SetState(GameStateMap.GetInstance());
                    break;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            AudioManager.GetInstance().StopAll();
            AudioManager.GetInstance().UnloadMusic();
            InputManager.GetInstance().ClearPadState();
            this.m_endingViewController.Cleanup();
            this.m_creditsViewController.Cleanup();
            Application.GetInstance().RemoveResponder(this.m_endingViewController);
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            this.m_endingViewController.Draw();
            if (this.m_endingState == 5)
            {
                float num = 0.0f;
                if (this.m_timer < 2.0)
                    num = this.m_timer > 0.0 ? (float)(1.0 - this.m_timer / 2.0) * byte.MaxValue : byte.MaxValue;
                Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
                instance.Begin2DMode(false);
                this.m_fadeColor.A = (byte)num;
                instance.DrawQuad(this.m_quadFade, VertexFormat.Position3Color, null, null, this.m_fadeColor);
                instance.End2DMode();
            }
            else
            {
                if (this.m_endingState != 2)
                    return;
                this.m_creditsViewController.Draw();
            }
        }
    }
}
