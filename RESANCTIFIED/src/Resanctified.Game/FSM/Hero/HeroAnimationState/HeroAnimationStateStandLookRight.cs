using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    public class HeroAnimationStateStandLookRight : IState
    {
        private static HeroAnimationStateStandLookRight m_instance = new HeroAnimationStateStandLookRight();

        public static HeroAnimationStateStandLookRight Instance => m_instance;

        public static HeroAnimationStateStandLookRight GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            float time = 0.0f;
            entity.GetView().RemoveSubview(entity.GetGunView());
            if (heroAnimationFsm.GetPreviousState() == GetInstance() || heroAnimationFsm.GetPreviousState() == HeroAnimationStateStandFireRight.GetInstance())
                time = heroAnimationFsm.AnimationArray[0].GetTime();
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[0]);
            entity.GetView().GetAnimation2D().SetTime(time);
            entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[1]);
            entity.GetGunView().GetAnimation2D().SetTime(time);
            entity.GetView().AddSubview(entity.GetGunView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
