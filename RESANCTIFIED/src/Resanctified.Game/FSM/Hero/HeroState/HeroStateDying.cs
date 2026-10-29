using Resanctified.Engine.FSM;
using Resanctified.Engine.Maths;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateDying : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.IsGrabbed)
            {
                entity.IsGrabbed = false;
                entity.GetAnimationStateMachine().SetState(HeroAnimationStateGrabDying.GetInstance());
            }
            else
                entity.GetAnimationStateMachine().SetState(HeroAnimationStateDying.GetInstance());
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Velocity.Y = 0.0f;
            if (MathHelperEx.GetRandomInt(0, 100) < 50)
                entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Dying_1).Play();
            else
                entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Dying_2).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            if (!((PlayableCharacter)heroFsm.GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            stateMachine.SetState(heroFsm.StateDead);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
