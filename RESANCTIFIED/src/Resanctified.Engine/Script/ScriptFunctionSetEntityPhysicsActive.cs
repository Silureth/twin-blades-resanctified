using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetEntityPhysicsActive : IScriptFunction
    {
        private Entity m_entity;
        private bool m_active;

        public ScriptFunctionSetEntityPhysicsActive(Entity entity, bool active)
        {
            this.m_entity = entity;
            this.m_active = active;
        }

        public bool Update(float gameTime) => true;

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }
    }
}
