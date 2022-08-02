// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Butcher.State.NPCButcherStateDying
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Butcher.State
{
  public class NPCButcherStateDying : IState
  {
    private static NPCButcherStateDying m_instance = new NPCButcherStateDying();
    private bool m_shakeDone;

    public static NPCButcherStateDying Instance => NPCButcherStateDying.m_instance;

    public static NPCButcherStateDying GetInstance() => NPCButcherStateDying.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCButcherFSM npcButcherFsm = (NPCButcherFSM) stateMachine;
      NPCButcher entity = (NPCButcher) npcButcherFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[5]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      if (entity.GetBallView().GetAnimation2D() != npcButcherFsm.AnimationArray[10])
      {
        entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[12]);
        entity.GetBallView().GetAnimation2D().SetTime(0.0f);
        entity.GetBallView().GetAnimation2D().SetSpeed(1f);
      }
      entity.GetView().RemoveSubview((View) entity.GetShadowView());
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      GameApplication.GetInstance().Score += currentWorld.GetBossKillPoints();
      entity.GetSound(NPCButcher.SoundFxId.SoundFx_Dying).Play();
      this.m_shakeDone = false;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (((NPCButcher) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
        stateMachine.SetState((IState) NPCButcherStateDead.GetInstance());
      if (stateMachine.GetCurrentStateTime() <= 1.29999995231628)
        return;
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      if (this.m_shakeDone)
        return;
      currentWorld.GetCamera().StartShaking(0.5f, 78.53982f, new Vector2(0.0f, 0.1f), Vector2.Zero);
      this.m_shakeDone = true;
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      NPCButcher entity = (NPCButcher) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      GameApplication.GetInstance().CheckBossKillAchievements((NPCBoss) entity);
    }
  }
}
