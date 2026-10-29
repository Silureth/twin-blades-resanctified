using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    public class HeroAnimationStateAirLookRight : IState
    {
        private static HeroAnimationStateAirLookRight m_instance = new HeroAnimationStateAirLookRight();

        public static HeroAnimationStateAirLookRight Instance => m_instance;

        public static HeroAnimationStateAirLookRight GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            entity.GetView().RemoveSubview(entity.GetGunView());
            heroAnimationFsm.AirTransitionAnimationIndex = 0;
            if (heroAnimationFsm.GetPreviousState() == HeroAnimationStateJump.GetInstance())
            {
                entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[21]);
                entity.GetView().GetAnimation2D().SetTime(0.0f);
                heroAnimationFsm.AirTransitionAnimationIndex = 0;
            }
            else if (entity.GetPhysics().Velocity.Y < 0.0)
            {
                entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[23]);
                entity.GetView().GetAnimation2D().SetTime(0.0f);
                heroAnimationFsm.AirTransitionAnimationIndex = 2;
            }
            else
            {
                entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[22]);
                entity.GetView().GetAnimation2D().SetTime(0.0f);
                heroAnimationFsm.AirTransitionAnimationIndex = 1;
            }
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            if (entity.GetPhysics().Velocity.Y < 0.0 && heroAnimationFsm.AirTransitionAnimationIndex != 2)
            {
                entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[23]);
                entity.GetView().GetAnimation2D().SetTime(0.0f);
                heroAnimationFsm.AirTransitionAnimationIndex = 2;
            }
            else
            {
                if (heroAnimationFsm.AirTransitionAnimationIndex != 0 || !entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                    return;
                entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[22]);
                entity.GetView().GetAnimation2D().SetTime(0.0f);
                heroAnimationFsm.AirTransitionAnimationIndex = 1;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
