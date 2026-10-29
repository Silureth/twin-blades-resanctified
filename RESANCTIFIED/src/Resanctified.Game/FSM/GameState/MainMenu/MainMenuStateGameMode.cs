using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateGameMode : IDrawableState, IState
    {
        private static MainMenuStateGameMode m_instance = new MainMenuStateGameMode();
        private bool m_savedGameAvailable;
        public bool BackRequest;
        public bool ContinueRequest;
        public bool DifficultyRequest;

        public static MainMenuStateGameMode Instance => m_instance;

        public static MainMenuStateGameMode GetInstance() => m_instance;

        public bool IsSavedGameAvailable() => this.m_savedGameAvailable;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.m_savedGameAvailable = Prefs.Instance.ReadGameData();
            if (stateMachine.GetPreviousState() == MainMenuStateMenu.GetInstance() && this.m_savedGameAvailable)
                GameStateMainMenu.GetInstance().GetGameModeView().SavedDifficulty = (GameApplication.GameDifficulty)Prefs.Instance.CachedGameData.m_difficulty;
            this.BackRequest = false;
            this.ContinueRequest = false;
            this.DifficultyRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetGameModeView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (this.BackRequest)
            {
                stateMachine.SetState(MainMenuStateMenu.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
            else if (this.ContinueRequest)
            {
                stateMachine.SetState(MainMenuStateContinueGame.GetInstance());
            }
            else
            {
                if (!this.DifficultyRequest)
                    return;
                if (this.m_savedGameAvailable)
                    stateMachine.SetState(MainMenuStateOverwrite.GetInstance());
                else
                    stateMachine.SetState(MainMenuStateDifficulty.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine) => Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetGameModeView());

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetGameModeView().Draw();
    }
}
