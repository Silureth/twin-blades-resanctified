// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Script.ScriptManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.Collections;
using PSS.GameEngine.Event;
using PSS.GameEngine.Sounds;

namespace PSS.GameEngine.Script
{
  public class ScriptManager : IGameComponent
  {
    private double m_timer;
    private GameEvent m_timeUpEvent;
    private PList<ScriptSequence> m_scriptSequenceList;
    private PList<ScriptSequence> m_scriptSequenceListSecondary;
    private static ScriptManager m_instance = new ScriptManager();
    private PList<Sound> m_scriptSoundList;
    private bool m_firstUpdate;
    public PSS.GameEngine.World.World WorldForSequenceInit;

    public static ScriptManager Instance => ScriptManager.m_instance;

    public bool IsScriptExecuting => this.m_scriptSequenceList.Count != 0 || this.m_scriptSequenceListSecondary.Count != 0;

    public PList<Sound> ScriptSoundList => this.m_scriptSoundList;

    public ScriptManager()
    {
      this.m_timer = 0.0;
      this.m_timeUpEvent = (GameEvent) null;
      this.WorldForSequenceInit = (PSS.GameEngine.World.World) null;
      this.m_scriptSequenceList = new PList<ScriptSequence>();
      this.m_scriptSequenceListSecondary = new PList<ScriptSequence>();
      this.m_scriptSoundList = new PList<Sound>();
    }

    public void Update(float gameTime)
    {
      if (this.m_firstUpdate)
        this.m_firstUpdate = false;
      if (this.m_timer > 0.0)
      {
        this.m_timer -= (double) gameTime;
        if (this.m_timer <= 0.0)
        {
          this.m_timer = 0.0;
          if (this.m_timeUpEvent != null)
            this.m_timeUpEvent.RaiseEvent();
        }
      }
      PList<ScriptSequence> plist1 = new PList<ScriptSequence>();
      PList<ScriptSequence> plist2 = new PList<ScriptSequence>();
      foreach (ScriptSequence scriptSequence in this.m_scriptSequenceList)
      {
        if (scriptSequence.Update(gameTime))
          plist1.Add(scriptSequence);
      }
      foreach (ScriptSequence scriptSequence in this.m_scriptSequenceListSecondary)
      {
        if (scriptSequence.Update(gameTime))
          plist2.Add(scriptSequence);
      }
      foreach (ScriptSequence scriptSequence in plist1)
        this.m_scriptSequenceList.Remove(scriptSequence);
      foreach (ScriptSequence scriptSequence in plist2)
        this.m_scriptSequenceListSecondary.Remove(scriptSequence);
      plist1.Clear();
      plist2.Clear();
    }

    public void Initialize() => this.m_firstUpdate = true;

    public void Cleanup()
    {
      this.m_scriptSequenceList.Clear();
      this.m_scriptSequenceListSecondary.Clear();
      this.m_scriptSoundList.Clear();
    }

    public void SetTimerEvent(float time, PSS.GameEngine.World.World world, ScriptManager.ScriptDelegate timeUpScript)
    {
      this.m_timer = (double) time;
      this.m_timeUpEvent = new GameEvent(timeUpScript, world);
    }

    public void AddScriptSequence(ScriptManager.ScriptDelegate script, PSS.GameEngine.World.World world)
    {
      this.WorldForSequenceInit = world;
      this.m_scriptSequenceList.Add(new ScriptSequence());
      script();
      this.WorldForSequenceInit = (PSS.GameEngine.World.World) null;
    }

    public void AddScriptFunction(IScriptFunction function) => this.m_scriptSequenceList[this.m_scriptSequenceList.Count - 1].AddScriptFunction(function);

    public void AddScriptFunctionSecondary(IScriptFunction function)
    {
      ScriptSequence scriptSequence = new ScriptSequence();
      scriptSequence.AddScriptFunction(function);
      this.m_scriptSequenceListSecondary.Add(scriptSequence);
    }

    public delegate void ScriptDelegate();
  }
}
