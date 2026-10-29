// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.State.NPCZombieStateDying
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Maths;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie.State
{
  public class NPCZombieStateDying : IState
  {
    private static NPCZombieStateDying m_instance = new NPCZombieStateDying();

    public static NPCZombieStateDying Instance => NPCZombieStateDying.m_instance;

    public static NPCZombieStateDying GetInstance() => NPCZombieStateDying.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCZombieFSM npcZombieFsm = (NPCZombieFSM) stateMachine;
      NPCZombie entity = (NPCZombie) npcZombieFsm.GetEntity();
      int randomInt = MathHelperEx.GetRandomInt(0, 100);
      bool flag = false;
      GameApplication instance = GameApplication.GetInstance();
      switch (entity.GetHitType())
      {
        case NPCZombie.HitType.Slash:
        case NPCZombie.HitType.AirSlash:
          if (randomInt < 50)
          {
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[14]);
            flag = true;
          }
          else if (randomInt < 75)
          {
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[12]);
            PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
          }
          else
          {
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[13]);
            flag = true;
          }
          entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_BodyShot).Play();
          if (entity.GetHitType() == NPCZombie.HitType.AirSlash)
          {
            ++instance.AchievementCounter_500ZombiesKilledAirSlash;
            break;
          }
          break;
        case NPCZombie.HitType.Bullet:
        case NPCZombie.HitType.Explosion:
          if (randomInt < 50)
          {
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[12]);
            PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_BodyShot).Play();
          }
          else
          {
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[13]);
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_HeadShot).Play();
            flag = true;
          }
          if (entity.GetHitType() == NPCZombie.HitType.Explosion)
          {
            ++instance.AchievementCounter_500ZombiesKilledExplosiveGun;
            break;
          }
          ++instance.AchievementCounter_500ZombiesKilledBullet;
          break;
        case NPCZombie.HitType.Burn:
          entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[15]);
          entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_Burned).Play();
          ++instance.AchievementCounter_500ZombiesKilledFlameThrower;
          break;
        case NPCZombie.HitType.Ice:
          entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[17]);
          entity.GetSound(NPCZombie.SoundFxId.SoundFx_IceBreak).Play();
          PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
          ++instance.AchievementCounter_500ZombiesKilledIceGun;
          break;
        case NPCZombie.HitType.Holy:
          entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[16]);
          entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_Burned).Play();
          PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
          ++instance.AchievementCounter_1000ZombiesKilledHolyBeam;
          break;
        case NPCZombie.HitType.NPC:
          if (randomInt < 50)
          {
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[14]);
            flag = true;
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_BodyShot).Play();
            break;
          }
          if (randomInt < 75)
          {
            entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[12]);
            PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
            entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_BodyShot).Play();
            break;
          }
          entity.GetView().SetAnimation2D(npcZombieFsm.AnimationArray[13]);
          flag = true;
          entity.GetSound(NPCZombie.SoundFxId.SoundFx_Dying_HeadShot).Play();
          break;
      }
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      entity.GetPhysics().Velocity.X = 0.0f;
      entity.IsAlive = false;
      Vector3 position = entity.GetView().GetTransform().GetPosition() with
      {
        Z = 3.5f
      };
      entity.GetView().GetTransform().SetPosition(position);
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      --currentWorld.ZombiesAliveNb;
      ++currentWorld.ZombiesKilledNb;
      --currentWorld.ZombiesLeftToKill;
      if ((double) currentWorld.GetHero().HitPoints < (double) currentWorld.GetHero().GetHitPointsMax() * 0.5 && currentWorld.GetActivePotionsNumber() == 0)
      {
        int num;
        if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
        {
          num = 1;
          if ((double) currentWorld.GetHero().HitPoints < (double) currentWorld.GetHero().GetHitPointsMax() * 0.25)
            num = 2;
        }
        else
        {
          num = 2;
          if ((double) currentWorld.GetHero().HitPoints < (double) currentWorld.GetHero().GetHitPointsMax() * 0.25)
            num = 4;
        }
        currentWorld.ZombiesLeftToKillForPotion -= num;
      }
      else
        --currentWorld.ZombiesLeftToKillForPotion;
      if (!entity.NoKillCount)
      {
        ++instance.AchievementCounter_100ZombiesKilled;
        ++instance.AchievementCounter_500ZombiesKilled;
        ++instance.AchievementCounter_1000ZombiesKilled;
        ++currentWorld.GetHero().Hearts;
        GameApplication.GetInstance().Score += currentWorld.GetKillPoints(entity);
      }
      else
        entity.NoKillCount = false;
      GameApplication.GetInstance().CheckKillsAchievements();
      if (currentWorld.ZombiesLeftToKillForPotion <= 0)
        currentWorld.AddPotion(entity.GetTransform().GetPosition().X, entity.GetTransform().GetPosition().Y);
      if (!flag || MathHelperEx.GetRandomInt(0, 100) > 40)
        return;
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHud().SplashBlood();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (!((NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      stateMachine.SetState((IState) NPCZombieStateDead.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
