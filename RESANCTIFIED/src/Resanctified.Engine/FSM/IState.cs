namespace Resanctified.Engine.FSM
{
    public interface IState
    {
        void Enter(IFiniteStateMachine stateMachine);

        void Update(IFiniteStateMachine stateMachine, float gameTime);

        void Exit(IFiniteStateMachine stateMachine);
    }
}
