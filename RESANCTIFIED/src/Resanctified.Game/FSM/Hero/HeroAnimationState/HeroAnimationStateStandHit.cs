// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroAnimationState.HeroAnimationStateStandHit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.UI;

namespace Resanctified.Game.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateStandHit : IState
  {
    private static HeroAnimationStateStandHit m_instance = new HeroAnimationStateStandHit();

    public static HeroAnimationStateStandHit Instance => HeroAnimationStateStandHit.m_instance;

    public static HeroAnimationStateStandHit GetInstance() => HeroAnimationStateStandHit.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[9]);
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
