// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Butcher.State.NPCButcherStateIdle
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.World;
using System;

namespace PSS.Game.NunVsZombies.FSM.NPC.Butcher.State
{
  public class NPCButcherStateIdle : IState
  {
    private static NPCButcherStateIdle m_instance = new NPCButcherStateIdle();
    private bool m_transition;

    public static NPCButcherStateIdle Instance => NPCButcherStateIdle.m_instance;

    public static NPCButcherStateIdle GetInstance() => NPCButcherStateIdle.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCButcherFSM npcButcherFsm = (NPCButcherFSM) stateMachine;
      NPCButcher entity = (NPCButcher) npcButcherFsm.GetEntity();
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      Vector2 scale = entity.GetTransform().GetScale();
      float num = hero.GetTransform().GetPosition().X - entity.GetTransform().GetPosition().X;
      scale.X = (double) entity.GetTransform().GetPosition().X != 6.5 ? ((double) num >= 0.0 ? Math.Abs(scale.X) : -Math.Abs(scale.X)) : ((double) num <= 0.0 ? -Math.Abs(scale.X) : Math.Abs(scale.X));
      entity.GetTransform().SetScale(scale);
      entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[6]);
      entity.GetBallView().GetAnimation2D().SetTime(0.0f);
      entity.GetBallView().GetAnimation2D().SetSpeed(1f);
      entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[0]);
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
