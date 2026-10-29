using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.FSM.NPC.Necromancer
{
    public class NPCNecromancerFSM : EntityFiniteStateMachine
    {
        public Animation2D[] AnimationArray;

        public NPCNecromancerFSM(Entity entity)
          : base(entity)
        {
            this.AnimationArray = new Animation2D[10];
            for (int index = 0; index < 10; ++index)
                this.AnimationArray[index] = new Animation2D();
        }

        public override void Cleanup()
        {
            for (int index = 0; index < 10; ++index)
                this.AnimationArray[index].Cleanup();
        }

        public enum NecromancerAnimationId
        {
            Stand,
            Hit,
            Dying,
            SpellBlast,
            SpellInvoqueStart,
            SpellInvoquing,
            SpellInvoqueEnd,
            Shield,
            ShieldBounce,
            ShieldDisappear,
            ArraySize,
        }
    }
}
