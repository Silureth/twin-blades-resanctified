using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateMenu : IDrawableState, IState
    {
        private static MainMenuStateMenu m_instance = new MainMenuStateMenu();
        public bool StartGameRequest;
        public bool OptionsRequest;
        public bool HighscoresRequest;
        public bool AchievementsRequest;
        public bool ExitRequest;

        public static MainMenuStateMenu Instance => m_instance;

        public static MainMenuStateMenu GetInstance() => m_instance;

        public MainMenuStateMenu()
        {
            this.StartGameRequest = false;
            this.OptionsRequest = false;
            this.HighscoresRequest = false;
            this.AchievementsRequest = false;
            this.ExitRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.StartGameRequest = false;
            this.OptionsRequest = false;
            this.HighscoresRequest = false;
            this.AchievementsRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetMenuView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                GameStateMainMenu.Instance.ExitRequest = true;
            else if (this.StartGameRequest)
                stateMachine.SetState(MainMenuStateGameMode.GetInstance());
            else if (this.AchievementsRequest)
                stateMachine.SetState(MainMenuStateAchievements.GetInstance());
            else if (this.HighscoresRequest)
                stateMachine.SetState(MainMenuStateHighscoresChoose.GetInstance());
            else if (this.OptionsRequest)
                stateMachine.SetState(MainMenuStateHelpAndOptions.GetInstance());
            else
            {
                if (!this.ExitRequest)
                    return;
                GameStateMainMenu.Instance.ExitRequest = true;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.StartGameRequest = false;
            this.OptionsRequest = false;
            this.HighscoresRequest = false;
            this.AchievementsRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetMenuView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetMenuView().Draw();
    }
}
