// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroState.HeroStateGrabbed
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;

namespace Resanctified.Game.FSM.Hero.HeroState
{
  public class HeroStateGrabbed : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateGrabbed.GetInstance());
      entity.GetPhysics().Velocity.X = 0.0f;
      entity.GetPhysics().Acceleration.X = 0.0f;
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHud().ShowShakeAnim(true);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      if (entity.HitPoints <= 0)
      {
        currentWorld.GetHud().ShowShakeAnim(false);
        stateMachine.SetState((IState) heroFsm.StateDying);
      }
      else
      {
        if (!heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.PushBack).IsActive() && heroFsm.GetCurrentStateTime() <= 3.0)
          return;
        entity.IsGrabbed = false;
        currentWorld.GetHud().ShowShakeAnim(false);
        stateMachine.SetState((IState) heroFsm.StatePushBack);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
