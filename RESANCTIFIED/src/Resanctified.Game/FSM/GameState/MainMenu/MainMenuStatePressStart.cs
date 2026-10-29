// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStatePressStart
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStatePressStart : IDrawableState, IState
  {
    private const float SPLASH_WAIT_TIME = 5f;
    public bool StartRequest;
    private static MainMenuStatePressStart m_instance = new MainMenuStatePressStart();

    public static MainMenuStatePressStart Instance => MainMenuStatePressStart.m_instance;

    public static MainMenuStatePressStart GetInstance() => MainMenuStatePressStart.m_instance;

    private MainMenuStatePressStart() => this.StartRequest = false;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.StartRequest = false;
      GameApplication.Instance.LoadSettings();
      GameApplication.Instance.SetSoundVolume();
      GameLocalisation.GetInstance().Initialize(GameApplication.Instance.CurrentLanguage);
      GameApplication.Instance.LoadHighscores();
      GameApplication.Instance.LoadAchievements();
     //AchievementManager.Instance.ReadAchievements();
     //AchievementManager.Instance.SynchronizeAchievementsRequest = true;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetSplashView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
      {
        GameStateMainMenu.Instance.ExitRequest = true;
      }
      else
      {
        GameStateMainMenu.GetInstance().GetSplashView().Update(gameTime);
        if (!this.StartRequest && stateMachine.GetCurrentStateTime() <= 5.0)
          return;
        stateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.StartRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetSplashView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetSplashView().Draw();
  }
}
