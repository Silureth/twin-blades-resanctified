// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Necromancer.NPCNecromancerFSM
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.FSM.NPC.Necromancer
{
  public class NPCNecromancerFSM : EntityFiniteStateMachine
  {
    public Animation2D[] AnimationArray;

    public NPCNecromancerFSM(Entity entity)
      : base((IEntity) entity)
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
