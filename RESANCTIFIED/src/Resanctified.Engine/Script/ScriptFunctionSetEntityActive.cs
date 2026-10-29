using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetEntityActive : IScriptFunction
    {
        private Entity m_entity;
        private bool m_active;

        public ScriptFunctionSetEntityActive(Entity entity, bool active)
        {
            this.m_entity = entity;
            this.m_active = active;
        }

        public bool Update(float gameTime)
        {
            this.m_entity.IsActive = this.m_active;
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
