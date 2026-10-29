using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateBackToMapConfirmation : IDrawableState, IState
    {
        private static InGameMenuStateBackToMapConfirmation m_instance = new InGameMenuStateBackToMapConfirmation();
        public bool PreviousRequest;
        public bool ExitRequest;

        public static InGameMenuStateBackToMapConfirmation Instance => m_instance;

        public static InGameMenuStateBackToMapConfirmation GetInstance() => m_instance;

        public InGameMenuStateBackToMapConfirmation()
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameBackToMapConfirmationView());
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
                stateMachine.SetState(InGameStateExitToMap.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameBackToMapConfirmationView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameBackToMapConfirmationView().Draw();
    }
}
