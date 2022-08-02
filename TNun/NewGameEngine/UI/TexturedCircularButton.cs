// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.TexturedCircularButton
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework.Graphics;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Resources;

namespace PSS.GameEngine.UI
{
  public class TexturedCircularButton : CircularButton
  {
    protected PSS.GameEngine.Resources.Texture m_textureSelector;
    protected PSS.GameEngine.Resources.Texture m_textureButton;
    protected VertexPositionTexture[] m_quadSelector;
    protected VertexPositionTexture[] m_quadButton;

    public TexturedCircularButton()
    {
      this.m_textureSelector = (PSS.GameEngine.Resources.Texture) null;
      this.m_textureButton = (PSS.GameEngine.Resources.Texture) null;
      this.m_quadSelector = (VertexPositionTexture[]) null;
      this.m_quadButton = (VertexPositionTexture[]) null;
    }

    public virtual void Initialize(
      ViewController vc,
      float radius,
      VertexPositionTexture[] quadSelector,
      VertexPositionTexture[] quadButton,
      PSS.GameEngine.Resources.Texture textureSelector,
      PSS.GameEngine.Resources.Texture textureButton)
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
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_textureButton);
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_textureSelector);
      base.Cleanup();
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      if (this.m_touch != null)
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawQuad<VertexPositionTexture>(this.m_quadSelector, VertexFormat.Position3Texture, this.m_transform, this.m_textureSelector, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawQuad<VertexPositionTexture>(this.m_quadButton, VertexFormat.Position3Texture, this.m_transform, this.m_textureButton, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
    }
  }
}
