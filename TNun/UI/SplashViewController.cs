// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.SplashViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM.GameState.MainMenu;
using PSS.GameEngine;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.UI
{
  public class SplashViewController : ViewController
  {
    public const float SPLASH_BG_TOP_X = 0.0f;
    public const float SPLASH_BG_TOP_Y = 192f;
    public const float SPLASH_BG_WIDTH = 480f;
    public const float SPLASH_BG_HEIGHT = 320f;
    public const float SPLASH_TEXTURE_WIDTH = 512f;
    public const float SPLASH_TEXTURE_HEIGHT = 512f;
    public const float MAINMENU_TITLE_TOP_U = 0.0f;
    public const float MAINMENU_TITLE_TOP_V = 0.0f;
    public const float MAINMENU_TITLE_WIDTH = 170f;
    public const float MAINMENU_TITLE_HEIGHT = 160f;
    public const float MAINMENU_TITLE_X = 395f;
    public const float MAINMENU_TITLE_Y = 320f;
    public const int HELP_LAST_PAGE_INDEX = 1;
    private const float SPLASH_PRESSSTART_X = 240f;
    private const float SPLASH_PRESSSTART_Y = 150f;
    private const float SPLASH_PRESSSTART_SCALE = 1.5f;
    private const float BLINK_TIME_ON = 0.8f;
    private const float BLINK_TIME_OFF = 0.3f;
    private float m_timer;
    private bool m_pressStartVisible;
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadTitle = new VertexPositionTexture[4];
    private Button m_buttonStart;
    private PSS.GameEngine.Resources.Texture m_splashTexture;
    private Transform m_t = new Transform();

    public SplashViewController()
    {
      this.m_splashTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_timer = 0.0f;
      this.m_pressStartVisible = true;
      this.m_buttonStart = new Button();
    }

    public override void Initialize()
    {
      base.Initialize();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_splashTexture);
      this.m_splashTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_splash");
      QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 192f, 480f, 320f, 512f, 512f, Anchor.Bottom);
      QuadFactory.CreateAnchoredQuad(this.m_quadTitle, 0.0f, 0.0f, 170f, 160f, 512f, 512f, Anchor.HCenter);
      this.m_timer = 0.8f;
      this.m_pressStartVisible = true;
      this.m_buttonStart.Initialize((ViewController) this, 480f, 320f);
      this.m_buttonStart.GetTransform().SetPosition(240f, 160f);
    }

    public override void Cleanup()
    {
      this.m_buttonStart.Cleanup();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_splashTexture);
      Application.GetInstance().RemoveResponder((Responder) this);
      base.Cleanup();
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      GameApplication.GetInstance().GetFont();
      instance.Begin2DMode(false);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_splashTexture);
      this.m_t.SetScale(1f);
      this.m_t.SetPosition(395f, 320f, 0.0f);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadTitle, VertexFormat.Position3Texture, this.m_t, this.m_splashTexture);
      instance.End2DMode();
    }

    public override void Update(float elapsedTime)
    {
      if (!this.m_active)
        return;
      this.m_timer -= elapsedTime;
      if ((double) this.m_timer <= 0.0)
      {
        if (this.m_pressStartVisible)
        {
          this.m_pressStartVisible = false;
          this.m_timer = 0.3f;
        }
        else
        {
          this.m_pressStartVisible = true;
          this.m_timer = 0.8f;
        }
      }
      base.Update(elapsedTime);
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || sender != this.m_buttonStart || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      MainMenuStatePressStart.GetInstance().StartRequest = true;
    }
  }
}
