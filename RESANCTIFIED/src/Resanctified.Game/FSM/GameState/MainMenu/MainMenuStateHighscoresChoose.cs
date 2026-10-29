// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateHighscoresChoose
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateHighscoresChoose : IDrawableState, IState
  {
    private static MainMenuStateHighscoresChoose m_instance = new MainMenuStateHighscoresChoose();
    public bool BackRequest;
    public bool StoryRequest;
    public bool SurvivalRequest;

    public static MainMenuStateHighscoresChoose Instance => MainMenuStateHighscoresChoose.m_instance;

    public static MainMenuStateHighscoresChoose GetInstance() => MainMenuStateHighscoresChoose.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.StoryRequest = false;
      this.SurvivalRequest = false;
      Application.GetInstance().AddResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresChooseView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
        this.BackRequest = true;
      if (this.BackRequest)
      {
        stateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Previous);
      }
      else if (this.StoryRequest)
      {
        stateMachine.SetState((IState) MainMenuStateHighscoresStory.GetInstance());
        GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = true;
        GameStateMainMenu.GetInstance().GetLeaderboardView().StoryMode = true;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
      else
      {
        if (!this.SurvivalRequest)
          return;
        stateMachine.SetState((IState) MainMenuStateHighscoresSurvival.GetInstance());
        GameStateMainMenu.GetInstance().GetHighscoresView().StoryMode = false;
        GameStateMainMenu.GetInstance().GetLeaderboardView().StoryMode = false;
        GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.BackRequest = false;
      this.StoryRequest = false;
      this.SurvivalRequest = false;
      Application.GetInstance().RemoveResponder((Responder) GameStateMainMenu.GetInstance().GetHighscoresChooseView());
    }

    public void Draw(IFiniteStateMachine stateMachine) => GameStateMainMenu.GetInstance().GetHighscoresChooseView().Draw();
  }
}
