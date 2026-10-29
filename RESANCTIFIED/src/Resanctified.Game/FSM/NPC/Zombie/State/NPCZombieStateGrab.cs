using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateGrab : IState
    {
        private static NPCZombieStateGrab m_instance = new NPCZombieStateGrab();

        public static NPCZombieStateGrab Instance => m_instance;

        public static NPCZombieStateGrab GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[5]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            PlayableCharacter hero = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero();
            Vector2 scale = entity.GetTransform().GetScale();
            scale.X = hero.GetTransform().GetPosition().X - (double)entity.GetTransform().GetPosition().X > 0.0 ? Math.Abs(scale.X) : -Math.Abs(scale.X);
            if (Math.Sign(scale.X) != Math.Sign(hero.GetTransform().GetScale().X))
                entity.GetPhysics().Velocity.X = scale.X * 1f;
            entity.GetTransform().SetScale(scale);
            Vector3 position = entity.GetView().GetTransform().GetPosition() with
            {
                Z = 3f
            };
            entity.GetView().GetTransform().SetPosition(position);
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_Walk_2).Play();
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
                if (((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero().IsGrabbed)
                    stateMachine.SetState(NPCZombieStateGrabEat.GetInstance());
                else
                    stateMachine.SetState(NPCZombieStateStand.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetPhysics().Velocity.X = 0.0f;
            Vector3 position = entity.GetView().GetTransform().GetPosition() with
            {
                Z = -1f
            };
            entity.GetView().GetTransform().SetPosition(position);
        }
    }
}
