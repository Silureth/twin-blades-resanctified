// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSetEntityPosition
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSetEntityPosition : IScriptFunction
  {
    private Entity m_entity;
    private Entity m_target;
    private Vector3 m_position;

    public ScriptFunctionSetEntityPosition(Entity entity, Vector3 position)
    {
      this.m_entity = entity;
      this.m_target = (Entity) null;
      this.m_position = position;
    }

    public ScriptFunctionSetEntityPosition(Entity entity, Entity target)
    {
      this.m_entity = entity;
      this.m_target = target;
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
