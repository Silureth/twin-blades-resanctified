// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Butcher.State.NPCButcherStateSlash
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
  public class NPCButcherStateSlash : IState
  {
    private static NPCButcherStateSlash m_instance = new NPCButcherStateSlash();

    public static NPCButcherStateSlash Instance => NPCButcherStateSlash.m_instance;

    public static NPCButcherStateSlash GetInstance() => NPCButcherStateSlash.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCButcherFSM npcButcherFsm = (NPCButcherFSM) stateMachine;
      NPCButcher entity = (NPCButcher) npcButcherFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[2]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      if (entity.GetBallView().GetAnimation2D() != npcButcherFsm.AnimationArray[10])
      {
        entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[8]);
        entity.GetBallView().GetAnimation2D().SetTime(0.0f);
        entity.GetBallView().GetAnimation2D().SetSpeed(1f);
      }
      entity.GetSound(NPCButcher.SoundFxId.SoundFx_Slash).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      NPCButcher entity = (NPCButcher) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.HitPoints <= 0)
      {
        stateMachine.SetState((IState) NPCButcherStateDying.GetInstance());
      }
      else
      {
        if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
          return;
        stateMachine.SetState((IState) NPCButcherStateStand.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
