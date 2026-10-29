// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.Prefs
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace PSS.Game.NunVsZombies
{
  public sealed class Prefs
  {
    public const int GAMEDATA_VERSION = 1;
    public const int PREFS_FILE_SIZE = 512;
    private static readonly Prefs s_instance = new Prefs();
    private static readonly string TITLE_NAME = "TwinBlades";
    private static readonly string GAMEDATA_SURVIVAL_FILENAME = "TwinBladesGameData.sav";
    private static readonly string GAMEDATA_STORY_FILENAME = "TwinBladesStoryGameData.sav";
    private static readonly string USERSETTINGS_FILENAME = "TwinBladesSettings.sav";
    private static readonly string HIGHSCORES_FILENAME = "TwinBladesHighscores.sav";
    private static readonly string ACHIEVEMENTS_FILENAME = "TwinBladesRewards.sav";
    private Prefs.UserSettings m_userSettings = new Prefs.UserSettings();
    private Prefs.GameData m_gameData = new Prefs.GameData();
    private Prefs.Highscores m_highscores = new Prefs.Highscores();
    private Prefs.Achievements m_achievements = new Prefs.Achievements();
    private IsolatedStorageFile m_storageDevice;
    private IAsyncResult m_result;

    public static Prefs Instance => Prefs.s_instance;

    public static Prefs GetInstance() => Prefs.s_instance;

    public Prefs.UserSettings CachedUserSettings => this.m_userSettings;

    public Prefs.GameData CachedGameData => this.m_gameData;

    public Prefs.Highscores CachedHighscores => this.m_highscores;

    public Prefs.Achievements CachedAchievements => this.m_achievements;

    private Prefs() => this.m_storageDevice = (IsolatedStorageFile) null;

    public void Initialize()
    {
      this.m_userSettings.version = 0;
      this.m_gameData.version = 0;
      this.m_highscores.version = 0;
      this.m_achievements.version = 0;
      this.m_storageDevice = IsolatedStorageFile.GetUserStoreForApplication();
    }

    public void Cleanup() => this.m_storageDevice = (IsolatedStorageFile) null;

    public void Update()
    {
    }

    public bool ReadUserSettings()
    {
      if (this.m_storageDevice == null)
        return this.m_userSettings.version > 0;
      try
      {
        if (this.m_storageDevice != null)
        {
          string usersettingsFilename = Prefs.USERSETTINGS_FILENAME;
          if (!this.m_storageDevice.FileExists(usersettingsFilename))
            return false;
          BinaryReader binaryReader = new BinaryReader((Stream) this.m_storageDevice.OpenFile(usersettingsFilename, FileMode.Open, FileAccess.Read));
          Prefs.UserSettings userSettings = this.m_userSettings;
          userSettings.version = binaryReader.ReadInt32();
          userSettings.languageId = binaryReader.ReadInt32();
          userSettings.musicGain = binaryReader.ReadSingle();
          userSettings.sfxGain = binaryReader.ReadSingle();
          userSettings.hudAlpha = binaryReader.ReadSingle();
          binaryReader.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public bool ReadAchievements()
    {
      if (this.m_storageDevice == null)
        return this.m_achievements.version > 0;
      try
      {
        if (this.m_storageDevice != null)
        {
          string achievementsFilename = Prefs.ACHIEVEMENTS_FILENAME;
          if (!this.m_storageDevice.FileExists(achievementsFilename))
            return false;
          BinaryReader binaryReader = new BinaryReader((Stream) this.m_storageDevice.OpenFile(achievementsFilename, FileMode.Open, FileAccess.Read));
          Prefs.Achievements achievements = this.m_achievements;
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
          binaryReader.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public bool ReadGameData()
    {
      if (this.m_storageDevice == null)
        return this.m_gameData.version > 0;
      try
      {
        if (this.m_storageDevice != null)
        {
          string path = GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? Prefs.GAMEDATA_SURVIVAL_FILENAME : Prefs.GAMEDATA_STORY_FILENAME;
          if (!this.m_storageDevice.FileExists(path))
            return false;
          BinaryReader binaryReader = new BinaryReader((Stream) this.m_storageDevice.OpenFile(path, FileMode.Open, FileAccess.Read));
          Prefs.GameData gameData = this.m_gameData;
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
          binaryReader.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public bool ReadHighscores()
    {
      if (this.m_storageDevice == null)
        return this.m_highscores.version > 0;
      try
      {
        if (this.m_storageDevice != null)
        {
          string highscoresFilename = Prefs.HIGHSCORES_FILENAME;
          if (!this.m_storageDevice.FileExists(highscoresFilename))
            return false;
          BinaryReader binaryReader = new BinaryReader((Stream) this.m_storageDevice.OpenFile(highscoresFilename, FileMode.Open, FileAccess.Read));
          Prefs.Highscores highscores = this.m_highscores;
          highscores.version = binaryReader.ReadInt32();
          for (int index = 0; index < 5; ++index)
            highscores.highscoresSurvival[index] = binaryReader.ReadInt32();
          for (int index = 0; index < 5; ++index)
            highscores.highscoresStory[index] = binaryReader.ReadInt32();
          binaryReader.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public bool WriteUserSettings()
    {
      this.m_userSettings.version = 1;
      try
      {
        if (this.m_storageDevice != null)
        {
          BinaryWriter binaryWriter = new BinaryWriter((Stream) this.m_storageDevice.OpenFile(Prefs.USERSETTINGS_FILENAME, FileMode.Create, FileAccess.Write));
          Prefs.UserSettings userSettings = this.m_userSettings;
          binaryWriter.Write(userSettings.version);
          binaryWriter.Write(userSettings.languageId);
          binaryWriter.Write(userSettings.musicGain);
          binaryWriter.Write(userSettings.sfxGain);
          binaryWriter.Write(userSettings.hudAlpha);
          binaryWriter.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public bool WriteAchievements()
    {
      this.m_achievements.version = 1;
      try
      {
        if (this.m_storageDevice != null)
        {
          BinaryWriter binaryWriter = new BinaryWriter((Stream) this.m_storageDevice.OpenFile(Prefs.ACHIEVEMENTS_FILENAME, FileMode.Create, FileAccess.Write));
          Prefs.Achievements achievements = this.m_achievements;
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
          binaryWriter.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public bool WriteHighscores()
    {
      this.m_highscores.version = 1;
      try
      {
        if (this.m_storageDevice != null)
        {
          BinaryWriter binaryWriter = new BinaryWriter((Stream) this.m_storageDevice.OpenFile(Prefs.HIGHSCORES_FILENAME, FileMode.Create, FileAccess.Write));
          Prefs.Highscores highscores = this.m_highscores;
          binaryWriter.Write(highscores.version);
          for (int index = 0; index < 5; ++index)
            binaryWriter.Write(highscores.highscoresSurvival[index]);
          for (int index = 0; index < 5; ++index)
            binaryWriter.Write(highscores.highscoresStory[index]);
          binaryWriter.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public bool WriteGameData()
    {
      this.m_gameData.version = 1;
      try
      {
        if (this.m_storageDevice != null)
        {
          BinaryWriter binaryWriter = new BinaryWriter((Stream) this.m_storageDevice.OpenFile(GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? Prefs.GAMEDATA_SURVIVAL_FILENAME : Prefs.GAMEDATA_STORY_FILENAME, FileMode.Create, FileAccess.Write));
          Prefs.GameData gameData = this.m_gameData;
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
          binaryWriter.Close();
          return true;
        }
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public void EraseGameDataFile()
    {
      try
      {
        if (this.m_storageDevice == null)
          return;
        string str = GameApplication.GetInstance().GetGameMode() != GameApplication.GameMode.Story ? Prefs.GAMEDATA_SURVIVAL_FILENAME : Prefs.GAMEDATA_STORY_FILENAME;
        if (!this.m_storageDevice.FileExists(str))
          return;
        this.m_storageDevice.DeleteFile(str);
      }
      catch (Exception ex)
      {
      }
    }

    public void EraseAchievementsFile()
    {
      try
      {
        if (this.m_storageDevice == null || !this.m_storageDevice.FileExists(Prefs.ACHIEVEMENTS_FILENAME))
          return;
        this.m_storageDevice.DeleteFile(Prefs.ACHIEVEMENTS_FILENAME);
      }
      catch (Exception ex)
      {
      }
    }

    public void EraseAllContainerFiles()
    {
      try
      {
        if (this.m_storageDevice == null)
          return;
        foreach (string fileName in this.m_storageDevice.GetFileNames())
          this.m_storageDevice.DeleteFile(fileName);
      }
      catch (Exception ex)
      {
      }
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
