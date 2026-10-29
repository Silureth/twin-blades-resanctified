using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.World;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.FSM.GameState.MainMenu;
using Resanctified.Game.UI;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateMainMenu : IDrawableState, IState
    {
        private static GameStateMainMenu m_instance = new GameStateMainMenu();
        private SplashViewController m_splashViewController = new SplashViewController();
        private MainMenuViewController m_menuViewController = new MainMenuViewController();
        private GameModeMenuViewController m_gameModeViewController = new GameModeMenuViewController();
        private DifficultyMenuViewController m_difficultyViewController = new DifficultyMenuViewController();
        private HelpAndOptionsMenuViewController m_helpAndOptionsViewController = new HelpAndOptionsMenuViewController();
        private AboutMenuViewController m_aboutViewController = new AboutMenuViewController();
        private OptionsMenuViewController m_optionsViewController = new OptionsMenuViewController();
        private HelpViewController m_helpViewController = new HelpViewController();
        private HighscoresChooseMenuViewController m_highscoresChooseViewController = new HighscoresChooseMenuViewController();
        private HighscoresMenuViewController m_highscoresViewController = new HighscoresMenuViewController();
        private LanguageMenuViewController m_languageSelectionViewController = new LanguageMenuViewController();
        private CreditsViewController m_creditsViewController = new CreditsViewController();
        private OverwriteSaveMenuViewController m_overwriteViewController = new OverwriteSaveMenuViewController();
        private AchievementsMenuViewController m_achievementsViewController = new AchievementsMenuViewController();
        private ExitConfirmationViewController m_exitConfirmationViewController = new ExitConfirmationViewController();
        private DrawableFiniteStateMachine m_subStateMachine;
        public bool StartGameRequest;
        public bool ContinueGameRequest;
        public bool ExitRequest;
        public bool PressStartRequest;
        public bool MuteMusic;
        public bool EndOfNightRequest;
        public bool LevelRequest;
        public bool EndingRequest;
        public bool EndingAnimationRequest;
        public bool EndingStoryRequest;
        public bool EpilogOpenMapRequest;
        public bool EpilogSuicideRequest;

        public static GameStateMainMenu Instance => m_instance;

        public static GameStateMainMenu GetInstance() => m_instance;

        public SplashViewController GetSplashView() => this.m_splashViewController;

        public MainMenuViewController GetMenuView() => this.m_menuViewController;

        public GameModeMenuViewController GetGameModeView() => this.m_gameModeViewController;

        public DifficultyMenuViewController GetDifficultyView() => this.m_difficultyViewController;

        public HelpAndOptionsMenuViewController GetHelpAndOptionsView() => this.m_helpAndOptionsViewController;

        public AboutMenuViewController GetAboutView() => this.m_aboutViewController;

        public HelpViewController GetHelpView() => this.m_helpViewController;

        public OptionsMenuViewController GetOptionsView() => this.m_optionsViewController;

        public HighscoresChooseMenuViewController GetHighscoresChooseView() => this.m_highscoresChooseViewController;

        public HighscoresMenuViewController GetHighscoresView() => this.m_highscoresViewController;

        public LanguageMenuViewController GetLanguageSelectionView() => this.m_languageSelectionViewController;

        public CreditsViewController GetCreditsView() => this.m_creditsViewController;

        public OverwriteSaveMenuViewController GetOverwriteView() => this.m_overwriteViewController;

        public AchievementsMenuViewController GetAchievementsView() => this.m_achievementsViewController;

        public ExitConfirmationViewController GetExitConfirmationView() => this.m_exitConfirmationViewController;

        public DrawableFiniteStateMachine SubStateMachine => this.m_subStateMachine;

        public DrawableFiniteStateMachine GetSubStateMachine() => this.m_subStateMachine;

        public GameStateMainMenu()
        {
            this.m_subStateMachine = new DrawableFiniteStateMachine();
            this.StartGameRequest = false;
            this.ContinueGameRequest = false;
            this.ExitRequest = false;
            this.PressStartRequest = false;
            this.MuteMusic = false;
            this.EndOfNightRequest = false;
            this.LevelRequest = false;
            this.EndingRequest = false;
            this.EndingAnimationRequest = false;
            this.EndingStoryRequest = false;
            this.EpilogOpenMapRequest = false;
            this.EpilogSuicideRequest = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            ResourceManager.GetInstance().DestroyUnusedResources();
            GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
            GameApplication.GetInstance().GetTextBoxViewController().DisableChoiceMode();
            this.StartGameRequest = false;
            this.ContinueGameRequest = false;
            this.ExitRequest = false;
            this.EndOfNightRequest = false;
            this.LevelRequest = false;
            this.EndingRequest = false;
            this.EndingAnimationRequest = false;
            this.EndingStoryRequest = false;
            this.EpilogOpenMapRequest = false;
            this.EpilogSuicideRequest = false;
            this.m_splashViewController.Initialize();
            this.m_menuViewController.Initialize();
            this.m_gameModeViewController.Initialize();
            this.m_difficultyViewController.Initialize();
            this.m_helpAndOptionsViewController.Initialize(true);
            this.m_aboutViewController.Initialize();
            this.m_helpViewController.Initialize(true);
            this.m_optionsViewController.Initialize(true);
            this.m_languageSelectionViewController.Initialize(true);
            this.m_highscoresChooseViewController.Initialize(true);
            this.m_highscoresViewController.Initialize(0, true);
            this.m_creditsViewController.Initialize();
            this.m_overwriteViewController.Initialize();
            this.m_achievementsViewController.Initialize(true);
            this.m_exitConfirmationViewController.Initialize(true);
            AudioManager.GetInstance().LoadMusic("Sounds\\music_splash");
            ResourceManager.GetInstance().LoadAllResources();
            GameApplication.GetInstance().SetSoundVolume();
            if (!this.MuteMusic)
                AudioManager.GetInstance().PlayMusic();
            this.MuteMusic = false;
            if (this.PressStartRequest)
            {
                this.m_subStateMachine.SetState(MainMenuStatePressStart.GetInstance());
                this.PressStartRequest = false;
            }
            else
                this.m_subStateMachine.SetState(MainMenuStateMenu.GetInstance());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            this.m_subStateMachine.Update(gameTime);
            if (this.StartGameRequest || this.ContinueGameRequest)
            {
                this.StartGameRequest = false;
                WorldManager.GetInstance().Initialize();
                GameWorld gameWorld = new GameWorld();
                WorldManager.GetInstance().AddWorld(gameWorld, false);
                WorldManager.GetInstance().SetCurrentWorld(gameWorld);
                GameApplication.GetInstance().Score = 0;
                Scenario.GetInstance().Initialize();
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                {
                    InGameStateStartLevel.GetInstance().DayDisplayRequest = false;
                    GameStateLoadLevel.GetInstance().ContinueGameRequest = this.ContinueGameRequest;
                    gameWorld.LoadMissionPartDisabled = false;
                    if (!this.ContinueGameRequest)
                    {
                        stateMachine.SetState(GameStateStoryIntro.GetInstance());
                    }
                    else
                    {
                        gameWorld.LoadGameData();
                        gameWorld.LoadMissionPartDisabled = true;
                        if (this.EndOfNightRequest)
                            stateMachine.SetState(GameStateEndOfNight.GetInstance());
                        else if (this.LevelRequest)
                        {
                            if (Scenario.GetInstance().GetCurrentMissionIndex() == 20)
                                stateMachine.SetState(GameStateMap.GetInstance());
                            else
                                stateMachine.SetState(GameStateLoadLevel.GetInstance());
                        }
                        else if (this.EndingRequest)
                            stateMachine.SetState(GameStateEnding.GetInstance());
                        else if (this.EndingAnimationRequest)
                            stateMachine.SetState(GameStateEndingAnimation.GetInstance());
                        else if (this.EndingStoryRequest)
                            stateMachine.SetState(GameStateEndingStory.GetInstance());
                        else if (this.EpilogOpenMapRequest)
                            stateMachine.SetState(GameStateStoryEpilogOpenMap.GetInstance());
                        else if (this.EpilogSuicideRequest)
                            stateMachine.SetState(GameStateStoryEpilogSuicide.GetInstance());
                        else if (Scenario.GetInstance().GetCurrentMissionIndex() == 0)
                        {
                            GameStateLoadLevel.GetInstance().ContinueGameRequest = false;
                            gameWorld.LevelIndex = GameWorld.LevelId.Level_Shop;
                            Scenario.GetInstance().RaiseEventSelectLevel(gameWorld.LevelIndex);
                            stateMachine.SetState(GameStateLoadLevel.GetInstance());
                        }
                        else
                            stateMachine.SetState(GameStateMap.GetInstance());
                    }
                }
                else
                {
                    InGameStateStartLevel.GetInstance().DayDisplayRequest = this.ContinueGameRequest;
                    GameStateLoadLevel.GetInstance().ContinueGameRequest = this.ContinueGameRequest;
                    if (this.EndOfNightRequest)
                    {
                        gameWorld.LoadGameData();
                        stateMachine.SetState(GameStateEndOfNight.GetInstance());
                    }
                    else
                        stateMachine.SetState(GameStateLoadLevel.GetInstance());
                }
            }
            else
            {
                if (!this.ExitRequest)
                    return;
                GameApplication.GetInstance().ExitRequest = true;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.MuteMusic = false;
            this.EndOfNightRequest = false;
            this.LevelRequest = false;
            this.EndingRequest = false;
            this.EndingAnimationRequest = false;
            this.EndingStoryRequest = false;
            this.EpilogOpenMapRequest = false;
            this.EpilogSuicideRequest = false;
            this.m_subStateMachine.GetCurrentState().Exit(this.m_subStateMachine);
            InputManager.GetInstance().ClearPadState();
            this.m_splashViewController.Cleanup();
            this.m_menuViewController.Cleanup();
            this.m_gameModeViewController.Cleanup();
            this.m_difficultyViewController.Cleanup();
            this.m_helpAndOptionsViewController.Cleanup();
            this.m_aboutViewController.Cleanup();
            this.m_helpViewController.Cleanup();
            this.m_optionsViewController.Cleanup();
            this.m_languageSelectionViewController.Cleanup();
            this.m_highscoresChooseViewController.Cleanup();
            this.m_highscoresViewController.Cleanup();
            this.m_creditsViewController.Cleanup();
            this.m_overwriteViewController.Cleanup();
            this.m_achievementsViewController.Cleanup();
            this.m_exitConfirmationViewController.Cleanup();
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine) => ((IDrawableState)this.m_subStateMachine.GetCurrentState()).Draw(this.m_subStateMachine);
    }
}
