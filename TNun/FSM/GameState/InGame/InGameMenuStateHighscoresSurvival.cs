// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameMenuStateHighscoresSurvival
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameMenuStateHighscoresSurvival : IDrawableState, IState
  {
    private static InGameMenuStateHighscoresSurvival m_instance = new InGameMenuStateHighscoresSurvival();
    public bool BackRequest;
    public bool LeaderboardRequest;
    public bool HighscoresRequest;
    public bool WeekLeaderboardRequest;
    private bool m_leaderboardMode;

    public static InGameMenuStateHighscoresSurvival Instance => InGameMenuStateHighscoresSurvival.m_instance;

    public static InGameMenuStateHighscoresSurvival GetInstance() => InGameMenuStateHighscoresSurvival.m_instance;

    public InGameMenuStateHighscoresSurvival()
    {
      this.BackRequest = false;
      this.LeaderboardRequest = false;
      this.WeekLeaderboardRequest = false;
      this.HighscoresRequest = false;
      this.m_leaderboardMode = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      GameApplication.GetInstance().GetInGameLeaderboardView().ReadLeaderboard(1, false);
      this.BackRequest = false;
      this.LeaderboardRequest = false;
      this.WeekLeaderboardRequest = false;
      this.HighscoresRequest = false;
      this.m_leaderboardMode = GuideManager.Instance.IsSignedInToLive();
      if (this.m_leaderboardMode)
        Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameLeaderboardView());
      else
        Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      GameApplication.GetInstance().GetInGameHighscoresView().Update(gameTime);
      GameApplication.GetInstance().GetInGameLeaderboardView().Update(gameTime);
      if (this.BackRequest)
      {
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        stateMachine.SetState((IState) InGameMenuStateHighscoresChoose.GetInstance());
      }
      else if (this.HighscoresRequest)
      {
        if (this.m_leaderboardMode)
        {
          Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresView());
          Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameLeaderboardView());
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
          Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameLeaderboardView());
          Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresView());
        }
        this.m_leaderboardMode = true;
        this.LeaderboardRequest = false;
        if (this.WeekLeaderboardRequest)
        {
          GameApplication.GetInstance().GetInGameLeaderboardView().ReadLeaderboard(1, true);
          this.WeekLeaderboardRequest = false;
        }
        else
          GameApplication.GetInstance().GetInGameLeaderboardView().ReadLeaderboard(1, false);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.LeaderboardRequest = false;
      this.WeekLeaderboardRequest = false;
      this.HighscoresRequest = false;
      this.m_leaderboardMode = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresView());
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameLeaderboardView());
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      if (this.m_leaderboardMode)
        GameApplication.GetInstance().GetInGameLeaderboardView().Draw();
      else
        GameApplication.GetInstance().GetInGameHighscoresView().Draw();
    }
  }
}
