// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.FSM.EntityFiniteStateMachine
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.World.Entities;

namespace PSS.GameEngine.FSM
{
  public class EntityFiniteStateMachine : FiniteStateMachine
  {
    private IEntity m_entity;

    public EntityFiniteStateMachine() => this.m_entity = (IEntity) null;

    public EntityFiniteStateMachine(IEntity entity) => this.m_entity = entity;

    public IEntity Entity => this.m_entity;

    public IEntity GetEntity() => this.m_entity;
  }
}
