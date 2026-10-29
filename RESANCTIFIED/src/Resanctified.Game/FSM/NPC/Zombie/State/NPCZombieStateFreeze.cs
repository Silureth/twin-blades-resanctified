using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateFreeze : IState
    {
        private static NPCZombieStateFreeze m_instance = new NPCZombieStateFreeze();

        public static NPCZombieStateFreeze Instance => m_instance;

        public static NPCZombieStateFreeze GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            if (entity.GetView().GetAnimation2D() == npcZombieFsm.AnimationArray[1] && (double)entity.GetView().GetAnimation2D().GetTime() < 2.0)
                entity.GetView().GetAnimation2D().SetTime(2f);
            entity.GetView().GetAnimation2D().SetSpeed(0.0f);
            entity.GetIceView().SetAnimation2D(npcZombieFsm.AnimationArray[18]);
            entity.GetIceView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().AddSubview(entity.GetIceView());
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_Iced).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            entity.FreezeTime -= gameTime;
            if (entity.GetIceView().GetAnimation2D().IsEndOfAnimationReached())
            {
                entity.GetIceView().SetAnimation2D(npcZombieFsm.AnimationArray[19]);
                entity.GetIceView().GetAnimation2D().SetTime(0.0f);
            }
            if (entity.IsHit)
            {
                if (entity.GetHitType() == NPCZombie.HitType.Ice)
                {
                    entity.IsHit = false;
                }
                else
                {
                    entity.GetView().RemoveSubview(entity.GetIceView());
                    entity.SetHitType(NPCZombie.HitType.Ice);
                    stateMachine.SetState(NPCZombieStateDying.GetInstance());
                }
            }
            else
            {
                if (entity.FreezeTime > 0.0)
                    return;
                entity.GetIceView().SetAnimation2D(npcZombieFsm.AnimationArray[20]);
                entity.GetIceView().GetAnimation2D().SetTime(0.0f);
                entity.BreakingIce = true;
                stateMachine.SetState(NPCZombieStateSlash.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.FreezeRatio = 0.0f;
            entity.GetView().GetAnimation2D().SetSpeed(1f);
        }
    }
}
