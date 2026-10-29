// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroAnimationFSM
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.FSM.Hero
{
  public class HeroAnimationFSM : EntityFiniteStateMachine
  {
    public Animation2D[] AnimationArray;
    public Animation2D[] GunFXAnimationArray;
    public int AirTransitionAnimationIndex;
    public int FireTransitionAnimationIndex;

    public HeroAnimationFSM(Entity entity)
      : base((IEntity) entity)
    {
      this.AirTransitionAnimationIndex = 0;
      this.FireTransitionAnimationIndex = 0;
      this.AnimationArray = new Animation2D[30];
      this.GunFXAnimationArray = new Animation2D[10];
      for (int index = 0; index < 30; ++index)
        this.AnimationArray[index] = new Animation2D();
      for (int index = 0; index < 10; ++index)
        this.GunFXAnimationArray[index] = new Animation2D();
    }

    public override void Cleanup()
    {
      for (int index = 0; index < 30; ++index)
      {
        if (this.AnimationArray[index] != null)
          this.AnimationArray[index].Cleanup();
      }
      for (int index = 0; index < 10; ++index)
      {
        if (this.GunFXAnimationArray[index] != null)
          this.GunFXAnimationArray[index].Cleanup();
      }
    }

    public enum HeroAnimationId
    {
      Stand_Body,
      Stand_Gun,
      Stand_Body_Fire,
      Stand_Gun_Fire_Right,
      Stand_Gun_Fire_Right_Transition,
      Stand_Gun_Fire_Right_Transition_Back,
      Stand_Slash1_Body,
      Stand_Slash2_Body,
      Stand_Slash3_Body,
      Stand_Hit_Body,
      Grab_Hit,
      Grabbed,
      PushBack,
      Grab_Dying,
      Walk_Body,
      Walk_Gun,
      Walk_Gun_Fire_Right,
      Walk_Gun_Fire_Right_Transition,
      Walk_Gun_Fire_Right_Transition_Back,
      Walk_Slide_Body,
      Jump_Body,
      Jump_Air_Body,
      Air_Body,
      Air_Fall_Body,
      Air_Hit_Body,
      Land_Body,
      Air_Slash_Finishing_Body,
      Dying_Body,
      Dead_Body,
      Crouch_Body,
      ArraySize,
    }

    public enum GunFXAnimationId
    {
      Gun_Fire,
      FlameThrower_Fire,
      FlameThrower_AutoFire,
      FlameThrower_Fire_End,
      IceGun_Fire,
      IceGun_AutoFire,
      IceGun_Fire_End,
      HolyGun_Fire,
      HolyGun_AutoFire,
      HolyGun_Fire_End,
      ArraySize,
    }
  }
}
