// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateSplashAnim
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateSplashAnim : IDrawableState, IState
  {
    private static MainMenuStateSplashAnim m_instance = new MainMenuStateSplashAnim();

    public static MainMenuStateSplashAnim Instance => MainMenuStateSplashAnim.m_instance;

    public static MainMenuStateSplashAnim GetInstance() => MainMenuStateSplashAnim.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime) => stateMachine.SetState((IState) MainMenuStatePressStart.GetInstance());

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
    }
  }
}
