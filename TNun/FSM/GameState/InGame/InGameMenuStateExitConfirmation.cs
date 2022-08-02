// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameMenuStateExitConfirmation
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameMenuStateExitConfirmation : IDrawableState, IState
  {
    private static InGameMenuStateExitConfirmation m_instance = new InGameMenuStateExitConfirmation();
    public bool PreviousRequest;
    public bool ExitRequest;

    public static InGameMenuStateExitConfirmation Instance => InGameMenuStateExitConfirmation.m_instance;

    public static InGameMenuStateExitConfirmation GetInstance() => InGameMenuStateExitConfirmation.m_instance;

    public InGameMenuStateExitConfirmation()
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameExitConfirmationView());
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
        GameApplication.Instance.ExitRequest = true;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameExitConfirmationView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameExitConfirmationView().Draw();
  }
}
