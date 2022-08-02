// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.LeaderboardMenuViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM;
using PSS.Game.NunVsZombies.FSM.GameState;
using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.Game.NunVsZombies.FSM.GameState.MainMenu;
using PSS.GameEngine;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Tools;
using PSS.GameEngine.UI;
using System;
using System.Text;

namespace PSS.Game.NunVsZombies.UI
{
    public class LeaderboardMenuViewController : ViewController
    {
        private const float HIGHSCORES_TITLE_X = 240f;
        private const float HIGHSCORES_TITLE_Y = 250f;
        private const float HIGHSCORES_SUB_TITLE_X = 240f;
        private const float HIGHSCORES_SUB_TITLE_Y = 205f;
        private const float HIGHSCORES_TEXT_X = 240f;
        private const float HIGHSCORES_TEXT_RANK_X = 95f;
        private const float HIGHSCORES_TEXT_GAMERTAG_X = 120f;
        private const float HIGHSCORES_TEXT_SCORE_X = 380f;
        private const float HIGHSCORES_TEXT_RANGE_Y = 185f;
        private const float HIGHSCORES_TEXT_1_Y = 170f;
        private const float HIGHSCORES_TEXT_OFFSET_Y = 25f;
        private const float HIGHSCORES_TEXT_2_Y = 145f;
        private const float HIGHSCORES_TEXT_3_Y = 120f;
        private const float HIGHSCORES_TEXT_4_Y = 95f;
        private const float HIGHSCORES_TEXT_5_Y = 70f;
        private const float SCORE_BLINK_TIME = 0.1f;
        private const float HIGHSCORES_BUTTON_PREVIOUS_X = 46f;
        private const float HIGHSCORES_BUTTON_PREVIOUS_Y = 205f;
        private const float HIGHSCORES_BUTTON_NEXT_X = 426f;
        private const float HIGHSCORES_BUTTON_NEXT_Y = 205f;
        private const float HIGHSCORES_BUTTON_UP_X = 421f;
        private const float HIGHSCORES_BUTTON_UP_Y = 145f;
        private const float HIGHSCORES_BUTTON_DOWN_X = 421f;
        private const float HIGHSCORES_BUTTON_DOWN_Y = 100f;
        private const float HIGHSCORES_BUTTON_UP_WIDTH = 40f;
        private const float HIGHSCORES_BUTTON_UP_HEIGHT = 40f;
        private const float HIGHSCORES_TEXT_UPDOWN_X = 411f;
        private const float HIGHSCORES_TEXT_UP_Y = 130f;
        private const float HIGHSCORES_TEXT_DOWN_Y = 80f;
        private const float READING_TIME = 0.3f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadNext = new VertexPositionTexture[4];
        private Button m_buttonOk;
        private Button m_buttonBack;
        private Button m_buttonNext;
        private Button m_buttonPrevious;
        private Button m_buttonUp;
        private Button m_buttonDown;
        private bool m_blink;
        private float m_timer;
        private bool m_scoreIsVisible;
        private PSS.GameEngine.Resources.Texture m_menuTexture;
        private Transform m_t = new Transform();
        private Font m_font;
        private bool m_texturedBackground;
        private StringBuilder m_stringBuilder = new StringBuilder(16);
        public bool StoryMode;
        private bool m_reading;
        private float m_readingTimer;
        private int m_readingState;
        private bool m_weeklyPage;

        public LeaderboardMenuViewController()
        {
            this.StoryMode = true;
            this.m_weeklyPage = false;
            this.m_texturedBackground = false;
            this.m_menuTexture = (PSS.GameEngine.Resources.Texture)null;
            this.m_blink = false;
            this.m_timer = 0.0f;
            this.m_scoreIsVisible = true;
            this.m_reading = false;
            this.m_readingTimer = 0.0f;
            this.m_readingState = 0;
            this.m_buttonOk = new Button();
            this.m_buttonBack = new Button();
            this.m_buttonNext = new Button();
            this.m_buttonPrevious = new Button();
            this.m_buttonUp = new Button();
            this.m_buttonDown = new Button();
        }

        public override void Initialize() => this.Initialize(false);

