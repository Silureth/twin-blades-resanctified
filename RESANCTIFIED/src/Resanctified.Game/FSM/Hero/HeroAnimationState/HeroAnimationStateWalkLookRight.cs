// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroAnimationState.HeroAnimationStateWalkLookRight
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.UI;
using System;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateWalkLookRight : IState
  {
    private static HeroAnimationStateWalkLookRight m_instance = new HeroAnimationStateWalkLookRight();

    public static HeroAnimationStateWalkLookRight Instance => HeroAnimationStateWalkLookRight.m_instance;

    public static HeroAnimationStateWalkLookRight GetInstance() => HeroAnimationStateWalkLookRight.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      float time = 0.0f;
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      if (heroAnimationFsm.GetPreviousState() == HeroAnimationStateWalkLookRight.GetInstance() || heroAnimationFsm.GetPreviousState() == HeroAnimationStateWalkFireRight.GetInstance())
        time = heroAnimationFsm.AnimationArray[14].GetTime();
      entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[14]);
      entity.GetView().GetAnimation2D().SetTime(time);
      entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[15]);
      entity.GetGunView().GetAnimation2D().SetTime(time);
      entity.GetView().AddSubview((View) entity.GetGunView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if ((double) Math.Abs(entity.GetPhysics().Velocity.X) < 2.0)
      {
        float speed = Math.Abs(entity.GetPhysics().Velocity.X) / 2f;
        if ((double) speed < 0.800000011920929)
          entity.GetView().GetAnimation2D().SetSpeed(0.8f);
        else
          entity.GetView().GetAnimation2D().SetSpeed(speed);
      }
      else
        entity.GetView().GetAnimation2D().SetSpeed(1f);
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity()).GetView().GetAnimation2D().SetSpeed(1f);
  }
}
