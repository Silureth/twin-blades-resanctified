// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.Explosion
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
  public class Explosion : Entity
  {
    private Animation2DView m_view;
    private Animation2D m_anim;
    private Sound m_sound;

    public Explosion()
    {
      this.IsActive = false;
      this.m_view = new Animation2DView();
      this.m_anim = new Animation2D();
      this.m_sound = new Sound();
    }

    public override void Initialize()
    {
      this.LoadContent();
      this.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
      this.m_view.GetTransform().SetPosition(0.0f, 0.0f, 4f);
      this.m_view.SetAnimation2D(this.m_anim);
      this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.IsActive = false;
    }

    public override void Update(float gameTime) => this.m_view.Update(gameTime);

    public override void Cleanup()
    {
      this.m_anim.Cleanup();
      this.m_view.Cleanup();
      this.m_sound.Cleanup();
    }

    public Animation2DView GetView() => this.m_view;

    public Sound GetSound() => this.m_sound;

    private void LoadContent()
    {
      this.m_anim.Initialize("Anims\\fx_explode");
      this.m_sound.Initialize("Sounds\\fx_explosion", true);
    }
  }
}
