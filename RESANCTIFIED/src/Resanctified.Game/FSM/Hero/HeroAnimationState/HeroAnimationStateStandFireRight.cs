using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    public class HeroAnimationStateStandFireRight : IState
    {
        private static HeroAnimationStateStandFireRight m_instance = new HeroAnimationStateStandFireRight();

        public static HeroAnimationStateStandFireRight Instance => m_instance;

        public static HeroAnimationStateStandFireRight GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            heroAnimationFsm.FireTransitionAnimationIndex = 0;
            entity.GetView().RemoveSubview(entity.GetGunView());
            if (heroAnimationFsm.GetPreviousState() != GetInstance() && heroAnimationFsm.GetPreviousState() != HeroAnimationStateWalkFireRight.GetInstance())
            {
                entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[2]);
                entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[4]);
                entity.GetGunView().GetAnimation2D().SetTime(0.0f);
                heroAnimationFsm.FireTransitionAnimationIndex = 0;
            }
            else
            {
                entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[2]);
                entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[3]);
                entity.GetGunView().GetAnimation2D().SetTime(0.0f);
                if (entity.GetGun().IsAutofireAvailable)
                    entity.GetGunView().GetAnimation2D().SetSpeed(0.0f);
                else
                    entity.GetGunView().GetAnimation2D().SetSpeed(1f);
                heroAnimationFsm.FireTransitionAnimationIndex = 1;
            }
            entity.GetView().AddSubview(entity.GetGunView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            switch (heroAnimationFsm.FireTransitionAnimationIndex)
            {
                case 0:
                    if (!entity.GetGunView().GetAnimation2D().IsEndOfAnimationReached())
                        break;
                    entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[2]);
                    entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[3]);
                    entity.GetGunView().GetAnimation2D().SetTime(0.0f);
                    if (entity.GetGun().IsAutofireAvailable)
                        entity.GetGunView().GetAnimation2D().SetSpeed(0.0f);
                    else
                        entity.GetGunView().GetAnimation2D().SetSpeed(1f);
                    heroAnimationFsm.FireTransitionAnimationIndex = 1;
                    break;
                case 1:
                    if (!entity.GetGunView().GetAnimation2D().IsEndOfAnimationReached() && (!entity.GetGun().IsAutofireAvailable || entity.GetStateMachine().GetGameActionFromId(HeroFSM.HeroGameActionId.AutoFire).IsActive()))
                        break;
                    entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[2]);
                    entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[5]);
                    entity.GetGunView().GetAnimation2D().SetTime(0.0f);
                    heroAnimationFsm.FireTransitionAnimationIndex = 2;
                    break;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
