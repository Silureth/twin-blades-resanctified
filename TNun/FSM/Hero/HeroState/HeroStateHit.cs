// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroStateHit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Maths;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroStateHit : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.IsInAir)
      {
        entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateAirHit.GetInstance());
        if ((double) entity.GetPhysics().Velocity.Y > 0.0)
          entity.GetPhysics().Velocity.Y = 0.0f;
        entity.IsGrabbed = false;
      }
      else
      {
        if (entity.IsGrabbed)
          entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateGrabHit.GetInstance());
        else
          entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateStandHit.GetInstance());
        entity.GetPhysics().Velocity.X = 0.0f;
        entity.GetPhysics().Velocity.Y = 0.0f;
      }
      if (!entity.IsGrabbed && entity.HitPoints > 0)
        entity.SetIsInvulnerable(true);
      int randomInt = MathHelperEx.GetRandomInt(0, 100);
      if (randomInt < 33)
        entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Hit_1).Play();
      else if (randomInt < 66)
        entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Hit_2).Play();
      else
        entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Hit_3).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      heroFsm.UpdateNextAction();
      if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      if (entity.HitPoints <= 0)
      {
        if (entity.IsInAir)
          return;
        stateMachine.SetState((IState) heroFsm.StateDying);
      }
      else if (entity.IsInAir)
        stateMachine.SetState((IState) heroFsm.StateAir);
      else if (entity.IsGrabbed)
        stateMachine.SetState((IState) heroFsm.StateGrabbed);
      else
        stateMachine.SetState((IState) heroFsm.StateStand);
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity()).IsHit = false;
  }
}
