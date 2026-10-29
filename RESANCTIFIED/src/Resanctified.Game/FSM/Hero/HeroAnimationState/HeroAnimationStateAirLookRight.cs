// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroAnimationState.HeroAnimationStateAirLookRight
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateAirLookRight : IState
  {
    private static HeroAnimationStateAirLookRight m_instance = new HeroAnimationStateAirLookRight();

    public static HeroAnimationStateAirLookRight Instance => HeroAnimationStateAirLookRight.m_instance;

    public static HeroAnimationStateAirLookRight GetInstance() => HeroAnimationStateAirLookRight.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      heroAnimationFsm.AirTransitionAnimationIndex = 0;
      if (heroAnimationFsm.GetPreviousState() == HeroAnimationStateJump.GetInstance())
      {
        entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[21]);
        entity.GetView().GetAnimation2D().SetTime(0.0f);
        heroAnimationFsm.AirTransitionAnimationIndex = 0;
      }
      else if ((double) entity.GetPhysics().Velocity.Y < 0.0)
      {
        entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[23]);
        entity.GetView().GetAnimation2D().SetTime(0.0f);
        heroAnimationFsm.AirTransitionAnimationIndex = 2;
      }
      else
      {
        entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[22]);
        entity.GetView().GetAnimation2D().SetTime(0.0f);
        heroAnimationFsm.AirTransitionAnimationIndex = 1;
      }
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      if ((double) entity.GetPhysics().Velocity.Y < 0.0 && heroAnimationFsm.AirTransitionAnimationIndex != 2)
      {
        entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[23]);
        entity.GetView().GetAnimation2D().SetTime(0.0f);
        heroAnimationFsm.AirTransitionAnimationIndex = 2;
      }
      else
      {
        if (heroAnimationFsm.AirTransitionAnimationIndex != 0 || !entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
          return;
        entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[22]);
        entity.GetView().GetAnimation2D().SetTime(0.0f);
        heroAnimationFsm.AirTransitionAnimationIndex = 1;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
