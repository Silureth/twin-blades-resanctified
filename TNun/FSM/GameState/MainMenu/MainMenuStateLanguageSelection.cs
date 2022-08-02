// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.MainMenu.MainMenuStateLanguageSelection
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.MainMenu
{
  public class MainMenuStateLanguageSelection : IDrawableState, IState
  {
    private static MainMenuStateLanguageSelection m_instance = new MainMenuStateLanguageSelection();
    public bool BackRequest;

    public static MainMenuStateLanguageSelection Instance => MainMenuStateLanguageSelection.m_instance;

    public static MainMenuStateLanguageSelection GetInstance() => MainMenuStateLanguageSelection.m_instance;

    public MainMenuStateLanguageSelection() => this.BackRequest = false;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetLanguageSelectionView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (!this.BackRequest)
        return;
      stateMachine.SetState((IState) MainMenuStateOptions.GetInstance());
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetLanguageSelectionView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetLanguageSelectionView().Draw();
  }
}
