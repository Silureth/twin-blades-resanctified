// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.GuideManager
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

//using Microsoft.Phone.Tasks;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using Resanctified.Engine;
using System;
using System.Collections.Generic;
using Application = Resanctified.Engine.Application;

namespace Resanctified.Game
{
    public class GuideManager
    {
        private static readonly GuideManager s_instance = new GuideManager();
        public bool RequestShowMarketplace;
        public bool RequestShowGameUpdateRequired;
        public bool RequestShowNetworkDown;
        public bool RequestShowAchievementTrialMode;
        public bool RequestShowMessageUserMusicPlaying;
        private bool m_checkedUpdate;
        private bool m_LiveDisabled;
        private bool m_trialMode;
        private bool m_gameUpdateRequired;
        private bool m_signedIn;

        public static GuideManager Instance => GuideManager.s_instance;

        public bool IsLiveDisabled() => this.m_LiveDisabled;

        public event EventHandler TrialModeChanged;

        public event EventHandler GameUpdateRequiredChanged;

        public event EventHandler SignedInChanged;

        private GuideManager()
        {
            this.m_checkedUpdate = false;
            //this.m_LiveDisabled = false;
            //Guide.IsScreenSaverEnabled = false;
            //SignedInGamer.SignedIn += new EventHandler<SignedInEventArgs>(this.SignedInGamer_SignedIn);
            //SignedInGamer.SignedOut += new EventHandler<SignedOutEventArgs>(this.SignedInGamer_SignedOut);
        }

        //private void SignedInGamer_SignedOut(object sender, SignedOutEventArgs e)
        //{
        //    if (!this.m_signedIn)
        //        return;
        //    this.m_signedIn = false;
        //    if (this.SignedInChanged == null)
        //        return;
        //    this.SignedInChanged((object)this, (EventArgs)null);
        //}

        //private void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
        //{
        //    bool flag = e.Gamer != null && e.Gamer.IsSignedInToLive;
        //    if (this.m_signedIn != flag)
        //    {
        //        this.m_signedIn = flag;
        //        if (this.SignedInChanged != null)
        //            this.SignedInChanged((object)this, (EventArgs)null);
        //        this.UpdateIsTrial();
        //    }
        //    if (!this.m_signedIn || this.m_LiveDisabled || this.m_checkedUpdate)
        //        return;
        //    LeaderboardManager.Instance.ReadBestScores(0);
        //    this.m_checkedUpdate = true;
        //}

        public void Initialize()
        {
            this.m_LiveDisabled = false;
            this.RequestShowMarketplace = false;
            this.RequestShowGameUpdateRequired = false;
            this.RequestShowNetworkDown = false;
            this.RequestShowAchievementTrialMode = false;
            this.RequestShowMessageUserMusicPlaying = false;
            this.m_trialMode = false;//Guide.IsTrialMode;
            this.m_gameUpdateRequired = false;
            this.m_signedIn = false;
        }

        public void Cleanup()
        {
        }

        public void UpdateIsTrial()
        {
            bool isTrialMode = false;//Guide.IsTrialMode;
            if (isTrialMode == this.m_trialMode)
                return;
            this.m_trialMode = isTrialMode;
            this.OnTrialModeChanged();
        }

        public void Update()
        {
            if (this.RequestShowGameUpdateRequired && !this.m_gameUpdateRequired)
            {
                //  Application.Instance.GetGamerServiceInstance().Enabled = false;
                this.m_gameUpdateRequired = true;
                if (this.GameUpdateRequiredChanged != null)
                    this.GameUpdateRequiredChanged((object)this, (EventArgs)null);
            }
            //  if (Guide.IsVisible)
            //    return;
            if (this.RequestShowAchievementTrialMode)
            {
                this.RequestShowAchievementTrialMode = false;
                this.ShowMessageAchievementTrial();
            }
            else if (this.RequestShowMarketplace)
            {
                this.RequestShowMarketplace = false;
                this.ShowMarketPlace();
            }
            else
            {
                if (this.RequestShowNetworkDown)
                {
                    this.RequestShowNetworkDown = false;
                    this.ShowNetworkDown();
                }
                if (this.RequestShowGameUpdateRequired)
                {
                    this.RequestShowGameUpdateRequired = false;
                    this.ShowGameUpdateRequired();
                }
                if (!this.RequestShowMessageUserMusicPlaying)
                    return;
                this.RequestShowMessageUserMusicPlaying = false;
                this.ShowMessageUserMusicPlaying();
            }
        }

        public bool IsTrialMode() => this.m_trialMode;

        public bool IsGameUpdateRequired() => this.m_gameUpdateRequired;

        public bool IsSignedInToLive() => !this.m_LiveDisabled && this.m_signedIn;

        public bool IsPurchaseAvailable() => true;

