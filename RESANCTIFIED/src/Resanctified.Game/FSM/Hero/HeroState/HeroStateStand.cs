// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroState.HeroStateStand
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.Hero.HeroState
{
  public class HeroStateStand : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateStandLookRight.GetInstance());
      entity.GetPhysics().Velocity.X = 0.0f;
      entity.GetPhysics().Velocity.Y = 0.0f;
      entity.GetPhysics().Acceleration.X = 0.0f;
      entity.GetPhysics().Acceleration.Y = 0.0f;
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
        switch (heroFsm.NextAction)
        {
          case HeroFSM.HeroActionId.None:
            if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
            {
              stateMachine.SetState((IState) heroFsm.StateWalk);
              break;
            }
            if (entity.GetAnimationStateMachine().IsInState((IState) HeroAnimationStateStandLookRight.GetInstance()))
              break;
            entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateStandLookRight.GetInstance());
            break;
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
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
