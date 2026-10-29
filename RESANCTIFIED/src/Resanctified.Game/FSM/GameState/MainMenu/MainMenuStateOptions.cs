using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateOptions : IDrawableState, IState
    {
        private static MainMenuStateOptions m_instance = new MainMenuStateOptions();
        public bool BackRequest;
        public bool LanguageSelectionRequest;

        public static MainMenuStateOptions Instance => m_instance;

        public static MainMenuStateOptions GetInstance() => m_instance;

        public MainMenuStateOptions()
        {
            this.BackRequest = false;
            this.LanguageSelectionRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.LanguageSelectionRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetOptionsView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (this.BackRequest)
            {
                stateMachine.SetState(MainMenuStateHelpAndOptions.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
            else
            {
                if (!this.LanguageSelectionRequest)
                    return;
                stateMachine.SetState(MainMenuStateLanguageSelection.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.LanguageSelectionRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetOptionsView());
            GameApplication.Instance.SaveSettings();
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetOptionsView().Draw();
    }
}
