using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.UI;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateLogoLive : IDrawableState, IState
    {
        private const float LOGO_TIME = 2f;
        private LogoLiveViewController m_logoViewController;
        private static GameStateLogoLive m_instance = new GameStateLogoLive();

        public static GameStateLogoLive Instance => m_instance;

        public static GameStateLogoLive GetInstance() => m_instance;

        public GameStateLogoLive() => this.m_logoViewController = new LogoLiveViewController();

        public void Enter(IFiniteStateMachine stateMachine)
        {
            Engine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
            CameraEntity cameraEntity = new CameraEntity();
            cameraEntity.Initialize();
            cameraEntity.SetOrthogonalProjection(480f, 320f, -1f, 1f);
            cameraEntity.GetTransform().SetPosition(240f, 160f, 0.0f);
            Engine.Graphics.Renderer.Renderer.GetInstance().SetViewMatrix(cameraEntity.GetViewMatrix());
            Engine.Graphics.Renderer.Renderer.GetInstance().SetProjectionMatrix(cameraEntity.GetProjectionMatrix());
            this.m_logoViewController.Initialize();
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
                if (stateMachine.CurrentStateTime < 2.0)
                    return;
                stateMachine.SetState(GameStateLogoMGS.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            Engine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
            InputManager.GetInstance().ClearPadState();
            this.m_logoViewController.Cleanup();
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine) => this.m_logoViewController.Draw();
    }
}
