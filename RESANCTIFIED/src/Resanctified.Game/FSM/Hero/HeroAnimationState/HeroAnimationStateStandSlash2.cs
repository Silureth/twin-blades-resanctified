// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroAnimationState.HeroAnimationStateStandSlash2
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateStandSlash2 : IState
  {
    private static HeroAnimationStateStandSlash2 m_instance = new HeroAnimationStateStandSlash2();

    public static HeroAnimationStateStandSlash2 Instance => HeroAnimationStateStandSlash2.m_instance;

    public static HeroAnimationStateStandSlash2 GetInstance() => HeroAnimationStateStandSlash2.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[7]);
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
