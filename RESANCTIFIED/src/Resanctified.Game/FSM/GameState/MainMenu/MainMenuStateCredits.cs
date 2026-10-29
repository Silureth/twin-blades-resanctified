// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateCredits
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateCredits : IDrawableState, IState
  {
    private float m_timer;
    public bool BackRequest;
    private static MainMenuStateCredits m_instance = new MainMenuStateCredits();

    public static MainMenuStateCredits Instance => MainMenuStateCredits.m_instance;

    public static MainMenuStateCredits GetInstance() => MainMenuStateCredits.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.m_timer = 4f;
      GameStateMainMenu.GetInstance().GetCreditsView().SetPage(0);
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetCreditsView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      this.m_timer -= gameTime;
      if ((double) this.m_timer <= 0.0)
      {
        this.m_timer = 4f;
        if (!GameStateMainMenu.GetInstance().GetCreditsView().NextPage())
        {
          stateMachine.SetState((IState) MainMenuStateHelpAndOptions.GetInstance());
          return;
        }
      }
      if (!this.BackRequest)
        return;
      stateMachine.SetState((IState) MainMenuStateHelpAndOptions.GetInstance());
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetCreditsView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetCreditsView().Draw();
  }
}
