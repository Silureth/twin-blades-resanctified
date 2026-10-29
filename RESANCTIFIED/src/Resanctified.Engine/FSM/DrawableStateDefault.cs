namespace Resanctified.Engine.FSM
{
    public class DrawableStateDefault : IDrawableState, IState
    {
        private static DrawableStateDefault m_instance = new DrawableStateDefault();

        public static DrawableStateDefault Instance => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
        }
    }
}
