namespace Resanctified.Engine.Script
{
    public class ScriptFunctionWait : IScriptFunction
    {
        private double m_timer;

        public ScriptFunctionWait(float time) => this.m_timer = time;

        public bool Update(float gameTime)
        {
            if (this.m_timer > 0.0)
            {
                this.m_timer -= gameTime;
                if (this.m_timer <= 0.0)
                    return true;
            }
            return false;
        }

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }
    }
}
