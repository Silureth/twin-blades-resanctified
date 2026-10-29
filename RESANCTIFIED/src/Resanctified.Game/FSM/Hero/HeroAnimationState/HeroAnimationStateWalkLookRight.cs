using System;
using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    public class HeroAnimationStateWalkLookRight : IState
    {
        private static HeroAnimationStateWalkLookRight m_instance = new HeroAnimationStateWalkLookRight();

        public static HeroAnimationStateWalkLookRight Instance => m_instance;

        public static HeroAnimationStateWalkLookRight GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            float time = 0.0f;
            entity.GetView().RemoveSubview(entity.GetGunView());
            if (heroAnimationFsm.GetPreviousState() == GetInstance() || heroAnimationFsm.GetPreviousState() == HeroAnimationStateWalkFireRight.GetInstance())
                time = heroAnimationFsm.AnimationArray[14].GetTime();
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[14]);
            entity.GetView().GetAnimation2D().SetTime(time);
            entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[15]);
            entity.GetGunView().GetAnimation2D().SetTime(time);
            entity.GetView().AddSubview(entity.GetGunView());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if ((double)Math.Abs(entity.GetPhysics().Velocity.X) < 2.0)
            {
                float speed = Math.Abs(entity.GetPhysics().Velocity.X) / 2f;
                if ((double)speed < 0.800000011920929)
                    entity.GetView().GetAnimation2D().SetSpeed(0.8f);
                else
                    entity.GetView().GetAnimation2D().SetSpeed(speed);
            }
            else
                entity.GetView().GetAnimation2D().SetSpeed(1f);
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity()).GetView().GetAnimation2D().SetSpeed(1f);
    }
}
