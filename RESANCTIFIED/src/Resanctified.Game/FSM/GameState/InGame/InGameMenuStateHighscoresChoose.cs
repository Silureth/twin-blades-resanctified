// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameMenuStateHighscoresChoose
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.InGame
{
  public class InGameMenuStateHighscoresChoose : IDrawableState, IState
  {
    private static InGameMenuStateHighscoresChoose m_instance = new InGameMenuStateHighscoresChoose();
    public bool BackRequest;
    public bool StoryRequest;
    public bool SurvivalRequest;

    public static InGameMenuStateHighscoresChoose Instance => InGameMenuStateHighscoresChoose.m_instance;

    public static InGameMenuStateHighscoresChoose GetInstance() => InGameMenuStateHighscoresChoose.m_instance;

    public InGameMenuStateHighscoresChoose()
    {
      this.BackRequest = false;
      this.StoryRequest = false;
      this.SurvivalRequest = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.StoryRequest = false;
      this.SurvivalRequest = false;
      Application.GetInstance().AddResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresChooseView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (this.BackRequest)
      {
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
        stateMachine.SetState((IState) InGameStateMenu.GetInstance());
      }
      else if (this.StoryRequest)
      {
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
        stateMachine.SetState((IState) InGameMenuStateHighscoresStory.GetInstance());
        GameApplication.GetInstance().GetInGameHighscoresView().StoryMode = true;
      }
      else
      {
        if (!this.SurvivalRequest)
          return;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
        stateMachine.SetState((IState) InGameMenuStateHighscoresSurvival.GetInstance());
        GameApplication.GetInstance().GetInGameHighscoresView().StoryMode = false;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.StoryRequest = false;
      this.SurvivalRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameApplication.GetInstance().GetInGameHighscoresChooseView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameApplication.GetInstance().GetInGameHighscoresChooseView().Draw();
  }
}
