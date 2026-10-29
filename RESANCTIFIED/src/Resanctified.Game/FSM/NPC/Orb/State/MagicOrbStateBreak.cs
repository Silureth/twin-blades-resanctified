using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Orb.State
{
    public class MagicOrbStateBreak : IState
    {
        private static MagicOrbStateBreak m_instance = new MagicOrbStateBreak();

        public static MagicOrbStateBreak Instance => m_instance;

        public static MagicOrbStateBreak GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            MagicOrb entity = (MagicOrb)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetView().SetAnimation2D(entity.AnimationArray[1]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            entity.GetShieldView().SetAnimation2D(entity.AnimationArray[5], true);
            entity.GetShieldView().GetAnimation2D().SetTime(0.0f);
            entity.GetShieldView().GetAnimation2D().SetSpeed(1f);
            entity.IsBroken = true;
            entity.GetSound(MagicOrb.SoundFxId.SoundFx_Break).Play();
            ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero().DisableManaDrainFx();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            MagicOrb entity = (MagicOrb)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                return;
            entity.GetView().RemoveSubview(entity.GetShieldView());
            stateMachine.SetState(MagicOrbStateBroken.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((MagicOrb)((EntityFiniteStateMachine)stateMachine).GetEntity()).IsHit = false;
    }
}
