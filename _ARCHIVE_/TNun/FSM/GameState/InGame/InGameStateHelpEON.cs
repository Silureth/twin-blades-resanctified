// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateHelpEON
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateHelpEON : IDrawableState, IState
  {
    private static InGameStateHelpEON m_instance = new InGameStateHelpEON();
    public bool NextRequest;

    public static InGameStateHelpEON Instance => InGameStateHelpEON.m_instance;

    public static InGameStateHelpEON GetInstance() => InGameStateHelpEON.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.NextRequest = false;
      GameApplication.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHelpEONView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      GameApplication.GetInstance().GetInGameHelpEONView().Update(gameTime);
      if (InputManager.GetInstance().IsBackPressed())
        this.NextRequest = true;
      if (!this.NextRequest)
        return;
      this.NextRequest = false;
      stateMachine.SetState((IState) InGameMenuStateHelpAndOptions.GetInstance());
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
    }

    public void Exit(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHelpEONView());

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHelpEONView().Draw();
  }
}
