//// Decompiled with JetBrains decompiler
//// Type: PSS.Game.NunVsZombies.AchievementManager
//// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
//// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
//using System;

//namespace PSS.Game.NunVsZombies
//{
//  public class AchievementManager
//  {
//    private static readonly AchievementManager s_instance = new AchievementManager();
//    public bool SynchronizeAchievementsRequest;
//    private object m_achievementsLockObject = new object();
//    private AchievementCollection m_achievements;
//    private int m_earnedGamerScore;
//    private int m_maxGamerScore;
//    private int m_nbUnlockedAchievements;
//    private bool m_achievementsSynchronized;

//    public static AchievementManager Instance => AchievementManager.s_instance;

//    public int GetEarnGamerScore() => this.m_earnedGamerScore;

//    public int GetNbUnlockedAchievements() => this.m_nbUnlockedAchievements;

//    private AchievementManager()
//    {
//      this.SynchronizeAchievementsRequest = false;
//      this.m_earnedGamerScore = 0;
//      this.m_maxGamerScore = 0;
//      this.m_nbUnlockedAchievements = 0;
//      this.m_achievementsSynchronized = false;
//      this.m_achievements = (AchievementCollection) null;
//    }

//    public void Initialize()
//    {
//      this.SynchronizeAchievementsRequest = false;
//      this.m_earnedGamerScore = 0;
//      this.m_maxGamerScore = 0;
//      this.m_nbUnlockedAchievements = 0;
//      this.m_achievementsSynchronized = false;
//      this.m_achievements = (AchievementCollection) null;
//    }

//    public void Cleanup()
//    {
//    }

//    public void Update()
//    {
//      if (!this.SynchronizeAchievementsRequest)
//        return;
//      this.m_achievementsSynchronized = false;
//      this.SynchronizeAchievements();
//      if (!this.m_achievementsSynchronized)
//        return;
//      this.SynchronizeAchievementsRequest = false;
//    }

//    public void ReadAchievements()
//    {
//      SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
//      signedInGamer?.BeginGetAchievements(new AsyncCallback(this.GetAchievementsCallback), (object) signedInGamer);
//    }

//    protected void GetAchievementsCallback(IAsyncResult result)
//    {
//      if (!(result.AsyncState is SignedInGamer asyncState))
//        return;
//      lock (this.m_achievementsLockObject)
//      {
//        this.m_maxGamerScore = 0;
//        this.m_earnedGamerScore = 0;
//        this.m_nbUnlockedAchievements = 0;
//        this.m_achievements = asyncState.EndGetAchievements(result);
//        for (int index = 0; index < this.m_achievements.Count; ++index)
//        {
//          Achievement achievement = this.m_achievements[index];
//          this.m_maxGamerScore += achievement.GamerScore;
//          if (achievement.IsEarned)
//          {
//            this.m_earnedGamerScore += achievement.GamerScore;
//            ++this.m_nbUnlockedAchievements;
//          }
//        }
//      }
//    }

//    protected void AwardAchievementCallback(IAsyncResult result)
//    {
//      if (!(result.AsyncState is SignedInGamer asyncState))
//        return;
//      asyncState.EndAwardAchievement(result);
//      asyncState.BeginGetAchievements(new AsyncCallback(this.GetAchievementsCallback), (object) asyncState);
//    }

//    public void AwardAchievement(string achievementKey)
//    {
//      SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
//      if (signedInGamer == null)
//        return;
//      if (GuideManager.Instance.IsTrialMode())
//      {
//        GuideManager.Instance.RequestShowAchievementTrialMode = true;
//      }
//      else
//      {
//        lock (this.m_achievementsLockObject)
//        {
//          if (this.m_achievements == null)
//            return;
//          foreach (Achievement achievement in this.m_achievements)
//          {
//            if (achievement.Key == achievementKey)
//            {
//              if (achievement.IsEarned)
//                break;
//              signedInGamer.BeginAwardAchievement(achievementKey, new AsyncCallback(this.AwardAchievementCallback), (object) signedInGamer);
//              break;
//            }
//          }
//        }
//      }
//    }

//    public void SynchronizeAchievements() => this.SynchronizeAchievements(true);

