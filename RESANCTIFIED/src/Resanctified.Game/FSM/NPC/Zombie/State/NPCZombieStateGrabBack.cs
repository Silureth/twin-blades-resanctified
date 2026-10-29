using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateGrabBack : IState
    {
        private static NPCZombieStateGrabBack m_instance = new NPCZombieStateGrabBack();

        public static NPCZombieStateGrabBack Instance => m_instance;

        public static NPCZombieStateGrabBack GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[7]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            entity.GetPhysics().Velocity.X = (float)(-(double)entity.GetTransform().GetScale().X * 2.79999995231628);
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_Push_Back).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.IsHit)
            {
                stateMachine.SetState(NPCZombieStateHit.GetInstance());
            }
            else
            {
                if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                    return;
                stateMachine.SetState(NPCZombieStateStand.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity()).GetPhysics().Velocity.X = 0.0f;
    }
}
