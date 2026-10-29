// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Necromancer.State.NPCNecromancerStateFire
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
  public class NPCNecromancerStateFire : IState
  {
    private static NPCNecromancerStateFire m_instance = new NPCNecromancerStateFire();

    public static NPCNecromancerStateFire Instance => NPCNecromancerStateFire.m_instance;

    public static NPCNecromancerStateFire GetInstance() => NPCNecromancerStateFire.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM) stateMachine;
      NPCNecromancer entity = (NPCNecromancer) npcNecromancerFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[3]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Blast).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (!((NPCNecromancer) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      stateMachine.SetState((IState) NPCNecromancerStateStand.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((NPCNecromancer) ((EntityFiniteStateMachine) stateMachine).GetEntity()).SpellTimer = 0.0f;
  }
}
