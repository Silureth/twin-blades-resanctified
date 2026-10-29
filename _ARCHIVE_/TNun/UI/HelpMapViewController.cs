// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.HelpMapViewController
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
  public class HelpMapViewController : ViewController
  {
    private const float HELPMAP_POS_X = 240f;
    private const float HELPMAP_POS_Y = 200f;
    private Button m_buttonNext;
    private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
    private Transform m_t = new Transform();

    public HelpMapViewController() => this.m_buttonNext = new Button();

    public override void Initialize()
    {
      base.Initialize();
      QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      this.m_buttonNext.Initialize((ViewController) this, 480f, 320f);
      this.m_buttonNext.GetTransform().SetPosition(240f, 180f);
    }

    public override void Cleanup()
    {
      this.m_buttonNext.Cleanup();
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
      instance.DrawQuad<VertexPositionColor>(this.m_quadBackgroundBlack, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      Font font = GameApplication.GetInstance().GetFont();
      this.m_t.SetPosition(240f, 200f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_HELP_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Help_Map));
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || sender != this.m_buttonNext || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      InGameStateHelpMap.GetInstance().NextRequest = true;
    }
  }
}
