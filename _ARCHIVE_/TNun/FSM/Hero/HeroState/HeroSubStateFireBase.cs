// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroSubStateFireBase
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroSubStateFireBase : IState
  {
    public void Enter(IFiniteStateMachine stateMachine) => ((PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity()).HasFired = false;

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.GetAnimationStateMachine().FireTransitionAnimationIndex < 1 || entity.HasFired)
        return;
      entity.HasFired = true;
      entity.GetGun().Fire();
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
