using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.FSM.NPC.Zombie.State;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStatePushBack : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetAnimationStateMachine().SetState(HeroAnimationStatePushBack.GetInstance());
            Entity[] buffer = new Entity[55];
            int entitiesByType = WorldManager.GetInstance().GetCurrentWorld().GetEntityManager().GetEntitiesByType(3, buffer, 55);
            for (int index = 0; index < entitiesByType; ++index)
            {
                NPCZombie npcZombie = (NPCZombie)buffer[index];
                if (npcZombie.GetStateMachine().GetCurrentState() != NPCZombieStateFreeze.GetInstance() && npcZombie.IsAlive)
                {
                    float num = entity.GetTransform().GetPosition().X - buffer[index].GetTransform().GetPosition().X;
                    if ((double)(num * num) < 1.0)
                        npcZombie.GetStateMachine().SetState(NPCZombieStateGrabBack.GetInstance());
                }
            }
            entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_PushBack).Play();
            ++GameApplication.GetInstance().AchievementCounter_100Ungrab;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            if (!((PlayableCharacter)heroFsm.GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            stateMachine.SetState(heroFsm.StateStand);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