        private void ShowGameUpdateRequired()
        {
          //  try
          //  {
          //      if (Guide.IsVisible)
          //          return;
          //      Guide.BeginShowMessageBox(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.GameUpdateRequired), GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.GameUpdateRequiredDesc), (IEnumerable<string>)new string[2]
          //      {
          //GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.No),
          //GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Yes)
          //      }, 0, MessageBoxIcon.Alert, new AsyncCallback(this.ShowGameUpdateRequiredCallback), (object)null);
          //  }
          //  catch (Exception ex)
          //  {
          //  }
        }

        private void ShowGameUpdateRequiredCallback(IAsyncResult result)
        {
            //try
            //{
            //    int? nullable1 = Guide.EndShowMessageBox(result);
            //    int? nullable2 = nullable1;
            //    if ((nullable2.GetValueOrDefault() != 0 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
            //    {
            //        this.m_LiveDisabled = true;
            //    }
            //    else
            //    {
            //        int? nullable3 = nullable1;
            //        if ((nullable3.GetValueOrDefault() != 1 ? 0 : (nullable3.HasValue ? 1 : 0)) == 0)
            //            return;
            //        if (Guide.IsTrialMode)
            //            this.RequestShowMarketplace = true;
            //        else return;// TODO
            //        /* new MarketplaceDetailTask()
            //         {
            //           ContentType = ((MarketplaceContentType) 1)
            //         }.Show();*/
            //    }
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void OnTrialModeChanged()
        {
            if (this.TrialModeChanged == null)
                return;
            this.TrialModeChanged((object)this, (EventArgs)null);
        }

        private void ShowMarketPlace()
        {
            //try
            //{
            //    SignedInGamer signedInGamer = Gamer.SignedInGamers[PlayerIndex.One];
            //    if (signedInGamer == null || Guide.IsVisible)
            //        return;
            //    Guide.ShowMarketplace(signedInGamer.PlayerIndex);
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void ShowNetworkDown()
        {
          //  try
          //  {
          //      if (Guide.IsVisible)
          //          return;
          //      Guide.BeginShowMessageBox(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.NetworkLost), GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.NetworkLostDesc), (IEnumerable<string>)new string[1]
          //      {
          //GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Ok)
          //      }, 0, MessageBoxIcon.Error, new AsyncCallback(this.ShowNetworkDownCallback), (object)null);
          //  }
          //  catch (Exception ex)
          //  {
          //  }
        }

        private void ShowNetworkDownCallback(IAsyncResult result)
        {
        //    try
        //    {
        //        Guide.EndShowMessageBox(result);
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        }

        private void ShowMessageAchievementTrial()
        {
          //  try
          //  {
          //      if (Guide.IsVisible)
          //          return;
          //      Guide.BeginShowMessageBox(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.UnlockFullGame), GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.EarnAchievementUnlockGame), (IEnumerable<string>)new string[2]
          //      {
          //GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.BuyGame),
          //GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.NotNow)
          //      }, 0, MessageBoxIcon.Warning, new AsyncCallback(this.ShowMessageAchievementTrialCallback), (object)null);
          //  }
          //  catch (Exception ex)
          //  {
          //  }
        }

        private void ShowMessageAchievementTrialCallback(IAsyncResult result)
        {
            //try
            //{
            //    int? nullable = Guide.EndShowMessageBox(result);
            //    if ((nullable.GetValueOrDefault() != 0 ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
            //        return;
            //    this.RequestShowMarketplace = true;
            //}
            //catch (Exception ex)
            //{
            //}
        }

        private void ShowMessageUserMusicPlaying()
        {
          //  try
          //  {
          //      if (Guide.IsVisible)
          //          return;
          //      Guide.BeginShowMessageBox(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.UserMusicPlaying), GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.UserMusicPlayingDesc), (IEnumerable<string>)new string[2]
          //      {
          //GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.No),
          //GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Yes)
          //      }, 0, MessageBoxIcon.Warning, new AsyncCallback(this.ShowMessageUserMusicPlayingCallback), (object)null);
          //  }
          //  catch (Exception ex)
          //  {
          //  }
        }

        private void ShowMessageUserMusicPlayingCallback(IAsyncResult result)
        {
            //try
            //{
            //    int? nullable1 = Guide.EndShowMessageBox(result);
            //    int? nullable2 = nullable1;
            //    if ((nullable2.GetValueOrDefault() != 0 ? 0 : (nullable2.HasValue ? 1 : 0)) != 0)
            //        return;
            //    int? nullable3 = nullable1;
            //    if ((nullable3.GetValueOrDefault() != 1 ? 0 : (nullable3.HasValue ? 1 : 0)) == 0)
            //        return;
            //    MediaPlayer.Stop();
            //}
            //catch (Exception ex)
            //{
            //}
        }
    }
}
