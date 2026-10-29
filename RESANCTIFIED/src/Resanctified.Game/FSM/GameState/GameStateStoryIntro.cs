// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.GameStateStoryIntro
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.UI;
using Resanctified.Game.World;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;

namespace Resanctified.Game.FSM.GameState
{
  public class GameStateStoryIntro : IDrawableState, IState
  {
    private static GameStateStoryIntro m_instance = new GameStateStoryIntro();
    private StoryIntroViewController m_storyViewController;
    private VertexPositionColor[] m_quadFade;
    public bool NextRequest;

    public static GameStateStoryIntro Instance => GameStateStoryIntro.m_instance;

    public static GameStateStoryIntro GetInstance() => GameStateStoryIntro.m_instance;

    public GameStateStoryIntro()
    {
      this.NextRequest = false;
      this.m_storyViewController = new StoryIntroViewController();
      this.m_quadFade = new VertexPositionColor[4];
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.NextRequest = false;
      this.m_storyViewController.Initialize();
      ResourceManager.GetInstance().LoadAllResources();
      Application.GetInstance().AddResponder((Responder) this.m_storyViewController);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
      {
        WorldManager.GetInstance().Cleanup(false, true);
        GameFSM.GetInstance().SetState((IState) GameStateMainMenu.GetInstance());
      }
      else
      {
        this.m_storyViewController.Update(gameTime);
        if (!this.NextRequest)
          return;
        this.NextRequest = false;
        GameWorld currentWorld = (GameWorld) GameWorldManager.GetInstance().GetCurrentWorld();
        currentWorld.LevelIndex = GameWorld.LevelId.Level_Shop;
        Scenario.GetInstance().RaiseEventSelectLevel(currentWorld.LevelIndex);
        stateMachine.SetState((IState) GameStateLoadLevel.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      InputManager.GetInstance().ClearPadState();
      this.m_storyViewController.Cleanup();
      Application.GetInstance().RemoveResponder((Responder) this.m_storyViewController);
      ResourceManager.GetInstance().DestroyUnusedResources();
    }

    public void Draw(IFiniteStateMachine stateMachine) => this.m_storyViewController.Draw();
  }
}
