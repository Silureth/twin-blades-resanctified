using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateHelpAndOptions : IDrawableState, IState
    {
        public bool SettingsRequest;
        public bool HelpRequest;
        public bool KeyboardRequest;
        public bool CreditsRequest;
        public bool BackRequest;
        private static InGameMenuStateHelpAndOptions m_instance = new InGameMenuStateHelpAndOptions();

        public static InGameMenuStateHelpAndOptions Instance => m_instance;

        public static InGameMenuStateHelpAndOptions GetInstance() => m_instance;

        public InGameMenuStateHelpAndOptions()
        {
            this.SettingsRequest = false;
            this.HelpRequest = false;
            this.KeyboardRequest = false;
            this.BackRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.HelpRequest = false;
            this.KeyboardRequest = false;
            this.SettingsRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameHelpAndOptionsView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (this.BackRequest)
            {
                stateMachine.SetState(InGameStateMenu.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
            else if (this.HelpRequest)
            {
                if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
                    stateMachine.SetState(InGameStateHelp.GetInstance());
                else if (GameFSM.GetInstance().GetCurrentState() == GameStateMap.GetInstance())
                    stateMachine.SetState(InGameStateHelpMap.GetInstance());
                else if (GameFSM.GetInstance().GetCurrentState() == GameStateEndOfNight.GetInstance())
                    stateMachine.SetState(InGameStateHelpEON.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            else if (this.KeyboardRequest)
            {
                stateMachine.SetState(InGameMenuStateKeyboard.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            else
            {
                if (!this.SettingsRequest)
                    return;
                stateMachine.SetState(InGameMenuStateOptions.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.HelpRequest = false;
            this.KeyboardRequest = false;
            this.SettingsRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameHelpAndOptionsView());
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpAndOptionsView().Draw();
    }
}
