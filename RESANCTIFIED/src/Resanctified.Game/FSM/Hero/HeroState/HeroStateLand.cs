// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroState.HeroStateLand
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.Hero.HeroState
{
  public class HeroStateLand : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateLand.GetInstance());
      entity.GetPhysics().Velocity.X = 0.0f;
      entity.GetPhysics().Velocity.Y = 0.0f;
      entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Land).Play();
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
        if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
        {
          stateMachine.SetState((IState) heroFsm.StateWalk);
        }
        else
        {
          if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
            return;
          stateMachine.SetState((IState) heroFsm.StateStand);
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
