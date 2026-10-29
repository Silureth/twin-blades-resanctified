using System.Collections.Generic;

namespace Resanctified.Engine.World
{
    public class WorldManager : IGameComponent
    {
        protected static WorldManager m_instance;
        protected World m_currentWorld;
        protected List<World> m_worldList;

        public static WorldManager Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new WorldManager();
                return m_instance;
            }
        }

        public static WorldManager GetInstance() => Instance;

        public WorldManager()
        {
            this.m_worldList = new List<World>();
            this.m_currentWorld = null;
        }

        public virtual void Initialize() => this.m_currentWorld = null;

        public virtual void Update(float gameTime)
        {
            for (int index = 0; index < this.m_worldList.Count; ++index)
                this.m_worldList[index].Update(gameTime);
        }

        public virtual void Cleanup() => this.Cleanup(false, true);

        public virtual void Cleanup(bool cleanupWorlds, bool deleteWorlds)
        {
            if (cleanupWorlds || deleteWorlds)
            {
                for (int index = 0; index < this.m_worldList.Count; ++index)
                    this.m_worldList[index].Cleanup();
            }
            this.m_worldList.Clear();
            this.m_currentWorld = null;
        }

        public int GetNumWorlds() => this.m_worldList.Count;

        public World GetCurrentWorld() => this.m_currentWorld;

        public void SetCurrentWorld(World world) => this.m_currentWorld = world;

        public World GetWorldAtIndex(int index) => this.m_worldList[index];

        public void AddWorld(World world) => this.AddWorld(world, false);

        public void AddWorld(World world, bool initializeWhenAdded)
        {
            this.m_worldList.Add(world);
            if (!initializeWhenAdded)
                return;
            world.Initialize();
        }

        private void RemoveWorld(World world) => this.RemoveWorld(world, false, true);

        private void RemoveWorld(World world, bool cleanupWhenRemoved, bool deleteWhenRemoved)
        {
            if (cleanupWhenRemoved || deleteWhenRemoved)
                world.Cleanup();
            this.m_worldList.Remove(world);
            if (!ReferenceEquals(world, this.m_currentWorld))
                return;
            this.m_currentWorld = null;
        }
    }
}
