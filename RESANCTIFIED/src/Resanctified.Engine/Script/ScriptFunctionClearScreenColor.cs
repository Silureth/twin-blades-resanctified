using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionClearScreenColor : IScriptFunction
    {
        private double m_timer;

        public ScriptFunctionClearScreenColor(Color color, float time)
        {
            this.m_timer = time;
            Graphics.Renderer.Renderer.Instance.ClearColor = color;
        }

        public bool Update(float gameTime)
        {
            this.m_timer -= gameTime;
            return this.m_timer <= 0.0;
        }

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }
    }
}
