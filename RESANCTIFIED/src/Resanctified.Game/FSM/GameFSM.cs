// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameFSM
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.GameState;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM
{
  public class GameFSM : DrawableFiniteStateMachine
  {
    private static GameFSM m_instance = new GameFSM();

    public static GameFSM Instance => GameFSM.m_instance;

    public static GameFSM GetInstance() => GameFSM.m_instance;

    public override void Initialize() => this.SetState((IState) GameStateInit.Instance);
  }
}
