using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
    public class NPCNecromancerStateHit : IState
    {
        private static NPCNecromancerStateHit m_instance = new NPCNecromancerStateHit();

        public static NPCNecromancerStateHit Instance => m_instance;

        public static NPCNecromancerStateHit GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM)stateMachine;
            NPCNecromancer entity = (NPCNecromancer)npcNecromancerFsm.GetEntity();
            if (entity.HitPoints <= 0)
            {
                stateMachine.SetState(NPCNecromancerStateDying.GetInstance());
            }
            else
            {
                entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[1]);
                entity.GetView().GetAnimation2D().SetTime(0.0f);
                entity.GetView().GetAnimation2D().SetSpeed(1f);
                entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Hit).Play();
            }
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCNecromancer entity = (NPCNecromancer)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            if (entity.IsHit)
                stateMachine.SetState(GetInstance());
            else if (entity.VulnerableTimer > 0.0)
                stateMachine.SetState(NPCNecromancerStateVulnerable.GetInstance());
            else
                stateMachine.SetState(NPCNecromancerStateStand.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((NPCBoss)((EntityFiniteStateMachine)stateMachine).GetEntity()).IsHit = false;
    }
}
