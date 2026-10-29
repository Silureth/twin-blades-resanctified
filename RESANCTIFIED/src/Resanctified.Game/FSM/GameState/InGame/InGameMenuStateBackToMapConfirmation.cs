// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameMenuStateBackToMapConfirmation
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.InGame
{
  public class InGameMenuStateBackToMapConfirmation : IDrawableState, IState
  {
    private static InGameMenuStateBackToMapConfirmation m_instance = new InGameMenuStateBackToMapConfirmation();
    public bool PreviousRequest;
    public bool ExitRequest;

    public static InGameMenuStateBackToMapConfirmation Instance => InGameMenuStateBackToMapConfirmation.m_instance;

    public static InGameMenuStateBackToMapConfirmation GetInstance() => InGameMenuStateBackToMapConfirmation.m_instance;

    public InGameMenuStateBackToMapConfirmation()
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameBackToMapConfirmationView());
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
        stateMachine.SetState((IState) InGameStateExitToMap.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.PreviousRequest = false;
      this.ExitRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameBackToMapConfirmationView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameBackToMapConfirmationView().Draw();
  }
}
