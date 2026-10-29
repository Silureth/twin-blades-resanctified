using Resanctified.Engine.Collections;

namespace Resanctified.Engine.Event
{
    public class GameEventManager : IGameComponent
    {
        private PList<GameEvent> m_raisedEventList;
        private static GameEventManager m_instance = new GameEventManager();

        public static GameEventManager Instance => m_instance;

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
