using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionTransitionFadeToColor : IScriptFunction
    {
        private double m_timer;
        private double m_time;

        public ScriptFunctionTransitionFadeToColor(Color color, float time)
        {
            this.m_time = time;
            this.m_timer = 0.0;
        }

        public bool Update(float gameTime)
        {
            this.m_timer -= gameTime;
            return this.m_timer <= 0.0;
        }

        public void Initialize() => this.m_timer = this.m_time;

        public void Cleanup()
        {
        }
    }
}
