using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.FSM.NPC.Butcher
{
    public class NPCButcherFSM : EntityFiniteStateMachine
    {
        public Animation2D[] AnimationArray;

        public NPCButcherFSM(Entity entity)
          : base(entity)
        {
            this.AnimationArray = new Animation2D[14];
            for (int index = 0; index < 14; ++index)
                this.AnimationArray[index] = new Animation2D();
        }

        public override void Cleanup()
        {
            for (int index = 0; index < 14; ++index)
                this.AnimationArray[index].Cleanup();
        }

        public enum ButcherAnimationId
        {
            Stand,
            Walk,
            Slash,
            Throw,
            Angry,
            Dying,
            BallStand,
            BallWalk,
            BallSlash,
            BallThrow,
            BallThrow_Break,
            BallAngry,
            BallDying,
            BallStandTransition,
            ArraySize,
        }
    }
}
