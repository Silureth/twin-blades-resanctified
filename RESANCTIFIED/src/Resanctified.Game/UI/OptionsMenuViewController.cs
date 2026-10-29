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
    public class OptionsMenuViewController : ViewController
    {
        private const float OPTIONS_TITLE_X = 240f;
        private const float OPTIONS_TITLE_Y = 255f;
        private const float OPTIONS_TEXT_X = 210f;
        private const float OPTIONS_TEXT_MUSIC_Y = 220f;
        private const float OPTIONS_TEXT_OFFSET_Y = 43f;
        private const float OPTIONS_TEXT_SOUND_Y = 177f;
        private const float OPTIONS_TEXT_HUD_Y = 134f;
        private const float OPTIONS_TEXT_LANGUAGE_X = 230f;
        private const float OPTIONS_TEXT_LANGUAGE_Y = 91f;
        private const float OPTIONS_TEXT_SLASH_X = 290f;
        private const float OPTIONS_TEXT_ON_X = 280f;
        private const float OPTIONS_TEXT_OFF_X = 305f;
        private const float OPTIONS_BUTTON_ON_X = 260f;
        private const float OPTIONS_BUTTON_OFF_X = 325f;
        private const float OPTIONS_BUTTON_LANGUAGE_X = 280f;
        private const float OPTIONS_BUTTON_Y_OFFSET = -10f;
        private const float OPTIONS_BUTTON_WIDTH = 40f;
        private const float OPTIONS_BUTTON_HEIGHT = 30f;
        private const float OPTIONS_BUTTON_LANGUAGE_WIDTH = 100f;
        private const float OPTIONS_BUTTON_LANGUAGE_HEIGHT = 30f;
        private const float OPTIONS_SLIDER_WIDTH = 150f;
        private const float OPTIONS_SLIDER_RADIUS = 15f;
        private const float OPTIONS_SLIDER_X = 300f;
        private const float OPTIONS_SLIDER_Y_OFFSET = -12f;
        private const float OPTIONS_INGAME_OFFSET_Y = 0.0f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_menuTexture;
        private bool m_texturedBackground;
        private bool m_disableMusicButton;
        private Transform m_t = new Transform();
        private Font m_font;
        private Button m_buttonLanguage;
        private Button m_buttonBack;
        private CustomSlider m_sliderSfx;
        private CustomSlider m_sliderMusic;
        private CustomSlider m_sliderHUD;

        public OptionsMenuViewController()
        {
            this.m_menuTexture = null;
            this.m_texturedBackground = false;
            this.m_disableMusicButton = false;
            this.m_buttonLanguage = new Button();
            this.m_buttonBack = new Button();
            this.m_sliderSfx = new CustomSlider();
            this.m_sliderMusic = new CustomSlider();
            this.m_sliderHUD = new CustomSlider();
        }

        public override void Initialize() => this.Initialize(false);

        public void Initialize(bool texturedBackground)
        {
            float num = 0.0f;
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
            else
                num = 0.0f;
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            this.m_sliderSfx.Initialize(this, 150f, 15f, SliderOrientation.Horizontal);
            this.m_sliderSfx.GetTransform().SetPosition(300f, 165f + num);
            this.m_sliderMusic.Initialize(this, 150f, 15f, SliderOrientation.Horizontal);
            this.m_sliderMusic.GetTransform().SetPosition(300f, 208f + num);
            this.m_sliderMusic.SetActive(!this.m_disableMusicButton);
            this.m_sliderHUD.Initialize(this, 150f, 15f, SliderOrientation.Horizontal);
            this.m_sliderHUD.GetTransform().SetPosition(300f, 122f + num);
            this.m_buttonLanguage.Initialize(this, 100f, 30f);
            this.m_buttonLanguage.GetTransform().SetPosition(280f, 81f + num);
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
            this.m_sliderSfx.Cleanup();
            this.m_sliderMusic.Cleanup();
            this.m_sliderHUD.Cleanup();
            this.m_buttonLanguage.Cleanup();
            this.m_buttonBack.Cleanup();
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
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
            Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
            Color textSelectedColor = GameConstant.MENU_TEXT_SELECTED_COLOR;
            this.m_t.SetScale(1f);
            float num = 0.0f;
            if (this.m_texturedBackground)
            {
                instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
                this.m_t.SetPosition(240f, byte.MaxValue, 0.0f);
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SettingsBig));
                this.m_t.SetPosition(36f, 40f, 0.0f);
                instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            }
            else
            {
                instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
                this.m_t.SetPosition(30f, 40f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
                num = 0.0f;
            }
            if (this.m_disableMusicButton)
            {
                this.m_t.SetPosition(210f, 220f + num, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Music));
            }
            else
            {
                this.m_t.SetPosition(210f, 220f + num, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Music));
            }
            this.m_sliderMusic.SetNormalizedPosition(GameApplication.GetInstance().MusicGain);
            this.m_sliderMusic.Draw();
            this.m_t.SetPosition(210f, 177f + num, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Sound));
            this.m_sliderSfx.SetNormalizedPosition(GameApplication.GetInstance().SfxGain);
            this.m_sliderSfx.Draw();
            this.m_t.SetPosition(210f, 134f + num, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HUD));
            this.m_sliderHUD.SetNormalizedPosition(GameApplication.GetInstance().HudAlpha);
            this.m_sliderHUD.Draw();
            this.m_t.SetPosition(210f, 91f + num, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, menuTextColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Language));
            this.m_t.SetPosition(290f, 91f + num, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, textSelectedColor, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Language_Name));
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonBack)
            {
                if (((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
                {
                    if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                        MainMenuStateOptions.GetInstance().BackRequest = true;
                    else
                        InGameMenuStateOptions.GetInstance().BackRequest = true;
                }
            }
            else if (sender == this.m_buttonLanguage && ((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
            {
                if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    MainMenuStateOptions.GetInstance().LanguageSelectionRequest = true;
                else
                    InGameMenuStateOptions.GetInstance().LanguageSelectionRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            if (sender == this.m_sliderSfx)
            {
                SliderActionEvent sliderActionEvent = (SliderActionEvent)evt;
                GameApplication.GetInstance().SfxGain = sliderActionEvent.normalizedPosition;
                GameApplication.GetInstance().SetSoundVolume();
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Select);
            }
            if (sender == this.m_sliderMusic)
            {
                SliderActionEvent sliderActionEvent = (SliderActionEvent)evt;
                GameApplication.GetInstance().MusicGain = sliderActionEvent.normalizedPosition;
                GameApplication.GetInstance().SetSoundVolume();
            }
            if (sender != this.m_sliderHUD)
                return;
            SliderActionEvent sliderActionEvent1 = (SliderActionEvent)evt;
            GameApplication.GetInstance().HudAlpha = sliderActionEvent1.normalizedPosition;
        }
    }
}
