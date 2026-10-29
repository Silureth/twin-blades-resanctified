using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM.GameState.MainMenu;

namespace Resanctified.Game.UI
{
    public class CreditsViewController : ViewController
    {
        public const int CREDITS_LAST_PAGE_INDEX = 9;
        private const float CREDITS_TEXT_TITLE_X = 240f;
        private const float CREDITS_TEXT_TITLE1_Y = 250f;
        private const float CREDITS_TEXT_TITLE2_Y = 200f;
        private const float CREDITS_TEXT_NAME1_X = 205f;
        private const float CREDITS_TEXT_NAME1_Y = 160f;
        private const float CREDITS_TEXT_NAME2_X = 270f;
        private const float CREDITS_TEXT_NAME2_Y = 160f;
        private const float CREDITS_TEXT_TEXT_Y = 230f;
        private const float CREDITS_TEXT_OFFSET_Y = -20f;
        private const float CREDITS_TEXT_NAME_Y = 210f;
        private const float CREDITS_TEXT_COPYRIGHT_Y = 190f;
        private const float CREDITS_TEXT_WEB_Y = 170f;
        private const float CREDITS_TEXT_MAIL_Y = 150f;
        private const float CREDITS_TEXT_1_Y = 160f;
        private const float CREDITS_TEXT_2_Y = 140f;
        private const float CREDITS_TEXT_3_Y = 120f;
        private const float CREDITS_TEXT_4_Y = 100f;
        private const float CREDITS_TEXT_5_Y = 80f;
        private const float CREDITS_TEXT_6_Y = 60f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Button m_buttonBack;
        private Engine.Resources.Texture m_menuTexture;
        private bool m_endingMode;
        private int m_pageIndex;
        private Transform m_t = new Transform();

        public int GetPage() => this.m_pageIndex;

        public bool NextPage()
        {
            if (this.m_pageIndex == 9)
                return false;
            ++this.m_pageIndex;
            return true;
        }

        public void SetPage(int pageIndex) => this.m_pageIndex = pageIndex;

        public CreditsViewController()
        {
            this.m_menuTexture = null;
            this.m_pageIndex = 0;
            this.m_buttonBack = new Button();
        }

        public override void Initialize() => this.Initialize(false);

        public void Initialize(bool endingMode)
        {
            this.m_endingMode = endingMode;
            base.Initialize();
            Resource.SafeRelease(ref this.m_menuTexture);
            this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            if (!this.m_endingMode)
            {
                QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
                this.m_buttonBack.Initialize(this, 56f, 56f);
                this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
            }
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
            this.m_pageIndex = 0;
        }

        public override void Cleanup()
        {
            this.m_buttonBack.Cleanup();
            Resource.SafeRelease(ref this.m_menuTexture);
            Application.GetInstance().RemoveResponder(this);
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
            Font font = GameApplication.GetInstance().GetFont();
            if (!this.m_endingMode)
            {
                instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
                this.m_t.SetPosition(36f, 40f, 0.0f);
                instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            }
            else
                instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
            if (this.m_pageIndex < 3)
            {
                this.m_t.SetPosition(240f, 250f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE1_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PressStartStudio));
            }
            if (this.m_pageIndex == 0)
            {
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Programming));
                this.m_t.SetPosition(205f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ProgrammerName1));
                this.m_t.SetPosition(270f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ProgrammerName2));
            }
            if (this.m_pageIndex == 1)
            {
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Graphism));
                this.m_t.SetPosition(205f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.GraphistName1));
                this.m_t.SetPosition(270f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.GraphistName2));
            }
            if (this.m_pageIndex == 2)
            {
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SalesMarketing));
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SalesMarketingName));
            }
            if (this.m_pageIndex == 3)
            {
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MusicAndSound));
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MusicAndSoundName));
            }
            if (this.m_pageIndex == 4)
            {
                this.m_t.SetPosition(240f, 250f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE1_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherName));
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherProduction));
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherProductionName1));
                this.m_t.SetPosition(240f, 140f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherProductionName2));
                this.m_t.SetPosition(240f, 120f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherProductionName3));
                this.m_t.SetPosition(240f, 100f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherProductionName4));
            }
            if (this.m_pageIndex == 5)
            {
                this.m_t.SetPosition(240f, 250f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE1_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherName));
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherTest));
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherTestName1));
                this.m_t.SetPosition(240f, 140f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherTestName2));
                this.m_t.SetPosition(240f, 120f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherTestName3));
                this.m_t.SetPosition(240f, 100f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherTestName4));
            }
            if (this.m_pageIndex == 6)
            {
                this.m_t.SetPosition(240f, 250f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE1_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherName));
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherBusiness));
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherBusinessName1));
                this.m_t.SetPosition(240f, 140f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherBusinessName2));
            }
            if (this.m_pageIndex == 7)
            {
                this.m_t.SetPosition(240f, 250f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE1_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherName));
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherMarketing));
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherMarketingName));
            }
            if (this.m_pageIndex == 8)
            {
                this.m_t.SetPosition(240f, 250f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE1_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherName));
                this.m_t.SetPosition(240f, 200f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE2_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherThanks));
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherThanksName1));
                this.m_t.SetPosition(240f, 140f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherThanksName2));
                this.m_t.SetPosition(240f, 120f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherThanksName3));
                this.m_t.SetPosition(240f, 100f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherThanksName4));
                this.m_t.SetPosition(240f, 80f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherThanksName5));
                this.m_t.SetPosition(240f, 60f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PublisherThanksName6));
            }
            if (this.m_pageIndex == 9)
            {
                this.m_t.SetPosition(240f, 210f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.TwinBladesTM));
                this.m_t.SetPosition(240f, 190f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PSSCopyrights));
                this.m_t.SetPosition(240f, 170f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PSSWeb));
            }
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || sender != this.m_buttonBack || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            MainMenuStateCredits.GetInstance().BackRequest = true;
        }
    }
}
