// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.AchievementsMenuViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM;
using PSS.Game.NunVsZombies.FSM.GameState;
using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.Game.NunVsZombies.FSM.GameState.MainMenu;
using PSS.GameEngine;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Tools;
using PSS.GameEngine.UI;
using System.Text;

namespace PSS.Game.NunVsZombies.UI
{
  public class AchievementsMenuViewController : ViewController
  {
    public const float AWARDS_TEXTURE_WIDTH = 256f;
    public const float AWARDS_TEXTURE_HEIGHT = 256f;
    private const float ACHIEVEMENTS_SELECTOR_SCALE = 1.1f;
    private const int ACHIEVEMENTS_BUTTON_NB = 10;
    private const int ACHIEVEMENTS_PAGES_NB = 2;
    private const float ACHIEVEMENTS_BUTTON_PREVIOUS_X = 46f;
    private const float ACHIEVEMENTS_BUTTON_PREVIOUS_Y = 120f;
    private const float ACHIEVEMENTS_BUTTON_NEXT_X = 426f;
    private const float ACHIEVEMENTS_BUTTON_NEXT_Y = 120f;
    private const float ACHIEVEMENTS_BUTTON_WIDTH = 58f;
    private const float ACHIEVEMENTS_BUTTON_HEIGHT = 58f;
    private const float ACHIEVEMENTS_ICON_START_X = 120f;
    private const float ACHIEVEMENTS_ICON_START_Y = 120f;
    private const float ACHIEVEMENTS_ICON_OFFSET_X = 60f;
    private const float ACHIEVEMENTS_ICON_OFFSET_Y = 60f;
    private const float ACHIEVEMENTS_ICON_1_X = 120f;
    private const float ACHIEVEMENTS_ICON_1_Y = 120f;
    private const float ACHIEVEMENTS_ICON_2_X = 180f;
    private const float ACHIEVEMENTS_ICON_2_Y = 120f;
    private const float ACHIEVEMENTS_ICON_3_X = 240f;
    private const float ACHIEVEMENTS_ICON_3_Y = 120f;
    private const float ACHIEVEMENTS_ICON_4_X = 300f;
    private const float ACHIEVEMENTS_ICON_4_Y = 120f;
    private const float ACHIEVEMENTS_ICON_5_X = 360f;
    private const float ACHIEVEMENTS_ICON_5_Y = 120f;
    private const float ACHIEVEMENTS_ICON_6_X = 120f;
    private const float ACHIEVEMENTS_ICON_6_Y = 60f;
    private const float ACHIEVEMENTS_ICON_7_X = 180f;
    private const float ACHIEVEMENTS_ICON_7_Y = 60f;
    private const float ACHIEVEMENTS_ICON_8_X = 240f;
    private const float ACHIEVEMENTS_ICON_8_Y = 60f;
    private const float ACHIEVEMENTS_ICON_9_X = 300f;
    private const float ACHIEVEMENTS_ICON_9_Y = 60f;
    private const float ACHIEVEMENTS_ICON_10_X = 360f;
    private const float ACHIEVEMENTS_ICON_10_Y = 60f;
    private const float ACHIEVEMENTS_TEXT_TITLE_X = 240f;
    private const float ACHIEVEMENTS_TEXT_TITLE_Y = 208f;
    private const float ACHIEVEMENTS_TEXT_DESC_X = 240f;
    private const float ACHIEVEMENTS_TEXT_DESC_Y = 182f;
    private const float ACHIEVEMENTS_TITLE_X = 240f;
    private const float ACHIEVEMENTS_TITLE_Y = 260f;
    private const float ACHIEVEMENTS_GAMERSCORE_X = 430f;
    private const float ACHIEVEMENTS_GAMERSCORE_Y = 230f;
    private const float ACHIEVEMENTS_NBUNLOCKED_X = 30f;
    private const float ACHIEVEMENTS_NBUNLOCKED_Y = 230f;
    private const float ACHIEVEMENTS_TEXT_TITLE_G_X = 430f;
    private const float ACHIEVEMENTS_TEXT_TITLE_G_Y = 208f;
    private const float ACHIEVEMENTS_TITLE_TOP_X = 126f;
    private const float ACHIEVEMENTS_TITLE_TOP_Y = 328f;
    private const float ACHIEVEMENTS_TITLE_WIDTH = 150f;
    private const float ACHIEVEMENTS_TITLE_HEIGHT = 58f;
    private const float ACHIEVEMENTS_ICON_WIDTH = 50f;
    private const float ACHIEVEMENTS_ICON_HEIGHT = 50f;
    private const float ACHIEVEMENTS_ICON_PESTCONTROL_TOP_X = 0.0f;
    private const float ACHIEVEMENTS_ICON_PESTCONTROL_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_EXTERMINATOR_TOP_X = 50f;
    private const float ACHIEVEMENTS_ICON_EXTERMINATOR_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_GRIMREAPER_TOP_X = 100f;
    private const float ACHIEVEMENTS_ICON_GRIMREAPER_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_PISTOLERA_TOP_X = 150f;
    private const float ACHIEVEMENTS_ICON_PISTOLERA_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_INQUISITOR_TOP_X = 201f;
    private const float ACHIEVEMENTS_ICON_INQUISITOR_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_ICECOLDKILLER_TOP_X = 0.0f;
    private const float ACHIEVEMENTS_ICON_ICECOLDKILLER_TOP_Y = 53f;
    private const float ACHIEVEMENTS_ICON_FALLOUT_TOP_X = 50f;
    private const float ACHIEVEMENTS_ICON_FALLOUT_TOP_Y = 53f;
    private const float ACHIEVEMENTS_ICON_WRATHOFGOD_TOP_X = 100f;
    private const float ACHIEVEMENTS_ICON_WRATHOFGOD_TOP_Y = 53f;
    private const float ACHIEVEMENTS_ICON_THEFALCON_TOP_X = 150f;
    private const float ACHIEVEMENTS_ICON_THEFALCON_TOP_Y = 53f;
    private const float ACHIEVEMENTS_ICON_SHAKEYOURBOOTY_TOP_X = 201f;
    private const float ACHIEVEMENTS_ICON_SHAKEYOURBOOTY_TOP_Y = 53f;
    private const float ACHIEVEMENTS_ICON_ONENUNARMY_TOP_X = 0.0f;
    private const float ACHIEVEMENTS_ICON_ONENUNARMY_TOP_Y = 103f;
    private const float ACHIEVEMENTS_ICON_WAYOFTHENUNJA_TOP_X = 50f;
    private const float ACHIEVEMENTS_ICON_WAYOFTHENUNJA_TOP_Y = 103f;
    private const float ACHIEVEMENTS_ICON_SURVIVAL_1000_KILLED_HARD_TOP_X = 0.0f;
    private const float ACHIEVEMENTS_ICON_SURVIVAL_1000_KILLED_HARD_TOP_Y = 153f;
    private const float ACHIEVEMENTS_ICON_STORY_100_KILLED_TOP_X = 0.0f;
    private const float ACHIEVEMENTS_ICON_STORY_100_KILLED_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_STORY_500_KILLED_TOP_X = 50f;
    private const float ACHIEVEMENTS_ICON_STORY_500_KILLED_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_STORY_1000_KILLED_TOP_X = 100f;
    private const float ACHIEVEMENTS_ICON_STORY_1000_KILLED_TOP_Y = 3f;
    private const float ACHIEVEMENTS_ICON_STORY_BOSS1_KILLED_TOP_X = 0.0f;
    private const float ACHIEVEMENTS_ICON_STORY_BOSS1_KILLED_TOP_Y = 203f;
    private const float ACHIEVEMENTS_ICON_STORY_BOSS2_KILLED_TOP_X = 50f;
    private const float ACHIEVEMENTS_ICON_STORY_BOSS2_KILLED_TOP_Y = 203f;
    private const float ACHIEVEMENTS_ICON_STORY_BOSS3_KILLED_TOP_X = 100f;
    private const float ACHIEVEMENTS_ICON_STORY_BOSS3_KILLED_TOP_Y = 203f;
    private const float ACHIEVEMENTS_ICON_STORY_FULLUPGRADE_TOP_X = 150f;
    private const float ACHIEVEMENTS_ICON_STORY_FULLUPGRADE_TOP_Y = 103f;
    private const float ACHIEVEMENTS_ICON_STORY_GAME_FINISHED_TOP_X = 100f;
    private const float ACHIEVEMENTS_ICON_STORY_GAME_FINISHED_TOP_Y = 103f;
    private const float ACHIEVEMENTS_ICON_STORY_GAME_FINISHED_HARD_TOP_X = 100f;
    private const float ACHIEVEMENTS_ICON_STORY_GAME_FINISHED_HARD_TOP_Y = 153f;
    private const float ACHIEVEMENTS_ICON_SECTORCLEAR_TOP_X = 201f;
    private const float ACHIEVEMENTS_ICON_SECTORCLEAR_TOP_Y = 103f;
    private const float ACHIEVEMENTS_ICON_SURVIVAL_GAME_FINISHED_HARD_TOP_X = 201f;
    private const float ACHIEVEMENTS_ICON_SURVIVAL_GAME_FINISHED_HARD_TOP_Y = 153f;
    private const float ACHIEVEMENTS_ICON_LOCKED_TOP_X = 151f;
    private const float ACHIEVEMENTS_ICON_LOCKED_TOP_Y = 203f;
    private const float ACHIEVEMENTS_ICON_SELECTOR_TOP_X = 201f;
    private const float ACHIEVEMENTS_ICON_SELECTOR_TOP_Y = 203f;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconPistolera = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconInquisitor = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconIceColdKiller = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconFallout = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconWrathOfGod = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconTheFalcon = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconShakeYourBooty = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconOneNunArmy = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconWayOfTheNunja = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconSectorClear = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconSurvival_GameFinishedHard = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_100ZombiesKilled = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_500ZombiesKilled = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_1000ZombiesKilled = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_Boss1Killed = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_Boss2Killed = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_Boss3Killed = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_AllWeaponsAndAbilitiesUpgraded = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_GameFinished = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconStory_GameFinishedHard = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconLocked = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIconSelector = new VertexPositionTexture[4];
    private Button m_buttonBack;
    private Button m_buttonNext;
    private Button m_buttonPrevious;
    private Button[] m_buttonArray;
    private PSS.GameEngine.Resources.Texture m_menuTexture;
    private PSS.GameEngine.Resources.Texture m_awardsTexture;
    private Transform m_t = new Transform();
    private Font m_font;
    private AchievementsMenuViewController.MenuAwardId m_selectedAward;
    private int m_pageIndex;
    private bool m_texturedBackground;
    private StringBuilder m_stringBuilder = new StringBuilder(16);

