using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    internal class HeroAnimationStateGrabHit : IState
    {
        private static HeroAnimationStateGrabHit m_instance = new HeroAnimationStateGrabHit();

        public static HeroAnimationStateGrabHit Instance => m_instance;

        public static HeroAnimationStateGrabHit GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            entity.GetView().RemoveSubview(entity.GetGunView());
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[10]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
