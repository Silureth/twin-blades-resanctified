using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    public class HeroAnimationStateJump : IState
    {
        private static HeroAnimationStateJump m_instance = new HeroAnimationStateJump();

        public static HeroAnimationStateJump Instance => m_instance;

        public static HeroAnimationStateJump GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            entity.GetView().RemoveSubview(entity.GetGunView());
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[20]);
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
