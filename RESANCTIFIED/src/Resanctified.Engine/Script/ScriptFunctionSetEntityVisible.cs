using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetEntityVisible : IScriptFunction
    {
        private Entity m_entity;
        private bool m_visible;

        public ScriptFunctionSetEntityVisible(Entity entity, bool visible)
        {
            this.m_entity = entity;
            this.m_visible = visible;
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
