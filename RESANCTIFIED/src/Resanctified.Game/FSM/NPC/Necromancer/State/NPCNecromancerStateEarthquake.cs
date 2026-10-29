using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
    public class NPCNecromancerStateEarthquake : IState
    {
        private static NPCNecromancerStateEarthquake m_instance = new NPCNecromancerStateEarthquake();
        private int m_state;

        public static NPCNecromancerStateEarthquake Instance => m_instance;

        public static NPCNecromancerStateEarthquake GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM)stateMachine;
            NPCNecromancer entity = (NPCNecromancer)npcNecromancerFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[4]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            this.m_state = 0;
            entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Invoque).Play();
            entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Earthquake).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM)stateMachine;
            NPCNecromancer entity = (NPCNecromancer)npcNecromancerFsm.GetEntity();
            float y1 = entity.GetShieldView().GetTransform().GetPosition().Y;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float num = 0.3f;
            if (entity.GetBossType() == NPCBoss.BossType.NecromancerForm2)
                num = 0.5f;
            switch (this.m_state)
            {
                case 0:
                    float y2 = y1 + num * (1f / entity.GetView().GetAnimation2D().GetModel().GetDuration()) * gameTime;
                    entity.GetShieldView().GetTransform().SetPosition(0.0f, y2);
                    if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                        break;
                    currentWorld.EnableEarthquake();
                    entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[5]);
                    entity.GetView().GetAnimation2D().SetTime(0.0f);
                    ++this.m_state;
                    break;
                case 1:
                    if (stateMachine.CurrentStateTime < 9.0)
                        break;
                    currentWorld.DisableEarthquake();
                    entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[6]);
                    entity.GetView().GetAnimation2D().SetTime(0.0f);
                    ++this.m_state;
                    break;
                case 2:
                    float y3 = y1 - num * (1f / entity.GetView().GetAnimation2D().GetModel().GetDuration()) * gameTime;
                    entity.GetShieldView().GetTransform().SetPosition(0.0f, y3);
                    if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                        break;
                    entity.GetShieldView().GetTransform().SetPosition(0.0f, 1f);
                    stateMachine.SetState(NPCNecromancerStateStand.GetInstance());
                    break;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCNecromancer entity = (NPCNecromancer)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.SpellTimer = 0.0f;
            entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Earthquake).Stop();
        }
    }
}
