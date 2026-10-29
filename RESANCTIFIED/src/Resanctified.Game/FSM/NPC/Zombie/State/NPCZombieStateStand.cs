using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateStand : IState
    {
        private static NPCZombieStateStand m_instance = new NPCZombieStateStand();

        public static NPCZombieStateStand Instance => m_instance;

        public static NPCZombieStateStand GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[0]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
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
                Vector2 scale = entity.GetTransform().GetScale();
                float num1 = hero.GetTransform().GetPosition().X - entity.GetTransform().GetPosition().X;
                if ((double)num1 <= 0.0)
                    scale.X = -Math.Abs(scale.X);
                else if ((double)num1 >= 0.100000001490116)
                    scale.X = Math.Abs(scale.X);
                entity.GetTransform().SetScale(scale);
                float num2 = num1 * num1;
                if (entity.GetZombieType() == ZombieType.ArmoredNormal)
                {
                    if ((double)num2 <= 0.360000014305115)
                    {
                        if (npcZombieFsm.GetCurrentStateTime() <= 1.0 && npcZombieFsm.GetPreviousState() != NPCZombieStateSpawn.GetInstance())
                            return;
                        stateMachine.SetState(NPCZombieStateSlash.GetInstance());
                    }
                    else
                        stateMachine.SetState(NPCZombieStateWalk.GetInstance());
                }
                else if ((double)num2 <= 0.25)
                {
                    if (npcZombieFsm.GetCurrentStateTime() <= 1.0 && npcZombieFsm.GetPreviousState() != NPCZombieStateSpawn.GetInstance())
                        return;
                    if (entity.GetZombieType() == ZombieType.Grabber || entity.GetZombieType() == ZombieType.ArmoredGrabber)
                        stateMachine.SetState(NPCZombieStateGrab.GetInstance());
                    else
                        stateMachine.SetState(NPCZombieStateSlash.GetInstance());
                }
                else
                    stateMachine.SetState(NPCZombieStateWalk.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
