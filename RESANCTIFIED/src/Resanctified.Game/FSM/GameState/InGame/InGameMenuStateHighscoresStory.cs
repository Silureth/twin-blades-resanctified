using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    // Shows the local highscores only; the Xbox Live leaderboard pages of the phone version are gone.
    public class InGameMenuStateHighscoresStory : IDrawableState, IState
    {
        private static InGameMenuStateHighscoresStory m_instance = new InGameMenuStateHighscoresStory();
        public bool BackRequest;

        public static InGameMenuStateHighscoresStory Instance => m_instance;

        public static InGameMenuStateHighscoresStory GetInstance() => m_instance;

        public InGameMenuStateHighscoresStory() => this.BackRequest = false;

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
