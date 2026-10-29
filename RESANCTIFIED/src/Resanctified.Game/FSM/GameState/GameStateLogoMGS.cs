using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.UI;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateLogoMGS : IDrawableState, IState
    {
        private const float LOGO_TIME = 5f;
        private LogoMGSViewController m_logoViewController;
        private float m_timer;
        public bool NextRequest;
        private static GameStateLogoMGS m_instance = new GameStateLogoMGS();

        public static GameStateLogoMGS Instance => m_instance;

        public static GameStateLogoMGS GetInstance() => m_instance;

        public GameStateLogoMGS() => this.m_logoViewController = new LogoMGSViewController();

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            this.m_timer = 5f;
            Engine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
            CameraEntity cameraEntity = new CameraEntity();
            cameraEntity.Initialize();
            cameraEntity.SetOrthogonalProjection(480f, 320f, -1f, 1f);
            cameraEntity.GetTransform().SetPosition(240f, 160f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().SetViewMatrix(cameraEntity.GetViewMatrix());
            Engine.Graphics.Renderer.Renderer.GetInstance().SetProjectionMatrix(cameraEntity.GetProjectionMatrix());
            this.m_logoViewController.Initialize();
            Application.GetInstance().AddResponder(this.m_logoViewController);
            ResourceManager.GetInstance().LoadAllResources();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
            {
                GameApplication.GetInstance().ExitRequest = true;
            }
            else
            {
                if (this.NextRequest)
                {
                    Application.GetInstance().RemoveResponder(this.m_logoViewController);
                    this.NextRequest = false;
                    this.m_timer = 0.0f;
                }
                this.m_timer -= gameTime;
                if (this.m_timer > 0.0)
                    return;
                stateMachine.SetState(GameStateLogo.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            Engine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
            InputManager.GetInstance().ClearPadState();
            this.m_logoViewController.Cleanup();
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine) => this.m_logoViewController.Draw();
    }
}
