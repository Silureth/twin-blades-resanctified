using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateDead : IState
    {
        private static NPCZombieStateDead m_instance = new NPCZombieStateDead();

        public static NPCZombieStateDead Instance => m_instance;

        public static NPCZombieStateDead GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (stateMachine.GetCurrentStateTime() < 6.0)
                return;
            stateMachine.SetState(NPCZombieStateUnSpawn.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView(entity.GetShadowView());
        }
    }
}
