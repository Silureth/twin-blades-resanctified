// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.FSM.DrawableStateDefault
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

namespace Resanctified.Engine.FSM
{
  public class DrawableStateDefault : IDrawableState, IState
  {
    private static DrawableStateDefault m_instance = new DrawableStateDefault();

    public static DrawableStateDefault Instance => DrawableStateDefault.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
    }
  }
}
