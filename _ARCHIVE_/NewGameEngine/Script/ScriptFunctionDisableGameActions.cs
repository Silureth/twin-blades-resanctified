// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Script.ScriptFunctionDisableGameActions
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;

namespace PSS.GameEngine.Script
{
  public class ScriptFunctionDisableGameActions : IScriptFunction
  {
    private bool m_disable;
    private PlayerIndex m_playerId;

    public ScriptFunctionDisableGameActions(bool disable, PlayerIndex playerId)
    {
      this.m_disable = disable;
      this.m_playerId = playerId;
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
