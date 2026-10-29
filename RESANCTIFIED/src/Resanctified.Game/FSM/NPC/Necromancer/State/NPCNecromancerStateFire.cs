using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
    public class NPCNecromancerStateFire : IState
    {
        private static NPCNecromancerStateFire m_instance = new NPCNecromancerStateFire();

        public static NPCNecromancerStateFire Instance => m_instance;

        public static NPCNecromancerStateFire GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM)stateMachine;
            NPCNecromancer entity = (NPCNecromancer)npcNecromancerFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[3]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Blast).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (!((NPCNecromancer)((EntityFiniteStateMachine)stateMachine).GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            stateMachine.SetState(NPCNecromancerStateStand.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((NPCNecromancer)((EntityFiniteStateMachine)stateMachine).GetEntity()).SpellTimer = 0.0f;
    }
}
