// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.GameStateLogoLive
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.UI;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.FSM.GameState
{
  public class GameStateLogoLive : IDrawableState, IState
  {
    private const float LOGO_TIME = 2f;
    private LogoLiveViewController m_logoViewController;
    private static GameStateLogoLive m_instance = new GameStateLogoLive();

    public static GameStateLogoLive Instance => GameStateLogoLive.m_instance;

    public static GameStateLogoLive GetInstance() => GameStateLogoLive.m_instance;

    public GameStateLogoLive() => this.m_logoViewController = new LogoLiveViewController();

    public void Enter(IFiniteStateMachine stateMachine)
    {
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
      CameraEntity cameraEntity = new CameraEntity();
      cameraEntity.Initialize();
      cameraEntity.SetOrthogonalProjection(480f, 320f, -1f, 1f);
      cameraEntity.GetTransform().SetPosition(240f, 160f, 0.0f);
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().SetViewMatrix(cameraEntity.GetViewMatrix());
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().SetProjectionMatrix(cameraEntity.GetProjectionMatrix());
      this.m_logoViewController.Initialize();
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
        if (stateMachine.CurrentStateTime < 2.0)
          return;
        stateMachine.SetState((IState) GameStateLogoMGS.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
      InputManager.GetInstance().ClearPadState();
      this.m_logoViewController.Cleanup();
      ResourceManager.GetInstance().DestroyUnusedResources();
    }

    public void Draw(IFiniteStateMachine stateMachine) => this.m_logoViewController.Draw();
  }
}
