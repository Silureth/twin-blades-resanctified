// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateGameOver
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.World;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Graphics.Renderer;
using PSS.GameEngine.Maths;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateGameOver : IDrawableState, IState
  {
    private static InGameStateGameOver m_instance = new InGameStateGameOver();
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;
    public bool DisableFade;

    public static InGameStateGameOver Instance => InGameStateGameOver.m_instance;

    public static InGameStateGameOver GetInstance() => InGameStateGameOver.m_instance;

    public InGameStateGameOver()
    {
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
      this.DisableFade = false;
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      GameApplication.GetInstance().Achievement_OneLife = false;
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetMiscAnimation(GameWorld.MiscAnimId.GameOver).SetTime(0.0f);
      QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, Color.White);
      GameApplication.GetInstance().CheckKillsAchievements();
      this.DisableFade = false;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).Update(gameTime);
      if (!currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).IsEndOfAnimationReached())
        return;
      GameStateInGame.GetInstance().HighscoresRequest = true;
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      GameApplication instance = GameApplication.GetInstance();
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      int score = instance.Score;
      currentWorld.GetHero().HitPoints = currentWorld.GetHero().GetHitPointsMax();
      currentWorld.GetHero().ManaPoints = currentWorld.GetHero().GetManaPointsMax();
      instance.Score = 0;
      currentWorld.SaveGameData();
      instance.Score = score;
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      float num = (float) byte.MaxValue;
      if (!this.DisableFade && stateMachine.GetCurrentStateTime() < 2.0)
        num = (float) (stateMachine.GetCurrentStateTime() / 2.0) * (float) byte.MaxValue;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      this.m_fadeColor.A = (byte) num;
      instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, this.m_fadeColor);
      instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.GameOver).GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha);
      instance.End2DMode();
    }
  }
}
