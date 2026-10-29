// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.UI.MainMenuViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.FSM.GameState.MainMenu;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using System.Text;

namespace Resanctified.Game.UI
{
    public class MainMenuViewController : ViewController
    {
        public const float MENU_ITEM_SCALE_SELECTED = 1.2f;
        public const float MENU_ITEM_SCALE_UNSELECTED = 1f;
        public const float MENU_BG_TOP_X = 0.0f;
        public const float MENU_BG_TOP_Y = 0.0f;
        public const float MENU_BG_WIDTH = 480f;
        public const float MENU_BG_HEIGHT = 320f;
        public const float MENU_TEXTURE_WIDTH = 512f;
        public const float MENU_TEXTURE_HEIGHT = 512f;
        public const float MENU_BUTTON_BACK_X = 36f;
        public const float MENU_BUTTON_BACK_Y = 40f;
        public const float MENU_BUTTON_BACK_WIDTH = 56f;
        public const float MENU_BUTTON_BACK_HEIGHT = 56f;
        public const float MENU_BUTTON_BACK_TOP_X = 89f;
        public const float MENU_BUTTON_BACK_TOP_Y = 452f;
        public const float MENU_BUTTON_OK_X = 440f;
        public const float MENU_BUTTON_OK_Y = 40f;
        public const float MENU_BUTTON_OK_WIDTH = 56f;
        public const float MENU_BUTTON_OK_HEIGHT = 56f;
        public const float MENU_BUTTON_OK_TOP_X = 5f;
        public const float MENU_BUTTON_OK_TOP_Y = 452f;
        public const float MENU_BUTTON_NEXT_X = 440f;
        public const float MENU_BUTTON_NEXT_Y = 40f;
        public const float MENU_BUTTON_NEXT_WIDTH = 56f;
        public const float MENU_BUTTON_NEXT_HEIGHT = 56f;
        public const float MENU_BUTTON_NEXT_TOP_X = 8f;
        public const float MENU_BUTTON_NEXT_TOP_Y = 331f;
        public const float TITLE_OPTIONS_TOP_X = 75f;
        public const float TITLE_OPTIONS_TOP_Y = 386f;
        public const float TITLE_OPTIONS_WIDTH = 160f;
        public const float TITLE_OPTIONS_HEIGHT = 58f;
        public const float TITLE_HIGHSCORES_TOP_X = 182f;
        public const float TITLE_HIGHSCORES_TOP_Y = 446f;
        public const float TITLE_HIGHSCORES_WIDTH = 222f;
        public const float TITLE_HIGHSCORES_HEIGHT = 60f;

        public const float TITLE_STORY_LEFT = 290f;
        public const float TITLE_STORY_TOP = 326f;
        public const float TITLE_STORY_WIDTH = 115f;
        public const float TITLE_STORY_HEIGHT = 58f;

        public const float TITLE_SURVIVAL_LEFT = 242f;
        public const float TITLE_SURVIVAL_TOP = 386f;
        public const float TITLE_SURVIVAL_WIDTH = 161f;
        public const float TITLE_SURVIVAL_HEIGHT = 58f;
        public const float MENU_BUTTON_BACK_TEXT_X = 30f;
        public const float MENU_BUTTON_BACK_TEXT_Y = 40f;
        public const float MENU_BUTTON_BACK_TEXT_WIDTH = 100f;
        public const float MENU_BUTTON_BACK_TEXT_HEIGHT = 56f;
        public const float MENU_BUTTON_OK_TEXT_X = 440f;
        public const float MENU_BUTTON_OK_TEXT_Y = 40f;
        public const float MENU_BUTTON_OK_TEXT_WIDTH = 100f;
        public const float MENU_BUTTON_OK_TEXT_HEIGHT = 56f;
        public const float MAINMENU_TEXT_X = 150f;
        public const float MAINMENU_TEXT_STORY_Y = 280f;
        public const float MAINMENU_TEXT_OFFSET_Y = 40f;
        public const float MAINMENU_TEXT_SURVIVAL_Y = 240f;
        public const float MAINMENU_TEXT_ACHIEVEMENTS_Y = 200f;
        public const float MAINMENU_TEXT_HIGHSCORES_Y = 160f;
        public const float MAINMENU_TEXT_OPTIONS_Y = 120f;
        public const float MAINMENU_TEXT_EXIT1_Y = 80f;
        public const float MAINMENU_TEXT_UNLOCK_Y = 80f;
        public const float MAINMENU_TEXT_EXIT2_Y = 40f;
        public const float MAINMENU_BUTTON_X = 150f;
        public const float MAINMENU_BUTTON_Y_OFFSET = -10f;
        public const float MAINMENU_BUTTON_WIDTH = 200f;
        public const float MAINMENU_BUTTON_HEIGHT = 30f;
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadTitle = new VertexPositionTexture[4];
        private Resanctified.Engine.Resources.Texture m_splashTexture;
        private Button m_buttonStory;
        private Button m_buttonSurvival;
        private Button m_buttonOptions;
        private Button m_buttonHighscores;
        private Button m_buttonAchievements;
        private Button m_buttonUnlock;
        private Button m_buttonExit;
        private Transform m_t = new Transform();
        private string m_version;
        private StringBuilder m_stringBuilder = new StringBuilder(256);

