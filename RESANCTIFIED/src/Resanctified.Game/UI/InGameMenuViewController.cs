// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.UI.InGameMenuViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.FSM;
using Resanctified.Game.FSM.GameState;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.World;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Tools;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using System.Text;

namespace Resanctified.Game.UI
{
  public class InGameMenuViewController : ViewController
  {
    private const float INGAMEMENU_TEXT_X = 240f;
    private const float INGAMEMENU_TEXT_UNLOCK_Y = 255f;
    private const float INGAMEMENU_TEXT_OFFSET_Y = 36f;
    private const float INGAMEMENU_TEXT_RESUME_Y = 219f;
    private const float INGAMEMENU_TEXT_ACHIEVEMENTS_Y = 183f;
    private const float INGAMEMENU_TEXT_HIGHSCORES_Y = 147f;
    private const float INGAMEMENU_TEXT_OPTIONS_Y = 111f;
    private const float INGAMEMENU_TEXT_BACKTOMAP_Y = 75f;
    private const float INGAMEMENU_TEXT_BACKTOMAINMENU_SURVIVAL_Y = 75f;
    private const float INGAMEMENU_TEXT_BACKTOMAINMENU_STORY_Y = 39f;
    private const float INGAMEMENU_BUTTON_X = 240f;
    private const float INGAMEMENU_BUTTON_Y_OFFSET = -10f;
    private const float INGAMEMENU_BUTTON_WIDTH = 200f;
    private const float INGAMEMENU_BUTTON_HEIGHT = 26f;
    private const float INGAMEMENU_DAY_X = 240f;
    private const float INGAMEMENU_DAY_Y = 287f;
    private const float INGAMEMENU_DIFFICULTY_X = 240f;
    private const float INGAMEMENU_DIFFICULTY_Y = 267f;
    private VertexPositionColor[] m_quadBackground = new VertexPositionColor[4];
    private Button m_buttonUnlock;
    private Button m_buttonResume;
    private Button m_buttonAchievements;
    private Button m_buttonHighscores;
    private Button m_buttonOptions;
    private Button m_buttonBackToMap;
    private Button m_buttonBackToMainMenuStory;
    private Button m_buttonBackToMainMenuSurvival;
    private Transform m_t = new Transform();
    private StringBuilder m_stringBuilder = new StringBuilder(16);

    public InGameMenuViewController()
    {
      this.m_buttonUnlock = new Button();
      this.m_buttonResume = new Button();
      this.m_buttonAchievements = new Button();
      this.m_buttonHighscores = new Button();
      this.m_buttonOptions = new Button();
      this.m_buttonBackToMap = new Button();
      this.m_buttonBackToMainMenuStory = new Button();
      this.m_buttonBackToMainMenuSurvival = new Button();
    }

