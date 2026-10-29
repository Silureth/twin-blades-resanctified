// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.MainMenu.MainMenuStateMenu
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.MainMenu
{
  public class MainMenuStateMenu : IDrawableState, IState
  {
    private static MainMenuStateMenu m_instance = new MainMenuStateMenu();
    public bool StartGameRequest;
    public bool OptionsRequest;
    public bool HighscoresRequest;
    public bool AchievementsRequest;
    public bool UnlockRequest;
    public bool ExitRequest;

    public static MainMenuStateMenu Instance => MainMenuStateMenu.m_instance;

    public static MainMenuStateMenu GetInstance() => MainMenuStateMenu.m_instance;

    public MainMenuStateMenu()
    {
      this.StartGameRequest = false;
      this.OptionsRequest = false;
      this.HighscoresRequest = false;
      this.AchievementsRequest = false;
      this.UnlockRequest = false;
      this.ExitRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.StartGameRequest = false;
      this.OptionsRequest = false;
      this.HighscoresRequest = false;
      this.AchievementsRequest = false;
      this.UnlockRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetMenuView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        GameStateMainMenu.Instance.ExitRequest = true;
      else if (this.StartGameRequest)
        stateMachine.SetState((IState) MainMenuStateGameMode.GetInstance());
      else if (this.AchievementsRequest)
        stateMachine.SetState((IState) MainMenuStateAchievements.GetInstance());
      else if (this.HighscoresRequest)
        stateMachine.SetState((IState) MainMenuStateHighscoresChoose.GetInstance());
      else if (this.OptionsRequest)
        stateMachine.SetState((IState) MainMenuStateHelpAndOptions.GetInstance());
      else if (this.UnlockRequest)
      {
        this.UnlockRequest = false;
        GuideManager.Instance.RequestShowMarketplace = true;
      }
      else
      {
        if (!this.ExitRequest)
          return;
        GameStateMainMenu.Instance.ExitRequest = true;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.StartGameRequest = false;
      this.OptionsRequest = false;
      this.HighscoresRequest = false;
      this.AchievementsRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetMenuView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetMenuView().Draw();
  }
}
