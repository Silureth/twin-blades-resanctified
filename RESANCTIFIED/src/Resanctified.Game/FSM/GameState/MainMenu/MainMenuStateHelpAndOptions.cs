using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
    public class MainMenuStateHelpAndOptions : IDrawableState, IState
    {
        private static MainMenuStateHelpAndOptions m_instance = new MainMenuStateHelpAndOptions();
        public bool BackRequest;
        public bool HelpRequest;
        public bool SettingsRequest;
        public bool CreditsRequest;
        public bool AboutRequest;

        public static MainMenuStateHelpAndOptions Instance => m_instance;

        public static MainMenuStateHelpAndOptions GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.HelpRequest = false;
            this.SettingsRequest = false;
            this.CreditsRequest = false;
            this.AboutRequest = false;
            Application.GetInstance().AddResponder(GameStateMainMenu.GetInstance().GetHelpAndOptionsView());
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
            else if (this.HelpRequest)
            {
                stateMachine.SetState(MainMenuStateHelp.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            else if (this.SettingsRequest)
            {
                stateMachine.SetState(MainMenuStateOptions.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            else if (this.CreditsRequest)
            {
                stateMachine.SetState(MainMenuStateCredits.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            else
            {
                if (!this.AboutRequest)
                    return;
                stateMachine.SetState(MainMenuStateAbout.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.HelpRequest = false;
            this.SettingsRequest = false;
            this.CreditsRequest = false;
            this.AboutRequest = false;
            Application.GetInstance().RemoveResponder(GameStateMainMenu.GetInstance().GetHelpAndOptionsView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHelpAndOptionsView().Draw();
    }
}
