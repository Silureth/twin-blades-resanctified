// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Necromancer.State.NPCNecromancerStateDying
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.FSM.NPC.Orb.State;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Necromancer.State
{
  public class NPCNecromancerStateDying : IState
  {
    private static NPCNecromancerStateDying m_instance = new NPCNecromancerStateDying();

    public static NPCNecromancerStateDying Instance => NPCNecromancerStateDying.m_instance;

    public static NPCNecromancerStateDying GetInstance() => NPCNecromancerStateDying.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM) stateMachine;
      NPCNecromancer entity = (NPCNecromancer) npcNecromancerFsm.GetEntity();
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[2]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      entity.GetShieldView().SetAnimation2D(npcNecromancerFsm.AnimationArray[9]);
      entity.GetShieldView().GetAnimation2D().SetTime(0.0f);
      entity.GetShieldView().GetAnimation2D().SetSpeed(1f);
      if (entity.GetBossType() == NPCBoss.BossType.NecromancerForm2)
      {
        currentWorld.GetMagicOrb().IsDestroyed = true;
        if (currentWorld.GetMagicOrb().GetStateMachine().CurrentState != MagicOrbStateBreak.GetInstance() && currentWorld.GetMagicOrb().GetStateMachine().CurrentState != MagicOrbStateBroken.GetInstance())
          currentWorld.GetMagicOrb().GetStateMachine().SetState((IState) MagicOrbStateBreak.GetInstance());
        currentWorld.GetCamera().StartShaking(8f, 78.53982f, new Vector2(0.0f, 0.1f), Vector2.Zero);
      }
      GameApplication.GetInstance().Score += currentWorld.GetBossKillPoints();
      entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Dying).Play();
      if (entity.GetBossType() != NPCBoss.BossType.NecromancerForm2)
        return;
      entity.GetView().GetTransform().SetPosition(0.0f, 0.0f, 3.7f);
      currentWorld.GetHero().SetIsInvulnerable(true);
      currentWorld.GetHero().ResetBlink();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      NPCNecromancer entity = (NPCNecromancer) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.GetBossType() == NPCBoss.BossType.NecromancerForm2)
      {
        GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
        currentWorld.GetHero().SetIsInvulnerable(true);
        currentWorld.GetHero().ResetBlink();
      }
      if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      entity.GetView().RemoveSubview((View) entity.GetShieldView());
      stateMachine.SetState((IState) NPCNecromancerStateDead.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetCamera().StopShaking();
      NPCNecromancer entity = (NPCNecromancer) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.GetBossType() != NPCBoss.BossType.NecromancerForm1)
        return;
      GameApplication.GetInstance().CheckBossKillAchievements((NPCBoss) entity);
    }
  }
}
