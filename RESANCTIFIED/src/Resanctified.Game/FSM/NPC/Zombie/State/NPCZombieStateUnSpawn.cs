// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Zombie.State.NPCZombieStateUnSpawn
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;

namespace Resanctified.Game.FSM.NPC.Zombie.State
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
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetView());
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
        Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetView());
        entity.GetView().SetVisible(true);
      }
      stateMachine.SetState((IState) StateDefault.GetInstance());
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
      NPCZombie entity = (NPCZombie) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.IsActive = false;
      entity.IsAlive = false;
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) entity.GetShadowView());
    }
  }
}
