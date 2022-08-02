// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.EONExitConfirmationViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.UI
{
  public class EONExitConfirmationViewController : ViewController
  {
    private const float EXIT_TEXT_X = 240f;
    private const float EXIT_TEXT_Y = 160f;
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
    private PSS.GameEngine.Resources.Texture m_menuTexture;
    private Button m_buttonBack;
    private Button m_buttonOk;
    private Transform m_t = new Transform();
    public EONExitConfirmationViewController.ExitRequest Request;
    public bool ButtonsEnabled = true;

    public EONExitConfirmationViewController()
    {
      this.m_menuTexture = (PSS.GameEngine.Resources.Texture) null;
      this.Request = EONExitConfirmationViewController.ExitRequest.None;
      this.m_buttonBack = new Button();
      this.m_buttonOk = new Button();
      QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadOk, 5f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
      this.ButtonsEnabled = true;
    }

    public override void Initialize()
    {
      base.Initialize();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
      this.m_buttonBack.Initialize((ViewController) this, 56f, 56f);
      this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
      this.m_buttonOk.Initialize((ViewController) this, 56f, 56f);
      this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
      this.Request = EONExitConfirmationViewController.ExitRequest.None;
      this.ButtonsEnabled = true;
    }

    public override void Cleanup()
    {
      this.m_buttonOk.Cleanup();
      this.m_buttonBack.Cleanup();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
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
      instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_menuTexture);
      this.m_t.SetPosition(240f, 160f);
      instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.EONExitConfirmation));
      this.m_t.SetPosition(36f, 40f, 0.0f);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
      this.m_t.SetPosition(440f, 40f, 0.0f);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadOk, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || !this.ButtonsEnabled)
        return;
      ButtonActionEvent buttonActionEvent = (ButtonActionEvent) evt;
      if (sender == this.m_buttonOk && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed)
      {
        this.Request = EONExitConfirmationViewController.ExitRequest.Ok;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      else
      {
        if (sender != this.m_buttonBack || buttonActionEvent.buttonState != PSSButtonState.ButtonStatePressed)
          return;
        this.Request = EONExitConfirmationViewController.ExitRequest.Back;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
    }

    public enum ExitRequest
    {
      None,
      Ok,
      Back,
    }
  }
}
