using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.World;
using Resanctified.Game.FSM.GameState.MainMenu;
using Resanctified.Game.UI;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateStoryEpilogSuicide : IDrawableState, IState
    {
        private static GameStateStoryEpilogSuicide m_instance = new GameStateStoryEpilogSuicide();
        private StoryEpilogViewController m_storyViewController;
        private VertexPositionColor[] m_quadFade;
        public bool NextRequest;

        public static GameStateStoryEpilogSuicide Instance => m_instance;

        public static GameStateStoryEpilogSuicide GetInstance() => m_instance;

        public GameStateStoryEpilogSuicide()
        {
            this.m_storyViewController = new StoryEpilogViewController();
            this.m_quadFade = new VertexPositionColor[4];
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.NextRequest = false;
            this.m_storyViewController.Initialize(true);
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
            GameFSM.GetInstance().SetState(GameStateMainMenu.GetInstance());
            GameApplication instance = GameApplication.GetInstance();
            GameStateMainMenu.GetInstance().GetSubStateMachine().SetState(MainMenuStateHighscoresStory.GetInstance());
            GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = true;
            int num = instance.UpdateHighscores();
            GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(num + 1);
            if (num >= 5)
                return;
            GameApplication.Instance.SaveHighscores();
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            WorldManager.GetInstance().Cleanup(false, true);
            AudioManager.GetInstance().StopAll();
            AudioManager.GetInstance().UnloadMusic();
            InputManager.GetInstance().ClearPadState();
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
