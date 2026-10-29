// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.GameStateLogoMGS
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.UI;
using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.Resources;
using PSS.GameEngine.UI;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.FSM.GameState
{
  public class GameStateLogoMGS : IDrawableState, IState
  {
    private const float LOGO_TIME = 5f;
    private LogoMGSViewController m_logoViewController;
    private float m_timer;
    public bool NextRequest;
    private static GameStateLogoMGS m_instance = new GameStateLogoMGS();

    public static GameStateLogoMGS Instance => GameStateLogoMGS.m_instance;

    public static GameStateLogoMGS GetInstance() => GameStateLogoMGS.m_instance;

    public GameStateLogoMGS() => this.m_logoViewController = new LogoMGSViewController();

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.NextRequest = false;
      this.m_timer = 5f;
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
      CameraEntity cameraEntity = new CameraEntity();
      cameraEntity.Initialize();
      cameraEntity.SetOrthogonalProjection(480f, 320f, -1f, 1f);
      cameraEntity.GetTransform().SetPosition(240f, 160f, 0.0f);
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().SetViewMatrix(cameraEntity.GetViewMatrix());
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().SetProjectionMatrix(cameraEntity.GetProjectionMatrix());
      this.m_logoViewController.Initialize();
      Application.GetInstance().AddResponder((Responder) this.m_logoViewController);
      ResourceManager.GetInstance().LoadAllResources();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
      {
        GameApplication.GetInstance().ExitRequest = true;
      }
      else
      {
        if (this.NextRequest)
        {
          Application.GetInstance().RemoveResponder((Responder) this.m_logoViewController);
          this.NextRequest = false;
          this.m_timer = 0.0f;
        }
        this.m_timer -= gameTime;
        if ((double) this.m_timer > 0.0)
          return;
        stateMachine.SetState((IState) GameStateLogo.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.NextRequest = false;
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
      InputManager.GetInstance().ClearPadState();
      this.m_logoViewController.Cleanup();
      ResourceManager.GetInstance().DestroyUnusedResources();
    }

    public void Draw(IFiniteStateMachine stateMachine) => this.m_logoViewController.Draw();
  }
}
