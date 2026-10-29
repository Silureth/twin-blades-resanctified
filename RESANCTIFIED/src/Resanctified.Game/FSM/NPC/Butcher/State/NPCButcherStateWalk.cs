// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Butcher.State.NPCButcherStateWalk
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
  public class NPCButcherStateWalk : IState
  {
    private static NPCButcherStateWalk m_instance = new NPCButcherStateWalk();
    private bool m_soundPlayed;

    public static NPCButcherStateWalk Instance => NPCButcherStateWalk.m_instance;

    public static NPCButcherStateWalk GetInstance() => NPCButcherStateWalk.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCButcherFSM npcButcherFsm = (NPCButcherFSM) stateMachine;
      NPCButcher entity = (NPCButcher) npcButcherFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[1]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      if (entity.GetBallView().GetAnimation2D() != npcButcherFsm.AnimationArray[10])
      {
        entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[7]);
        entity.GetBallView().GetAnimation2D().SetTime(0.0f);
        entity.GetBallView().GetAnimation2D().SetSpeed(1f);
        entity.GetPhysics().Velocity.X = 1f * entity.GetTransform().GetScale().X;
      }
      else
        entity.GetPhysics().Velocity.X = 1.2f * entity.GetTransform().GetScale().X;
      this.m_soundPlayed = false;
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
        Vector3 position = entity.GetTransform().GetPosition() with
        {
          X = 0.5f
        };
        if ((double) entity.GetTransform().GetScale().X > 0.0)
          position.X = 6.5f;
        float num = position.X - entity.GetTransform().GetPosition().X;
        if ((double) (num * num) <= 0.5)
        {
          entity.GetPhysics().Velocity.X = 0.0f;
          Vector2 scale = entity.GetTransform().GetScale();
          entity.GetTransform().SetPosition(position);
          scale.X = -scale.X;
          entity.GetTransform().SetScale(scale);
          if (entity.HitPoints > 1500)
            entity.ThrowTimer = 0.0f;
          stateMachine.SetState((IState) NPCButcherStateStand.GetInstance());
        }
        else if ((double) entity.GetView().GetAnimation2D().GetTime() < 0.100000001490116 && !this.m_soundPlayed)
        {
          entity.GetSound(NPCButcher.SoundFxId.SoundFx_Slash).Play();
          this.m_soundPlayed = true;
        }
        else
          this.m_soundPlayed = false;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((NPCBoss) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetPhysics().Velocity.X = 0.0f;
  }
}
