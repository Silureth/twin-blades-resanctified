using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateWalk : IState
    {
        private const int FOOTSTEP_FRAME_1 = 1;
        private const int FOOTSTEP_FRAME_2 = 3;
        private bool m_footstepDone;

        public virtual void Enter(IFiniteStateMachine stateMachine)
        {
            this.SetAnimation(stateMachine);
            this.SetMovePhysics(stateMachine);
            this.m_footstepDone = false;
        }

        public virtual void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            if ((double)Math.Abs(entity.GetPhysics().Velocity.X) >= 2.0)
            {
                entity.GetPhysics().Velocity.X = entity.GetPhysics().Velocity.X >= 0.0 ? 2f : -2f;
                entity.GetPhysics().Acceleration.X = 0.0f;
            }
            if (entity.IsHit)
                stateMachine.SetState(heroFsm.StateHit);
            else if (entity.IsInAir)
            {
                stateMachine.SetState(heroFsm.StateAir);
            }
            else
            {
                if (entity.GetView().GetAnimation2D().GetFrame() == 1 || entity.GetView().GetAnimation2D().GetFrame() == 3)
                {
                    if (!this.m_footstepDone)
                        this.m_footstepDone = true;
                }
                else
                    this.m_footstepDone = false;
                heroFsm.UpdateNextAction();
                this.UpdateStateFromGameActions(stateMachine, gameTime);
            }
        }

        public virtual void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity()).GetPhysics().Acceleration.X = 0.0f;

        protected virtual void SetAnimation(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.GetAnimationStateMachine().IsInState(HeroAnimationStateWalkLookRight.GetInstance()))
                return;
            entity.GetAnimationStateMachine().SetState(HeroAnimationStateWalkLookRight.GetInstance());
        }

        protected virtual void UpdateStateFromGameActions(
          IFiniteStateMachine stateMachine,
          float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
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
                        this.UpdateMove(stateMachine, gameTime);
                        break;
                    }
                    if ((double)Math.Abs(entity.GetPhysics().Velocity.X) >= 2.0)
                    {
                        stateMachine.SetState(heroFsm.StateSlide);
                        break;
                    }
                    stateMachine.SetState(heroFsm.StateStand);
                    break;
            }
        }

        protected virtual void UpdateMove(IFiniteStateMachine stateMachine, float gameTime)
        {
            this.SetAnimation(stateMachine);
            this.SetMovePhysics(stateMachine);
        }

        protected virtual void SetMovePhysics(IFiniteStateMachine stateMachine)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            Vector2 scale = entity.GetTransform().GetScale();
            if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive())
            {
                if (scale.X > 0.0)
                    entity.GetPhysics().Velocity.X = -0.8f;
                entity.GetPhysics().Acceleration.X = -5f;
                scale.X = -Math.Abs(scale.X);
                entity.GetTransform().SetScale(scale);
            }
            else if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
            {
                if (scale.X < 0.0)
                    entity.GetPhysics().Velocity.X = 0.8f;
                entity.GetPhysics().Acceleration.X = 5f;
                scale.X = Math.Abs(scale.X);
                entity.GetTransform().SetScale(scale);
            }
            if (Math.Sign(scale.X) != Math.Sign(entity.GetPhysics().Velocity.X))
                entity.GetPhysics().Velocity.X = 0.0f;
            if ((double)Math.Abs(entity.GetPhysics().Velocity.X) >= 0.800000011920929)
                return;
            if (scale.X < 0.0)
                entity.GetPhysics().Velocity.X = -0.8f;
            else
                entity.GetPhysics().Velocity.X = 0.8f;
        }
    }
}
