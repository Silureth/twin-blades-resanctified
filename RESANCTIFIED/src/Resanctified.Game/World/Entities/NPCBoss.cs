// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.NPCBoss
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
  public class NPCBoss : Entity
  {
    public bool IsHit;
    public int HitPoints;
    protected Physics2D m_physics;
    protected NPCZombie.HitType m_currentHitType;
    protected NPCBoss.BossType m_bossType;

    public NPCBoss()
    {
      this.m_physics = new Physics2D((Entity) this, 0.0f);
      this.IsHit = false;
      this.HitPoints = 3000;
      this.m_bossType = NPCBoss.BossType.ButcherForm1;
      this.m_type = 5;
    }

    public virtual int Hit(NPCZombie.HitType hitType, int damage)
    {
      this.IsHit = true;
      this.m_currentHitType = hitType;
      int num = damage;
      this.HitPoints -= damage;
      if (this.HitPoints <= 0)
      {
        num += this.HitPoints;
        this.HitPoints = 0;
      }
      return num;
    }

    public virtual bool IsDead() => this.HitPoints <= 0;

    public Physics2D GetPhysics() => this.m_physics;

    public NPCZombie.HitType GetHitType() => this.m_currentHitType;

    public void SetHitType(NPCZombie.HitType type) => this.m_currentHitType = type;

    public NPCBoss.BossType GetBossType() => this.m_bossType;

    public enum BossType
    {
      ButcherForm1,
      ButcherForm2,
      NecromancerForm1,
      NecromancerForm2,
      Mistress,
      Number,
    }
  }
}
