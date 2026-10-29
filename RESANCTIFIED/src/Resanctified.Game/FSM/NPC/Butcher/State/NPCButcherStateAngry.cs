// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Butcher.State.NPCButcherStateAngry
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
  public class NPCButcherStateAngry : IState
  {
    private static NPCButcherStateAngry m_instance = new NPCButcherStateAngry();

    public static NPCButcherStateAngry Instance => NPCButcherStateAngry.m_instance;

    public static NPCButcherStateAngry GetInstance() => NPCButcherStateAngry.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCButcherFSM npcButcherFsm = (NPCButcherFSM) stateMachine;
      NPCButcher entity = (NPCButcher) npcButcherFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[4]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      if (entity.GetBallView().GetAnimation2D() != npcButcherFsm.AnimationArray[10])
      {
        entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[11]);
        entity.GetBallView().GetAnimation2D().SetTime(0.0f);
        entity.GetBallView().GetAnimation2D().SetSpeed(1f);
      }
      entity.GetSound(NPCButcher.SoundFxId.SoundFx_Angry).Play();
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
        entity.IsAngry = entity.HitPoints <= 1500;
        stateMachine.SetState((IState) NPCButcherStateWalk.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
