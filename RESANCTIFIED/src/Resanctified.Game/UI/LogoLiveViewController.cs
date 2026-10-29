using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;

namespace Resanctified.Game.UI
{
    public class LogoLiveViewController : ViewController
    {
        private const float LOGO_TOP_X = 0.0f;
        private const float LOGO_TOP_Y = 0.0f;
        private const float LOGO_WIDTH = 480f;
        private const float LOGO_HEIGHT = 320f;
        private const float LOGO_TEXTURE_WIDTH = 512f;
        private const float LOGO_TEXTURE_HEIGHT = 512f;
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_texture;

        public LogoLiveViewController() => this.m_texture = null;

        public override void Initialize()
        {
            base.Initialize();
            Resource.SafeRelease(ref this.m_texture);
            this.m_texture = ResourceManager.GetInstance().LoadTexture("Textures\\logo_live");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_texture);
            Application.GetInstance().RemoveResponder(this);
            base.Cleanup();
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_texture);
            instance.End2DMode();
        }

        public override void Update(float elapsedTime)
        {
            int num = this.m_active ? 1 : 0;
        }

        public override void Action(View sender, ActionEvent evt)
        {
            int num = this.m_active ? 1 : 0;
        }
    }
}
