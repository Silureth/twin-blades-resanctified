// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameMenuStateBackToMainMenuConfirmation
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameMenuStateBackToMainMenuConfirmation : IDrawableState, IState
  {
    private static InGameMenuStateBackToMainMenuConfirmation m_instance = new InGameMenuStateBackToMainMenuConfirmation();
    public bool PreviousRequest;
    public bool ExitRequest;

    public static InGameMenuStateBackToMainMenuConfirmation Instance => InGameMenuStateBackToMainMenuConfirmation.m_instance;

    public static InGameMenuStateBackToMainMenuConfirmation GetInstance() => InGameMenuStateBackToMainMenuConfirmation.m_instance;

    public InGameMenuStateBackToMainMenuConfirmation()
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameBackToMainMenuConfirmationView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.PreviousRequest = true;
      if (this.PreviousRequest)
      {
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        stateMachine.SetState((IState) InGameStateMenu.GetInstance());
      }
      else
      {
        if (!this.ExitRequest)
          return;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
        if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
          GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
        InGameStateMenu.GetInstance().ScriptExecuting = false;
        stateMachine.SetState((IState) InGameStateExit.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameBackToMainMenuConfirmationView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameBackToMainMenuConfirmationView().Draw();
  }
}
