// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.HelpAndOptionsMenuViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM;
using PSS.Game.NunVsZombies.FSM.GameState;
using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.Game.NunVsZombies.FSM.GameState.MainMenu;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.UI
{
  public class HelpAndOptionsMenuViewController : ViewController
  {
    private const float TITLE_X = 240f;
    private const float TITLE_Y = 250f;
    private const float TEXT_X = 240f;
    private const float TEXT_HELP_Y = 200f;
    private const float TEXT_SETTINGS_Y = 155f;
    private const float TEXT_CREDITS_Y = 110f;
    private const float TEXT_ABOUT_Y = 65f;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
    private PSS.GameEngine.Resources.Texture m_menuTexture;
    private Button m_buttonBack;
    private Button m_buttonHelp;
    private Button m_buttonSettings;
    private Button m_buttonCredits;
    private Button m_buttonAbout;
    private Transform m_t = new Transform();
    private Font m_font;
    private bool m_texturedBackground;

    public HelpAndOptionsMenuViewController()
    {
      this.m_texturedBackground = false;
      this.m_menuTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_buttonBack = new Button();
      this.m_buttonHelp = new Button();
      this.m_buttonSettings = new Button();
      this.m_buttonCredits = new Button();
      this.m_buttonAbout = new Button();
    }

    public override void Initialize() => this.Initialize(false);

    public void Initialize(bool texturedBackground)
    {
      this.m_texturedBackground = texturedBackground;
      base.Initialize();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      Resource.SafeRelease<Font>(ref this.m_font);
      if (this.m_texturedBackground)
      {
        this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
        this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
        QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
        QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      }
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      this.m_buttonHelp.Initialize((ViewController) this, 200f, 30f);
      this.m_buttonHelp.GetTransform().SetPosition(240f, 190f);
      this.m_buttonSettings.Initialize((ViewController) this, 200f, 30f);
      this.m_buttonSettings.GetTransform().SetPosition(240f, 145f);
      this.m_buttonCredits.Initialize((ViewController) this, 200f, 30f);
      this.m_buttonCredits.GetTransform().SetPosition(240f, 100f);
      this.m_buttonAbout.Initialize((ViewController) this, 200f, 30f);
      this.m_buttonAbout.GetTransform().SetPosition(240f, 55f);
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
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      Resource.SafeRelease<Font>(ref this.m_font);
      this.m_buttonBack.Cleanup();
      this.m_buttonHelp.Cleanup();
      this.m_buttonSettings.Cleanup();
      this.m_buttonCredits.Cleanup();
      this.m_buttonAbout.Cleanup();
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
      int currentLanguage = (int) GameApplication.GetInstance().CurrentLanguage;
      Font font = GameApplication.GetInstance().GetFont();
      if (this.m_texturedBackground)
      {
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_menuTexture);
        this.m_t.SetPosition(240f, 250f);
        instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.OptionsBig));
        this.m_t.SetPosition(36f, 40f, 0.0f);
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
      }
      else
      {
        instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
        this.m_t.SetPosition(240f, 250f);
        instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.HelpAndOptions));
        this.m_t.SetPosition(30f, 40f, 0.0f);
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
      }
      Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
      this.m_t.SetPosition(240f, 200f);
      instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help));
      this.m_t.SetPosition(240f, 155f);
      instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Settings));
      if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
      {
        this.m_t.SetPosition(240f, 110f);
        instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Credits));
        this.m_t.SetPosition(240f, 65f);
        instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.About));
      }
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonBack)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
          MainMenuStateHelpAndOptions.GetInstance().BackRequest = true;
        else
          InGameMenuStateHelpAndOptions.GetInstance().BackRequest = true;
      }
      else if (sender == this.m_buttonHelp)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStateReleased)
          return;
        if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
          MainMenuStateHelpAndOptions.GetInstance().HelpRequest = true;
        else
          InGameMenuStateHelpAndOptions.GetInstance().HelpRequest = true;
      }
      else if (sender == this.m_buttonSettings)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStateReleased)
          return;
        if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
          MainMenuStateHelpAndOptions.GetInstance().SettingsRequest = true;
        else
          InGameMenuStateHelpAndOptions.GetInstance().SettingsRequest = true;
      }
      else if (sender == this.m_buttonCredits)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStateReleased || GameFSM.GetInstance().GetCurrentState() != GameStateMainMenu.GetInstance())
          return;
        MainMenuStateHelpAndOptions.GetInstance().CreditsRequest = true;
      }
      else
      {
        if (sender != this.m_buttonAbout || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStateReleased || GameFSM.GetInstance().GetCurrentState() != GameStateMainMenu.GetInstance())
          return;
        MainMenuStateHelpAndOptions.GetInstance().AboutRequest = true;
      }
    }
  }
}
