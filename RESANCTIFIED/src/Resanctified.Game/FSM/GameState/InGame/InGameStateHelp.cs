using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.World;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateHelp : IDrawableState, IState
    {
        private static InGameStateHelp m_instance = new InGameStateHelp();

        public static InGameStateHelp Instance => m_instance;

        public static InGameStateHelp GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            GameApplication.GetInstance().AddResponder(GameApplication.GetInstance().GetInGameHelpView());
            ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHud().HelpScreenEnabled = true;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
            {
                if (stateMachine.GetPreviousState() == InGameMenuStateHelpAndOptions.GetInstance())
                {
                    stateMachine.SetState(InGameMenuStateHelpAndOptions.GetInstance());
                    GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
                }
                else
                    stateMachine.SetState(InGameStatePlay.GetInstance());
            }
            else
            {
                GameApplication.GetInstance().GetInGameHelpView().Update(gameTime);
                if (GameApplication.GetInstance().GetInGameHelpView().GetPage() <= 1)
                    return;
                if (stateMachine.GetPreviousState() == InGameMenuStateHelpAndOptions.GetInstance())
                {
                    stateMachine.SetState(InGameMenuStateHelpAndOptions.GetInstance());
                    GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
                }
                else
                    stateMachine.SetState(InGameStatePlay.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHud().HelpScreenEnabled = false;
            GameApplication.GetInstance().RemoveResponder(GameApplication.GetInstance().GetInGameHelpView());
            GameApplication.GetInstance().GetInGameHelpView().ResetPageIndex();
        }

        public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpView().Draw();
    }
}
