// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Script.ScriptFunctionMusicPlay
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

namespace PSS.GameEngine.Script
{
  public class ScriptFunctionMusicPlay : IScriptFunction
  {
    private int m_index;
    private float m_volume;
    private bool m_loop;

    public ScriptFunctionMusicPlay(int index, float volume, bool loop)
    {
      this.m_index = index;
      this.m_volume = volume;
      this.m_loop = loop;
    }

    public bool Update(float gameTime) => true;

    public void Initialize()
    {
    }

    public void Cleanup()
    {
    }
  }
}
