using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateGrabEat : IState
    {
        private static NPCZombieStateGrabEat m_instance = new NPCZombieStateGrabEat();

        public static NPCZombieStateGrabEat Instance => m_instance;

        public static NPCZombieStateGrabEat GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[6]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            npcZombieFsm.GrabEatTimer = 0.0f;
            Vector3 position = entity.GetView().GetTransform().GetPosition() with
            {
                Z = 3f
            };
            entity.GetView().GetTransform().SetPosition(position);
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_GrabEat_1).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            if (entity.IsHit)
            {
                stateMachine.SetState(NPCZombieStateHit.GetInstance());
            }
            else
            {
                PlayableCharacter hero = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero();
                npcZombieFsm.GrabEatTimer -= gameTime;
                if (npcZombieFsm.GrabEatTimer <= 0.0)
                {
                    hero.HitPoints -= entity.GetGrabDamage();
                    npcZombieFsm.GrabEatTimer = 0.5f;
                }
                if (hero.HitPoints > 0)
                    return;
                stateMachine.SetState(NPCZombieStateStand.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            Vector3 position = entity.GetView().GetTransform().GetPosition() with
            {
                Z = -1f
            };
            entity.GetView().GetTransform().SetPosition(position);
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_GrabEat_1).Stop();
        }
    }
}
