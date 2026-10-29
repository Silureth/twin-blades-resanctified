using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateHighscoresChoose : IDrawableState, IState
    {
        private static InGameMenuStateHighscoresChoose m_instance = new InGameMenuStateHighscoresChoose();
        public bool BackRequest;
        public bool StoryRequest;
        public bool SurvivalRequest;

        public static InGameMenuStateHighscoresChoose Instance => m_instance;

        public static InGameMenuStateHighscoresChoose GetInstance() => m_instance;

        public InGameMenuStateHighscoresChoose()
        {
            this.BackRequest = false;
            this.StoryRequest = false;
            this.SurvivalRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.StoryRequest = false;
            this.SurvivalRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameHighscoresChooseView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (this.BackRequest)
            {
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
                stateMachine.SetState(InGameStateMenu.GetInstance());
            }
            else if (this.StoryRequest)
            {
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                stateMachine.SetState(InGameMenuStateHighscoresStory.GetInstance());
                GameApplication.GetInstance().GetInGameHighscoresView().StoryMode = true;
            }
            else
            {
                if (!this.SurvivalRequest)
                    return;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                stateMachine.SetState(InGameMenuStateHighscoresSurvival.GetInstance());
                GameApplication.GetInstance().GetInGameHighscoresView().StoryMode = false;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.StoryRequest = false;
            this.SurvivalRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameHighscoresChooseView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHighscoresChooseView().Draw();
    }
}
