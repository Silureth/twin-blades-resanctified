using System;
using System.IO;
using Resanctified.Engine.IO;

namespace Resanctified.Game
{
    /// <summary>
    /// Cached player data and its binary save files. The byte layout is unchanged from the
    /// Windows Phone release; the files themselves live wherever FileManager points.
    /// </summary>
    public sealed class Prefs
    {
        public const int GAMEDATA_VERSION = 1;
        public const int PREFS_FILE_SIZE = 512;
        private static readonly Prefs s_instance = new Prefs();
        private static readonly string GAMEDATA_SURVIVAL_FILENAME = "TwinBladesGameData.sav";
        private static readonly string GAMEDATA_STORY_FILENAME = "TwinBladesStoryGameData.sav";
        private static readonly string USERSETTINGS_FILENAME = "TwinBladesSettings.sav";
        private static readonly string HIGHSCORES_FILENAME = "TwinBladesHighscores.sav";
        private static readonly string ACHIEVEMENTS_FILENAME = "TwinBladesRewards.sav";
        private UserSettings m_userSettings = new UserSettings();
        private GameData m_gameData = new GameData();
        private Highscores m_highscores = new Highscores();
        private Achievements m_achievements = new Achievements();

        public static Prefs Instance => s_instance;

        public static Prefs GetInstance() => s_instance;

        public UserSettings CachedUserSettings => this.m_userSettings;

        public GameData CachedGameData => this.m_gameData;

        public Highscores CachedHighscores => this.m_highscores;

        public Achievements CachedAchievements => this.m_achievements;

        private Prefs()
        {
        }

        /// <summary>Expects FileManager.Instance to be initialized first.</summary>
        public void Initialize()
        {
            this.m_userSettings.version = 0;
            this.m_gameData.version = 0;
            this.m_highscores.version = 0;
            this.m_achievements.version = 0;
        }

        public void Cleanup()
        {
        }

        public bool ReadUserSettings() => ReadFile(USERSETTINGS_FILENAME, binaryReader =>
        {
            UserSettings userSettings = this.m_userSettings;
            userSettings.version = binaryReader.ReadInt32();
            userSettings.languageId = binaryReader.ReadInt32();
            userSettings.musicGain = binaryReader.ReadSingle();
            userSettings.sfxGain = binaryReader.ReadSingle();
            userSettings.hudAlpha = binaryReader.ReadSingle();
        });

        public bool ReadAchievements() => ReadFile(ACHIEVEMENTS_FILENAME, binaryReader =>
        {
            Achievements achievements = this.m_achievements;
            achievements.version = binaryReader.ReadInt32();
            achievements.achievement_Survival_500ZombiesKilledBullet = binaryReader.ReadBoolean();
            achievements.achievement_Survival_500ZombiesKilledFlameThrower = binaryReader.ReadBoolean();
            achievements.achievement_Survival_500ZombiesKilledIceGun = binaryReader.ReadBoolean();
            achievements.achievement_Survival_500ZombiesKilledExplosiveGun = binaryReader.ReadBoolean();
            achievements.achievement_Survival_1000ZombiesKilledHolyBeam = binaryReader.ReadBoolean();
            achievements.achievement_Survival_500ZombiesKilledAirSlash = binaryReader.ReadBoolean();
            achievements.achievement_Survival_100Ungrab = binaryReader.ReadBoolean();
            achievements.achievement_Survival_AllWeaponsUpgraded = binaryReader.ReadBoolean();
            achievements.achievement_Survival_AllAbilitiesUpgraded = binaryReader.ReadBoolean();
            achievements.achievement_Survival_GameFinished = binaryReader.ReadBoolean();
            achievements.achievement_Survival_GameFinishedHard = binaryReader.ReadBoolean();
            achievements.achievement_Story_100ZombiesKilled = binaryReader.ReadBoolean();
            achievements.achievement_Story_500ZombiesKilled = binaryReader.ReadBoolean();
            achievements.achievement_Story_1000ZombiesKilled = binaryReader.ReadBoolean();
            achievements.achievement_Story_Boss1Killed = binaryReader.ReadBoolean();
            achievements.achievement_Story_Boss2Killed = binaryReader.ReadBoolean();
            achievements.achievement_Story_Boss3Killed = binaryReader.ReadBoolean();
            achievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded = binaryReader.ReadBoolean();
            achievements.achievement_Story_GameFinished = binaryReader.ReadBoolean();
            achievements.achievement_Story_GameFinishedHard = binaryReader.ReadBoolean();
        });

