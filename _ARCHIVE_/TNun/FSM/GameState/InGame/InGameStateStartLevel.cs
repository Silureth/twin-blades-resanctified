// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateStartLevel
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.World;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Graphics.Renderer;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateStartLevel : IDrawableState, IState
  {
    private const float TEXT_NIGHT_0_OFFSET_X = 50f;
    private const float TEXT_DIGIT_0_OFFSET_X = 170f;
    public bool DayDisplayRequest;
    public bool InGameMenuRequest;
    private VertexPositionColor[] m_quadFade;
    private Color m_fadeColor;
    private Transform m_t = new Transform();
    private static InGameStateStartLevel m_instance = new InGameStateStartLevel();

    public static InGameStateStartLevel Instance => InGameStateStartLevel.m_instance;

    public static InGameStateStartLevel GetInstance() => InGameStateStartLevel.m_instance;

    public InGameStateStartLevel()
    {
      this.DayDisplayRequest = false;
      this.m_quadFade = new VertexPositionColor[4];
      this.m_fadeColor = new Color(0, 0, 0, (int) byte.MaxValue);
    }

    public void Enter(IFiniteStateMachine stateMachine)
    {
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
      {
        GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
        currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).SetTime(0.0f);
        currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).SetTime(0.0f);
        currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).SetTime(0.0f);
      }
      QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, Color.White);
      InputManager.GetInstance().ClearPadState();
      this.InGameMenuRequest = false;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      currentWorld.GetCamera().Update(gameTime);
      currentWorld.GetEntityManager().Update(gameTime);
      currentWorld.ConstraintToZone();
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
      {
        if (stateMachine.GetCurrentStateTime() < 2.0)
          return;
        if (Scenario.GetInstance().DisplayHelp)
        {
          stateMachine.SetState((IState) InGameStateHelp.GetInstance());
        }
        else
        {
          bool inGameMenuRequest = this.InGameMenuRequest;
          stateMachine.SetState((IState) InGameStatePlay.GetInstance());
          if (!inGameMenuRequest)
            return;
          InGameStatePlay.GetInstance().InGameMenuRequest = true;
        }
      }
      else if ((currentWorld.LevelIndex == GameWorld.LevelId.Level_Survival_1 || this.DayDisplayRequest) && GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
      {
        currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).Update(gameTime);
        currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).Update(gameTime);
        currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).Update(gameTime);
        if (!currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).IsEndOfAnimationReached())
          return;
        if (currentWorld.DayIndex == 1 && currentWorld.LevelIndex == GameWorld.LevelId.Level_Survival_1)
        {
          stateMachine.SetState((IState) InGameStateHelp.GetInstance());
        }
        else
        {
          bool inGameMenuRequest = this.InGameMenuRequest;
          stateMachine.SetState((IState) InGameStatePlay.GetInstance());
          if (!inGameMenuRequest)
            return;
          InGameStatePlay.GetInstance().InGameMenuRequest = true;
        }
      }
      else
      {
        if (stateMachine.GetCurrentStateTime() < 2.0)
          return;
        bool inGameMenuRequest = this.InGameMenuRequest;
        stateMachine.SetState((IState) InGameStatePlay.GetInstance());
        if (!inGameMenuRequest)
          return;
        InGameStatePlay.GetInstance().InGameMenuRequest = true;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      this.DayDisplayRequest = false;
      this.InGameMenuRequest = false;
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      float num = 0.0f;
      if (stateMachine.GetCurrentStateTime() < 2.0)
        num = (float) (1.0 - stateMachine.GetCurrentStateTime() / 2.0) * (float) byte.MaxValue;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      this.m_fadeColor.A = (byte) num;
      instance.DrawQuad<VertexPositionColor>(this.m_quadFade, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, this.m_fadeColor);
      if ((currentWorld.LevelIndex == GameWorld.LevelId.Level_Survival_1 || this.DayDisplayRequest) && GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
      {
        float x = 0.0f;
        if (currentWorld.DayIndex < 10)
          x = 50f;
        this.m_t.SetPosition(x, 0.0f, 0.0f);
        instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha);
        if (currentWorld.DayIndex >= 10)
        {
          instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha);
          x += 170f;
        }
        this.m_t.SetPosition(x, 0.0f, 0.0f);
        instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha);
      }
      instance.End2DMode();
    }
  }
}
