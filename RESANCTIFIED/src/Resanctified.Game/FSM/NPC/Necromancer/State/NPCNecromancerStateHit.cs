// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Necromancer.State.NPCNecromancerStateHit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
  public class NPCNecromancerStateHit : IState
  {
    private static NPCNecromancerStateHit m_instance = new NPCNecromancerStateHit();

    public static NPCNecromancerStateHit Instance => NPCNecromancerStateHit.m_instance;

    public static NPCNecromancerStateHit GetInstance() => NPCNecromancerStateHit.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM) stateMachine;
      NPCNecromancer entity = (NPCNecromancer) npcNecromancerFsm.GetEntity();
      if (entity.HitPoints <= 0)
      {
        stateMachine.SetState((IState) NPCNecromancerStateDying.GetInstance());
      }
      else
      {
        entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[1]);
        entity.GetView().GetAnimation2D().SetTime(0.0f);
        entity.GetView().GetAnimation2D().SetSpeed(1f);
        entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Hit).Play();
      }
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      NPCNecromancer entity = (NPCNecromancer) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      if (entity.IsHit)
        stateMachine.SetState((IState) NPCNecromancerStateHit.GetInstance());
      else if ((double) entity.VulnerableTimer > 0.0)
        stateMachine.SetState((IState) NPCNecromancerStateVulnerable.GetInstance());
      else
        stateMachine.SetState((IState) NPCNecromancerStateStand.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((NPCBoss) ((EntityFiniteStateMachine) stateMachine).GetEntity()).IsHit = false;
  }
}
