using Microsoft.Xna.Framework;
using Resanctified.Engine;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.FSM.GameState;

namespace Resanctified.Game.World.Entities
{
    public class NPCShopViewController : ViewController
    {
        private Button m_iconButton;
        private NPCShop m_shop;

        public NPCShopViewController()
        {
            this.m_iconButton = new Button();
            this.m_shop = null;
        }

        public void Initialize(NPCShop shop)
        {
            this.Initialize();
            this.m_iconButton.Initialize(this, 120f, 200f);
            this.m_shop = shop;
            Application.GetInstance().AddResponder(this);
        }

        public override void Cleanup()
        {
            this.m_iconButton.Cleanup();
            this.m_shop = null;
            Application.GetInstance().RemoveResponder(this);
            base.Cleanup();
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || !this.m_shop.IsIconActive() || GameApplication.GetInstance().GetTextBoxViewController().IsActive())
                return;
            GameStateInGame.GetInstance();
            if (GameStateInGame.GetInstance().IsMenuInGameActive() || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            this.m_shop.EnterShopRequest = true;
        }

        public void SetButtonPosition(CameraEntity camera)
        {
            Vector3 vector3 = Vector3.Transform(new Vector3(6f, 0.85f, 0.0f), camera.GetViewMatrix());
            float num1 = camera.GetOrthoWidth() * 0.5f;
            float num2 = camera.GetOrthoHeight() * 0.5f;
            Vector3 zero = Vector3.Zero with
            {
                X = (float)(vector3.X * 240.0 / (double)num1 + 240.0),
                Y = (float)(vector3.Y * 160.0 / (double)num2 + 160.0)
            };
            this.m_iconButton.GetTransform().SetPosition(zero);
        }

        public override void Update(float elapsedTime) => this.SetButtonPosition(((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetCamera());
    }
}
