// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroStateDying
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Maths;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroStateDying : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.IsGrabbed)
      {
        entity.IsGrabbed = false;
        entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateGrabDying.GetInstance());
      }
      else
        entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateDying.GetInstance());
      entity.GetPhysics().Velocity.X = 0.0f;
      entity.GetPhysics().Velocity.Y = 0.0f;
      if (MathHelperEx.GetRandomInt(0, 100) < 50)
        entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Dying_1).Play();
      else
        entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Dying_2).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      if (!((PlayableCharacter) heroFsm.GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      stateMachine.SetState((IState) heroFsm.StateDead);
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
