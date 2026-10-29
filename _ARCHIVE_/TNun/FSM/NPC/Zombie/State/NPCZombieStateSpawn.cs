// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.State.NPCZombieStateSpawn
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Maths;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;
using System;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie.State
{
  public class NPCZombieStateSpawn : IState
  {
    private static NPCZombieStateSpawn m_instance = new NPCZombieStateSpawn();

    public static NPCZombieStateSpawn Instance => NPCZombieStateSpawn.m_instance;

    public static NPCZombieStateSpawn GetInstance() => NPCZombieStateSpawn.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCZombieFSM npcZombieFsm = (NPCZombieFSM) stateMachine;
      NPCZombie entity = (NPCZombie) npcZombieFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[1]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      entity.IsActive = true;
      entity.Reset();
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      PlayableCharacter hero = currentWorld.GetHero();
      float num = MathHelperEx.GetRandomFloat(1f, 9f);
      int randomInt = MathHelperEx.GetRandomInt(0, 100);
      Vector3 position1 = entity.GetTransform().GetPosition();
      Vector2 scale = entity.GetTransform().GetScale();
      if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
      {
        num = currentWorld.LevelIndex != GameWorld.LevelId.Level_Town_Boss ? MathHelperEx.GetRandomFloat(0.0f, 5.5f) : MathHelperEx.GetRandomFloat(1.5f, 5.5f);
        position1.X = num;
        scale.X = (double) hero.GetTransform().GetPosition().X - (double) num <= 0.0 ? -Math.Abs(scale.X) : Math.Abs(scale.X);
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
        if ((double) position1.X < 0.0)
        {
          position1.X = hero.GetTransform().GetPosition().X + num;
          scale.X = -Math.Abs(scale.X);
        }
      }
      else
      {
        position1.X = hero.GetTransform().GetPosition().X + num;
        scale.X = -Math.Abs(scale.X);
        if ((double) position1.X > (double) currentWorld.EndOfLevelPosX)
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
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) entity.GetView(), entity.GetTransform());
      entity.IsAlive = true;
      if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
        stateMachine.SetState((IState) NPCZombieStateStand.GetInstance());
      else if ((double) num > 6.69999980926514)
        stateMachine.SetState((IState) NPCZombieStateStand.GetInstance());
      else if (MathHelperEx.GetRandomInt(0, 100) < 50)
        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Spawn_1).Play();
      else
        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Spawn_2).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.IsHit)
      {
        stateMachine.SetState((IState) NPCZombieStateHit.GetInstance());
      }
      else
      {
        if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
          return;
        stateMachine.SetState((IState) NPCZombieStateStand.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) entity.GetShadowView(), (Transform) null);
    }
  }
}
