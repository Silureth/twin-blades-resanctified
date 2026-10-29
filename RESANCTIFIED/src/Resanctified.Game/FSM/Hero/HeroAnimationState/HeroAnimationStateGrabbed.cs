using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    internal class HeroAnimationStateGrabbed : IState
    {
        private static HeroAnimationStateGrabbed m_instance = new HeroAnimationStateGrabbed();

        public static HeroAnimationStateGrabbed Instance => m_instance;

        public static HeroAnimationStateGrabbed GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            entity.GetView().RemoveSubview(entity.GetGunView());
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[11]);
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
