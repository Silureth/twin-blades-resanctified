// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameStateEndLevelEpilogOpenMap
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Game.World;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Maths;
using Resanctified.Engine.World;

namespace Resanctified.Game.FSM.GameState.InGame
{
  public class InGameStateEndLevelEpilogOpenMap : IDrawableState, IState
  {
    private static InGameStateEndLevelEpilogOpenMap m_instance = new InGameStateEndLevelEpilogOpenMap();
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;

    public static InGameStateEndLevelEpilogOpenMap Instance => InGameStateEndLevelEpilogOpenMap.m_instance;

    public static InGameStateEndLevelEpilogOpenMap GetInstance() => InGameStateEndLevelEpilogOpenMap.m_instance;

    public InGameStateEndLevelEpilogOpenMap()
    {
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero().GetStateMachine().SetGameActionsDisabled(true);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      currentWorld.Update(gameTime);
      if (stateMachine.CurrentStateTime < 2.0)
        return;
      currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(false);
      GameFSM.GetInstance().SetState((IState) GameStateStoryEpilogOpenMap.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      float num = stateMachine.CurrentStateTime >= 2.0 ? (float) byte.MaxValue : (float) (stateMachine.CurrentStateTime / 2.0) * (float) byte.MaxValue;
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      this.m_fadeColor.A = (byte) num;
      instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, this.m_fadeColor);
      instance.End2DMode();
    }
  }
}
