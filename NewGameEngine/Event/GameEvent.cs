// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Event.GameEvent
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.Script;

namespace PSS.GameEngine.Event
{
  public class GameEvent
  {
    protected ScriptManager.ScriptDelegate m_script;
    private PSS.GameEngine.World.World m_world;

    public GameEvent(ScriptManager.ScriptDelegate script, PSS.GameEngine.World.World world)
    {
      this.m_script = script;
      this.m_world = world;
    }

    public void RaiseEvent() => GameEventManager.Instance.RaisedEventList.Add(this);

    public void ExecuteScript() => ScriptManager.Instance.AddScriptSequence(this.m_script, this.m_world);
  }
}
