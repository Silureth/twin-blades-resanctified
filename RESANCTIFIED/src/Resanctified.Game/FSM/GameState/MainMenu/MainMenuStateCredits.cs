using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateCredits : IDrawableState, IState
    {
        private float m_timer;
        public bool BackRequest;
        private static MainMenuStateCredits m_instance = new MainMenuStateCredits();

        public static MainMenuStateCredits Instance => m_instance;

        public static MainMenuStateCredits GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.m_timer = 4f;
            GameStateMainMenu.GetInstance().GetCreditsView().SetPage(0);
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetCreditsView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            this.m_timer -= gameTime;
            if (this.m_timer <= 0.0)
            {
                this.m_timer = 4f;
                if (!GameStateMainMenu.GetInstance().GetCreditsView().NextPage())
                {
                    stateMachine.SetState(MainMenuStateHelpAndOptions.GetInstance());
                    return;
                }
            }
            if (!this.BackRequest)
                return;
            stateMachine.SetState(MainMenuStateHelpAndOptions.GetInstance());
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetCreditsView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetCreditsView().Draw();
    }
}
