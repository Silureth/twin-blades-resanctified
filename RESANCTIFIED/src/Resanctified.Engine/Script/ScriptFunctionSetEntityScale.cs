using Microsoft.Xna.Framework;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetEntityScale : IScriptFunction
    {
        private Entity m_entity;
        private Vector2 m_scale;

        public ScriptFunctionSetEntityScale(Entity entity, Vector2 scale)
        {
            this.m_entity = entity;
            this.m_scale = scale;
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
