using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM.GameState.MainMenu;

namespace Resanctified.Game.UI
{
    public class AboutMenuViewController : ViewController
    {
        private const float TITLE_X = 240f;
        private const float TITLE_Y = 250f;
        private const float TEXT_X = 240f;
        private const float TEXT_TITLE_Y = 250f;
        private const float TEXT_VERSION_Y = 220f;
        private const float TEXT_1_Y = 180f;
        private const float TEXT_2_Y = 150f;
        private const float TEXT_3_Y = 120f;
        private const float TEXT_4_Y = 90f;
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_menuTexture;
        private Button m_buttonBack;
        private Transform m_t = new Transform();
        private string m_version;

        public AboutMenuViewController()
        {
            this.m_menuTexture = null;
            this.m_buttonBack = new Button();
            this.m_version = "v. " + GameApplication.GetInstance().GetAssemblyVersionString();
        }

        public override void Initialize()
        {
            base.Initialize();
            Resource.SafeRelease(ref this.m_menuTexture);
            this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            this.m_buttonBack.Initialize(this, 56f, 56f);
            this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_menuTexture);
            this.m_buttonBack.Cleanup();
            base.Cleanup();
        }

        public override void Update(float elapsedTime)
        {
            if (!this.m_active)
                return;
            base.Update(elapsedTime);
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            int currentLanguage = (int)GameApplication.GetInstance().CurrentLanguage;
            Font font = GameApplication.GetInstance().GetFont();
            instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
            this.m_t.SetPosition(36f, 40f, 0.0f);
            instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
            this.m_t.SetPosition(240f, 250f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, "Twin Blades: The Reaping Vanguard");
            this.m_t.SetScale(0.9f);
            this.m_t.SetPosition(240f, 220f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, this.m_version);
            this.m_t.SetScale(1f);
            this.m_t.SetPosition(240f, 180f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.TwinBladesTM));
            this.m_t.SetPosition(240f, 150f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PSSCopyrights));
            this.m_t.SetPosition(240f, 120f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PSSWeb));
            this.m_t.SetPosition(240f, 90f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PSSSupport));
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || sender != this.m_buttonBack || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            MainMenuStateAbout.GetInstance().BackRequest = true;
        }
    }
}
