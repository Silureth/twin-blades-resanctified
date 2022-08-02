// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.LogoMGSViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM.GameState;
using PSS.GameEngine;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.UI
{
  public class LogoMGSViewController : ViewController
  {
    private const float LOGO_TOP_X = 0.0f;
    private const float LOGO_TOP_Y = 0.0f;
    private const float LOGO_WIDTH = 480f;
    private const float LOGO_HEIGHT = 320f;
    private const float LOGO_TEXTURE_WIDTH = 512f;
    private const float LOGO_TEXTURE_HEIGHT = 512f;
    private Button m_buttonNext;
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private PSS.GameEngine.Resources.Texture m_texture;

    public LogoMGSViewController()
    {
      this.m_texture = (PSS.GameEngine.Resources.Texture) null;
      this.m_buttonNext = new Button();
    }

    public override void Initialize()
    {
      base.Initialize();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_texture);
      this.m_texture = ResourceManager.GetInstance().LoadTexture("Textures\\logo_mgs");
      QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
      this.m_buttonNext.Initialize((ViewController) this, 480f, 320f);
      this.m_buttonNext.GetTransform().SetPosition(240f, 160f);
    }

    public override void Cleanup()
    {
      this.m_buttonNext.Cleanup();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_texture);
      Application.GetInstance().RemoveResponder((Responder) this);
      base.Cleanup();
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_texture);
      instance.End2DMode();
    }

    public override void Update(float elapsedTime)
    {
      int num = this.m_active ? 1 : 0;
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || sender != this.m_buttonNext || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      GameStateLogoMGS.GetInstance().NextRequest = true;
    }
  }
}
