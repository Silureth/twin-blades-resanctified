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
    public class StoryIntroViewController : ViewController
    {
        private const float STORY_INTRO_TEXT_X = 40f;
        private const float STORY_INTRO_TEXT_Y = 260f;
        private const float STORY_INTRO_TEXT_SIGN_X = 260f;
        private const float STORY_INTRO_TEXT_SIGN_Y = 60f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_menuTexture;
        private Transform m_t = new Transform();
        private Button m_buttonOk;
        private List<OLPair> m_introTextPairs;

        public StoryIntroViewController()
        {
            this.m_menuTexture = null;
            this.m_buttonOk = new Button();
            this.DefaultButton = this.m_buttonOk;
            this.m_introTextPairs = new List<OLPair>(20);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.m_introTextPairs.Clear();
            Resource.SafeRelease(ref this.m_menuTexture);
            this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            QuadFactory.CreateAnchoredQuad(this.m_quadOk, 5f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
            this.m_buttonOk.Initialize(this, 56f, 56f);
            this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
            TextUtils.WrapText(ref this.m_introTextPairs, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryIntro), GameApplication.GetInstance().GetFont(), 480f);
        }

        public override void Cleanup()
        {
            this.m_buttonOk.Cleanup();
            this.m_introTextPairs.Clear();
            Resource.SafeRelease(ref this.m_menuTexture);
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
            instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
            instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
            this.m_t.SetPosition(40f, 260f, 0.0f);
            this.m_t.SetScale(0.8f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryIntro), this.m_introTextPairs, 0, this.m_introTextPairs.Count, true);
            this.m_t.SetScale(1f);
            this.m_t.SetPosition(260f, 60f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryIntroTheresa));
            this.m_t.SetPosition(440f, 40f, 0.0f);
            instance.DrawQuad(this.m_quadOk, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || sender != this.m_buttonOk || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            GameStateStoryIntro.GetInstance().NextRequest = true;
        }
    }
}
