// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameMenuStateOptions
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.InGame
{
  public class InGameMenuStateOptions : IDrawableState, IState
  {
    private static InGameMenuStateOptions m_instance = new InGameMenuStateOptions();
    public bool BackRequest;
    public bool LanguageSelectionRequest;

    public static InGameMenuStateOptions Instance => InGameMenuStateOptions.m_instance;

    public static InGameMenuStateOptions GetInstance() => InGameMenuStateOptions.m_instance;

    public InGameMenuStateOptions()
    {
      this.BackRequest = false;
      this.LanguageSelectionRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.LanguageSelectionRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameOptionsView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (this.BackRequest)
      {
        stateMachine.SetState((IState) InGameMenuStateHelpAndOptions.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else
      {
        if (!this.LanguageSelectionRequest)
          return;
        stateMachine.SetState((IState) InGameMenuStateLanguageSelection.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.LanguageSelectionRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameOptionsView());
      GameApplication.Instance.SaveSettings();
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameOptionsView().Draw();
  }
}
