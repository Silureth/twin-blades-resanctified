// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.State.NPCZombieStateGrabEat
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie.State
{
  public class NPCZombieStateGrabEat : IState
  {
    private static NPCZombieStateGrabEat m_instance = new NPCZombieStateGrabEat();

    public static NPCZombieStateGrabEat Instance => NPCZombieStateGrabEat.m_instance;

    public static NPCZombieStateGrabEat GetInstance() => NPCZombieStateGrabEat.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCZombieFSM npcZombieFsm = (NPCZombieFSM) stateMachine;
      NPCZombie entity = (NPCZombie) npcZombieFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[6]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      npcZombieFsm.GrabEatTimer = 0.0f;
      Vector3 position = entity.GetView().GetTransform().GetPosition() with
      {
        Z = 3f
      };
      entity.GetView().GetTransform().SetPosition(position);
      entity.GetSound(NPCZombie.SoundFxId.SoundFx_GrabEat_1).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      NPCZombieFSM npcZombieFsm = (NPCZombieFSM) stateMachine;
      NPCZombie entity = (NPCZombie) npcZombieFsm.GetEntity();
      if (entity.IsHit)
      {
        stateMachine.SetState((IState) NPCZombieStateHit.GetInstance());
      }
      else
      {
        PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
        npcZombieFsm.GrabEatTimer -= gameTime;
        if ((double) npcZombieFsm.GrabEatTimer <= 0.0)
        {
          hero.HitPoints -= entity.GetGrabDamage();
          npcZombieFsm.GrabEatTimer = 0.5f;
        }
        if (hero.HitPoints > 0)
          return;
        stateMachine.SetState((IState) NPCZombieStateStand.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      Vector3 position = entity.GetView().GetTransform().GetPosition() with
      {
        Z = -1f
      };
      entity.GetView().GetTransform().SetPosition(position);
      entity.GetSound(NPCZombie.SoundFxId.SoundFx_GrabEat_1).Stop();
    }
  }
}
