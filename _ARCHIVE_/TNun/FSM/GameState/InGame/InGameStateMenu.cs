// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateMenu
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateMenu : IDrawableState, IState
  {
    public bool UnlockRequest;
    public bool ResumeGameRequest;
    public bool AchievementsRequest;
    public bool HighscoresRequest;
    public bool OptionsRequest;
    public bool BackToMainMenuRequest;
    public bool BackToMapRequest;
    public bool ScriptExecuting;
    private static InGameStateMenu m_instance = new InGameStateMenu();

    public static InGameStateMenu Instance => InGameStateMenu.m_instance;

    public static InGameStateMenu GetInstance() => InGameStateMenu.m_instance;

    public InGameStateMenu()
    {
      this.UnlockRequest = false;
      this.ResumeGameRequest = false;
      this.AchievementsRequest = false;
      this.HighscoresRequest = false;
      this.OptionsRequest = false;
      this.BackToMainMenuRequest = false;
      this.BackToMapRequest = false;
      this.ScriptExecuting = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      InputManager.GetInstance().ClearPadState();
      this.UnlockRequest = false;
      this.ResumeGameRequest = false;
      this.AchievementsRequest = false;
      this.HighscoresRequest = false;
      this.OptionsRequest = false;
      this.BackToMainMenuRequest = false;
      this.BackToMapRequest = false;
      if (!GameStateInGame.GetInstance().IsMenuInGameActiveInPreviousState())
        this.ScriptExecuting = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameMenuView());
      if (stateMachine.GetPreviousState() != InGameStateScript.GetInstance())
        return;
      this.ScriptExecuting = true;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.ResumeGameRequest = true;
      GameApplication.GetInstance().GetInGameMenuView().Update(gameTime);
      if (this.ResumeGameRequest)
      {
        AudioManager.GetInstance().ResumeAll();
        if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
        {
          if (this.ScriptExecuting)
          {
            GameApplication.GetInstance().GetTextBoxViewController().SetActive(InGameStateScript.GetInstance().TextBoxIsActive);
            this.ScriptExecuting = false;
            stateMachine.SetState((IState) InGameStateScript.GetInstance());
          }
          else
            stateMachine.SetState((IState) InGameStatePlay.GetInstance());
        }
        else if (GameFSM.GetInstance().GetCurrentState() == GameStateMap.GetInstance())
        {
          stateMachine.SetState((IState) InGameStateMapNavigation.GetInstance());
          GameStateMap.GetInstance().ClearInput = true;
        }
        else
        {
          if (GameFSM.GetInstance().GetCurrentState() != GameStateEndOfNight.GetInstance())
            return;
          stateMachine.SetState((IState) InGameStateEONNavigation.GetInstance());
          GameStateEndOfNight.GetInstance().ClearInput = true;
        }
      }
      else if (this.AchievementsRequest)
        stateMachine.SetState((IState) InGameMenuStateAchievements.GetInstance());
      else if (this.HighscoresRequest)
        stateMachine.SetState((IState) InGameMenuStateHighscoresChoose.GetInstance());
      else if (this.OptionsRequest)
        stateMachine.SetState((IState) InGameMenuStateHelpAndOptions.GetInstance());
      else if (this.BackToMapRequest)
        stateMachine.SetState((IState) InGameMenuStateBackToMapConfirmation.GetInstance());
      else if (this.BackToMainMenuRequest)
      {
        stateMachine.SetState((IState) InGameMenuStateBackToMainMenuConfirmation.GetInstance());
      }
      else
      {
        if (!this.UnlockRequest)
          return;
        this.UnlockRequest = false;
        GuideManager.Instance.RequestShowMarketplace = true;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      InputManager.GetInstance().ClearPadState();
      this.UnlockRequest = false;
      this.ResumeGameRequest = false;
      this.AchievementsRequest = false;
      this.HighscoresRequest = false;
      this.OptionsRequest = false;
      this.BackToMainMenuRequest = false;
      this.BackToMapRequest = false;
      if (!GameStateInGame.GetInstance().IsMenuInGameActiveInPreviousState())
        this.ScriptExecuting = false;
      GameApplication.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameMenuView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameMenuView().Draw();
  }
}
