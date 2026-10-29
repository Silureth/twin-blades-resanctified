// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroState.HeroStatePushed
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.Hero.HeroState
{
  public class HeroStatePushed : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.IsInAir)
      {
        entity.GetPhysics().Velocity.Y = 0.0f;
        entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateAirHit.GetInstance());
      }
      else
      {
        entity.GetPhysics().Velocity.X = 0.0f;
        entity.GetPhysics().Velocity.Y = 0.0f;
        entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateStandHit.GetInstance());
      }
      entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_PushBack).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      heroFsm.UpdateNextAction();
      if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      if (entity.IsInAir)
        stateMachine.SetState((IState) heroFsm.StateAir);
      else
        stateMachine.SetState((IState) heroFsm.StateStand);
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetPhysics().Velocity.X = 0.0f;
      entity.GetPhysics().Velocity.Y = 0.0f;
    }
  }
}
