// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.Animation2DView
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.Graphics;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;

namespace PSS.GameEngine.UI
{
  public sealed class Animation2DView : View
  {
    private Animation2D m_anim;
    private Texture m_srcTexture;
    private Texture m_dstTexture;

    public Animation2DView()
    {
      this.m_anim = (Animation2D) null;
      this.m_srcTexture = (Texture) null;
      this.m_dstTexture = (Texture) null;
    }

    public void SetAnimation2D(Animation2D anim)
    {
      this.m_anim = anim;
      this.m_useCustomColor = false;
    }

    public void SetAnimation2D(Animation2D anim, bool preserveCustomColor)
    {
      this.m_anim = anim;
      if (preserveCustomColor)
        return;
      this.m_useCustomColor = false;
    }

    public Animation2D GetAnimation2D() => this.m_anim;

    public void RemapTexture(Texture src, Texture dst)
    {
      Resource.SafeRelease<Texture>(ref this.m_srcTexture);
      Resource.SafeRelease<Texture>(ref this.m_dstTexture);
      if (src != null)
      {
        this.m_srcTexture = src;
        this.m_srcTexture.AddRef();
      }
      if (dst == null)
        return;
      this.m_dstTexture = dst;
      this.m_dstTexture.AddRef();
    }

    public override void Initialize()
    {
      base.Initialize();
      this.m_anim = (Animation2D) null;
      Resource.SafeRelease<Texture>(ref this.m_srcTexture);
      Resource.SafeRelease<Texture>(ref this.m_dstTexture);
    }

    public override void Cleanup()
    {
      this.m_anim = (Animation2D) null;
      Resource.SafeRelease<Texture>(ref this.m_srcTexture);
      Resource.SafeRelease<Texture>(ref this.m_dstTexture);
      base.Cleanup();
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawAnimation2DModel(this.m_anim.GetModel(), this.m_anim.GetFrame(), this.m_transform, this.m_blendFunc, this.m_useCustomColor, this.m_customColor, this.m_srcTexture, this.m_dstTexture, this.m_opaque);
    }

    public override void Update(float gameTime)
    {
      if (this.m_anim == null)
        return;
      this.m_anim.Update(gameTime);
    }

    public override bool IsInsideCamera(AABB cameraBoundingBox)
    {
      if (!this.m_visible)
        return false;
      AABB transformedBox;
      MathHelperEx.TransformBox(out transformedBox, this.m_anim.GetModel().GetBoundingBox(), this.m_transform.GetWorldTransform());
      return MathHelperEx.BoxIntersectsBox2D(transformedBox, cameraBoundingBox);
    }
  }
}
