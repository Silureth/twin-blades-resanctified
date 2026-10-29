using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
    public class NPCButcherStateDead : IState
    {
        private static NPCButcherStateDead m_instance = new NPCButcherStateDead();

        public static NPCButcherStateDead Instance => m_instance;

        public static NPCButcherStateDead GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
