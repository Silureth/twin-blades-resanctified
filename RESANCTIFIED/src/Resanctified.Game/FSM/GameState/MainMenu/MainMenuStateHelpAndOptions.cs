// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateHelpAndOptions
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateHelpAndOptions : IDrawableState, IState
  {
    private static MainMenuStateHelpAndOptions m_instance = new MainMenuStateHelpAndOptions();
    public bool BackRequest;
    public bool HelpRequest;
    public bool SettingsRequest;
    public bool CreditsRequest;
    public bool AboutRequest;

    public static MainMenuStateHelpAndOptions Instance => MainMenuStateHelpAndOptions.m_instance;

    public static MainMenuStateHelpAndOptions GetInstance() => MainMenuStateHelpAndOptions.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.HelpRequest = false;
      this.SettingsRequest = false;
      this.CreditsRequest = false;
      this.AboutRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetHelpAndOptionsView());
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
      else if (this.HelpRequest)
      {
        stateMachine.SetState((IState) MainMenuStateHelp.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      else if (this.SettingsRequest)
      {
        stateMachine.SetState((IState) MainMenuStateOptions.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      else if (this.CreditsRequest)
      {
        stateMachine.SetState((IState) MainMenuStateCredits.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      else
      {
        if (!this.AboutRequest)
          return;
        stateMachine.SetState((IState) MainMenuStateAbout.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.HelpRequest = false;
      this.SettingsRequest = false;
      this.CreditsRequest = false;
      this.AboutRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetHelpAndOptionsView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHelpAndOptionsView().Draw();
  }
}
