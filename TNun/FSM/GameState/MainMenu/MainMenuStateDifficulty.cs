// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.MainMenu.MainMenuStateDifficulty
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.GameState.MainMenu
{
  public class MainMenuStateDifficulty : IDrawableState, IState
  {
    private static MainMenuStateDifficulty m_instance = new MainMenuStateDifficulty();
    public bool BackRequest;
    public bool StartGameRequest;

    public static MainMenuStateDifficulty Instance => MainMenuStateDifficulty.m_instance;

    public static MainMenuStateDifficulty GetInstance() => MainMenuStateDifficulty.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.StartGameRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetDifficultyView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (this.BackRequest)
      {
        stateMachine.SetState((IState) MainMenuStateGameMode.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else
      {
        if (!this.StartGameRequest)
          return;
        stateMachine.SetState((IState) MainMenuStateStartGame.GetInstance());
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.StartGameRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetDifficultyView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetDifficultyView().Draw();
  }
}
