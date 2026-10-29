// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionTransitionFadeFromColor
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionTransitionFadeFromColor : IScriptFunction
  {
    private double m_timer;
    private double m_time;

    public ScriptFunctionTransitionFadeFromColor(Color color, float time)
    {
      this.m_time = (double) time;
      this.m_timer = 0.0;
    }

    public bool Update(float gameTime)
    {
      this.m_timer -= (double) gameTime;
      return this.m_timer <= 0.0;
    }

    public void Initialize() => this.m_timer = this.m_time;

    public void Cleanup()
    {
    }
  }
}
