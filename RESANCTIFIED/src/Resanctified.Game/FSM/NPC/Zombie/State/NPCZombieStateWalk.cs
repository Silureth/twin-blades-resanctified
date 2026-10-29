// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Zombie.State.NPCZombieStateWalk
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Maths;
using Resanctified.Engine.World;
using System;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
  public class NPCZombieStateWalk : IState
  {
    private static NPCZombieStateWalk m_instance = new NPCZombieStateWalk();

    public static NPCZombieStateWalk Instance => NPCZombieStateWalk.m_instance;

    public static NPCZombieStateWalk GetInstance() => NPCZombieStateWalk.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCZombieFSM npcZombieFsm = (NPCZombieFSM) stateMachine;
      NPCZombie entity = (NPCZombie) npcZombieFsm.GetEntity();
      if (MathHelperEx.GetRandomInt(0, 100) < 50)
        entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[2]);
      else
        entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[3]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      entity.GetPhysics().Velocity.X = 0.8f * entity.GetTransform().GetScale().X;
      entity.Timer = MathHelperEx.GetRandomFloat(3f, 6f);
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
        PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
        Vector2 scale = entity.GetTransform().GetScale();
        float num1 = hero.GetTransform().GetPosition().X - entity.GetTransform().GetPosition().X;
        float num2 = Math.Abs(hero.GetTransform().GetPosition().Y - entity.GetTransform().GetPosition().Y);
        if ((double) num1 * (double) num1 > 169.0)
        {
          stateMachine.SetState((IState) NPCZombieStateUnSpawn.GetInstance());
        }
        else
        {
          if (entity.GetZombieType() == ZombieType.ArmoredNormal)
          {
            if ((double) num1 * (double) num1 <= 0.360000014305115)
            {
              stateMachine.SetState((IState) NPCZombieStateSlash.GetInstance());
              return;
            }
          }
          else if ((double) num1 * (double) num1 <= 0.25)
          {
            if (entity.GetZombieType() == ZombieType.Grabber || entity.GetZombieType() == ZombieType.ArmoredGrabber)
            {
              stateMachine.SetState((IState) NPCZombieStateGrab.GetInstance());
              return;
            }
            stateMachine.SetState((IState) NPCZombieStateSlash.GetInstance());
            return;
          }
          if ((double) num1 <= -(0.200000002980232 + (double) num2))
            scale.X = -Math.Abs(scale.X);
          else if ((double) num1 >= 0.200000002980232 + (double) num2)
            scale.X = Math.Abs(scale.X);
          entity.GetTransform().SetScale(scale);
          entity.GetPhysics().Velocity.X = 0.8f * entity.GetTransform().GetScale().X;
          entity.Timer -= gameTime;
          if ((double) entity.Timer > 0.0)
            return;
          if (MathHelperEx.GetRandomInt(0, 100) < 50)
          {
            if (MathHelperEx.GetRandomInt(0, 100) < 50)
              entity.GetSound(NPCZombie.SoundFxId.SoundFx_Walk_1).Play();
            else
              entity.GetSound(NPCZombie.SoundFxId.SoundFx_Walk_2).Play();
          }
          entity.Timer = MathHelperEx.GetRandomFloat(3f, 6f);
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetPhysics().Velocity.X = 0.0f;
  }
}
