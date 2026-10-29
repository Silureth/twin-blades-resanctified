// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameMenuStateHighscoresStory
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.InGame
{
  // Shows the local highscores only; the Xbox Live leaderboard pages of the phone version are gone.
  public class InGameMenuStateHighscoresStory : IDrawableState, IState
  {
    private static InGameMenuStateHighscoresStory m_instance = new InGameMenuStateHighscoresStory();
    public bool BackRequest;

    public static InGameMenuStateHighscoresStory Instance => InGameMenuStateHighscoresStory.m_instance;

    public static InGameMenuStateHighscoresStory GetInstance() => InGameMenuStateHighscoresStory.m_instance;

    public InGameMenuStateHighscoresStory() => this.BackRequest = false;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      GameApplication.GetInstance().GetInGameHighscoresView().Update(gameTime);
      if (!this.BackRequest)
        return;
      GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      stateMachine.SetState((IState) InGameMenuStateHighscoresChoose.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHighscoresView().Draw();
  }
}
