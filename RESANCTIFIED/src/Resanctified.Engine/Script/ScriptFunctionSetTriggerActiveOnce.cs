// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionSetTriggerActiveOnce
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.World.Zone;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionSetTriggerActiveOnce : IScriptFunction
  {
    private Trigger m_trigger;
    private bool m_activeOnce;

    public ScriptFunctionSetTriggerActiveOnce(Trigger trigger, bool activeOnce)
    {
      this.m_trigger = trigger;
      this.m_activeOnce = activeOnce;
    }

    public bool Update(float gameTime)
    {
      this.m_trigger.ActivateOnlyOnce = this.m_activeOnce;
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
