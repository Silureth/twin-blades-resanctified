// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroStateStandFire
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroStateStandFire : IState
  {
    private HeroSubStateFireBase m_fireSubState;

    public HeroStateStandFire() => this.m_fireSubState = new HeroSubStateFireBase();

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateStandFireRight.GetInstance());
      entity.GetPhysics().Velocity.X = 0.0f;
      entity.GetPhysics().Velocity.Y = 0.0f;
      heroFsm.NextAction = HeroFSM.HeroActionId.None;
      this.m_fireSubState.Enter(stateMachine);
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
        if (entity.HasFired && entity.GetAnimationStateMachine().FireTransitionAnimationIndex >= 1)
        {
          switch (heroFsm.NextAction)
          {
            case HeroFSM.HeroActionId.None:
              if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
              {
                stateMachine.SetState((IState) heroFsm.StateWalk);
                break;
              }
              if (entity.GetAnimationStateMachine().FireTransitionAnimationIndex != 2)
                break;
              stateMachine.SetState((IState) heroFsm.StateStand);
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
        else
          this.m_fireSubState.Update(stateMachine, gameTime);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine) => this.m_fireSubState.Exit(stateMachine);
  }
}
