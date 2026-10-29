using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateSlash : IState
    {
        private float m_timer;
        private float m_newVelocityX;
        private float m_newAccelerationX;
        private bool m_heroWasInAir;
        private bool m_burstFXPlayed;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            this.m_timer = 0.0f;
            this.m_heroWasInAir = entity.IsInAir;
            this.m_burstFXPlayed = false;
            this.m_newAccelerationX = 0.0f;
            this.m_newVelocityX = 0.0f;
            if (entity.IsInAir)
            {
                if (!entity.AirSlashAvailable)
                {
                    heroFsm.NextAction = HeroFSM.HeroActionId.None;
                    stateMachine.SetState(heroFsm.GetPreviousState());
                    return;
                }
                entity.SetIsInvulnerable(false);
                entity.GetPhysics().Velocity.X = 0.0f;
                entity.GetPhysics().Velocity.Y = -6f;
                entity.GetAnimationStateMachine().SetState(HeroAnimationStateAirSlashFinishing.GetInstance());
                entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Slash_2).Play();
            }
            else
            {
                Vector2 scale = entity.GetTransform().GetScale();
                if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive())
                {
                    if (scale.X > 0.0)
                    {
                        scale.X = -Math.Abs(scale.X);
                        entity.GetTransform().SetScale(scale);
                    }
                }
                else if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive() && scale.X < 0.0)
                {
                    scale.X = Math.Abs(scale.X);
                    entity.GetTransform().SetScale(scale);
                }
                if (entity.GetAnimationStateMachine().GetCurrentState() == HeroAnimationStateStandSlash.GetInstance())
                {
                    if (entity.SlashDamageLevel >= 2)
                        entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandSlash2.GetInstance());
                    else
                        entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandSlash.GetInstance());
                    this.m_timer = 0.0f;
                    this.m_newVelocityX = 0.0f;
                    this.m_newAccelerationX = 0.0f;
                }
                else if (entity.GetAnimationStateMachine().GetCurrentState() == HeroAnimationStateStandSlash2.GetInstance())
                {
                    if (entity.SlashDamageLevel >= 3)
                    {
                        entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandSlash3.GetInstance());
                        this.m_timer = 0.1f;
                        if ((double)Math.Abs(entity.GetPhysics().Velocity.X) > 0.800000011920929)
                        {
                            this.m_newVelocityX = 2.8f * entity.GetTransform().GetScale().X;
                            this.m_newAccelerationX = -6f * entity.GetTransform().GetScale().X;
                        }
                        else
                            this.m_newVelocityX = 0.8f * entity.GetTransform().GetScale().X;
                    }
                    else
                    {
                        entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandSlash.GetInstance());
                        this.m_timer = 0.0f;
                        this.m_newVelocityX = 0.0f;
                        this.m_newAccelerationX = 0.0f;
                    }
                }
                else
                {
                    entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandSlash.GetInstance());
                    this.m_timer = 0.0f;
                    this.m_newVelocityX = 0.0f;
                    this.m_newAccelerationX = 0.0f;
                }
                entity.GetPhysics().Velocity.X = 0.0f;
                entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_Slash_1).Play();
            }
            heroFsm.NextAction = HeroFSM.HeroActionId.None;
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
                if (this.m_timer > 0.0)
                {
                    this.m_timer -= gameTime;
                    if (this.m_timer <= 0.0)
                    {
                        this.m_timer = 0.0f;
                        entity.GetPhysics().Velocity.X = this.m_newVelocityX;
                        entity.GetPhysics().Acceleration.X = this.m_newAccelerationX;
                    }
                }
                heroFsm.UpdateNextAction();
                if (entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                {
                    switch (heroFsm.NextAction)
                    {
                        case HeroFSM.HeroActionId.None:
                            if (entity.IsInAir)
                            {
                                stateMachine.SetState(heroFsm.StateAir);
                                break;
                            }
                            if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                            {
                                stateMachine.SetState(heroFsm.StateWalk);
                                break;
                            }
                            stateMachine.SetState(heroFsm.StateStand);
                            break;
                        case HeroFSM.HeroActionId.Slash:
                            stateMachine.SetState(heroFsm.StateSlash);
                            break;
                        case HeroFSM.HeroActionId.Fire:
                            if (entity.IsInAir)
                            {
                                heroFsm.NextAction = HeroFSM.HeroActionId.None;
                                break;
                            }
                            stateMachine.SetState(heroFsm.StateStandFire);
                            break;
                        case HeroFSM.HeroActionId.Jump:
                            if (entity.IsInAir)
                            {
                                heroFsm.NextAction = HeroFSM.HeroActionId.None;
                                break;
                            }
                            stateMachine.SetState(heroFsm.StateJump);
                            break;
                    }
                }
                if (!this.m_heroWasInAir || entity.IsInAir)
                    return;
                int num = this.m_burstFXPlayed ? 1 : 0;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Acceleration.X = 0.0f;
        }
    }
}
