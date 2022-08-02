// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.World.Entities.BulletImpact
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.Graphics;
using PSS.GameEngine.Graphics.Renderer;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.UI;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.World.Entities
{
  public class BulletImpact : Entity
  {
    private BulletImpact.ImpactType m_impactType;
    private Animation2DView m_view;
    private Animation2D[] m_animArray;
    private Sound[] m_soundArray;

    public BulletImpact()
    {
      this.m_impactType = BulletImpact.ImpactType.Blood;
      this.IsActive = false;
      this.m_view = new Animation2DView();
      this.m_animArray = new Animation2D[5];
      for (int index = 0; index < 5; ++index)
        this.m_animArray[index] = new Animation2D();
      this.m_soundArray = new Sound[4];
      for (int index = 0; index < 4; ++index)
        this.m_soundArray[index] = new Sound();
    }

    public override void Initialize()
    {
      this.LoadContent();
      this.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
      this.m_view.GetTransform().SetPosition(0.0f, 0.0f, 3.5f);
      this.m_view.SetAnimation2D(this.m_animArray[0]);
      this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.IsActive = false;
    }

    public override void Update(float gameTime) => this.m_view.Update(gameTime);

    public override void Cleanup()
    {
      for (int index = 0; index < 5; ++index)
        this.m_animArray[index].Cleanup();
      this.m_view.Cleanup();
      for (int index = 0; index < 4; ++index)
        this.m_soundArray[index].Cleanup();
    }

    public void SetImpactType(BulletImpact.ImpactType type)
    {
      this.m_impactType = type;
      switch (type)
      {
        case BulletImpact.ImpactType.Blood:
          this.m_view.SetAnimation2D(this.m_animArray[0]);
          break;
        case BulletImpact.ImpactType.Armor:
          this.m_view.SetAnimation2D(this.m_animArray[1]);
          break;
        case BulletImpact.ImpactType.Burn:
          this.m_view.SetAnimation2D(this.m_animArray[2]);
          break;
        case BulletImpact.ImpactType.Ice:
          this.m_view.SetAnimation2D(this.m_animArray[3]);
          break;
        case BulletImpact.ImpactType.Holy:
          this.m_view.SetAnimation2D(this.m_animArray[4]);
          break;
      }
    }

    public Animation2DView GetView() => this.m_view;

    public Sound GetSound()
    {
      switch (this.m_impactType)
      {
        case BulletImpact.ImpactType.Blood:
          return this.m_soundArray[0];
        case BulletImpact.ImpactType.Armor:
          return this.m_soundArray[1];
        case BulletImpact.ImpactType.Burn:
        case BulletImpact.ImpactType.Holy:
          return (Sound) null;
        case BulletImpact.ImpactType.Ice:
          return this.m_soundArray[3];
        default:
          return (Sound) null;
      }
    }

    private void LoadContent()
    {
      this.m_animArray[0].Initialize("Anims\\fx_impact_blood");
      this.m_animArray[1].Initialize("Anims\\fx_impact_armor");
      this.m_animArray[2].Initialize("Anims\\fx_impact_burn");
      this.m_animArray[3].Initialize("Anims\\fx_impact_ice");
      this.m_animArray[4].Initialize("Anims\\fx_impact_holy");
      this.m_soundArray[0].Initialize("Sounds\\zombie_hit01", true);
      this.m_soundArray[1].Initialize("Sounds\\zombie_hit_armor", true);
      this.m_soundArray[2].Initialize("Sounds\\hero_weapon_flame_start", true);
      this.m_soundArray[3].Initialize("Sounds\\zombie_iced", true);
    }

    public enum ImpactType
    {
      Blood,
      Armor,
      Burn,
      Ice,
      Holy,
    }

    public enum ImpactAnimationId
    {
      Blood,
      Armor,
      Burn,
      Ice,
      Holy,
      ArraySize,
    }

    public enum SoundFxId
    {
      SoundFx_Blood,
      SoundFx_Armor,
      SoundFx_Burn,
      SoundFx_Ice,
      SoundFx_ArraySize,
    }
  }
}
