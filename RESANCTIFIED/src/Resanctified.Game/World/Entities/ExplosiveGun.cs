// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.ExplosiveGun
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.World;

namespace Resanctified.Game.World.Entities
{
  public class ExplosiveGun : Gun
  {
    public ExplosiveGun()
    {
      this.m_gunType = Gun.GunType.Explosive;
      this.IsAutofireAvailable = false;
      this.SetLevel(1);
    }

    public override void SetLevel(int level)
    {
      this.m_level = level;
      this.m_damage = 80 + (level - 1) * 35;
      this.m_manaConsumption = (int) (20.0 + (double) (level - 1) * 8.0);
    }

    public override bool Fire()
    {
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      if ((double) this.m_cooldown > 0.0)
        return false;
      this.m_cooldown = 0.3f;
      if (hero.ManaPoints - this.m_manaConsumption < 0)
      {
        hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_Weapon_Empty).Play();
        this.OutOfMana = true;
        return false;
      }
      this.OutOfMana = false;
      hero.DoGunRaycast = true;
      hero.ManaPoints -= this.m_manaConsumption;
      hero.GetGunFireView().SetAnimation2D(hero.GetAnimationStateMachine().GunFXAnimationArray[0]);
      hero.GetGunFireView().GetAnimation2D().SetTime(0.0f);
      hero.GetGunFireView().SetVisible(true);
      hero.GetSound(PlayableCharacter.SoundFxId.SoundFx_Gun).Play();
      return true;
    }
  }
}
