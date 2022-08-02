// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.FSM.Hero.HeroState.HeroStatePushBack
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.FSM.Hero.HeroAnimationState;
using PSS.Game.NunVsZombies.FSM.NPC.Zombie.State;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine.FSM;
using PSS.GameEngine.World;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.FSM.Hero.HeroState
{
  public class HeroStatePushBack : IState
  {
    public void Enter(IFiniteStateMachine stateMachine)
    {
      PlayableCharacter entity = (PlayableCharacter) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetAnimationStateMachine().SetState((IState) HeroAnimationStatePushBack.GetInstance());
      Entity[] buffer = new Entity[55];
      int entitiesByType = WorldManager.GetInstance().GetCurrentWorld().GetEntityManager().GetEntitiesByType(3, buffer, 55);
      for (int index = 0; index < entitiesByType; ++index)
      {
        NPCZombie npcZombie = (NPCZombie) buffer[index];
        if (npcZombie.GetStateMachine().GetCurrentState() != NPCZombieStateFreeze.GetInstance() && npcZombie.IsAlive)
        {
          float num = entity.GetTransform().GetPosition().X - buffer[index].GetTransform().GetPosition().X;
          if ((double) (num * num) < 1.0)
            npcZombie.GetStateMachine().SetState((IState) NPCZombieStateGrabBack.GetInstance());
        }
      }
      entity.GetSound(PlayableCharacter.SoundFxId.SoundFx_PushBack).Play();
      ++GameApplication.GetInstance().AchievementCounter_100Ungrab;
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      HeroFSM heroFsm = (HeroFSM) stateMachine;
      if (!((PlayableCharacter) heroFsm.GetEntity()).GetView().GetAnimation2D().IsEndOfAnimationReached())
        return;
      stateMachine.SetState((IState) heroFsm.StateStand);
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
