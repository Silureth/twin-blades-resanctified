// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Script.ScriptFunctionSetTimer
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

namespace PSS.GameEngine.Script
{
  public class ScriptFunctionSetTimer : IScriptFunction
  {
    public ScriptFunctionSetTimer(float time, PSS.GameEngine.World.World world, ScriptManager.ScriptDelegate script) => ScriptManager.Instance.SetTimerEvent(time, world, script);

    public bool Update(float time) => true;

    public void Initialize()
    {
    }

    public void Cleanup()
    {
    }
  }
}
