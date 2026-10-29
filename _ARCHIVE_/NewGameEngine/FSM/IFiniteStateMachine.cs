// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.FSM.IFiniteStateMachine
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

namespace PSS.GameEngine.FSM
{
  public interface IFiniteStateMachine
  {
    void SetState(IState state);

    IState CurrentState { get; }

    IState GetCurrentState();

    IState PreviousState { get; }

    IState GetPreviousState();

    IFiniteStateMachine ParentStateMachine { get; }

    double CurrentStateTime { get; }

    double GetCurrentStateTime();

    bool IsActive { get; set; }

    void Initialize();

    void Update(float gameTime);

    void Cleanup();
  }
}
