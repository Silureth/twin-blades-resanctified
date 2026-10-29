namespace Resanctified.Engine.FSM
{
    public class StateDefault : IState
    {
        private static StateDefault m_instance = new StateDefault();

        public static StateDefault Instance => m_instance;

        public static StateDefault GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
