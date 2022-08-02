// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.World.Entities.Spike
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.Graphics;
using PSS.GameEngine.Graphics.Renderer;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.World.Entities
{
  public class Spike : Entity
  {
    private Animation2DView m_view;
    private Animation2D m_animMoveOut;
    private Animation2D m_animMoveBack;
    private Sound m_sound;
    private bool m_moveBack;
    private float m_time;

    public Spike()
    {
      this.IsActive = false;
      this.m_view = new Animation2DView();
      this.m_animMoveOut = new Animation2D();
      this.m_animMoveBack = new Animation2D();
      this.m_sound = new Sound();
      this.m_moveBack = false;
      this.m_time = 0.0f;
    }

    public override void Initialize()
    {
      this.LoadContent();
      this.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
      this.m_view.GetTransform().SetPosition(0.0f, 0.0f, -1.5f);
      this.m_view.SetAnimation2D(this.m_animMoveOut);
      this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.IsActive = false;
      this.m_moveBack = false;
      this.m_time = 0.0f;
    }

    public override void Update(float gameTime)
    {
      if (!this.IsActive)
        return;
      if (!this.m_moveBack)
      {
        this.m_time += gameTime;
        if ((double) this.m_time >= 1.5)
        {
          this.m_view.SetAnimation2D(this.m_animMoveBack);
          this.m_view.GetAnimation2D().SetTime(0.0f);
          this.m_view.GetAnimation2D().SetSpeed(1f);
          this.m_moveBack = true;
        }
      }
      else if (this.m_view.GetAnimation2D().IsEndOfAnimationReached())
      {
        this.IsActive = false;
        this.m_moveBack = false;
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) this.m_view);
      }
      this.m_view.Update(gameTime);
    }

    public override void UpdateCollisions()
    {
      if (!this.IsActive)
        return;
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      AABB? box = this.GetView().GetAnimation2D().GetBox("attack1");
      if (!box.HasValue)
        return;
      PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
      if (hero.IsHit || hero.GetIsInvulnerable() || hero.IsInAir)
        return;
      AABB transformedBox;
      MathHelperEx.TransformBox(out transformedBox, box.Value, this.m_transform.GetWorldTransform());
      CollisionResult[] results = new CollisionResult[1];
      Entity[] entities = new Entity[1]
      {
        (Entity) hero
      };
      if (CollisionHelper.CollideEntities(transformedBox, entities, 1, ref results, 1, false) <= 0)
        return;
      hero.Hit(100, false);
    }

    public override void Cleanup()
    {
      this.m_animMoveOut.Cleanup();
      this.m_animMoveBack.Cleanup();
      this.m_view.Cleanup();
      this.m_sound.Cleanup();
    }

    public void Activate()
    {
      this.m_view.SetAnimation2D(this.m_animMoveOut);
      this.m_view.GetAnimation2D().SetTime(0.0f);
      this.m_view.GetAnimation2D().SetSpeed(1f);
      this.m_sound.Play();
      this.IsActive = true;
      this.m_moveBack = false;
      this.m_time = 0.0f;
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) this.m_view, this.GetTransform());
    }

    public Animation2DView GetView() => this.m_view;

    public Sound GetSound() => this.m_sound;

    private void LoadContent()
    {
      this.m_animMoveOut.Initialize("Anims\\fx_spike_move_out");
      this.m_animMoveBack.Initialize("Anims\\fx_spike_move_back");
      this.m_sound.Initialize("Sounds\\fx_spikes", true);
    }
  }
}
