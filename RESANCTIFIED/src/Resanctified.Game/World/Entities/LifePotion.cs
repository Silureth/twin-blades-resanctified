// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.LifePotion
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
  public class LifePotion : Entity
  {
    private float m_timer;
    private float m_blinkTimer;
    private LifePotion.StateId m_stateId;
    private Animation2DView m_view;
    private Animation2D[] m_animArray;
    private Sound m_sound;

    public LifePotion()
    {
      this.m_animArray = new Animation2D[3];
      for (int index = 0; index < 3; ++index)
        this.m_animArray[index] = new Animation2D();
      this.m_view = new Animation2DView();
      this.m_sound = new Sound();
      this.IsActive = false;
      this.m_timer = 0.0f;
      this.m_blinkTimer = 0.0f;
      this.m_stateId = LifePotion.StateId.Spawn;
    }

    public override void Initialize()
    {
      this.LoadContent();
      this.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
      this.m_view.GetTransform().SetPosition(0.0f, 0.0f, 3.6f);
      this.m_view.SetAnimation2D(this.m_animArray[0]);
      this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.m_view.GetAnimation2D().SetTime(0.0f);
      this.IsActive = false;
      this.m_timer = 0.0f;
      this.m_stateId = LifePotion.StateId.Spawn;
    }

    public override void Update(float gameTime)
    {
      if (!this.IsActive)
        return;
      this.m_view.Update(gameTime);
      this.m_timer += gameTime;
      if ((double) this.m_timer >= 18.0)
      {
        this.m_blinkTimer -= gameTime;
        if ((double) this.m_blinkTimer <= 0.0)
        {
          this.m_blinkTimer = 0.05f;
          this.m_view.SetVisible(!this.m_view.IsVisible());
        }
        if ((double) this.m_timer >= 20.0)
        {
          this.IsActive = false;
          Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) this.m_view);
          return;
        }
      }
      switch (this.m_stateId)
      {
        case LifePotion.StateId.Spawn:
          if (!this.m_view.GetAnimation2D().IsEndOfAnimationReached())
            break;
          this.m_view.SetAnimation2D(this.m_animArray[1]);
          this.m_view.GetAnimation2D().SetTime(0.0f);
          this.m_stateId = LifePotion.StateId.Stand;
          break;
        case LifePotion.StateId.Stand:
          PlayableCharacter hero = ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero();
          if (hero.HitPoints <= 0)
            break;
          Vector3 vector3 = this.GetTransform().GetPosition() - hero.GetTransform().GetPosition();
          vector3.X += 1.5f;
          if ((double) vector3.X * (double) vector3.X + (double) vector3.Y * (double) vector3.Y > 0.25)
            break;
          this.m_view.SetAnimation2D(this.m_animArray[2]);
          this.m_view.GetAnimation2D().SetTime(0.0f);
          this.m_sound.Play();
          this.m_stateId = LifePotion.StateId.Picked;
          hero.HitPoints += (int) ((double) hero.GetHitPointsMax() * 0.5);
          if (hero.HitPoints <= hero.GetHitPointsMax())
            break;
          hero.HitPoints = hero.GetHitPointsMax();
          break;
        case LifePotion.StateId.Picked:
          if (!this.m_view.GetAnimation2D().IsEndOfAnimationReached())
            break;
          this.IsActive = false;
          Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) this.m_view);
          break;
      }
    }

    public override void Cleanup()
    {
      for (int index = 0; index < 3; ++index)
        this.m_animArray[index].Cleanup();
      this.m_view.Cleanup();
      this.m_sound.Cleanup();
    }

    public Animation2DView GetView() => this.m_view;

    public Sound GetSound() => this.m_sound;

    public void SetActive(bool active)
    {
      this.IsActive = active;
      if (!active)
        return;
      this.m_timer = 0.0f;
      this.m_stateId = LifePotion.StateId.Spawn;
      this.m_view.SetAnimation2D(this.m_animArray[0]);
      this.m_view.GetAnimation2D().SetTime(0.0f);
      this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.m_view.SetVisible(true);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) this.m_view, this.GetTransform());
    }

    private void LoadContent()
    {
      this.m_animArray[0].Initialize("Anims\\life_potion_spawn");
      this.m_animArray[1].Initialize("Anims\\life_potion_stand");
      this.m_animArray[1].SetLoop(Animation2DLoop.Always);
      this.m_animArray[2].Initialize("Anims\\life_potion_picked");
      this.m_sound.Initialize("Sounds\\hero_potion_pickup", true);
    }

    public enum AnimId
    {
      Spawn,
      Stand,
      Picked,
      ArraySize,
    }

    public enum StateId
    {
      Spawn,
      Stand,
      Picked,
    }
  }
}
