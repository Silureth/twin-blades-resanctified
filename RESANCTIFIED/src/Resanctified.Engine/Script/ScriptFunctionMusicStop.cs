// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionMusicStop
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionMusicStop : IScriptFunction
  {
    public bool Update(float gameTime)
    {
      AudioManager.Instance.StopMusic();
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
