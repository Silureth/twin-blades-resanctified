// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameMenuStateLanguageSelection
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameMenuStateLanguageSelection : IDrawableState, IState
  {
    private static InGameMenuStateLanguageSelection m_instance = new InGameMenuStateLanguageSelection();
    public bool BackRequest;

    public static InGameMenuStateLanguageSelection Instance => InGameMenuStateLanguageSelection.m_instance;

    public static InGameMenuStateLanguageSelection GetInstance() => InGameMenuStateLanguageSelection.m_instance;

    public InGameMenuStateLanguageSelection() => this.BackRequest = false;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameLanguageSelectionView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (!this.BackRequest)
        return;
      stateMachine.SetState((IState) InGameMenuStateOptions.GetInstance());
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameLanguageSelectionView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameLanguageSelectionView().Draw();
  }
}
