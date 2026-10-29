// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.Gun
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
  public class Gun : Entity
  {
    public bool IsAvailable;
    public bool IsAutofireAvailable;
    public bool OutOfMana;
    protected Gun.GunType m_gunType;
    protected int m_damage;
    protected int m_manaConsumption;
    protected int m_level;
    protected float m_cooldown;

    public Gun()
    {
      this.m_gunType = Gun.GunType.Gun;
      this.IsAutofireAvailable = false;
      this.m_damage = 10;
      this.m_manaConsumption = 1;
      this.m_cooldown = 0.0f;
      this.m_level = 1;
      this.IsAvailable = true;
      this.OutOfMana = false;
    }

    public Gun.GunType GetGunType() => this.m_gunType;

    public int GetDamage() => this.m_damage;

    public int GetLevel() => this.m_level;

    public virtual void SetLevel(int level)
    {
      this.m_level = level;
      this.m_damage = 10 + (level - 1) * 5;
      this.m_manaConsumption = (int) (1.0 + (double) (level - 1) * 0.5);
    }

    public override void Initialize()
    {
    }

    public override void Update(float gameTime)
    {
      if ((double) this.m_cooldown <= 0.0)
        return;
      this.m_cooldown -= gameTime;
    }

    public override void Cleanup()
    {
    }

    public virtual bool Fire()
    {
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      if ((double) this.m_cooldown > 0.0)
        return false;
      this.m_cooldown = 0.2f;
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

    public enum GunType
    {
      Gun,
      MachineGun,
      FlameThrower,
      Ice,
      Explosive,
      HolyBeam,
      Number,
    }
  }
}
