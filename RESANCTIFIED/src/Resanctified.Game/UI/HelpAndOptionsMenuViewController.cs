using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
    public class HelpAndOptionsMenuViewController : ViewController
    {
        private const float TITLE_X = 240f;
        private const float TITLE_Y = 250f;
        private const float TEXT_X = 240f;
        private const float TEXT_HELP_Y = 205f;
        private const float TEXT_KEYBOARD_Y = 170f;
        private const float TEXT_SETTINGS_Y = 135f;
        private const float TEXT_CREDITS_Y = 100f;
        private const float TEXT_ABOUT_Y = 65f;
        // Item text hangs below its y; the touch area is centered on the text line.
        private const float BUTTON_OFFSET_Y = -10f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_menuTexture;
        private Button m_buttonBack;
        private Button m_buttonHelp;
        private Button m_buttonKeyboard;
        private Button m_buttonSettings;
        private Button m_buttonCredits;
        private Button m_buttonAbout;
        private Transform m_t = new Transform();
        private Font m_font;
        private bool m_texturedBackground;

        public HelpAndOptionsMenuViewController()
        {
            this.m_texturedBackground = false;
            this.m_menuTexture = null;
            this.m_buttonBack = new Button();
            this.m_buttonHelp = new Button();
            this.m_buttonKeyboard = new Button();
            this.m_buttonSettings = new Button();
            this.m_buttonCredits = new Button();
            this.m_buttonAbout = new Button();
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
                this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
                this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
                QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
                QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            }
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            this.m_buttonHelp.Initialize(this, 200f, 30f);
            this.m_buttonHelp.GetTransform().SetPosition(TEXT_X, TEXT_HELP_Y + BUTTON_OFFSET_Y);
            this.m_buttonKeyboard.Initialize(this, 200f, 30f);
            this.m_buttonKeyboard.GetTransform().SetPosition(TEXT_X, TEXT_KEYBOARD_Y + BUTTON_OFFSET_Y);
            this.m_buttonSettings.Initialize(this, 200f, 30f);
            this.m_buttonSettings.GetTransform().SetPosition(TEXT_X, TEXT_SETTINGS_Y + BUTTON_OFFSET_Y);
            this.m_buttonCredits.Initialize(this, 200f, 30f);
            this.m_buttonCredits.GetTransform().SetPosition(TEXT_X, TEXT_CREDITS_Y + BUTTON_OFFSET_Y);
            this.m_buttonAbout.Initialize(this, 200f, 30f);
            this.m_buttonAbout.GetTransform().SetPosition(TEXT_X, TEXT_ABOUT_Y + BUTTON_OFFSET_Y);
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
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
            this.m_buttonBack.Cleanup();
            this.m_buttonHelp.Cleanup();
            this.m_buttonKeyboard.Cleanup();
            this.m_buttonSettings.Cleanup();
            this.m_buttonCredits.Cleanup();
            this.m_buttonAbout.Cleanup();
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
            if (this.m_texturedBackground)
            {
                instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
                this.m_t.SetPosition(240f, 250f);
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.OptionsBig));
                this.m_t.SetPosition(36f, 40f, 0.0f);
                instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            }
            else
            {
                instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
                this.m_t.SetPosition(240f, 250f);
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HelpAndOptions));
                this.m_t.SetPosition(30f, 40f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
            }
            Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
            this.m_t.SetPosition(TEXT_X, TEXT_HELP_Y);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help));
            this.m_t.SetPosition(TEXT_X, TEXT_KEYBOARD_Y);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Keyboard));
            this.m_t.SetPosition(TEXT_X, TEXT_SETTINGS_Y);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Settings));
            if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
            {
                this.m_t.SetPosition(TEXT_X, TEXT_CREDITS_Y);
                instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Credits));
                this.m_t.SetPosition(TEXT_X, TEXT_ABOUT_Y);
                instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.About));
            }
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
                    MainMenuStateHelpAndOptions.GetInstance().BackRequest = true;
                else
                    InGameMenuStateHelpAndOptions.GetInstance().BackRequest = true;
            }
            else if (sender == this.m_buttonHelp)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStateReleased)
                    return;
                if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    MainMenuStateHelpAndOptions.GetInstance().HelpRequest = true;
                else
                    InGameMenuStateHelpAndOptions.GetInstance().HelpRequest = true;
            }
            else if (sender == this.m_buttonKeyboard)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStateReleased)
                    return;
                if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    MainMenuStateHelpAndOptions.GetInstance().KeyboardRequest = true;
                else
                    InGameMenuStateHelpAndOptions.GetInstance().KeyboardRequest = true;
            }
            else if (sender == this.m_buttonSettings)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStateReleased)
                    return;
                if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    MainMenuStateHelpAndOptions.GetInstance().SettingsRequest = true;
                else
                    InGameMenuStateHelpAndOptions.GetInstance().SettingsRequest = true;
            }
            else if (sender == this.m_buttonCredits)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStateReleased || GameFSM.GetInstance().GetCurrentState() != GameStateMainMenu.GetInstance())
                    return;
                MainMenuStateHelpAndOptions.GetInstance().CreditsRequest = true;
            }
            else
            {
                if (sender != this.m_buttonAbout || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStateReleased || GameFSM.GetInstance().GetCurrentState() != GameStateMainMenu.GetInstance())
                    return;
                MainMenuStateHelpAndOptions.GetInstance().AboutRequest = true;
            }
        }
    }
}
