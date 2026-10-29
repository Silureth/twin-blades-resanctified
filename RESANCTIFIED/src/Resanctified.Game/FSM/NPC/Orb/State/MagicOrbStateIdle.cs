using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Orb.State
{
    public class MagicOrbStateIdle : IState
    {
        private static MagicOrbStateIdle m_instance = new MagicOrbStateIdle();

        public static MagicOrbStateIdle Instance => m_instance;

        public static MagicOrbStateIdle GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            MagicOrb entity = (MagicOrb)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetView().SetAnimation2D(entity.AnimationArray[0]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            entity.GetShieldView().SetAnimation2D(entity.AnimationArray[3], true);
            entity.GetShieldView().GetAnimation2D().SetTime(0.0f);
            entity.GetShieldView().GetAnimation2D().SetSpeed(1f);
            entity.IsBroken = false;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
