using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM.GameState.InGame;

namespace Resanctified.Game.UI
{
    public class BackToMainMenuConfirmationViewController : ViewController
    {
        private const float EXIT_TEXT_X = 240f;
        private const float EXIT_TEXT_1_Y = 240f;
        private const float EXIT_TEXT_2_Y = 190f;
        private const float EXIT_TEXT_3_Y = 160f;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        private Button m_buttonOk;
        private Button m_buttonCancel;
        private Transform m_t = new Transform();

        public BackToMainMenuConfirmationViewController()
        {
            this.m_buttonOk = new Button();
            this.DefaultButton = this.m_buttonOk;
            this.m_buttonCancel = new Button();
        }

        public override void Initialize()
        {
            base.Initialize();
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            this.m_buttonOk.Initialize(this, 100f, 56f);
            this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
            this.m_buttonCancel.Initialize(this, 100f, 56f);
            this.m_buttonCancel.GetTransform().SetPosition(30f, 40f);
        }

        public override void Cleanup()
        {
            this.m_buttonOk.Cleanup();
            this.m_buttonCancel.Cleanup();
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
            instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            this.m_t.SetPosition(30f, 40f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Cancel));
            this.m_t.SetPosition(440f, 40f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 9, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Ok));
            this.m_t.SetPosition(240f, 240f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.BackToMainMenu));
            this.m_t.SetPosition(240f, 190f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.AskAreYouSure));
            this.m_t.SetPosition(240f, 160f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ConfirmProgressErased));
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            if (sender == this.m_buttonOk && ((ButtonActionEvent)evt).buttonState == PSSButtonState.ButtonStatePressed)
                InGameMenuStateBackToMainMenuConfirmation.GetInstance().ExitRequest = true;
            if (sender != this.m_buttonCancel || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            InGameMenuStateBackToMainMenuConfirmation.GetInstance().PreviousRequest = true;
        }
    }
}
