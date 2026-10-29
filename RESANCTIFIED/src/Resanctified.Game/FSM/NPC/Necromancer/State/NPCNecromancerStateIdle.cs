// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Necromancer.State.NPCNecromancerStateIdle
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
  public class NPCNecromancerStateIdle : IState
  {
    private static NPCNecromancerStateIdle m_instance = new NPCNecromancerStateIdle();

    public static NPCNecromancerStateIdle Instance => NPCNecromancerStateIdle.m_instance;

    public static NPCNecromancerStateIdle GetInstance() => NPCNecromancerStateIdle.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM) stateMachine;
      NPCNecromancer entity = (NPCNecromancer) npcNecromancerFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[0]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
