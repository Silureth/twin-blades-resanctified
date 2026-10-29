using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateLanguageSelection : IDrawableState, IState
    {
        private static InGameMenuStateLanguageSelection m_instance = new InGameMenuStateLanguageSelection();
        public bool BackRequest;

        public static InGameMenuStateLanguageSelection Instance => m_instance;

        public static InGameMenuStateLanguageSelection GetInstance() => m_instance;

        public InGameMenuStateLanguageSelection() => this.BackRequest = false;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameLanguageSelectionView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (!this.BackRequest)
                return;
            stateMachine.SetState(InGameMenuStateOptions.GetInstance());
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameLanguageSelectionView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameLanguageSelectionView().Draw();
    }
}
