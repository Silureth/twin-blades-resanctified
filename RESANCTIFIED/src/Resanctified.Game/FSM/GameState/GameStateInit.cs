// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.GameStateInit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.GameState
{
  public class GameStateInit : IDrawableState, IState
  {
    private static readonly GameStateInit s_instance = new GameStateInit();

    public static GameStateInit Instance => GameStateInit.s_instance;

    public static GameStateInit GetInstance() => GameStateInit.s_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      GameApplication.Instance.LoadSettings();
      GameApplication.Instance.SetSoundVolume();
      GameLocalisation.GetInstance().Initialize(GameApplication.Instance.CurrentLanguage);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime) => stateMachine.SetState((IState) GameStateLogoMGS.Instance);

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine) => Application.Instance.GraphicsDevice.Clear(Color.Black);
  }
}
