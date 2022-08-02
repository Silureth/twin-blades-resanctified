//// Decompiled with JetBrains decompiler
//// Type: PSS.Game.NunVsZombies.LeaderboardManager
//// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
//// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
//using PSS.GameEngine.IO;
//using System;
//using System.Collections.Generic;

//namespace PSS.Game.NunVsZombies
//{
//  public class LeaderboardManager
//  {
//    public const int LEADERBOARD_PAGE_SIZE = 5;
//    private static readonly LeaderboardManager s_instance = new LeaderboardManager();
//    private LeaderboardReader m_leaderboardReader;
//    private Stack<LeaderboardManager.PendingRequest> m_pendingRequests = new Stack<LeaderboardManager.PendingRequest>(8);

//    public static LeaderboardManager Instance => LeaderboardManager.s_instance;

//    public LeaderboardReader GetLeaderboardReader() => this.m_leaderboardReader;

//    public event EventHandler BestScoresReadEvent;

//    public event EventHandler LeaderboardTimeOutEvent;

//    private LeaderboardManager()
//    {
//    }

//    public void Initialize() => this.m_pendingRequests.Clear();

//    public void Cleanup() => this.m_pendingRequests.Clear();

//    public void Read(PSSBinaryReader reader)
//    {
//      this.m_pendingRequests.Clear();
//      int num = reader.ReadInt32();
//      for (int index = 0; index < num; ++index)
//        this.m_pendingRequests.Push(new LeaderboardManager.PendingRequest(reader.ReadInt32(), reader.ReadInt32()));
//    }

//    public void Write(PSSBinaryWriter writer)
//    {
//      int count = this.m_pendingRequests.Count;
//      writer.Write(count);
//      if (count == 0)
//        return;
//      LeaderboardManager.PendingRequest[] array = this.m_pendingRequests.ToArray();
//      for (int index = count - 1; index >= 0; --index)
//      {
//        writer.Write(array[index].table);
//        writer.Write(array[index].score);
//      }
//    }

//    public void Update()
//    {
//      if (this.m_pendingRequests.Count <= 0 || GuideManager.Instance.IsTrialMode() || !GuideManager.Instance.IsSignedInToLive() || GuideManager.Instance.IsGameUpdateRequired())
//        return;
//      LeaderboardManager.PendingRequest pendingRequest = this.m_pendingRequests.Pop();
//      this.WriteBestScore(pendingRequest.score, pendingRequest.table);
//    }

//    public bool CanPageUp() => this.m_leaderboardReader != null && this.m_leaderboardReader.CanPageUp;

//    public int GetTotalLeaderboardSize() => this.m_leaderboardReader != null ? this.m_leaderboardReader.TotalLeaderboardSize : 0;

//    public int GetPageStart() => this.m_leaderboardReader != null ? this.m_leaderboardReader.PageStart : 0;

//    public bool CanPageDown() => this.m_leaderboardReader != null && this.m_leaderboardReader.CanPageDown;

//    public int GetCurrentTable() => this.m_leaderboardReader != null ? this.m_leaderboardReader.LeaderboardIdentity.GameMode : -1;

//    public LeaderboardKey GetCurrentKey()
//    {
//      LeaderboardKey currentKey = LeaderboardKey.BestScoreLifeTime;
//      if (this.m_leaderboardReader != null)
//      {
//        string key = this.m_leaderboardReader.LeaderboardIdentity.Key;
//        if (key.Equals(LeaderboardKey.BestScoreLifeTime.ToString()))
//          currentKey = LeaderboardKey.BestScoreLifeTime;
//        else if (key.Equals(LeaderboardKey.BestScoreRecent.ToString()))
//          currentKey = LeaderboardKey.BestScoreRecent;
//        else if (key.Equals(LeaderboardKey.BestTimeLifeTime.ToString()))
//          currentKey = LeaderboardKey.BestTimeLifeTime;
//        else if (key.Equals(LeaderboardKey.BestTimeRecent.ToString()))
//          currentKey = LeaderboardKey.BestTimeRecent;
//      }
//      return currentKey;
//    }

