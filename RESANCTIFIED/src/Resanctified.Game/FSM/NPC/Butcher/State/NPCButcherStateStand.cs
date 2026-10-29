// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Butcher.State.NPCButcherStateStand
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using System;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
  public class NPCButcherStateStand : IState
  {
    private static NPCButcherStateStand m_instance = new NPCButcherStateStand();
    private bool m_transition;

    public static NPCButcherStateStand Instance => NPCButcherStateStand.m_instance;

    public static NPCButcherStateStand GetInstance() => NPCButcherStateStand.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCButcherFSM npcButcherFsm = (NPCButcherFSM) stateMachine;
      NPCButcher entity = (NPCButcher) npcButcherFsm.GetEntity();
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      Vector2 scale = entity.GetTransform().GetScale();
      float num = hero.GetTransform().GetPosition().X - entity.GetTransform().GetPosition().X;
      float x = scale.X;
      scale.X = (double) entity.GetTransform().GetPosition().X != 6.5 ? ((double) num >= 0.0 ? Math.Abs(scale.X) : -Math.Abs(scale.X)) : ((double) num <= 0.0 ? -Math.Abs(scale.X) : Math.Abs(scale.X));
      if ((double) scale.X != (double) x)
      {
        entity.GetTransform().SetScale(scale);
        if (entity.GetBallView().GetAnimation2D() != npcButcherFsm.AnimationArray[10])
        {
          this.m_transition = true;
          entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[13]);
          entity.GetBallView().GetAnimation2D().SetTime(0.0f);
          entity.GetBallView().GetAnimation2D().SetSpeed(1f);
        }
        else
          this.m_transition = false;
      }
      else
      {
        this.m_transition = false;
        if (entity.GetBallView().GetAnimation2D() != npcButcherFsm.AnimationArray[10])
        {
          entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[6]);
          entity.GetBallView().GetAnimation2D().SetTime(0.0f);
          entity.GetBallView().GetAnimation2D().SetSpeed(1f);
        }
      }
      entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[0]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      NPCButcherFSM npcButcherFsm = (NPCButcherFSM) stateMachine;
      NPCButcher entity = (NPCButcher) npcButcherFsm.GetEntity();
      if (this.m_transition)
      {
        if (!entity.GetBallView().GetAnimation2D().IsEndOfAnimationReached())
          return;
        this.m_transition = false;
        entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[6]);
        entity.GetBallView().GetAnimation2D().SetTime(0.0f);
        entity.GetBallView().GetAnimation2D().SetSpeed(1f);
      }
      else
      {
        PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
        entity.GetTransform().GetScale();
        float num = hero.GetTransform().GetPosition().X - entity.GetTransform().GetPosition().X;
        if ((double) (num * num) <= 1.44000005722046)
          stateMachine.SetState((IState) NPCButcherStateSlash.GetInstance());
        else if (!entity.IsBallBrocken && (double) entity.ThrowTimer >= 4.0)
        {
          entity.ThrowTimer = 0.0f;
          stateMachine.SetState((IState) NPCButcherStateThrow.GetInstance());
        }
        else
        {
          if (!entity.IsAngry)
            return;
          stateMachine.SetState((IState) NPCButcherStateAngry.GetInstance());
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
