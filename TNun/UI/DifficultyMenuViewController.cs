// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.DifficultyMenuViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM.GameState.MainMenu;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.UI
{
  public class DifficultyMenuViewController : ViewController
  {
    private const float TITLE_X = 240f;
    private const float TITLE_Y = 250f;
    private const float TEXT_X = 240f;
    private const float TEXT_CHOOSE_Y = 200f;
    private const float TEXT_NORMAL_Y = 150f;
    private const float TEXT_HARD_Y = 105f;
    private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
    private PSS.GameEngine.Resources.Texture m_menuTexture;
    private Button m_buttonBack;
    private Button m_buttonNormal;
    private Button m_buttonHard;
    private Transform m_t = new Transform();
    private Font m_font;

    public DifficultyMenuViewController()
    {
      this.m_menuTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_buttonBack = new Button();
      this.m_buttonNormal = new Button();
      this.m_buttonHard = new Button();
    }

    public override void Initialize()
    {
      base.Initialize();
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      GameEngine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
      this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
      this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
      QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
      QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      this.m_buttonBack.Initialize((ViewController) this, 56f, 56f);
      this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
      this.m_buttonNormal.Initialize((ViewController) this, 200f, 30f);
      this.m_buttonNormal.GetTransform().SetPosition(240f, 140f);
      this.m_buttonHard.Initialize((ViewController) this, 200f, 30f);
      this.m_buttonHard.GetTransform().SetPosition(240f, 95f);
    }

    public override void Cleanup()
    {
      GameEngine.Resources.Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_menuTexture);
      GameEngine.Resources.Resource.SafeRelease<Font>(ref this.m_font);
      this.m_buttonBack.Cleanup();
      this.m_buttonNormal.Cleanup();
      this.m_buttonHard.Cleanup();
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
      instance.DrawQuad<VertexPositionTexture>(this.m_quadBackground, VertexFormat.Position3Texture, (Transform) null, this.m_menuTexture);
      this.m_t.SetPosition(36f, 40f, 0.0f);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
      Color menuTextColor = GameConstant.MENU_TEXT_COLOR;
      this.m_t.SetPosition(240f, 250f);
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
        instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.StoryModeBig));
      else
        instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.SurvivalModeBig));
      this.m_t.SetPosition(240f, 200f);
      instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.ChooseDifficulty));
      this.m_t.SetPosition(240f, 150f);
      instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.DifficultyNormal));
      this.m_t.SetPosition(240f, 105f);
      instance.DrawString(font, this.m_t, 4, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.DifficultyHard));
      instance.End2DMode();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      if (sender == this.m_buttonBack && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
        MainMenuStateDifficulty.GetInstance().BackRequest = true;
      if (sender == this.m_buttonNormal && ((ButtonActionEvent) evt).buttonState == PSSButtonState.ButtonStatePressed)
      {
        MainMenuStateDifficulty.GetInstance().StartGameRequest = true;
        GameApplication.GetInstance().SetDifficulty(GameApplication.GameDifficulty.Normal);
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      if (sender != this.m_buttonHard || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      MainMenuStateDifficulty.GetInstance().StartGameRequest = true;
      GameApplication.GetInstance().SetDifficulty(GameApplication.GameDifficulty.Hard);
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
    }
  }
}
