using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
    public class HeroAnimationStateDying : IState
    {
        private static HeroAnimationStateDying m_instance = new HeroAnimationStateDying();

        public static HeroAnimationStateDying Instance => m_instance;

        public static HeroAnimationStateDying GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroAnimationFsm.GetEntity();
            entity.GetView().RemoveSubview(entity.GetGunView());
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[27]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            Vector3 position = entity.GetView().GetTransform().GetPosition() with
            {
                Z = 3.7f
            };
            entity.GetView().GetTransform().SetPosition(position);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
