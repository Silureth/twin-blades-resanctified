// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Necromancer.State.NPCNecromancerStateStand
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Maths;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Necromancer.State
{
  public class NPCNecromancerStateStand : IState
  {
    private static NPCNecromancerStateStand m_instance = new NPCNecromancerStateStand();

    public static NPCNecromancerStateStand Instance => NPCNecromancerStateStand.m_instance;

    public static NPCNecromancerStateStand GetInstance() => NPCNecromancerStateStand.m_instance;

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
      NPCNecromancer entity = (NPCNecromancer) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if ((double) entity.SpellTimer < 4.0)
        return;
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      entity.SpellTimer = 0.0f;
      if (entity.IsInvoqueDone || currentWorld.ZombiesAliveNb >= GameConstant.ZOMBIE_ALIVE_NB_BOSS)
      {
        entity.IsInvoqueDone = false;
        int num = 0;
        if ((double) entity.HitPoints < (double) entity.GetHitpointsMax() * 0.75)
          num = MathHelperEx.GetRandomInt(0, 100);
        if (num < 40)
          stateMachine.SetState((IState) NPCNecromancerStateFire.GetInstance());
        else if (num < 70)
          stateMachine.SetState((IState) NPCNecromancerStateSpikes.GetInstance());
        else if (entity.GetBossType() == NPCBoss.BossType.NecromancerForm2 && (double) entity.HitPoints < (double) entity.GetHitpointsMax() * 0.5)
          stateMachine.SetState((IState) NPCNecromancerStateEarthquake.GetInstance());
        else if (num < 90)
          stateMachine.SetState((IState) NPCNecromancerStateFire.GetInstance());
        else
          stateMachine.SetState((IState) NPCNecromancerStateSpikes.GetInstance());
      }
      else
        stateMachine.SetState((IState) NPCNecromancerStateInvoque.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
