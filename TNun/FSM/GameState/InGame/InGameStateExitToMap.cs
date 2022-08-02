// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateExitToMap
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Maths;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateExitToMap : IDrawableState, IState
  {
    private static InGameStateExitToMap m_instance = new InGameStateExitToMap();
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;

    public static InGameStateExitToMap Instance => InGameStateExitToMap.m_instance;

    public static InGameStateExitToMap GetInstance() => InGameStateExitToMap.m_instance;

    public InGameStateExitToMap()
    {
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
    }

    public void Enter(IFiniteStateMachine stateMachine) => QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (stateMachine.GetCurrentStateTime() < 2.0)
        return;
      GameStateInGame.Instance.BackToMap = true;
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      float num = (float) byte.MaxValue;
      if (stateMachine.GetCurrentStateTime() < 2.0)
        num = (float) (stateMachine.GetCurrentStateTime() / 2.0) * (float) byte.MaxValue;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      this.m_fadeColor.A = (byte) num;
      instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, this.m_fadeColor);
      instance.End2DMode();
    }
  }
}
