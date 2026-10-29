using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.World;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.FSM.GameState.MainMenu;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateInGame : IDrawableState, IState
    {
        private static GameStateInGame m_instance = new GameStateInGame();
        public bool NextLevelRequest;
        public bool BackToMap;
        public bool ShopRequest;
        public bool BackToMainMenuRequest;
        public bool HighscoresRequest;
        private bool m_deleteWorld;
        private DrawableFiniteStateMachine m_subStateMachine;

        public static GameStateInGame Instance => m_instance;

        public static GameStateInGame GetInstance() => m_instance;

        public GameStateInGame()
        {
            this.m_subStateMachine = new DrawableFiniteStateMachine();
            this.NextLevelRequest = false;
            this.BackToMap = false;
            this.ShopRequest = false;
            this.BackToMainMenuRequest = false;
            this.HighscoresRequest = false;
            this.m_deleteWorld = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.m_deleteWorld = false;
            this.NextLevelRequest = false;
            this.BackToMap = false;
            this.ShopRequest = false;
            this.BackToMainMenuRequest = false;
            this.HighscoresRequest = false;
            GameApplication instance = GameApplication.GetInstance();
            instance.SetSoundVolume();
            instance.Achievement_NoGunUpgrades = false;
            if (((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
                AudioManager.GetInstance().SetMusicGain(instance.MusicGain * 0.8f);
            else
                AudioManager.GetInstance().SetMusicGain(instance.MusicGain);
            AudioManager.GetInstance().PlayMusic();
            this.m_subStateMachine.SetState(InGameStateStartLevel.GetInstance());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            this.m_subStateMachine.Update(gameTime);
            if (this.BackToMainMenuRequest || this.HighscoresRequest)
            {
                this.BackToMainMenuRequest = false;
                this.m_deleteWorld = true;
                GameFSM.GetInstance().SetState(GameStateMainMenu.GetInstance());
                if (!this.HighscoresRequest)
                    return;
                this.HighscoresRequest = false;
                GameApplication instance = GameApplication.GetInstance();
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                {
                    GameStateMainMenu.GetInstance().GetSubStateMachine().SetState(MainMenuStateHighscoresStory.GetInstance());
                    GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = true;
                }
                else
                {
                    GameStateMainMenu.GetInstance().GetSubStateMachine().SetState(MainMenuStateHighscoresSurvival.GetInstance());
                    GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = false;
                }
                int num = instance.UpdateHighscores();
                GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(num + 1);
                if (num >= 5)
                    return;
                GameApplication.Instance.SaveHighscores();
            }
            else if (this.ShopRequest)
            {
                this.ShopRequest = false;
                GameFSM.GetInstance().SetState(GameStateEndOfNight.GetInstance());
            }
            else if (this.BackToMap)
            {
                this.BackToMap = false;
                GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
                currentWorld.NextDayPart();
                currentWorld.SaveGameData();
                GameFSM.GetInstance().SetState(GameStateMap.GetInstance());
            }
            else
            {
                if (!this.NextLevelRequest)
                    return;
                this.NextLevelRequest = false;
                this.m_deleteWorld = false;
                bool flag = false;
                GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                {
                    currentWorld.GetCurrentLevel().SetInfestationLevel(0);
                    currentWorld.NextDayPart();
                    currentWorld.SaveGameData();
                    if (currentWorld.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
                        GameFSM.GetInstance().SetState(GameStateEndingAnimation.GetInstance());
                    else
                        GameFSM.GetInstance().SetState(GameStateMap.GetInstance());
                }
                else
                {
                    if (currentWorld.NextLevel())
                    {
                        ++currentWorld.DayIndex;
                        flag = true;
                        currentWorld.SaveGameData();
                        if (currentWorld.DayIndex > 31)
                        {
                            this.m_deleteWorld = true;
                            GameApplication instance = GameApplication.GetInstance();
                            int score = instance.Score;
                            instance.Score = 0;
                            currentWorld.DayIndex = 31;
                            currentWorld.SaveGameData();
                            instance.Score = score;
                            int num = 0;
                            Gun[] gunArray = currentWorld.GetHero().GetGunArray();
                            for (int index = 0; index < 6; ++index)
                            {
                                if (gunArray[index].IsAvailable)
                                    ++num;
                            }
                            if (num == 1 && gunArray[0].GetLevel() == 1)
                                instance.Achievement_NoGunUpgrades = true;
                            GameFSM.GetInstance().SetState(GameStateEnding.GetInstance());
                            return;
                        }
                    }
                    else
                        currentWorld.SaveGameData();
                    if (flag)
                        GameFSM.GetInstance().SetState(GameStateEndOfNight.GetInstance());
                    else
                        GameFSM.GetInstance().SetState(GameStateLoadLevel.GetInstance());
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            Engine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
            this.m_subStateMachine.GetCurrentState().Exit(this.m_subStateMachine);
            if (this.m_deleteWorld)
                WorldManager.GetInstance().Cleanup(false, true);
            else
                WorldManager.GetInstance().GetCurrentWorld().Cleanup();
            AudioManager.GetInstance().StopAll();
            AudioManager.GetInstance().UnloadMusic();
            InputManager.GetInstance().ClearPadState();
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            Engine.Graphics.Renderer.Renderer.GetInstance().SetViewMatrix(currentWorld.GetCamera().GetViewMatrix());
            Engine.Graphics.Renderer.Renderer.GetInstance().Draw(false, currentWorld.GetCamera().GetBoundingBox());
            currentWorld.GetHud().Draw();
            ((IDrawableState)this.m_subStateMachine.GetCurrentState()).Draw(this.m_subStateMachine);
        }

        public bool IsMenuInGameActive() => this.IsMenuInGameActive(this.m_subStateMachine.GetCurrentState());

        public bool IsMenuInGameActiveInPreviousState() => this.IsMenuInGameActive(this.m_subStateMachine.GetPreviousState());

        public bool IsMenuInGameActive(IState state) => state == InGameMenuStateLanguageSelection.GetInstance() || state == InGameMenuStateHelpAndOptions.GetInstance() || state == InGameMenuStateOptions.GetInstance() || state == InGameStateHelp.GetInstance() || state == InGameStateHelpEON.GetInstance() || state == InGameStateHelpMap.GetInstance() || state == InGameStateMenu.GetInstance() || state == InGameMenuStateAchievements.GetInstance() || state == InGameMenuStateHighscoresChoose.GetInstance() || state == InGameMenuStateHighscoresStory.GetInstance() || state == InGameMenuStateHighscoresSurvival.GetInstance() || state == InGameMenuStateBackToMapConfirmation.GetInstance() || state == InGameMenuStateBackToMainMenuConfirmation.GetInstance() || state == InGameMenuStateExitConfirmation.GetInstance();
    }
}
