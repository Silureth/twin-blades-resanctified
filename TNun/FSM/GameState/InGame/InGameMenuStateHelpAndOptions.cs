// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameMenuStateHelpAndOptions
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameMenuStateHelpAndOptions : IDrawableState, IState
  {
    public bool SettingsRequest;
    public bool HelpRequest;
    public bool CreditsRequest;
    public bool BackRequest;
    private static InGameMenuStateHelpAndOptions m_instance = new InGameMenuStateHelpAndOptions();

    public static InGameMenuStateHelpAndOptions Instance => InGameMenuStateHelpAndOptions.m_instance;

    public static InGameMenuStateHelpAndOptions GetInstance() => InGameMenuStateHelpAndOptions.m_instance;

    public InGameMenuStateHelpAndOptions()
    {
      this.SettingsRequest = false;
      this.HelpRequest = false;
      this.BackRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.HelpRequest = false;
      this.SettingsRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHelpAndOptionsView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (this.BackRequest)
      {
        stateMachine.SetState((IState) InGameStateMenu.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else if (this.HelpRequest)
      {
        if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
          stateMachine.SetState((IState) InGameStateHelp.GetInstance());
        else if (GameFSM.GetInstance().GetCurrentState() == GameStateMap.GetInstance())
          stateMachine.SetState((IState) InGameStateHelpMap.GetInstance());
        else if (GameFSM.GetInstance().GetCurrentState() == GameStateEndOfNight.GetInstance())
          stateMachine.SetState((IState) InGameStateHelpEON.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      else
      {
        if (!this.SettingsRequest)
          return;
        stateMachine.SetState((IState) InGameMenuStateOptions.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.HelpRequest = false;
      this.SettingsRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHelpAndOptionsView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpAndOptionsView().Draw();
  }
}
