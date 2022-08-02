// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.FSM.DrawableFiniteStateMachine
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

namespace PSS.GameEngine.FSM
{
  public class DrawableFiniteStateMachine : 
    FiniteStateMachine,
    IDrawableFiniteStateMachine,
    IFiniteStateMachine
  {
    public virtual void Draw() => ((IDrawableState) this.CurrentState).Draw((IFiniteStateMachine) this);
  }
}
