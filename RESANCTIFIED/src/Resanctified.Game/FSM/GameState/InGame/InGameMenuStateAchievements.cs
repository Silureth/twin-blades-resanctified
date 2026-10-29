// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameMenuStateAchievements
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.InGame
{
  public class InGameMenuStateAchievements : IDrawableState, IState
  {
    private static InGameMenuStateAchievements m_instance = new InGameMenuStateAchievements();
    public bool BackRequest;

    public static InGameMenuStateAchievements Instance => InGameMenuStateAchievements.m_instance;

    public static InGameMenuStateAchievements GetInstance() => InGameMenuStateAchievements.m_instance;

    public InGameMenuStateAchievements() => this.BackRequest = false;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameAchievementsView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (!this.BackRequest)
        return;
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      stateMachine.SetState((IState) InGameStateMenu.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameAchievementsView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameAchievementsView().Draw();
  }
}
