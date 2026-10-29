// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateHelp
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateHelp : IDrawableState, IState
  {
    private static MainMenuStateHelp m_instance = new MainMenuStateHelp();

    public static MainMenuStateHelp Instance => MainMenuStateHelp.m_instance;

    public static MainMenuStateHelp GetInstance() => MainMenuStateHelp.m_instance;

    public void Enter(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetHelpView());

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
      {
        stateMachine.SetState((IState) MainMenuStateHelpAndOptions.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else
      {
        GameApplication.GetInstance().GetInGameHelpView().Update(gameTime);
        if (GameStateMainMenu.GetInstance().GetHelpView().GetPage() <= 1)
          return;
        stateMachine.SetState((IState) MainMenuStateHelpAndOptions.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      GameApplication.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetHelpView());
      GameStateMainMenu.GetInstance().GetHelpView().ResetPageIndex();
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHelpView().Draw();
  }
}
