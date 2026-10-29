// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.GameStateFeatures
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.UI;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState
{
  public class GameStateFeatures : IDrawableState, IState
  {
    private const float FEATURES_PAGE_TIME = 7f;
    private static GameStateFeatures m_instance = new GameStateFeatures();
    private FeaturesViewController m_featuresViewController;
    private float m_timer;
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;
    private int m_state;
    public bool BuyRequest;
    public bool ExitRequest;

    public static GameStateFeatures Instance => GameStateFeatures.m_instance;

    public static GameStateFeatures GetInstance() => GameStateFeatures.m_instance;

    public int GetState() => this.m_state;

    public GameStateFeatures()
    {
      this.m_featuresViewController = new FeaturesViewController();
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
      this.m_state = 0;
      this.BuyRequest = false;
      this.ExitRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BuyRequest = false;
      this.ExitRequest = false;
      this.m_featuresViewController.Initialize();
      AudioManager.GetInstance().LoadMusic("Sounds\\music_credits");
      ResourceManager.GetInstance().LoadAllResources();
      GameApplication.GetInstance().SetSoundVolume();
      AudioManager.GetInstance().PlayMusic();
      QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
      this.m_timer = 3.5f;
      this.m_featuresViewController.SetScreen(0);
      this.m_state = 0;
      this.m_featuresViewController.BackToArcade = stateMachine.GetPreviousState() != GameStateInGame.GetInstance();
      Application.GetInstance().AddResponder((Responder) this.m_featuresViewController);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      this.m_featuresViewController.Update(gameTime);
      switch (this.m_state)
      {
        case 0:
          if (!GuideManager.Instance.IsTrialMode())
          {
            this.m_state = 1;
            this.m_timer = 2f;
            break;
          }
          if (InputManager.GetInstance().IsBackPressed())
          {
            stateMachine.SetState((IState) GameStateMainMenu.GetInstance());
            break;
          }
          if (this.ExitRequest)
          {
            this.m_state = 2;
            break;
          }
          if (GuideManager.Instance.IsPurchaseAvailable() && this.BuyRequest)
          {
            this.BuyRequest = false;
            GuideManager.Instance.RequestShowMarketplace = true;
          }
          //if (Guide.IsVisible)
          //  break;
          this.m_timer -= gameTime;
          if ((double) this.m_timer > 0.0)
            break;
          this.m_timer = 7f;
          if (this.m_featuresViewController.NextScreen())
            break;
          this.m_state = 1;
          this.m_timer = 2f;
          break;
        case 1:
          this.m_timer -= gameTime;
          if ((double) this.m_timer > 0.0)
            break;
          this.m_timer = 0.0f;
          this.m_state = 2;
          break;
        case 2:
          if (stateMachine.GetPreviousState() == GameStateInGame.GetInstance())
          {
            stateMachine.SetState((IState) GameStateMainMenu.GetInstance());
            break;
          }
          //if (Guide.IsVisible)
          //  break;
          GameApplication.GetInstance().ExitRequest = true;
          break;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      Application.GetInstance().RemoveResponder((Responder) this.m_featuresViewController);
      AudioManager.GetInstance().StopAll();
      AudioManager.GetInstance().UnloadMusic();
      InputManager.GetInstance().ClearPadState();
      this.m_featuresViewController.Cleanup();
      ResourceManager.GetInstance().DestroyUnusedResources();
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      this.m_featuresViewController.Draw();
      if (this.m_state <= 0)
        return;
      float num = 0.0f;
      if ((double) this.m_timer < 2.0)
        num = (double) this.m_timer > 0.0 ? (float) (1.0 - (double) this.m_timer / 2.0) * (float) byte.MaxValue : (float) byte.MaxValue;
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      this.m_fadeColor.A = (byte) num;
      instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, this.m_fadeColor);
      instance.End2DMode();
    }
  }
}
