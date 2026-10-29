// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.GameStateMainMenu
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.Game.NunVsZombies.FSM.GameState.MainMenu;
using PSS.Game.NunVsZombies.UI;
using PSS.Game.NunVsZombies.World;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.GameState
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
    private LeaderboardMenuViewController m_leaderboardViewController = new LeaderboardMenuViewController();
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
    private bool m_isOtherAudioPlayingChecked;

    public static GameStateMainMenu Instance => GameStateMainMenu.m_instance;

    public static GameStateMainMenu GetInstance() => GameStateMainMenu.m_instance;

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

    public LeaderboardMenuViewController GetLeaderboardView() => this.m_leaderboardViewController;

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
      this.m_isOtherAudioPlayingChecked = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      GuideManager.Instance.UpdateIsTrial();
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
      this.m_leaderboardViewController.Initialize(true);
      this.m_creditsViewController.Initialize();
      this.m_overwriteViewController.Initialize();
      this.m_achievementsViewController.Initialize(true);
      this.m_exitConfirmationViewController.Initialize(true);
      AudioManager.GetInstance().LoadMusic("Sounds\\music_splash");
      ResourceManager.GetInstance().LoadAllResources();
      GameApplication.GetInstance().SetSoundVolume();
      if (!this.m_isOtherAudioPlayingChecked)
      {
        if (AudioManager.GetInstance().IsOtherAudioPlaying())
          GuideManager.Instance.RequestShowMessageUserMusicPlaying = true;
        this.m_isOtherAudioPlayingChecked = true;
      }
      if (!this.MuteMusic)
        AudioManager.GetInstance().PlayMusic();
      this.MuteMusic = false;
      if (this.PressStartRequest)
      {
        this.m_subStateMachine.SetState((IState) MainMenuStatePressStart.GetInstance());
        this.PressStartRequest = false;
      }
      else
        this.m_subStateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      this.m_subStateMachine.Update(gameTime);
      if (this.StartGameRequest || this.ContinueGameRequest)
      {
        this.StartGameRequest = false;
        WorldManager.GetInstance().Initialize();
        GameWorld gameWorld = new GameWorld();
        WorldManager.GetInstance().AddWorld((PSS.GameEngine.World.World) gameWorld, false);
        WorldManager.GetInstance().SetCurrentWorld((PSS.GameEngine.World.World) gameWorld);
        GameApplication.GetInstance().Score = 0;
        Scenario.GetInstance().Initialize();
        if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
        {
          InGameStateStartLevel.GetInstance().DayDisplayRequest = false;
          GameStateLoadLevel.GetInstance().ContinueGameRequest = this.ContinueGameRequest;
          gameWorld.LoadMissionPartDisabled = false;
          if (!this.ContinueGameRequest)
          {
            stateMachine.SetState((IState) GameStateStoryIntro.GetInstance());
          }
          else
          {
            gameWorld.LoadGameData();
            gameWorld.LoadMissionPartDisabled = true;
            if (this.EndOfNightRequest)
              stateMachine.SetState((IState) GameStateEndOfNight.GetInstance());
            else if (this.LevelRequest)
            {
              if (Scenario.GetInstance().GetCurrentMissionIndex() == 20)
                stateMachine.SetState((IState) GameStateMap.GetInstance());
              else
                stateMachine.SetState((IState) GameStateLoadLevel.GetInstance());
            }
            else if (this.EndingRequest)
              stateMachine.SetState((IState) GameStateEnding.GetInstance());
            else if (this.EndingAnimationRequest)
              stateMachine.SetState((IState) GameStateEndingAnimation.GetInstance());
            else if (this.EndingStoryRequest)
              stateMachine.SetState((IState) GameStateEndingStory.GetInstance());
            else if (this.EpilogOpenMapRequest)
              stateMachine.SetState((IState) GameStateStoryEpilogOpenMap.GetInstance());
            else if (this.EpilogSuicideRequest)
              stateMachine.SetState((IState) GameStateStoryEpilogSuicide.GetInstance());
            else if (Scenario.GetInstance().GetCurrentMissionIndex() == 0)
            {
              GameStateLoadLevel.GetInstance().ContinueGameRequest = false;
              gameWorld.LevelIndex = GameWorld.LevelId.Level_Shop;
              Scenario.GetInstance().RaiseEventSelectLevel(gameWorld.LevelIndex);
              stateMachine.SetState((IState) GameStateLoadLevel.GetInstance());
            }
            else
              stateMachine.SetState((IState) GameStateMap.GetInstance());
          }
        }
        else
        {
          InGameStateStartLevel.GetInstance().DayDisplayRequest = this.ContinueGameRequest;
          GameStateLoadLevel.GetInstance().ContinueGameRequest = this.ContinueGameRequest;
          if (this.EndOfNightRequest)
          {
            gameWorld.LoadGameData();
            stateMachine.SetState((IState) GameStateEndOfNight.GetInstance());
          }
          else
            stateMachine.SetState((IState) GameStateLoadLevel.GetInstance());
        }
      }
      else
      {
        if (!this.ExitRequest)
          return;
        if (GuideManager.Instance.IsTrialMode())
          stateMachine.SetState((IState) GameStateFeatures.Instance);
        else
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
      this.m_subStateMachine.GetCurrentState().Exit((IFiniteStateMachine) this.m_subStateMachine);
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
      this.m_leaderboardViewController.Cleanup();
      this.m_creditsViewController.Cleanup();
      this.m_overwriteViewController.Cleanup();
      this.m_achievementsViewController.Cleanup();
      this.m_exitConfirmationViewController.Cleanup();
      ResourceManager.GetInstance().DestroyUnusedResources();
    }

    public void Draw(IFiniteStateMachine stateMachine) => ((IDrawableState) this.m_subStateMachine.GetCurrentState()).Draw((IFiniteStateMachine) this.m_subStateMachine);
  }
}
