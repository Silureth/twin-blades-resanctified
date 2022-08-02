// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Script.ScriptFunctionSetEntityPhysicsActive
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.World.Entities;

namespace PSS.GameEngine.Script
{
  public class ScriptFunctionSetEntityPhysicsActive : IScriptFunction
  {
    private Entity m_entity;
    private bool m_active;

    public ScriptFunctionSetEntityPhysicsActive(Entity entity, bool active)
    {
      this.m_entity = entity;
      this.m_active = active;
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
