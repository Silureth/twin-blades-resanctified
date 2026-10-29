// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateOverwrite
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateOverwrite : IDrawableState, IState
  {
    private static MainMenuStateOverwrite m_instance = new MainMenuStateOverwrite();
    public bool OverwriteOkRequest;
    public bool OverwriteCancelRequest;

    public static MainMenuStateOverwrite Instance => MainMenuStateOverwrite.m_instance;

    public static MainMenuStateOverwrite GetInstance() => MainMenuStateOverwrite.m_instance;

    public MainMenuStateOverwrite()
    {
      this.OverwriteOkRequest = false;
      this.OverwriteCancelRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.OverwriteOkRequest = false;
      this.OverwriteCancelRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetOverwriteView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.OverwriteCancelRequest = true;
      if (this.OverwriteCancelRequest)
      {
        stateMachine.SetState((IState) MainMenuStateGameMode.GetInstance());
      }
      else
      {
        if (!this.OverwriteOkRequest)
          return;
        Prefs.GetInstance().EraseGameDataFile();
        stateMachine.SetState((IState) MainMenuStateDifficulty.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.OverwriteOkRequest = false;
      this.OverwriteCancelRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetOverwriteView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetOverwriteView().Draw();
  }
}
