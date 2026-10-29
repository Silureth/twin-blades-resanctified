using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    public class HeroAnimationStateGrabDying : IState
    {
        private static HeroAnimationStateGrabDying m_instance = new HeroAnimationStateGrabDying();

        public static HeroAnimationStateGrabDying Instance => m_instance;

        public static HeroAnimationStateGrabDying GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            entity.GetView().RemoveSubview(entity.GetGunView());
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[13]);
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
