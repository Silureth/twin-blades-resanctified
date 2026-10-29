using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    // Shows the local highscores only; the Xbox Live leaderboard pages of the phone version are gone.
    public class InGameMenuStateHighscoresSurvival : IDrawableState, IState
    {
        private static InGameMenuStateHighscoresSurvival m_instance = new InGameMenuStateHighscoresSurvival();
        public bool BackRequest;

        public static InGameMenuStateHighscoresSurvival Instance => m_instance;

        public static InGameMenuStateHighscoresSurvival GetInstance() => m_instance;

        public InGameMenuStateHighscoresSurvival() => this.BackRequest = false;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameHighscoresView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            GameApplication.GetInstance().GetInGameHighscoresView().Update(gameTime);
            if (!this.BackRequest)
                return;
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            stateMachine.SetState(InGameMenuStateHighscoresChoose.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameHighscoresView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHighscoresView().Draw();
    }
}
