// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.NPCShop
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Maths;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
  public class NPCShop : Entity
  {
    private Animation2DView m_view;
    private Animation2DView m_iconView;
    private Animation2DView m_shadowView;
    private Animation2DView m_deadNunView;
    private Animation2D m_standAnimation;
    private Animation2D m_iconAnimation;
    private Animation2D m_deadNunAnimation;
    private bool m_iconActive;
    private bool m_deadNunActive;
    private NPCShopViewController m_viewController;
    public bool EnterShopRequest;

    public NPCShopViewController GetViewController() => this.m_viewController;

    public bool IsIconActive() => this.m_iconActive;

    public void SetIconActive(bool active)
    {
      if (active)
      {
        if (!this.m_iconActive)
        {
          this.m_view.AddSubview((View) this.m_iconView);
          this.m_iconActive = true;
        }
      }
      else if (this.m_iconActive)
      {
        this.m_view.RemoveSubview((View) this.m_iconView);
        this.m_iconActive = false;
      }
      this.EnterShopRequest = false;
    }

    public void SetDeadNunActive(bool active)
    {
      if (active)
      {
        if (this.m_deadNunActive)
          return;
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) this.m_deadNunView, (Transform) null);
        this.m_deadNunActive = true;
      }
      else
      {
        if (!this.m_deadNunActive)
          return;
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) this.m_deadNunView);
        this.m_deadNunActive = false;
      }
    }

    public NPCShop()
    {
      this.m_view = new Animation2DView();
      this.m_iconView = new Animation2DView();
      this.m_shadowView = new Animation2DView();
      this.m_deadNunView = new Animation2DView();
      this.m_type = 9;
      this.m_standAnimation = new Animation2D();
      this.m_iconAnimation = new Animation2D();
      this.m_deadNunAnimation = new Animation2D();
      this.m_iconActive = false;
      this.m_deadNunActive = false;
      this.EnterShopRequest = false;
      this.m_viewController = new NPCShopViewController();
    }

    public override void Initialize()
    {
      this.LoadContent();
      this.m_view.Initialize();
      this.m_view.GetTransform().SetPosition(0.0f, 0.0f, -2f);
      this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.m_view.SetAnimation2D(this.m_standAnimation);
      this.m_iconView.Initialize();
      this.m_iconView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.m_iconView.GetTransform().SetPosition(0.0f, 1.7f, 0.1f);
      this.m_iconView.SetAnimation2D(this.m_iconAnimation);
      this.m_shadowView.Initialize();
      this.m_shadowView.SetAnimation2D(((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetMiscAnimation(GameWorld.MiscAnimId.HeroShadow));
      this.m_shadowView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.m_shadowView.GetTransform().Clear();
      this.m_shadowView.GetTransform().SetPosition(0.08f, 0.02f, -0.2f);
      this.m_shadowView.GetTransform().SetScale(1.3f);
      this.GetTransform().SetPosition(6f, 0.0f, 0.0f);
      this.GetTransform().SetScale(-1f, 1f);
      this.m_deadNunView.Initialize();
      this.m_deadNunView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.m_deadNunView.GetTransform().SetPosition(5f, 0.0f, -2f);
      this.m_deadNunView.SetAnimation2D(this.m_deadNunAnimation);
      this.m_deadNunActive = false;
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) this.m_view, this.GetTransform());
      this.m_view.AddSubview((View) this.m_iconView);
      this.m_view.AddSubview((View) this.m_shadowView);
      this.m_iconActive = true;
      this.m_viewController.Initialize(this);
      this.EnterShopRequest = false;
    }

    public override void Update(float elapsedTime)
    {
      this.m_view.Update(elapsedTime);
      this.m_shadowView.Update(elapsedTime);
      if (this.m_iconActive)
        this.m_iconView.Update(elapsedTime);
      if (this.m_deadNunActive)
        this.m_deadNunView.Update(elapsedTime);
      this.m_viewController.Update(elapsedTime);
      base.Update(elapsedTime);
    }

    public override void Cleanup()
    {
      this.m_standAnimation.Cleanup();
      this.m_iconAnimation.Cleanup();
      this.m_deadNunAnimation.Cleanup();
      this.m_view.Cleanup();
      this.m_iconView.Cleanup();
      this.m_shadowView.Cleanup();
      this.m_deadNunView.Cleanup();
      this.m_viewController.Cleanup();
      base.Cleanup();
    }

    public Animation2DView GetView() => this.m_view;

    private void LoadContent()
    {
      this.m_standAnimation.Initialize("Anims\\shop_npc_stand");
      this.m_standAnimation.SetLoop(Animation2DLoop.Always);
      this.m_iconAnimation.Initialize("Anims\\shop_npc_icon");
      this.m_iconAnimation.SetLoop(Animation2DLoop.Always);
      this.m_deadNunAnimation.Initialize("Anims\\shop_npc_dead_nun");
      this.m_deadNunAnimation.SetLoop(Animation2DLoop.Always);
    }
  }
}
