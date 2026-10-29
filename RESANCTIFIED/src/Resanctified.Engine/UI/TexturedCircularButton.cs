using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Resources;

namespace Resanctified.Engine.UI
{
    public class TexturedCircularButton : CircularButton
    {
        protected Resources.Texture m_textureSelector;
        protected Resources.Texture m_textureButton;
        protected VertexPositionTexture[] m_quadSelector;
        protected VertexPositionTexture[] m_quadButton;

        public TexturedCircularButton()
        {
            this.m_textureSelector = null;
            this.m_textureButton = null;
            this.m_quadSelector = null;
            this.m_quadButton = null;
        }

        public virtual void Initialize(
          ViewController vc,
          float radius,
          VertexPositionTexture[] quadSelector,
          VertexPositionTexture[] quadButton,
          Resources.Texture textureSelector,
          Resources.Texture textureButton)
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
            Resource.SafeRelease(ref this.m_textureButton);
            Resource.SafeRelease(ref this.m_textureSelector);
            base.Cleanup();
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            if (this.m_touch != null)
                Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quadSelector, VertexFormat.Position3Texture, this.m_transform, this.m_textureSelector, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
            Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quadButton, VertexFormat.Position3Texture, this.m_transform, this.m_textureButton, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
        }
    }
}
