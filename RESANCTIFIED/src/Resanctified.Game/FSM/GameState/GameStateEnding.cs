using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Game.FSM.GameState.MainMenu;
using Resanctified.Game.UI;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateEnding : IDrawableState, IState
    {
        private const float ENDING_TIME_MIN = 2f;
        private const float ENDING_TIME_MAX = 8f;
        private const float ENDING_TIME_BACK = 180f;
        private static GameStateEnding m_instance = new GameStateEnding();
        private EndingViewController m_endingViewController;
        private CreditsViewController m_creditsViewController;
        private float m_timer;
        private int m_endingState;
        public bool NextRequest;
        public bool DisplayThankYouRequest;
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;

        public static GameStateEnding Instance => m_instance;

        public static GameStateEnding GetInstance() => m_instance;

        public int GetState() => this.m_endingState;

        public GameStateEnding()
        {
            this.m_endingViewController = new EndingViewController();
            this.m_creditsViewController = new CreditsViewController();
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            this.DisplayThankYouRequest = false;
            this.m_endingState = 0;
            this.m_endingViewController.Initialize();
            this.m_creditsViewController.Initialize(true);
            AudioManager.GetInstance().LoadMusic("Sounds\\music_credits");
            ResourceManager.GetInstance().LoadAllResources();
            GameApplication.GetInstance().SetSoundVolume();
            AudioManager.GetInstance().PlayMusic();
            this.m_timer = 4f;
            this.m_creditsViewController.SetPage(0);
            Application.GetInstance().AddResponder(this.m_endingViewController);
            QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
            GameApplication.GetInstance().CheckEndingAchievements();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            this.m_endingViewController.Update(gameTime);
            if (stateMachine.GetCurrentStateTime() < 2.0 || stateMachine.GetCurrentStateTime() < 8.0 && !this.NextRequest)
                return;
            this.m_timer -= gameTime;
            if (this.m_timer <= 0.0 && this.m_endingState < 2)
            {
                this.m_timer = 4f;
                switch (this.m_endingState)
                {
                    case 0:
                        if (!this.m_creditsViewController.NextPage())
                        {
                            this.DisplayThankYouRequest = true;
                            ++this.m_endingState;
                            break;
                        }
                        break;
                    case 1:
                        this.DisplayThankYouRequest = false;
                        ++this.m_endingState;
                        this.NextRequest = false;
                        break;
                }
            }
            if (this.m_endingState == 2)
            {
                if (!this.NextRequest && stateMachine.GetCurrentStateTime() <= 180.0)
                    return;
                ++this.m_endingState;
                this.m_timer = 2f;
            }
            else
            {
                if (this.m_endingState != 3 || this.m_timer > 0.0)
                    return;
                stateMachine.SetState(GameStateMainMenu.GetInstance());
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                {
                    GameStateMainMenu.GetInstance().GetSubStateMachine().SetState(MainMenuStateHighscoresStory.GetInstance());
                    GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = true;
                }
                else
                {
                    GameStateMainMenu.GetInstance().GetSubStateMachine().SetState(MainMenuStateHighscoresSurvival.GetInstance());
                    GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = false;
                }
                int num = GameApplication.GetInstance().UpdateHighscores();
                GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(num + 1);
                if (num >= 5)
                    return;
                GameApplication.Instance.SaveHighscores();
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            AudioManager.GetInstance().StopAll();
            AudioManager.GetInstance().UnloadMusic();
            InputManager.GetInstance().ClearPadState();
            this.m_endingViewController.Cleanup();
            this.m_creditsViewController.Cleanup();
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            this.m_endingViewController.Draw();
            if (this.m_endingState == 3)
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
                if (stateMachine.GetCurrentStateTime() < 2.0 || stateMachine.GetCurrentStateTime() < 8.0 && !this.NextRequest || this.m_endingState > 0)
                    return;
                this.m_creditsViewController.Draw();
            }
        }
    }
}
