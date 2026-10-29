using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Resources;

namespace Resanctified.Engine.UI
{
    public class TexturedSwipeCircularButton : SwipeCircularButton
    {
        protected Resources.Texture m_texture;
        protected VertexPositionTexture[] m_quad;

        public TexturedSwipeCircularButton()
        {
            this.m_texture = null;
            this.m_quad = null;
        }

        public override void Initialize(
          ViewController vc,
          float radius,
          float minSpeedX,
          float minSpeedY)
        {
            base.Initialize(vc, radius, minSpeedX, minSpeedY);
            Resource.SafeRelease(ref this.m_texture);
            this.m_quad = null;
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_texture);
            this.m_quad = null;
            base.Cleanup();
        }

        public override void Draw()
        {
            if (!this.m_visible || this.m_quad == null || this.m_texture == null)
                return;
            Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quad, VertexFormat.Position3Texture, this.m_transform, this.m_texture, this.m_customColor, 0, true, this.m_blendFunc);
        }

        public virtual void SetQuad(VertexPositionTexture[] quad, Resources.Texture texture)
        {
            this.m_quad = quad;
            this.m_texture = texture;
            if (this.m_texture == null)
                return;
            this.m_texture.AddRef();
        }
    }
}
