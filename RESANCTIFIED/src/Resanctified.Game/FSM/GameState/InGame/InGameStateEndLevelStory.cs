// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.GameState.InGame.InGameStateEndLevelStory
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
  public class InGameStateEndLevelStory : IDrawableState, IState
  {
    private static InGameStateEndLevelStory m_instance = new InGameStateEndLevelStory();
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;
    public float m_timer;
    private int m_state;

    public static InGameStateEndLevelStory Instance => InGameStateEndLevelStory.m_instance;

    public static InGameStateEndLevelStory GetInstance() => InGameStateEndLevelStory.m_instance;

    public InGameStateEndLevelStory()
    {
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, this.m_fadeColor);
      GameApplication.GetInstance().CheckKillsAchievements();
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      this.m_timer = 0.0f;
      this.m_state = 0;
      currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(true);
      if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Normal)
        return;
      this.m_state = 1;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      currentWorld.Update(gameTime);
      if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
        return;
      switch (this.m_state)
      {
        case 0:
          if (stateMachine.CurrentStateTime < 2.0)
            break;
          ++this.m_state;
          break;
        case 1:
          Scenario.GetInstance().RaiseEventEndLevel(currentWorld.LevelIndex);
          ++this.m_state;
          break;
        case 2:
          this.m_timer += gameTime;
          if ((double) this.m_timer < 2.0)
            break;
          currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(false);
          GameStateInGame.GetInstance().NextLevelRequest = true;
          break;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      if (this.m_state != 2)
        return;
      float num = (float) byte.MaxValue;
      if ((double) this.m_timer < 2.0)
        num = this.m_timer / 2f * (float) byte.MaxValue;
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      this.m_fadeColor.A = (byte) num;
      instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (Resanctified.Engine.Resources.Texture) null, this.m_fadeColor);
      instance.End2DMode();
    }
  }
}
