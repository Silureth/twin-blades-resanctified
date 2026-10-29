using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Tools;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM;
using Resanctified.Game.FSM.GameState;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.FSM.GameState.MainMenu;

namespace Resanctified.Game.UI
{
    public class HighscoresMenuViewController : ViewController
    {
        private const float HIGHSCORES_TITLE_X = 240f;
        private const float HIGHSCORES_TITLE_Y = 250f;
        private const float HIGHSCORES_SUB_TITLE_X = 240f;
        private const float HIGHSCORES_SUB_TITLE_Y = 205f;
        private const float HIGHSCORES_TEXT_X = 240f;
        private const float HIGHSCORES_TEXT_1_Y = 185f;
        private const float HIGHSCORES_TEXT_OFFSET_Y = 25f;
        private const float HIGHSCORES_TEXT_2_Y = 160f;
        private const float HIGHSCORES_TEXT_3_Y = 135f;
        private const float HIGHSCORES_TEXT_4_Y = 110f;
        private const float HIGHSCORES_TEXT_5_Y = 85f;
        private const float HIGHSCORES_TEXT_6_Y = 60f;
        private const float SCORE_BLINK_TIME = 0.1f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Button m_buttonOk;
        private Button m_buttonBack;
        private int m_indexToBlink;
        private float m_timer;
        private bool m_scoreIsVisible;
        private Engine.Resources.Texture m_menuTexture;
        private Transform m_t = new Transform();
        private Font m_font;
        private bool m_texturedBackground;
        private StringBuilder m_stringBuilder = new StringBuilder(16);
        public bool StoryMode;

        public int GetBlinkIndex() => this.m_indexToBlink;

        public HighscoresMenuViewController()
        {
            this.StoryMode = true;
            this.m_texturedBackground = false;
            this.m_menuTexture = null;
            this.m_indexToBlink = 0;
            this.m_timer = 0.0f;
            this.m_scoreIsVisible = true;
            this.m_buttonOk = new Button();
            this.m_buttonBack = new Button();
        }

        public override void Initialize() => this.Initialize(0, false);

