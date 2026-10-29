namespace Resanctified.Engine.FSM
{
    public interface IDrawableState : IState
    {
        void Draw(IFiniteStateMachine stateMachine);
    }
}
