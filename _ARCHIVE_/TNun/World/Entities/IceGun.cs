// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.World.Entities.IceGun
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.World.Entities
{
  public class IceGun : Gun
  {
    private float m_freezeTime;

    public float GetFreezeTime() => this.m_freezeTime;

    public IceGun()
    {
      this.m_gunType = Gun.GunType.Ice;
      this.IsAutofireAvailable = true;
      this.SetLevel(1);
    }

    public override void SetLevel(int level)
    {
      this.m_level = level;
      this.m_damage = 10 + (level - 1) * 5;
      this.m_manaConsumption = (int) (2.0 + (double) (level - 1) * 0.5);
      this.m_freezeTime = (float) (2.0 + (double) (level - 1) * 1.0);
    }

    public override bool Fire()
    {
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      if ((double) this.m_cooldown > 0.0)
        return false;
      this.m_cooldown = 0.1f;
      if (hero.ManaPoints - this.m_manaConsumption < 0)
      {
        hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_Weapon_Empty).Play();
        this.OutOfMana = true;
        return false;
      }
      this.OutOfMana = false;
      hero.DoGunRaycast = true;
      hero.ManaPoints -= this.m_manaConsumption;
      if (!hero.GetGunFireView().IsVisible())
      {
        hero.GetGunFireView().SetAnimation2D(hero.GetAnimationStateMachine().GunFXAnimationArray[4]);
        hero.GetGunFireView().GetAnimation2D().SetTime(0.0f);
        hero.GetGunFireView().SetVisible(true);
        hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_IceGun_Start).Play();
        hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_IceGun).Play();
      }
      return true;
    }
  }
}
