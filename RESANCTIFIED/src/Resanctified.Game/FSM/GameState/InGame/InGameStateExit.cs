// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameStateExit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Maths;

namespace Resanctified.Game.FSM.GameState.InGame
{
  public class InGameStateExit : IDrawableState, IState
  {
    private static InGameStateExit m_instance = new InGameStateExit();
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;

    public static InGameStateExit Instance => InGameStateExit.m_instance;

    public static InGameStateExit GetInstance() => InGameStateExit.m_instance;

    public InGameStateExit()
    {
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
    }

    public void Enter(IFiniteStateMachine stateMachine) => QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (stateMachine.GetCurrentStateTime() < 2.0)
        return;
      if (GameFSM.GetInstance().GetCurrentState() == GameStateInGame.GetInstance())
        GameStateInGame.Instance.BackToMainMenuRequest = true;
      else if (GameFSM.GetInstance().GetCurrentState() == GameStateMap.GetInstance())
      {
        GameStateMap.Instance.BackToMainMenuRequest = true;
      }
      else
      {
        if (GameFSM.GetInstance().GetCurrentState() != GameStateEndOfNight.GetInstance())
          return;
        GameStateEndOfNight.Instance.BackToMainMenuRequest = true;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
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
