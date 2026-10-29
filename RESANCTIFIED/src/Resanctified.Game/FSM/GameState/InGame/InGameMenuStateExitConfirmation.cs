using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateExitConfirmation : IDrawableState, IState
    {
        private static InGameMenuStateExitConfirmation m_instance = new InGameMenuStateExitConfirmation();
        public bool PreviousRequest;
        public bool ExitRequest;

        public static InGameMenuStateExitConfirmation Instance => m_instance;

        public static InGameMenuStateExitConfirmation GetInstance() => m_instance;

        public InGameMenuStateExitConfirmation()
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameExitConfirmationView());
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
                GameApplication.Instance.ExitRequest = true;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameExitConfirmationView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameExitConfirmationView().Draw();
    }
}
