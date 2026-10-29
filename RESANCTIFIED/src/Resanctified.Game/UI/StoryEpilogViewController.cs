using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Tools;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM.GameState;

namespace Resanctified.Game.UI
{
    public class StoryEpilogViewController : ViewController
    {
        private const float STORY_EPILOG_BG_TOP_X = 0.0f;
        private const float STORY_EPILOG_BG_TOP_Y = 0.0f;
        private const float STORY_EPILOG_BG_WIDTH = 480f;
        private const float STORY_EPILOG_BG_HEIGHT = 320f;
        private const float STORY_EPILOG_TEXTURE_WIDTH = 512f;
        private const float STORY_EPILOG_TEXTURE_HEIGHT = 512f;
        private const float STORY_EPILOG_TEXT_SUICIDE_X = 40f;
        private const float STORY_EPILOG_TEXT_SUICIDE_Y = 260f;
        private const float STORY_EPILOG_TEXT_OPENMAP_X = 240f;
        private const float STORY_EPILOG_TEXT_OPENMAP_Y = 160f;
        private const float STORY_EPILOG_TEXT_SUICIDE_SIGN_X = 260f;
        private const float STORY_EPILOG_TEXT_SUICIDE_SIGN_Y = 60f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_epilogTexture;
        private Transform m_t = new Transform();
        private bool m_suicideEnding;
        private Button m_buttonOk;
        private string m_text;
        private List<OLPair> m_textPairs;

        public StoryEpilogViewController()
        {
            this.m_epilogTexture = null;
            this.m_suicideEnding = false;
            this.m_buttonOk = new Button();
            this.m_textPairs = new List<OLPair>(20);
        }

        public override void Initialize() => this.Initialize(false);

        public void Initialize(bool suicideEnding)
        {
            this.m_suicideEnding = suicideEnding;
            base.Initialize();
            Resource.SafeRelease(ref this.m_epilogTexture);
            this.m_epilogTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
            QuadFactory.CreateAnchoredQuad(this.m_quadOk, 5f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            this.m_buttonOk.Initialize(this, 56f, 56f);
            this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
            this.m_text = !suicideEnding ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Story_Ending_OpenMap) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Story_Ending);
            this.m_textPairs.Clear();
            TextUtils.WrapText(ref this.m_textPairs, this.m_text, GameApplication.GetInstance().GetFont(), 480f);
        }

        public override void Cleanup()
        {
            this.m_buttonOk.Cleanup();
            Resource.SafeRelease(ref this.m_epilogTexture);
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
            instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_epilogTexture);
            instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
            if (this.m_suicideEnding)
            {
                this.m_t.SetPosition(40f, 260f, 0.0f);
                this.m_t.SetScale(0.8f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, GameConstant.MENU_TEXT_COLOR, this.m_text, this.m_textPairs, 0, this.m_textPairs.Count, true);
                this.m_t.SetScale(1f);
                this.m_t.SetPosition(260f, 60f, 0.0f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Story_Ending2));
            }
            else
            {
                this.m_t.SetPosition(240f, 160f, 0.0f);
                this.m_t.SetScale(1f);
                Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, this.m_text, this.m_textPairs, 0, this.m_textPairs.Count, true);
            }
            this.m_t.SetPosition(440f, 40f, 0.0f);
            instance.DrawQuad(this.m_quadOk, VertexFormat.Position3Texture, this.m_t, this.m_epilogTexture);
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || sender != this.m_buttonOk || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            if (this.m_suicideEnding)
                GameStateStoryEpilogSuicide.GetInstance().NextRequest = true;
            else
                GameStateStoryEpilogOpenMap.GetInstance().NextRequest = true;
        }
    }
}
