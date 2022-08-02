// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState.HeroAnimationStateAirHit
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState
{
  public class HeroAnimationStateAirHit : IState
  {
    private static HeroAnimationStateAirHit m_instance = new HeroAnimationStateAirHit();

    public static HeroAnimationStateAirHit Instance => HeroAnimationStateAirHit.m_instance;

    public static HeroAnimationStateAirHit GetInstance() => HeroAnimationStateAirHit.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      HeroAnimationFSM heroAnimationFsm = (HeroAnimationFSM) stateMachine;
      PlayableCharacter entity = (PlayableCharacter) heroAnimationFsm.GetEntity();
      entity.GetView().RemoveSubview((View) entity.GetGunView());
      entity.GetView().SetAnimation2D(heroAnimationFsm.AnimationArray[24]);
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