        public void Initialize(bool texturedBackground)
        {
            this.StoryMode = true;
            this.m_weeklyPage = false;
            this.m_texturedBackground = texturedBackground;
            this.m_blink = false;
            this.m_timer = 0.0f;
            this.m_reading = false;
            this.m_readingTimer = 0.0f;
            this.m_readingState = 0;
            base.Initialize();
            GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
            GameEngine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
            if (this.m_texturedBackground)
            {
                this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
                this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
                QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
                QuadFactory.CreateAnchoredQuad(this.m_quadOk, 5f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
                QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
                QuadFactory.CreateAnchoredQuad(this.m_quadNext, 8f, 331f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            }
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            if (this.m_texturedBackground)
            {
                this.m_buttonBack.Initialize((ViewController)this, 56f, 56f);
                this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
            }
            else
            {
                this.m_buttonBack.Initialize((ViewController)this, 100f, 56f);
                this.m_buttonBack.GetTransform().SetPosition(30f, 40f);
            }
            this.m_buttonNext.Initialize((ViewController)this, 56f, 56f);
            this.m_buttonNext.GetTransform().SetPosition(426f, 205f);
            this.m_buttonPrevious.Initialize((ViewController)this, 56f, 56f);
            this.m_buttonPrevious.GetTransform().SetPosition(46f, 205f);
            this.m_buttonOk.Initialize((ViewController)this, 56f, 56f);
            this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
            this.m_buttonUp.Initialize((ViewController)this, 40f, 40f);
            this.m_buttonUp.GetTransform().SetPosition(421f, 145f);
            this.m_buttonDown.Initialize((ViewController)this, 40f, 40f);
            this.m_buttonDown.GetTransform().SetPosition(421f, 100f);
            //LeaderboardManager.Instance.BestScoresReadEvent += new EventHandler(this.LeaderboardManager_BestScoresReadEvent);
            //LeaderboardManager.Instance.LeaderboardTimeOutEvent += new EventHandler(this.LeaderboardManager_LeaderboardTimeOutEvent);
        }

        public override void Cleanup()
        {
            //LeaderboardManager.Instance.BestScoresReadEvent -= new EventHandler(this.LeaderboardManager_BestScoresReadEvent);
            //LeaderboardManager.Instance.LeaderboardTimeOutEvent -= new EventHandler(this.LeaderboardManager_LeaderboardTimeOutEvent);
            this.m_buttonOk.Cleanup();
            this.m_buttonBack.Cleanup();
            this.m_buttonNext.Cleanup();
            this.m_buttonPrevious.Cleanup();
            GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
            GameEngine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
            Application.GetInstance().RemoveResponder((Responder)this);
            base.Cleanup();
        }

        public void ReadLeaderboard(int table, bool weekly)
        {
            this.m_weeklyPage = weekly;
           // LeaderboardManager.Instance.ReadBestScores(table, weekly);
            this.m_reading = true;
        }

        private void LeaderboardManager_LeaderboardTimeOutEvent(object sender, EventArgs e) => this.m_reading = false;

        private void LeaderboardManager_BestScoresReadEvent(object sender, EventArgs e) => this.m_reading = false;

        public override void Update(float elapsedTime)
        {
            if (this.m_blink)
            {
                this.m_timer -= elapsedTime;
                if ((double)this.m_timer < 0.0)
                {
                    this.m_timer = 0.1f;
                    this.m_scoreIsVisible = !this.m_scoreIsVisible;
                }
            }
            if (this.m_reading)
            {
                this.m_readingTimer += elapsedTime;
                if ((double)this.m_readingTimer > 0.300000011920929)
                {
                    this.m_readingTimer = 0.0f;
                    ++this.m_readingState;
                    if (this.m_readingState > 3)
                        this.m_readingState = 0;
                }
            }
            if (!this.m_active)
                return;
            base.Update(elapsedTime);
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            Font font = GameApplication.GetInstance().GetFont();
            this.m_t.SetScale(1f);
            if (this.m_texturedBackground)
            {
                instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform)null, this.m_menuTexture);
                this.m_t.SetPosition(240f, 250f, 0.0f);
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HighscoresBig));
                if (!this.m_blink)
                {
                    this.m_t.SetPosition(36f, 40f, 0.0f);
                    instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
                }
                else
                {
                    this.m_t.SetPosition(440f, 40f, 0.0f);
                    instance.DrawQuad<VertexPositionTexture>(this.m_quadOk, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
                }
            }
            else
            {
                instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform)null, (PSS.GameEngine.Resources.Texture)null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
                this.m_t.SetPosition(240f, 250f, 0.0f);
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HighscoresBig));
                this.m_t.SetPosition(30f, 40f, 0.0f);
                PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
            }
            this.m_t.SetPosition(426f, 205f, 0.0f);
            if (this.m_reading)
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, ">>");
            else
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, ">>");
            this.m_t.SetPosition(46f, 205f, 0.0f);
            if (this.m_reading)
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, "<<");
            else
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, "<<");
          //  LeaderboardReader leaderboardReader = LeaderboardManager.Instance.GetLeaderboardReader();
            bool flag1 = true;
            bool flag2 = false;
            bool flag3 = false;
            this.m_t.SetScale(1f);
            this.m_t.SetPosition(240f, 205f, 0.0f);
            this.m_stringBuilder.Length = 0;
            if (this.StoryMode)
            {
                this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryModeBig));
                this.m_stringBuilder.Append(" - ");
            }
            else
            {
                this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SurvivalModeBig));
                this.m_stringBuilder.Append(" - ");
            }
            if (this.m_weeklyPage)
                this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Weekly));
            else
                this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Friends));
            instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, this.m_stringBuilder);
            this.m_t.SetScale(0.9f);
            this.m_t.SetPosition(240f, 185f, 0.0f);
            //if (leaderboardReader != null && !this.m_reading)
            //{
            //    this.m_stringBuilder.Length = 0;
            //    int num1 = leaderboardReader.PageStart + 1;
            //    int num2 = leaderboardReader.PageStart + 5;
            //    if (num1 > leaderboardReader.TotalLeaderboardSize)
            //        num1 = leaderboardReader.TotalLeaderboardSize;
            //    if (num2 > leaderboardReader.TotalLeaderboardSize)
            //        num2 = leaderboardReader.TotalLeaderboardSize;
            //    this.m_stringBuilder.Append("(");
            //    StringHelper.Append(this.m_stringBuilder, num1);
            //    this.m_stringBuilder.Append("-");
            //    StringHelper.Append(this.m_stringBuilder, num2);
            //    this.m_stringBuilder.Append("/");
            //    StringHelper.Append(this.m_stringBuilder, leaderboardReader.TotalLeaderboardSize);
            //    this.m_stringBuilder.Append(")");
            //    instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
            //}
            this.m_t.SetScale(1f);
            if (this.m_reading)
            {
                this.m_t.SetPosition(230f, 185f, 0.0f);
                switch (this.m_readingState)
                {
                    case 1:
                        instance.DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, ".");
                        break;
                    case 2:
                        instance.DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, "..");
                        break;
                    case 3:
                        instance.DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, "...");
                        break;
                }
            }
            //if (leaderboardReader != null && !this.m_reading)
            //{
            //    if (leaderboardReader.CanPageDown)
            //        flag3 = true;
            //    if (leaderboardReader.CanPageUp)
            //        flag2 = true;
            //    for (int index = 0; index < leaderboardReader.Entries.Count; ++index)
            //    {
            //        flag1 = false;
            //        LeaderboardEntry entry = leaderboardReader.Entries[index];
            //        bool flag4 = entry.Gamer.Gamertag == Gamer.SignedInGamers[PlayerIndex.One].Gamertag;
            //        if (!flag4 || !this.m_blink || this.m_scoreIsVisible)
            //        {
            //            Color color = flag4 ? GameConstant.MENU_TEXT_SELECTED_COLOR : GameConstant.MENU_TEXT_COLOR;
            //            this.m_stringBuilder.Length = 0;
            //            StringHelper.Append(this.m_stringBuilder, leaderboardReader.PageStart + index + 1);
            //            this.m_stringBuilder.Append(".");
            //            this.m_t.SetPosition(95f, (float)(170.0 - 25.0 * (double)index), 0.0f);
            //            instance.DrawString(font, this.m_t, 0, color, this.m_stringBuilder);
            //            this.m_t.SetPosition(120f, (float)(170.0 - 25.0 * (double)index), 0.0f);
            //            instance.DrawString(font, this.m_t, 0, color, entry.Gamer.Gamertag);
            //            this.m_t.SetPosition(380f, (float)(170.0 - 25.0 * (double)index), 0.0f);
            //            PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, color, entry.Rating.ToString());
            //        }
            //    }
            //}
            if (flag1)
            {
                this.m_t.SetPosition(95f, 170f, 0.0f);
                instance.DrawString(font, this.m_t, 0, GameConstant.MENU_TEXT_COLOR, "-.");
                this.m_t.SetPosition(120f, 170f, 0.0f);
                instance.DrawString(font, this.m_t, 0, GameConstant.MENU_TEXT_COLOR, "---");
                this.m_t.SetPosition(380f, 170f, 0.0f);
                PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 1, GameConstant.MENU_TEXT_COLOR, "0");
            }
            this.m_t.SetPosition(411f, 130f, 0.0f);
            this.m_t.SetRotationZ(1.570796f);
            if (flag2 && !this.m_reading)
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, ">");
            else
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, ">");
            this.m_t.SetPosition(411f, 80f, 0.0f);
            if (flag3 && !this.m_reading)
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, "<");
            else
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, "<");
            this.m_t.SetRotationZ(0.0f);
            instance.End2DMode();
        }

        public void BlinkScore(bool flag)
        {
            this.m_scoreIsVisible = true;
            this.m_blink = flag;
            this.m_timer = 0.0f;
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonOk)
            {
                if (!this.m_blink)
                    return;
                if (((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
                {
                    MainMenuStateHighscoresStory.GetInstance().BackRequest = true;
                    MainMenuStateHighscoresSurvival.GetInstance().BackRequest = true;
                    GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                }
            }
            if (sender == this.m_buttonBack)
            {
                if (this.m_blink)
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
            if (this.m_reading)
                return;
            if (sender == this.m_buttonNext)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                if (this.m_weeklyPage)
                {
                    this.m_weeklyPage = false;
                    if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    {
                        MainMenuStateHighscoresStory.GetInstance().HighscoresRequest = true;
                        MainMenuStateHighscoresSurvival.GetInstance().HighscoresRequest = true;
                    }
                    else
                    {
                        InGameMenuStateHighscoresStory.GetInstance().HighscoresRequest = true;
                        InGameMenuStateHighscoresSurvival.GetInstance().HighscoresRequest = true;
                    }
                }
                else
                {
                    this.m_weeklyPage = true;
                    if (this.StoryMode)
                        this.ReadLeaderboard(0, this.m_weeklyPage);
                    else
                        this.ReadLeaderboard(1, this.m_weeklyPage);
                }
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_buttonPrevious)
            {
                if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                    return;
                if (this.m_weeklyPage)
                {
                    this.m_weeklyPage = false;
                    if (this.StoryMode)
                        this.ReadLeaderboard(0, this.m_weeklyPage);
                    else
                        this.ReadLeaderboard(1, this.m_weeklyPage);
                }
                else
                {
                    this.m_weeklyPage = false;
                    if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                    {
                        MainMenuStateHighscoresStory.GetInstance().HighscoresRequest = true;
                        MainMenuStateHighscoresSurvival.GetInstance().HighscoresRequest = true;
                    }
                    else
                    {
                        InGameMenuStateHighscoresStory.GetInstance().HighscoresRequest = true;
                        InGameMenuStateHighscoresSurvival.GetInstance().HighscoresRequest = true;
                    }
                }
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else
            {
                //LeaderboardReader leaderboardReader = LeaderboardManager.Instance.GetLeaderboardReader();
                //if (leaderboardReader == null)
                //    return;
                //if (sender == this.m_buttonUp)
                //{
                //    if (((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed || !leaderboardReader.CanPageUp)
                //        return;
                //    LeaderboardManager.Instance.ReadBestScoresPageUp();
                //    this.m_reading = true;
                //}
                //else
                //{
                //    if (sender != this.m_buttonDown || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed || !leaderboardReader.CanPageDown)
                //        return;
                //    LeaderboardManager.Instance.ReadBestScoresPageDown();
                //    this.m_reading = true;
                //}
            }
        }
    }
}
