using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.Tools;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.Resources;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState
{
    public sealed class GameStateLoadLevel : IDrawableState, IState
    {
        private const float HINT_TEXT_WIDTH = 440f;
        // How long the finished loading screen (with its hint) stays up before the level starts.
        private const float HINT_DISPLAY_TIME = 3f;
        public bool ContinueGameRequest;
        private int m_phase;
        private float m_hintDisplayTime;
        private int m_numFontsToLoad;
        private int m_numFontsLoaded;
        private int m_numAnimsToLoad;
        private int m_numAnimsLoaded;
        private int m_numTexturesToLoad;
        private int m_numTexturesLoaded;
        private int m_numSoundsToLoad;
        private int m_numSoundsLoaded;
        private int m_currentResourceIndex;
        private int m_percentageFonts;
        private int m_percentageAnims;
        private int m_percentageTextures;
        private int m_percentageSounds;
        private string m_hint1;
        private List<OLPair> m_hint1Pairs = new List<OLPair>(10);
        private Transform m_t = new Transform();
        private StringBuilder m_stringBuilder = new StringBuilder(16);
        private static readonly GameStateLoadLevel s_instance = new GameStateLoadLevel();

        public static GameStateLoadLevel Instance => s_instance;

        public static GameStateLoadLevel GetInstance() => s_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.m_phase = 0;
            this.m_hintDisplayTime = 0.0f;
            this.m_numFontsToLoad = 0;
            this.m_numFontsLoaded = 0;
            this.m_numAnimsToLoad = 0;
            this.m_numAnimsLoaded = 0;
            this.m_numTexturesToLoad = 0;
            this.m_numTexturesLoaded = 0;
            this.m_numSoundsToLoad = 0;
            this.m_numSoundsLoaded = 0;
            this.m_currentResourceIndex = 0;
            this.m_percentageFonts = 0;
            this.m_percentageAnims = 0;
            this.m_percentageTextures = 0;
            this.m_percentageSounds = 0;
            int randomInt = MathHelperEx.GetRandomInt(0, 90);
            this.m_hint1 = randomInt >= 5 ? (randomInt >= 10 ? (randomInt >= 15 ? (randomInt >= 20 ? (randomInt >= 25 ? (randomInt >= 30 ? (randomInt >= 35 ? (randomInt >= 40 ? (randomInt >= 45 ? (randomInt >= 50 ? (randomInt >= 55 ? (randomInt >= 60 ? (randomInt >= 65 ? (randomInt >= 70 ? (randomInt >= 75 ? (randomInt >= 85 ? (randomInt >= 90 || GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_17) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_Story_3)) : (GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_7) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_Story_2))) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_15)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_14)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_13)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_12)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_11)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_10)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_9)) : (GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_8) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_8_Story))) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_7)) : (GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_6) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_Story_1))) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_5)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_4)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_3)) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_2)) : (GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_1) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_1_Story));
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            if ((currentWorld.LevelIndex == GameWorld.LevelId.Level_Forest_Boss || currentWorld.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss) && randomInt < 30)
                this.m_hint1 = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Hint_Boss_1);
            this.m_hint1Pairs.Clear();
            if (this.m_hint1 == null)
                return;
            TextUtils.WrapText(ref this.m_hint1Pairs, this.m_hint1, GameApplication.GetInstance().GetFont(), 440f);
        }

        private void RemapParallaxTextures()
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            Level currentLevel = currentWorld.GetCurrentLevel();
            ParallaxEntity[] buffer = new ParallaxEntity[128];
            int entitiesByType = currentWorld.GetEntityManager().GetEntitiesByType(1, buffer, 128);
            for (int index1 = 0; index1 < entitiesByType; ++index1)
            {
                Texture[] textures = buffer[index1].GetAnimation2D().GetModel().GetTextures();
                if (textures != null)
                {
                    for (int index2 = 0; index2 < textures.Length; ++index2)
                    {
                        // Resource names are normalized by ResourceManager.NormalizeContentPath: lowercase, '/' separators.
                        if (textures[index2].GetResourceFilename().StartsWith("textures/sky"))
                        {
                            Resource.SafeRelease(ref textures[index2]);
                            switch (currentWorld.GetDayPart())
                            {
                                case GameWorld.DayPart.Morning:
                                    textures[index2] = ResourceManager.Instance.LoadTexture(currentLevel.GetTextureSkyMorningPath());
                                    continue;
                                case GameWorld.DayPart.Day:
                                    textures[index2] = ResourceManager.Instance.LoadTexture(currentLevel.GetTextureSkyDayPath());
                                    continue;
                                case GameWorld.DayPart.Evening:
                                    textures[index2] = ResourceManager.Instance.LoadTexture(currentLevel.GetTextureSkyEveningPath());
                                    continue;
                                case GameWorld.DayPart.Night:
                                    textures[index2] = ResourceManager.Instance.LoadTexture(currentLevel.GetTextureSkyNightPath());
                                    continue;
                                default:
                                    continue;
                            }
                        }
                        else if (textures[index2].GetResourceFilename().StartsWith("textures/texture"))
                        {
                            Resource.SafeRelease(ref textures[index2]);
                            switch (currentWorld.GetDayPart())
                            {
                                case GameWorld.DayPart.Morning:
                                case GameWorld.DayPart.Day:
                                    textures[index2] = ResourceManager.Instance.LoadTexture(currentLevel.GetTextureDayPath());
                                    continue;
                                case GameWorld.DayPart.Evening:
                                case GameWorld.DayPart.Night:
                                    textures[index2] = ResourceManager.Instance.LoadTexture(currentLevel.GetTextureNightPath());
                                    continue;
                                default:
                                    continue;
                            }
                        }
                    }
                }
            }
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            ResourceManager instance = ResourceManager.GetInstance();
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            if (this.m_phase == 0)
            {
                currentWorld.Initialize(this.ContinueGameRequest);
                new GameLevelLoader().LoadLevel(currentWorld.GetCurrentLevel().GetModelPath(), currentWorld.GetEntityFactory(), currentWorld.GetEntityManager());
                ParallaxEntity[] buffer = new ParallaxEntity[128];
                int entitiesByType = currentWorld.GetEntityManager().GetEntitiesByType(1, buffer, 128);
                for (int index = 0; index < entitiesByType; ++index)
                    buffer[index].MoveRelativeToCamera(currentWorld.GetCamera(), currentWorld.GetCamera().GetTransform().GetWorldPosition());
                this.m_numFontsToLoad = instance.BeginLoadingResources(Resource.ResourceType.Font);
                this.m_currentResourceIndex = 0;
                ++this.m_phase;
            }
            else if (this.m_phase == 1)
            {
                if (this.m_currentResourceIndex < this.m_numFontsToLoad)
                {
                    instance.LoadResource(this.m_currentResourceIndex);
                    ++this.m_currentResourceIndex;
                    ++this.m_numFontsLoaded;
                    this.m_percentageFonts = this.m_numFontsLoaded * 100 / this.m_numFontsToLoad;
                }
                else
                {
                    instance.EndLoadingResources();
                    this.m_numAnimsToLoad = instance.BeginLoadingResources(Resource.ResourceType.Animation2DModel);
                    this.m_currentResourceIndex = 0;
                    this.m_percentageFonts = 100;
                    ++this.m_phase;
                }
            }
            else if (this.m_phase == 2)
            {
                if (this.m_currentResourceIndex < this.m_numAnimsToLoad)
                {
                    instance.LoadResource(this.m_currentResourceIndex);
                    ++this.m_currentResourceIndex;
                    ++this.m_numAnimsLoaded;
                    this.m_percentageAnims = this.m_numAnimsLoaded * 100 / this.m_numAnimsToLoad;
                }
                else
                {
                    instance.EndLoadingResources();
                    if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                        this.RemapParallaxTextures();
                    this.m_numTexturesToLoad = instance.BeginLoadingResources(Resource.ResourceType.Texture);
                    this.m_currentResourceIndex = 0;
                    this.m_percentageAnims = 100;
                    ++this.m_phase;
                    instance.DestroyUnusedResources();
                    GC.Collect();
                }
            }
            else if (this.m_phase == 3)
            {
                if (this.m_currentResourceIndex < this.m_numTexturesToLoad)
                {
                    instance.LoadResource(this.m_currentResourceIndex);
                    ++this.m_currentResourceIndex;
                    ++this.m_numTexturesLoaded;
                    this.m_percentageTextures = this.m_numTexturesLoaded * 100 / this.m_numTexturesToLoad;
                }
                else
                {
                    instance.EndLoadingResources();
                    this.m_numSoundsToLoad = instance.BeginLoadingResources(Resource.ResourceType.SoundModel);
                    this.m_currentResourceIndex = 0;
                    this.m_percentageTextures = 100;
                    ++this.m_phase;
                }
            }
            else if (this.m_phase == 4)
            {
                if (this.m_currentResourceIndex < this.m_numSoundsToLoad)
                {
                    instance.LoadResource(this.m_currentResourceIndex);
                    ++this.m_currentResourceIndex;
                    ++this.m_numSoundsLoaded;
                    this.m_percentageSounds = this.m_numSoundsLoaded * 100 / this.m_numSoundsToLoad;
                }
                else
                {
                    instance.EndLoadingResources();
                    this.m_percentageSounds = 100;
                    AudioManager.GetInstance().StopMusic();
                    AudioManager.GetInstance().UnloadMusic();
                    ++this.m_phase;
                }
            }
            else if (this.m_phase == 5)
            {
                bool isStory = GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story;
                // The phone build blocked here with Thread.Sleep(3000), which froze the desktop window;
                // counting frame time keeps the same pause while the window stays responsive.
                if (!isStory || currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Shop)
                {
                    this.m_hintDisplayTime += gameTime;
                    if (this.m_hintDisplayTime < HINT_DISPLAY_TIME)
                        return;
                }
                if (isStory)
                {
                    switch (currentWorld.GetCurrentLevel().GetLevelType())
                    {
                        case Level.LevelType.Normal:
                            if (currentWorld.GetDayPart() == GameWorld.DayPart.Morning)
                                AudioManager.GetInstance().LoadMusic("Sounds\\music_01");
                            if (currentWorld.GetDayPart() == GameWorld.DayPart.Day)
                                AudioManager.GetInstance().LoadMusic("Sounds\\music_02");
                            if (currentWorld.GetDayPart() == GameWorld.DayPart.Evening)
                                AudioManager.GetInstance().LoadMusic("Sounds\\music_01");
                            if (currentWorld.GetDayPart() == GameWorld.DayPart.Night)
                            {
                                AudioManager.GetInstance().LoadMusic("Sounds\\music_03");
                                break;
                            }
                            break;
                        case Level.LevelType.Boss:
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_04");
                            break;
                        case Level.LevelType.Shop:
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_credits");
                            break;
                        default:
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_01");
                            break;
                    }
                }
                else
                {
                    switch (currentWorld.LevelIndex)
                    {
                        case GameWorld.LevelId.Level_Survival_1:
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_01");
                            break;
                        case GameWorld.LevelId.Level_Survival_2:
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_02");
                            break;
                        case GameWorld.LevelId.Level_Survival_3:
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_03");
                            break;
                        default:
                            AudioManager.GetInstance().LoadMusic("Sounds\\music_01");
                            break;
                    }
                }
                GC.Collect();
                ++this.m_phase;
            }
            else
            {
                if (this.m_phase != 6)
                    return;
                stateMachine.SetState(GameStateInGame.Instance);
                ++this.m_phase;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine) => this.ContinueGameRequest = true;

        public void Draw(IFiniteStateMachine stateMachine)
        {
            Font font = GameApplication.GetInstance().GetFont();
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.ClearColor = Color.Black;
            instance.Begin2DMode(true);
            if (this.m_hint1 != null)
            {
                this.m_t.SetPosition(240f, (float)(160.0 + (double)font.GetLineHeight() * 2.0));
                this.m_t.SetScale(1f);
                instance.DrawString(font, this.m_t, 6, Color.White, this.m_hint1, this.m_hint1Pairs, 0, this.m_hint1Pairs.Count, true);
            }
            this.m_t.SetScale(0.8f);
            int num = (this.m_percentageFonts + this.m_percentageAnims + this.m_percentageTextures + this.m_percentageSounds) / 4;
            this.m_t.SetPosition(240f, (float)(160.0 - (double)font.GetLineHeight() * 3.0));
            instance.DrawString(font, this.m_t, 6, Color.White, GameLocalisation.Instance.GetString(GameLocalisation.TextId.Loading));
            this.m_stringBuilder.Length = 0;
            StringHelper.Append(this.m_stringBuilder, num, 2).Append('%');
            instance.DrawString(font, this.m_t, 4, Color.White, this.m_stringBuilder);
            instance.End2DMode();
        }
    }
}
