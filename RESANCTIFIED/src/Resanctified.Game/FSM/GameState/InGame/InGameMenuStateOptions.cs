using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameMenuStateOptions : IDrawableState, IState
    {
        private static InGameMenuStateOptions m_instance = new InGameMenuStateOptions();
        public bool BackRequest;
        public bool LanguageSelectionRequest;

        public static InGameMenuStateOptions Instance => m_instance;

        public static InGameMenuStateOptions GetInstance() => m_instance;

        public InGameMenuStateOptions()
        {
            this.BackRequest = false;
            this.LanguageSelectionRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.LanguageSelectionRequest = false;
            Application.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameOptionsView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
                this.BackRequest = true;
            if (this.BackRequest)
            {
                stateMachine.SetState(InGameMenuStateHelpAndOptions.GetInstance());
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
            }
            else
            {
                if (!this.LanguageSelectionRequest)
                    return;
                stateMachine.SetState(InGameMenuStateLanguageSelection.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.BackRequest = false;
            this.LanguageSelectionRequest = false;
            Application.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameOptionsView());
            GameApplication.Instance.SaveSettings();
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameOptionsView().Draw();
    }
}
