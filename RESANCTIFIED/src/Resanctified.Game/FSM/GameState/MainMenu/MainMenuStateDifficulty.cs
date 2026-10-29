using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateDifficulty : IDrawableState, IState
    {
        private static MainMenuStateDifficulty m_instance = new MainMenuStateDifficulty();
        public bool BackRequest;
        public bool StartGameRequest;

        public static MainMenuStateDifficulty Instance => m_instance;

        public static MainMenuStateDifficulty GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.StartGameRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetDifficultyView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (this.BackRequest)
            {
                stateMachine.SetState(MainMenuStateGameMode.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
            else
            {
                if (!this.StartGameRequest)
                    return;
                stateMachine.SetState(MainMenuStateStartGame.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.StartGameRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetDifficultyView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetDifficultyView().Draw();
    }
}
