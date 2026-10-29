using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateExit : IDrawableState, IState
    {
        private static MainMenuStateExit m_instance = new MainMenuStateExit();
        public bool PreviousRequest;
        public bool ExitRequest;

        public static MainMenuStateExit Instance => m_instance;

        public static MainMenuStateExit GetInstance() => m_instance;

        public MainMenuStateExit()
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetExitConfirmationView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.PreviousRequest = true;
            if (this.PreviousRequest)
            {
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
                stateMachine.SetState(MainMenuStateMenu.GetInstance());
            }
            else
            {
                if (!this.ExitRequest)
                    return;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                GameStateMainMenu.Instance.ExitRequest = true;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.PreviousRequest = false;
            this.ExitRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetExitConfirmationView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetExitConfirmationView().Draw();
    }
}
