using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
    public class NPCNecromancerStateDead : IState
    {
        private static NPCNecromancerStateDead m_instance = new NPCNecromancerStateDead();

        public static NPCNecromancerStateDead Instance => m_instance;

        public static NPCNecromancerStateDead GetInstance() => m_instance;

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
