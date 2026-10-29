using Resanctified.Engine.Maths;
using Resanctified.Engine.Sounds;
using Resanctified.Game.FSM;
using Resanctified.Game.FSM.GameState;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.UI;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game
{
    public class Scenario
    {
        public const int SCENARIO_MISSION_INDEX_NECRO1_KILLED = 9;
        public const int SCENARIO_MISSION_INDEX_END = 20;
        public const int SCENARIO_MISSION_INDEX_OPENMAP = 99;
        private static readonly Scenario s_instance = new Scenario();
        private int m_currentMissionIndex;
        private int m_missionPart;
        private GameWorld.LevelId m_currentLevel;
        private bool m_eventSelectLevel;
        private bool m_eventEndLevel;
        private bool m_eventNextText;
        private bool m_eventNextText2;
        private bool m_eventExitShop;
        private bool m_enableTextBox;
        private float m_timer;
        public bool DisplayHelp;

        public static Scenario Instance => s_instance;

        public static Scenario GetInstance() => s_instance;

        public Scenario()
        {
            this.m_currentMissionIndex = 0;
            this.m_missionPart = 0;
            this.m_currentLevel = GameWorld.LevelId.Level_Shop;
            this.m_eventSelectLevel = false;
            this.m_eventEndLevel = false;
            this.m_eventNextText = false;
            this.m_eventNextText2 = false;
            this.m_eventExitShop = false;
            this.m_enableTextBox = false;
            this.m_timer = 0.0f;
            this.DisplayHelp = false;
        }

        public void RaiseEventSelectLevel(GameWorld.LevelId levelId)
        {
            this.m_currentLevel = levelId;
            this.m_eventSelectLevel = true;
        }

        public void RaiseEventEndLevel(GameWorld.LevelId levelId)
        {
            this.m_currentLevel = levelId;
            this.m_eventEndLevel = true;
        }

        public void RaiseEventExitShop() => this.m_eventExitShop = true;

        public void RaiseEventNextText() => this.m_eventNextText = true;

        public void RaiseEventNextText2() => this.m_eventNextText2 = true;

        public void Initialize()
        {
            this.m_currentMissionIndex = 0;
            this.m_missionPart = 0;
            this.m_currentLevel = GameWorld.LevelId.Level_Shop;
            this.m_eventSelectLevel = false;
            this.m_eventEndLevel = false;
            this.m_eventNextText = false;
            this.m_eventNextText2 = false;
            this.m_eventExitShop = false;
            this.m_enableTextBox = false;
            this.DisplayHelp = false;
        }

        public void Cleanup()
        {
            this.m_eventSelectLevel = false;
            this.m_eventEndLevel = false;
            this.m_eventNextText = false;
            this.m_eventNextText2 = false;
            this.m_eventExitShop = false;
            this.m_enableTextBox = false;
            this.DisplayHelp = false;
        }

        public void InitializeMission()
        {
            switch (this.m_currentMissionIndex)
            {
                case 0:
                    this.InitializeMission0();
                    break;
                case 1:
                    this.InitializeMission1();
                    break;
                case 2:
                    this.InitializeMission2();
                    break;
                case 3:
                    this.InitializeMission3();
                    break;
                case 4:
                    this.InitializeMission4();
                    break;
                case 5:
                    this.InitializeMission5();
                    break;
                case 6:
                    this.InitializeMission6();
                    break;
                case 7:
                    this.InitializeMission7();
                    break;
                case 8:
                    this.InitializeMission8();
                    break;
                case 9:
                    this.InitializeMission9();
                    break;
                case 10:
                    this.InitializeMission10();
                    break;
                case 11:
                    this.InitializeMission11();
                    break;
            }
        }

        public void Update(float gameTime)
        {
            switch (this.m_currentMissionIndex)
            {
                case 0:
                    this.UpdateMission0(gameTime);
                    break;
                case 1:
                    this.UpdateMission1(gameTime);
                    break;
                case 2:
                    this.UpdateMission2(gameTime);
                    break;
                case 3:
                    this.UpdateMission3(gameTime);
                    break;
                case 4:
                    this.UpdateMission4(gameTime);
                    break;
                case 5:
                    this.UpdateMission5(gameTime);
                    break;
                case 6:
                    this.UpdateMission6(gameTime);
                    break;
                case 7:
                    this.UpdateMission7(gameTime);
                    break;
                case 8:
                    this.UpdateMission8(gameTime);
                    break;
                case 9:
                    this.UpdateMission9(gameTime);
                    break;
                case 10:
                    this.UpdateMission10(gameTime);
                    break;
                case 11:
                    this.UpdateMission11(gameTime);
                    break;
                case 20:
                    this.UpdateMissionEnd(gameTime);
                    break;
                case 99:
                    this.UpdateMissionOpenMap(gameTime);
                    break;
            }
            if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard && GameFSM.GetInstance().CurrentState == GameStateMap.GetInstance())
            {
                GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
                for (int index = 4; index < 14; ++index)
                {
                    if (currentWorld.GetLevel(index).GetInfestationLevel() == 1)
                        currentWorld.GetLevel(index).SetInfestationLevel(2);
                }
            }
            this.m_eventSelectLevel = false;
            this.m_eventEndLevel = false;
            this.m_eventNextText = false;
            this.m_eventNextText2 = false;
            this.m_eventExitShop = false;
        }

        public int GetCurrentMissionIndex() => this.m_currentMissionIndex;

        public void SetCurrentMissionIndex(int index) => this.m_currentMissionIndex = index;

        public int GetCurrentMissionPart() => this.m_missionPart;

        public void SetCurrentMissionPart(int part) => this.m_missionPart = part;

        public string GetCurrentMissionName()
        {
            switch (this.m_currentMissionIndex)
            {
                case 0:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_Tutorial);
                case 1:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_1);
                case 2:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_2);
                case 3:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_3);
                case 4:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_4);
                case 5:
                    return this.m_missionPart < 2 ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_5) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_5b);
                case 6:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_6);
                case 7:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_7);
                case 8:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_8);
                case 9:
                    if (this.m_missionPart == 0)
                        return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_9);
                    return this.m_missionPart <= 3 ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_9b) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_9c);
                case 10:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_10);
                case 11:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_11);
                case 99:
                    return GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.MissionName_End);
                default:
                    return string.Empty;
            }
        }

        public int GetCurrentMissionPartToSave()
        {
            switch (this.m_currentMissionIndex)
            {
                case 0:
                    return 0;
                case 1:
                    return this.m_missionPart;
                case 2:
                    return this.m_missionPart;
                case 3:
                    return 0;
                case 4:
                    return 0;
                case 5:
                    return this.m_missionPart;
                case 6:
                    return this.m_missionPart;
                case 7:
                    return 0;
                case 8:
                    return 0;
                case 9:
                    return this.m_missionPart;
                case 10:
                    return 0;
                case 11:
                    return 0;
                case 20:
                    return 0;
                case 99:
                    return 0;
                default:
                    return 0;
            }
        }

        private void InitializeMission0()
        {
        }

        private void UpdateMission0(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateInGame.GetInstance())
            {
                switch (this.m_missionPart)
                {
                    case 0:
                        currentWorld.IsShopEnabled = false;
                        currentWorld.IsShopExitEnabled = false;
                        currentWorld.GetNPCShop().SetIconActive(false);
                        this.DisplayHelp = true;
                        if (currentWorld.GetHud().HelpScreenEnabled)
                        {
                            this.m_missionPart = 10;
                            break;
                        }
                        break;
                    case 1:
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                            currentWorld.IsZombieSpawnEnabled = true;
                            currentWorld.ZombiesLeftToKill = 20;
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 2:
                        if (currentWorld.ZombiesKilledNb >= 20)
                        {
                            currentWorld.IsZombieSpawnEnabled = false;
                            if (currentWorld.ZombiesAliveNb <= 0)
                            {
                                ++this.m_missionPart;
                                this.m_timer = 2f;
                                break;
                            }
                            break;
                        }
                        break;
                    case 3:
                        this.m_timer -= gameTime;
                        if (this.m_timer <= 0.0 && !GameStateInGame.GetInstance().IsMenuInGameActive())
                        {
                            ++this.m_missionPart;
                            this.m_timer = 0.0f;
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_credits");
                            AudioManager.GetInstance().PlayMusic();
                            break;
                        }
                        break;
                    case 4:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Tutorial_Father_2));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                            currentWorld.IsShopEnabled = true;
                            currentWorld.GetNPCShop().SetIconActive(true);
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 6:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Tutorial_Father_3));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                            currentWorld.GetHud().ShowLeftArrow(true);
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 7:
                        if (this.m_eventEndLevel)
                        {
                            currentWorld.GetHud().ShowLeftArrow(false);
                            this.m_currentMissionIndex = 1;
                            this.m_missionPart = 0;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(1);
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(1);
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).Locked = false;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).Locked = false;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_Boss).Locked = true;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).Locked = true;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).Locked = true;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_Boss).Locked = true;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Graveyard_1).Locked = true;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_1).Locked = true;
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_Boss).Locked = true;
                            break;
                        }
                        break;
                    case 10:
                        if (!currentWorld.GetHud().HelpScreenEnabled)
                        {
                            this.DisplayHelp = false;
                            this.m_timer = 0.5f;
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 11:
                        this.m_timer -= gameTime;
                        if (this.m_timer <= 0.0 && !GameStateInGame.GetInstance().IsMenuInGameActive())
                        {
                            this.m_timer = 0.0f;
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Tutorial_Father_1));
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 12:
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Tutorial_Father_1a));
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 13:
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Tutorial_Father_1b));
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_03");
                            AudioManager.GetInstance().PlayMusic();
                            this.m_missionPart = 1;
                            break;
                        }
                        break;
                }
            }
            if (this.m_missionPart != 5 || !this.m_eventExitShop)
                return;
            ++this.m_missionPart;
        }

        private void InitializeMission1()
        {
            GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            switch (this.m_missionPart)
            {
                case 0:
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() == 0)
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(1);
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() != 0)
                        break;
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(1);
                    break;
                case 2:
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() == 0)
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(1);
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() == 0)
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(1);
                    this.m_missionPart = 5;
                    break;
                case 5:
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() != 0 || currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() != 0)
                        break;
                    this.m_enableTextBox = true;
                    break;
                case 8:
                    this.m_missionPart = 7;
                    break;
            }
        }

        private void UpdateMission1(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() > 2)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(2);
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() > 2)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
                switch (this.m_missionPart)
                {
                    case 0:
                        currentWorld.SetDayPart(GameWorld.DayPart.Morning);
                        ++this.m_missionPart;
                        break;
                    case 1:
                        if (currentWorld.GetDayPart() != GameWorld.DayPart.Evening)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission1_Nun_NightFall));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(1);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(1);
                        ++this.m_missionPart;
                        break;
                    case 2:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        this.m_missionPart = 5;
                        break;
                    case 3:
                        if (currentWorld.GetDayPart() != GameWorld.DayPart.Morning)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission1_Nun_Morning));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(1);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(1);
                        ++this.m_missionPart;
                        break;
                    case 4:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_missionPart;
                        break;
                    case 5:
                        if (!this.m_enableTextBox)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission1_Nun_GoShop));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        this.m_enableTextBox = false;
                        ++this.m_missionPart;
                        break;
                    case 6:
                        GameStateMap.GetInstance().GetMapView().ShopWarningEnabled = true;
                        break;
                    case 7:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission2_Nun_FindBoss));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() == 0)
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(1);
                        else
                            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(2);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
                        ++this.m_missionPart;
                        break;
                    case 8:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 0:
                    case 1:
                    case 2:
                        currentWorld.IsArmoredZombieSpawnEnabled = false;
                        break;
                    case 3:
                        currentWorld.IsGrabberZombieSpawnEnabled = true;
                        currentWorld.IsArmoredZombieSpawnEnabled = false;
                        break;
                    case 4:
                        currentWorld.IsArmoredZombieSpawnEnabled = false;
                        break;
                    case 5:
                        if (!this.m_eventEndLevel || currentWorld.LevelIndex == GameWorld.LevelId.Level_Shop)
                            break;
                        this.m_enableTextBox = true;
                        break;
                    case 6:
                        if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Shop)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission1_Father_Talk));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(2);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
                        ++this.m_missionPart;
                        break;
                }
            }
        }

        private void InitializeMission2()
        {
            GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (this.m_missionPart != 0 || currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() != 0)
                return;
            currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
        }

        private void UpdateMission2(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() > 2)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(2);
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() > 2)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
                switch (this.m_missionPart)
                {
                    case 1:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission2_Nun_BossFound));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_Boss).Locked = false;
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_Boss).SetInfestationLevel(1);
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance() || this.m_missionPart != 0 || !this.m_eventEndLevel || currentWorld.LevelIndex != GameWorld.LevelId.Level_Town_2)
                    return;
                ++this.m_missionPart;
            }
        }

        private void InitializeMission3()
        {
        }

        private void UpdateMission3(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(2);
                currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_Boss).GetInfestationLevel() <= 2)
                    return;
                currentWorld.GetLevel(GameWorld.LevelId.Level_Town_Boss).SetInfestationLevel(2);
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance() || currentWorld.LevelIndex != GameWorld.LevelId.Level_Town_Boss)
                    return;
                switch (this.m_missionPart)
                {
                    case 0:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Butcher, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission3_Butcher_Talk));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_missionPart;
                        break;
                    case 1:
                        if (!this.m_eventEndLevel)
                            break;
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
        }

        private void InitializeMission4()
        {
        }

        private void UpdateMission4(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() > 2)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(2);
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() > 2)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
                switch (this.m_missionPart)
                {
                    case 0:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission3_Nun_BossKilled));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_missionPart;
                        break;
                    case 1:
                        if (currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).GetInfestationLevel() != 0 || currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).GetInfestationLevel() != 0)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Nun_Clear));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_missionPart;
                        break;
                    case 2:
                        GameStateMap.GetInstance().GetMapView().ShopWarningEnabled = true;
                        break;
                }
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 2:
                        if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Shop)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Father_Talk));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Nun_Talk));
                        ++this.m_missionPart;
                        break;
                    case 3:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Father_Talk2));
                        ++this.m_missionPart;
                        break;
                    case 4:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Father_Talk3));
                        ++this.m_missionPart;
                        break;
                    case 5:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Nun_Talk2));
                        ++this.m_missionPart;
                        break;
                    case 6:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Nun_Talk3));
                        ++this.m_missionPart;
                        break;
                    case 7:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Father_Worried));
                        ++this.m_missionPart;
                        break;
                    case 8:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission4_Father_Report));
                        ++this.m_missionPart;
                        break;
                    case 9:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).Locked = false;
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).SetInfestationLevel(1);
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
        }

        private void InitializeMission5()
        {
            GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            switch (this.m_missionPart)
            {
                case 0:
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).GetInfestationLevel() != 0)
                        break;
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).SetInfestationLevel(1);
                    break;
                case 1:
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).GetInfestationLevel() != 0)
                        break;
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).SetInfestationLevel(1);
                    break;
            }
        }

        private void UpdateMission5(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                switch (this.m_missionPart)
                {
                    case 2:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission5_Nun_NotFound));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).SetInfestationLevel(1);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).SetInfestationLevel(1);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_1).SetInfestationLevel(2);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_2).SetInfestationLevel(2);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Town_Boss).SetInfestationLevel(2);
                        GameStateMap.GetInstance().GetMapView().ShopWarningEnabled = true;
                        ++this.m_missionPart;
                        break;
                    case 3:
                        GameStateMap.GetInstance().GetMapView().ShopWarningEnabled = true;
                        break;
                }
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 0:
                        if (!this.m_eventEndLevel || currentWorld.LevelIndex != GameWorld.LevelId.Level_Forest_1)
                            break;
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).Locked = false;
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).SetInfestationLevel(1);
                        ++this.m_missionPart;
                        break;
                    case 1:
                        if (!this.m_eventEndLevel || currentWorld.LevelIndex != GameWorld.LevelId.Level_Forest_2)
                            break;
                        ++this.m_missionPart;
                        break;
                    case 3:
                        if (currentWorld.LevelIndex == GameWorld.LevelId.Level_Shop)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission5_Father_SenseMagic));
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                            if (this.m_eventNextText)
                            {
                                GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                                ++this.m_currentMissionIndex;
                                this.m_missionPart = 0;
                            }
                        }
                        if (!this.m_eventEndLevel || currentWorld.GetDayPart() != GameWorld.DayPart.Night)
                            break;
                        for (int index = 4; index <= 8; ++index)
                        {
                            if (currentWorld.GetLevel(index).GetInfestationLevel() == 0 && index != 6 && MathHelperEx.GetRandomInt(0, 100) < 50)
                                currentWorld.GetLevel(index).SetInfestationLevel(1);
                        }
                        break;
                }
            }
        }

        private void InitializeMission6()
        {
            GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (this.m_missionPart != 0 || currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).GetInfestationLevel() != 0)
                return;
            currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).SetInfestationLevel(1);
        }

        private void UpdateMission6(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).GetInfestationLevel() == 0)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).SetInfestationLevel(1);
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).GetInfestationLevel() == 0)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).SetInfestationLevel(1);
                if (this.m_missionPart != 1)
                    return;
                GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission6_Nun_MagicNearby));
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                if (!this.m_eventNextText)
                    return;
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_Boss).Locked = false;
                currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_Boss).SetInfestationLevel(1);
                ++this.m_currentMissionIndex;
                this.m_missionPart = 0;
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance() || this.m_missionPart != 0 || !this.m_eventEndLevel || currentWorld.LevelIndex != GameWorld.LevelId.Level_Forest_2 || currentWorld.GetDayPart() != GameWorld.DayPart.Evening && currentWorld.GetDayPart() != GameWorld.DayPart.Night)
                    return;
                ++this.m_missionPart;
            }
        }

        private void InitializeMission7()
        {
        }

        private void UpdateMission7(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).GetInfestationLevel() == 0)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_1).SetInfestationLevel(1);
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).GetInfestationLevel() == 0)
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_2).SetInfestationLevel(1);
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_Boss).GetInfestationLevel() <= 2)
                    return;
                currentWorld.GetLevel(GameWorld.LevelId.Level_Forest_Boss).SetInfestationLevel(2);
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance() || currentWorld.LevelIndex != GameWorld.LevelId.Level_Forest_Boss)
                    return;
                ((NPCNecromancer)currentWorld.GetBoss()).ShowDeadNun();
                switch (this.m_missionPart)
                {
                    case 0:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Necro1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission7_Boss_Talk1));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission7_Nun_Talk1));
                        ++this.m_missionPart;
                        break;
                    case 1:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun2, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission7_Sister_Talk1));
                        ++this.m_missionPart;
                        break;
                    case 2:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Necro1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission7_Boss_Talk2));
                        ++this.m_missionPart;
                        break;
                    case 3:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun2, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission7_Sister_Talk_Hint));
                        ++this.m_missionPart;
                        break;
                    case 4:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_missionPart;
                        break;
                    case 5:
                        if (!this.m_eventEndLevel)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission7_Nun_Talk2));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        ++this.m_missionPart;
                        break;
                    case 6:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission7_Nun_Talk3));
                        ++this.m_missionPart;
                        break;
                    case 7:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
        }

        private void InitializeMission8()
        {
        }

        private void UpdateMission8(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (this.m_missionPart != 0)
                    return;
                GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Nun_HoldOnMagdalena));
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                if (!this.m_eventNextText)
                    return;
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                ++this.m_missionPart;
                currentWorld.LevelIndex = GameWorld.LevelId.Level_Shop;
                GameFSM.GetInstance().SetState(GameStateLoadLevel.GetInstance());
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 1:
                        if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Shop)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Nun_MagIsDead));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Father_Rest));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        ++this.m_missionPart;
                        break;
                    case 2:
                        if (!this.m_eventNextText || !this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Father_Restb));
                        ++this.m_missionPart;
                        break;
                    case 3:
                        if (!this.m_eventNextText || !this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Nun_MagIsDead2));
                        ++this.m_missionPart;
                        break;
                    case 4:
                        if (!this.m_eventNextText || !this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Father_Rest2));
                        ++this.m_missionPart;
                        break;
                    case 5:
                        if (!this.m_eventNextText || !this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Father_Rest3));
                        ++this.m_missionPart;
                        break;
                    case 6:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Nun_Talk));
                        ++this.m_missionPart;
                        break;
                    case 7:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Father_Spirit));
                        ++this.m_missionPart;
                        break;
                    case 8:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Father_Spirit2));
                        ++this.m_missionPart;
                        break;
                    case 9:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Nun_Where));
                        ++this.m_missionPart;
                        break;
                    case 10:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission8_Father_Graveyard));
                        ++this.m_missionPart;
                        break;
                    case 11:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Graveyard_1).Locked = false;
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Graveyard_1).SetInfestationLevel(2);
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
        }

        private void InitializeMission9()
        {
            GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            switch (this.m_missionPart)
            {
                case 0:
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Graveyard_1).GetInfestationLevel() != 0)
                        break;
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Graveyard_1).SetInfestationLevel(1);
                    break;
                case 2:
                    this.m_missionPart = 1;
                    break;
                case 3:
                    if (currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_1).GetInfestationLevel() != 0)
                        break;
                    currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_1).SetInfestationLevel(1);
                    break;
            }
        }

        private void UpdateMission9(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                switch (this.m_missionPart)
                {
                    case 1:
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_1).Locked = false;
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_1).SetInfestationLevel(3);
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission9_Nun_Chapel));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission9_Nun_Dead_Rising));
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 2:
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                            for (int index = 4; index <= 11; ++index)
                                currentWorld.GetLevel(index).SetInfestationLevel(3);
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 4:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission9_Nun_Crypt_Stair));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                            ++this.m_missionPart;
                            break;
                        }
                        break;
                    case 5:
                        GameStateMap.GetInstance().GetMapView().ShopWarningEnabled = true;
                        break;
                }
                if (this.m_missionPart < 3)
                    return;
                for (int index = 4; index <= 11; ++index)
                    currentWorld.GetLevel(index).SetInfestationLevel(3);
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 0:
                        if (!this.m_eventEndLevel || currentWorld.LevelIndex != GameWorld.LevelId.Level_Graveyard_1)
                            break;
                        ++this.m_missionPart;
                        break;
                    case 3:
                        if (!this.m_eventEndLevel || currentWorld.LevelIndex != GameWorld.LevelId.Level_Chapel_1)
                            break;
                        ++this.m_missionPart;
                        break;
                    case 5:
                        if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Shop)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission9_Father_Crypt_Magic));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
        }

        private void InitializeMission10()
        {
        }

        private void UpdateMission10(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                switch (this.m_missionPart)
                {
                    case 0:
                    case 1:
                        bool flag1 = true;
                        for (int index = 4; index <= 11; ++index)
                        {
                            if (currentWorld.GetLevel(index).GetInfestationLevel() != 0)
                                flag1 = false;
                        }
                        if (!flag1)
                            break;
                        if (!currentWorld.GetHero().GetGunArray()[5].IsAvailable)
                        {
                            if (this.m_missionPart != 0)
                                break;
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission10_Nun_Need_HolyBeam));
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                            if (!this.m_eventNextText)
                                break;
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                            this.m_missionPart = 1;
                            break;
                        }
                        this.m_missionPart = 2;
                        break;
                    case 2:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission10_Nun_Enter_Crypt));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_Boss).Locked = false;
                        currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_Boss).SetInfestationLevel(1);
                        ++this.m_currentMissionIndex;
                        this.m_missionPart = 0;
                        break;
                }
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 0:
                    case 1:
                        if (!this.m_eventEndLevel)
                            break;
                        if (this.m_missionPart == 1)
                            this.m_missionPart = 0;
                        if (currentWorld.GetDayPart() != GameWorld.DayPart.Night)
                            break;
                        bool flag2 = true;
                        for (int index = 4; index <= 11; ++index)
                        {
                            if (currentWorld.GetLevel(index).GetInfestationLevel() != 0 && currentWorld.LevelIndex != (GameWorld.LevelId)index)
                                flag2 = false;
                        }
                        if (flag2 && currentWorld.GetHero().GetGunArray()[5].IsAvailable)
                        {
                            this.m_missionPart = 2;
                            break;
                        }
                        if (currentWorld.GetHero().GetGunArray()[5].IsAvailable)
                            break;
                        for (int index = 4; index <= 11; ++index)
                        {
                            if (index != 6 && index != 9 && (GameWorld.LevelId)index != currentWorld.LevelIndex && currentWorld.GetLevel(index).GetInfestationLevel() == 0 && MathHelperEx.GetRandomInt(0, 100) < 50)
                            {
                                currentWorld.GetLevel(index).SetInfestationLevel(1);
                                break;
                            }
                        }
                        break;
                }
            }
        }

        private void InitializeMission11()
        {
        }

        private void UpdateMission11(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_Boss).GetInfestationLevel() <= 2)
                    return;
                currentWorld.GetLevel(GameWorld.LevelId.Level_Chapel_Boss).SetInfestationLevel(2);
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 0:
                        if (currentWorld.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Necro2, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission12_Boss_Talk));
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                            if (!this.m_eventNextText)
                                break;
                            ++this.m_missionPart;
                            break;
                        }
                        if (!this.m_eventEndLevel || currentWorld.GetDayPart() != GameWorld.DayPart.Night)
                            break;
                        for (int index = 4; index <= 11; ++index)
                        {
                            int num = 0;
                            if (currentWorld.GetLevel(index).GetInfestationLevel() == 0 && MathHelperEx.GetRandomInt(0, 100) < 50)
                            {
                                currentWorld.GetLevel(index).SetInfestationLevel(1);
                                if (num + 1 >= 2)
                                    break;
                            }
                        }
                        break;
                    case 1:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission12_Nun_Swallow));
                        ++this.m_missionPart;
                        break;
                    case 2:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Necro2, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission12_Boss_Talk2));
                        ++this.m_missionPart;
                        break;
                    case 3:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission12_Nun_Talk));
                        ++this.m_missionPart;
                        break;
                    case 4:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        ++this.m_missionPart;
                        break;
                    case 5:
                        if (!this.m_eventEndLevel || currentWorld.LevelIndex != GameWorld.LevelId.Level_Chapel_Boss)
                            break;
                        this.m_currentMissionIndex = 20;
                        for (int index = 4; index < 14; ++index)
                            currentWorld.GetLevel(index).SetInfestationLevel(0);
                        this.m_missionPart = 0;
                        if (currentWorld.GetDayPart() != GameWorld.DayPart.Morning)
                        {
                            currentWorld.SetDayPart(GameWorld.DayPart.Morning);
                            ++currentWorld.DayIndex;
                        }
                        GameStateInGame.GetInstance().NextLevelRequest = true;
                        break;
                }
            }
        }

        private void UpdateMissionEnd(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                if (this.m_missionPart != 0)
                    return;
                GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Go_Funerals));
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                if (!this.m_eventNextText)
                    return;
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                ++this.m_missionPart;
                currentWorld.LevelIndex = GameWorld.LevelId.Level_Ending;
                GameFSM.GetInstance().SetState(GameStateLoadLevel.GetInstance());
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance())
                    return;
                switch (this.m_missionPart)
                {
                    case 1:
                        if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Ending)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Talk));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        AudioManager.GetInstance().LoadMusic("Sounds\\music_end");
                        AudioManager.GetInstance().PlayMusic();
                        ++this.m_missionPart;
                        break;
                    case 2:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Priest_Talk));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        ++this.m_missionPart;
                        break;
                    case 3:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Talk2));
                        ++this.m_missionPart;
                        break;
                    case 4:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Priest_Talk2));
                        ++this.m_missionPart;
                        break;
                    case 5:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Talk3));
                        ++this.m_missionPart;
                        break;
                    case 6:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Priest, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Priest_Talk3));
                        ++this.m_missionPart;
                        break;
                    case 7:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        currentWorld.GetHero().GetStateMachine().SetState(currentWorld.GetHero().GetStateMachine().StateScript);
                        currentWorld.GetHero().GetAnimationStateMachine().SetState(HeroAnimationStateCrouch.GetInstance());
                        ++this.m_missionPart;
                        this.m_timer = 2f;
                        break;
                    case 8:
                        this.m_timer -= gameTime;
                        if (this.m_timer > 0.0 || GameStateInGame.GetInstance().IsMenuInGameActive())
                            break;
                        ++this.m_missionPart;
                        this.m_timer = 0.0f;
                        break;
                    case 9:
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Body));
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        ++this.m_missionPart;
                        break;
                    case 10:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().EnableChoiceMode(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Choice1), GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Choice2));
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Choice));
                        ++this.m_missionPart;
                        break;
                    case 11:
                        if (this.m_eventNextText)
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().DisableChoiceMode();
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_RIP));
                            currentWorld.GetHero().GetAnimationStateMachine().SetState(HeroAnimationStateStandLookRight.GetInstance());
                            this.m_missionPart = 12;
                            break;
                        }
                        if (!this.m_eventNextText2)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().DisableChoiceMode();
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_MissionEnd_Nun_Shoot));
                        this.m_missionPart = 13;
                        break;
                    case 12:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        this.m_currentMissionIndex = 99;
                        this.m_missionPart = 0;
                        InGameStateScript.GetInstance().NextRequest = true;
                        break;
                    case 13:
                        if (!this.m_eventNextText)
                            break;
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                        InGameStateScript.GetInstance().SuicideRequest = true;
                        break;
                }
            }
        }

        private void UpdateMissionOpenMap(float gameTime)
        {
            GameFSM instance = GameFSM.GetInstance();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            if (instance.CurrentState == GameStateMap.GetInstance())
            {
                for (int index = 4; index < 14; ++index)
                {
                    currentWorld.GetLevel(index).Locked = false;
                    currentWorld.GetLevel(index).SetInfestationLevel(3);
                }
            }
            else
            {
                if (instance.CurrentState != GameStateInGame.GetInstance() || currentWorld.LevelIndex != GameWorld.LevelId.Level_Chapel_Boss || !this.m_eventEndLevel)
                    return;
                GameStateInGame.GetInstance().NextLevelRequest = true;
            }
        }
    }
}
