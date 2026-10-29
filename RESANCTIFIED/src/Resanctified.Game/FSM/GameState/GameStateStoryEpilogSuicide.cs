// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.GameStateStoryEpilogSuicide
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.FSM.GameState.MainMenu;
using Resanctified.Game.UI;
using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;

namespace Resanctified.Game.FSM.GameState
{
  public class GameStateStoryEpilogSuicide : IDrawableState, IState
  {
    private static GameStateStoryEpilogSuicide m_instance = new GameStateStoryEpilogSuicide();
    private StoryEpilogViewController m_storyViewController;
    private VertexPositionColor[] m_quadFade;
    public bool NextRequest;

    public static GameStateStoryEpilogSuicide Instance => GameStateStoryEpilogSuicide.m_instance;

    public static GameStateStoryEpilogSuicide GetInstance() => GameStateStoryEpilogSuicide.m_instance;

    public GameStateStoryEpilogSuicide()
    {
      this.m_storyViewController = new StoryEpilogViewController();
      this.m_quadFade = new VertexPositionColor[4];
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.NextRequest = false;
      this.m_storyViewController.Initialize(true);
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
      GameFSM.GetInstance().SetState((IState) GameStateMainMenu.GetInstance());
      GameApplication instance = GameApplication.GetInstance();
      GameStateMainMenu.GetInstance().GetSubStateMachine().SetState((IState) MainMenuStateHighscoresStory.GetInstance());
      GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = true;
      int num = instance.UpdateHighscores();
      GameStateMainMenu.GetInstance().GetHighscoresView().BlinkScore(num + 1);
      if (num >= 5)
        return;
      GameApplication.Instance.SaveHighscores();
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      WorldManager.GetInstance().Cleanup(false, true);
      AudioManager.GetInstance().StopAll();
      AudioManager.GetInstance().UnloadMusic();
      InputManager.GetInstance().ClearPadState();
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
