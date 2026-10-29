// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.UI.FeaturesViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.FSM.GameState;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;

namespace Resanctified.Game.UI
{
  public class FeaturesViewController : ViewController
  {
    private const int FEATURES_SCREEN_NB = 5;
    private const float FEATURES_BG_TOP_X = 0.0f;
    private const float FEATURES_BG_TOP_Y = 0.0f;
    private const float FEATURES_BG_WIDTH = 480f;
    private const float FEATURES_BG_HEIGHT = 320f;
    private const float FEATURES_TEXTURE_WIDTH = 512f;
    private const float FEATURES_TEXTURE_HEIGHT = 512f;
    private const float FEATURES_TEXT_X = 240f;
    private const float FEATURES_TEXT1_Y = 180f;
    private const float FEATURES_TEXT2_Y = 150f;
    private const float FEATURES_TEXT_BG_Y = 210f;
    private const float FEATURES_TEXT_BG_HEIGHT = 80f;
    private const float FEATURES_TEXT_BG2_Y = 40f;
    private const float FEATURES_TEXT_BG2_HEIGHT = 40f;
    private const float FEATURES_BUTTON_BUY_X = 360f;
    private const float FEATURES_BUTTON_BUY_Y = 20f;
    private const float FEATURES_BUTTON_EXIT_X = 80f;
    private const float FEATURES_BUTTON_EXIT_Y = 20f;
    private const float FEATURES_BUTTON_WIDTH = 160f;
    private const float FEATURES_BUTTON_HEIGHT = 40f;
    private const float FEATURES_TEXT_SCREEN_TIME = 2f;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionColor[] m_quadTextBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionColor[] m_quadTextBackgroundBlack2 = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private Resanctified.Engine.Resources.Texture[] m_featuresTextureArray;
    private int m_screenIndex;
    private float m_timer;
    private Transform m_t = new Transform();
    public bool BackToArcade;
    private Button m_buttonExit;
    private Button m_buttonBuy;

    public int GetScreenIndex() => this.m_screenIndex;

    public bool NextScreen()
    {
      if (this.m_screenIndex == 4)
        return false;
      this.m_timer = 0.0f;
      ++this.m_screenIndex;
      return true;
    }

    public void SetScreen(int screenIndex) => this.m_screenIndex = screenIndex;

    public FeaturesViewController()
    {
      this.m_buttonExit = new Button();
      this.m_buttonBuy = new Button();
      this.BackToArcade = false;
      this.m_screenIndex = 0;
      this.m_timer = 0.0f;
      this.m_featuresTextureArray = new Resanctified.Engine.Resources.Texture[5];
      for (int index = 0; index < 5; ++index)
        this.m_featuresTextureArray[index] = (Resanctified.Engine.Resources.Texture) null;
    }

    public override void Initialize()
    {
      base.Initialize();
      this.BackToArcade = false;
      for (int index = 0; index < 5; ++index)
        Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_featuresTextureArray[index]);
      this.m_featuresTextureArray[0] = (Resanctified.Engine.Resources.Texture) null;
      this.m_featuresTextureArray[1] = ResourceManager.GetInstance().LoadTexture("Textures\\screenshot2");
      this.m_featuresTextureArray[2] = ResourceManager.GetInstance().LoadTexture("Textures\\screenshot1");
      this.m_featuresTextureArray[3] = ResourceManager.GetInstance().LoadTexture("Textures\\screenshot3");
      this.m_featuresTextureArray[4] = ResourceManager.GetInstance().LoadTexture("Textures\\screenshot4");
      QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, Color.Black);
      QuadFactory.CreateQuad(this.m_quadTextBackgroundBlack, 0.0f, 210f, 480f, 80f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
      QuadFactory.CreateQuad(this.m_quadTextBackgroundBlack2, 0.0f, 40f, 480f, 40f, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
      this.m_buttonExit.Initialize((ViewController) this, 160f, 40f);
      this.m_buttonExit.GetTransform().SetPosition(80f, 20f);
      this.m_buttonBuy.Initialize((ViewController) this, 160f, 40f);
      this.m_buttonBuy.GetTransform().SetPosition(360f, 20f);
    }

    public override void Cleanup()
    {
      for (int index = 0; index < 5; ++index)
        Resanctified.Engine.Resources.Resource.SafeRelease<Resanctified.Engine.Resources.Texture>(ref this.m_featuresTextureArray[index]);
      base.Cleanup();
    }

    public override void Update(float elapsedTime)
    {
      if (!this.m_active)
        return;
      this.m_timer += elapsedTime;
      base.Update(elapsedTime);
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      Font font = GameApplication.GetInstance().GetFont();
      if (this.m_screenIndex == 0)
      {
        instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, Color.Black);
        this.m_t.SetPosition(240f, 200f, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ThankYou));
        this.m_t.SetPosition(240f, 160f, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Features_0));
      }
      else
        instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_featuresTextureArray[this.m_screenIndex]);
      instance.DrawQuad<VertexPositionColor>(this.m_quadTextBackgroundBlack2, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
      this.m_t.SetPosition(360f, 20f, 0.0f);
      if (GuideManager.Instance.IsPurchaseAvailable())
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.UnlockFullGame));
      else
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.UnlockFullGame));
      if (this.BackToArcade)
      {
        this.m_t.SetPosition(80f, 20f, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Exit));
      }
      else
      {
        this.m_t.SetPosition(80f, 20f, 0.0f);
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.BackToMainMenu));
      }
      if ((double) this.m_timer >= 2.0)
      {
        if (this.m_screenIndex > 0)
          instance.DrawQuad<VertexPositionColor>(this.m_quadTextBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, GameConstant.OUTGAME_BLACKBACKGROUND_COLOR);
        switch (this.m_screenIndex)
        {
          case 1:
            this.m_t.SetPosition(240f, 180f, 0.0f);
            Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Features_1));
            break;
          case 2:
            this.m_t.SetPosition(240f, 180f, 0.0f);
            Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Features_2));
            break;
          case 3:
            this.m_t.SetPosition(240f, 180f, 0.0f);
            Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Features_3));
            break;
          case 4:
            this.m_t.SetPosition(240f, 180f, 0.0f);
            Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Features_4));
            break;
        }
      }
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonExit)
      {
        if (((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        GameStateFeatures.GetInstance().ExitRequest = true;
      }
      else
      {
        if (sender != this.m_buttonBuy || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        GameStateFeatures.GetInstance().BuyRequest = true;
      }
    }
  }
}
