// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSetEntityRotation
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSetEntityRotation : IScriptFunction
  {
    private Entity m_entity;
    private float m_rotation;

    public ScriptFunctionSetEntityRotation(Entity entity, float rotation)
    {
      this.m_entity = entity;
      this.m_rotation = rotation;
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
