using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Sounds;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateEONNavigation : IDrawableState, IState
    {
        public bool InGameMenuRequest;
        private static InGameStateEONNavigation m_instance = new InGameStateEONNavigation();

        public static InGameStateEONNavigation Instance => m_instance;

        public static InGameStateEONNavigation GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine) => this.InGameMenuRequest = false;

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().StdPause.IsActive())
                this.InGameMenuRequest = true;
            if (InputManager.GetInstance().IsBackPressed())
                this.InGameMenuRequest = true;
            if (!this.InGameMenuRequest)
                return;
            this.InGameMenuRequest = false;
            AudioManager.GetInstance().PauseAll();
            stateMachine.SetState(InGameStateMenu.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
        }
    }
}
