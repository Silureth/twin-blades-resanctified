using Resanctified.Engine.Collections;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetCameraTarget : IScriptFunction
    {
        private CameraEntity m_camera;
        private PList<Entity> m_entityList;

        public ScriptFunctionSetCameraTarget(CameraEntity camera, PList<Entity> entityList)
        {
            this.m_entityList = entityList;
            this.m_camera = camera;
        }

        public bool Update(float gameTime) => true;

        public void Initialize()
        {
        }

        public void Cleanup()
        {
            if (this.m_entityList != null)
                this.m_entityList.Clear();
            this.m_entityList = null;
        }
    }
}
