// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.LanguageMenuViewController
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
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.UI
{
  public class LanguageMenuViewController : ViewController
  {
    private const float LANGUAGES_TEXT_X = 240f;
    private const float LANGUAGES_TEXT_ENGLISH_Y = 250f;
    private const float LANGUAGES_TEXT_OFFSET_Y = 45f;
    private const float LANGUAGES_TEXT_FRENCH_Y = 205f;
    private const float LANGUAGES_TEXT_ITALIAN_Y = 160f;
    private const float LANGUAGES_TEXT_GERMAN_Y = 115f;
    private const float LANGUAGES_TEXT_SPANISH_Y = 70f;
    private const float LANGUAGES_BUTTON_Y_OFFSET = -10f;
    private const float LANGUAGES_BUTTON_WIDTH = 100f;
    private const float LANGUAGES_BUTTON_HEIGHT = 30f;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
    private PSS.GameEngine.Resources.Texture m_menuTexture;
    private bool m_texturedBackground;
    private Transform m_t = new Transform();
    private Button m_buttonEnglish;
    private Button m_buttonFrench;
    private Button m_buttonItalian;
    private Button m_buttonGerman;
    private Button m_buttonSpanish;
    private Button m_buttonBack;

    public LanguageMenuViewController()
    {
      this.m_menuTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_texturedBackground = false;
      this.m_buttonEnglish = new Button();
      this.m_buttonFrench = new Button();
      this.m_buttonItalian = new Button();
      this.m_buttonGerman = new Button();
      this.m_buttonSpanish = new Button();
      this.m_buttonBack = new Button();
    }

    public override void Initialize() => this.Initialize(false);

    public void Initialize(bool texturedBackground)
    {
      this.m_texturedBackground = texturedBackground;
      base.Initialize();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      if (this.m_texturedBackground)
      {
        this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
        QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
        QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      }
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
      this.m_buttonEnglish.Initialize((ViewController) this, 100f, 30f);
      this.m_buttonEnglish.GetTransform().SetPosition(240f, 240f);
      this.m_buttonFrench.Initialize((ViewController) this, 100f, 30f);
      this.m_buttonFrench.GetTransform().SetPosition(240f, 195f);
      this.m_buttonItalian.Initialize((ViewController) this, 100f, 30f);
      this.m_buttonItalian.GetTransform().SetPosition(240f, 150f);
      this.m_buttonGerman.Initialize((ViewController) this, 100f, 30f);
      this.m_buttonGerman.GetTransform().SetPosition(240f, 105f);
      this.m_buttonSpanish.Initialize((ViewController) this, 100f, 30f);
      this.m_buttonSpanish.GetTransform().SetPosition(240f, 60f);
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
      this.m_buttonEnglish.Cleanup();
      this.m_buttonFrench.Cleanup();
      this.m_buttonItalian.Cleanup();
      this.m_buttonGerman.Cleanup();
      this.m_buttonSpanish.Cleanup();
      this.m_buttonBack.Cleanup();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
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
      GameLocalisation.LanguageId currentLanguage = GameApplication.GetInstance().CurrentLanguage;
      Font font = GameApplication.GetInstance().GetFont();
      Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
      Color textSelectedColor = GameConstant.MENU_TEXT_SELECTED_COLOR;
      this.m_t.SetScale(1f);
      if (this.m_texturedBackground)
      {
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_menuTexture);
        this.m_t.SetPosition(36f, 40f, 0.0f);
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
      }
      else
      {
        instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
        this.m_t.SetPosition(30f, 40f, 0.0f);
        instance.DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
      }
      this.m_t.SetPosition(240f, 250f, 0.0f);
      if (currentLanguage == GameLocalisation.LanguageId.English)
        instance.DrawString(font, this.m_t, 4, textSelectedColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.English));
      else
        instance.DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.English));
      this.m_t.SetPosition(240f, 205f, 0.0f);
      if (currentLanguage == GameLocalisation.LanguageId.French)
        instance.DrawString(font, this.m_t, 4, textSelectedColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.French));
      else
        instance.DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.French));
      this.m_t.SetPosition(240f, 160f, 0.0f);
      if (currentLanguage == GameLocalisation.LanguageId.Italian)
        instance.DrawString(font, this.m_t, 4, textSelectedColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.Italian));
      else
        instance.DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.Italian));
      this.m_t.SetPosition(240f, 115f, 0.0f);
      if (currentLanguage == GameLocalisation.LanguageId.German)
        instance.DrawString(font, this.m_t, 4, textSelectedColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.German));
      else
        instance.DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.German));
      this.m_t.SetPosition(240f, 70f, 0.0f);
      if (currentLanguage == GameLocalisation.LanguageId.Spanish)
        instance.DrawString(font, this.m_t, 4, textSelectedColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.Spanish));
      else
        instance.DrawString(font, this.m_t, 4, menuTextColor, GameLocalisation.GetInstance().GetLanguageName(GameLocalisation.LanguageId.Spanish));
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonBack && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
      {
        if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
          MainMenuStateLanguageSelection.GetInstance().BackRequest = true;
        else
          InGameMenuStateLanguageSelection.GetInstance().BackRequest = true;
      }
      if (sender == this.m_buttonEnglish)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        GameApplication.GetInstance().CurrentLanguage = GameLocalisation.LanguageId.English;
        GameLocalisation.GetInstance().Initialize(GameLocalisation.LanguageId.English);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
      }
      else if (sender == this.m_buttonFrench)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        GameApplication.GetInstance().CurrentLanguage = GameLocalisation.LanguageId.French;
        GameLocalisation.GetInstance().Initialize(GameLocalisation.LanguageId.French);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
      }
      else if (sender == this.m_buttonItalian)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        GameApplication.GetInstance().CurrentLanguage = GameLocalisation.LanguageId.Italian;
        GameLocalisation.GetInstance().Initialize(GameLocalisation.LanguageId.Italian);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
      }
      else if (sender == this.m_buttonGerman)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        GameApplication.GetInstance().CurrentLanguage = GameLocalisation.LanguageId.German;
        GameLocalisation.GetInstance().Initialize(GameLocalisation.LanguageId.German);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
      }
      else
      {
        if (sender != this.m_buttonSpanish || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        GameApplication.GetInstance().CurrentLanguage = GameLocalisation.LanguageId.Spanish;
        GameLocalisation.GetInstance().Initialize(GameLocalisation.LanguageId.Spanish);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
      }
    }
  }
}
