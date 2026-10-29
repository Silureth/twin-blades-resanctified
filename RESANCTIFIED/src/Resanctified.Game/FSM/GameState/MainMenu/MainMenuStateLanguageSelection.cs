using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateLanguageSelection : IDrawableState, IState
    {
        private static MainMenuStateLanguageSelection m_instance = new MainMenuStateLanguageSelection();
        public bool BackRequest;

        public static MainMenuStateLanguageSelection Instance => m_instance;

        public static MainMenuStateLanguageSelection GetInstance() => m_instance;

        public MainMenuStateLanguageSelection() => this.BackRequest = false;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetLanguageSelectionView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (!this.BackRequest)
                return;
            stateMachine.SetState(MainMenuStateOptions.GetInstance());
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetLanguageSelectionView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetLanguageSelectionView().Draw();
    }
}
