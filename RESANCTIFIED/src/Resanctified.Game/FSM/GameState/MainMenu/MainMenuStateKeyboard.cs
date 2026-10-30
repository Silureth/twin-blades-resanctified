using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateKeyboard : IDrawableState, IState
    {
        private static MainMenuStateKeyboard m_instance = new MainMenuStateKeyboard();
        public bool BackRequest;

        public static MainMenuStateKeyboard Instance => m_instance;

        public static MainMenuStateKeyboard GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetKeyboardView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (!this.BackRequest)
                return;
            stateMachine.SetState(MainMenuStateHelpAndOptions.GetInstance());
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetKeyboardView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetKeyboardView().Draw();
    }
}
