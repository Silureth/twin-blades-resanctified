// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroStateWalkFire
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroStateWalkFire : HeroStateWalk
  {
    private HeroSubStateFireBase m_fireSubState;
    private bool m_forceAnimReset;

    public HeroStateWalkFire()
    {
      this.m_fireSubState = new HeroSubStateFireBase();
      this.m_forceAnimReset = false;
    }

    public override void Enter(IFiniteStateMachine stateMachine)
    {
      this.m_fireSubState.Enter(stateMachine);
      this.m_forceAnimReset = true;
      ((HeroFSM) stateMachine).NextAction = HeroFSM.HeroActionId.None;
      base.Enter(stateMachine);
    }

    public override void Update(IFiniteStateMachine stateMachine, float gameTime) => base.Update(stateMachine, gameTime);

    public override void Exit(IFiniteStateMachine stateMachine)
    {
      this.m_fireSubState.Exit(stateMachine);
      base.Exit(stateMachine);
    }

    protected override void SetAnimation(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.GetAnimationStateMachine().IsInState((IState) HeroAnimationStateWalkFireRight.GetInstance()) && !this.m_forceAnimReset)
        return;
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStateWalkFireRight.GetInstance());
      this.m_forceAnimReset = false;
    }

    protected override void UpdateStateFromGameActions(
      IFiniteStateMachine stateMachine,
      float gameTime)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.HasFired && entity.GetAnimationStateMachine().FireTransitionAnimationIndex >= 1)
        base.UpdateStateFromGameActions(stateMachine, gameTime);
      else
        this.m_fireSubState.Update(stateMachine, gameTime);
    }

    protected override void UpdateMove(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroFsm.GetEntity();
      this.SetMovePhysics(stateMachine);
      if (entity.GetAnimationStateMachine().FireTransitionAnimationIndex != 2)
        return;
      stateMachine.SetState((IState) heroFsm.StateWalk);
    }
  }
}
