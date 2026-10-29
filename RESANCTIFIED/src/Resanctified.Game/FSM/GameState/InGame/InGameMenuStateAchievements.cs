using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateAchievements : IDrawableState, IState
    {
        private static InGameMenuStateAchievements m_instance = new InGameMenuStateAchievements();
        public bool BackRequest;

        public static InGameMenuStateAchievements Instance => m_instance;

        public static InGameMenuStateAchievements GetInstance() => m_instance;

        public InGameMenuStateAchievements() => this.BackRequest = false;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameAchievementsView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (!this.BackRequest)
                return;
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            stateMachine.SetState(InGameStateMenu.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameAchievementsView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameAchievementsView().Draw();
    }
}
