using Resanctified.Engine.FSM;
using Resanctified.Engine.Maths;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
    public class NPCNecromancerStateStand : IState
    {
        private static NPCNecromancerStateStand m_instance = new NPCNecromancerStateStand();

        public static NPCNecromancerStateStand Instance => m_instance;

        public static NPCNecromancerStateStand GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM)stateMachine;
            NPCNecromancer entity = (NPCNecromancer)npcNecromancerFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[0]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCNecromancer entity = (NPCNecromancer)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.SpellTimer < 4.0)
                return;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            entity.SpellTimer = 0.0f;
            if (entity.IsInvoqueDone || currentWorld.ZombiesAliveNb >= GameConstant.ZOMBIE_ALIVE_NB_BOSS)
            {
                entity.IsInvoqueDone = false;
                int num = 0;
                if (entity.HitPoints < entity.GetHitpointsMax() * 0.75)
                    num = MathHelperEx.GetRandomInt(0, 100);
                if (num < 40)
                    stateMachine.SetState(NPCNecromancerStateFire.GetInstance());
                else if (num < 70)
                    stateMachine.SetState(NPCNecromancerStateSpikes.GetInstance());
                else if (entity.GetBossType() == NPCBoss.BossType.NecromancerForm2 && entity.HitPoints < entity.GetHitpointsMax() * 0.5)
                    stateMachine.SetState(NPCNecromancerStateEarthquake.GetInstance());
                else if (num < 90)
                    stateMachine.SetState(NPCNecromancerStateFire.GetInstance());
                else
                    stateMachine.SetState(NPCNecromancerStateSpikes.GetInstance());
            }
            else
                stateMachine.SetState(NPCNecromancerStateInvoque.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
