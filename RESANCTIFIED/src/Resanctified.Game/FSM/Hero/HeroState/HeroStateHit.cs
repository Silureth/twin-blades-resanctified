using Resanctified.Engine.FSM;
using Resanctified.Engine.Maths;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateHit : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.IsInAir)
            {
                entity.GetAnimationStateMachine().SetState(HeroAnimationStateAirHit.GetInstance());
                if (entity.GetPhysics().Velocity.Y > 0.0)
                    entity.GetPhysics().Velocity.Y = 0.0f;
                entity.IsGrabbed = false;
            }
            else
            {
                if (entity.IsGrabbed)
                    entity.GetAnimationStateMachine().SetState(HeroAnimationStateGrabHit.GetInstance());
                else
                    entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandHit.GetInstance());
                entity.GetPhysics().Velocity.X = 0.0f;
                entity.GetPhysics().Velocity.Y = 0.0f;
            }
            if (!entity.IsGrabbed && entity.HitPoints > 0)
                entity.SetIsInvulnerable(true);
            int randomInt = MathHelperEx.GetRandomInt(0, 100);
            if (randomInt < 33)
                entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Hit_1).Play();
            else if (randomInt < 66)
                entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Hit_2).Play();
            else
                entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Hit_3).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            heroFsm.UpdateNextAction();
            if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            if (entity.HitPoints <= 0)
            {
                if (entity.IsInAir)
                    return;
                stateMachine.SetState(heroFsm.StateDying);
            }
            else if (entity.IsInAir)
                stateMachine.SetState(heroFsm.StateAir);
            else if (entity.IsGrabbed)
                stateMachine.SetState(heroFsm.StateGrabbed);
            else
                stateMachine.SetState(heroFsm.StateStand);
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity()).IsHit = false;
    }
}
