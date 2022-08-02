// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.FSM.StateDefault
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

namespace PSS.GameEngine.FSM
{
  public class StateDefault : IState
  {
    private static StateDefault m_instance = new StateDefault();

    public static StateDefault Instance => StateDefault.m_instance;

    public static StateDefault GetInstance() => StateDefault.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
