// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroState.HeroStateSlide
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using System;

namespace Resanctified.Game.FSM.Hero.HeroState
{
  public class HeroStateSlide : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateWalkSlide.GetInstance());
      entity.GetPhysics().Acceleration.X = -5f * entity.GetTransform().GetScale().X;
    }

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
        heroFsm.UpdateNextAction();
        if ((double) Math.Abs(entity.GetPhysics().Velocity.X) < 0.800000011920929)
        {
          entity.GetPhysics().Velocity.X = 0.0f;
          entity.GetPhysics().Acceleration.X = 0.0f;
        }
        switch (heroFsm.NextAction)
        {
          case HeroFSM.HeroActionId.Slash:
            stateMachine.SetState((IState) heroFsm.StateSlash);
            break;
          case HeroFSM.HeroActionId.Fire:
            if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
            {
              stateMachine.SetState((IState) heroFsm.StateWalkFire);
              break;
            }
            stateMachine.SetState((IState) heroFsm.StateStandFire);
            break;
          case HeroFSM.HeroActionId.Jump:
            stateMachine.SetState((IState) heroFsm.StateJump);
            break;
          default:
            if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
            {
              stateMachine.SetState((IState) heroFsm.StateWalk);
              break;
            }
            if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
              break;
            stateMachine.SetState((IState) heroFsm.StateStand);
            break;
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetPhysics().Acceleration.X = 0.0f;
  }
}
