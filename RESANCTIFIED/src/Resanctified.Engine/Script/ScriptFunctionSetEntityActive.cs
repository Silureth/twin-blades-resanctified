// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSetEntityActive
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSetEntityActive : IScriptFunction
  {
    private Entity m_entity;
    private bool m_active;

    public ScriptFunctionSetEntityActive(Entity entity, bool active)
    {
      this.m_entity = entity;
      this.m_active = active;
    }

    public bool Update(float gameTime)
    {
      this.m_entity.IsActive = this.m_active;
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
