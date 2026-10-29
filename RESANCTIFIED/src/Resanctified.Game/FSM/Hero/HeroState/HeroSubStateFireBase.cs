using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroSubStateFireBase : IState
    {
        public void Enter(IFiniteStateMachine stateMachine) => ((PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity()).HasFired = false;

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.GetAnimationStateMachine().FireTransitionAnimationIndex < 1 || entity.HasFired)
                return;
            entity.HasFired = true;
            entity.GetGun().Fire();
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
