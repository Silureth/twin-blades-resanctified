using Resanctified.Engine.World.Zone;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetTriggerActive : IScriptFunction
    {
        private Trigger m_trigger;
        private bool m_active;

        public ScriptFunctionSetTriggerActive(Trigger trigger, bool active)
        {
            this.m_trigger = trigger;
            this.m_active = active;
        }

        public bool Update(float gameTime)
        {
            this.m_trigger.IsActive = this.m_active;
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
