// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.UI.EndingViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.FSM.GameState;
using Resanctified.Engine;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;

namespace Resanctified.Game.UI
{
  public class EndingViewController : ViewController
  {
    private const float ENDING_BG_TOP_X = 0.0f;
    private const float ENDING_BG_TOP_Y = 0.0f;
    private const float ENDING_BG_WIDTH = 480f;
    private const float ENDING_BG_HEIGHT = 320f;
    private const float ENDING_TEXTURE_WIDTH = 512f;
    private const float ENDING_TEXTURE_HEIGHT = 512f;
    private const float ENDING_TEXT_TITLE_X = 240f;
    private const float ENDING_TEXT_TITLE1_Y = 220f;
    private const float ENDING_TEXT_TITLE2_Y = 180f;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private Resanctified.Engine.Resources.Texture m_endingTexture;
    private Transform m_t = new Transform();
    private Button m_buttonNext;

    public EndingViewController()
    {
      this.m_endingTexture = (Resanctified.Engine.Resources.Texture) null;
      this.m_buttonNext = new Button();
    }

    public override void Initialize()
    {
      base.Initialize();
      Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_endingTexture);
      this.m_endingTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_ending");
      QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
      this.m_buttonNext.Initialize((ViewController) this, 480f, 320f);
      this.m_buttonNext.GetTransform().SetPosition(240f, 160f);
    }

    public override void Cleanup()
    {
      this.m_buttonNext.Cleanup();
      Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_endingTexture);
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
      Font font = GameApplication.GetInstance().GetFont();
      instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_endingTexture);
      if (GameStateEnding.GetInstance().DisplayThankYouRequest)
      {
        instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
        this.m_t.SetPosition(240f, 220f, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_TITLE1_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.PressStartStudio));
        this.m_t.SetPosition(240f, 180f, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.CREDITS_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ThankYou));
      }
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || sender != this.m_buttonNext || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      GameStateEnding.GetInstance().NextRequest = true;
    }
  }
}