    public AchievementsMenuViewController()
    {
      this.m_texturedBackground = false;
      this.m_pageIndex = 0;
      this.m_menuTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_awardsTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_selectedAward = AchievementsMenuViewController.MenuAwardId.Achievement_Story_100ZombiesKilled;
      this.m_buttonBack = new Button();
      this.m_buttonNext = new Button();
      this.m_buttonPrevious = new Button();
      this.m_buttonArray = new Button[10];
      for (int index = 0; index < 10; ++index)
        this.m_buttonArray[index] = new Button();
    }

    public override void Initialize() => this.Initialize(false);

    public void Initialize(bool texturedBackground)
    {
      this.m_texturedBackground = texturedBackground;
      this.m_pageIndex = 0;
      this.m_selectedAward = AchievementsMenuViewController.MenuAwardId.Achievement_Story_100ZombiesKilled;
      base.Initialize();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_awardsTexture);
      GameEngine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
      if (this.m_texturedBackground)
      {
        this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
        this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
        QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
        QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      }
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      this.m_awardsTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_awards");
      QuadFactory.CreateAnchoredQuad(this.m_quadIconPistolera, 150f, 3f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconInquisitor, 201f, 3f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconIceColdKiller, 0.0f, 53f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconFallout, 50f, 53f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconWrathOfGod, 100f, 53f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconTheFalcon, 150f, 53f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconShakeYourBooty, 201f, 53f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconOneNunArmy, 0.0f, 103f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconWayOfTheNunja, 50f, 103f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconSectorClear, 201f, 103f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconSurvival_GameFinishedHard, 201f, 153f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_100ZombiesKilled, 0.0f, 3f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_500ZombiesKilled, 50f, 3f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_1000ZombiesKilled, 100f, 3f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_Boss1Killed, 0.0f, 203f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_Boss2Killed, 50f, 203f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_Boss3Killed, 100f, 203f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_AllWeaponsAndAbilitiesUpgraded, 150f, 103f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_GameFinished, 100f, 103f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconStory_GameFinishedHard, 100f, 153f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconLocked, 151f, 203f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadIconSelector, 201f, 203f, 50f, 50f, 256f, 256f, Anchor.HCenter | Anchor.VCenter);
      this.m_buttonNext.Initialize((ViewController) this, 56f, 56f);
      this.m_buttonNext.GetTransform().SetPosition(426f, 120f);
      this.m_buttonPrevious.Initialize((ViewController) this, 56f, 56f);
      this.m_buttonPrevious.GetTransform().SetPosition(46f, 120f);
      for (int index = 0; index < 10; ++index)
      {
        this.m_buttonArray[index].Initialize((ViewController) this, 58f, 58f);
        this.m_buttonArray[index].GetTransform().SetPosition((float) (120.0 + 60.0 * (double) (index % 5)), (float) (120.0 - 60.0 * (double) (index / 5)));
      }
      if (this.m_texturedBackground)
      {
        this.m_buttonBack.Initialize((ViewController) this, 56f, 56f);
        this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
      }
      else
      {
        this.m_buttonBack.Initialize((ViewController) this, 100f, 56f);
        this.m_buttonBack.GetTransform().SetPosition(30f, 40f);
      }
    }

