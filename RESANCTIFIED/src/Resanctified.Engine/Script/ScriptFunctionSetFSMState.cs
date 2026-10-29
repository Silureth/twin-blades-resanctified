using Resanctified.Engine.FSM;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSetFSMState : IScriptFunction
    {
        private IFiniteStateMachine m_stateMachine;
        private IState m_state;

        public ScriptFunctionSetFSMState(IFiniteStateMachine stateMachine, IState state)
        {
            this.m_stateMachine = stateMachine;
            this.m_state = state;
        }

        public bool Update(float gameTime)
        {
            this.m_stateMachine.SetState(this.m_state);
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
