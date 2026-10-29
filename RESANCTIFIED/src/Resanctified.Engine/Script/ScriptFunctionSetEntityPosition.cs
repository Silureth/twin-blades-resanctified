using Microsoft.Xna.Framework;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetEntityPosition : IScriptFunction
    {
        private Entity m_entity;
        private Entity m_target;
        private Vector3 m_position;

        public ScriptFunctionSetEntityPosition(Entity entity, Vector3 position)
        {
            this.m_entity = entity;
            this.m_target = null;
            this.m_position = position;
        }

        public ScriptFunctionSetEntityPosition(Entity entity, Entity target)
        {
            this.m_entity = entity;
            this.m_target = target;
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
