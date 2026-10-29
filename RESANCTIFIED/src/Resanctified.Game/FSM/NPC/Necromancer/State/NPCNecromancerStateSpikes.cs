// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Necromancer.State.NPCNecromancerStateSpikes
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;

namespace Resanctified.Game.FSM.NPC.Necromancer.State
{
  public class NPCNecromancerStateSpikes : IState
  {
    private static NPCNecromancerStateSpikes m_instance = new NPCNecromancerStateSpikes();
    private int m_state;

    public static NPCNecromancerStateSpikes Instance => NPCNecromancerStateSpikes.m_instance;

    public static NPCNecromancerStateSpikes GetInstance() => NPCNecromancerStateSpikes.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM) stateMachine;
      NPCNecromancer entity = (NPCNecromancer) npcNecromancerFsm.GetEntity();
      entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[4]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      this.m_state = 0;
      entity.GetSound(NPCNecromancer.SoundFxId.SoundFx_Invoque).Play();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      NPCNecromancerFSM npcNecromancerFsm = (NPCNecromancerFSM) stateMachine;
      NPCNecromancer entity = (NPCNecromancer) npcNecromancerFsm.GetEntity();
      float y1 = entity.GetShieldView().GetTransform().GetPosition().Y;
      float num = 0.3f;
      if (entity.GetBossType() == NPCBoss.BossType.NecromancerForm2)
        num = 0.5f;
      switch (this.m_state)
      {
        case 0:
          float y2 = y1 + num * (1f / entity.GetView().GetAnimation2D().GetModel().GetDuration()) * gameTime;
          entity.GetShieldView().GetTransform().SetPosition(0.0f, y2);
          if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
            break;
          entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[5]);
          entity.GetView().GetAnimation2D().SetTime(0.0f);
          ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).EnableSpikeTrap();
          ++this.m_state;
          break;
        case 1:
          if (stateMachine.CurrentStateTime < 4.0)
            break;
          entity.GetView().SetAnimation2D(npcNecromancerFsm.AnimationArray[6]);
          entity.GetView().GetAnimation2D().SetTime(0.0f);
          ++this.m_state;
          break;
        case 2:
          float y3 = y1 - num * (1f / entity.GetView().GetAnimation2D().GetModel().GetDuration()) * gameTime;
          entity.GetShieldView().GetTransform().SetPosition(0.0f, y3);
          if (!entity.GetView().GetAnimation2D().IsEndOfAnimationReached())
            break;
          entity.GetShieldView().GetTransform().SetPosition(0.0f, 1f);
          stateMachine.SetState((IState) NPCNecromancerStateStand.GetInstance());
          break;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine) => ((NPCNecromancer) ((EntityFiniteStateMachine) stateMachine).GetEntity()).SpellTimer = 0.0f;
  }
}