        public bool ReadGameData() => ReadFile(GetGameDataFilename(), binaryReader =>
        {
            GameData gameData = this.m_gameData;
            gameData.version = binaryReader.ReadInt32();
            gameData.score = binaryReader.ReadInt32();
            gameData.nightIndex = binaryReader.ReadInt32();
            gameData.levelIndex = binaryReader.ReadInt32();
            gameData.hearts = binaryReader.ReadInt32();
            gameData.currentGunIndex = binaryReader.ReadInt32();
            gameData.manaPoints = binaryReader.ReadInt32();
            gameData.hitPoints = binaryReader.ReadInt32();
            gameData.slashDamageLevel = binaryReader.ReadInt32();
            gameData.slashManaDrainLevel = binaryReader.ReadInt32();
            gameData.hitPointsLevel = binaryReader.ReadInt32();
            gameData.manaPointsLevel = binaryReader.ReadInt32();
            gameData.airSlashDamageLevel = binaryReader.ReadInt32();
            gameData.airSlashAvailable = binaryReader.ReadBoolean();
            for (int index = 0; index < 6; ++index)
            {
                gameData.m_weaponLevels[index] = binaryReader.ReadInt32();
                gameData.m_weaponAvailable[index] = binaryReader.ReadBoolean();
            }
            gameData.m_achievementCounter_100ZombiesKilled = binaryReader.ReadInt32();
            gameData.m_achievementCounter_500ZombiesKilled = binaryReader.ReadInt32();
            gameData.m_achievementCounter_1000ZombiesKilled = binaryReader.ReadInt32();
            gameData.m_achievementCounter_500ZombiesKilledAirSlash = binaryReader.ReadInt32();
            gameData.m_achievementCounter_500ZombiesKilledBullet = binaryReader.ReadInt32();
            gameData.m_achievementCounter_500ZombiesKilledFlameThrower = binaryReader.ReadInt32();
            gameData.m_achievementCounter_500ZombiesKilledIceGun = binaryReader.ReadInt32();
            gameData.m_achievementCounter_500ZombiesKilledExplosiveGun = binaryReader.ReadInt32();
            gameData.m_achievementCounter_1000ZombiesKilledHolyBeam = binaryReader.ReadInt32();
            gameData.m_achievementCounter_100Ungrab = binaryReader.ReadInt32();
            gameData.m_achievement_OneLife = binaryReader.ReadBoolean();
            gameData.GameOverFlag = binaryReader.ReadBoolean();
            for (int index = 0; index < 14; ++index)
            {
                gameData.m_storyLevelsInfestation[index] = binaryReader.ReadInt32();
                gameData.m_storyLevelsLocked[index] = binaryReader.ReadBoolean();
            }
            gameData.m_dayPart = binaryReader.ReadInt32();
            gameData.m_missionIndex = binaryReader.ReadInt32();
            gameData.m_missionPart = binaryReader.ReadInt32();
            gameData.m_difficulty = binaryReader.ReadInt32();
        });

        public bool ReadHighscores() => ReadFile(HIGHSCORES_FILENAME, binaryReader =>
        {
            Highscores highscores = this.m_highscores;
            highscores.version = binaryReader.ReadInt32();
            for (int index = 0; index < 5; ++index)
                highscores.highscoresSurvival[index] = binaryReader.ReadInt32();
            for (int index = 0; index < 5; ++index)
                highscores.highscoresStory[index] = binaryReader.ReadInt32();
        });

