using Resanctified.Engine.FSM;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetFSMActive : IScriptFunction
    {
        private IFiniteStateMachine m_stateMachine;
        private bool m_active;

        public ScriptFunctionSetFSMActive(IFiniteStateMachine stateMachine, bool active)
        {
            this.m_stateMachine = stateMachine;
            this.m_active = active;
        }

        public bool Update(float gameTime)
        {
            this.m_stateMachine.IsActive = this.m_active;
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
