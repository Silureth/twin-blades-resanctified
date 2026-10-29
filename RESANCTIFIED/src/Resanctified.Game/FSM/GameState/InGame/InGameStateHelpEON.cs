using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateHelpEON : IDrawableState, IState
    {
        private static InGameStateHelpEON m_instance = new InGameStateHelpEON();
        public bool NextRequest;

        public static InGameStateHelpEON Instance => m_instance;

        public static InGameStateHelpEON GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            GameApplication.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameHelpEONView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            GameApplication.GetInstance().GetInGameHelpEONView().Update(gameTime);
            if (InputManager.GetInstance().IsBackPressed())
                this.NextRequest = true;
            if (!this.NextRequest)
                return;
            this.NextRequest = false;
            stateMachine.SetState(InGameMenuStateHelpAndOptions.GetInstance());
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }

        public void Exit(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameHelpEONView());

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpEONView().Draw();
    }
}
