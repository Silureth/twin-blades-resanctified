// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.GameApplication
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

//using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM;
using PSS.Game.NunVsZombies.FSM.GameState;
using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.Game.NunVsZombies.FSM.GameState.MainMenu;
using PSS.Game.NunVsZombies.UI;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.IO;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;
using System;
using System.Globalization;
using Application = PSS.GameEngine.Application;

namespace PSS.Game.NunVsZombies
{
    public class GameApplication : Application
    {
        public const int LEADERBOARDID_STORY = 0;
        public const int LEADERBOARDID_SURVIVAL = 1;
        private static readonly string STATE_KEY = "TwinBladesState";
        private GraphicsDeviceManager m_graphics;
        private TextBoxViewController m_textBoxViewController = new TextBoxViewController();
        private InGameMenuViewController m_inGameMenuViewController = new InGameMenuViewController();
        private HelpViewController m_inGameHelpViewController = new HelpViewController();
        private HelpMapViewController m_inGameHelpMapViewController = new HelpMapViewController();
        private EONHelpViewController m_inGameHelpEONViewController = new EONHelpViewController();
        private OptionsMenuViewController m_inGameOptionsViewController = new OptionsMenuViewController();
        private HelpAndOptionsMenuViewController m_inGameHelpAndOptionsViewController = new HelpAndOptionsMenuViewController();
        private LanguageMenuViewController m_inGameLanguageSelectionViewController = new LanguageMenuViewController();
        private BackToMapConfirmationViewController m_inGameBackToMapConfirmationViewController = new BackToMapConfirmationViewController();
        private BackToMainMenuConfirmationViewController m_inGameBackToMainMenuConfirmationViewController = new BackToMainMenuConfirmationViewController();
        private ExitConfirmationViewController m_inGameExitConfirmationViewController = new ExitConfirmationViewController();
        private AchievementsMenuViewController m_inGameAchievementsViewController = new AchievementsMenuViewController();
        private HighscoresChooseMenuViewController m_inGameHighscoresChooseViewController = new HighscoresChooseMenuViewController();
        private HighscoresMenuViewController m_inGameHighscoresViewController = new HighscoresMenuViewController();
        private LeaderboardMenuViewController m_inGameLeaderboardViewController = new LeaderboardMenuViewController();
        public bool ExitRequest;
        private int[] m_savedState;
        public int Score;
        public float MusicGain;
        public float SfxGain;
        public float HudAlpha;
        public GameLocalisation.LanguageId CurrentLanguage;
        private GameLocalisation.LanguageId PlatformLanguage;
        public int AchievementCounter_100ZombiesKilled;
        public int AchievementCounter_500ZombiesKilled;
        public int AchievementCounter_1000ZombiesKilled;
        public int AchievementCounter_500ZombiesKilledAirSlash;
        public int AchievementCounter_500ZombiesKilledBullet;
        public int AchievementCounter_500ZombiesKilledFlameThrower;
        public int AchievementCounter_500ZombiesKilledIceGun;
        public int AchievementCounter_500ZombiesKilledExplosiveGun;
        public int AchievementCounter_1000ZombiesKilledHolyBeam;
        public int AchievementCounter_100Ungrab;
        public bool Achievement_OneLife;
        public bool Achievement_NoGunUpgrades;
        private Font m_font;
        private int[] m_highscoresSurvival = new int[5];
        private int[] m_highscoresStory = new int[5];
        private Sound[] m_outgameSounds = new Sound[6];
        private string m_assemblyVersionString;
        private GameApplication.GameMode m_gameMode;
        private GameApplication.GameDifficulty m_difficulty;

        public TextBoxViewController GetTextBoxViewController() => this.m_textBoxViewController;

        public InGameMenuViewController GetInGameMenuView() => this.m_inGameMenuViewController;

        public HelpViewController GetInGameHelpView() => this.m_inGameHelpViewController;

        public HelpMapViewController GetInGameHelpMapView() => this.m_inGameHelpMapViewController;

        public EONHelpViewController GetInGameHelpEONView() => this.m_inGameHelpEONViewController;

        public HelpAndOptionsMenuViewController GetInGameHelpAndOptionsView() => this.m_inGameHelpAndOptionsViewController;

        public OptionsMenuViewController GetInGameOptionsView() => this.m_inGameOptionsViewController;

        public LanguageMenuViewController GetInGameLanguageSelectionView() => this.m_inGameLanguageSelectionViewController;

        public BackToMapConfirmationViewController GetInGameBackToMapConfirmationView() => this.m_inGameBackToMapConfirmationViewController;

