using Resanctified.Engine.World.Zone;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetTriggerActiveOnce : IScriptFunction
    {
        private Trigger m_trigger;
        private bool m_activeOnce;

        public ScriptFunctionSetTriggerActiveOnce(Trigger trigger, bool activeOnce)
        {
            this.m_trigger = trigger;
            this.m_activeOnce = activeOnce;
        }

        public bool Update(float gameTime)
        {
            this.m_trigger.ActivateOnlyOnce = this.m_activeOnce;
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
