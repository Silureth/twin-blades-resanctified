using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;

namespace Resanctified.Engine.UI
{
    public class TexturedRoundThumbstick : RoundThumbstick
    {
        protected VertexPositionTexture[] m_quadSocket;
        protected VertexPositionTexture[] m_quadThumbstick;
        protected VertexPositionTexture[] m_quadSelector;
        protected Resources.Texture m_textureSelector;
        protected Resources.Texture m_textureSocket;
        protected Resources.Texture m_textureThumbstick;
        protected Transform m_thumbstickTransform;

        public TexturedRoundThumbstick() => this.m_thumbstickTransform = new Transform();

        public virtual void Initialize(
          ViewController vc,
          PSSThumbstick thumbstick,
          float detectionRange,
          float thumbstickRange,
          VertexPositionTexture[] quadSelector,
          VertexPositionTexture[] quadSocket,
          VertexPositionTexture[] quadThumbstick,
          Resources.Texture textureSelector,
          Resources.Texture textureSocket,
          Resources.Texture textureThumbstick)
        {
            this.Initialize(vc, thumbstick, detectionRange, thumbstickRange);
            this.m_textureSelector = textureSelector;
            textureSelector.AddRef();
            this.m_textureSocket = textureSocket;
            textureSocket.AddRef();
            this.m_textureThumbstick = textureThumbstick;
            textureThumbstick.AddRef();
            this.m_quadSelector = quadSelector;
            this.m_quadSocket = quadSocket;
            this.m_quadThumbstick = quadThumbstick;
            this.m_thumbstickTransform.Clear();
            this.m_thumbstickTransform.SetParent(this.m_transform);
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_textureSocket);
            Resource.SafeRelease(ref this.m_textureSelector);
            Resource.SafeRelease(ref this.m_textureThumbstick);
            base.Cleanup();
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            if (this.m_touch != null)
                Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quadSelector, VertexFormat.Position3Texture, this.m_transform, this.m_textureSelector, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
            Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quadSocket, VertexFormat.Position3Texture, this.m_transform, this.m_textureSocket, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
            this.m_thumbstickTransform.SetPosition(this.m_thumbstickPosition.X * this.m_thumbstickRange, this.m_thumbstickPosition.Y * this.m_thumbstickRange, 0.0f);
            Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quadThumbstick, VertexFormat.Position3Texture, this.m_thumbstickTransform, this.m_textureThumbstick, this.m_customColor, 0, true, this.m_blendFunc, this.m_opaque);
        }
    }
}
