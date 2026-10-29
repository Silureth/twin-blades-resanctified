// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateHelpMap
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateHelpMap : IDrawableState, IState
  {
    private static InGameStateHelpMap m_instance = new InGameStateHelpMap();
    public bool NextRequest;

    public static InGameStateHelpMap Instance => InGameStateHelpMap.m_instance;

    public static InGameStateHelpMap GetInstance() => InGameStateHelpMap.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.NextRequest = false;
      GameApplication.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHelpMapView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      GameApplication.GetInstance().GetInGameHelpMapView().Update(gameTime);
      if (InputManager.GetInstance().IsBackPressed())
        this.NextRequest = true;
      if (!this.NextRequest)
        return;
      this.NextRequest = false;
      stateMachine.SetState((IState) InGameMenuStateHelpAndOptions.GetInstance());
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
    }

    public void Exit(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHelpMapView());

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpMapView().Draw();
  }
}
