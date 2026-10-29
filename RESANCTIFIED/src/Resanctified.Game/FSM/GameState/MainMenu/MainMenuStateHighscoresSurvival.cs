// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateHighscoresSurvival
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  // Shows the local highscores only; the Xbox Live leaderboard pages of the phone version are gone.
  public class MainMenuStateHighscoresSurvival : IDrawableState, IState
  {
    private static MainMenuStateHighscoresSurvival m_instance = new MainMenuStateHighscoresSurvival();
    public bool BackRequest;

    public static MainMenuStateHighscoresSurvival Instance => MainMenuStateHighscoresSurvival.m_instance;

    public static MainMenuStateHighscoresSurvival GetInstance() => MainMenuStateHighscoresSurvival.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      GameStateMainMenu.GetInstance().GetHighscoresView().Update(gameTime);
      if (!this.BackRequest)
        return;
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

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(0);
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHighscoresView().Draw();
  }
}
