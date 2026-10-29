// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.UI.LogoViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.GameState;
using Resanctified.Engine;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.UI
{
  public class LogoViewController : ViewController
  {
    private const float LOGO_BG_WIDTH = 480f;
    private const float LOGO_BG_HEIGHT = 320f;
    private Button m_buttonNext;

    public LogoViewController() => this.m_buttonNext = new Button();

    public override void Initialize()
    {
      base.Initialize();
      this.m_buttonNext.Initialize((ViewController) this, 480f, 320f);
      this.m_buttonNext.GetTransform().SetPosition(240f, 160f);
    }

    public override void Cleanup()
    {
      this.m_buttonNext.Cleanup();
      Application.GetInstance().RemoveResponder((Responder) this);
      base.Cleanup();
    }

    public override void Draw()
    {
      int num = this.m_visible ? 1 : 0;
    }

    public override void Update(float elapsedTime)
    {
      int num = this.m_active ? 1 : 0;
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || sender != this.m_buttonNext || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      GameStateLogo.GetInstance().NextRequest = true;
    }
  }
}
