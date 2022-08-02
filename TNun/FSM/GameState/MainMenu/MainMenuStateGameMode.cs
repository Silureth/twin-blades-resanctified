// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.MainMenu.MainMenuStateGameMode
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.MainMenu
{
  public class MainMenuStateGameMode : IDrawableState, IState
  {
    private static MainMenuStateGameMode m_instance = new MainMenuStateGameMode();
    private bool m_savedGameAvailable;
    public bool BackRequest;
    public bool ContinueRequest;
    public bool DifficultyRequest;

    public static MainMenuStateGameMode Instance => MainMenuStateGameMode.m_instance;

    public static MainMenuStateGameMode GetInstance() => MainMenuStateGameMode.m_instance;

    public bool IsSavedGameAvailable() => this.m_savedGameAvailable;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.m_savedGameAvailable = Prefs.Instance.ReadGameData();
      if (stateMachine.GetPreviousState() == MainMenuStateMenu.GetInstance() && this.m_savedGameAvailable)
        GameStateMainMenu.GetInstance().GetGameModeView().SavedDifficulty = (GameApplication.GameDifficulty) Prefs.Instance.CachedGameData.m_difficulty;
      this.BackRequest = false;
      this.ContinueRequest = false;
      this.DifficultyRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetGameModeView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (this.BackRequest)
      {
        stateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else if (this.ContinueRequest)
      {
        stateMachine.SetState((IState) MainMenuStateContinueGame.GetInstance());
      }
      else
      {
        if (!this.DifficultyRequest)
          return;
        if (GuideManager.Instance.IsTrialMode())
        {
          Prefs.Instance.EraseAchievementsFile();
          GameApplication.Instance.LoadAchievements();
          Prefs.GetInstance().EraseGameDataFile();
          stateMachine.SetState((IState) MainMenuStateDifficulty.GetInstance());
        }
        else if (this.m_savedGameAvailable)
          stateMachine.SetState((IState) MainMenuStateOverwrite.GetInstance());
        else
          stateMachine.SetState((IState) MainMenuStateDifficulty.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine) => Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetGameModeView());

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetGameModeView().Draw();
  }
}
