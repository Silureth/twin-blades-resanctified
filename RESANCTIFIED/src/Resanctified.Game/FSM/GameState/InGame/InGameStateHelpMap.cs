using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateHelpMap : IDrawableState, IState
    {
        private static InGameStateHelpMap m_instance = new InGameStateHelpMap();
        public bool NextRequest;

        public static InGameStateHelpMap Instance => m_instance;

        public static InGameStateHelpMap GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            GameApplication.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameHelpMapView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            GameApplication.GetInstance().GetInGameHelpMapView().Update(gameTime);
            if (InputManager.GetInstance().IsBackPressed())
                this.NextRequest = true;
            if (!this.NextRequest)
                return;
            this.NextRequest = false;
            stateMachine.SetState(InGameMenuStateHelpAndOptions.GetInstance());
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }

        public void Exit(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameHelpMapView());

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpMapView().Draw();
    }
}
