// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.GameStateExit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.FSM;

namespace PSS.Game.NunVsZombies.FSM.GameState
{
  public class GameStateExit : IDrawableState, IState
  {
    private static GameStateExit m_instance = new GameStateExit();

    public static GameStateExit Instance => GameStateExit.m_instance;

    public static GameStateExit GetInstance() => GameStateExit.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime) => GameApplication.Instance.Exit();

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
    }
  }
}
