// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Orb.State.MagicOrbStateBroken
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Orb.State
{
  public class MagicOrbStateBroken : IState
  {
    private static MagicOrbStateBroken m_instance = new MagicOrbStateBroken();

    public static MagicOrbStateBroken Instance => MagicOrbStateBroken.m_instance;

    public static MagicOrbStateBroken GetInstance() => MagicOrbStateBroken.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      MagicOrb entity = (MagicOrb) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (!((NPCNecromancer) ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetBoss()).IsAlive() || stateMachine.CurrentStateTime <= 20.0)
        return;
      stateMachine.SetState((IState) MagicOrbStateRestoring.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