        public bool WriteUserSettings()
        {
            this.m_userSettings.version = 1;
            return WriteFile(USERSETTINGS_FILENAME, binaryWriter =>
            {
                UserSettings userSettings = this.m_userSettings;
                binaryWriter.Write(userSettings.version);
                binaryWriter.Write(userSettings.languageId);
                binaryWriter.Write(userSettings.musicGain);
                binaryWriter.Write(userSettings.sfxGain);
                binaryWriter.Write(userSettings.hudAlpha);
            });
        }

        public bool WriteAchievements()
        {
            this.m_achievements.version = 1;
            return WriteFile(ACHIEVEMENTS_FILENAME, binaryWriter =>
            {
                Achievements achievements = this.m_achievements;
                binaryWriter.Write(achievements.version);
                binaryWriter.Write(achievements.achievement_Survival_500ZombiesKilledBullet);
                binaryWriter.Write(achievements.achievement_Survival_500ZombiesKilledFlameThrower);
                binaryWriter.Write(achievements.achievement_Survival_500ZombiesKilledIceGun);
                binaryWriter.Write(achievements.achievement_Survival_500ZombiesKilledExplosiveGun);
                binaryWriter.Write(achievements.achievement_Survival_1000ZombiesKilledHolyBeam);
                binaryWriter.Write(achievements.achievement_Survival_500ZombiesKilledAirSlash);
                binaryWriter.Write(achievements.achievement_Survival_100Ungrab);
                binaryWriter.Write(achievements.achievement_Survival_AllWeaponsUpgraded);
                binaryWriter.Write(achievements.achievement_Survival_AllAbilitiesUpgraded);
                binaryWriter.Write(achievements.achievement_Survival_GameFinished);
                binaryWriter.Write(achievements.achievement_Survival_GameFinishedHard);
                binaryWriter.Write(achievements.achievement_Story_100ZombiesKilled);
                binaryWriter.Write(achievements.achievement_Story_500ZombiesKilled);
                binaryWriter.Write(achievements.achievement_Story_1000ZombiesKilled);
                binaryWriter.Write(achievements.achievement_Story_Boss1Killed);
                binaryWriter.Write(achievements.achievement_Story_Boss2Killed);
                binaryWriter.Write(achievements.achievement_Story_Boss3Killed);
                binaryWriter.Write(achievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded);
                binaryWriter.Write(achievements.achievement_Story_GameFinished);
                binaryWriter.Write(achievements.achievement_Story_GameFinishedHard);
            });
        }

        public bool WriteHighscores()
        {
            this.m_highscores.version = 1;
            return WriteFile(HIGHSCORES_FILENAME, binaryWriter =>
            {
                Highscores highscores = this.m_highscores;
                binaryWriter.Write(highscores.version);
                for (int index = 0; index < 5; ++index)
                    binaryWriter.Write(highscores.highscoresSurvival[index]);
                for (int index = 0; index < 5; ++index)
                    binaryWriter.Write(highscores.highscoresStory[index]);
            });
        }

