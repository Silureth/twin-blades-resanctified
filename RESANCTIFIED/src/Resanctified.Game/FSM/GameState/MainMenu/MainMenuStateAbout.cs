using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateAbout : IDrawableState, IState
    {
        private static MainMenuStateAbout m_instance = new MainMenuStateAbout();
        public bool BackRequest;

        public static MainMenuStateAbout Instance => m_instance;

        public static MainMenuStateAbout GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetAboutView());
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
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetAboutView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetAboutView().Draw();
    }
}
