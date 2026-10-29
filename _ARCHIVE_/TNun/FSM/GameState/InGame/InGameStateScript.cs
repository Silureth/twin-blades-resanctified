// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.GameState.InGame.InGameStateScript
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.NPC.Butcher.State;
using PSS.Game.NunVsZombies.FSM.NPC.Necromancer.State;
using PSS.Game.NunVsZombies.FSM.NPC.Orb.State;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.Input;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.GameState.InGame
{
  public class InGameStateScript : IDrawableState, IState
  {
    private static InGameStateScript m_instance = new InGameStateScript();
    private bool m_spawn;
    public bool NextRequest;
    public bool SuicideRequest;
    public bool TextBoxIsActive;

    public static InGameStateScript Instance => InGameStateScript.m_instance;

    public static InGameStateScript GetInstance() => InGameStateScript.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      this.TextBoxIsActive = false;
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(true);
      this.m_spawn = currentWorld.IsZombieSpawnEnabled;
      if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
      {
        switch (currentWorld.GetBoss().GetBossType())
        {
          case NPCBoss.BossType.ButcherForm1:
          case NPCBoss.BossType.ButcherForm2:
            ((NPCButcher) currentWorld.GetBoss()).GetStateMachine().SetState((IState) NPCButcherStateIdle.GetInstance());
            currentWorld.IsZombieSpawnEnabled = false;
            break;
          case NPCBoss.BossType.NecromancerForm1:
            ((NPCNecromancer) currentWorld.GetBoss()).GetStateMachine().SetState((IState) NPCNecromancerStateIdle.GetInstance());
            break;
          case NPCBoss.BossType.NecromancerForm2:
            ((NPCNecromancer) currentWorld.GetBoss()).GetStateMachine().SetState((IState) NPCNecromancerStateIdle.GetInstance());
            currentWorld.GetMagicOrb().GetStateMachine().SetState((IState) MagicOrbStateIdle.GetInstance());
            break;
        }
      }
      this.NextRequest = false;
      this.SuicideRequest = false;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (InputManager.GetInstance().IsBackPressed())
      {
        this.TextBoxIsActive = GameApplication.GetInstance().GetTextBoxViewController().IsActive();
        GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
        stateMachine.SetState((IState) InGameStateMenu.GetInstance());
      }
      else
      {
        GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
        currentWorld.Update(gameTime);
        if (currentWorld.IsZombieSpawnEnabled)
          this.m_spawn = true;
        if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Script)
        {
          if (this.NextRequest)
          {
            this.NextRequest = false;
            currentWorld.LevelIndex = GameWorld.LevelId.Level_Shop;
            stateMachine.SetState((IState) InGameStateEndLevelEpilogOpenMap.GetInstance());
          }
          else
          {
            if (!this.SuicideRequest)
              return;
            this.SuicideRequest = false;
            stateMachine.SetState((IState) InGameStateEndLevelEpilogSuicide.GetInstance());
          }
        }
        else
        {
          if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
            return;
          stateMachine.SetState((IState) InGameStatePlay.GetInstance());
        }
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(false);
      currentWorld.IsZombieSpawnEnabled = this.m_spawn;
      if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Boss)
        return;
      switch (currentWorld.GetBoss().GetBossType())
      {
        case NPCBoss.BossType.ButcherForm1:
        case NPCBoss.BossType.ButcherForm2:
          ((NPCButcher) currentWorld.GetBoss()).GetStateMachine().SetState((IState) NPCButcherStateStand.GetInstance());
          break;
        case NPCBoss.BossType.NecromancerForm1:
          ((NPCNecromancer) currentWorld.GetBoss()).GetStateMachine().SetState((IState) NPCNecromancerStateStand.GetInstance());
          break;
        case NPCBoss.BossType.NecromancerForm2:
          ((NPCNecromancer) currentWorld.GetBoss()).GetStateMachine().SetState((IState) NPCNecromancerStateStand.GetInstance());
          currentWorld.GetMagicOrb().GetStateMachine().SetState((IState) MagicOrbStateStand.GetInstance());
          break;
      }
    }

    public void Draw(IFiniteStateMachine stateMachine)
    {
    }
  }
}
