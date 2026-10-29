// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroStateAir
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using System;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroStateAir : IState
  {
    public void Enter(IFiniteStateMachine stateMachine) => ((PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetAnimationStateMachine().SetState((IState) HeroAnimationStateAirLookRight.GetInstance());

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      if (entity.IsHit)
      {
        stateMachine.SetState((IState) heroFsm.StateHit);
      }
      else
      {
        if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
        {
          entity.GetPhysics().Velocity.X += 4f * gameTime;
          Vector2 scale = entity.GetTransform().GetScale();
          scale.X = Math.Abs(scale.X);
          entity.GetTransform().SetScale(scale);
        }
        else if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive())
        {
          entity.GetPhysics().Velocity.X -= 4f * gameTime;
          Vector2 scale = entity.GetTransform().GetScale();
          scale.X = -Math.Abs(scale.X);
          entity.GetTransform().SetScale(scale);
        }
        heroFsm.UpdateNextAction();
        if (heroFsm.NextAction == HeroFSM.HeroActionId.Slash && entity.AirSlashAvailable)
        {
          if ((double) entity.GetTransform().GetPosition().Y <= 1.0 && (double) entity.GetPhysics().Velocity.Y > 0.0)
            return;
          stateMachine.SetState((IState) heroFsm.StateSlash);
        }
        else
        {
          heroFsm.NextAction = HeroFSM.HeroActionId.None;
          if (entity.IsInAir)
            return;
          if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
            stateMachine.SetState((IState) heroFsm.StateWalk);
          else
            stateMachine.SetState((IState) heroFsm.StateLand);
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
