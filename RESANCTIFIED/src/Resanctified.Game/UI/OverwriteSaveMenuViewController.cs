using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM.GameState.MainMenu;

namespace Resanctified.Game.UI
{
    public class OverwriteSaveMenuViewController : ViewController
    {
        private const float OVERWRITE_TEXT_X = 240f;
        private const float OVERWRITE_TEXT_Y = 220f;
        private const float OVERWRITE_TEXT2_Y = 195f;
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_menuTexture;
        private Transform m_t = new Transform();
        private Button m_buttonOk;
        private Button m_buttonCancel;

        public OverwriteSaveMenuViewController()
        {
            this.m_menuTexture = null;
            this.m_buttonOk = new Button();
            this.m_buttonCancel = new Button();
        }

        public override void Initialize() => this.Initialize(false);

        public void Initialize(bool texturedBackground)
        {
            base.Initialize();
            Resource.SafeRelease(ref this.m_menuTexture);
            this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
            QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
            QuadFactory.CreateAnchoredQuad(this.m_quadOk, 5f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            this.m_buttonOk.Initialize(this, 56f, 56f);
            this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
            this.m_buttonCancel.Initialize(this, 56f, 56f);
            this.m_buttonCancel.GetTransform().SetPosition(36f, 40f);
        }

        public override void Cleanup()
        {
            this.m_buttonOk.Cleanup();
            this.m_buttonCancel.Cleanup();
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
            if (!this.m_visible || !this.m_visible)
                return;
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            Font font = GameApplication.GetInstance().GetFont();
            instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
            this.m_t.SetPosition(440f, 40f, 0.0f);
            instance.DrawQuad(this.m_quadOk, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            this.m_t.SetPosition(36f, 40f, 0.0f);
            instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            this.m_t.SetPosition(240f, 220f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.AskAreYouSure));
            this.m_t.SetPosition(240f, 195f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.OverwriteGame));
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonOk && ((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
            {
                MainMenuStateOverwrite.GetInstance().OverwriteOkRequest = true;
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
            }
            if (sender != this.m_buttonCancel || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            MainMenuStateOverwrite.GetInstance().OverwriteCancelRequest = true;
            GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        }
    }
}
