// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateHelp
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateHelp : IDrawableState, IState
  {
    private static InGameStateHelp m_instance = new InGameStateHelp();

    public static InGameStateHelp Instance => InGameStateHelp.m_instance;

    public static InGameStateHelp GetInstance() => InGameStateHelp.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      GameApplication.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHelpView());
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHud().HelpScreenEnabled = true;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
      {
        if (stateMachine.GetPreviousState() == InGameMenuStateHelpAndOptions.GetInstance())
        {
          stateMachine.SetState((IState) InGameMenuStateHelpAndOptions.GetInstance());
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }
        else
          stateMachine.SetState((IState) InGameStatePlay.GetInstance());
      }
      else
      {
        GameApplication.GetInstance().GetInGameHelpView().Update(gameTime);
        if (GameApplication.GetInstance().GetInGameHelpView().GetPage() <= 1)
          return;
        if (stateMachine.GetPreviousState() == InGameMenuStateHelpAndOptions.GetInstance())
        {
          stateMachine.SetState((IState) InGameMenuStateHelpAndOptions.GetInstance());
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }
        else
          stateMachine.SetState((IState) InGameStatePlay.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHud().HelpScreenEnabled = false;
      GameApplication.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHelpView());
      GameApplication.GetInstance().GetInGameHelpView().ResetPageIndex();
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpView().Draw();
  }
}
