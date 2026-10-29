using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateEndingAnimation : IDrawableState, IState
    {
        private static GameStateEndingAnimation m_instance = new GameStateEndingAnimation();
        private Animation2D m_anim;

        public static GameStateEndingAnimation Instance => m_instance;

        public static GameStateEndingAnimation GetInstance() => m_instance;

        public GameStateEndingAnimation() => this.m_anim = new Animation2D();

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.m_anim.Initialize("Anims\\endofgame");
            this.m_anim.SetLoop(Animation2DLoop.None);
            this.m_anim.SetSpeed(1f);
            AudioManager.GetInstance().LoadMusic("Sounds\\cutscene", true);
            AudioManager.GetInstance().SetMusicLoopCount(1);
            ResourceManager.GetInstance().LoadAllResources();
            AudioManager.GetInstance().PlayMusic();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            this.m_anim.Update(gameTime);
            if (!this.m_anim.IsEndOfAnimationReached())
                return;
            GameApplication.GetInstance().CheckBossNecro2KillAchievement();
            if (Scenario.GetInstance().GetCurrentMissionIndex() == 99)
                GameFSM.GetInstance().SetState(GameStateMap.GetInstance());
            else
                GameFSM.GetInstance().SetState(GameStateEndingStory.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            Renderer.GetInstance().ClearTopLevelViewList();
            AudioManager.GetInstance().StopAll();
            AudioManager.GetInstance().UnloadMusic();
            AudioManager.GetInstance().SetMusicLoopCount(-1);
            InputManager.GetInstance().ClearPadState();
            this.m_anim.Cleanup();
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            Renderer instance = Renderer.GetInstance();
            instance.Begin2DMode(true);
            instance.DrawAnimation2DModel(this.m_anim.GetModel(), this.m_anim.GetFrame(), null, BlendFunc.SrcAlpha_InvSrcAlpha);
            instance.End2DMode();
        }
    }
}
