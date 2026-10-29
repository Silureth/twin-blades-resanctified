// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.State.NPCZombieStateGrab
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.World;
using System;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie.State
{
  public class NPCZombieStateGrab : IState
  {
    private static NPCZombieStateGrab m_instance = new NPCZombieStateGrab();

    public static NPCZombieStateGrab Instance => NPCZombieStateGrab.m_instance;

    public static NPCZombieStateGrab GetInstance() => NPCZombieStateGrab.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCZombieFSM npcZombieFsm = (NPCZombieFSM) stateMachine;
      NPCZombie entity = (NPCZombie) npcZombieFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[5]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      Vector2 scale = entity.GetTransform().GetScale();
      scale.X = (double) hero.GetTransform().GetPosition().X - (double) entity.GetTransform().GetPosition().X > 0.0 ? Math.Abs(scale.X) : -Math.Abs(scale.X);
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
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.IsHit)
      {
        stateMachine.SetState((IState) NPCZombieStateHit.GetInstance());
      }
      else
      {
        if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
          return;
        if (((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero().IsGrabbed)
          stateMachine.SetState((IState) NPCZombieStateGrabEat.GetInstance());
        else
          stateMachine.SetState((IState) NPCZombieStateStand.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetPhysics().Velocity.X = 0.0f;
      Vector3 position = entity.GetView().GetTransform().GetPosition() with
      {
        Z = -1f
      };
      entity.GetView().GetTransform().SetPosition(position);
    }
  }
}
