// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.NPC.Zombie.State.NPCZombieStateUnSpawn
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.FSM.NPC.Zombie.State
{
  public class NPCZombieStateUnSpawn : IState
  {
    private static NPCZombieStateUnSpawn m_instance = new NPCZombieStateUnSpawn();

    public static NPCZombieStateUnSpawn Instance => NPCZombieStateUnSpawn.m_instance;

    public static NPCZombieStateUnSpawn GetInstance() => NPCZombieStateUnSpawn.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (stateMachine.GetPreviousState() != NPCZombieStateDead.GetInstance())
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetView());
      if (entity.IsAlive)
        --((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).ZombiesAliveNb;
      entity.Timer = 0.05f;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      if (stateMachine.GetPreviousState() == NPCZombieStateDead.GetInstance())
      {
        NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
        if (stateMachine.GetCurrentStateTime() < 1.0)
        {
          entity.Timer -= gameTime;
          if ((double) entity.Timer > 0.0)
            return;
          entity.Timer = 0.05f;
          entity.GetView().SetVisible(!entity.GetView().IsVisible());
          return;
        }
        PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetView());
        entity.GetView().SetVisible(true);
      }
      stateMachine.SetState((IState) StateDefault.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.IsActive = false;
      entity.IsAlive = false;
      PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
    }
  }
}
