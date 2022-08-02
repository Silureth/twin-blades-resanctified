// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.State.NPCZombieStateSlash
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Maths;
using PSS.GameEngine.World;
using System;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie.State
{
  public class NPCZombieStateSlash : IState
  {
    private static NPCZombieStateSlash m_instance = new NPCZombieStateSlash();

    public static NPCZombieStateSlash Instance => NPCZombieStateSlash.m_instance;

    public static NPCZombieStateSlash GetInstance() => NPCZombieStateSlash.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCZombieFSM npcZombieFsm = (NPCZombieFSM) stateMachine;
      NPCZombie entity = (NPCZombie) npcZombieFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[4]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      Vector2 scale = entity.GetTransform().GetScale();
      scale.X = (double) hero.GetTransform().GetPosition().X - (double) entity.GetTransform().GetPosition().X > 0.0 ? Math.Abs(scale.X) : -Math.Abs(scale.X);
      entity.GetTransform().SetScale(scale);
      if (MathHelperEx.GetRandomInt(0, 100) < 50)
        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Attack_1).Play();
      else
        entity.GetSound(NPCZombie.SoundFxId.SoundFx_Attack_2).Play();
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
    }
  }
}
