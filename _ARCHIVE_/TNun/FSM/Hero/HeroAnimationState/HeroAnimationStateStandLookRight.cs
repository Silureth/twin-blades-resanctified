// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState.HeroAnimationStateStandLookRight
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateStandLookRight : IState
  {
    private static HeroAnimationStateStandLookRight m_instance = new HeroAnimationStateStandLookRight();

    public static HeroAnimationStateStandLookRight Instance => HeroAnimationStateStandLookRight.m_instance;

    public static HeroAnimationStateStandLookRight GetInstance() => HeroAnimationStateStandLookRight.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      float time = 0.0f;
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      if (heroAnimationFsm.GetPreviousState() == HeroAnimationStateStandLookRight.GetInstance() || heroAnimationFsm.GetPreviousState() == HeroAnimationStateStandFireRight.GetInstance())
        time = heroAnimationFsm.AnimationArray[0].GetTime();
      entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[0]);
      entity.GetView().GetAnimation2D().SetTime(time);
      entity.GetGunView().SetAnimation2D(heroAnimationFsm.AnimationArray[1]);
      entity.GetGunView().GetAnimation2D().SetTime(time);
      entity.GetView().AddSubview((View) entity.GetGunView());
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
