using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie.State
{
    public class NPCZombieStateUnSpawn : IState
    {
        private static NPCZombieStateUnSpawn m_instance = new NPCZombieStateUnSpawn();

        public static NPCZombieStateUnSpawn Instance => m_instance;

        public static NPCZombieStateUnSpawn GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (stateMachine.GetPreviousState() != NPCZombieStateDead.GetInstance())
                Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView(entity.GetView());
            if (entity.IsAlive)
                --((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).ZombiesAliveNb;
            entity.Timer = 0.05f;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (stateMachine.GetPreviousState() == NPCZombieStateDead.GetInstance())
            {
                NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
                if (stateMachine.GetCurrentStateTime() < 1.0)
                {
                    entity.Timer -= gameTime;
                    if (entity.Timer > 0.0)
                        return;
                    entity.Timer = 0.05f;
                    entity.GetView().SetVisible(!entity.GetView().IsVisible());
                    return;
                }
                Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView(entity.GetView());
                entity.GetView().SetVisible(true);
            }
            stateMachine.SetState(StateDefault.GetInstance());
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            NPCZombie entity = (NPCZombie)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.IsActive = false;
            entity.IsAlive = false;
            Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView(entity.GetShadowView());
        }
    }
}
