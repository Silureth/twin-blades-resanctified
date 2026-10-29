using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.FSM
{
    public class EntityFiniteStateMachine : FiniteStateMachine
    {
        private IEntity m_entity;

        public EntityFiniteStateMachine() => this.m_entity = null;

        public EntityFiniteStateMachine(IEntity entity) => this.m_entity = entity;

        public IEntity Entity => this.m_entity;

        public IEntity GetEntity() => this.m_entity;
    }
}
