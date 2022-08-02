// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Butcher.NPCButcherFSM
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.FSM;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.FSM.NPC.Butcher
{
  public class NPCButcherFSM : EntityFiniteStateMachine
  {
    public Animation2D[] AnimationArray;

    public NPCButcherFSM(Entity entity)
      : base((IEntity) entity)
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
