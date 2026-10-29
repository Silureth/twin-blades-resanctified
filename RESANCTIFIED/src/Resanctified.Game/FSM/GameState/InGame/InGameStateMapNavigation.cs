// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameStateMapNavigation
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Sounds;

namespace Resanctified.Game.FSM.GameState.InGame
{
  public class InGameStateMapNavigation : IDrawableState, IState
  {
    public bool InGameMenuRequest;
    private static InGameStateMapNavigation m_instance = new InGameStateMapNavigation();

    public static InGameStateMapNavigation Instance => InGameStateMapNavigation.m_instance;

    public static InGameStateMapNavigation GetInstance() => InGameStateMapNavigation.m_instance;

    public void Enter(IFiniteStateMachine stateMachine) => this.InGameMenuRequest = false;

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
        return;
      if (InputManager.GetInstance().StdPause.IsActive())
        this.InGameMenuRequest = true;
      if (InputManager.GetInstance().IsBackPressed())
        this.InGameMenuRequest = true;
      if (!this.InGameMenuRequest)
        return;
      this.InGameMenuRequest = false;
      AudioManager.GetInstance().PauseAll();
      stateMachine.SetState((IState) InGameStateMenu.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
    }
  }
}