        public BackToMainMenuConfirmationViewController GetInGameBackToMainMenuConfirmationView() => this.m_inGameBackToMainMenuConfirmationViewController;

        public ExitConfirmationViewController GetInGameExitConfirmationView() => this.m_inGameExitConfirmationViewController;

        public AchievementsMenuViewController GetInGameAchievementsView() => this.m_inGameAchievementsViewController;

        public HighscoresChooseMenuViewController GetInGameHighscoresChooseView() => this.m_inGameHighscoresChooseViewController;

        public HighscoresMenuViewController GetInGameHighscoresView() => this.m_inGameHighscoresViewController;

        public LeaderboardMenuViewController GetInGameLeaderboardView() => this.m_inGameLeaderboardViewController;

        public bool HasSavedState() => this.m_savedState != null;

        public static GameApplication Instance => (GameApplication)Application.Instance;

        public static GameApplication GetInstance() => (GameApplication)Application.Instance;

        public Font GetFont() => this.m_font;

        public GameApplication()
        {
            this.ExitRequest = false;
            this.m_gameMode = GameApplication.GameMode.Survival;
            this.m_difficulty = GameApplication.GameDifficulty.Normal;
            this.m_graphics = new GraphicsDeviceManager((Microsoft.Xna.Framework.Game)this);
            this.m_graphics.PreferredBackBufferWidth = 520;
            this.m_graphics.PreferredBackBufferHeight = 320;
            this.m_graphics.PreferredDepthStencilFormat = DepthFormat.None;
            this.m_graphics.IsFullScreen = true;
            this.m_graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
            this.m_graphics.SynchronizeWithVerticalRetrace = true;
            this.Content.RootDirectory = "Content";
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / 30.0);
            this.MusicGain = 1f;
            this.SfxGain = 1f;
            this.HudAlpha = 1f;
            this.CurrentLanguage = GameLocalisation.LanguageId.English;
            this.Score = 0;
            this.AchievementCounter_100ZombiesKilled = 0;
            this.AchievementCounter_500ZombiesKilled = 0;
            this.AchievementCounter_1000ZombiesKilled = 0;
            this.AchievementCounter_500ZombiesKilledAirSlash = 0;
            this.AchievementCounter_500ZombiesKilledBullet = 0;
            this.AchievementCounter_500ZombiesKilledFlameThrower = 0;
            this.AchievementCounter_500ZombiesKilledIceGun = 0;
            this.AchievementCounter_500ZombiesKilledExplosiveGun = 0;
            this.AchievementCounter_1000ZombiesKilledHolyBeam = 0;
            this.AchievementCounter_100Ungrab = 0;
            this.Achievement_OneLife = true;
            this.Achievement_NoGunUpgrades = false;
            for (int index = 0; index < 6; ++index)
                this.m_outgameSounds[index] = new Sound();
        }

