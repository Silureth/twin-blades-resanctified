// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Orb.State.MagicOrbStateBreak
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Orb.State
{
  public class MagicOrbStateBreak : IState
  {
    private static MagicOrbStateBreak m_instance = new MagicOrbStateBreak();

    public static MagicOrbStateBreak Instance => MagicOrbStateBreak.m_instance;

    public static MagicOrbStateBreak GetInstance() => MagicOrbStateBreak.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      MagicOrb entity = (MagicOrb) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetView().SetAnimation2D(entity.AnimationArray[1]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      entity.GetShieldView().SetAnimation2D(entity.AnimationArray[5], true);
      entity.GetShieldView().GetAnimation2D().SetTime(0.0f);
      entity.GetShieldView().GetAnimation2D().SetSpeed(1f);
      entity.IsBroken = true;
      entity.GetSound(MagicOrb.SoundFxId.SoundFx_Break).Play();
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero().DisableManaDrainFx();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      MagicOrb entity = (MagicOrb) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      entity.GetView().RemoveSubview((View) entity.GetShieldView());
      stateMachine.SetState((IState) MagicOrbStateBroken.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((MagicOrb) ((EntityFiniteStateMachine) stateMachine).GetEntity()).IsHit = false;
  }
}