//    public void ReadBestScores(int tableIndex) => this.ReadBestScores(tableIndex, LeaderboardKey.BestScoreLifeTime);

//    public void ReadBestScores(int tableIndex, bool weekly)
//    {
//      if (weekly)
//        this.ReadBestScores(tableIndex, LeaderboardKey.BestScoreRecent);
//      else
//        this.ReadBestScores(tableIndex, LeaderboardKey.BestScoreLifeTime);
//    }

//    public void ReadBestScores(int tableIndex, LeaderboardKey key)
//    {
//      if (GuideManager.Instance.IsLiveDisabled())
//        return;
//      try
//      {
//        SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
//        LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(key, tableIndex);
//        if (signedInGamer != null && !GuideManager.Instance.IsTrialMode())
//          LeaderboardReader.BeginRead(leaderboardId, (Gamer) signedInGamer, 5, new AsyncCallback(this.ReadBestScoresCallback), (object) null);
//        else
//          LeaderboardReader.BeginRead(leaderboardId, 0, 5, new AsyncCallback(this.ReadBestScoresCallback), (object) null);
//      }
//      catch (GameUpdateRequiredException ex)
//      {
//        GuideManager.Instance.RequestShowGameUpdateRequired = true;
//        if (this.LeaderboardTimeOutEvent == null)
//          return;
//        this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//      catch (Exception ex)
//      {
//        if (this.LeaderboardTimeOutEvent == null)
//          return;
//        this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//    }

//    private void ReadBestScoresCallback(IAsyncResult result)
//    {
//      try
//      {
//        this.m_leaderboardReader = LeaderboardReader.EndRead(result);
//      }
//      catch (Exception ex)
//      {
//        this.m_leaderboardReader = (LeaderboardReader) null;
//        GuideManager.Instance.RequestShowNetworkDown = true;
//        if (this.LeaderboardTimeOutEvent != null)
//          this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//      if (this.BestScoresReadEvent == null)
//        return;
//      this.BestScoresReadEvent((object) this, (EventArgs) null);
//    }

//    public void ReadBestScoresPageUp()
//    {
//      if (GuideManager.Instance.IsLiveDisabled())
//        return;
//      if (this.m_leaderboardReader == null)
//        return;
//      try
//      {
//        this.m_leaderboardReader.BeginPageUp(new AsyncCallback(this.ReadBestScoresPageUpCallback), (object) null);
//      }
//      catch (GameUpdateRequiredException ex)
//      {
//        GuideManager.Instance.RequestShowGameUpdateRequired = true;
//        if (this.LeaderboardTimeOutEvent == null)
//          return;
//        this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//      catch (Exception ex)
//      {
//        if (this.LeaderboardTimeOutEvent == null)
//          return;
//        this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//    }

//    private void ReadBestScoresPageUpCallback(IAsyncResult result)
//    {
//      try
//      {
//        this.m_leaderboardReader.EndPageUp(result);
//      }
//      catch (Exception ex)
//      {
//        this.m_leaderboardReader = (LeaderboardReader) null;
//        GuideManager.Instance.RequestShowNetworkDown = true;
//        if (this.LeaderboardTimeOutEvent != null)
//          this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//      if (this.BestScoresReadEvent == null)
//        return;
//      this.BestScoresReadEvent((object) this, (EventArgs) null);
//    }

//    public void ReadBestScoresPageDown()
//    {
//      if (GuideManager.Instance.IsLiveDisabled())
//        return;
//      if (this.m_leaderboardReader == null)
//        return;
//      try
//      {
//        this.m_leaderboardReader.BeginPageDown(new AsyncCallback(this.ReadBestScoresPageDownCallback), (object) null);
//      }
//      catch (GameUpdateRequiredException ex)
//      {
//        GuideManager.Instance.RequestShowGameUpdateRequired = true;
//        if (this.LeaderboardTimeOutEvent == null)
//          return;
//        this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//      catch (Exception ex)
//      {
//        if (this.LeaderboardTimeOutEvent == null)
//          return;
//        this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//    }

