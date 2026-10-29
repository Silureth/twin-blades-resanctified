using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;

namespace Resanctified.Game.UI
{
    public class HelpViewController : ViewController
    {
        public const int HELP_LAST_PAGE_INDEX = 1;
        private const float HELP_PAGE1_POS_X = 120f;
        private const float HELP_PAGE1_POS_Y = 180f;
        private const float HELP_PAGE2_POS_X = 120f;
        private const float HELP_PAGE2_POS_Y = 190f;
        private const float HELP_LIFE_POS_X = 100f;
        private const float HELP_LIFE_POS_Y = 255f;
        private const float HELP_ENERGY_POS_X = 45f;
        private const float HELP_ENERGY_POS_Y = 230f;
        private const float HELP_HEART_POS_X = 340f;
        private const float HELP_HEART_POS_Y = 285f;
        private const float HELP_GUNTYPE_POS_X = 470f;
        private const float HELP_GUNTYPE_POS_Y = 210f;
        private const float HELP_FIRE_POS_X = 425f;
        private const float HELP_FIRE_POS_Y = 95f;
        private const float HELP_SLASH_POS_X = 345f;
        private const float HELP_SLASH_POS_Y = 95f;
        private const float HELP_MOVE_POS_X = 120f;
        private const float HELP_MOVE_POS_Y = 85f;
        private const float HELP_PAUSE_POS_X = 240f;
        private const float HELP_PAUSE_POS_Y_STORY = 255f;
        private const float HELP_PAUSE_POS_Y_SURVIVAL = 285f;
        private Button m_buttonNext;
        public int m_pageIndex;
        private Transform m_t = new Transform();
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_menuTexture;
        private bool m_texturedBackground;

        public int GetPage() => this.m_pageIndex;

        public void ResetPageIndex() => this.m_pageIndex = 0;

        public HelpViewController()
        {
            this.m_buttonNext = new Button();
            this.m_pageIndex = 0;
            this.m_menuTexture = null;
            this.m_texturedBackground = false;
        }

        public void Initialize(bool texturedBackground)
        {
            base.Initialize();
            this.m_buttonNext.Initialize(this, 480f, 320f);
            this.m_buttonNext.GetTransform().SetPosition(240f, 180f);
            this.m_pageIndex = 0;
            this.m_texturedBackground = texturedBackground;
            Resource.SafeRelease(ref this.m_menuTexture);
            if (!this.m_texturedBackground)
                return;
            this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_ogm_help");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
        }

        public override void Initialize() => this.Initialize(false);

        public override void Cleanup()
        {
            this.m_buttonNext.Cleanup();
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
            if (this.m_texturedBackground)
                instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
            Color helpSelectedColor = GameConstant.MENU_TEXT_HELP_SELECTED_COLOR;
            Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
            switch (this.m_pageIndex)
            {
                case 0:
                    this.m_t.SetPosition(120f, 180f, 0.0f);
                    Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Page1));
                    break;
                case 1:
                    this.m_t.SetPosition(120f, 190f, 0.0f);
                    if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                    {
                        Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Page2Story));
                        break;
                    }
                    Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Page2));
                    break;
            }
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story || this.m_texturedBackground)
                this.m_t.SetPosition(240f, byte.MaxValue, 0.0f);
            else
                this.m_t.SetPosition(240f, 285f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PauseGame));
            this.m_t.SetPosition(100f, byte.MaxValue, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Life));
            this.m_t.SetPosition(45f, 230f, 0.0f);
            if (this.m_pageIndex == 0)
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Energy));
            else
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Energy));
            this.m_t.SetPosition(340f, 285f, 0.0f);
            if (this.m_pageIndex == 1)
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Heart));
            else
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Heart));
            this.m_t.SetPosition(470f, 210f, 0.0f);
            if (this.m_pageIndex == 1)
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_GunType));
            else
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_GunType));
            this.m_t.SetPosition(425f, 95f, 0.0f);
            if (this.m_pageIndex == 0)
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Fire));
            else
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Fire));
            this.m_t.SetPosition(345f, 95f, 0.0f);
            if (this.m_pageIndex == 0)
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, helpSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Slash));
            else
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Slash));
            this.m_t.SetPosition(120f, 85f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Move));
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || sender != this.m_buttonNext || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            ++this.m_pageIndex;
        }
    }
}
