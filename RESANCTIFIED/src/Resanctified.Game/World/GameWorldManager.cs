using Resanctified.Engine.World;

namespace Resanctified.Game.World
{
    public class GameWorldManager : WorldManager
    {
        public static GameWorldManager Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new GameWorldManager();
                return (GameWorldManager)m_instance;
            }
        }

        public static GameWorldManager GetInstance()
        {
            if (m_instance == null)
                m_instance = new GameWorldManager();
            return (GameWorldManager)m_instance;
        }

        public override void Initialize() => base.Initialize();

        public override void Cleanup() => base.Cleanup();

        public override void Cleanup(bool cleanupWorlds, bool deleteWorlds) => base.Cleanup(cleanupWorlds, deleteWorlds);

        public override void Update(float gameTime) => base.Update(gameTime);
    }
}
