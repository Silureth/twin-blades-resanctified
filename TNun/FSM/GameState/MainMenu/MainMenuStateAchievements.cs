// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.MainMenu.MainMenuStateAchievements
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.MainMenu
{
  public class MainMenuStateAchievements : IDrawableState, IState
  {
    private static MainMenuStateAchievements m_instance = new MainMenuStateAchievements();
    public bool BackRequest;

    public static MainMenuStateAchievements Instance => MainMenuStateAchievements.m_instance;

    public static MainMenuStateAchievements GetInstance() => MainMenuStateAchievements.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      AchievementManager.Instance.SynchronizeAchievements(false);
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetAchievementsView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (!this.BackRequest)
        return;
      stateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetAchievementsView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetAchievementsView().Draw();
  }
}
