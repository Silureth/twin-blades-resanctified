// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.NPCZombieFSM
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.FSM;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie
{
  public class NPCZombieFSM : EntityFiniteStateMachine
  {
    public float GrabEatTimer;
    public Animation2D[] AnimationArray;

    public NPCZombieFSM(Entity entity)
      : base((IEntity) entity)
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
