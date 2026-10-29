// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.UI.GameModeMenuViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.FSM.GameState.MainMenu;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using System.Text;

namespace Resanctified.Game.UI
{
    public class GameModeMenuViewController : ViewController
    {
        private const float TITLE_X = 240f;
        private const float TITLE_Y = 250f;
        private const float TEXT_X = 240f;
        private const float TEXT_DESC_Y = 200f;
        private const float TEXT_CONTINUE_Y = 150f;
        private const float TEXT_START_Y = 105f;
        private const float TEXT_CHEAT_OPEN_MAP_Y = 60f;
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Button m_buttonBack;
        private Button m_buttonStartNewGame;
        private Button m_buttonContinueGame;
        private Resanctified.Engine.Resources.Texture m_menuTexture;
        private StringBuilder m_stringBuilder = new StringBuilder((int)byte.MaxValue);
        private Transform m_t = new Transform();
        private Font m_font;
        public GameApplication.GameDifficulty SavedDifficulty;

        public GameModeMenuViewController()
        {
            this.m_menuTexture = (Resanctified.Engine.Resources.Texture)null;
            this.m_buttonBack = new Button();
            this.m_buttonStartNewGame = new Button();
            this.m_buttonContinueGame = new Button();
        }

        public override void Initialize()
        {
            base.Initialize();
            Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_menuTexture);
            Resanctified.Engine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
            this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
            this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            this.m_buttonBack.Initialize((ViewController)this, 56f, 56f);
            this.m_buttonBack.GetTransform().SetPosition(36f, 40f);

            this.m_buttonStartNewGame.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonStartNewGame.GetTransform().SetPosition(240f, 95f);
            this.m_buttonContinueGame.Initialize((ViewController)this, 200f, 30f);
            this.m_buttonContinueGame.GetTransform().SetPosition(240f, 140f);
        }

        public override void Cleanup()
        {
            this.m_buttonBack.Cleanup();
            this.m_buttonStartNewGame.Cleanup();
            this.m_buttonContinueGame.Cleanup();
            Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_menuTexture);
            Resanctified.Engine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
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
            Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            int currentLanguage = (int)GameApplication.GetInstance().CurrentLanguage;
            Font font = GameApplication.GetInstance().GetFont();
            instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform)null, this.m_menuTexture);
            this.m_t.SetPosition(36f, 40f, 0.0f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
            GameLocalisation.TextId textId = GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GameLocalisation.TextId.SurvivalMode_Desc : GameLocalisation.TextId.StoryMode_Desc;
            this.m_t.SetPosition(240f, 250f);
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryModeBig));
            else
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SurvivalModeBig));
            this.m_t.SetPosition(240f, 200f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(textId));
            this.m_t.SetPosition(240f, 105f);
            instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StartNewGame));
            this.m_t.SetPosition(240f, 150f);
            this.m_stringBuilder.Length = 0;
            this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ContinueGame));
            Color color;
            if (MainMenuStateGameMode.GetInstance().IsSavedGameAvailable())
            {
                this.m_stringBuilder.Append(" (");
                if (this.SavedDifficulty == GameApplication.GameDifficulty.Normal)
                    this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.DifficultyNormal));
                else
                    this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.DifficultyHard));
                this.m_stringBuilder.Append(')');
                color = GameConstant.MENU_TEXT_COLOR;
            }
            else
                color = GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR;
            instance.DrawString(font, this.m_t, 4, color, this.m_stringBuilder);
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonBack && ((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
                MainMenuStateGameMode.GetInstance().BackRequest = true;
            if (sender == this.m_buttonStartNewGame && ((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
            {
                MainMenuStateGameMode.GetInstance().DifficultyRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            if (sender != this.m_buttonContinueGame || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed || !MainMenuStateGameMode.GetInstance().IsSavedGameAvailable())
                return;
            MainMenuStateGameMode.GetInstance().ContinueRequest = true;
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
        }
    }
}
