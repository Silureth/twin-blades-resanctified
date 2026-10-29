// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.UI.TexturedCircularButton
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Resources;

namespace Resanctified.Engine.UI
{
  public class TexturedCircularButton : CircularButton
  {
    protected Resanctified.Engine.Resources.Texture m_textureSelector;
    protected Resanctified.Engine.Resources.Texture m_textureButton;
    protected VertexPositionTexture[] m_quadSelector;
    protected VertexPositionTexture[] m_quadButton;

    public TexturedCircularButton()
    {
      this.m_textureSelector = (Resanctified.Engine.Resources.Texture) null;
      this.m_textureButton = (Resanctified.Engine.Resources.Texture) null;
      this.m_quadSelector = (VertexPositionTexture[]) null;
      this.m_quadButton = (VertexPositionTexture[]) null;
    }

    public virtual void Initialize(
      ViewController vc,
      float radius,
      VertexPositionTexture[] quadSelector,
      VertexPositionTexture[] quadButton,
      Resanctified.Engine.Resources.Texture textureSelector,
      Resanctified.Engine.Resources.Texture textureButton)
    {
      this.Initialize(vc, radius);
      this.m_textureSelector = textureSelector;
      this.m_textureSelector.AddRef();
      this.m_textureButton = textureButton;
      this.m_textureButton.AddRef();
      this.m_quadSelector = quadSelector;
      this.m_quadButton = quadButton;
    }

    public override void Cleanup()
    {
      Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_textureButton);
      Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_textureSelector);
      base.Cleanup();
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      if (this.m_touch != null)
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawQuad<VertexPositionTexture>(this.m_quadSelector, VertexFormat.Position3Texture, this.m_transform, this.m_textureSelector, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawQuad<VertexPositionTexture>(this.m_quadButton, VertexFormat.Position3Texture, this.m_transform, this.m_textureButton, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
    }
  }
}
