using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateStand : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandLookRight.GetInstance());
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Velocity.Y = 0.0f;
            entity.GetPhysics().Acceleration.X = 0.0f;
            entity.GetPhysics().Acceleration.Y = 0.0f;
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
                switch (heroFsm.NextAction)
                {
                    case HeroFSM.HeroActionId.None:
                        if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                        {
                            stateMachine.SetState(heroFsm.StateWalk);
                            break;
                        }
                        if (entity.GetAnimationStateMachine().IsInState(HeroAnimationStateStandLookRight.GetInstance()))
                            break;
                        entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandLookRight.GetInstance());
                        break;
                    case HeroFSM.HeroActionId.Slash:
                        stateMachine.SetState(heroFsm.StateSlash);
                        break;
                    case HeroFSM.HeroActionId.Fire:
                        if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                        {
                            stateMachine.SetState(heroFsm.StateWalkFire);
                            break;
                        }
                        stateMachine.SetState(heroFsm.StateStandFire);
                        break;
                    case HeroFSM.HeroActionId.Jump:
                        stateMachine.SetState(heroFsm.StateJump);
                        break;
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
