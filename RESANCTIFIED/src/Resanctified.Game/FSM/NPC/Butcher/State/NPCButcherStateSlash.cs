using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
    public class NPCButcherStateSlash : IState
    {
        private static NPCButcherStateSlash m_instance = new NPCButcherStateSlash();

        public static NPCButcherStateSlash Instance => m_instance;

        public static NPCButcherStateSlash GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCButcherFSM npcButcherFsm = (NPCButcherFSM)stateMachine;
            NPCButcher entity = (NPCButcher)npcButcherFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[2]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            if (entity.GetBallView().GetAnimation2D() != npcButcherFsm.AnimationArray[10])
            {
                entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[8]);
                entity.GetBallView().GetAnimation2D().SetTime(0.0f);
                entity.GetBallView().GetAnimation2D().SetSpeed(1f);
            }
            entity.GetSound(NPCButcher.SoundFxId.SoundFx_Slash).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCButcher entity = (NPCButcher)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.HitPoints <= 0)
            {
                stateMachine.SetState(NPCButcherStateDying.GetInstance());
            }
            else
            {
                if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                    return;
                stateMachine.SetState(NPCButcherStateStand.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
