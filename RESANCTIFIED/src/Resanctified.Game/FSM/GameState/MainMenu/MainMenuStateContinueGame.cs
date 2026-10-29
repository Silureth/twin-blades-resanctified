// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.MainMenu.MainMenuStateContinueGame
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;

namespace Resanctified.Game.FSM.GameState.MainMenu
{
  public class MainMenuStateContinueGame : IDrawableState, IState
  {
    private static MainMenuStateContinueGame m_instance = new MainMenuStateContinueGame();
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;

    public static MainMenuStateContinueGame Instance => MainMenuStateContinueGame.m_instance;

    public static MainMenuStateContinueGame GetInstance() => MainMenuStateContinueGame.m_instance;

    public MainMenuStateContinueGame()
    {
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
    }

    public void Enter(IFiniteStateMachine stateMachine) => QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
      {
        stateMachine.SetState((IState) MainMenuStateMenu.GetInstance());
      }
      else
      {
        if (stateMachine.GetCurrentStateTime() < 2.0)
          return;
        GameStateMainMenu.Instance.StartGameRequest = true;
        GameStateMainMenu.Instance.ContinueGameRequest = true;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      GameStateMainMenu.GetInstance().GetGameModeView().Draw();
      float num = (float) byte.MaxValue;
      if (stateMachine.GetCurrentStateTime() < 2.0)
        num = (float) (stateMachine.GetCurrentStateTime() / 2.0) * (float) byte.MaxValue;
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      this.m_fadeColor.A = (byte) num;
      instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, this.m_fadeColor);
      instance.End2DMode();
    }
  }
}