        public bool WriteGameData()
        {
            this.m_gameData.version = 1;
            return WriteFile(GetGameDataFilename(), binaryWriter =>
            {
                GameData gameData = this.m_gameData;
                binaryWriter.Write(gameData.version);
                binaryWriter.Write(gameData.score);
                binaryWriter.Write(gameData.nightIndex);
                binaryWriter.Write(gameData.levelIndex);
                binaryWriter.Write(gameData.hearts);
                binaryWriter.Write(gameData.currentGunIndex);
                binaryWriter.Write(gameData.manaPoints);
                binaryWriter.Write(gameData.hitPoints);
                binaryWriter.Write(gameData.slashDamageLevel);
                binaryWriter.Write(gameData.slashManaDrainLevel);
                binaryWriter.Write(gameData.hitPointsLevel);
                binaryWriter.Write(gameData.manaPointsLevel);
                binaryWriter.Write(gameData.airSlashDamageLevel);
                binaryWriter.Write(gameData.airSlashAvailable);
                for (int index = 0; index < 6; ++index)
                {
                    binaryWriter.Write(gameData.m_weaponLevels[index]);
                    binaryWriter.Write(gameData.m_weaponAvailable[index]);
                }
                binaryWriter.Write(gameData.m_achievementCounter_100ZombiesKilled);
                binaryWriter.Write(gameData.m_achievementCounter_500ZombiesKilled);
                binaryWriter.Write(gameData.m_achievementCounter_1000ZombiesKilled);
                binaryWriter.Write(gameData.m_achievementCounter_500ZombiesKilledAirSlash);
                binaryWriter.Write(gameData.m_achievementCounter_500ZombiesKilledBullet);
                binaryWriter.Write(gameData.m_achievementCounter_500ZombiesKilledFlameThrower);
                binaryWriter.Write(gameData.m_achievementCounter_500ZombiesKilledIceGun);
                binaryWriter.Write(gameData.m_achievementCounter_500ZombiesKilledExplosiveGun);
                binaryWriter.Write(gameData.m_achievementCounter_1000ZombiesKilledHolyBeam);
                binaryWriter.Write(gameData.m_achievementCounter_100Ungrab);
                binaryWriter.Write(gameData.m_achievement_OneLife);
                binaryWriter.Write(gameData.GameOverFlag);
                for (int index = 0; index < 14; ++index)
                {
                    binaryWriter.Write(gameData.m_storyLevelsInfestation[index]);
                    binaryWriter.Write(gameData.m_storyLevelsLocked[index]);
                }
                binaryWriter.Write(gameData.m_dayPart);
                binaryWriter.Write(gameData.m_missionIndex);
                binaryWriter.Write(gameData.m_missionPart);
                binaryWriter.Write(gameData.m_difficulty);
            });
        }

        public void EraseGameDataFile() => FileManager.Instance.DeleteFile(GetGameDataFilename());

        private static string GetGameDataFilename() => GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? GAMEDATA_SURVIVAL_FILENAME : GAMEDATA_STORY_FILENAME;

        /// <summary>
        /// Returns false when the file is missing or shorter than expected (truncated or corrupt);
        /// callers then fall back to defaults.
        /// </summary>
        private static bool ReadFile(string filename, Action<BinaryReader> read)
        {
            byte[] data = FileManager.Instance.ReadFile(filename);
            if (data == null)
                return false;
            try
            {
                using (BinaryReader binaryReader = new BinaryReader(new MemoryStream(data)))
                    read(binaryReader);
                return true;
            }
            catch (EndOfStreamException)
            {
                return false;
            }
        }

        private static bool WriteFile(string filename, Action<BinaryWriter> write)
        {
            MemoryStream stream = new MemoryStream(PREFS_FILE_SIZE);
            using (BinaryWriter binaryWriter = new BinaryWriter(stream))
                write(binaryWriter);
            return FileManager.Instance.WriteFile(filename, stream.ToArray());
        }

        public class UserSettings
        {
            public int version;
            public int languageId;
            public float musicGain;
            public float sfxGain;
            public float hudAlpha;
        }

