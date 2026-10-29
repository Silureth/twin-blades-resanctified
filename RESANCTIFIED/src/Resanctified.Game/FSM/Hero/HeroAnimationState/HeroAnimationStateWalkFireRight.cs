// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroAnimationState.HeroAnimationStateWalkFireRight
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.UI;
using System;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateWalkFireRight : IState
  {
    private static HeroAnimationStateWalkFireRight m_instance = new HeroAnimationStateWalkFireRight();

    public static HeroAnimationStateWalkFireRight Instance => HeroAnimationStateWalkFireRight.m_instance;

    public static HeroAnimationStateWalkFireRight GetInstance() => HeroAnimationStateWalkFireRight.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      heroAnimationFsm.FireTransitionAnimationIndex = 0;
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      if (heroAnimationFsm.GetPreviousState() != HeroAnimationStateStandFireRight.GetInstance() && heroAnimationFsm.GetPreviousState() != HeroAnimationStateWalkFireRight.GetInstance())
      {
        entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[14]);
        entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[17]);
        entity.GetGunView().GetAnimation2D().SetTime(0.0f);
        heroAnimationFsm.FireTransitionAnimationIndex = 0;
      }
      else
      {
        entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[14]);
        entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[16]);
        entity.GetGunView().GetAnimation2D().SetTime(0.0f);
        if (entity.GetGun().IsAutofireAvailable)
          entity.GetGunView().GetAnimation2D().SetSpeed(0.0f);
        else
          entity.GetGunView().GetAnimation2D().SetSpeed(1f);
        heroAnimationFsm.FireTransitionAnimationIndex = 1;
      }
      entity.GetView().AddSubview((View) entity.GetGunView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      switch (heroAnimationFsm.FireTransitionAnimationIndex)
      {
        case 0:
          if (entity.GetGunView().GetAnimation2D().IsEndOfAnimationReached())
          {
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[14]);
            entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[16]);
            entity.GetGunView().GetAnimation2D().SetTime(0.0f);
            if (entity.GetGun().IsAutofireAvailable)
              entity.GetGunView().GetAnimation2D().SetSpeed(0.0f);
            else
              entity.GetGunView().GetAnimation2D().SetSpeed(1f);
            heroAnimationFsm.FireTransitionAnimationIndex = 1;
            break;
          }
          break;
        case 1:
          if (entity.GetGunView().GetAnimation2D().IsEndOfAnimationReached() || entity.GetGun().IsAutofireAvailable && !entity.GetStateMachine().GetGameActionFromId(HeroFSM.HeroGameActionId.AutoFire).IsActive())
          {
            entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[14]);
            entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[18]);
            entity.GetGunView().GetAnimation2D().SetTime(0.0f);
            heroAnimationFsm.FireTransitionAnimationIndex = 2;
            break;
          }
          break;
      }
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