//    private void ReadBestScoresPageDownCallback(IAsyncResult result)
//    {
//      try
//      {
//        this.m_leaderboardReader.EndPageDown(result);
//      }
//      catch (Exception ex)
//      {
//        this.m_leaderboardReader = (LeaderboardReader) null;
//        GuideManager.Instance.RequestShowNetworkDown = true;
//        if (this.LeaderboardTimeOutEvent != null)
//          this.LeaderboardTimeOutEvent((object) this, (EventArgs) null);
//      }
//      if (this.BestScoresReadEvent == null)
//        return;
//      this.BestScoresReadEvent((object) this, (EventArgs) null);
//    }

//    public void WriteBestScore(int score, int tableIndex)
//    {
//      if (GuideManager.Instance.IsLiveDisabled())
//        return;
//      if (GuideManager.Instance.IsTrialMode())
//        this.m_pendingRequests.Push(new LeaderboardManager.PendingRequest(tableIndex, score));
//      else if (!GuideManager.Instance.IsSignedInToLive())
//        this.m_pendingRequests.Push(new LeaderboardManager.PendingRequest(tableIndex, score));
//      else if (GuideManager.Instance.IsGameUpdateRequired())
//      {
//        this.m_pendingRequests.Push(new LeaderboardManager.PendingRequest(tableIndex, score));
//      }
//      else
//      {
//        try
//        {
//          SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
//          if (signedInGamer == null)
//            return;
//          LeaderboardIdentity leaderboardId = LeaderboardIdentity.Create(LeaderboardKey.BestScoreLifeTime, tableIndex);
//          LeaderboardEntry leaderboard = signedInGamer.LeaderboardWriter.GetLeaderboard(leaderboardId);
//          leaderboard.Rating = (long) score;
//          leaderboard.Columns.SetValue("Score", score);
//          leaderboard.Columns.SetValue("Outcome", LeaderboardOutcome.None);
//          leaderboard.Columns.SetValue("TimeStamp", DateTime.Now);
//        }
//        catch (GameUpdateRequiredException ex)
//        {
//          GuideManager.Instance.RequestShowGameUpdateRequired = true;
//          this.m_pendingRequests.Push(new LeaderboardManager.PendingRequest(tableIndex, score));
//        }
//        catch (Exception ex)
//        {
//          this.m_pendingRequests.Push(new LeaderboardManager.PendingRequest(tableIndex, score));
//        }
//      }
//    }

//    public LeaderboardItem[] GetBestScores()
//    {
//      if (GuideManager.Instance.IsLiveDisabled())
//        return (LeaderboardItem[]) null;
//      if (this.m_leaderboardReader == null)
//        return (LeaderboardItem[]) null;
//      SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
//      if (signedInGamer == null)
//        return (LeaderboardItem[]) null;
//      LeaderboardItem[] bestScores = (LeaderboardItem[]) null;
//      try
//      {
//        LeaderboardReader leaderboardReader = this.m_leaderboardReader;
//        bestScores = new LeaderboardItem[leaderboardReader.Entries.Count];
//        for (int index = 0; index < leaderboardReader.Entries.Count; ++index)
//        {
//          LeaderboardEntry entry = leaderboardReader.Entries[index];
//          bestScores[index].gamertag = entry.Gamer.Gamertag;
//          bestScores[index].score = (int) entry.Rating;
//          bestScores[index].isSignedInGamer = entry.Gamer.Gamertag.Equals(signedInGamer.Gamertag);
//        }
//      }
//      catch (GameUpdateRequiredException ex)
//      {
//        GuideManager.Instance.RequestShowGameUpdateRequired = true;
//      }
//      catch (Exception ex)
//      {
//      }
//      return bestScores;
//    }

//    private struct PendingRequest
//    {
//      public int score;
//      public int table;

//      public PendingRequest(int aTableIndex, int aScore)
//      {
//        this.score = aScore;
//        this.table = aTableIndex;
//      }
//    }
//  }
//}
