using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM;
using Resanctified.Game.FSM.GameState;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.FSM.GameState.MainMenu;

namespace Resanctified.Game.UI
{
    public class HighscoresChooseMenuViewController : ViewController
    {
        private const float TITLE_X = 240f;
        private const float TITLE_Y = 250f;
        private const float TEXT_X = 240f;
        private const float TEXT_STORY_Y = 180f;
        private const float TEXT_SURVIVAL_Y = 130f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_menuTexture;
        private Button m_buttonBack;
        private Button m_buttonStory;
        private Button m_buttonSurvival;
        private Transform m_t = new Transform();
        private Font m_font;
        private bool m_texturedBackground;

        public HighscoresChooseMenuViewController()
        {
            this.m_texturedBackground = false;
            this.m_menuTexture = null;
            this.m_buttonBack = new Button();
            this.m_buttonStory = new Button();
            this.m_buttonSurvival = new Button();
        }

        public override void Initialize() => this.Initialize(false);

        public void Initialize(bool texturedBackground)
        {
            this.m_texturedBackground = texturedBackground;
            base.Initialize();
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
            if (this.m_texturedBackground)
            {
                this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
                this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
                QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
                QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            }
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            if (this.m_texturedBackground)
            {
                this.m_buttonBack.Initialize(this, 56f, 56f);
                this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
            }
            else
            {
                this.m_buttonBack.Initialize(this, 100f, 56f);
                this.m_buttonBack.GetTransform().SetPosition(30f, 40f);
            }
            this.m_buttonStory.Initialize(this, 200f, 30f);
            this.m_buttonStory.GetTransform().SetPosition(240f, 170f);
            this.m_buttonSurvival.Initialize(this, 200f, 30f);
            this.m_buttonSurvival.GetTransform().SetPosition(240f, 120f);
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
            this.m_buttonBack.Cleanup();
            this.m_buttonStory.Cleanup();
            this.m_buttonSurvival.Cleanup();
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
            int currentLanguage = (int)GameApplication.GetInstance().CurrentLanguage;
            Font font = GameApplication.GetInstance().GetFont();
            Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
            if (this.m_texturedBackground)
            {
                instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
                this.m_t.SetPosition(36f, 40f, 0.0f);
                instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
                this.m_t.SetPosition(240f, 250f);
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HighscoresBig));
            }
            else
            {
                instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
                this.m_t.SetPosition(240f, 250f);
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HighscoresBig));
                this.m_t.SetPosition(30f, 40f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
            }
            this.m_t.SetPosition(240f, 180f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryMode));
            this.m_t.SetPosition(240f, 130f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SurvivalMode));
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonBack)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    MainMenuStateHighscoresChoose.GetInstance().BackRequest = true;
                else
                    InGameMenuStateHighscoresChoose.GetInstance().BackRequest = true;
            }
            else if (sender == this.m_buttonStory)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    MainMenuStateHighscoresChoose.GetInstance().StoryRequest = true;
                else
                    InGameMenuStateHighscoresChoose.GetInstance().StoryRequest = true;
            }
            else
            {
                if (sender != this.m_buttonSurvival || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    MainMenuStateHighscoresChoose.GetInstance().SurvivalRequest = true;
                else
                    InGameMenuStateHighscoresChoose.GetInstance().SurvivalRequest = true;
            }
        }
    }
}
