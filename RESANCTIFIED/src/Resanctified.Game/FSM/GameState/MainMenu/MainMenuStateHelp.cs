using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateHelp : IDrawableState, IState
    {
        private static MainMenuStateHelp m_instance = new MainMenuStateHelp();

        public static MainMenuStateHelp Instance => m_instance;

        public static MainMenuStateHelp GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetHelpView());

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
            {
                stateMachine.SetState(MainMenuStateHelpAndOptions.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
            else
            {
                GameApplication.GetInstance().GetInGameHelpView().Update(gameTime);
                if (GameStateMainMenu.GetInstance().GetHelpView().GetPage() <= 1)
                    return;
                stateMachine.SetState(MainMenuStateHelpAndOptions.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            GameApplication.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetHelpView());
            GameStateMainMenu.GetInstance().GetHelpView().ResetPageIndex();
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHelpView().Draw();
    }
}
