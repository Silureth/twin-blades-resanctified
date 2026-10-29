using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Butcher.State
{
    public class NPCButcherStateThrow : IState
    {
        private static NPCButcherStateThrow m_instance = new NPCButcherStateThrow();
        private bool m_shakeDone;

        public static NPCButcherStateThrow Instance => m_instance;

        public static NPCButcherStateThrow GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            NPCButcherFSM npcButcherFsm = (NPCButcherFSM)stateMachine;
            NPCButcher entity = (NPCButcher)npcButcherFsm.GetEntity();
            entity.GetView().SetAnimation2D(npcButcherFsm.AnimationArray[3]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            if (entity.HitPoints <= 1500)
            {
                entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[10]);
                entity.IsBallBrocken = true;
                entity.GetView().RemoveSubview(entity.GetBallView());
                entity.GetBallView().GetTransform().SetPosition(entity.GetTransform().GetPosition().X, entity.GetTransform().GetPosition().Y, -1.5f);
                entity.GetBallView().GetTransform().SetScale(entity.GetTransform().GetScale());
                Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView(entity.GetBallView(), null);
            }
            else
                entity.GetBallView().SetAnimation2D(npcButcherFsm.AnimationArray[9]);
            entity.GetBallView().GetAnimation2D().SetTime(0.0f);
            entity.GetBallView().GetAnimation2D().SetSpeed(1f);
            entity.GetSound(NPCButcher.SoundFxId.SoundFx_Throw).Play();
            this.m_shakeDone = false;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            NPCButcher entity = (NPCButcher)((EntityFiniteStateMachine)stateMachine).GetEntity();
            if (entity.HitPoints <= 0)
            {
                stateMachine.SetState(NPCButcherStateDying.GetInstance());
            }
            else
            {
                if (stateMachine.GetCurrentStateTime() > 1.0)
                {
                    GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
                    if (!this.m_shakeDone)
                    {
                        currentWorld.GetCamera().StartShaking(0.2f, 78.53982f, new Vector2(0.0f, 0.1f), Vector2.Zero);
                        this.m_shakeDone = true;
                    }
                }
                if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
                    return;
                if (entity.HitPoints <= 1500)
                {
                    entity.BallCollisionOff = true;
                    stateMachine.SetState(NPCButcherStateAngry.GetInstance());
                }
                else if (entity.IsAngry)
                    stateMachine.SetState(NPCButcherStateAngry.GetInstance());
                else
                    stateMachine.SetState(NPCButcherStateStand.GetInstance());
            }
        }

        public void Exit(IFiniteStateMachine stateMachine) => ((NPCButcher)((EntityFiniteStateMachine)stateMachine).GetEntity()).ThrowTimer = 0.0f;
    }
}
