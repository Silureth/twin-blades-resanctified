using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateKeyboard : IDrawableState, IState
    {
        private static InGameMenuStateKeyboard m_instance = new InGameMenuStateKeyboard();
        public bool BackRequest;

        public static InGameMenuStateKeyboard Instance => m_instance;

        public static InGameMenuStateKeyboard GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameKeyboardView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (!this.BackRequest)
                return;
            stateMachine.SetState(InGameMenuStateHelpAndOptions.GetInstance());
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameKeyboardView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameKeyboardView().Draw();
    }
}
