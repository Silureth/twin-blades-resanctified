// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.World.Entities.ParallaxEntity
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.UI;

namespace Resanctified.Engine.World.Entities
{
  public class ParallaxEntity : Entity
  {
    private float m_factorX;
    private float m_factorY;
    private Vector3 m_initialCameraPosition;
    private CameraEntity m_camera;
    private Animation2D m_anim;
    private Animation2DView m_view;
    private bool m_sky;

    public ParallaxEntity()
    {
      this.m_type = 1;
      this.m_anim = new Animation2D();
      this.m_view = new Animation2DView();
    }

    public void Initialize(
      string animFilename,
      float factorX,
      float factorY,
      bool opaque,
      BlendFunc blendFunc,
      bool sky)
    {
      this.Initialize();
      this.m_factorX = factorX;
      this.m_factorY = factorY;
      this.m_anim.Initialize(animFilename);
      this.m_anim.SetLoop(Animation2DLoop.Always);
      this.m_view.Initialize();
      this.m_view.SetAnimation2D(this.m_anim);
      this.m_view.SetOpaque(opaque);
      this.m_view.SetBlendFunc(blendFunc);
      this.m_camera = (CameraEntity) null;
      this.m_sky = sky;
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) this.m_view, this.m_transform);
    }

    public override void Cleanup()
    {
      this.m_anim.Cleanup();
      this.m_view.Cleanup();
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) this.m_view);
      base.Cleanup();
    }

    public void MoveRelativeToCamera(CameraEntity camera, Vector3 initialCameraPosition)
    {
      this.m_camera = camera;
      this.m_initialCameraPosition = initialCameraPosition;
      if (!this.m_sky)
        return;
      this.m_view.GetTransform().SetPosition(this.m_camera.GetTransform().GetPosition());
    }

    public override void Update(float elapsedTime)
    {
      if (this.m_sky)
      {
        this.m_view.GetTransform().SetPosition(this.m_camera.GetTransform().GetPosition());
      }
      else
      {
        Vector3 position = this.m_transform.GetPosition();
        Vector3 vector3 = this.m_camera.GetTransform().GetPosition() - this.m_initialCameraPosition;
        vector3.X *= this.m_factorX;
        vector3.Y *= this.m_factorY;
        vector3.Z = 0.0f;
        this.m_view.GetTransform().SetPosition(position.X + vector3.X, 0.0f, 0.0f);
      }
      this.m_anim.Update(elapsedTime);
      base.Update(elapsedTime);
    }

    public Animation2D GetAnimation2D() => this.m_anim;
  }
}