        public void Initialize(int indexToBlink, bool texturedBackground)
        {
            this.StoryMode = true;
            this.m_texturedBackground = texturedBackground;
            this.m_indexToBlink = indexToBlink;
            this.m_timer = 0.0f;
            base.Initialize();
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
            if (this.m_texturedBackground)
            {
                this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
                this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
                QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
                QuadFactory.CreateAnchoredQuad(this.m_quadOk, 5f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
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
            this.m_buttonOk.Initialize(this, 56f, 56f);
            this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
        }

        public override void Cleanup()
        {
            this.m_buttonOk.Cleanup();
            this.m_buttonBack.Cleanup();
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
            Application.GetInstance().RemoveResponder(this);
            base.Cleanup();
        }

        public override void Update(float elapsedTime)
        {
            this.m_timer -= elapsedTime;
            if (this.m_timer < 0.0)
            {
                this.m_timer = 0.1f;
                this.m_scoreIsVisible = !this.m_scoreIsVisible;
            }
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
            this.m_t.SetScale(1f);
            if (this.m_texturedBackground)
            {
                instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
                this.m_t.SetPosition(240f, 250f, 0.0f);
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HighscoresBig));
                if (this.m_indexToBlink == 0)
                {
                    this.m_t.SetPosition(36f, 40f, 0.0f);
                    instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
                }
                else
                {
                    this.m_t.SetPosition(440f, 40f, 0.0f);
                    instance.DrawQuad(this.m_quadOk, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
                }
            }
            else
            {
                instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
                this.m_t.SetPosition(240f, 250f, 0.0f);
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HighscoresBig));
                this.m_t.SetPosition(30f, 40f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
            }
            this.m_t.SetScale(1f);
            this.m_t.SetPosition(240f, 205f, 0.0f);
            int[] numArray;
            if (this.StoryMode)
            {
                numArray = GameApplication.GetInstance().GetHighscoresStory();
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryModeBig));
            }
            else
            {
                numArray = GameApplication.GetInstance().GetHighscoresSurvival();
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SurvivalModeBig));
            }
            if (this.m_indexToBlink == 1)
            {
                if (this.m_scoreIsVisible)
                {
                    this.m_stringBuilder.Length = 0;
                    StringHelper.Append(this.m_stringBuilder, numArray[0]);
                    this.m_t.SetPosition(240f, 185f, 0.0f);
                    Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_SELECTED_COLOR, this.m_stringBuilder);
                }
            }
            else
            {
                this.m_stringBuilder.Length = 0;
                StringHelper.Append(this.m_stringBuilder, numArray[0]);
                this.m_t.SetPosition(240f, 185f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
            }
            if (this.m_indexToBlink == 2)
            {
                if (this.m_scoreIsVisible)
                {
                    this.m_stringBuilder.Length = 0;
                    StringHelper.Append(this.m_stringBuilder, numArray[1]);
                    this.m_t.SetPosition(240f, 160f, 0.0f);
                    Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_SELECTED_COLOR, this.m_stringBuilder);
                }
            }
            else
            {
                this.m_stringBuilder.Length = 0;
                StringHelper.Append(this.m_stringBuilder, numArray[1]);
                this.m_t.SetPosition(240f, 160f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
            }
            if (this.m_indexToBlink == 3)
            {
                if (this.m_scoreIsVisible)
                {
                    this.m_stringBuilder.Length = 0;
                    StringHelper.Append(this.m_stringBuilder, numArray[2]);
                    this.m_t.SetPosition(240f, 135f, 0.0f);
                    Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_SELECTED_COLOR, this.m_stringBuilder);
                }
            }
            else
            {
                this.m_stringBuilder.Length = 0;
                StringHelper.Append(this.m_stringBuilder, numArray[2]);
                this.m_t.SetPosition(240f, 135f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
            }
            if (this.m_indexToBlink == 4)
            {
                if (this.m_scoreIsVisible)
                {
                    this.m_stringBuilder.Length = 0;
                    StringHelper.Append(this.m_stringBuilder, numArray[3]);
                    this.m_t.SetPosition(240f, 110f, 0.0f);
                    Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_SELECTED_COLOR, this.m_stringBuilder);
                }
            }
            else
            {
                this.m_stringBuilder.Length = 0;
                StringHelper.Append(this.m_stringBuilder, numArray[3]);
                this.m_t.SetPosition(240f, 110f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
            }
            if (this.m_indexToBlink == 5)
            {
                if (this.m_scoreIsVisible)
                {
                    this.m_stringBuilder.Length = 0;
                    StringHelper.Append(this.m_stringBuilder, numArray[4]);
                    this.m_t.SetPosition(240f, 85f, 0.0f);
                    Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_SELECTED_COLOR, this.m_stringBuilder);
                }
            }
            else
            {
                this.m_stringBuilder.Length = 0;
                StringHelper.Append(this.m_stringBuilder, numArray[4]);
                this.m_t.SetPosition(240f, 85f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
            }
            if (this.m_indexToBlink == 6 && this.m_scoreIsVisible)
            {
                this.m_stringBuilder.Length = 0;
                StringHelper.Append(this.m_stringBuilder, GameApplication.GetInstance().Score);
                this.m_t.SetPosition(240f, 60f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, this.m_stringBuilder);
            }
            instance.End2DMode();
        }

        public void BlinkScore(int indexToBlink)
        {
            this.m_indexToBlink = indexToBlink;
            this.m_timer = 0.0f;
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonOk)
            {
                if (this.m_indexToBlink == 0)
                    return;
                if (((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
                {
                    MainMenuStateHighscoresStory.GetInstance().BackRequest = true;
                    MainMenuStateHighscoresSurvival.GetInstance().BackRequest = true;
                }
            }
            if (sender == this.m_buttonBack)
            {
                if (this.m_indexToBlink != 0)
                    return;
                if (((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
                {
                    if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    {
                        MainMenuStateHighscoresStory.GetInstance().BackRequest = true;
                        MainMenuStateHighscoresSurvival.GetInstance().BackRequest = true;
                    }
                    else
                    {
                        InGameMenuStateHighscoresStory.GetInstance().BackRequest = true;
                        InGameMenuStateHighscoresSurvival.GetInstance().BackRequest = true;
                    }
                }
            }
        }
    }
}
