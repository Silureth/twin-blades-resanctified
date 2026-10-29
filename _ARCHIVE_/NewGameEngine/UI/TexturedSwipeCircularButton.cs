// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.TexturedSwipeCircularButton
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework.Graphics;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Resources;

namespace PSS.GameEngine.UI
{
  public class TexturedSwipeCircularButton : SwipeCircularButton
  {
    protected PSS.GameEngine.Resources.Texture m_texture;
    protected VertexPositionTexture[] m_quad;

    public TexturedSwipeCircularButton()
    {
      this.m_texture = (PSS.GameEngine.Resources.Texture) null;
      this.m_quad = (VertexPositionTexture[]) null;
    }

    public override void Initialize(
      ViewController vc,
      float radius,
      float minSpeedX,
      float minSpeedY)
    {
      base.Initialize(vc, radius, minSpeedX, minSpeedY);
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_texture);
      this.m_quad = (VertexPositionTexture[]) null;
    }

    public override void Cleanup()
    {
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_texture);
      this.m_quad = (VertexPositionTexture[]) null;
      base.Cleanup();
    }

    public override void Draw()
    {
      if (!this.m_visible || this.m_quad == null || this.m_texture == null)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawQuad<VertexPositionTexture>(this.m_quad, VertexFormat.Position3Texture, this.m_transform, this.m_texture, this.m_customColor, 0, true, this.m_blendFunc);
    }

    public virtual void SetQuad(VertexPositionTexture[] quad, PSS.GameEngine.Resources.Texture texture)
    {
      this.m_quad = quad;
      this.m_texture = texture;
      if (this.m_texture == null)
        return;
      this.m_texture.AddRef();
    }
  }
}
