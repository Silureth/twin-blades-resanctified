using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStatePushed : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.IsInAir)
            {
                entity.GetPhysics().Velocity.Y = 0.0f;
                entity.GetAnimationStateMachine().SetState(HeroAnimationStateAirHit.GetInstance());
            }
            else
            {
                entity.GetPhysics().Velocity.X = 0.0f;
                entity.GetPhysics().Velocity.Y = 0.0f;
                entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandHit.GetInstance());
            }
            entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_PushBack).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            heroFsm.UpdateNextAction();
            if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            if (entity.IsInAir)
                stateMachine.SetState(heroFsm.StateAir);
            else
                stateMachine.SetState(heroFsm.StateStand);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Velocity.Y = 0.0f;
        }
    }
}
