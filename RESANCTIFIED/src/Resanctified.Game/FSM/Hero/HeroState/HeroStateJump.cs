// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroState.HeroStateJump
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;

namespace Resanctified.Game.FSM.Hero.HeroState
{
  public class HeroStateJump : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateJump.GetInstance());
      heroFsm.NextAction = HeroFSM.HeroActionId.None;
      if (MathHelperEx.GetRandomInt(0, 100) < 50)
        entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Jump_1).Play();
      else
        entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Jump_2).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      if (entity.IsHit)
      {
        stateMachine.SetState((IState) heroFsm.StateHit);
        entity.GetPhysics().Velocity.X = 0.0f;
        entity.GetPhysics().Velocity.Y = 0.0f;
      }
      else
      {
        heroFsm.UpdateNextAction();
        if (heroFsm.NextAction != HeroFSM.HeroActionId.Slash || !entity.AirSlashAvailable)
          heroFsm.NextAction = HeroFSM.HeroActionId.None;
        if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
          return;
        Vector2 thumbstickPosition = InputManager.GetInstance().GetThumbstickPosition(PSSThumbstick.LeftThumbstick);
        if ((double) thumbstickPosition.X == 0.0)
        {
          if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive())
            thumbstickPosition.X = -1f;
          else if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
            thumbstickPosition.X = 1f;
        }
        entity.GetPhysics().Velocity.X = thumbstickPosition.X * 2f;
        entity.GetPhysics().Velocity.Y = 8f;
        stateMachine.SetState((IState) heroFsm.StateAir);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
