using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM.GameState;

namespace Resanctified.Game.UI
{
    public class LogoMGSViewController : ViewController
    {
        private const float LOGO_TOP_X = 0.0f;
        private const float LOGO_TOP_Y = 0.0f;
        private const float LOGO_WIDTH = 480f;
        private const float LOGO_HEIGHT = 320f;
        private const float LOGO_TEXTURE_WIDTH = 512f;
        private const float LOGO_TEXTURE_HEIGHT = 512f;
        private Button m_buttonNext;
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_texture;

        public LogoMGSViewController()
        {
            this.m_texture = null;
            this.m_buttonNext = new Button();
        }

        public override void Initialize()
        {
            base.Initialize();
            Resource.SafeRelease(ref this.m_texture);
            this.m_texture = ResourceManager.GetInstance().LoadTexture("Textures\\logo_mgs");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            this.m_buttonNext.Initialize(this, 480f, 320f);
            this.m_buttonNext.GetTransform().SetPosition(240f, 160f);
        }

        public override void Cleanup()
        {
            this.m_buttonNext.Cleanup();
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
            if (!this.m_active || sender != this.m_buttonNext || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            GameStateLogoMGS.GetInstance().NextRequest = true;
        }
    }
}
