using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.UI;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateLogo : IDrawableState, IState
    {
        private const float LOGO_TIME = 2f;
        private const float LOGO_SOUNDWALK_TIME = 3.1f;
        private const float LOGO_SOUNDSPLAT_TIME = 4.8f;
        private Animation2D m_anim;
        private Animation2DView m_view;
        private LogoViewController m_logoViewController;
        private float m_timer;
        private bool m_logoEndDisplay;
        private bool m_logoSplatPlayed;
        private Sound m_soundWalk;
        private Sound m_soundStand;
        private Sound m_soundDying;
        public bool NextRequest;
        private static GameStateLogo m_instance = new GameStateLogo();

        public static GameStateLogo Instance => m_instance;

        public static GameStateLogo GetInstance() => m_instance;

        public GameStateLogo()
        {
            this.m_anim = new Animation2D();
            this.m_view = new Animation2DView();
            this.m_soundWalk = new Sound();
            this.m_soundStand = new Sound();
            this.m_soundDying = new Sound();
            this.m_logoViewController = new LogoViewController();
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            this.m_logoEndDisplay = false;
            this.m_logoSplatPlayed = false;
            this.m_timer = 3.1f;
            Renderer.GetInstance().ClearTopLevelViewList();
            CameraEntity cameraEntity = new CameraEntity();
            cameraEntity.Initialize();
            cameraEntity.SetOrthogonalProjection(480f, 320f, -1f, 1f);
            cameraEntity.GetTransform().SetPosition(240f, 160f, 0.0f);
            Renderer.GetInstance().SetViewMatrix(cameraEntity.GetViewMatrix());
            Renderer.GetInstance().SetProjectionMatrix(cameraEntity.GetProjectionMatrix());
            this.m_anim.Initialize("Anims\\intro_logo");
            this.m_anim.SetLoop(Animation2DLoop.None);
            this.m_anim.SetSpeed(1f);
            this.m_view.SetAnimation2D(this.m_anim);
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            Renderer.GetInstance().AddTopLevelView(this.m_view, null);
            this.m_logoViewController.Initialize();
            Application.GetInstance().AddResponder(this.m_logoViewController);
            this.m_soundWalk.Initialize("Sounds\\zombie_walk02", true);
            this.m_soundStand.Initialize("Sounds\\zombie_pushback01", true);
            this.m_soundDying.Initialize("Sounds\\zombie_dying_headshot", true);
            ResourceManager.GetInstance().LoadAllResources();
            this.m_soundWalk.Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
            {
                GameApplication.GetInstance().ExitRequest = true;
            }
            else
            {
                this.m_view.Update(gameTime);
                if (this.NextRequest)
                {
                    Application.GetInstance().RemoveResponder(this.m_logoViewController);
                    this.NextRequest = false;
                    this.m_anim.SetFrame(this.m_anim.GetFrameCount());
                }
                if (!this.m_logoEndDisplay && !this.m_logoSplatPlayed)
                {
                    this.m_timer -= gameTime;
                    if (this.m_timer <= 0.0)
                    {
                        this.m_timer = 4.8f;
                        this.m_soundStand.Play();
                    }
                    if (stateMachine.GetCurrentStateTime() >= 4.80000019073486)
                    {
                        this.m_soundDying.Play();
                        this.m_logoSplatPlayed = true;
                    }
                }
                if (this.m_anim.IsEndOfAnimationReached() && !this.m_logoEndDisplay)
                {
                    this.m_timer = 2f;
                    this.m_logoEndDisplay = true;
                    if (!this.m_logoSplatPlayed)
                    {
                        this.m_soundDying.Play();
                        this.m_logoSplatPlayed = true;
                    }
                }
                if (!this.m_logoEndDisplay)
                    return;
                this.m_timer -= gameTime;
                if (this.m_timer > 0.0)
                    return;
                GameStateMainMenu.Instance.PressStartRequest = true;
                stateMachine.SetState(GameStateMainMenu.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            Renderer.GetInstance().ClearTopLevelViewList();
            InputManager.GetInstance().ClearPadState();
            this.m_logoViewController.Cleanup();
            this.m_view.Cleanup();
            this.m_anim.Cleanup();
            this.m_anim = null;
            this.m_view = null;
            this.m_soundWalk.Cleanup();
            this.m_soundStand.Cleanup();
            this.m_soundDying.Cleanup();
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine) => Renderer.GetInstance().Draw(false);
    }
}
