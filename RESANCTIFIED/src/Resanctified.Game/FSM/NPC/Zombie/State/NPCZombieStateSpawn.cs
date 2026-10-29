using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Maths;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateSpawn : IState
    {
        private static NPCZombieStateSpawn m_instance = new NPCZombieStateSpawn();

        public static NPCZombieStateSpawn Instance => m_instance;

        public static NPCZombieStateSpawn GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombieFSM npcZombieFsm = (NPCZombieFSM)stateMachine;
            NPCZombie entity = (NPCZombie)npcZombieFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[1]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            entity.IsActive = true;
            entity.Reset();
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            PlayableCharacter hero = currentWorld.GetHero();
            float num = MathHelperEx.GetRandomFloat(1f, 9f);
            int randomInt = MathHelperEx.GetRandomInt(0, 100);
            Vector3 position1 = entity.GetTransform().GetPosition();
            Vector2 scale = entity.GetTransform().GetScale();
            if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                num = currentWorld.LevelIndex != GameWorld.LevelId.Level_Town_Boss ? MathHelperEx.GetRandomFloat(0.0f, 5.5f) : MathHelperEx.GetRandomFloat(1.5f, 5.5f);
                position1.X = num;
                scale.X = hero.GetTransform().GetPosition().X - (double)num <= 0.0 ? -Math.Abs(scale.X) : Math.Abs(scale.X);
            }
            else if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
            {
                position1.X = MathHelperEx.GetRandomFloat(-2f, -3f);
                scale.X = Math.Abs(scale.X);
            }
            else if (randomInt > 80)
            {
                position1.X = hero.GetTransform().GetPosition().X - num;
                scale.X = Math.Abs(scale.X);
                if (position1.X < 0.0)
                {
                    position1.X = hero.GetTransform().GetPosition().X + num;
                    scale.X = -Math.Abs(scale.X);
                }
            }
            else
            {
                position1.X = hero.GetTransform().GetPosition().X + num;
                scale.X = -Math.Abs(scale.X);
                if (position1.X > (double)currentWorld.EndOfLevelPosX)
                {
                    position1.X = hero.GetTransform().GetPosition().X - num;
                    scale.X = Math.Abs(scale.X);
                }
            }
            entity.GetTransform().SetPosition(position1);
            entity.GetTransform().SetScale(scale);
            Vector3 position2 = entity.GetView().GetTransform().GetPosition() with
            {
                Z = -1f
            };
            entity.GetView().GetTransform().SetPosition(position2);
            Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView(entity.GetView(), entity.GetTransform());
            entity.IsAlive = true;
            if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
                stateMachine.SetState(NPCZombieStateStand.GetInstance());
            else if ((double)num > 6.69999980926514)
                stateMachine.SetState(NPCZombieStateStand.GetInstance());
            else if (MathHelperEx.GetRandomInt(0, 100) < 50)
                entity.GetSound(NPCZombie.SoundFxId.SoundFx_Spawn_1).Play();
            else
                entity.GetSound(NPCZombie.SoundFxId.SoundFx_Spawn_2).Play();
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

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView(entity.GetShadowView(), null);
        }
    }
}
