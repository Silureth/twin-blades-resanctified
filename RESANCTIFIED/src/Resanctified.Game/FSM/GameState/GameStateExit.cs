using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateExit : IDrawableState, IState
    {
        private static GameStateExit m_instance = new GameStateExit();

        public static GameStateExit Instance => m_instance;

        public static GameStateExit GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime) => GameApplication.Instance.Exit();

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
        }
    }
}
