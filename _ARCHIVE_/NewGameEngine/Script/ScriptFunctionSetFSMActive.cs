// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Script.ScriptFunctionSetFSMActive
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.FSM;

namespace PSS.GameEngine.Script
{
  public class ScriptFunctionSetFSMActive : IScriptFunction
  {
    private IFiniteStateMachine m_stateMachine;
    private bool m_active;

    public ScriptFunctionSetFSMActive(IFiniteStateMachine stateMachine, bool active)
    {
      this.m_stateMachine = stateMachine;
      this.m_active = active;
    }

    public bool Update(float gameTime)
    {
      this.m_stateMachine.IsActive = this.m_active;
      return true;
    }

    public void Initialize()
    {
    }

    public void Cleanup()
    {
    }
  }
}
