using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateOverwrite : IDrawableState, IState
    {
        private static MainMenuStateOverwrite m_instance = new MainMenuStateOverwrite();
        public bool OverwriteOkRequest;
        public bool OverwriteCancelRequest;

        public static MainMenuStateOverwrite Instance => m_instance;

        public static MainMenuStateOverwrite GetInstance() => m_instance;

        public MainMenuStateOverwrite()
        {
            this.OverwriteOkRequest = false;
            this.OverwriteCancelRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.OverwriteOkRequest = false;
            this.OverwriteCancelRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetOverwriteView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.OverwriteCancelRequest = true;
            if (this.OverwriteCancelRequest)
            {
                stateMachine.SetState(MainMenuStateGameMode.GetInstance());
            }
            else
            {
                if (!this.OverwriteOkRequest)
                    return;
                Prefs.GetInstance().EraseGameDataFile();
                stateMachine.SetState(MainMenuStateDifficulty.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.OverwriteOkRequest = false;
            this.OverwriteCancelRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetOverwriteView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetOverwriteView().Draw();
    }
}
