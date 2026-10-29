// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroState.HeroStateWalk
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using System;

namespace Resanctified.Game.FSM.Hero.HeroState
{
  public class HeroStateWalk : IState
  {
    private const int FOOTSTEP_FRAME_1 = 1;
    private const int FOOTSTEP_FRAME_2 = 3;
    private bool m_footstepDone;

    public virtual void Enter(IFiniteStateMachine stateMachine)
    {
      this.SetAnimation(stateMachine);
      this.SetMovePhysics(stateMachine);
      this.m_footstepDone = false;
    }

    public virtual void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      if ((double) Math.Abs(entity.GetPhysics().Velocity.X) >= 2.0)
      {
        entity.GetPhysics().Velocity.X = (double) entity.GetPhysics().Velocity.X >= 0.0 ? 2f : -2f;
        entity.GetPhysics().Acceleration.X = 0.0f;
      }
      if (entity.IsHit)
        stateMachine.SetState((IState) heroFsm.StateHit);
      else if (entity.IsInAir)
      {
        stateMachine.SetState((IState) heroFsm.StateAir);
      }
      else
      {
        if (entity.GetView().GetAnimation2D().GetFrame() == 1 || entity.GetView().GetAnimation2D().GetFrame() == 3)
        {
          if (!this.m_footstepDone)
            this.m_footstepDone = true;
        }
        else
          this.m_footstepDone = false;
        heroFsm.UpdateNextAction();
        this.UpdateStateFromGameActions(stateMachine, gameTime);
      }
    }

    public virtual void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetPhysics().Acceleration.X = 0.0f;

    protected virtual void SetAnimation(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.GetAnimationStateMachine().IsInState((IState) HeroAnimationStateWalkLookRight.GetInstance()))
        return;
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateWalkLookRight.GetInstance());
    }

    protected virtual void UpdateStateFromGameActions(
      IFiniteStateMachine stateMachine,
      float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
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
            this.UpdateMove(stateMachine, gameTime);
            break;
          }
          if ((double) Math.Abs(entity.GetPhysics().Velocity.X) >= 2.0)
          {
            stateMachine.SetState((IState) heroFsm.StateSlide);
            break;
          }
          stateMachine.SetState((IState) heroFsm.StateStand);
          break;
      }
    }

    protected virtual void UpdateMove(IFiniteStateMachine stateMachine, float gameTime)
    {
      this.SetAnimation(stateMachine);
      this.SetMovePhysics(stateMachine);
    }

    protected virtual void SetMovePhysics(IFiniteStateMachine stateMachine)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      Vector2 scale = entity.GetTransform().GetScale();
      if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive())
      {
        if ((double) scale.X > 0.0)
          entity.GetPhysics().Velocity.X = -0.8f;
        entity.GetPhysics().Acceleration.X = -5f;
        scale.X = -Math.Abs(scale.X);
        entity.GetTransform().SetScale(scale);
      }
      else if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
      {
        if ((double) scale.X < 0.0)
          entity.GetPhysics().Velocity.X = 0.8f;
        entity.GetPhysics().Acceleration.X = 5f;
        scale.X = Math.Abs(scale.X);
        entity.GetTransform().SetScale(scale);
      }
      if (Math.Sign(scale.X) != Math.Sign(entity.GetPhysics().Velocity.X))
        entity.GetPhysics().Velocity.X = 0.0f;
      if ((double) Math.Abs(entity.GetPhysics().Velocity.X) >= 0.800000011920929)
        return;
      if ((double) scale.X < 0.0)
        entity.GetPhysics().Velocity.X = -0.8f;
      else
        entity.GetPhysics().Velocity.X = 0.8f;
    }
  }
}
