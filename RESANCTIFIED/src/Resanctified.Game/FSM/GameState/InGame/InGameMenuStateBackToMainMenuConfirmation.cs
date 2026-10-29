using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateBackToMainMenuConfirmation : IDrawableState, IState
    {
        private static InGameMenuStateBackToMainMenuConfirmation m_instance = new InGameMenuStateBackToMainMenuConfirmation();
        public bool PreviousRequest;
        public bool ExitRequest;

        public static InGameMenuStateBackToMainMenuConfirmation Instance => m_instance;

        public static InGameMenuStateBackToMainMenuConfirmation GetInstance() => m_instance;

        public InGameMenuStateBackToMainMenuConfirmation()
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameBackToMainMenuConfirmationView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.PreviousRequest = true;
            if (this.PreviousRequest)
            {
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
                stateMachine.SetState(InGameStateMenu.GetInstance());
            }
            else
            {
                if (!this.ExitRequest)
                    return;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
                    GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                InGameStateMenu.GetInstance().ScriptExecuting = false;
                stateMachine.SetState(InGameStateExit.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameBackToMainMenuConfirmationView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameBackToMainMenuConfirmationView().Draw();
    }
}
