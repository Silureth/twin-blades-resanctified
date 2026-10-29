using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
    public class NPCButcherStateIdle : IState
    {
        private static NPCButcherStateIdle m_instance = new NPCButcherStateIdle();
        private bool m_transition;

        public static NPCButcherStateIdle Instance => m_instance;

        public static NPCButcherStateIdle GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCButcherFSM npcButcherFsm = (NPCButcherFSM)stateMachine;
            NPCButcher entity = (NPCButcher)npcButcherFsm.GetEntity();
            PlayableCharacter hero = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero();
            Vector2 scale = entity.GetTransform().GetScale();
            float num = hero.GetTransform().GetPosition().X - entity.GetTransform().GetPosition().X;
            scale.X = entity.GetTransform().GetPosition().X != 6.5 ? ((double)num >= 0.0 ? Math.Abs(scale.X) : -Math.Abs(scale.X)) : ((double)num <= 0.0 ? -Math.Abs(scale.X) : Math.Abs(scale.X));
            entity.GetTransform().SetScale(scale);
            entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[6]);
            entity.GetBallView().GetAnimation2D().SetTime(0.0f);
            entity.GetBallView().GetAnimation2D().SetSpeed(1f);
            entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[0]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
