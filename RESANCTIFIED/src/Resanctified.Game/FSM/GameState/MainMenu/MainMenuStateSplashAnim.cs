using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateSplashAnim : IDrawableState, IState
    {
        private static MainMenuStateSplashAnim m_instance = new MainMenuStateSplashAnim();

        public static MainMenuStateSplashAnim Instance => m_instance;

        public static MainMenuStateSplashAnim GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime) => stateMachine.SetState(MainMenuStatePressStart.GetInstance());

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
        }
    }
}
