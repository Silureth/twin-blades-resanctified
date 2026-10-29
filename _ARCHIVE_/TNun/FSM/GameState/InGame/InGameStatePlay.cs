// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStatePlay
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World;
using PSS.GameEngine;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStatePlay : IDrawableState, IState
  {
    public bool InGameMenuRequest;
    public bool GameOverRequest;
    private static InGameStatePlay m_instance = new InGameStatePlay();

    public static InGameStatePlay Instance => InGameStatePlay.m_instance;

    public static InGameStatePlay GetInstance() => InGameStatePlay.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.InGameMenuRequest = false;
      this.GameOverRequest = false;
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      Application.GetInstance().AddResponder((Responder) currentWorld.GetHud());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
      {
        stateMachine.SetState((IState) InGameStateScript.GetInstance());
      }
      else
      {
        GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
        currentWorld.Update(gameTime);
        if (this.GameOverRequest)
        {
          this.GameOverRequest = false;
          stateMachine.SetState((IState) InGameStateGameOver.GetInstance());
        }
        else
        {
          if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
          {
            if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
            {
              if (currentWorld.IsShopEnabled && currentWorld.GetNPCShop().EnterShopRequest)
              {
                currentWorld.GetNPCShop().EnterShopRequest = false;
                GameStateInGame.GetInstance().ShopRequest = true;
                return;
              }
              if (currentWorld.IsShopExitEnabled && (double) currentWorld.GetHero().GetTransform().GetPosition().X <= -0.5)
              {
                stateMachine.SetState((IState) InGameStateEndLevel.GetInstance());
                return;
              }
            }
            else if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
              if (currentWorld.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
              {
                if (currentWorld.GetBoss().IsDead())
                  Scenario.GetInstance().RaiseEventEndLevel(currentWorld.LevelIndex);
              }
              else if (currentWorld.GetBoss().IsDead() && currentWorld.ZombiesAliveNb <= 0)
              {
                stateMachine.SetState((IState) InGameStateEndLevelStory.GetInstance());
                return;
              }
            }
            else if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Normal)
            {
              if ((double) currentWorld.GetHero().GetTransform().GetPosition().X < 8.0 || currentWorld.ZombiesLeftToKill <= 0 && currentWorld.ZombiesAliveNb <= 0)
                currentWorld.GetHud().ShowRightArrow(true);
              else
                currentWorld.GetHud().ShowRightArrow(false);
              if (currentWorld.ZombiesLeftToKill <= 0 && currentWorld.ZombiesAliveNb <= 0 && (double) currentWorld.GetHero().GetTransform().GetPosition().X >= (double) currentWorld.EndOfLevelPosX)
              {
                stateMachine.SetState((IState) InGameStateEndLevelStory.GetInstance());
                return;
              }
            }
          }
          else if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
          {
            if ((double) currentWorld.GetHero().GetTransform().GetPosition().X < 8.0 || (double) currentWorld.GetHero().GetTransform().GetPosition().X > (double) currentWorld.EndOfLevelPosX - 8.0)
              currentWorld.GetHud().ShowRightArrow(true);
            else
              currentWorld.GetHud().ShowRightArrow(false);
            if ((double) currentWorld.GetHero().GetTransform().GetPosition().X >= (double) currentWorld.EndOfLevelPosX)
            {
              stateMachine.SetState((IState) InGameStateEndLevel.GetInstance());
              return;
            }
          }
          if (InputManager.GetInstance().IsBackPressed())
            this.InGameMenuRequest = true;
          if (!this.InGameMenuRequest)
            return;
          this.InGameMenuRequest = false;
          AudioManager.GetInstance().PauseAll();
          stateMachine.SetState((IState) InGameStateMenu.GetInstance());
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      Application.GetInstance().RemoveResponder((Responder) currentWorld.GetHud());
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
    }
  }
}
