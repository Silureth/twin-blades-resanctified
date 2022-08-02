// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Event.GameEventManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.Collections;

namespace PSS.GameEngine.Event
{
  public class GameEventManager : IGameComponent
  {
    private PList<GameEvent> m_raisedEventList;
    private static GameEventManager m_instance = new GameEventManager();

    public static GameEventManager Instance => GameEventManager.m_instance;

    public PList<GameEvent> RaisedEventList => this.m_raisedEventList;

    public GameEventManager() => this.m_raisedEventList = new PList<GameEvent>();

    public void Update(float gameTime)
    {
      foreach (GameEvent raisedEvent in this.m_raisedEventList)
        raisedEvent.ExecuteScript();
      this.m_raisedEventList.Clear();
    }

    public void Initialize()
    {
    }

    public void Cleanup()
    {
    }
  }
}