        public class Achievements
        {
            public int version;
            public bool achievement_Survival_500ZombiesKilledBullet;
            public bool achievement_Survival_500ZombiesKilledFlameThrower;
            public bool achievement_Survival_500ZombiesKilledIceGun;
            public bool achievement_Survival_500ZombiesKilledExplosiveGun;
            public bool achievement_Survival_1000ZombiesKilledHolyBeam;
            public bool achievement_Survival_500ZombiesKilledAirSlash;
            public bool achievement_Survival_100Ungrab;
            public bool achievement_Survival_AllWeaponsUpgraded;
            public bool achievement_Survival_AllAbilitiesUpgraded;
            public bool achievement_Survival_GameFinished;
            public bool achievement_Survival_GameFinishedHard;
            public bool achievement_Story_100ZombiesKilled;
            public bool achievement_Story_500ZombiesKilled;
            public bool achievement_Story_1000ZombiesKilled;
            public bool achievement_Story_Boss1Killed;
            public bool achievement_Story_Boss2Killed;
            public bool achievement_Story_Boss3Killed;
            public bool achievement_Story_AllWeaponsAndAbilitiesUpgraded;
            public bool achievement_Story_GameFinished;
            public bool achievement_Story_GameFinishedHard;

            public int GetUnlockedCount()
            {
                bool[] unlocked = new bool[]
                {
          this.achievement_Survival_500ZombiesKilledBullet,
          this.achievement_Survival_500ZombiesKilledFlameThrower,
          this.achievement_Survival_500ZombiesKilledIceGun,
          this.achievement_Survival_500ZombiesKilledExplosiveGun,
          this.achievement_Survival_1000ZombiesKilledHolyBeam,
          this.achievement_Survival_500ZombiesKilledAirSlash,
          this.achievement_Survival_100Ungrab,
          this.achievement_Survival_AllWeaponsUpgraded,
          this.achievement_Survival_AllAbilitiesUpgraded,
          this.achievement_Survival_GameFinished,
          this.achievement_Survival_GameFinishedHard,
          this.achievement_Story_100ZombiesKilled,
          this.achievement_Story_500ZombiesKilled,
          this.achievement_Story_1000ZombiesKilled,
          this.achievement_Story_Boss1Killed,
          this.achievement_Story_Boss2Killed,
          this.achievement_Story_Boss3Killed,
          this.achievement_Story_AllWeaponsAndAbilitiesUpgraded,
          this.achievement_Story_GameFinished,
          this.achievement_Story_GameFinishedHard
                };
                int count = 0;
                for (int index = 0; index < unlocked.Length; ++index)
                {
                    if (unlocked[index])
                        ++count;
                }
                return count;
            }
        }

        public class Highscores
        {
            public int version;
            public int[] highscoresSurvival = new int[5];
            public int[] highscoresStory = new int[5];
        }

        public class GameData
        {
            public int version;
            public int score;
            public int nightIndex;
            public int levelIndex;
            public int hearts;
            public int currentGunIndex;
            public int manaPoints;
            public int hitPoints;
            public int slashDamageLevel;
            public int slashManaDrainLevel;
            public int hitPointsLevel;
            public int manaPointsLevel;
            public int airSlashDamageLevel;
            public bool airSlashAvailable;
            public int[] m_weaponLevels = new int[6];
            public bool[] m_weaponAvailable = new bool[6];
            public int m_achievementCounter_100ZombiesKilled;
            public int m_achievementCounter_500ZombiesKilled;
            public int m_achievementCounter_1000ZombiesKilled;
            public int m_achievementCounter_500ZombiesKilledAirSlash;
            public int m_achievementCounter_500ZombiesKilledBullet;
            public int m_achievementCounter_500ZombiesKilledFlameThrower;
            public int m_achievementCounter_500ZombiesKilledIceGun;
            public int m_achievementCounter_500ZombiesKilledExplosiveGun;
            public int m_achievementCounter_1000ZombiesKilledHolyBeam;
            public int m_achievementCounter_100Ungrab;
            public bool m_achievement_OneLife;
            public bool GameOverFlag;
            public int[] m_storyLevelsInfestation = new int[14];
            public bool[] m_storyLevelsLocked = new bool[14];
            public int m_dayPart;
            public int m_missionIndex;
            public int m_missionPart;
            public int m_difficulty;
        }
    }
}
