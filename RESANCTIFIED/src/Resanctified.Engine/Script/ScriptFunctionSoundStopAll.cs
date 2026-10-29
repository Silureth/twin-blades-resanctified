// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSoundStopAll
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSoundStopAll : IScriptFunction
  {
    public bool Update(float gameTime)
    {
      foreach (Sound scriptSound in ScriptManager.Instance.ScriptSoundList)
        scriptSound.Stop();
      ScriptManager.Instance.ScriptSoundList.Clear();
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
