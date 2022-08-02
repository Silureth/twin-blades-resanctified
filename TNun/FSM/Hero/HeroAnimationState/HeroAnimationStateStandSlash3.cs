// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState.HeroAnimationStateStandSlash3
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateStandSlash3 : IState
  {
    private static HeroAnimationStateStandSlash3 m_instance = new HeroAnimationStateStandSlash3();

    public static HeroAnimationStateStandSlash3 Instance => HeroAnimationStateStandSlash3.m_instance;

    public static HeroAnimationStateStandSlash3 GetInstance() => HeroAnimationStateStandSlash3.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[8]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
