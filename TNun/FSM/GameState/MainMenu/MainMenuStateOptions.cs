// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.MainMenu.MainMenuStateOptions
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.MainMenu
{
  public class MainMenuStateOptions : IDrawableState, IState
  {
    private static MainMenuStateOptions m_instance = new MainMenuStateOptions();
    public bool BackRequest;
    public bool LanguageSelectionRequest;

    public static MainMenuStateOptions Instance => MainMenuStateOptions.m_instance;

    public static MainMenuStateOptions GetInstance() => MainMenuStateOptions.m_instance;

    public MainMenuStateOptions()
    {
      this.BackRequest = false;
      this.LanguageSelectionRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.LanguageSelectionRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetOptionsView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (this.BackRequest)
      {
        stateMachine.SetState((IState) MainMenuStateHelpAndOptions.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else
      {
        if (!this.LanguageSelectionRequest)
          return;
        stateMachine.SetState((IState) MainMenuStateLanguageSelection.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.LanguageSelectionRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetOptionsView());
      GameApplication.Instance.SaveSettings();
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetOptionsView().Draw();
  }
}
