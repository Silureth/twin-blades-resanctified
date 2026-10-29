using System;
using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateSlide : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetAnimationStateMachine().SetState(HeroAnimationStateWalkSlide.GetInstance());
            entity.GetPhysics().Acceleration.X = -5f * entity.GetTransform().GetScale().X;
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
                if ((double)Math.Abs(entity.GetPhysics().Velocity.X) < 0.800000011920929)
                {
                    entity.GetPhysics().Velocity.X = 0.0f;
                    entity.GetPhysics().Acceleration.X = 0.0f;
                }
                switch (heroFsm.NextAction)
                {
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
                    default:
                        if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                        {
                            stateMachine.SetState(heroFsm.StateWalk);
                            break;
                        }
                        if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                            break;
                        stateMachine.SetState(heroFsm.StateStand);
                        break;
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity()).GetPhysics().Acceleration.X = 0.0f;
    }
}
