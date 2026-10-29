// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSetTimer
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSetTimer : IScriptFunction
  {
    public ScriptFunctionSetTimer(float time, Resanctified.Engine.World.World world, ScriptManager.ScriptDelegate script) => ScriptManager.Instance.SetTimerEvent(time, world, script);

    public bool Update(float time) => true;

    public void Initialize()
    {
    }

    public void Cleanup()
    {
    }
  }
}
