// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSoundPlay
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSoundPlay : IScriptFunction
  {
    private Sound m_sound;

    public ScriptFunctionSoundPlay(string soundName, string bankName)
    {
    }

    public bool Update(float gameTime) => true;

    public void Initialize()
    {
    }

    public void Cleanup() => this.m_sound = (Sound) null;
  }
}
