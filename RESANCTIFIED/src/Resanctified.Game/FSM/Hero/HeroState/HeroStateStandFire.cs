using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateStandFire : IState
    {
        private HeroSubStateFireBase m_fireSubState;

        public HeroStateStandFire() => this.m_fireSubState = new HeroSubStateFireBase();

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            entity.GetAnimationStateMachine().SetState(HeroAnimationStateStandFireRight.GetInstance());
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Velocity.Y = 0.0f;
            heroFsm.NextAction = HeroFSM.HeroActionId.None;
            this.m_fireSubState.Enter(stateMachine);
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
                if (entity.HasFired && entity.GetAnimationStateMachine().FireTransitionAnimationIndex >= 1)
                {
                    switch (heroFsm.NextAction)
                    {
                        case HeroFSM.HeroActionId.None:
                            if (heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Left).IsActive() || heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.Right).IsActive())
                            {
                                stateMachine.SetState(heroFsm.StateWalk);
                                break;
                            }
                            if (entity.GetAnimationStateMachine().FireTransitionAnimationIndex != 2)
                                break;
                            stateMachine.SetState(heroFsm.StateStand);
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
                else
                    this.m_fireSubState.Update(stateMachine, gameTime);
            }
        }

        public void Exit(IFiniteStateMachine stateMachine) => this.m_fireSubState.Exit(stateMachine);
    }
}
