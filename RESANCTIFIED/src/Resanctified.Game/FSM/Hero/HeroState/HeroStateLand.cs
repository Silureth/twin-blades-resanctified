using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateLand : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetAnimationStateMachine().SetState(HeroAnimationStateLand.GetInstance());
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Velocity.Y = 0.0f;
            entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Land).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            if (entity.IsHit)
            {
                stateMachine.SetState(heroFsm.StateHit);
            }
            else
            {
                heroFsm.UpdateNextAction();
                if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                {
                    stateMachine.SetState(heroFsm.StateWalk);
                }
                else
                {
                    if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                        return;
                    stateMachine.SetState(heroFsm.StateStand);
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
