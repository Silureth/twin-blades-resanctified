// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.MainMenu.MainMenuStateExit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.MainMenu
{
  public class MainMenuStateExit : IDrawableState, IState
  {
    private static MainMenuStateExit m_instance = new MainMenuStateExit();
    public bool PreviousRequest;
    public bool ExitRequest;

    public static MainMenuStateExit Instance => MainMenuStateExit.m_instance;

    public static MainMenuStateExit GetInstance() => MainMenuStateExit.m_instance;

    public MainMenuStateExit()
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetExitConfirmationView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.PreviousRequest = true;
      if (this.PreviousRequest)
      {
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        stateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
      }
      else
      {
        if (!this.ExitRequest)
          return;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
        GameStateMainMenu.Instance.ExitRequest = true;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetExitConfirmationView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetExitConfirmationView().Draw();
  }
}
