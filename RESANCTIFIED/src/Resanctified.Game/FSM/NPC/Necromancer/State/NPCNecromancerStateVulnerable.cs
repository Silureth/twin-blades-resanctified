using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
    public class NPCNecromancerStateVulnerable : IState
    {
        private static NPCNecromancerStateVulnerable m_instance = new NPCNecromancerStateVulnerable();

        public static NPCNecromancerStateVulnerable Instance => m_instance;

        public static NPCNecromancerStateVulnerable GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM)stateMachine;
            NPCNecromancer entity = (NPCNecromancer)npcNecromancerFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[0]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            entity.VulnerableTimer = 3f;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCNecromancer entity = (NPCNecromancer)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.IsHit)
            {
                stateMachine.SetState(NPCNecromancerStateHit.GetInstance());
            }
            else
            {
                if (entity.VulnerableTimer > 0.0)
                    return;
                stateMachine.SetState(NPCNecromancerStateStand.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
