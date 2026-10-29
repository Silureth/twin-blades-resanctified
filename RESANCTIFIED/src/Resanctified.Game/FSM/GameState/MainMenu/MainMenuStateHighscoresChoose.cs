using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateHighscoresChoose : IDrawableState, IState
    {
        private static MainMenuStateHighscoresChoose m_instance = new MainMenuStateHighscoresChoose();
        public bool BackRequest;
        public bool StoryRequest;
        public bool SurvivalRequest;

        public static MainMenuStateHighscoresChoose Instance => m_instance;

        public static MainMenuStateHighscoresChoose GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.StoryRequest = false;
            this.SurvivalRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetHighscoresChooseView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (this.BackRequest)
            {
                stateMachine.SetState(MainMenuStateMenu.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
            else if (this.StoryRequest)
            {
                stateMachine.SetState(MainMenuStateHighscoresStory.GetInstance());
                GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            else
            {
                if (!this.SurvivalRequest)
                    return;
                stateMachine.SetState(MainMenuStateHighscoresSurvival.GetInstance());
                GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = false;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.StoryRequest = false;
            this.SurvivalRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetHighscoresChooseView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHighscoresChooseView().Draw();
    }
}
