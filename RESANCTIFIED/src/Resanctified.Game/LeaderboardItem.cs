// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.LeaderboardItem
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

namespace Resanctified.Game
{
  public struct LeaderboardItem
  {
    public string gamertag;
    public int score;
    public bool isSignedInGamer;

    public LeaderboardItem(string gamertag, int score)
    {
      this.gamertag = gamertag;
      this.score = score;
      this.isSignedInGamer = false;
    }
  }
}
