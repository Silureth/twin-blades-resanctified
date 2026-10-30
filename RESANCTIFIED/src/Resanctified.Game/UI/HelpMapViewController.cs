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
    public class HelpMapViewController : ViewController
    {
        private const float HELPMAP_POS_X = 240f;
        private const float HELPMAP_POS_Y = 200f;
        private Button m_buttonNext;
        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private Transform m_t = new Transform();

        public HelpMapViewController()
        {
            this.m_buttonNext = new Button();
            this.DefaultButton = this.m_buttonNext;
        }

        public override void Initialize()
        {
            base.Initialize();
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            this.m_buttonNext.Initialize(this, 480f, 320f);
            this.m_buttonNext.GetTransform().SetPosition(240f, 180f);
        }

        public override void Cleanup()
        {
            this.m_buttonNext.Cleanup();
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
            instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
            Font font = GameApplication.GetInstance().GetFont();
            this.m_t.SetPosition(240f, 200f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_HELP_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Map));
            instance.End2DMode();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || sender != this.m_buttonNext || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            InGameStateHelpMap.GetInstance().NextRequest = true;
        }
    }
}
