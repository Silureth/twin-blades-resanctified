using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateAir : IState
    {
        public void Enter(IFiniteStateMachine stateMachine) => ((PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity()).GetAnimationStateMachine().SetState(HeroAnimationStateAirLookRight.GetInstance());

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
                if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                {
                    entity.GetPhysics().Velocity.X += 4f * gameTime;
                    Vector2 scale = entity.GetTransform().GetScale();
                    scale.X = Math.Abs(scale.X);
                    entity.GetTransform().SetScale(scale);
                }
                else if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive())
                {
                    entity.GetPhysics().Velocity.X -= 4f * gameTime;
                    Vector2 scale = entity.GetTransform().GetScale();
                    scale.X = -Math.Abs(scale.X);
                    entity.GetTransform().SetScale(scale);
                }
                heroFsm.UpdateNextAction();
                if (heroFsm.NextAction == HeroFSM.HeroActionId.Slash && entity.AirSlashAvailable)
                {
                    if (entity.GetTransform().GetPosition().Y <= 1.0 && entity.GetPhysics().Velocity.Y > 0.0)
                        return;
                    stateMachine.SetState(heroFsm.StateSlash);
                }
                else
                {
                    heroFsm.NextAction = HeroFSM.HeroActionId.None;
                    if (entity.IsInAir)
                        return;
                    if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                        stateMachine.SetState(heroFsm.StateWalk);
                    else
                        stateMachine.SetState(heroFsm.StateLand);
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
