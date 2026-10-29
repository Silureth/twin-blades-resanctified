using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    // Shows the local highscores only; the Xbox Live leaderboard pages of the phone version are gone.
    public class MainMenuStateHighscoresSurvival : IDrawableState, IState
    {
        private static MainMenuStateHighscoresSurvival m_instance = new MainMenuStateHighscoresSurvival();
        public bool BackRequest;

        public static MainMenuStateHighscoresSurvival Instance => m_instance;

        public static MainMenuStateHighscoresSurvival GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetHighscoresView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            GameStateMainMenu.GetInstance().GetHighscoresView().Update(gameTime);
            if (!this.BackRequest)
                return;
            if (GameStateMainMenu.GetInstance().GetHighscoresView().GetBlinkIndex() > 0)
            {
                stateMachine.SetState(MainMenuStateMenu.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            else
            {
                stateMachine.SetState(MainMenuStateHighscoresChoose.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(0);
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetHighscoresView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHighscoresView().Draw();
    }
}
