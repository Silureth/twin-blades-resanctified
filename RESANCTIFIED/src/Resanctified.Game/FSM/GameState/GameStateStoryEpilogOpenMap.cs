using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Game.UI;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateStoryEpilogOpenMap : IDrawableState, IState
    {
        private static GameStateStoryEpilogOpenMap m_instance = new GameStateStoryEpilogOpenMap();
        private StoryEpilogViewController m_storyViewController;
        private VertexPositionColor[] m_quadFade;
        public bool NextRequest;

        public static GameStateStoryEpilogOpenMap Instance => m_instance;

        public static GameStateStoryEpilogOpenMap GetInstance() => m_instance;

        public GameStateStoryEpilogOpenMap()
        {
            this.m_storyViewController = new StoryEpilogViewController();
            this.m_quadFade = new VertexPositionColor[4];
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            this.m_storyViewController.Initialize(false);
            AudioManager.GetInstance().LoadMusic("Sounds\\music_splash");
            ResourceManager.GetInstance().LoadAllResources();
            GameApplication.GetInstance().SetSoundVolume();
            AudioManager.GetInstance().PlayMusic();
            Application.GetInstance().AddResponder(this.m_storyViewController);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            this.m_storyViewController.Update(gameTime);
            if (!this.NextRequest)
                return;
            this.NextRequest = false;
            stateMachine.SetState(GameStateMap.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            AudioManager.GetInstance().StopAll();
            AudioManager.GetInstance().UnloadMusic();
            InputManager.GetInstance().ClearPadState();
            this.m_storyViewController.Cleanup();
            ResourceManager.GetInstance().DestroyUnusedResources();
            Application.GetInstance().RemoveResponder(this.m_storyViewController);
        }

        public void Draw(IFiniteStateMachine stateMachine) => this.m_storyViewController.Draw();
    }
}
