using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStatePressStart : IDrawableState, IState
    {
        private const float SPLASH_WAIT_TIME = 5f;
        public bool StartRequest;
        private static MainMenuStatePressStart m_instance = new MainMenuStatePressStart();

        public static MainMenuStatePressStart Instance => m_instance;

        public static MainMenuStatePressStart GetInstance() => m_instance;

        private MainMenuStatePressStart() => this.StartRequest = false;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.StartRequest = false;
            GameApplication.Instance.LoadSettings();
            GameApplication.Instance.SetSoundVolume();
            GameLocalisation.GetInstance().Initialize(GameApplication.Instance.CurrentLanguage);
            GameApplication.Instance.LoadHighscores();
            GameApplication.Instance.LoadAchievements();
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetSplashView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
            {
                GameStateMainMenu.Instance.ExitRequest = true;
            }
            else
            {
                GameStateMainMenu.GetInstance().GetSplashView().Update(gameTime);
                if (!this.StartRequest && stateMachine.GetCurrentStateTime() <= 5.0)
                    return;
                stateMachine.SetState(MainMenuStateMenu.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.StartRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetSplashView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetSplashView().Draw();
    }
}
