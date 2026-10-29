// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSetTriggerActive
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.World.Zone;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSetTriggerActive : IScriptFunction
  {
    private Trigger m_trigger;
    private bool m_active;

    public ScriptFunctionSetTriggerActive(Trigger trigger, bool active)
    {
      this.m_trigger = trigger;
      this.m_active = active;
    }

    public bool Update(float gameTime)
    {
      this.m_trigger.IsActive = this.m_active;
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
