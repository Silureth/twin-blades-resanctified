using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetEntityRotation : IScriptFunction
    {
        private Entity m_entity;
        private float m_rotation;

        public ScriptFunctionSetEntityRotation(Entity entity, float rotation)
        {
            this.m_entity = entity;
            this.m_rotation = rotation;
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
