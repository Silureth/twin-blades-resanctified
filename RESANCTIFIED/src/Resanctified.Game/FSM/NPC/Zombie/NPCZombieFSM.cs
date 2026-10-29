using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.FSM.NPC.Zombie
{
    public class NPCZombieFSM : EntityFiniteStateMachine
    {
        public float GrabEatTimer;
        public Animation2D[] AnimationArray;

        public NPCZombieFSM(Entity entity)
          : base(entity)
        {
            this.GrabEatTimer = 0.0f;
            this.AnimationArray = new Animation2D[21];
            for (int index = 0; index < 21; ++index)
                this.AnimationArray[index] = new Animation2D();
        }

        public override void Cleanup()
        {
            for (int index = 0; index < 21; ++index)
                this.AnimationArray[index].Cleanup();
        }

        public enum ZombieAnimationId
        {
            Stand,
            Spawn,
            Walk1,
            Walk2,
            Slash,
            Grab,
            Grab_Eat,
            Grab_Back,
            Hit,
            Hit2,
            Hit_Burn,
            Hit_Holy,
            Dying_Body_Shot,
            Dying_Head_Shot,
            Dying_Body_Cut,
            Dying_Burn,
            Dying_Holy,
            Dying_Ice,
            IceBloc_Appear,
            IceBloc,
            IceBloc_Melt,
            ArraySize,
        }
    }
}
