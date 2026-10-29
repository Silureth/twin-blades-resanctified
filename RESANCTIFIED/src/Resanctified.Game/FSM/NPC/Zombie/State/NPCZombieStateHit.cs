using Resanctified.Engine.FSM;
using Resanctified.Engine.Maths;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateHit : IState
    {
        private static NPCZombieStateHit m_instance = new NPCZombieStateHit();

        public static NPCZombieStateHit Instance => m_instance;

        public static NPCZombieStateHit GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            entity.IsHitReset = false;
            if (entity.GetHitType() == NPCZombie.HitType.Ice)
            {
                if (entity.IsBurning)
                {
                    entity.IsBurning = false;
                    entity.GetSound(NPCZombie.SoundFxId.SoundFx_Burning).Stop();
                    entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[0]);
                }
                stateMachine.SetState(NPCZombieStateFreeze.GetInstance());
            }
            else if (entity.HitPoints <= 0)
            {
                if (entity.IsBurning)
                {
                    entity.IsBurning = false;
                    entity.GetSound(NPCZombie.SoundFxId.SoundFx_Burning).Stop();
                }
                stateMachine.SetState(NPCZombieStateDying.GetInstance());
            }
            else
            {
                if (entity.GetHitType() == NPCZombie.HitType.Burn || entity.GetHitType() == NPCZombie.HitType.Holy)
                {
                    if (!entity.IsBurning)
                    {
                        entity.IsBurning = true;
                        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Burning).Play();
                    }
                    if (entity.GetHitType() == NPCZombie.HitType.Holy)
                        entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[11]);
                    else
                        entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[10]);
                }
                else
                {
                    if (MathHelperEx.GetRandomInt(0, 100) < 50)
                        entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[8]);
                    else
                        entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[9]);
                    int randomInt = MathHelperEx.GetRandomInt(0, 100);
                    if (randomInt < 33)
                        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Hit_1).Play();
                    else if (randomInt < 66)
                        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Hit_2).Play();
                    else
                        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Hit_3).Play();
                    if (entity.GetHitType() == NPCZombie.HitType.AirSlash)
                        entity.GetPhysics().Velocity.X = (float)(-(double)entity.GetTransform().GetScale().X * 1.0);
                    if (entity.GetHitType() == NPCZombie.HitType.Explosion)
                        entity.GetPhysics().Velocity.X = (float)(-(double)entity.GetTransform().GetScale().X * 2.0);
                }
                entity.GetView().GetAnimation2D().SetTime(0.0f);
                entity.GetView().GetAnimation2D().SetSpeed(1f);
            }
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.IsHit && entity.IsHitReset)
            {
                entity.IsHitReset = false;
                if (entity.GetHitType() != NPCZombie.HitType.Burn && entity.GetHitType() != NPCZombie.HitType.Holy && entity.IsBurning)
                {
                    entity.IsBurning = false;
                    entity.GetSound(NPCZombie.SoundFxId.SoundFx_Burning).Stop();
                }
                if (entity.HitPoints > 0)
                {
                    stateMachine.SetState(GetInstance());
                    return;
                }
            }
            if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            if (entity.IsBurning)
            {
                entity.IsBurning = false;
                entity.GetSound(NPCZombie.SoundFxId.SoundFx_Burning).Stop();
            }
            if (entity.HitPoints > 0)
                stateMachine.SetState(NPCZombieStateStand.GetInstance());
            else
                stateMachine.SetState(NPCZombieStateDying.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.IsHit = false;
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Acceleration.X = 0.0f;
        }
    }
}
