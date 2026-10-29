using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.World
{
    public class World
    {
        protected EntityManager m_entityManager;
        protected EntityFactory m_entityFactory;

        public World()
        {
            this.m_entityManager = new EntityManager();
            this.m_entityFactory = new EntityFactory();
        }

        public virtual void Initialize()
        {
            this.m_entityManager.Initialize();
            this.m_entityFactory.Initialize();
        }

        public virtual void Update(float gameTime) => this.m_entityManager.Update(gameTime);

        public virtual void Cleanup()
        {
            this.m_entityManager.Cleanup();
            this.m_entityFactory.Cleanup();
        }

        public EntityManager GetEntityManager() => this.m_entityManager;

        public EntityFactory GetEntityFactory() => this.m_entityFactory;
    }
}
