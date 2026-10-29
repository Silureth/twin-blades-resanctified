using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Orb.State
{
    public class MagicOrbStateBroken : IState
    {
        private static MagicOrbStateBroken m_instance = new MagicOrbStateBroken();

        public static MagicOrbStateBroken Instance => m_instance;

        public static MagicOrbStateBroken GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            MagicOrb entity = (MagicOrb)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (!((NPCNecromancer)((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetBoss()).IsAlive() || stateMachine.CurrentStateTime <= 20.0)
                return;
            stateMachine.SetState(MagicOrbStateRestoring.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
