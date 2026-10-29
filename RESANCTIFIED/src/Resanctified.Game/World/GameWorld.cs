// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.GameWorld
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Game.FSM.NPC.Zombie.State;
using Resanctified.Game.UI;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World
{
    public class GameWorld : Resanctified.Engine.World.World
    {
        private Level[] m_levelArray;
        public Vector3 WorldMin;
        public Vector3 WorldMax;
        public float EndOfLevelPosX;
        public int DayIndex;
        public GameWorld.LevelId LevelIndex;
        public int ZombiesAliveNb;
        public int ZombiesKilledNb;
        public int ZombiesLeftToKillInit;
        public int ZombiesLeftToKill;
        public int ZombiesLeftToKillForPotion;
        private GameWorld.DayPart m_dayPart;
        private PlayableCharacter m_hero;
        private NPCZombie[] m_zombieArray;
        private Entity m_cameraTarget;
        private NPCButcher m_bossButcher;
        private NPCNecromancer m_bossNecro;
        private NPCShop m_npcShop;
        private MagicOrb m_orb;
        private CameraEntity m_camera;
        private HudViewController m_hud;
        private Explosion[] m_explosionArray;
        private LifePotion[] m_potionArray;
        private BulletImpact[] m_impactArray;
        private FallingRock[] m_rockArray;
        private Spike[] m_spikeArray;
        private float m_spikeTrapTimer;
        private float m_spikeTrapStartPos;
        private bool m_earthquakeEnabled;
        private float m_earthquakeTimer;
        private float m_spawnTime;
        private float m_spawnSwarmTimer;
        private float m_spawnTimer;
        private int m_zombiesAliveNbMax;
        private int m_zombiesToSpawnNb;
        private int m_explosionIndex;
        private int m_impactIndex;
        private int m_spikeIndex;
        private int m_rockIndex;
        private int m_spawnGrabberRatio;
        private int m_spawnArmoredRatio;
        private int m_levelSectorIndex;
        public bool IsZombieSpawnEnabled;
        public bool IsGrabberZombieSpawnEnabled;
        public bool IsArmoredZombieSpawnEnabled;
        public bool IsShopEnabled;
        public bool IsShopExitEnabled;
        private Texture m_zombieNormalTexture;
        private Texture m_zombieNormalTextureAlt1;
        private Texture m_zombieGrabberTextureAlt1;
        private Animation2D[] m_miscAnimArray;
        public bool LoadMissionPartDisabled;
        private string[] g_digitAnimFilename = new string[10]
        {
      "Anims\\text_digit_0",
      "Anims\\text_digit_1",
      "Anims\\text_digit_2",
      "Anims\\text_digit_3",
      "Anims\\text_digit_4",
      "Anims\\text_digit_5",
      "Anims\\text_digit_6",
      "Anims\\text_digit_7",
      "Anims\\text_digit_8",
      "Anims\\text_digit_9"
        };

        public PlayableCharacter GetHero() => this.m_hero;

        public CameraEntity GetCamera() => this.m_camera;

        public HudViewController GetHud() => this.m_hud;

        public int GetLevelSectorIndex() => this.m_levelSectorIndex;

        public NPCBoss GetBoss()
        {
            switch (this.LevelIndex)
            {
                case GameWorld.LevelId.Level_Town_Boss:
                    return (NPCBoss)this.m_bossButcher;
                case GameWorld.LevelId.Level_Forest_Boss:
                    return Scenario.GetInstance().GetCurrentMissionIndex() >= 9 && Scenario.GetInstance().GetCurrentMissionIndex() < 99 ? (NPCBoss)this.m_bossButcher : (NPCBoss)this.m_bossNecro;
                case GameWorld.LevelId.Level_Chapel_Boss:
                    return (NPCBoss)this.m_bossNecro;
                default:
                    return (NPCBoss)null;
            }
        }

        public NPCShop GetNPCShop() => this.m_npcShop;

        public MagicOrb GetMagicOrb() => this.m_orb;

        public GameWorld.DayPart GetDayPart() => this.m_dayPart;

        public void SetDayPart(GameWorld.DayPart part) => this.m_dayPart = part;

        public void NextDayPart()
        {
            if (this.m_dayPart == GameWorld.DayPart.Night)
            {
                for (int index = 0; index < 14; ++index)
                {
                    if (this.m_levelArray[index].GetInfestationLevel() > 0)
                        this.m_levelArray[index].IncreaseInfestationLevel();
                }
                this.m_dayPart = GameWorld.DayPart.Morning;
                ++this.DayIndex;
            }
            else
                ++this.m_dayPart;
        }

        public bool NextLevel()
        {
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
            {
                if (this.LevelIndex == GameWorld.LevelId.Level_Survival_3)
                {
                    this.LevelIndex = GameWorld.LevelId.Level_Survival_1;
                    return true;
                }
                ++this.LevelIndex;
            }
            return false;
        }

        public Level GetLevel(GameWorld.LevelId id) => this.m_levelArray[(int)id];

        public Level GetLevel(int index) => this.m_levelArray[index];

        public Level GetCurrentLevel() => this.m_levelArray[(int)this.LevelIndex];

        public string GetLevelName(GameWorld.LevelId id)
        {
            switch (id)
            {
                case GameWorld.LevelId.Level_Survival_1:
                    return "Survival 1";
                case GameWorld.LevelId.Level_Survival_2:
                    return "Survival 2";
                case GameWorld.LevelId.Level_Survival_3:
                    return "Survival 3";
                case GameWorld.LevelId.Level_Shop:
                    return "Shop";
                case GameWorld.LevelId.Level_Town_1:
                    return "Town 1";
                case GameWorld.LevelId.Level_Town_2:
                    return "Town 2";
                case GameWorld.LevelId.Level_Town_Boss:
                    return "Town Boss";
                case GameWorld.LevelId.Level_Forest_1:
                    return "Forest 1";
                case GameWorld.LevelId.Level_Forest_2:
                    return "Forest 2";
                case GameWorld.LevelId.Level_Forest_Boss:
                    return "Forest Boss";
                case GameWorld.LevelId.Level_Graveyard_1:
                    return "Graveyard";
                case GameWorld.LevelId.Level_Chapel_1:
                    return "Chapel";
                case GameWorld.LevelId.Level_Chapel_Boss:
                    return "Crypt Boss";
                case GameWorld.LevelId.Level_Ending:
                    return "Ending";
                default:
                    return string.Empty;
            }
        }

        public string GetDayPartName(GameWorld.DayPart part)
        {
            switch (part)
            {
                case GameWorld.DayPart.Morning:
                    return "Morning";
                case GameWorld.DayPart.Day:
                    return "Afternoon";
                case GameWorld.DayPart.Evening:
                    return "Evening";
                case GameWorld.DayPart.Night:
                    return "Night";
                default:
                    return string.Empty;
            }
        }

        public Animation2D GetMiscAnimation(GameWorld.MiscAnimId animId) => this.m_miscAnimArray[(int)animId];

        public int GetKillPoints(NPCZombie zombie)
        {
            int num1 = 0;
            switch (zombie.GetZombieType())
            {
                case ZombieType.Normal:
                    num1 = 10;
                    break;
                case ZombieType.Grabber:
                    num1 = 20;
                    break;
                case ZombieType.ArmoredNormal:
                    num1 = 30;
                    break;
                case ZombieType.ArmoredGrabber:
                    num1 = 40;
                    break;
            }
            int num2 = this.DayIndex;
            if (num2 > 31)
                num2 = 31;
            int killPoints = num1 * num2;
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && this.GetCurrentLevel().GetLevelType() != Level.LevelType.Shop)
                killPoints *= this.GetCurrentLevel().GetInfestationLevel();
            return killPoints;
        }

        public int GetBossKillPoints()
        {
            int num = 0;
            switch (this.GetBoss().GetBossType())
            {
                case NPCBoss.BossType.ButcherForm1:
                    num = 5000;
                    break;
                case NPCBoss.BossType.NecromancerForm1:
                    num = 8000;
                    break;
                case NPCBoss.BossType.NecromancerForm2:
                    num = 10000;
                    break;
            }
            return num * this.GetCurrentLevel().GetInfestationLevel();
        }

        public GameWorld()
        {
            this.LoadMissionPartDisabled = false;
            this.m_levelArray = new Level[14];
            this.InitLevelArray();
            this.m_zombieArray = new NPCZombie[55];
            this.m_explosionArray = new Explosion[8];
            this.m_potionArray = new LifePotion[4];
            this.m_miscAnimArray = new Animation2D[6];
            this.m_impactArray = new BulletImpact[8];
            this.m_spikeArray = new Spike[8];
            this.m_rockArray = new FallingRock[4];
            this.m_earthquakeEnabled = false;
            this.m_earthquakeTimer = 0.0f;
            this.m_spikeTrapTimer = 0.0f;
            this.m_spikeTrapStartPos = 0.0f;
            this.m_spawnTime = 4f;
            this.m_spawnSwarmTimer = 0.0f;
            this.m_spawnTimer = 0.0f;
            this.IsZombieSpawnEnabled = true;
            this.IsGrabberZombieSpawnEnabled = true;
            this.IsArmoredZombieSpawnEnabled = true;
            this.IsShopEnabled = true;
            this.IsShopExitEnabled = true;
            this.EndOfLevelPosX = 40f;
            this.DayIndex = 1;
            this.LevelIndex = GameWorld.LevelId.Level_Survival_1;
            this.m_dayPart = GameWorld.DayPart.Morning;
            this.ZombiesAliveNb = 0;
            this.m_zombiesToSpawnNb = 1;
            this.m_zombiesAliveNbMax = 1;
            this.ZombiesKilledNb = 0;
            this.ZombiesLeftToKillInit = 0;
            this.ZombiesLeftToKill = 0;
            this.ZombiesLeftToKillForPotion = 30;
            this.m_spawnGrabberRatio = 10;
            this.m_spawnArmoredRatio = 0;
            this.m_explosionIndex = 0;
            this.m_impactIndex = 0;
            this.m_spikeIndex = 0;
            this.m_levelSectorIndex = 1;
            this.m_zombieNormalTexture = (Texture)null;
            this.m_zombieNormalTextureAlt1 = (Texture)null;
            this.m_zombieGrabberTextureAlt1 = (Texture)null;
            this.m_hero = new PlayableCharacter();
            this.m_camera = new CameraEntity();
            this.m_cameraTarget = new Entity();
            this.m_hud = new HudViewController();
            this.m_bossButcher = new NPCButcher();
            this.m_bossNecro = new NPCNecromancer();
            this.m_npcShop = new NPCShop();
            this.m_orb = new MagicOrb();
            for (int index = 0; index < 55; ++index)
                this.m_zombieArray[index] = new NPCZombie();
            for (int index = 0; index < 8; ++index)
                this.m_explosionArray[index] = new Explosion();
            for (int index = 0; index < 4; ++index)
                this.m_potionArray[index] = new LifePotion();
            for (int index = 0; index < 8; ++index)
                this.m_impactArray[index] = new BulletImpact();
            for (int index = 0; index < 6; ++index)
                this.m_miscAnimArray[index] = new Animation2D();
            for (int index = 0; index < 8; ++index)
                this.m_spikeArray[index] = new Spike();
            for (int index = 0; index < 4; ++index)
                this.m_rockArray[index] = new FallingRock();
        }

        private void InitLevelArray()
        {
            this.m_levelArray[0] = new Level("Levels\\lvl01\\lvl01", (string)null, (string)null, (string)null, (string)null, (string)null, (string)null, Level.LevelType.Normal);
            this.m_levelArray[1] = new Level("Levels\\lvl02\\lvl02", (string)null, (string)null, (string)null, (string)null, (string)null, (string)null, Level.LevelType.Normal);
            this.m_levelArray[2] = new Level("Levels\\lvl03\\lvl03", (string)null, (string)null, (string)null, (string)null, (string)null, (string)null, Level.LevelType.Normal);
            this.m_levelArray[3] = new Level("Levels\\lvl13\\lvl13", "Textures\\texture_shop", "Textures\\texture_shop_night", "Textures\\sky_day", "Textures\\sky_day", "Textures\\sky_night", "Textures\\sky_night", Level.LevelType.Shop);
            this.m_levelArray[4] = new Level("Levels\\lvl07\\lvl07", "Textures\\texture_day_story", "Textures\\texture_day_night_story", "Textures\\sky_cemetary_story", "Textures\\sky_cliff", "Textures\\sky_sunburn", "Textures\\sky_cliff_night", Level.LevelType.Normal);
            this.m_levelArray[5] = new Level("Levels\\lvl09\\lvl09", "Textures\\texture_day2_story", "Textures\\texture_day2_night_story", "Textures\\sky_cemetary_story", "Textures\\sky_cliff", "Textures\\sky_cemetary2", "Textures\\sky_cliff_night", Level.LevelType.Normal);
            this.m_levelArray[6] = new Level("Levels\\lvl08\\lvl08", "Textures\\texture_butcher_kitchen", "Textures\\texture_butcher_night_kitchen", "Textures\\sky_kitchen", "Textures\\sky_kitchen", "Textures\\sky_kitchen_night", "Textures\\sky_kitchen_night", Level.LevelType.Boss);
            this.m_levelArray[7] = new Level("Levels\\lvl04\\lvl04", "Textures\\texture_forest", "Textures\\texture_forest_night", "Textures\\sky_cemetary_story", "Textures\\sky_cliff", "Textures\\sky_sunburn", "Textures\\sky_cliff_night", Level.LevelType.Normal);
            this.m_levelArray[8] = new Level("Levels\\lvl05\\lvl05", "Textures\\texture_cliff", "Textures\\texture_cliff_night", "Textures\\sky_cemetary_story", "Textures\\sky_cliff", "Textures\\sky_cemetary2", "Textures\\sky_cliff_night", Level.LevelType.Normal);
            this.m_levelArray[9] = new Level("Levels\\lvl06\\lvl06", "Textures\\texture_butcher_arena", "Textures\\texture_butcher_night_arena", "Textures\\sky_day", "Textures\\sky_day", "Textures\\sky_night", "Textures\\sky_night", Level.LevelType.Boss);
            this.m_levelArray[10] = new Level("Levels\\lvl11\\lvl11", "Textures\\texture_cemetery_story", "Textures\\texture_cemetery_night_story", "Textures\\sky_cemetary_story", "Textures\\sky_cliff", "Textures\\sky_cemetary2", "Textures\\sky_cliff_night", Level.LevelType.Normal);
            this.m_levelArray[11] = new Level("Levels\\lvl12\\lvl12", "Textures\\texture_chapel_story", "Textures\\texture_chapel_night_story", "Textures\\sky_chapel", "Textures\\sky_chapel", "Textures\\sky_chapel_night", "Textures\\sky_chapel_night", Level.LevelType.Normal);
            this.m_levelArray[12] = new Level("Levels\\lvl10\\lvl10", "Textures\\texture_final_boss", "Textures\\texture_final_boss", "Textures\\sky_day", "Textures\\sky_day", "Textures\\sky_night", "Textures\\sky_night", Level.LevelType.Boss);
            this.m_levelArray[13] = new Level("Levels\\lvl11\\lvl11", "Textures\\texture_cemetery_story", "Textures\\texture_cemetery_night_story", "Textures\\sky_cemetary_story", "Textures\\sky_cliff", "Textures\\sky_cemetary2", "Textures\\sky_cliff_night", Level.LevelType.Script);
        }

        public void Initialize(bool continueGame)
        {
            this.Initialize();
            this.m_levelSectorIndex = 1;
            this.IsZombieSpawnEnabled = true;
            this.IsGrabberZombieSpawnEnabled = true;
            this.IsArmoredZombieSpawnEnabled = true;
            this.IsShopEnabled = true;
            this.IsShopExitEnabled = true;
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
                this.m_hero.Initialize(3.5f, 0.0f);
            else if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
            {
                this.m_hero.Initialize(3.8f, 0.0f);
                this.m_npcShop.Initialize();
            }
            else if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Script)
            {
                this.m_hero.Initialize(4f, 0.0f);
                this.m_npcShop.Initialize();
                this.m_npcShop.SetIconActive(false);
                this.m_npcShop.SetDeadNunActive(true);
            }
            else
                this.m_hero.Initialize(0.0f, 0.0f);
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
            {
                if (continueGame)
                {
                    this.LoadGameData();
                }
                else
                {
                    this.SetDefaultGameData();
                    Scenario.GetInstance().Initialize();
                    this.SaveGameData();
                }
                this.LoadMissionPartDisabled = true;
                this.InitLevelParametersStory();
            }
            else
            {
                if (continueGame)
                {
                    this.LoadGameData();
                }
                else
                {
                    this.SetDefaultGameData();
                    this.SaveGameData();
                }
                this.InitLevelParametersSurvival();
            }
            this.LoadMiscAnimations();
            NPCBoss boss = (NPCBoss)null;
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                switch (this.LevelIndex)
                {
                    case GameWorld.LevelId.Level_Town_Boss:
                        this.m_bossButcher.Initialize(NPCBoss.BossType.ButcherForm1);
                        boss = (NPCBoss)this.m_bossButcher;
                        break;
                    case GameWorld.LevelId.Level_Forest_Boss:
                        if (Scenario.GetInstance().GetCurrentMissionIndex() >= 9 && Scenario.GetInstance().GetCurrentMissionIndex() < 99)
                        {
                            this.m_bossButcher.Initialize(NPCBoss.BossType.ButcherForm1);
                            boss = (NPCBoss)this.m_bossButcher;
                            break;
                        }
                        this.m_bossNecro.Initialize(NPCBoss.BossType.NecromancerForm1);
                        boss = (NPCBoss)this.m_bossNecro;
                        break;
                    case GameWorld.LevelId.Level_Chapel_Boss:
                        this.m_bossNecro.Initialize(NPCBoss.BossType.NecromancerForm2);
                        this.m_orb.Initialize();
                        boss = (NPCBoss)this.m_bossNecro;
                        break;
                }
            }
            Resanctified.Engine.Resources.Resource.SafeRelease<Texture>(ref this.m_zombieNormalTexture);
            Resanctified.Engine.Resources.Resource.SafeRelease<Texture>(ref this.m_zombieNormalTextureAlt1);
            Resanctified.Engine.Resources.Resource.SafeRelease<Texture>(ref this.m_zombieGrabberTextureAlt1);
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                this.InitZombieSkinStory();
            else
                this.InitZombieSkinSurvival();
            for (int index = 0; index < 55; ++index)
            {
                if (index < 20)
                    this.m_zombieArray[index].Initialize(this.DayIndex, ZombieType.ArmoredNormal);
                else
                    this.m_zombieArray[index].Initialize(this.DayIndex, ZombieType.Normal);
            }
            for (int index = 0; index < 8; ++index)
                this.m_explosionArray[index].Initialize();
            this.m_explosionIndex = 0;
            for (int index = 0; index < 8; ++index)
                this.m_impactArray[index].Initialize();
            this.m_impactIndex = 0;
            if (this.LevelIndex == GameWorld.LevelId.Level_Forest_Boss || this.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
            {
                for (int index = 0; index < 8; ++index)
                    this.m_spikeArray[index].Initialize();
            }
            this.m_spikeIndex = 0;
            this.m_spikeTrapTimer = 0.0f;
            this.m_spikeTrapStartPos = 0.0f;
            if (this.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
            {
                for (int index = 0; index < 4; ++index)
                    this.m_rockArray[index].Initialize();
            }
            this.m_rockIndex = 0;
            this.m_earthquakeEnabled = false;
            this.m_earthquakeTimer = 0.0f;
            for (int index = 0; index < 4; ++index)
                this.m_potionArray[index].Initialize();
            this.m_spawnSwarmTimer = 0.0f;
            this.m_spawnTimer = 0.0f;
            this.ZombiesAliveNb = 0;
            this.ZombiesKilledNb = 0;
            this.ZombiesLeftToKillForPotion = 30;
            if (this.m_hero.HitPoints < (int)((double)this.m_hero.GetHitPointsMax() * 0.5))
                this.ZombiesLeftToKillForPotion = 20;
            this.m_camera.Initialize();
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
            {
                if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
                {
                    this.m_camera.SetOrthogonalProjection(4.8f, 3.2f, 0.0f, 1f);
                    this.m_camera.GetTransform().SetPosition(1f, 2f, 0.0f);
                }
                else
                {
                    this.m_camera.SetOrthogonalProjection(4.363636f, 2.909091f, 0.0f, 1f);
                    this.m_camera.GetTransform().SetPosition(1f, 1f, 0.0f);
                }
            }
            else
            {
                this.m_camera.SetOrthogonalProjection(4f, 2.666667f, 0.0f, 1f);
                this.m_camera.GetTransform().SetPosition(1f, 2f, 0.0f);
            }
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                this.m_cameraTarget.GetTransform().SetPosition(this.m_hero.GetTransform().GetPosition());
                this.m_camera.FollowEntity(this.m_cameraTarget, 1.5f, 2f, 20f, 0.95f, 30f, 1f, false, false);
            }
            else if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                this.m_camera.FollowEntity((Entity)this.m_hero, 2.5f, 2f, 30f, 0.95f, 30f, 1f, false, false);
            else
                this.m_camera.FollowEntity((Entity)this.m_hero, 2.5f, 2.5f, 30f, 0.95f, 30f, 1f, false, false);
            Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().SetProjectionMatrix(this.m_camera.GetProjectionMatrix());
            this.m_hud.Initialize(this.m_hero, boss);
            if (this.GetCurrentLevel().GetLevelType() != Level.LevelType.Shop && this.GetCurrentLevel().GetLevelType() != Level.LevelType.Script)
                return;
            this.m_npcShop.GetViewController().SetButtonPosition(this.GetCamera());
        }

        private void InitZombieSkinSurvival()
        {
            this.m_zombieNormalTexture = ResourceManager.GetInstance().LoadTexture("Textures\\zombie");
            if (this.DayIndex < 3)
                this.m_zombieNormalTextureAlt1 = this.DayIndex != 1 || this.LevelIndex >= GameWorld.LevelId.Level_Town_2 ? ResourceManager.GetInstance().LoadTexture("Textures\\zombie2") : ResourceManager.GetInstance().LoadTexture("Textures\\zombie");
            else if (this.DayIndex == 3)
                this.m_zombieNormalTextureAlt1 = this.LevelIndex < GameWorld.LevelId.Level_Graveyard_1 ? ResourceManager.GetInstance().LoadTexture("Textures\\zombie2") : ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie");
            else if (this.DayIndex < 7)
                this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie");
            else if (this.DayIndex == 7)
            {
                this.m_zombieNormalTextureAlt1 = this.LevelIndex != GameWorld.LevelId.Level_Graveyard_1 ? ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie") : ResourceManager.GetInstance().LoadTexture("Textures\\skeleton");
            }
            else
            {
                int randomInt = MathHelperEx.GetRandomInt(0, 100);
                this.m_zombieNormalTextureAlt1 = randomInt >= 33 ? (randomInt >= 66 ? ResourceManager.GetInstance().LoadTexture("Textures\\skeleton") : ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie")) : ResourceManager.GetInstance().LoadTexture("Textures\\zombie2");
            }
            if (this.DayIndex < 3)
                this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie_grab");
            else if (this.DayIndex < 5)
            {
                if (MathHelperEx.GetRandomInt(0, 100) < 50)
                    this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie2_grab");
                else
                    this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie_grab");
            }
            else
            {
                int randomInt = MathHelperEx.GetRandomInt(0, 100);
                if (randomInt < 33)
                    this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie_grab");
                else if (randomInt < 66)
                    this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie2_grab");
                else
                    this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie_grab");
            }
        }

        private void InitZombieSkinStory()
        {
            this.m_zombieNormalTexture = ResourceManager.GetInstance().LoadTexture("Textures\\zombie");
            if (this.DayIndex == 1)
            {
                this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie");
                this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie_grab");
            }
            else
            {
                switch (this.LevelIndex)
                {
                    case GameWorld.LevelId.Level_Shop:
                    case GameWorld.LevelId.Level_Town_1:
                    case GameWorld.LevelId.Level_Town_2:
                    case GameWorld.LevelId.Level_Town_Boss:
                        if (this.GetDayPart() == GameWorld.DayPart.Day)
                        {
                            this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie2");
                            this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie_grab");
                            break;
                        }
                        this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie");
                        this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie_grab");
                        break;
                    case GameWorld.LevelId.Level_Forest_1:
                    case GameWorld.LevelId.Level_Forest_Boss:
                        this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie");
                        this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie2_grab");
                        break;
                    case GameWorld.LevelId.Level_Forest_2:
                        if (this.GetDayPart() == GameWorld.DayPart.Morning || this.GetDayPart() == GameWorld.DayPart.Day)
                        {
                            this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie2");
                            this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie2_grab");
                            break;
                        }
                        this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie");
                        this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\zombie2_grab");
                        break;
                    case GameWorld.LevelId.Level_Graveyard_1:
                    case GameWorld.LevelId.Level_Chapel_1:
                    case GameWorld.LevelId.Level_Chapel_Boss:
                        this.m_zombieNormalTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\skeleton");
                        this.m_zombieGrabberTextureAlt1 = ResourceManager.GetInstance().LoadTexture("Textures\\sk_zombie_grab");
                        break;
                }
            }
        }

        private void InitLevelParametersSurvival()
        {
            this.ZombiesLeftToKill = GameConstant.ZOMBIE_TOKILL_NB_ARRAY[this.DayIndex - 1];
            this.ZombiesLeftToKillInit = this.ZombiesLeftToKill;
            this.IsZombieSpawnEnabled = true;
            this.IsGrabberZombieSpawnEnabled = true;
            this.IsArmoredZombieSpawnEnabled = true;
            int num = this.DayIndex - 1;
            if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                num += 10;
            this.m_spawnGrabberRatio = 10 + num * 2;
            if (this.m_spawnGrabberRatio > 30)
                this.m_spawnGrabberRatio = 30;
            if (this.DayIndex >= 2)
            {
                this.m_spawnArmoredRatio = 10 + (num - 2) * 3;
                if (this.m_spawnArmoredRatio > 50)
                    this.m_spawnArmoredRatio = 50;
            }
            else
            {
                this.IsArmoredZombieSpawnEnabled = false;
                this.m_spawnArmoredRatio = 0;
            }
            this.m_zombiesAliveNbMax = GameConstant.ZOMBIE_ALIVE_NB_ARRAY[this.DayIndex - 1];
            if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard && this.m_zombiesAliveNbMax < 10)
                this.m_zombiesAliveNbMax = 10;
            this.m_spawnTime = (float)(4.0 - (double)num * 0.0700000002980232);
            if ((double)this.m_spawnTime < 3.0)
                this.m_spawnTime = 3f;
            if (this.DayIndex != 1 || this.LevelIndex != GameWorld.LevelId.Level_Survival_1)
                return;
            this.IsGrabberZombieSpawnEnabled = false;
        }

        private void InitLevelParametersStory()
        {
            int num1 = this.GetCurrentLevel().GetInfestationLevel();
            if (num1 <= 0)
                num1 = 1;
            switch (this.LevelIndex)
            {
                case GameWorld.LevelId.Level_Town_1:
                case GameWorld.LevelId.Level_Town_2:
                    this.ZombiesLeftToKill = 10 + 8 * (num1 - 1);
                    break;
                case GameWorld.LevelId.Level_Forest_1:
                case GameWorld.LevelId.Level_Forest_2:
                    this.ZombiesLeftToKill = 12 + 8 * (num1 - 1);
                    break;
                case GameWorld.LevelId.Level_Graveyard_1:
                case GameWorld.LevelId.Level_Chapel_1:
                    this.ZombiesLeftToKill = 15 + 8 * (num1 - 1);
                    break;
                default:
                    this.ZombiesLeftToKill = 0;
                    break;
            }
            if (this.GetDayPart() == GameWorld.DayPart.Evening)
                this.ZombiesLeftToKill += 2;
            if (this.GetDayPart() == GameWorld.DayPart.Night)
                this.ZombiesLeftToKill += 4;
            this.ZombiesLeftToKill += MathHelperEx.GetRandomInt(0, this.DayIndex);
            this.ZombiesLeftToKillInit = this.ZombiesLeftToKill;
            this.IsZombieSpawnEnabled = true;
            this.IsGrabberZombieSpawnEnabled = true;
            this.IsArmoredZombieSpawnEnabled = true;
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                this.m_spawnGrabberRatio = 0;
                this.m_spawnArmoredRatio = 0;
                this.IsGrabberZombieSpawnEnabled = false;
                this.IsArmoredZombieSpawnEnabled = false;
                this.m_spawnTime = 3f;
                this.m_zombiesAliveNbMax = GameConstant.ZOMBIE_ALIVE_NB_BOSS;
                this.ZombiesLeftToKill = 0;
                this.ZombiesLeftToKillInit = 0;
                switch (this.LevelIndex)
                {
                    case GameWorld.LevelId.Level_Town_Boss:
                        this.m_spawnTime = 4f;
                        break;
                    case GameWorld.LevelId.Level_Forest_Boss:
                        if (Scenario.GetInstance().GetCurrentMissionIndex() >= 9 && Scenario.GetInstance().GetCurrentMissionIndex() < 99)
                        {
                            this.m_spawnTime = 4f;
                            break;
                        }
                        this.IsZombieSpawnEnabled = false;
                        break;
                    case GameWorld.LevelId.Level_Chapel_Boss:
                        this.IsZombieSpawnEnabled = false;
                        break;
                }
            }
            else if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
            {
                this.IsZombieSpawnEnabled = false;
                this.m_spawnGrabberRatio = 0;
                this.m_spawnArmoredRatio = 0;
                this.m_spawnTime = 4f;
                this.m_zombiesAliveNbMax = GameConstant.ZOMBIE_ALIVE_NB_TUTORIAL;
                this.ZombiesLeftToKill = 0;
                this.ZombiesLeftToKillInit = 0;
            }
            else if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Script)
            {
                this.IsZombieSpawnEnabled = false;
                this.m_spawnGrabberRatio = 0;
                this.m_spawnArmoredRatio = 0;
                this.m_spawnTime = 4f;
                this.m_zombiesAliveNbMax = 0;
                this.ZombiesLeftToKill = 0;
                this.ZombiesLeftToKillInit = 0;
            }
            else
            {
                int num2 = this.DayIndex - 1 + (num1 - 1) * 4;
                if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                    num2 += 10;
                this.m_spawnGrabberRatio = 10 + num2 * 2;
                if (this.m_spawnGrabberRatio > 30)
                    this.m_spawnGrabberRatio = 30;
                this.m_spawnArmoredRatio = 10 + num2 * 3;
                if (this.m_spawnArmoredRatio > 50)
                    this.m_spawnArmoredRatio = 50;
                switch (this.LevelIndex)
                {
                    case GameWorld.LevelId.Level_Town_1:
                    case GameWorld.LevelId.Level_Town_2:
                        this.m_zombiesAliveNbMax = GameConstant.ZOMBIE_ALIVE_NB_STORY_TOWN;
                        break;
                    case GameWorld.LevelId.Level_Forest_1:
                    case GameWorld.LevelId.Level_Forest_2:
                        this.m_zombiesAliveNbMax = GameConstant.ZOMBIE_ALIVE_NB_STORY_FOREST;
                        break;
                    case GameWorld.LevelId.Level_Graveyard_1:
                    case GameWorld.LevelId.Level_Chapel_1:
                        this.m_zombiesAliveNbMax = GameConstant.ZOMBIE_ALIVE_NB_STORY_GRAVEYARD;
                        break;
                    default:
                        this.m_zombiesAliveNbMax = 1;
                        break;
                }
                this.m_zombiesAliveNbMax += GameConstant.ZOMBIE_ALIVE_NB_STORY_INFESTATION_ADD * (num1 - 1);
                this.m_spawnTime = 4f;
                this.m_spawnTime -= (float)(num1 - 1) * 0.2f;
                this.m_spawnTime -= (float)(this.DayIndex - 1 + 10) * 0.07f;
                if (this.GetDayPart() == GameWorld.DayPart.Evening)
                    this.m_spawnTime -= 0.2f;
                if (this.GetDayPart() == GameWorld.DayPart.Night)
                    this.m_spawnTime -= 0.4f;
                if ((double)this.m_spawnTime >= 3.0)
                    return;
                this.m_spawnTime = 3f;
            }
        }

        public override void Update(float gameTime)
        {
            this.RemoveInactiveZombies();
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
                this.m_npcShop.Update(gameTime);
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Script)
                this.m_npcShop.Update(gameTime);
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                switch (this.LevelIndex)
                {
                    case GameWorld.LevelId.Level_Town_Boss:
                        this.m_bossButcher.Update(gameTime);
                        this.m_spawnSwarmTimer -= gameTime;
                        if ((double)this.m_spawnSwarmTimer <= 0.0)
                        {
                            this.SpawnZombieSwarm();
                            this.m_spawnSwarmTimer = this.m_spawnTime;
                            break;
                        }
                        break;
                    case GameWorld.LevelId.Level_Forest_Boss:
                    case GameWorld.LevelId.Level_Chapel_Boss:
                        if (this.LevelIndex == GameWorld.LevelId.Level_Forest_Boss && Scenario.GetInstance().GetCurrentMissionIndex() >= 9 && Scenario.GetInstance().GetCurrentMissionIndex() < 99)
                        {
                            this.m_bossButcher.Update(gameTime);
                            this.m_spawnSwarmTimer -= gameTime;
                            if ((double)this.m_spawnSwarmTimer <= 0.0)
                            {
                                this.SpawnZombieSwarm();
                                this.m_spawnSwarmTimer = this.m_spawnTime;
                                break;
                            }
                            break;
                        }
                        if (this.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
                            this.m_orb.Update(gameTime);
                        this.m_bossNecro.Update(gameTime);
                        if (this.m_bossNecro.IsInvoquing())
                        {
                            this.IsZombieSpawnEnabled = true;
                            if (!this.m_bossNecro.IsInvoqueDone)
                            {
                                this.m_zombiesToSpawnNb = 8;
                                this.m_bossNecro.IsInvoqueDone = true;
                                break;
                            }
                            break;
                        }
                        this.IsZombieSpawnEnabled = false;
                        break;
                }
            }
            else
            {
                this.m_spawnSwarmTimer -= gameTime;
                if ((double)this.m_spawnSwarmTimer <= 0.0)
                {
                    this.SpawnZombieSwarm();
                    this.m_spawnSwarmTimer = this.m_spawnTime;
                }
            }
            if (this.IsZombieSpawnEnabled && this.m_zombiesToSpawnNb > 0)
            {
                this.m_spawnTimer -= gameTime;
                if ((double)this.m_spawnTimer <= 0.0)
                {
                    this.SpawnZombie();
                    this.m_spawnTimer = 0.2f;
                    --this.m_zombiesToSpawnNb;
                }
            }
            this.m_hero.Update(gameTime);
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                float x = (float)(((double)this.m_hero.GetTransform().GetPosition().X + (double)this.GetBoss().GetTransform().GetPosition().X) * 0.5);
                if ((double)x > (double)this.m_hero.GetTransform().GetPosition().X + 0.800000011920929)
                    x = this.m_hero.GetTransform().GetPosition().X + 0.8f;
                if ((double)x < (double)this.m_hero.GetTransform().GetPosition().X - 0.800000011920929)
                    x = this.m_hero.GetTransform().GetPosition().X - 0.8f;
                this.m_cameraTarget.GetTransform().SetPosition(x, this.m_hero.GetTransform().GetPosition().Y);
                this.m_cameraTarget.GetTransform().SetScale(this.m_hero.GetTransform().GetScale());
            }
            this.m_camera.Update(gameTime);
            this.ConstraintToZone();
            this.UpdateExplosions(gameTime);
            this.UpdateBulletImpact(gameTime);
            this.UpdatePotions(gameTime);
            this.UpdateSpikes(gameTime);
            this.UpdateEarthquake(gameTime);
            base.Update(gameTime);
            this.m_hero.UpdateCollisions();
            NPCZombie[] buffer = new NPCZombie[55];
            int entitiesByType = this.GetEntityManager().GetEntitiesByType(3, (Entity[])buffer, 55);
            for (int index = 0; index < entitiesByType; ++index)
                buffer[index].UpdateCollisions();
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                switch (this.LevelIndex)
                {
                    case GameWorld.LevelId.Level_Town_Boss:
                        this.m_bossButcher.UpdateCollisions();
                        break;
                    case GameWorld.LevelId.Level_Forest_Boss:
                        if (Scenario.GetInstance().GetCurrentMissionIndex() >= 9 && Scenario.GetInstance().GetCurrentMissionIndex() < 99)
                        {
                            this.m_bossButcher.UpdateCollisions();
                            break;
                        }
                        this.m_bossNecro.UpdateCollisions();
                        for (int index = 0; index < 8; ++index)
                            this.m_spikeArray[index].UpdateCollisions();
                        break;
                    case GameWorld.LevelId.Level_Chapel_Boss:
                        this.m_bossNecro.UpdateCollisions();
                        this.m_orb.UpdateCollisions();
                        for (int index = 0; index < 8; ++index)
                            this.m_spikeArray[index].UpdateCollisions();
                        for (int index = 0; index < 4; ++index)
                            this.m_rockArray[index].UpdateCollisions();
                        break;
                }
            }
            this.m_hud.Update(gameTime, this.m_hero);
            if (GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story || this.GetCurrentLevel().GetLevelType() != Level.LevelType.Normal || this.m_levelSectorIndex >= 4)
                return;
            float num = this.EndOfLevelPosX * 0.25f;
            if ((double)this.m_hero.GetTransform().GetPosition().X <= (double)this.m_levelSectorIndex * (double)num)
                return;
            this.ZombiesLeftToKill += this.ZombiesLeftToKillInit;
            ++this.m_levelSectorIndex;
        }

        public override void Cleanup()
        {
            this.m_hero.Cleanup();
            this.m_bossButcher.Cleanup();
            this.m_bossNecro.Cleanup();
            this.m_npcShop.Cleanup();
            this.m_orb.Cleanup();
            for (int index = 0; index < 55; ++index)
                this.m_zombieArray[index].Cleanup();
            this.m_camera.Cleanup();
            this.m_hud.Cleanup();
            for (int index = 0; index < 8; ++index)
                this.m_explosionArray[index].Cleanup();
            for (int index = 0; index < 8; ++index)
                this.m_impactArray[index].Cleanup();
            Resanctified.Engine.Resources.Resource.SafeRelease<Texture>(ref this.m_zombieNormalTexture);
            Resanctified.Engine.Resources.Resource.SafeRelease<Texture>(ref this.m_zombieNormalTextureAlt1);
            Resanctified.Engine.Resources.Resource.SafeRelease<Texture>(ref this.m_zombieGrabberTextureAlt1);
            for (int index = 0; index < 6; ++index)
                this.m_miscAnimArray[index].Cleanup();
            for (int index = 0; index < 4; ++index)
                this.m_potionArray[index].Cleanup();
            for (int index = 0; index < 8; ++index)
                this.m_spikeArray[index].Cleanup();
            for (int index = 0; index < 4; ++index)
                this.m_rockArray[index].Cleanup();
            base.Cleanup();
        }

        private void SpawnZombieSwarm()
        {
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
            {
                int num = this.DayIndex - 1;
                if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                {
                    num += 10;
                    if (num >= 31)
                        num = 30;
                }
                int zombieSwarmNb1 = GameConstant.ZOMBIE_SWARM_NB_ARRAY[num * 2];
                int zombieSwarmNb2 = GameConstant.ZOMBIE_SWARM_NB_ARRAY[num * 2 + 1];
                if (this.ZombiesKilledNb > GameConstant.ZOMBIE_TOKILL_NB_ARRAY[this.DayIndex - 1])
                {
                    this.m_spawnTime += 0.1f;
                    this.m_zombiesToSpawnNb = this.ZombiesKilledNb <= GameConstant.ZOMBIE_TOKILL_NB_ARRAY[this.DayIndex - 1] + GameConstant.ZOMBIE_TOKILL_NB_ARRAY[this.DayIndex - 1] / 4 ? zombieSwarmNb1 : 1;
                }
                else
                    this.m_zombiesToSpawnNb = MathHelperEx.GetRandomInt(zombieSwarmNb1, zombieSwarmNb2);
            }
            else if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
            {
                int num = this.GetCurrentLevel().GetInfestationLevel() - 1;
                if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                    num = 2;
                if (num < 0)
                    num = 0;
                int min = GameConstant.ZOMBIE_STORY_SWARM_NB_ARRAY[num * 2];
                int zombieStorySwarmNb = GameConstant.ZOMBIE_STORY_SWARM_NB_ARRAY[num * 2 + 1];
                if (this.m_dayPart == GameWorld.DayPart.Evening || this.m_dayPart == GameWorld.DayPart.Night)
                {
                    ++min;
                    ++zombieStorySwarmNb;
                }
                if (this.LevelIndex == GameWorld.LevelId.Level_Town_Boss)
                {
                    min = 3;
                    if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                        ++min;
                }
                this.m_zombiesToSpawnNb = MathHelperEx.GetRandomInt(min, zombieStorySwarmNb);
            }
            else
                this.m_zombiesToSpawnNb = 1;
            this.m_spawnTimer = 0.0f;
        }

        private void SpawnZombie()
        {
            if (this.ZombiesAliveNb + 1 > this.m_zombiesAliveNbMax || GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && ((this.GetCurrentLevel().GetLevelType() == Level.LevelType.Normal || this.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop) && this.ZombiesLeftToKill - this.ZombiesAliveNb <= 0 || this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss && this.GetBoss().IsDead()))
                return;
            int randomInt = MathHelperEx.GetRandomInt(0, 100);
            if (this.IsArmoredZombieSpawnEnabled && MathHelperEx.GetRandomInt(0, 100) < this.m_spawnArmoredRatio)
            {
                for (int index = 0; index < 20; ++index)
                {
                    if (this.m_zombieArray[index].GetStateMachine().GetCurrentState() == StateDefault.GetInstance())
                    {
                        if (randomInt < this.m_spawnGrabberRatio && this.IsGrabberZombieSpawnEnabled)
                        {
                            this.m_zombieArray[index].SetZombieType(ZombieType.ArmoredGrabber);
                            this.m_zombieArray[index].GetView().RemapTexture(this.m_zombieNormalTexture, this.m_zombieGrabberTextureAlt1);
                        }
                        else
                        {
                            this.m_zombieArray[index].SetZombieType(ZombieType.ArmoredNormal);
                            if (MathHelperEx.GetRandomInt(0, 100) > 50)
                                this.m_zombieArray[index].GetView().RemapTexture(this.m_zombieNormalTexture, this.m_zombieNormalTextureAlt1);
                            else
                                this.m_zombieArray[index].GetView().RemapTexture((Texture)null, (Texture)null);
                        }
                        this.GetEntityManager().AddEntity((Entity)this.m_zombieArray[index]);
                        ++this.ZombiesAliveNb;
                        this.m_zombieArray[index].GetStateMachine().SetState((IState)NPCZombieStateSpawn.GetInstance());
                        return;
                    }
                }
            }
            for (int index = 20; index < 55; ++index)
            {
                if (this.m_zombieArray[index].GetStateMachine().GetCurrentState() == StateDefault.GetInstance())
                {
                    if (randomInt < this.m_spawnGrabberRatio && this.IsGrabberZombieSpawnEnabled)
                    {
                        this.m_zombieArray[index].SetZombieType(ZombieType.Grabber);
                        this.m_zombieArray[index].GetView().RemapTexture(this.m_zombieNormalTexture, this.m_zombieGrabberTextureAlt1);
                    }
                    else
                    {
                        this.m_zombieArray[index].SetZombieType(ZombieType.Normal);
                        if (this.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
                            this.m_zombieArray[index].GetView().RemapTexture(this.m_zombieNormalTexture, this.m_zombieNormalTextureAlt1);
                        else if (MathHelperEx.GetRandomInt(0, 100) > 50)
                            this.m_zombieArray[index].GetView().RemapTexture(this.m_zombieNormalTexture, this.m_zombieNormalTextureAlt1);
                        else
                            this.m_zombieArray[index].GetView().RemapTexture((Texture)null, (Texture)null);
                    }
                    this.GetEntityManager().AddEntity((Entity)this.m_zombieArray[index]);
                    ++this.ZombiesAliveNb;
                    this.m_zombieArray[index].GetStateMachine().SetState((IState)NPCZombieStateSpawn.GetInstance());
                    break;
                }
            }
        }

        private void RemoveInactiveZombies()
        {
            for (int index = 0; index < 55; ++index)
            {
                if (!this.m_zombieArray[index].IsActive)
                {
                    this.GetEntityManager().RemoveEntity((Entity)this.m_zombieArray[index]);
                    this.m_zombieArray[index].IsActive = true;
                }
            }
        }

        public void AddExplosion(float x, float y)
        {
            Vector3 position = this.m_explosionArray[this.m_explosionIndex].GetTransform().GetPosition() with
            {
                X = x,
                Y = y
            };
            this.m_explosionArray[this.m_explosionIndex].GetTransform().SetPosition(position);
            this.m_explosionArray[this.m_explosionIndex].GetView().GetAnimation2D().SetTime(0.0f);
            if (!this.m_explosionArray[this.m_explosionIndex].IsActive)
            {
                this.m_explosionArray[this.m_explosionIndex].GetSound().Play();
                this.m_explosionArray[this.m_explosionIndex].IsActive = true;
                Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View)this.m_explosionArray[this.m_explosionIndex].GetView(), this.m_explosionArray[this.m_explosionIndex].GetTransform());
            }
            ++this.m_explosionIndex;
            if (this.m_explosionIndex < 8)
                return;
            this.m_explosionIndex = 0;
        }

        private void UpdateExplosions(float gameTime)
        {
            for (int index = 0; index < 8; ++index)
            {
                if (this.m_explosionArray[index].IsActive)
                {
                    this.m_explosionArray[index].Update(gameTime);
                    if (this.m_explosionArray[index].GetView().GetAnimation2D().IsEndOfAnimationReached())
                    {
                        this.m_explosionArray[index].IsActive = false;
                        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View)this.m_explosionArray[index].GetView());
                    }
                }
            }
        }

        public void AddSpike(float x, float y)
        {
            Vector3 position = this.m_spikeArray[this.m_spikeIndex].GetTransform().GetPosition() with
            {
                X = x,
                Y = y
            };
            this.m_spikeArray[this.m_spikeIndex].GetTransform().SetPosition(position);
            this.m_spikeArray[this.m_spikeIndex].GetView().GetAnimation2D().SetTime(0.0f);
            if (!this.m_spikeArray[this.m_spikeIndex].IsActive)
                this.m_spikeArray[this.m_spikeIndex].Activate();
            ++this.m_spikeIndex;
            if (this.m_spikeIndex >= 8)
            {
                this.m_spikeTrapTimer = 0.0f;
                this.m_spikeIndex = 0;
            }
            else
                this.m_spikeTrapTimer = 0.5f;
        }

        public void EnableSpikeTrap()
        {
            this.m_spikeTrapStartPos = this.m_hero.GetTransform().GetPosition().X - 1f;
            this.m_spikeIndex = 0;
            this.AddSpike(this.m_spikeTrapStartPos, 0.0f);
            this.m_camera.StartShaking(2f, 78.53982f, new Vector2(0.0f, 0.1f), Vector2.Zero);
        }

        private void UpdateSpikes(float gameTime)
        {
            if ((double)this.m_spikeTrapTimer > 0.0)
            {
                this.m_spikeTrapTimer -= gameTime;
                if ((double)this.m_spikeTrapTimer <= 0.0)
                {
                    float x = this.m_spikeArray[this.m_spikeIndex - 1].GetTransform().GetPosition().X + 0.7f;
                    if ((double)x > 5.5)
                        this.m_spikeTrapTimer = 0.0f;
                    else
                        this.AddSpike(x, 0.0f);
                }
            }
            for (int index = 0; index < 8; ++index)
            {
                if (this.m_spikeArray[index].IsActive)
                    this.m_spikeArray[index].Update(gameTime);
            }
        }

        public void EnableEarthquake()
        {
            this.m_earthquakeEnabled = true;
            this.m_rockIndex = 0;
            this.m_rockArray[this.m_rockIndex].Activate();
            this.m_camera.StartShaking(9f, 78.53982f, new Vector2(0.0f, 0.1f), Vector2.Zero);
        }

        public void DisableEarthquake()
        {
            this.m_earthquakeEnabled = false;
            this.m_camera.StopShaking();
        }

        private void UpdateEarthquake(float gameTime)
        {
            if (this.m_earthquakeEnabled)
            {
                this.m_earthquakeTimer += gameTime;
                if ((double)this.m_earthquakeTimer >= 2.0)
                {
                    this.m_earthquakeTimer = 0.0f;
                    ++this.m_rockIndex;
                    if (this.m_rockIndex >= 4)
                        this.m_rockIndex = 0;
                    this.m_rockArray[this.m_rockIndex].Activate();
                }
            }
            for (int index = 0; index < 4; ++index)
                this.m_rockArray[index].Update(gameTime);
        }

        public void AddBulletImpact(float x, float y, float scaleX, BulletImpact.ImpactType type) => this.AddBulletImpact(x, y, scaleX, type, true);

        public void AddBulletImpact(
          float x,
          float y,
          float scaleX,
          BulletImpact.ImpactType type,
          bool soundEnabled)
        {
            Vector3 position = this.m_impactArray[this.m_impactIndex].GetTransform().GetPosition() with
            {
                X = x,
                Y = y
            };
            this.m_impactArray[this.m_impactIndex].GetTransform().SetPosition(position);
            this.m_impactArray[this.m_impactIndex].GetTransform().SetScale(scaleX, 1f);
            this.m_impactArray[this.m_impactIndex].SetImpactType(type);
            this.m_impactArray[this.m_impactIndex].GetView().GetAnimation2D().SetTime(0.0f);
            if (!this.m_impactArray[this.m_impactIndex].IsActive)
            {
                if (this.m_impactArray[this.m_impactIndex].GetSound() != null && soundEnabled)
                    this.m_impactArray[this.m_impactIndex].GetSound().Play();
                this.m_impactArray[this.m_impactIndex].IsActive = true;
                Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View)this.m_impactArray[this.m_impactIndex].GetView(), this.m_impactArray[this.m_impactIndex].GetTransform());
            }
            ++this.m_impactIndex;
            if (this.m_impactIndex < 8)
                return;
            this.m_impactIndex = 0;
        }

        private void UpdateBulletImpact(float gameTime)
        {
            for (int index = 0; index < 8; ++index)
            {
                if (this.m_impactArray[index].IsActive)
                {
                    this.m_impactArray[index].Update(gameTime);
                    if (this.m_impactArray[index].GetView().GetAnimation2D().IsEndOfAnimationReached())
                    {
                        this.m_impactArray[index].IsActive = false;
                        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View)this.m_impactArray[index].GetView());
                    }
                }
            }
        }

        public bool AddPotion(float x, float y)
        {
            int index1 = -1;
            for (int index2 = 0; index2 < 4; ++index2)
            {
                if (!this.m_potionArray[index2].IsActive)
                {
                    index1 = index2;
                    break;
                }
            }
            if (index1 < 0)
                return false;
            Vector3 position = this.m_potionArray[index1].GetTransform().GetPosition() with
            {
                X = x,
                Y = y
            };
            this.m_potionArray[index1].GetTransform().SetPosition(position);
            this.m_potionArray[index1].SetActive(true);
            this.ZombiesLeftToKillForPotion = GameApplication.GetInstance().GetDifficulty() != GameApplication.GameDifficulty.Hard ? MathHelperEx.GetRandomInt(20, 30) : MathHelperEx.GetRandomInt(30, 40);
            return true;
        }

        public int GetActivePotionsNumber()
        {
            int activePotionsNumber = 0;
            for (int index = 0; index < 4; ++index)
            {
                if (this.m_potionArray[index].IsActive)
                    ++activePotionsNumber;
            }
            return activePotionsNumber;
        }

        private void UpdatePotions(float gameTime)
        {
            for (int index = 0; index < 4; ++index)
            {
                if (this.m_potionArray[index].IsActive)
                    this.m_potionArray[index].Update(gameTime);
            }
        }

        public void ConstraintToZone()
        {
            Vector3 position1 = this.m_hero.GetTransform().GetPosition();
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                if (this.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
                {
                    if ((double)position1.X < 0.199999988079071)
                    {
                        position1.X = 0.2f;
                        this.m_hero.GetTransform().SetPosition(position1);
                    }
                }
                else if ((double)position1.X < -0.5)
                {
                    position1.X = -0.5f;
                    this.m_hero.GetTransform().SetPosition(position1);
                }
                if ((double)position1.X > 7.5)
                {
                    position1.X = 7.5f;
                    this.m_hero.GetTransform().SetPosition(position1);
                }
            }
            else if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
            {
                if ((double)position1.X < -1.0)
                {
                    position1.X = -1f;
                    this.m_hero.GetTransform().SetPosition(position1);
                }
                if ((double)position1.X > 8.0)
                {
                    position1.X = 8f;
                    this.m_hero.GetTransform().SetPosition(position1);
                }
            }
            else
            {
                if ((double)position1.X < -1.0)
                {
                    position1.X = -1f;
                    this.m_hero.GetTransform().SetPosition(position1);
                }
                if ((double)position1.X > (double)this.EndOfLevelPosX)
                {
                    position1.X = this.EndOfLevelPosX;
                    this.m_hero.GetTransform().SetPosition(position1);
                }
            }
            Vector3 position2 = this.m_camera.GetTransform().GetPosition();
            if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                if ((double)position2.X < 1.40000009536743)
                {
                    position2.X = 1.4f;
                    this.m_camera.GetTransform().SetPosition(position2);
                }
                if ((double)position2.X > 5.5)
                {
                    position2.X = 5.5f;
                    this.m_camera.GetTransform().SetPosition(position2);
                }
            }
            else if (this.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
            {
                if ((double)position2.X < 0.700000047683716)
                {
                    position2.X = 0.7f;
                    this.m_camera.GetTransform().SetPosition(position2);
                }
                if ((double)position2.X > 6.30000019073486)
                {
                    position2.X = 6.3f;
                    this.m_camera.GetTransform().SetPosition(position2);
                }
            }
            else
            {
                if ((double)position2.X < 0.600000023841858)
                {
                    position2.X = 0.6f;
                    this.m_camera.GetTransform().SetPosition(position2);
                }
                float num = this.EndOfLevelPosX - 1f;
                if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                    num = this.EndOfLevelPosX - 1.4f;
                if ((double)position2.X > (double)num)
                {
                    position2.X = num;
                    this.m_camera.GetTransform().SetPosition(position2);
                }
            }
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival || this.m_camera.IsLockedY())
                return;
            AABB boundingBox = this.m_camera.GetBoundingBox();
            if ((double)boundingBox.boundMin.Y < (double)this.WorldMin.Y)
                position2.Y = this.WorldMin.Y + boundingBox.extents.Y;
            else if ((double)boundingBox.boundMax.Y > (double)this.WorldMax.Y)
                position2.Y = this.WorldMax.Y - boundingBox.extents.Y;
            this.m_camera.GetTransform().SetPosition(position2);
        }

        private void LoadMiscAnimations()
        {
            int index = this.DayIndex / 10;
            this.m_miscAnimArray[0].Initialize(this.g_digitAnimFilename[this.DayIndex % 10]);
            this.m_miscAnimArray[1].Initialize(this.g_digitAnimFilename[index]);
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
                this.m_miscAnimArray[2].Initialize("Anims\\text_night");
            this.m_miscAnimArray[3].Initialize("Anims\\text_gameover");
            this.m_miscAnimArray[4].Initialize("Anims\\zombie_shadow");
            this.m_miscAnimArray[4].SetLoop(Animation2DLoop.Always);
            this.m_miscAnimArray[5].Initialize("Anims\\nun_shadow");
            this.m_miscAnimArray[5].SetLoop(Animation2DLoop.Always);
        }

        public void LoadGameData()
        {
            if (!Prefs.Instance.ReadGameData())
            {
                this.SetDefaultGameData();
            }
            else
            {
                GameApplication instance = GameApplication.GetInstance();
                Prefs.GameData cachedGameData = Prefs.Instance.CachedGameData;
                instance.Score = cachedGameData.score;
                this.DayIndex = cachedGameData.nightIndex;
                this.LevelIndex = (GameWorld.LevelId)cachedGameData.levelIndex;
                this.m_hero.Hearts = cachedGameData.hearts;
                this.m_hero.SetCurrentGunIndex(cachedGameData.currentGunIndex);
                this.m_hero.ManaPoints = cachedGameData.manaPoints;
                this.m_hero.HitPoints = cachedGameData.hitPoints;
                this.m_hero.SlashDamageLevel = cachedGameData.slashDamageLevel;
                this.m_hero.SlashManaDrainLevel = cachedGameData.slashManaDrainLevel;
                this.m_hero.HitPointsLevel = cachedGameData.hitPointsLevel;
                this.m_hero.ManaPointsLevel = cachedGameData.manaPointsLevel;
                this.m_hero.AirSlashAvailable = cachedGameData.airSlashAvailable;
                this.m_hero.AirSlashDamageLevel = cachedGameData.airSlashDamageLevel;
                Gun[] gunArray = this.m_hero.GetGunArray();
                for (int index = 0; index < 6; ++index)
                {
                    gunArray[index].IsAvailable = cachedGameData.m_weaponAvailable[index];
                    gunArray[index].SetLevel(cachedGameData.m_weaponLevels[index]);
                }
                instance.AchievementCounter_100ZombiesKilled = cachedGameData.m_achievementCounter_100ZombiesKilled;
                instance.AchievementCounter_500ZombiesKilled = cachedGameData.m_achievementCounter_500ZombiesKilled;
                instance.AchievementCounter_1000ZombiesKilled = cachedGameData.m_achievementCounter_1000ZombiesKilled;
                instance.AchievementCounter_500ZombiesKilledAirSlash = cachedGameData.m_achievementCounter_500ZombiesKilledAirSlash;
                instance.AchievementCounter_500ZombiesKilledBullet = cachedGameData.m_achievementCounter_500ZombiesKilledBullet;
                instance.AchievementCounter_500ZombiesKilledFlameThrower = cachedGameData.m_achievementCounter_500ZombiesKilledFlameThrower;
                instance.AchievementCounter_500ZombiesKilledIceGun = cachedGameData.m_achievementCounter_500ZombiesKilledIceGun;
                instance.AchievementCounter_500ZombiesKilledExplosiveGun = cachedGameData.m_achievementCounter_500ZombiesKilledExplosiveGun;
                instance.AchievementCounter_1000ZombiesKilledHolyBeam = cachedGameData.m_achievementCounter_1000ZombiesKilledHolyBeam;
                instance.AchievementCounter_100Ungrab = cachedGameData.m_achievementCounter_100Ungrab;
                instance.Achievement_OneLife = cachedGameData.m_achievement_OneLife;
                for (int index = 0; index < 14; ++index)
                {
                    this.m_levelArray[index].SetInfestationLevel(cachedGameData.m_storyLevelsInfestation[index]);
                    this.m_levelArray[index].Locked = cachedGameData.m_storyLevelsLocked[index];
                }
                this.m_dayPart = (GameWorld.DayPart)cachedGameData.m_dayPart;
                Scenario.GetInstance().SetCurrentMissionIndex(cachedGameData.m_missionIndex);
                if (!this.LoadMissionPartDisabled)
                {
                    Scenario.GetInstance().SetCurrentMissionPart(cachedGameData.m_missionPart);
                    Scenario.GetInstance().InitializeMission();
                }
                GameApplication.GetInstance().SetDifficulty((GameApplication.GameDifficulty)cachedGameData.m_difficulty);
            }
        }

        public void SaveGameData()
        {
            GameApplication instance = GameApplication.GetInstance();
            Prefs.GameData cachedGameData = Prefs.Instance.CachedGameData;
            cachedGameData.score = instance.Score;
            cachedGameData.nightIndex = this.DayIndex;
            cachedGameData.levelIndex = (int)this.LevelIndex;
            cachedGameData.hearts = this.m_hero.Hearts;
            cachedGameData.currentGunIndex = this.m_hero.GetCurrentGunIndex();
            cachedGameData.manaPoints = this.m_hero.ManaPoints;
            cachedGameData.hitPoints = this.m_hero.HitPoints;
            cachedGameData.slashDamageLevel = this.m_hero.SlashDamageLevel;
            cachedGameData.slashManaDrainLevel = this.m_hero.SlashManaDrainLevel;
            cachedGameData.hitPointsLevel = this.m_hero.HitPointsLevel;
            cachedGameData.manaPointsLevel = this.m_hero.ManaPointsLevel;
            cachedGameData.airSlashAvailable = this.m_hero.AirSlashAvailable;
            cachedGameData.airSlashDamageLevel = this.m_hero.AirSlashDamageLevel;
            Gun[] gunArray = this.m_hero.GetGunArray();
            for (int index = 0; index < 6; ++index)
            {
                cachedGameData.m_weaponLevels[index] = gunArray[index].GetLevel();
                cachedGameData.m_weaponAvailable[index] = gunArray[index].IsAvailable;
            }
            cachedGameData.m_achievementCounter_100ZombiesKilled = instance.AchievementCounter_100ZombiesKilled;
            cachedGameData.m_achievementCounter_500ZombiesKilled = instance.AchievementCounter_500ZombiesKilled;
            cachedGameData.m_achievementCounter_1000ZombiesKilled = instance.AchievementCounter_1000ZombiesKilled;
            cachedGameData.m_achievementCounter_500ZombiesKilledAirSlash = instance.AchievementCounter_500ZombiesKilledAirSlash;
            cachedGameData.m_achievementCounter_500ZombiesKilledBullet = instance.AchievementCounter_500ZombiesKilledBullet;
            cachedGameData.m_achievementCounter_500ZombiesKilledFlameThrower = instance.AchievementCounter_500ZombiesKilledFlameThrower;
            cachedGameData.m_achievementCounter_500ZombiesKilledIceGun = instance.AchievementCounter_500ZombiesKilledIceGun;
            cachedGameData.m_achievementCounter_500ZombiesKilledExplosiveGun = instance.AchievementCounter_500ZombiesKilledExplosiveGun;
            cachedGameData.m_achievementCounter_1000ZombiesKilledHolyBeam = instance.AchievementCounter_1000ZombiesKilledHolyBeam;
            cachedGameData.m_achievementCounter_100Ungrab = instance.AchievementCounter_100Ungrab;
            cachedGameData.m_achievement_OneLife = instance.Achievement_OneLife;
            for (int index = 0; index < 14; ++index)
            {
                cachedGameData.m_storyLevelsInfestation[index] = this.m_levelArray[index].GetInfestationLevel();
                cachedGameData.m_storyLevelsLocked[index] = this.m_levelArray[index].Locked;
            }
            cachedGameData.m_dayPart = (int)this.m_dayPart;
            cachedGameData.m_missionIndex = Scenario.GetInstance().GetCurrentMissionIndex();
            cachedGameData.m_missionPart = Scenario.GetInstance().GetCurrentMissionPartToSave();
            cachedGameData.m_difficulty = (int)GameApplication.GetInstance().GetDifficulty();
            Prefs.Instance.WriteGameData();
        }

        public void SetDefaultGameData()
        {
            GameApplication instance = GameApplication.GetInstance();
            instance.Score = 0;
            this.DayIndex = 1;
            this.LevelIndex = GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GameWorld.LevelId.Level_Survival_1 : GameWorld.LevelId.Level_Shop;
            this.m_hero.Hearts = 0;
            this.m_hero.SetCurrentGunIndex(0);
            this.m_hero.ManaPoints = 100;
            this.m_hero.HitPoints = 100;
            this.m_hero.SlashDamageLevel = 1;
            this.m_hero.SlashManaDrainLevel = 1;
            this.m_hero.HitPointsLevel = 1;
            this.m_hero.ManaPointsLevel = 1;
            this.m_hero.AirSlashDamageLevel = 1;
            this.m_hero.AirSlashAvailable = false;
            Gun[] gunArray = this.m_hero.GetGunArray();
            for (int index = 0; index < 6; ++index)
            {
                gunArray[index].IsAvailable = false;
                gunArray[index].SetLevel(1);
            }
            gunArray[0].IsAvailable = true;
            gunArray[0].SetLevel(1);
            instance.AchievementCounter_100ZombiesKilled = 0;
            instance.AchievementCounter_500ZombiesKilled = 0;
            instance.AchievementCounter_1000ZombiesKilled = 0;
            instance.AchievementCounter_500ZombiesKilledAirSlash = 0;
            instance.AchievementCounter_500ZombiesKilledBullet = 0;
            instance.AchievementCounter_500ZombiesKilledFlameThrower = 0;
            instance.AchievementCounter_500ZombiesKilledIceGun = 0;
            instance.AchievementCounter_500ZombiesKilledExplosiveGun = 0;
            instance.AchievementCounter_1000ZombiesKilledHolyBeam = 0;
            instance.AchievementCounter_100Ungrab = 0;
            instance.Achievement_OneLife = true;
            for (int index = 0; index < 14; ++index)
            {
                this.m_levelArray[index].SetInfestationLevel(0);
                this.m_levelArray[index].Locked = index > 3;
            }
            this.m_dayPart = GameWorld.DayPart.Morning;
            Scenario.GetInstance().SetCurrentMissionIndex(0);
            Scenario.GetInstance().SetCurrentMissionPart(0);
        }

        public enum MiscAnimId
        {
            Digit_0,
            Digit_1,
            Night,
            GameOver,
            ZombieShadow,
            HeroShadow,
            ArraySize,
        }

        public enum DayPart
        {
            Morning,
            Day,
            Evening,
            Night,
        }

        public enum LevelId
        {
            Level_Survival_1,
            Level_Survival_2,
            Level_Survival_3,
            Level_Shop,
            Level_Town_1,
            Level_Town_2,
            Level_Town_Boss,
            Level_Forest_1,
            Level_Forest_2,
            Level_Forest_Boss,
            Level_Graveyard_1,
            Level_Chapel_1,
            Level_Chapel_Boss,
            Level_Ending,
            Number,
        }
    }
}
