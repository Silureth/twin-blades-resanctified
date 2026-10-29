// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Butcher.State.NPCButcherStateDead
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
  public class NPCButcherStateDead : IState
  {
    private static NPCButcherStateDead m_instance = new NPCButcherStateDead();

    public static NPCButcherStateDead Instance => NPCButcherStateDead.m_instance;

    public static NPCButcherStateDead GetInstance() => NPCButcherStateDead.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