    public override void Initialize()
    {
      base.Initialize();
      QuadFactory.CreateQuad(this.m_quadBackground, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      this.m_buttonUnlock.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonUnlock.GetTransform().SetPosition(240f, 245f);
      this.m_buttonResume.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonResume.GetTransform().SetPosition(240f, 209f);
      this.m_buttonAchievements.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonAchievements.GetTransform().SetPosition(240f, 173f);
      this.m_buttonHighscores.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonHighscores.GetTransform().SetPosition(240f, 137f);
      this.m_buttonOptions.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonOptions.GetTransform().SetPosition(240f, 101f);
      this.m_buttonBackToMap.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonBackToMap.GetTransform().SetPosition(240f, 65f);
      this.m_buttonBackToMainMenuStory.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonBackToMainMenuStory.GetTransform().SetPosition(240f, 29f);
      this.m_buttonBackToMainMenuSurvival.Initialize((ViewController) this, 200f, 26f);
      this.m_buttonBackToMainMenuSurvival.GetTransform().SetPosition(240f, 65f);
    }

    public override void Cleanup()
    {
      this.m_buttonUnlock.Cleanup();
      this.m_buttonResume.Cleanup();
      this.m_buttonAchievements.Cleanup();
      this.m_buttonHighscores.Cleanup();
      this.m_buttonOptions.Cleanup();
      this.m_buttonBackToMap.Cleanup();
      this.m_buttonBackToMainMenuStory.Cleanup();
      this.m_buttonBackToMainMenuSurvival.Cleanup();
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
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      instance.DrawQuad<VertexPositionColor>(this.m_quadBackground, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      Font font = GameApplication.GetInstance().GetFont();
      Color menuTextColor1 = GameConstant.MENU_TEXT_COLOR;
      this.m_t.SetScale(1f);
      this.m_t.SetPosition(240f, 287f);
      this.m_stringBuilder.Length = 0;
      this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Day));
      this.m_stringBuilder.Append(' ');
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
      {
        StringHelper.Append(this.m_stringBuilder, currentWorld.DayIndex, 2);
      }
      else
      {
        StringHelper.Append(this.m_stringBuilder, currentWorld.DayIndex, 2).Append('/');
        StringHelper.Append(this.m_stringBuilder, 31, 0);
      }
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 6, GameConstant.MENU_TEXT_HELP_SELECTED_COLOR, this.m_stringBuilder);
      this.m_t.SetPosition(240f, 267f);
      this.m_stringBuilder.Length = 0;
      this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Difficulty));
      if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
        this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.DifficultyHard));
      else
        this.m_stringBuilder.Append(GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.DifficultyNormal));
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 6, GameConstant.MENU_TEXT_HELP_SELECTED_COLOR, this.m_stringBuilder);
      if (GuideManager.Instance.IsTrialMode())
      {
        Color color = GameConstant.MENU_TEXT_COLOR;
        if (!GuideManager.Instance.IsPurchaseAvailable())
          color = GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR;
        this.m_t.SetScale(1f);
        this.m_t.SetPosition(240f, (float) byte.MaxValue, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, color, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.UnlockFullGame));
      }
      Color menuTextColor2 = GameConstant.MENU_TEXT_COLOR;
      this.m_t.SetScale(1f);
      this.m_t.SetPosition(240f, 219f, 0.0f);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, menuTextColor2, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ResumeGame));
      Color menuTextColor3 = GameConstant.MENU_TEXT_COLOR;
      this.m_t.SetScale(1f);
      this.m_t.SetPosition(240f, 183f, 0.0f);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, menuTextColor3, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Achievements));
      Color menuTextColor4 = GameConstant.MENU_TEXT_COLOR;
      this.m_t.SetScale(1f);
      this.m_t.SetPosition(240f, 147f, 0.0f);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, menuTextColor4, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Highscores));
      Color menuTextColor5 = GameConstant.MENU_TEXT_COLOR;
      this.m_t.SetScale(1f);
      this.m_t.SetPosition(240f, 111f, 0.0f);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, menuTextColor5, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HelpAndOptions));
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
      {
        Color color;
        if (Scenario.GetInstance().GetCurrentMissionIndex() == 0 || currentWorld.GetHero().HitPoints <= 0 || InGameStateMenu.GetInstance().ScriptExecuting)
        {
          color = GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR;
          this.m_t.SetScale(1f);
        }
        else
        {
          color = GameConstant.MENU_TEXT_COLOR;
          this.m_t.SetScale(1f);
        }
        this.m_t.SetPosition(240f, 75f, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, color, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.BackToMap));
      }
      Color color1;
      if (currentWorld.GetHero().HitPoints <= 0)
      {
        color1 = GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR;
        this.m_t.SetScale(1f);
      }
      else
      {
        color1 = GameConstant.MENU_TEXT_COLOR;
        this.m_t.SetScale(1f);
      }
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
        this.m_t.SetPosition(240f, 39f, 0.0f);
      else
        this.m_t.SetPosition(240f, 75f, 0.0f);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, color1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.BackToMainMenu));
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonResume)
      {
        if (((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
        {
          InGameStateMenu.GetInstance().ResumeGameRequest = true;
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
        }
      }
      else if (sender == this.m_buttonAchievements)
      {
        if (((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
        {
          InGameStateMenu.GetInstance().AchievementsRequest = true;
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
        }
      }
      else if (sender == this.m_buttonHighscores)
      {
        if (((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
        {
          InGameStateMenu.GetInstance().HighscoresRequest = true;
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
        }
      }
      else if (sender == this.m_buttonOptions)
      {
        if (((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
        {
          InGameStateMenu.GetInstance().OptionsRequest = true;
          GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
        }
      }
      else if (sender == this.m_buttonUnlock && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed && GuideManager.Instance.IsTrialMode() && GuideManager.Instance.IsPurchaseAvailable())
      {
        InGameStateMenu.GetInstance().UnlockRequest = true;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story && GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
      {
        if (sender == this.m_buttonBackToMap && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed && Scenario.GetInstance().GetCurrentMissionIndex() > 0 && currentWorld.GetHero().HitPoints > 0 && !InGameStateMenu.GetInstance().ScriptExecuting)
          InGameStateMenu.GetInstance().BackToMapRequest = true;
        if (sender != this.m_buttonBackToMainMenuStory || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed || currentWorld.GetHero().HitPoints <= 0)
          return;
        InGameStateMenu.GetInstance().BackToMainMenuRequest = true;
      }
      else
      {
        if (sender != this.m_buttonBackToMainMenuSurvival || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed || currentWorld.GetHero().HitPoints <= 0)
          return;
        InGameStateMenu.GetInstance().BackToMainMenuRequest = true;
      }
    }
  }
}
