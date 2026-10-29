// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.FSM.IState
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

namespace Resanctified.Engine.FSM
{
  public interface IState
  {
    void Enter(IFiniteStateMachine stateMachine);

    void Update(IFiniteStateMachine stateMachine, float gameTime);

    void Exit(IFiniteStateMachine stateMachine);
  }
}
