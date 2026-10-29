// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.GameStateEndOfNight
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.UI;
using Resanctified.Game.World;
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
  internal sealed class GameStateEndOfNight : IDrawableState, IState
  {
    private const float ENDOFNIGHT_FADE_TIME = 1f;
    private const float EON_HELP_PAGE_SWAP_TIME = 2f;
    private static readonly GameStateEndOfNight s_instance = new GameStateEndOfNight();
    private EndOfNightViewController m_viewController;
    private EONExitConfirmationViewController m_exitViewController;
    private VertexPositionColor[] m_quadFade;
    private int m_phase;
    private float m_fadeTime;
    private DrawableFiniteStateMachine m_subStateMachine;
    public bool BackToMainMenuRequest;
    public bool ClearInput;

    public static GameStateEndOfNight Instance => GameStateEndOfNight.s_instance;

    public static GameStateEndOfNight GetInstance() => GameStateEndOfNight.s_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
      {
        ResourceManager.GetInstance().DestroyUnusedResources();
        ResourceManager.GetInstance().UnloadContentManagerAndReloadResources();
      }
      this.m_viewController.Initialize(((GameWorld) GameWorldManager.Instance.GetCurrentWorld()).GetHero());
      this.m_exitViewController.Initialize();
      ResourceManager.Instance.LoadAllResources();
      this.m_phase = 0;
      this.m_fadeTime = 0.0f;
      AudioManager.Instance.LoadMusic("Sounds\\music_splash");
      GameApplication.GetInstance().SetSoundVolume();
      AudioManager.Instance.PlayMusic();
      this.BackToMainMenuRequest = false;
      this.ClearInput = false;
      this.m_subStateMachine.SetState((IState) InGameStateEONNavigation.GetInstance());
    }

    public void Update(IFiniteStateMachine stateMachine, float elapsedTime)
    {
      if (this.BackToMainMenuRequest)
      {
        this.BackToMainMenuRequest = false;
        GameFSM.GetInstance().SetState((IState) GameStateMainMenu.GetInstance());
      }
      else
      {
        this.m_subStateMachine.Update(elapsedTime);
        if (this.m_subStateMachine.GetCurrentState() != InGameStateEONNavigation.GetInstance())
        {
          this.m_viewController.ButtonsEnabled = false;
          this.m_exitViewController.ButtonsEnabled = false;
        }
        else
        {
          this.m_viewController.ButtonsEnabled = true;
          this.m_exitViewController.ButtonsEnabled = true;
          if (this.ClearInput)
          {
            InputManager.GetInstance().ClearPadState();
            this.ClearInput = false;
          }
          else
          {
            switch (this.m_phase)
            {
              case 0:
                this.m_fadeTime += elapsedTime;
                if ((double) this.m_fadeTime < 1.0)
                  break;
                Application.GetInstance().AddResponder((Responder) this.m_viewController);
                ++this.m_phase;
                break;
              case 1:
                this.m_viewController.Update(elapsedTime);
                if (!this.m_viewController.IsDone())
                  break;
                Application.GetInstance().RemoveResponder((Responder) this.m_viewController);
                Application.GetInstance().AddResponder((Responder) this.m_exitViewController);
                ++this.m_phase;
                break;
              case 2:
                if (this.m_exitViewController.Request == EONExitConfirmationViewController.ExitRequest.Ok)
                {
                  Application.GetInstance().RemoveResponder((Responder) this.m_exitViewController);
                  this.m_fadeTime = 0.0f;
                  ++this.m_phase;
                  break;
                }
                if (this.m_exitViewController.Request != EONExitConfirmationViewController.ExitRequest.Back)
                  break;
                Application.GetInstance().RemoveResponder((Responder) this.m_exitViewController);
                Application.GetInstance().AddResponder((Responder) this.m_viewController);
                this.m_viewController.CancelIsDone();
                this.m_exitViewController.Request = EONExitConfirmationViewController.ExitRequest.None;
                this.m_phase = 1;
                break;
              case 3:
                this.m_fadeTime += elapsedTime;
                if ((double) this.m_fadeTime < 1.0)
                  break;
                GameFSM.GetInstance().SetState((IState) GameStateLoadLevel.GetInstance());
                ++this.m_phase;
                break;
            }
          }
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      Application.GetInstance().RemoveResponder((Responder) this.m_viewController);
      Application.GetInstance().RemoveResponder((Responder) this.m_exitViewController);
      this.m_subStateMachine.GetCurrentState().Exit((IFiniteStateMachine) this.m_subStateMachine);
      this.m_viewController.Cleanup();
      this.m_exitViewController.Cleanup();
      AudioManager.GetInstance().StopAll();
      AudioManager.GetInstance().UnloadMusic();
      InputManager.GetInstance().ClearPadState();
      GameWorld currentWorld = (GameWorld) GameWorldManager.GetInstance().GetCurrentWorld();
      currentWorld.GetHero().HitPoints = currentWorld.GetHero().GetHitPointsMax();
      currentWorld.GetHero().ManaPoints = currentWorld.GetHero().GetManaPointsMax();
      currentWorld.SaveGameData();
      Scenario.GetInstance().RaiseEventExitShop();
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      if (this.m_phase <= 1)
        this.m_viewController.Draw();
      else
        this.m_exitViewController.Draw();
      instance.Begin2DMode(false);
      if (this.m_phase == 0)
      {
        float a = MathHelper.Clamp((float) ((1.0 - (double) this.m_fadeTime / 1.0) * (double) byte.MaxValue), 0.0f, (float) byte.MaxValue);
        instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, new Color(0, 0, 0, (int) a));
      }
      else if (this.m_phase == 3)
      {
        float a = MathHelper.Clamp((float) ((double) this.m_fadeTime / 1.0 * (double) byte.MaxValue), 0.0f, (float) byte.MaxValue);
        instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, new Color(0, 0, 0, (int) a));
      }
      instance.End2DMode();
      ((IDrawableState) this.m_subStateMachine.GetCurrentState()).Draw((IFiniteStateMachine) this.m_subStateMachine);
    }

    private GameStateEndOfNight()
    {
      this.m_quadFade = new VertexPositionColor[4];
      QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, Color.White);
      this.m_viewController = new EndOfNightViewController();
      this.m_exitViewController = new EONExitConfirmationViewController();
      this.m_subStateMachine = new DrawableFiniteStateMachine();
      this.BackToMainMenuRequest = false;
      this.ClearInput = false;
    }
  }
}