        public MainMenuViewController()
        {
            this.m_splashTexture = (Resanctified.Engine.Resources.Texture)null;
            this.m_buttonStory = new Button();
            this.m_buttonSurvival = new Button();
            this.m_buttonOptions = new Button();
            this.m_buttonHighscores = new Button();
            this.m_buttonAchievements = new Button();
            this.m_buttonUnlock = new Button();
            this.m_buttonExit = new Button();
            this.m_version = "v. " + GameApplication.GetInstance().GetAssemblyVersionString();
        }

        public override void Initialize()
        {
            base.Initialize();
            Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_splashTexture);
            this.m_splashTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_splash");

            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 192f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            QuadFactory.CreateAnchoredQuad(this.m_quadTitle, 0.0f, 0.0f, 170f, 160f, 512f, 512f, Anchor.HCenter);
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);

            this.m_buttonStory.Initialize((ViewController)this, 200f, 30f);
            //            this.m_buttonStory.GetTransform().SetPosition(Application.Instance.ConvertPositionX(150f), Application.Instance.ConvertPositionY(270f));
            this.m_buttonStory.GetTransform().SetPosition(150f, 270f);

            this.m_buttonSurvival.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonSurvival.GetTransform().SetPosition(150f, 230f);

            this.m_buttonAchievements.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonAchievements.GetTransform().SetPosition(150f, 190f);

            this.m_buttonHighscores.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonHighscores.GetTransform().SetPosition(150f, 150f);

            this.m_buttonOptions.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonOptions.GetTransform().SetPosition(150f, 110f);



            this.m_buttonUnlock.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonUnlock.GetTransform().SetPosition(150f, 70f);

            this.m_buttonExit.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonExit.GetTransform().SetPosition(150f, 30f);
        }

        public override void Cleanup()
        {
            this.m_buttonStory.Cleanup();
            this.m_buttonSurvival.Cleanup();
            this.m_buttonOptions.Cleanup();
            this.m_buttonHighscores.Cleanup();
            this.m_buttonAchievements.Cleanup();
            this.m_buttonUnlock.Cleanup();
            this.m_buttonExit.Cleanup();
            Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_splashTexture);
            base.Cleanup();
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform)null, this.m_splashTexture);
            instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform)null, (Resanctified.Engine.Resources.Texture)null, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
            this.m_t.SetPosition(395f, 320f, 0.0f);
            this.m_t.SetScale(1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadTitle, VertexFormat.Position3Texture, this.m_t, this.m_splashTexture);
            Font font = GameApplication.GetInstance().GetFont();
            this.m_t.SetPosition(150f, 280f, 0.0f);
            instance.DrawString(font, this.m_t, 4, GameConstant.SHIELD_FIRE_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryMode));//HERE!!
            this.m_t.SetPosition(150f, 240f, 0.0f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SurvivalMode));
            this.m_t.SetPosition(150f, 120f, 0.0f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HelpAndOptions));
            this.m_t.SetPosition(150f, 160f, 0.0f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Highscores));
            this.m_t.SetPosition(150f, 200f, 0.0f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievements));
            if (GuideManager.Instance.IsTrialMode())
            {
                Color color = GameConstant.MENU_TEXT_COLOR;
                if (!GuideManager.Instance.IsPurchaseAvailable())
                    color = GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR;
                this.m_t.SetPosition(150f, 80f, 0.0f);
                instance.DrawString(font, this.m_t, 4, color, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.UnlockFullGame));
                this.m_t.SetPosition(150f, 40f, 0.0f);
                instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Exit));
            }
            else
            {
                this.m_t.SetPosition(150f, 80f, 0.0f);
                instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Exit));
            }
            this.m_t.SetPosition(0.0f, 0.0f, 0.0f);
            this.m_t.SetScale(0.8f);
            instance.DrawString(font, this.m_t, 2, GameConstant.MENU_TEXT_COLOR, this.m_version);
            instance.End2DMode();

        }

        public override void Update(float elapsedTime)
        {
            if (!this.m_active)
                return;
            base.Update(elapsedTime);
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonStory)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                GameApplication.GetInstance().SetGameMode(GameApplication.GameMode.Story);
                MainMenuStateMenu.GetInstance().StartGameRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_buttonSurvival)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                GameApplication.GetInstance().SetGameMode(GameApplication.GameMode.Survival);
                MainMenuStateMenu.GetInstance().StartGameRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_buttonAchievements)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                MainMenuStateMenu.GetInstance().AchievementsRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_buttonHighscores)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                MainMenuStateMenu.GetInstance().HighscoresRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_buttonOptions)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                MainMenuStateMenu.GetInstance().OptionsRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_buttonUnlock)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                if (GuideManager.Instance.IsTrialMode())
                {
                    if (!GuideManager.Instance.IsPurchaseAvailable())
                        return;
                    MainMenuStateMenu.GetInstance().UnlockRequest = true;
                    GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                }
                else
                {
                    MainMenuStateMenu.GetInstance().ExitRequest = true;
                    GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                }
            }
            else
            {
                if (sender != this.m_buttonExit || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed || !GuideManager.Instance.IsTrialMode())
                    return;
                MainMenuStateMenu.GetInstance().ExitRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
        }
    }
}
