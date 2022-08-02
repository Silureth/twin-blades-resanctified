// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.GameStateEndingAnimation
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.FSM;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Graphics.Renderer;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Sounds;

namespace PSS.Game.NunVsZombies.FSM.GameState
{
  public class GameStateEndingAnimation : IDrawableState, IState
  {
    private static GameStateEndingAnimation m_instance = new GameStateEndingAnimation();
    private Animation2D m_anim;

    public static GameStateEndingAnimation Instance => GameStateEndingAnimation.m_instance;

    public static GameStateEndingAnimation GetInstance() => GameStateEndingAnimation.m_instance;

    public GameStateEndingAnimation() => this.m_anim = new Animation2D();

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.m_anim.Initialize("Anims\\endofgame");
      this.m_anim.SetLoop(Animation2DLoop.None);
      this.m_anim.SetSpeed(1f);
      AudioManager.GetInstance().LoadMusic("Sounds\\cutscene", true);
      AudioManager.GetInstance().SetMusicLoopCount(1);
      ResourceManager.GetInstance().LoadAllResources();
      AudioManager.GetInstance().PlayMusic();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      this.m_anim.Update(gameTime);
      if (!this.m_anim.IsEndOfAnimationReached())
        return;
      GameApplication.GetInstance().CheckBossNecro2KillAchievement();
      if (Scenario.GetInstance().GetCurrentMissionIndex() == 99)
        GameFSM.GetInstance().SetState((IState) GameStateMap.GetInstance());
      else
        GameFSM.GetInstance().SetState((IState) GameStateEndingStory.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().ClearTopLevelViewList();
      AudioManager.GetInstance().StopAll();
      AudioManager.GetInstance().UnloadMusic();
      AudioManager.GetInstance().SetMusicLoopCount(-1);
      InputManager.GetInstance().ClearPadState();
      this.m_anim.Cleanup();
      ResourceManager.GetInstance().DestroyUnusedResources();
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(true);
      instance.DrawAnimation2DModel(this.m_anim.GetModel(), this.m_anim.GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha);
      instance.End2DMode();
    }
  }
}
