using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.World;
using Resanctified.Game.UI;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateStoryIntro : IDrawableState, IState
    {
        private static GameStateStoryIntro m_instance = new GameStateStoryIntro();
        private StoryIntroViewController m_storyViewController;
        private VertexPositionColor[] m_quadFade;
        public bool NextRequest;

        public static GameStateStoryIntro Instance => m_instance;

        public static GameStateStoryIntro GetInstance() => m_instance;

        public GameStateStoryIntro()
        {
            this.NextRequest = false;
            this.m_storyViewController = new StoryIntroViewController();
            this.m_quadFade = new VertexPositionColor[4];
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            this.m_storyViewController.Initialize();
            ResourceManager.GetInstance().LoadAllResources();
            Application.GetInstance().AddResponder(this.m_storyViewController);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (InputManager.GetInstance().IsBackPressed())
            {
                WorldManager.GetInstance().Cleanup(false, true);
                GameFSM.GetInstance().SetState(GameStateMainMenu.GetInstance());
            }
            else
            {
                this.m_storyViewController.Update(gameTime);
                if (!this.NextRequest)
                    return;
                this.NextRequest = false;
                GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
                currentWorld.LevelIndex = GameWorld.LevelId.Level_Shop;
                Scenario.GetInstance().RaiseEventSelectLevel(currentWorld.LevelIndex);
                stateMachine.SetState(GameStateLoadLevel.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            InputManager.GetInstance().ClearPadState();
            this.m_storyViewController.Cleanup();
            Application.GetInstance().RemoveResponder(this.m_storyViewController);
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine) => this.m_storyViewController.Draw();
    }
}
