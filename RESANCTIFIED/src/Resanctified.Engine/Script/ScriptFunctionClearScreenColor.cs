// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionClearScreenColor
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionClearScreenColor : IScriptFunction
  {
    private double m_timer;

    public ScriptFunctionClearScreenColor(Color color, float time)
    {
      this.m_timer = (double) time;
      Resanctified.Engine.Graphics.Renderer.Renderer.Instance.ClearColor = color;
    }

    public bool Update(float gameTime)
    {
      this.m_timer -= (double) gameTime;
      return this.m_timer <= 0.0;
    }

    public void Initialize()
    {
    }

    public void Cleanup()
    {
    }
  }
}