        protected override void OnDeactivated(object sender, EventArgs args)
        {
            this.m_savedState = new int[5];
            this.m_savedState[0] = 0;
            this.m_savedState[1] = (int)this.m_gameMode;
            this.m_savedState[2] = (int)this.m_difficulty;
            this.m_savedState[3] = this.Score;
            this.m_savedState[4] = !GuideManager.Instance.IsTrialMode() ? 0 : 1;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance() || GameFSM.GetInstance().GetCurrentState() == GameStateLoadLevel.GetInstance())
                this.m_savedState[0] = 1;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance() && ((GameWorld)GameWorldManager.GetInstance().GetCurrentWorld()).GetHero().HitPoints <= 0)
                this.m_savedState[0] = 4;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateMap.GetInstance())
                this.m_savedState[0] = 2;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateEndOfNight.GetInstance())
                this.m_savedState[0] = GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story || Scenario.GetInstance().GetCurrentMissionIndex() != 0 ? 3 : 1;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateEnding.GetInstance())
                this.m_savedState[0] = 5;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateEndingAnimation.GetInstance())
                this.m_savedState[0] = 6;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateEndingStory.GetInstance())
                this.m_savedState[0] = 7;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateStoryEpilogOpenMap.GetInstance())
                this.m_savedState[0] = 8;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateStoryEpilogSuicide.GetInstance())
                this.m_savedState[0] = 9;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateStoryIntro.GetInstance())
                this.m_savedState[0] = 10;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateLogoLive.GetInstance() || GameFSM.GetInstance().GetCurrentState() == GameStateLogoMGS.GetInstance() || GameFSM.GetInstance().GetCurrentState() == GameStateLogo.GetInstance())
                this.m_savedState[0] = 11;
            //PhoneApplicationService.Current.State[GameApplication.STATE_KEY] = (object) this.m_savedState;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
            {
                InGameStateStartLevel.GetInstance().InGameMenuRequest = true;
                InGameStatePlay.GetInstance().InGameMenuRequest = true;
            }
            base.OnDeactivated(sender, args);
        }

        protected override void OnActivated(object sender, EventArgs args)
        {
            if (this.m_savedState != null)
            {
                GuideManager.Instance.UpdateIsTrial();
                if (!GuideManager.Instance.IsTrialMode() && this.m_savedState[4] == 1)
                {
                    if (GameFSM.GetInstance().GetCurrentState() == GameStateFeatures.GetInstance())
                        GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    //AchievementManager.Instance.SynchronizeAchievementsRequest = true;
                }
            }
            base.OnActivated(sender, args);
        }

        private void Rehydrate()
        {
            if (this.m_savedState == null)
                return;
            GameApplication.Instance.LoadSettings();
            GameApplication.Instance.SetSoundVolume();
            GameLocalisation.GetInstance().Initialize(GameApplication.Instance.CurrentLanguage);
            GameApplication.Instance.LoadHighscores();
            GameApplication.Instance.LoadAchievements();
            //AchievementManager.Instance.ReadAchievements();
            //AchievementManager.Instance.SynchronizeAchievementsRequest = true;
            this.m_gameMode = (GameApplication.GameMode)this.m_savedState[1];
            this.m_difficulty = (GameApplication.GameDifficulty)this.m_savedState[2];
            this.Score = this.m_savedState[3];
            switch (this.m_savedState[0])
            {
                case 0:
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    break;
                case 1:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    GameStateMainMenu.Instance.LevelRequest = true;
                    break;
                case 2:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    break;
                case 3:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    GameStateMainMenu.Instance.EndOfNightRequest = true;
                    break;
                case 4:
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameApplication instance = GameApplication.GetInstance();
                    instance.SendScore(instance.Score);
                    if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                    {
                        GameStateMainMenu.GetInstance().GetSubStateMachine().SetState((IState)MainMenuStateHighscoresStory.GetInstance());
                        GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = true;
                        GameStateMainMenu.GetInstance().GetLeaderboardView().StoryMode = true;
                    }
                    else
                    {
                        GameStateMainMenu.GetInstance().GetSubStateMachine().SetState((IState)MainMenuStateHighscoresSurvival.GetInstance());
                        GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = false;
                        GameStateMainMenu.GetInstance().GetLeaderboardView().StoryMode = false;
                    }
                    int num = instance.UpdateHighscores();
                    GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(num + 1);
                    GameStateMainMenu.GetInstance().GetLeaderboardView().BlinkScore(true);
                    if (num < 5)
                    {
                        GameApplication.Instance.SaveHighscores();
                        break;
                    }
                    break;
                case 5:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    GameStateMainMenu.Instance.EndingRequest = true;
                    break;
                case 6:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    GameStateMainMenu.Instance.EndingAnimationRequest = true;
                    break;
                case 7:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    GameStateMainMenu.Instance.EndingStoryRequest = true;
                    break;
                case 8:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    GameStateMainMenu.Instance.EpilogOpenMapRequest = true;
                    break;
                case 9:
                    GameStateMainMenu.Instance.MuteMusic = true;
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    GameStateMainMenu.Instance.ContinueGameRequest = true;
                    GameStateMainMenu.Instance.EpilogSuicideRequest = true;
                    break;
                case 10:
                    GameFSM.GetInstance().SetState((IState)GameStateMainMenu.GetInstance());
                    GameStateMainMenu.Instance.StartGameRequest = true;
                    break;
            }
            this.m_savedState = (int[])null;
        }

        protected override void Initialize()
        {
            base.Initialize();
            //TODO
           // if (PhoneApplicationService.Current.StartupMode == 1)
              this.m_savedState = (int[]) null;
            //else if (PhoneApplicationService.Current.StartupMode == null)
            //  this.m_savedState = PhoneApplicationService.Current.State[GameApplication.STATE_KEY] as int[];
            
            this.ExitRequest = false;
            FileManager.Instance.Initialize();
            GuideManager.Instance.Initialize();
            // LeaderboardManager.Instance.Initialize();
            //AchievementManager.Instance.Initialize();
            Prefs.Instance.Initialize();
            GameWorldManager.Instance.Initialize();
            InputManager.GetInstance().SetDeadZones(0.1f, 0.3f);
            InputManager.GetInstance().EnableStandardNavigationGameActions(PlayerIndex.One);
            this.m_outgameSounds[0].Initialize("Sounds\\otg_cancel", false);
            this.m_outgameSounds[1].Initialize("Sounds\\otg_next", false);
            this.m_outgameSounds[2].Initialize("Sounds\\otg_previous", false);
            this.m_outgameSounds[3].Initialize("Sounds\\otg_select", false);
            this.m_outgameSounds[4].Initialize("Sounds\\otg_upgrade", false);
            this.m_outgameSounds[5].Initialize("Sounds\\otg_validate", false);
            switch (CultureInfo.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "en":
                    this.PlatformLanguage = GameLocalisation.LanguageId.English;
                    break;
                case "fr":
                    this.PlatformLanguage = GameLocalisation.LanguageId.French;
                    break;
                case "it":
                    this.PlatformLanguage = GameLocalisation.LanguageId.Italian;
                    break;
                case "de":
                    this.PlatformLanguage = GameLocalisation.LanguageId.German;
                    break;
                case "es":
                    this.PlatformLanguage = GameLocalisation.LanguageId.Spanish;
                    break;
                default:
                    this.PlatformLanguage = GameLocalisation.LanguageId.English;
                    break;
            }
            this.Score = 0;
            this.CurrentLanguage = this.PlatformLanguage;
            this.m_assemblyVersionString = "1.3.0.0";
            GameFSM.Instance.Initialize();
            this.m_textBoxViewController.Initialize();
            this.AddResponder((Responder)this.m_textBoxViewController);
            this.GetInGameMenuView().Initialize();
            this.GetInGameHelpView().Initialize();
            this.GetInGameHelpMapView().Initialize();
            this.GetInGameHelpEONView().Initialize();
            this.GetInGameHelpAndOptionsView().Initialize(false);
            this.GetInGameOptionsView().Initialize(false);
            this.GetInGameLanguageSelectionView().Initialize(false);
            this.GetInGameBackToMapConfirmationView().Initialize();
            this.GetInGameBackToMainMenuConfirmationView().Initialize();
            this.GetInGameExitConfirmationView().Initialize(false);
            this.GetInGameAchievementsView().Initialize(false);
            this.GetInGameHighscoresChooseView().Initialize(false);
            this.GetInGameHighscoresView().Initialize(0, false);
            this.GetInGameLeaderboardView().Initialize(false);
            Scenario.GetInstance().Initialize();
            this.Rehydrate();
        }

        protected override void LoadContent()
        {
            this.m_font = ResourceManager.GetInstance().LoadFont("Fonts/brad.xml");
            ResourceManager.GetInstance().LoadAllResources();
            base.LoadContent();
        }

        protected override void UnloadContent()
        {
            ResourceManager.GetInstance().DestroyUnusedResources();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (this.ExitRequest)
            {
                this.Exit();
            }
            else
            {
                //try
                //{
                //    //LeaderboardManager.Instance.Update();
                //    //AchievementManager.Instance.Update();
                //    GuideManager.Instance.Update();
                //    base.Update(gameTime);
                //}
                //catch (GameUpdateRequiredException ex)
                //{
                //    GuideManager.Instance.RequestShowGameUpdateRequired = true;
                //}
                //catch (Exception ex)
                //{
                //}
                //if (Guide.IsVisible)
                //    InGameStatePlay.Instance.InGameMenuRequest = true;
                GameFSM.Instance.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                this.m_textBoxViewController.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (this.m_gameMode != GameApplication.GameMode.Story)
                    return;
                if (GameFSM.Instance.CurrentState == GameStateInGame.GetInstance() && GameStateInGame.GetInstance().IsMenuInGameActive())
                {
                    if (Scenario.GetInstance().GetCurrentMissionIndex() != 0 || Scenario.GetInstance().GetCurrentMissionPart() != 0)
                        return;
                    Scenario.GetInstance().Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                else
                    Scenario.GetInstance().Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GameFSM.Instance.Draw();
            this.m_textBoxViewController.Draw();
            base.Draw(gameTime);
        }

        protected override void Cleanup()
        {
            this.ResetMediaPlayerVolume();
            for (int index = 0; index < 6; ++index)
                this.m_outgameSounds[index].Cleanup();
            Prefs.Instance.Cleanup();
            GameWorldManager.Instance.Cleanup();
            GameFSM.Instance.Cleanup();
            this.m_textBoxViewController.Cleanup();
            this.GetInGameMenuView().Cleanup();
            this.GetInGameHelpView().Cleanup();
            this.GetInGameHelpMapView().Cleanup();
            this.GetInGameHelpEONView().Cleanup();
            this.GetInGameHelpAndOptionsView().Cleanup();
            this.GetInGameOptionsView().Cleanup();
            this.GetInGameLanguageSelectionView().Cleanup();
            this.GetInGameBackToMapConfirmationView().Cleanup();
            this.GetInGameBackToMainMenuConfirmationView().Cleanup();
            this.GetInGameExitConfirmationView().Cleanup();
            this.GetInGameAchievementsView().Cleanup();
            this.GetInGameHighscoresChooseView().Cleanup();
            this.GetInGameHighscoresView().Cleanup();
            this.GetInGameLeaderboardView().Cleanup();
            Scenario.GetInstance().Cleanup();
            //LeaderboardManager.Instance.Cleanup();
            //AchievementManager.Instance.Cleanup();
            GuideManager.Instance.Cleanup();
            FileManager.Instance.Cleanup();
            GameEngine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
            base.Cleanup();
        }

        public void SetDefaultSettings()
        {
            this.MusicGain = 1f;
            this.SfxGain = 1f;
            this.HudAlpha = 1f;
            this.CurrentLanguage = this.PlatformLanguage;
        }

        public void SaveSettings()
        {
            Prefs.UserSettings cachedUserSettings = Prefs.Instance.CachedUserSettings;
            cachedUserSettings.languageId = (int)this.CurrentLanguage;
            cachedUserSettings.musicGain = this.MusicGain;
            cachedUserSettings.sfxGain = this.SfxGain;
            cachedUserSettings.hudAlpha = this.HudAlpha;
            Prefs.Instance.WriteUserSettings();
        }

        public void LoadSettings()
        {
            Prefs.UserSettings cachedUserSettings = Prefs.Instance.CachedUserSettings;
            if (!Prefs.Instance.ReadUserSettings())
            {
                this.SetDefaultSettings();
            }
            else
            {
                this.MusicGain = cachedUserSettings.musicGain;
                this.SfxGain = cachedUserSettings.sfxGain;
                this.HudAlpha = cachedUserSettings.hudAlpha;
                this.CurrentLanguage = (GameLocalisation.LanguageId)cachedUserSettings.languageId;
            }
        }

        public void LoadAchievements()
        {
            if (Prefs.Instance.ReadAchievements())
                return;
            Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
            cachedAchievements.achievement_Survival_500ZombiesKilledBullet = false;
            cachedAchievements.achievement_Survival_500ZombiesKilledFlameThrower = false;
            cachedAchievements.achievement_Survival_500ZombiesKilledIceGun = false;
            cachedAchievements.achievement_Survival_500ZombiesKilledExplosiveGun = false;
            cachedAchievements.achievement_Survival_1000ZombiesKilledHolyBeam = false;
            cachedAchievements.achievement_Survival_500ZombiesKilledAirSlash = false;
            cachedAchievements.achievement_Survival_100Ungrab = false;
            cachedAchievements.achievement_Survival_AllWeaponsUpgraded = false;
            cachedAchievements.achievement_Survival_AllAbilitiesUpgraded = false;
            cachedAchievements.achievement_Survival_GameFinished = false;
            cachedAchievements.achievement_Survival_GameFinishedHard = false;
            cachedAchievements.achievement_Story_100ZombiesKilled = false;
            cachedAchievements.achievement_Story_500ZombiesKilled = false;
            cachedAchievements.achievement_Story_1000ZombiesKilled = false;
            cachedAchievements.achievement_Story_Boss1Killed = false;
            cachedAchievements.achievement_Story_Boss2Killed = false;
            cachedAchievements.achievement_Story_Boss3Killed = false;
            cachedAchievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded = false;
            cachedAchievements.achievement_Story_GameFinished = false;
            cachedAchievements.achievement_Story_GameFinishedHard = false;
        }

        public void SaveAchievements() => Prefs.Instance.WriteAchievements();

        public void SetSoundVolume()
        {
            AudioManager.GetInstance().SetMusicGain(this.MusicGain);
            AudioManager.GetInstance().SetListenerGain(this.SfxGain);
        }

        public void ResetMediaPlayerVolume() => AudioManager.GetInstance().SetMusicGain(1f);

        public void PlayOutgameSound(GameApplication.OutgameSound sound) => this.m_outgameSounds[(int)sound].Play();

        public int[] GetHighscoresSurvival() => this.m_highscoresSurvival;

        public int[] GetHighscoresStory() => this.m_highscoresStory;

        public int UpdateHighscores()
        {
            if (this.GetGameMode() == GameApplication.GameMode.Story)
            {
                for (int index1 = 0; index1 < 5; ++index1)
                {
                    if (this.m_highscoresStory[index1] <= this.Score)
                    {
                        for (int index2 = 4; index2 > index1; --index2)
                            this.m_highscoresStory[index2] = this.m_highscoresStory[index2 - 1];
                        this.m_highscoresStory[index1] = this.Score;
                        return index1;
                    }
                }
            }
            else
            {
                for (int index3 = 0; index3 < 5; ++index3)
                {
                    if (this.m_highscoresSurvival[index3] <= this.Score)
                    {
                        for (int index4 = 4; index4 > index3; --index4)
                            this.m_highscoresSurvival[index4] = this.m_highscoresSurvival[index4 - 1];
                        this.m_highscoresSurvival[index3] = this.Score;
                        return index3;
                    }
                }
            }
            return 5;
        }

        public void LoadHighscores()
        {
            if (Prefs.Instance.ReadHighscores())
            {
                for (int index = 0; index < 5; ++index)
                    this.m_highscoresSurvival[index] = Prefs.Instance.CachedHighscores.highscoresSurvival[index];
                for (int index = 0; index < 5; ++index)
                    this.m_highscoresStory[index] = Prefs.Instance.CachedHighscores.highscoresStory[index];
            }
            else
                this.InitHighscores();
        }

        public void SaveHighscores()
        {
            for (int index = 0; index < 5; ++index)
                Prefs.Instance.CachedHighscores.highscoresSurvival[index] = this.m_highscoresSurvival[index];
            for (int index = 0; index < 5; ++index)
                Prefs.Instance.CachedHighscores.highscoresStory[index] = this.m_highscoresStory[index];
            Prefs.Instance.WriteHighscores();
        }

        public void InitHighscores()
        {
            this.m_highscoresSurvival[0] = 100000;
            this.m_highscoresSurvival[1] = 50000;
            this.m_highscoresSurvival[2] = 25000;
            this.m_highscoresSurvival[3] = 10000;
            this.m_highscoresSurvival[4] = 0;
            this.m_highscoresStory[0] = 100000;
            this.m_highscoresStory[1] = 50000;
            this.m_highscoresStory[2] = 25000;
            this.m_highscoresStory[3] = 10000;
            this.m_highscoresStory[4] = 0;
        }

        public void SendScore(int score)
        {
            //if (this.m_gameMode == GameApplication.GameMode.Story)
            // // LeaderboardManager.Instance.WriteBestScore(score, 0);
            //else
            ////  LeaderboardManager.Instance.WriteBestScore(score, 1);
        }

        public void CheckBossKillAchievements(NPCBoss boss)
        {
            Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
            bool flag = false;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            if (boss.GetBossType() == NPCBoss.BossType.ButcherForm1 && !cachedAchievements.achievement_Story_Boss1Killed)
            {
                cachedAchievements.achievement_Story_Boss1Killed = true;
                flag = true;
                //AchievementManager.Instance.AwardAchievement("Boss1");
            }
            if (boss.GetBossType() == NPCBoss.BossType.NecromancerForm1 && !cachedAchievements.achievement_Story_Boss2Killed)
            {
                cachedAchievements.achievement_Story_Boss2Killed = true;
                flag = true;
                //AchievementManager.Instance.AwardAchievement("Boss2");
            }
            if (boss.GetBossType() == NPCBoss.BossType.NecromancerForm2 && !cachedAchievements.achievement_Story_Boss3Killed)
            {
                cachedAchievements.achievement_Story_Boss3Killed = true;
                flag = true;
                //AchievementManager.Instance.AwardAchievement("Boss3");
            }
            if (!flag)
                return;
            this.SaveAchievements();
        }

        public void CheckBossNecro2KillAchievement()
        {
            Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
            if (cachedAchievements.achievement_Story_Boss3Killed)
                return;
            cachedAchievements.achievement_Story_Boss3Killed = true;
            //AchievementManager.Instance.AwardAchievement("Boss3");
            this.SaveAchievements();
        }

        public void CheckKillsAchievements()
        {
            Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
            bool flag = false;
            if (this.AchievementCounter_100ZombiesKilled >= 100)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && !cachedAchievements.achievement_Story_100ZombiesKilled)
                {
                    cachedAchievements.achievement_Story_100ZombiesKilled = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("100KillStory");
                }
                this.AchievementCounter_100ZombiesKilled = 0;
            }
            if (this.AchievementCounter_500ZombiesKilled >= 500)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && !cachedAchievements.achievement_Story_500ZombiesKilled)
                {
                    cachedAchievements.achievement_Story_500ZombiesKilled = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("500KillStory");
                }
                this.AchievementCounter_500ZombiesKilled = 0;
            }
            if (this.AchievementCounter_1000ZombiesKilled >= 1000)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && !cachedAchievements.achievement_Story_1000ZombiesKilled)
                {
                    cachedAchievements.achievement_Story_1000ZombiesKilled = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("1000KillStory");
                }
                this.AchievementCounter_1000ZombiesKilled = 0;
            }
            if (this.AchievementCounter_500ZombiesKilledAirSlash >= 500)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival && !cachedAchievements.achievement_Survival_500ZombiesKilledAirSlash)
                {
                    cachedAchievements.achievement_Survival_500ZombiesKilledAirSlash = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("500KillAirSlashSurvival");
                }
                this.AchievementCounter_500ZombiesKilledAirSlash = 0;
            }
            if (this.AchievementCounter_500ZombiesKilledBullet >= 500)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival && !cachedAchievements.achievement_Survival_500ZombiesKilledBullet)
                {
                    cachedAchievements.achievement_Survival_500ZombiesKilledBullet = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("500KillGunSurvival");
                }
                this.AchievementCounter_500ZombiesKilledBullet = 0;
            }
            if (this.AchievementCounter_500ZombiesKilledFlameThrower >= 500)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival && !cachedAchievements.achievement_Survival_500ZombiesKilledFlameThrower)
                {
                    cachedAchievements.achievement_Survival_500ZombiesKilledFlameThrower = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("500KillFlameSurvival");
                }
                this.AchievementCounter_500ZombiesKilledFlameThrower = 0;
            }
            if (this.AchievementCounter_500ZombiesKilledIceGun >= 500)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival && !cachedAchievements.achievement_Survival_500ZombiesKilledIceGun)
                {
                    cachedAchievements.achievement_Survival_500ZombiesKilledIceGun = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("500KillIceSurvival");
                }
                this.AchievementCounter_500ZombiesKilledIceGun = 0;
            }
            if (this.AchievementCounter_500ZombiesKilledExplosiveGun >= 500)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival && !cachedAchievements.achievement_Survival_500ZombiesKilledExplosiveGun)
                {
                    cachedAchievements.achievement_Survival_500ZombiesKilledExplosiveGun = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("500KillExplodeSurvival");
                }
                this.AchievementCounter_500ZombiesKilledExplosiveGun = 0;
            }
            if (this.AchievementCounter_1000ZombiesKilledHolyBeam >= 1000)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival && !cachedAchievements.achievement_Survival_1000ZombiesKilledHolyBeam)
                {
                    cachedAchievements.achievement_Survival_1000ZombiesKilledHolyBeam = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("1000KillHolySurvival");
                }
                this.AchievementCounter_1000ZombiesKilledHolyBeam = 0;
            }
            if (this.AchievementCounter_100Ungrab >= 100)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival && !cachedAchievements.achievement_Survival_100Ungrab)
                {
                    cachedAchievements.achievement_Survival_100Ungrab = true;
                    flag = true;
                    //AchievementManager.Instance.AwardAchievement("100ShakeSurvival");
                }
                this.AchievementCounter_100Ungrab = 0;
            }
            if (!flag)
                return;
            this.SaveAchievements();
        }

        public void CheckUpgradesAchievements()
        {
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
            {
                if (!this.CheckGunUpgradesAchievements() || !this.CheckAbilitiesUpgradesAchievements())
                    return;
                Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
                if (cachedAchievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded)
                    return;
                cachedAchievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded = true;
                //AchievementManager.Instance.AwardAchievement("AllUpgradeStory");
                this.SaveAchievements();
            }
            else
            {
                if (!this.CheckGunUpgradesAchievements() && !this.CheckAbilitiesUpgradesAchievements())
                    return;
                this.SaveAchievements();
            }
        }

        public bool CheckGunUpgradesAchievements()
        {
            Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
            int num = 0;
            Gun[] gunArray = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero().GetGunArray();
            for (int index = 0; index < 6; ++index)
            {
                if (gunArray[index].GetLevel() >= 5)
                    ++num;
            }
            if (num >= 6)
            {
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                    return true;
                if (!cachedAchievements.achievement_Survival_AllWeaponsUpgraded)
                {
                    cachedAchievements.achievement_Survival_AllWeaponsUpgraded = true;
                    //AchievementManager.Instance.AwardAchievement("AllWeaponsSurvival");
                    return true;
                }
            }
            return false;
        }

        public bool CheckAbilitiesUpgradesAchievements()
        {
            //Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
            //int num = 0;
            //GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
            //if (currentWorld.GetHero().SlashDamageLevel == 5)
            //  ++num;
            //if (currentWorld.GetHero().SlashManaDrainLevel == 5)
            //  ++num;
            //if (currentWorld.GetHero().HitPointsLevel == 5)
            //  ++num;
            //if (currentWorld.GetHero().ManaPointsLevel == 5)
            //  ++num;
            //if (currentWorld.GetHero().AirSlashDamageLevel == 5)
            //  ++num;
            //if (num >= 5)
            //{
            //  if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
            //    return true;
            //  if (!cachedAchievements.achievement_Survival_AllAbilitiesUpgraded)
            //  {
            //    cachedAchievements.achievement_Survival_AllAbilitiesUpgraded = true;
            //   //AchievementManager.Instance.AwardAchievement("AllAbilitiesSurvival");
            //    return true;
            //  }
            //}
            return false;
        }

        public void CheckEndingAchievements()
        {
            //Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
            //bool flag = false;
            //if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
            //{
            //  if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
            //  {
            //    if (!cachedAchievements.achievement_Story_GameFinishedHard)
            //    {
            //      cachedAchievements.achievement_Story_GameFinishedHard = true;
            //      flag = true;
            //     //AchievementManager.Instance.AwardAchievement("FinishStoryHard");
            //    }
            //  }
            //  else if (!cachedAchievements.achievement_Story_GameFinished)
            //  {
            //    cachedAchievements.achievement_Story_GameFinished = true;
            //    flag = true;
            //   //AchievementManager.Instance.AwardAchievement("FinishStory");
            //  }
            //}
            //else if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
            //{
            //  if (!cachedAchievements.achievement_Survival_GameFinishedHard)
            //  {
            //    cachedAchievements.achievement_Survival_GameFinishedHard = true;
            //    flag = true;
            //   //AchievementManager.Instance.AwardAchievement("FinishSurvivalHard");
            //  }
            //}
            //else if (!cachedAchievements.achievement_Survival_GameFinished)
            //{
            //  cachedAchievements.achievement_Survival_GameFinished = true;
            //  flag = true;
            // //AchievementManager.Instance.AwardAchievement("FinishSurvival");
            //}
            //if (!flag)
            //  return;
            this.SaveAchievements();
        }

        public string GetAssemblyVersionString() => this.m_assemblyVersionString;

        public GameApplication.GameMode GetGameMode() => this.m_gameMode;

        public void SetGameMode(GameApplication.GameMode mode) => this.m_gameMode = mode;

        public GameApplication.GameDifficulty GetDifficulty() => this.m_difficulty;

        public void SetDifficulty(GameApplication.GameDifficulty d) => this.m_difficulty = d;

        public enum OutgameSound
        {
            Cancel,
            Next,
            Previous,
            Select,
            Upgrade,
            Validate,
            Number,
        }

        public enum GameAchievement
        {
            Achievement_Survival_500ZombiesKilledAirSlash,
            Achievement_Survival_500ZombiesKilledBullet,
            Achievement_Survival_500ZombiesKilledFlameThrower,
            Achievement_Survival_500ZombiesKilledIceGun,
            Achievement_Survival_500ZombiesKilledExplosiveGun,
            Achievement_Survival_1000ZombiesKilledHolyBeam,
            Achievement_Survival_100Ungrab,
            Achievement_Survival_AllWeaponsUpgraded,
            Achievement_Survival_AllAbilitiesUpgraded,
            Achievement_Survival_FinishedTheGame,
            Achievement_Survival_GameFinishedHard,
            Achievement_Story_100ZombiesKilled,
            Achievement_Story_500ZombiesKilled,
            Achievement_Story_1000ZombiesKilled,
            Achievement_Story_Boss1Killed,
            Achievement_Story_Boss2Killed,
            Achievement_Story_Boss3Killed,
            Achievement_Story_AllWeaponsAndAbilitiesUpgraded,
            Achievement_Story_GameFinished,
            Achievement_Story_GameFinishedHard,
            Achievement_Number,
        }

        public enum GameMode
        {
            Story,
            Survival,
            Number,
        }

        public enum GameDifficulty
        {
            Normal,
            Hard,
        }

        private enum SavedStateId
        {
            MainMenu,
            InGame,
            Map,
            EndOfNight,
            InGameGameOver,
            Ending,
            EndingAnimation,
            EndingStory,
            EpilogOpenMap,
            EpilogSuicide,
            StoryIntro,
            Logo,
            Number,
        }

        private enum SavedStateData
        {
            StateId,
            GameMode,
            Difficulty,
            Score,
            DemoMode,
            Size,
        }
    }
}
