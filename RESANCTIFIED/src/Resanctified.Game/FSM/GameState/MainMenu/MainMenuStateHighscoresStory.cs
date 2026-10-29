// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateHighscoresStory
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateHighscoresStory : IDrawableState, IState
  {
    private static MainMenuStateHighscoresStory m_instance = new MainMenuStateHighscoresStory();
    public bool BackRequest;
    public bool LeaderboardRequest;
    public bool HighscoresRequest;
    public bool WeekLeaderboardRequest;
    private bool m_leaderboardMode;

    public static MainMenuStateHighscoresStory Instance => MainMenuStateHighscoresStory.m_instance;

    public static MainMenuStateHighscoresStory GetInstance() => MainMenuStateHighscoresStory.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      GameStateMainMenu.GetInstance().GetLeaderboardView().ReadLeaderboard(0, false);
      this.BackRequest = false;
      this.LeaderboardRequest = false;
      this.WeekLeaderboardRequest = false;
      this.HighscoresRequest = false;
      this.m_leaderboardMode = GuideManager.Instance.IsSignedInToLive();
      if (this.m_leaderboardMode)
        Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetLeaderboardView());
      else
        Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      GameStateMainMenu.GetInstance().GetHighscoresView().Update(gameTime);
      GameStateMainMenu.GetInstance().GetLeaderboardView().Update(gameTime);
      if (this.BackRequest)
      {
        if (GameStateMainMenu.GetInstance().GetHighscoresView().GetBlinkIndex() > 0)
        {
          stateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
        }
        else
        {
          stateMachine.SetState((IState) MainMenuStateHighscoresChoose.GetInstance());
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }
      }
      else if (this.HighscoresRequest)
      {
        if (this.m_leaderboardMode)
        {
          Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresView());
          Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetLeaderboardView());
        }
        this.m_leaderboardMode = false;
        this.HighscoresRequest = false;
      }
      else
      {
        if (!this.LeaderboardRequest)
          return;
        if (!this.m_leaderboardMode)
        {
          Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetLeaderboardView());
          Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresView());
        }
        this.m_leaderboardMode = true;
        this.LeaderboardRequest = false;
        if (this.WeekLeaderboardRequest)
        {
          GameStateMainMenu.GetInstance().GetLeaderboardView().ReadLeaderboard(0, true);
          this.WeekLeaderboardRequest = false;
        }
        else
          GameStateMainMenu.GetInstance().GetLeaderboardView().ReadLeaderboard(0, false);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.LeaderboardRequest = false;
      this.WeekLeaderboardRequest = false;
      this.HighscoresRequest = false;
      this.m_leaderboardMode = false;
      GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(0);
      GameStateMainMenu.GetInstance().GetLeaderboardView().BlinkScore(false);
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresView());
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetLeaderboardView());
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      if (this.m_leaderboardMode)
        GameStateMainMenu.GetInstance().GetLeaderboardView().Draw();
      else
        GameStateMainMenu.GetInstance().GetHighscoresView().Draw();
    }
  }
}
