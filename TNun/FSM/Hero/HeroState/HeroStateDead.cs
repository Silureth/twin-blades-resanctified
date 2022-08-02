// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroStateDead
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroStateDead : IState
  {
    public void Enter(IFiniteStateMachine stateMachine) => ((PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetAnimationStateMachine().SetState((IState) HeroAnimationStateDead.GetInstance());

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (stateMachine.GetCurrentStateTime() < 0.5)
        return;
      InGameStatePlay.GetInstance().GameOverRequest = true;
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
