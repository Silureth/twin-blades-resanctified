// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.BackToMainMenuConfirmationViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.GameEngine;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.UI
{
  public class BackToMainMenuConfirmationViewController : ViewController
  {
    private const float EXIT_TEXT_X = 240f;
    private const float EXIT_TEXT_1_Y = 240f;
    private const float EXIT_TEXT_2_Y = 190f;
    private const float EXIT_TEXT_3_Y = 160f;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
    private Button m_buttonOk;
    private Button m_buttonCancel;
    private Transform m_t = new Transform();

    public BackToMainMenuConfirmationViewController()
    {
      this.m_buttonOk = new Button();
      this.m_buttonCancel = new Button();
    }

    public override void Initialize()
    {
      base.Initialize();
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      this.m_buttonOk.Initialize((ViewController) this, 100f, 56f);
      this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
      this.m_buttonCancel.Initialize((ViewController) this, 100f, 56f);
      this.m_buttonCancel.GetTransform().SetPosition(30f, 40f);
    }

    public override void Cleanup()
    {
      this.m_buttonOk.Cleanup();
      this.m_buttonCancel.Cleanup();
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
      instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      this.m_t.SetPosition(30f, 40f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Cancel));
      this.m_t.SetPosition(440f, 40f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 9, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Ok));
      this.m_t.SetPosition(240f, 240f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.BackToMainMenu));
      this.m_t.SetPosition(240f, 190f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.AskAreYouSure));
      this.m_t.SetPosition(240f, 160f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ConfirmProgressErased));
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonOk && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
        InGameMenuStateBackToMainMenuConfirmation.GetInstance().ExitRequest = true;
      if (sender != this.m_buttonCancel || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      InGameMenuStateBackToMainMenuConfirmation.GetInstance().PreviousRequest = true;
    }
  }
}