    public override void Cleanup()
    {
      this.m_buttonBack.Cleanup();
      this.m_buttonNext.Cleanup();
      this.m_buttonPrevious.Cleanup();
      for (int index = 0; index < 10; ++index)
        this.m_buttonArray[index].Cleanup();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_awardsTexture);
      GameEngine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
      Application.GetInstance().RemoveResponder((Responder) this);
      base.Cleanup();
    }

    public override void Update(float elapsedTime)
    {
      if (!this.m_active)
        return;
      base.Update(elapsedTime);
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      Font font = GameApplication.GetInstance().GetFont();
      Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
      Prefs.Achievements cachedAchievements = Prefs.Instance.CachedAchievements;
      this.m_t.SetScale(1f);
      if (this.m_texturedBackground)
      {
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_menuTexture);
        this.m_t.SetPosition(240f, 260f, 0.0f);
        instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.AchievementsBig));
        this.m_t.SetPosition(36f, 40f, 0.0f);
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
      }
      else
      {
        instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
        this.m_t.SetPosition(240f, 260f, 0.0f);
        instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.AchievementsBig));
        this.m_t.SetPosition(30f, 40f, 0.0f);
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
      }
      this.m_stringBuilder.Length = 0;
     // StringHelper.Append(this.m_stringBuilder,//AchievementManager.Instance.GetEarnGamerScore());
      this.m_stringBuilder.Append(" / 200G");
      this.m_t.SetPosition(430f, 230f, 0.0f);
      instance.DrawString(font, this.m_t, 9, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
      this.m_stringBuilder.Length = 0;
      //StringHelper.Append(this.m_stringBuilder,//AchievementManager.Instance.GetNbUnlockedAchievements());
      this.m_stringBuilder.Append(" / 20 ");
      this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievements));
      this.m_t.SetPosition(30f, 230f, 0.0f);
      instance.DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, this.m_stringBuilder);
      this.m_t.SetPosition(426f, 120f, 0.0f);
      if (this.m_pageIndex < 1)
        instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, ">>");
      else
        instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, ">>");
      this.m_t.SetPosition(46f, 120f, 0.0f);
      if (this.m_pageIndex > 0)
        instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, "<<");
      else
        instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, "<<");
      string empty = string.Empty;
      string s1 = string.Empty;
      string s2 = string.Empty;
      switch (this.m_selectedAward)
      {
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_100ZombiesKilled:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_100ZombiesKilled_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Story_100ZombiesKilled || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_100ZombiesKilled_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_100ZombiesKilled_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_500ZombiesKilled:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_500ZombiesKilled_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Story_500ZombiesKilled || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_500ZombiesKilled_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_500ZombiesKilled_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_1000ZombiesKilled:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_1000ZombiesKilled_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Story_1000ZombiesKilled || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_1000ZombiesKilled_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_1000ZombiesKilled_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_Boss1Killed:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss1Killed_Name);
          s2 = "10G";
          s1 = !cachedAchievements.achievement_Story_Boss1Killed || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss1Killed_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss1Killed_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_Boss2Killed:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss2Killed_Name);
          s2 = "10G";
          s1 = !cachedAchievements.achievement_Story_Boss2Killed || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss2Killed_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss2Killed_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_Boss3Killed:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss3Killed_Name);
          s2 = "10G";
          s1 = !cachedAchievements.achievement_Story_Boss3Killed || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss3Killed_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_Boss3Killed_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_AllWeaponsAndAbilitiesUpgraded:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_AllWeaponsAndAbilitiesUpgraded_Name);
          s2 = "15G";
          s1 = !cachedAchievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_AllWeaponsAndAbilitiesUpgraded_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_AllWeaponsAndAbilitiesUpgraded_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_GameFinished:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_GameFinished_Name);
          s2 = "20G";
          s1 = !cachedAchievements.achievement_Story_GameFinished || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_GameFinished_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_GameFinished_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Story_GameFinishedHard:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_GameFinishedHard_Name);
          s2 = "25G";
          s1 = !cachedAchievements.achievement_Story_GameFinishedHard || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_GameFinishedHard_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Story_GameFinishedHard_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Pistolera:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Pistolera_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_500ZombiesKilledBullet || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Pistolera_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Pistolera_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Inquisitor:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Inquisitor_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_500ZombiesKilledFlameThrower || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Inquisitor_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Inquisitor_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_IceColdKiller:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_IceColdKiller_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_500ZombiesKilledIceGun || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_IceColdKiller_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_IceColdKiller_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Fallout:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Fallout_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_500ZombiesKilledExplosiveGun || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Fallout_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Fallout_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_WrathOfGod:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_WrathOfGod_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_1000ZombiesKilledHolyBeam || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_WrathOfGod_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_WrathOfGod_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_TheFalcon:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_TheFalcon_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_500ZombiesKilledAirSlash || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_TheFalcon_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_TheFalcon_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_ShakeYourBooty:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_ShakeYourBooty_Name);
          s2 = "10G";
          s1 = !cachedAchievements.achievement_Survival_100Ungrab || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_ShakeYourBooty_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_ShakeYourBooty_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_OneNunArmy:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_OneNunArmy_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_AllWeaponsUpgraded || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_OneNunArmy_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_OneNunArmy_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_WayOfTheNunja:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_WayOfTheNunja_Name);
          s2 = "5G";
          s1 = !cachedAchievements.achievement_Survival_AllAbilitiesUpgraded || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_WayOfTheNunja_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_WayOfTheNunja_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_SectorClear:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_SectorClear_Name);
          s2 = "20G";
          s1 = !cachedAchievements.achievement_Survival_GameFinished || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_SectorClear_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_SectorClear_Desc2);
          break;
        case AchievementsMenuViewController.MenuAwardId.Achievement_Survival_GameFinishedHard:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Survival_GameFinishedHard_Name);
          s2 = "25G";
          s1 = !cachedAchievements.achievement_Survival_GameFinishedHard || GuideManager.Instance.IsTrialMode() ? GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Survival_GameFinishedHard_Desc) : GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievement_Survival_GameFinishedHard_Desc2);
          break;
      }
      this.m_t.SetScale(1f);
      this.m_t.SetPosition(240f, 208f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, empty);
      this.m_t.SetPosition(430f, 208f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 9, GameConstant.MENU_TEXT_COLOR, s2);
      this.m_t.SetPosition(240f, 182f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, s1);
      switch (this.m_pageIndex)
      {
        case 0:
          this.m_t.SetPosition(120f, 120f, 0.0f);
          if (cachedAchievements.achievement_Story_100ZombiesKilled && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_100ZombiesKilled, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_100ZombiesKilled)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(180f, 120f, 0.0f);
          if (cachedAchievements.achievement_Story_500ZombiesKilled && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_500ZombiesKilled, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_500ZombiesKilled)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(240f, 120f, 0.0f);
          if (cachedAchievements.achievement_Story_1000ZombiesKilled && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_1000ZombiesKilled, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_1000ZombiesKilled)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(300f, 120f, 0.0f);
          if (cachedAchievements.achievement_Story_Boss1Killed && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_Boss1Killed, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_Boss1Killed)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(360f, 120f, 0.0f);
          if (cachedAchievements.achievement_Story_Boss2Killed && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_Boss2Killed, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_Boss2Killed)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(120f, 60f, 0.0f);
          if (cachedAchievements.achievement_Story_Boss3Killed && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_Boss3Killed, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_Boss3Killed)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(180f, 60f, 0.0f);
          if (cachedAchievements.achievement_Story_AllWeaponsAndAbilitiesUpgraded && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_AllWeaponsAndAbilitiesUpgraded, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_AllWeaponsAndAbilitiesUpgraded)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(240f, 60f, 0.0f);
          if (cachedAchievements.achievement_Story_GameFinished && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_GameFinished, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_GameFinished)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(300f, 60f, 0.0f);
          if (cachedAchievements.achievement_Story_GameFinishedHard && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconStory_GameFinishedHard, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Story_GameFinishedHard)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(360f, 60f, 0.0f);
          if (cachedAchievements.achievement_Survival_500ZombiesKilledBullet && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconPistolera, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Pistolera && !GuideManager.Instance.IsTrialMode())
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
            break;
          }
          break;
        case 1:
          this.m_t.SetPosition(120f, 120f, 0.0f);
          if (cachedAchievements.achievement_Survival_500ZombiesKilledFlameThrower && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconInquisitor, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Inquisitor)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(180f, 120f, 0.0f);
          if (cachedAchievements.achievement_Survival_500ZombiesKilledIceGun && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconIceColdKiller, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_IceColdKiller)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(240f, 120f, 0.0f);
          if (cachedAchievements.achievement_Survival_500ZombiesKilledExplosiveGun && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconFallout, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Fallout)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(300f, 120f, 0.0f);
          if (cachedAchievements.achievement_Survival_1000ZombiesKilledHolyBeam && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconWrathOfGod, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_WrathOfGod)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(360f, 120f, 0.0f);
          if (cachedAchievements.achievement_Survival_500ZombiesKilledAirSlash && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconTheFalcon, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_TheFalcon)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(120f, 60f, 0.0f);
          if (cachedAchievements.achievement_Survival_100Ungrab && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconShakeYourBooty, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_ShakeYourBooty)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(180f, 60f, 0.0f);
          if (cachedAchievements.achievement_Survival_AllWeaponsUpgraded && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconOneNunArmy, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_OneNunArmy)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(240f, 60f, 0.0f);
          if (cachedAchievements.achievement_Survival_AllAbilitiesUpgraded && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconWayOfTheNunja, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_WayOfTheNunja)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(300f, 60f, 0.0f);
          if (cachedAchievements.achievement_Survival_GameFinished && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSectorClear, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_SectorClear)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          }
          this.m_t.SetScale(1f);
          this.m_t.SetPosition(360f, 60f, 0.0f);
          if (cachedAchievements.achievement_Survival_GameFinishedHard && !GuideManager.Instance.IsTrialMode())
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSurvival_GameFinishedHard, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          else
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconLocked, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
          if (this.m_selectedAward == AchievementsMenuViewController.MenuAwardId.Achievement_Survival_GameFinishedHard)
          {
            this.m_t.SetScale(1.1f);
            instance.DrawQuad<VertexPositionTexture>(this.m_quadIconSelector, VertexFormat.Position3Texture, this.m_t, this.m_awardsTexture);
            break;
          }
          break;
      }
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonBack && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
      {
        if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
          MainMenuStateAchievements.GetInstance().BackRequest = true;
        else
          InGameMenuStateAchievements.GetInstance().BackRequest = true;
        this.m_pageIndex = 0;
        this.m_selectedAward = AchievementsMenuViewController.MenuAwardId.Achievement_Story_100ZombiesKilled;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else if (sender == this.m_buttonNext && this.m_pageIndex < 1 && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
      {
        ++this.m_pageIndex;
        this.m_selectedAward = (AchievementsMenuViewController.MenuAwardId) (this.m_pageIndex * 10);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
      }
      else if (sender == this.m_buttonPrevious && this.m_pageIndex > 0 && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
      {
        --this.m_pageIndex;
        this.m_selectedAward = (AchievementsMenuViewController.MenuAwardId) (this.m_pageIndex * 10);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
      }
      else
      {
        for (int index = 0; index < 10; ++index)
        {
          if (sender == this.m_buttonArray[index] && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
          {
            this.m_selectedAward = (AchievementsMenuViewController.MenuAwardId) (index + this.m_pageIndex * 10);
            if (this.m_selectedAward < AchievementsMenuViewController.MenuAwardId.Number)
              break;
            this.m_selectedAward = AchievementsMenuViewController.MenuAwardId.Achievement_Survival_GameFinishedHard;
            break;
          }
        }
      }
    }

    public enum MenuAwardId
    {
      Achievement_Story_100ZombiesKilled,
      Achievement_Story_500ZombiesKilled,
      Achievement_Story_1000ZombiesKilled,
      Achievement_Story_Boss1Killed,
      Achievement_Story_Boss2Killed,
      Achievement_Story_Boss3Killed,
      Achievement_Story_AllWeaponsAndAbilitiesUpgraded,
      Achievement_Story_GameFinished,
      Achievement_Story_GameFinishedHard,
      Achievement_Pistolera,
      Achievement_Inquisitor,
      Achievement_IceColdKiller,
      Achievement_Fallout,
      Achievement_WrathOfGod,
      Achievement_TheFalcon,
      Achievement_ShakeYourBooty,
      Achievement_OneNunArmy,
      Achievement_WayOfTheNunja,
      Achievement_SectorClear,
      Achievement_Survival_GameFinishedHard,
      Number,
    }
  }
}