//    public void SynchronizeAchievements(bool save)
//    {
//      if (this.m_achievementsSynchronized && save || this.m_achievements == null)
//        return;
//      this.m_achievementsSynchronized = true;
//      if (Guide.IsTrialMode)
//        return;
//      Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
//      for (int index = 0; index < this.m_achievements.Count; ++index)
//      {
//        Achievement achievement = this.m_achievements[index];
//        if (achievement.IsEarned)
//        {
//          if (achievement.Key == "100KillStory")
//            cachedAchievements.achievement_Story_100ZombiesKilled = true;
//          else if (achievement.Key == "500KillStory")
//            cachedAchievements.achievement_Story_500ZombiesKilled = true;
//          else if (achievement.Key == "1000KillStory")
//            cachedAchievements.achievement_Story_1000ZombiesKilled = true;
//          else if (achievement.Key == "Boss1")
//            cachedAchievements.achievement_Story_Boss1Killed = true;
//          else if (achievement.Key == "Boss2")
//            cachedAchievements.achievement_Story_Boss2Killed = true;
//          else if (achievement.Key == "Boss3")
//            cachedAchievements.achievement_Story_Boss3Killed = true;
//          else if (achievement.Key == "AllUpgradeStory")
//            cachedAchievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded = true;
//          else if (achievement.Key == "FinishStory")
//            cachedAchievements.achievement_Story_GameFinished = true;
//          else if (achievement.Key == "FinishStoryHard")
//            cachedAchievements.achievement_Story_GameFinishedHard = true;
//          else if (achievement.Key == "500KillGunSurvival")
//            cachedAchievements.achievement_Survival_500ZombiesKilledBullet = true;
//          else if (achievement.Key == "500KillFlameSurvival")
//            cachedAchievements.achievement_Survival_500ZombiesKilledFlameThrower = true;
//          else if (achievement.Key == "500KillIceSurvival")
//            cachedAchievements.achievement_Survival_500ZombiesKilledIceGun = true;
//          else if (achievement.Key == "500KillExplodeSurvival")
//            cachedAchievements.achievement_Survival_500ZombiesKilledExplosiveGun = true;
//          else if (achievement.Key == "1000KillHolySurvival")
//            cachedAchievements.achievement_Survival_1000ZombiesKilledHolyBeam = true;
//          else if (achievement.Key == "500KillAirSlashSurvival")
//            cachedAchievements.achievement_Survival_500ZombiesKilledAirSlash = true;
//          else if (achievement.Key == "100ShakeSurvival")
//            cachedAchievements.achievement_Survival_100Ungrab = true;
//          else if (achievement.Key == "AllWeaponsSurvival")
//            cachedAchievements.achievement_Survival_AllWeaponsUpgraded = true;
//          else if (achievement.Key == "AllAbilitiesSurvival")
//            cachedAchievements.achievement_Survival_AllAbilitiesUpgraded = true;
//          else if (achievement.Key == "FinishSurvival")
//            cachedAchievements.achievement_Survival_GameFinished = true;
//          else if (achievement.Key == "FinishSurvivalHard")
//            cachedAchievements.achievement_Survival_GameFinishedHard = true;
//        }
//      }
//      if (save)
//        GameApplication.Instance.SaveAchievements();
//      if (cachedAchievements.achievement_Story_100ZombiesKilled)
//        this.AwardAchievement("100KillStory");
//      if (cachedAchievements.achievement_Story_500ZombiesKilled)
//        this.AwardAchievement("500KillStory");
//      if (cachedAchievements.achievement_Story_1000ZombiesKilled)
//        this.AwardAchievement("1000KillStory");
//      if (cachedAchievements.achievement_Story_Boss1Killed)
//        this.AwardAchievement("Boss1");
//      if (cachedAchievements.achievement_Story_Boss2Killed)
//        this.AwardAchievement("Boss2");
//      if (cachedAchievements.achievement_Story_Boss3Killed)
//        this.AwardAchievement("Boss3");
//      if (cachedAchievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded)
//        this.AwardAchievement("AllUpgradeStory");
//      if (cachedAchievements.achievement_Story_GameFinished)
//        this.AwardAchievement("FinishStory");
//      if (cachedAchievements.achievement_Story_GameFinishedHard)
//        this.AwardAchievement("FinishStoryHard");
//      if (cachedAchievements.achievement_Survival_500ZombiesKilledBullet)
//        this.AwardAchievement("500KillGunSurvival");
//      if (cachedAchievements.achievement_Survival_500ZombiesKilledFlameThrower)
//        this.AwardAchievement("500KillFlameSurvival");
//      if (cachedAchievements.achievement_Survival_500ZombiesKilledIceGun)
//        this.AwardAchievement("500KillIceSurvival");
//      if (cachedAchievements.achievement_Survival_500ZombiesKilledExplosiveGun)
//        this.AwardAchievement("500KillExplodeSurvival");
//      if (cachedAchievements.achievement_Survival_1000ZombiesKilledHolyBeam)
//        this.AwardAchievement("1000KillHolySurvival");
//      if (cachedAchievements.achievement_Survival_500ZombiesKilledAirSlash)
//        this.AwardAchievement("500KillAirSlashSurvival");
//      if (cachedAchievements.achievement_Survival_100Ungrab)
//        this.AwardAchievement("100ShakeSurvival");
//      if (cachedAchievements.achievement_Survival_AllWeaponsUpgraded)
//        this.AwardAchievement("AllWeaponsSurvival");
//      if (cachedAchievements.achievement_Survival_AllAbilitiesUpgraded)
//        this.AwardAchievement("AllAbilitiesSurvival");
//      if (cachedAchievements.achievement_Survival_GameFinished)
//        this.AwardAchievement("FinishSurvival");
//      if (!cachedAchievements.achievement_Survival_GameFinishedHard)
//        return;
//      this.AwardAchievement("FinishSurvivalHard");
//    }
//  }
//}
