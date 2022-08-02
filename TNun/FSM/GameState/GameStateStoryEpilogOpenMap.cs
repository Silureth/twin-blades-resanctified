// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.GameStateStoryEpilogOpenMap
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.UI;
using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState
{
  public class GameStateStoryEpilogOpenMap : IDrawableState, IState
  {
    private static GameStateStoryEpilogOpenMap m_instance = new GameStateStoryEpilogOpenMap();
    private StoryEpilogViewController m_storyViewController;
    private VertexPositionColor[] m_quadFade;
    public bool NextRequest;

    public static GameStateStoryEpilogOpenMap Instance => GameStateStoryEpilogOpenMap.m_instance;

    public static GameStateStoryEpilogOpenMap GetInstance() => GameStateStoryEpilogOpenMap.m_instance;

    public GameStateStoryEpilogOpenMap()
    {
      this.m_storyViewController = new StoryEpilogViewController();
      this.m_quadFade = new VertexPositionColor[4];
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.NextRequest = false;
      this.m_storyViewController.Initialize(false);
      AudioManager.GetInstance().LoadMusic("Sounds\\music_splash");
      ResourceManager.GetInstance().LoadAllResources();
      GameApplication.GetInstance().SetSoundVolume();
      AudioManager.GetInstance().PlayMusic();
      Application.GetInstance().AddResponder((Responder) this.m_storyViewController);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      this.m_storyViewController.Update(gameTime);
      if (!this.NextRequest)
        return;
      this.NextRequest = false;
      stateMachine.SetState((IState) GameStateMap.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      AudioManager.GetInstance().StopAll();
      AudioManager.GetInstance().UnloadMusic();
      InputManager.GetInstance().ClearPadState();
      this.m_storyViewController.Cleanup();
      ResourceManager.GetInstance().DestroyUnusedResources();
      Application.GetInstance().RemoveResponder((Responder) this.m_storyViewController);
    }

    public void Draw(IFiniteStateMachine stateMachine) => this.m_storyViewController.Draw();
  }
}
