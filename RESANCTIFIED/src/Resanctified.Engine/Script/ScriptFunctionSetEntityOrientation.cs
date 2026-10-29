using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetEntityOrientation : IScriptFunction
    {
        private Entity m_entity;
        private Orientation m_orientation;

        public ScriptFunctionSetEntityOrientation(
          Entity entity,
          Orientation orientation)
        {
            this.m_entity = entity;
            this.m_orientation = orientation;
        }

        public bool Update(float gameTime) => true;

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }

        public enum Orientation
        {
            Left,
            Right,
        }
    }
}
