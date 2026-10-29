// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.FSM.FiniteStateMachine
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

namespace Resanctified.Engine.FSM
{
  public class FiniteStateMachine : IFiniteStateMachine
  {
    private IState m_currentState;
    private IState m_previousState;
    private IFiniteStateMachine m_parentStateMachine;
    private double m_currentStateTime;

    public bool IsActive { get; set; }

    public FiniteStateMachine()
      : this((IFiniteStateMachine) null)
    {
    }

    public FiniteStateMachine(IFiniteStateMachine parentStateMachine)
    {
      this.m_currentState = this.m_previousState = (IState) StateDefault.Instance;
      this.m_parentStateMachine = parentStateMachine;
      this.m_currentStateTime = 0.0;
      this.IsActive = true;
    }

    public void SetState(IState state)
    {
      this.m_previousState = this.m_currentState;
      this.m_currentState.Exit((IFiniteStateMachine) this);
      this.m_currentState = state;
      this.m_currentState.Enter((IFiniteStateMachine) this);
      this.m_currentStateTime = 0.0;
    }

    public bool IsInState(IState state) => this.m_currentState.Equals((object) state);

    public IState CurrentState => this.m_currentState;

    public IState GetCurrentState() => this.m_currentState;

    public IState PreviousState => this.m_previousState;

    public IState GetPreviousState() => this.m_previousState;

    public IFiniteStateMachine ParentStateMachine => this.m_parentStateMachine;

    public IFiniteStateMachine GetParentStateMachine() => this.m_parentStateMachine;

    public double CurrentStateTime => this.m_currentStateTime;

    public double GetCurrentStateTime() => this.m_currentStateTime;

    public virtual void Initialize() => this.m_currentState = this.m_previousState = (IState) StateDefault.Instance;

    public virtual void Update(float gameTime)
    {
      if (!this.IsActive)
        return;
      this.m_currentStateTime += (double) gameTime;
      this.m_currentState.Update((IFiniteStateMachine) this, gameTime);
    }

    public virtual void Cleanup()
    {
    }
  }
}
