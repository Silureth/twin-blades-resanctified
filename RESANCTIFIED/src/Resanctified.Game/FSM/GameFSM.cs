using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.GameState;

namespace Resanctified.Game.FSM
{
    public class GameFSM : DrawableFiniteStateMachine
    {
        private static GameFSM m_instance = new GameFSM();

        public static GameFSM Instance => m_instance;

        public static GameFSM GetInstance() => m_instance;

        public override void Initialize() => this.SetState(GameStateInit.Instance);
    }
}
