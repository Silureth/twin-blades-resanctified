namespace Resanctified.Engine.FSM
{
    public interface IFiniteStateMachine
    {
        void SetState(IState state);

        IState CurrentState { get; }

        IState GetCurrentState();

        IState PreviousState { get; }

        IState GetPreviousState();

        IFiniteStateMachine ParentStateMachine { get; }

        double CurrentStateTime { get; }

        double GetCurrentStateTime();

        bool IsActive { get; set; }

        void Initialize();

        void Update(float gameTime);

        void Cleanup();
    }
}
