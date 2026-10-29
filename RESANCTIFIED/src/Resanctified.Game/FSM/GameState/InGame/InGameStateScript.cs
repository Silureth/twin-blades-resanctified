using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.World;
using Resanctified.Game.FSM.NPC.Butcher.State;
using Resanctified.Game.FSM.NPC.Necromancer.State;
using Resanctified.Game.FSM.NPC.Orb.State;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateScript : IDrawableState, IState
    {
        private static InGameStateScript m_instance = new InGameStateScript();
        private bool m_spawn;
        public bool NextRequest;
        public bool SuicideRequest;
        public bool TextBoxIsActive;

        public static InGameStateScript Instance => m_instance;

        public static InGameStateScript GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.TextBoxIsActive = false;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(true);
            this.m_spawn = currentWorld.IsZombieSpawnEnabled;
            if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
            {
                switch (currentWorld.GetBoss().GetBossType())
                {
                    case NPCBoss.BossType.ButcherForm1:
                    case NPCBoss.BossType.ButcherForm2:
                        ((NPCButcher)currentWorld.GetBoss()).GetStateMachine().SetState(NPCButcherStateIdle.GetInstance());
                        currentWorld.IsZombieSpawnEnabled = false;
                        break;
                    case NPCBoss.BossType.NecromancerForm1:
                        ((NPCNecromancer)currentWorld.GetBoss()).GetStateMachine().SetState(NPCNecromancerStateIdle.GetInstance());
                        break;
                    case NPCBoss.BossType.NecromancerForm2:
                        ((NPCNecromancer)currentWorld.GetBoss()).GetStateMachine().SetState(NPCNecromancerStateIdle.GetInstance());
                        currentWorld.GetMagicOrb().GetStateMachine().SetState(MagicOrbStateIdle.GetInstance());
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
                stateMachine.SetState(InGameStateMenu.GetInstance());
            }
            else
            {
                GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
                currentWorld.Update(gameTime);
                if (currentWorld.IsZombieSpawnEnabled)
                    this.m_spawn = true;
                if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Script)
                {
                    if (this.NextRequest)
                    {
                        this.NextRequest = false;
                        currentWorld.LevelIndex = GameWorld.LevelId.Level_Shop;
                        stateMachine.SetState(InGameStateEndLevelEpilogOpenMap.GetInstance());
                    }
                    else
                    {
                        if (!this.SuicideRequest)
                            return;
                        this.SuicideRequest = false;
                        stateMachine.SetState(InGameStateEndLevelEpilogSuicide.GetInstance());
                    }
                }
                else
                {
                    if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
                        return;
                    stateMachine.SetState(InGameStatePlay.GetInstance());
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            currentWorld.GetHero().GetStateMachine().SetGameActionsDisabled(false);
            currentWorld.IsZombieSpawnEnabled = this.m_spawn;
            if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Boss)
                return;
            switch (currentWorld.GetBoss().GetBossType())
            {
                case NPCBoss.BossType.ButcherForm1:
                case NPCBoss.BossType.ButcherForm2:
                    ((NPCButcher)currentWorld.GetBoss()).GetStateMachine().SetState(NPCButcherStateStand.GetInstance());
                    break;
                case NPCBoss.BossType.NecromancerForm1:
                    ((NPCNecromancer)currentWorld.GetBoss()).GetStateMachine().SetState(NPCNecromancerStateStand.GetInstance());
                    break;
                case NPCBoss.BossType.NecromancerForm2:
                    ((NPCNecromancer)currentWorld.GetBoss()).GetStateMachine().SetState(NPCNecromancerStateStand.GetInstance());
                    currentWorld.GetMagicOrb().GetStateMachine().SetState(MagicOrbStateStand.GetInstance());
                    break;
            }
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
        }
    }
}
