// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.State.NPCZombieStateDead
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie.State
{
  public class NPCZombieStateDead : IState
  {
    private static NPCZombieStateDead m_instance = new NPCZombieStateDead();

    public static NPCZombieStateDead Instance => NPCZombieStateDead.m_instance;

    public static NPCZombieStateDead GetInstance() => NPCZombieStateDead.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (stateMachine.GetCurrentStateTime() < 6.0)
        return;
      stateMachine.SetState((IState) NPCZombieStateUnSpawn.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
    }
  }
}
