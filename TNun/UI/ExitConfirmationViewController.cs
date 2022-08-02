// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.ExitConfirmationViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

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
  public class ExitConfirmationViewController : ViewController
  {
    private const float EXIT_TEXT_X = 240f;
    private const float EXIT_TEXT_Y = 240f;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
    private PSS.GameEngine.Resources.Texture m_menuTexture;
    private Button m_buttonOk;
    private Button m_buttonCancel;
    private bool m_texturedBackground;
    private Transform m_t = new Transform();

    public ExitConfirmationViewController()
    {
      this.m_texturedBackground = false;
      this.m_menuTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_buttonOk = new Button();
      this.m_buttonCancel = new Button();
    }

    public override void Initialize() => this.Initialize(false);

    public void Initialize(bool texturedBackground)
    {
      this.m_texturedBackground = texturedBackground;
      base.Initialize();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      if (this.m_texturedBackground)
      {
        this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
        QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
        QuadFactory.CreateAnchoredQuad(this.m_quadOk, 5f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
        QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      }
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      if (this.m_texturedBackground)
      {
        this.m_buttonOk.Initialize((ViewController) this, 56f, 56f);
        this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
        this.m_buttonCancel.Initialize((ViewController) this, 56f, 56f);
        this.m_buttonCancel.GetTransform().SetPosition(36f, 40f);
      }
      else
      {
        this.m_buttonOk.Initialize((ViewController) this, 100f, 56f);
        this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
        this.m_buttonCancel.Initialize((ViewController) this, 100f, 56f);
        this.m_buttonCancel.GetTransform().SetPosition(30f, 40f);
      }
    }

    public override void Cleanup()
    {
      this.m_buttonOk.Cleanup();
      this.m_buttonCancel.Cleanup();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
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
      if (!this.m_visible || !this.m_visible)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      Font font = GameApplication.GetInstance().GetFont();
      if (this.m_texturedBackground)
      {
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_menuTexture);
        this.m_t.SetPosition(440f, 40f, 0.0f);
        instance.DrawQuad<VertexPositionTexture>(this.m_quadOk, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
        this.m_t.SetPosition(36f, 40f, 0.0f);
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
      }
      else
      {
        instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
        this.m_t.SetPosition(30f, 40f, 0.0f);
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Cancel));
        this.m_t.SetPosition(440f, 40f, 0.0f);
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 9, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Ok));
      }
      this.m_t.SetPosition(240f, 240f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ExitConfirmation));
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonOk && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
      {
        if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
          MainMenuStateExit.GetInstance().ExitRequest = true;
        else
          InGameMenuStateExitConfirmation.GetInstance().ExitRequest = true;
      }
      if (sender != this.m_buttonCancel || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
        MainMenuStateExit.GetInstance().PreviousRequest = true;
      else
        InGameMenuStateExitConfirmation.GetInstance().PreviousRequest = true;
    }
  }
}
