using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateAchievements : IDrawableState, IState
    {
        private static MainMenuStateAchievements m_instance = new MainMenuStateAchievements();
        public bool BackRequest;

        public static MainMenuStateAchievements Instance => m_instance;

        public static MainMenuStateAchievements GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetAchievementsView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (!this.BackRequest)
                return;
            stateMachine.SetState(MainMenuStateMenu.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetAchievementsView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetAchievementsView().Draw();
    }
}
