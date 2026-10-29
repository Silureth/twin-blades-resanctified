// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.HolyBeamGun
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.World;

namespace Resanctified.Game.World.Entities
{
  public class HolyBeamGun : Gun
  {
    public HolyBeamGun()
    {
      this.m_gunType = Gun.GunType.HolyBeam;
      this.IsAutofireAvailable = true;
      this.SetLevel(1);
    }

    public override void SetLevel(int level)
    {
      this.m_level = level;
      this.m_damage = 35 + (level - 1) * 10;
      this.m_manaConsumption = (int) (6.0 + (double) (level - 1) * 2.0);
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
        hero.GetGunFireView().SetAnimation2D(hero.GetAnimationStateMachine().GunFXAnimationArray[7]);
        hero.GetGunFireView().GetAnimation2D().SetTime(0.0f);
        hero.GetGunFireView().SetVisible(true);
        hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_HolyGun_Start).Play();
        hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_HolyGun).Play();
      }
      return true;
    }
  }
}
