// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.NPC.Orb.State.MagicOrbStateStand
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.World;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.World;

namespace Resanctified.Game.FSM.NPC.Orb.State
{
  public class MagicOrbStateStand : IState
  {
    private static MagicOrbStateStand m_instance = new MagicOrbStateStand();
    private float m_manaToDrain;
    private float m_vulnerableTime;
    private bool m_isVulnerable;

    public static MagicOrbStateStand Instance => MagicOrbStateStand.m_instance;

    public static MagicOrbStateStand GetInstance() => MagicOrbStateStand.m_instance;

    public void Enter(IFiniteStateMachine stateMachine)
    {
      MagicOrb entity = (MagicOrb) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      entity.GetView().SetAnimation2D(entity.AnimationArray[0]);
      entity.GetView().GetAnimation2D().SetTime(0.0f);
      entity.GetView().GetAnimation2D().SetSpeed(1f);
      entity.GetShieldView().SetAnimation2D(entity.AnimationArray[3], true);
      entity.GetShieldView().GetAnimation2D().SetTime(0.0f);
      entity.GetShieldView().GetAnimation2D().SetSpeed(1f);
      this.m_manaToDrain = 0.0f;
      this.m_vulnerableTime = 10f;
      this.m_isVulnerable = false;
      entity.IsBroken = false;
      ((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetHero().EnableManaDrainFx();
    }

    public void Update(IFiniteStateMachine stateMachine, float gameTime)
    {
      MagicOrb entity = (MagicOrb) ((EntityFiniteStateMachine) stateMachine).GetEntity();
      if (entity.HitPoints <= 0)
      {
        this.m_isVulnerable = false;
        stateMachine.SetState((IState) MagicOrbStateBreak.GetInstance());
      }
      else
      {
        this.m_vulnerableTime -= gameTime;
        if ((double) this.m_vulnerableTime <= 0.0)
        {
          if (this.m_isVulnerable)
          {
            this.m_vulnerableTime = 10f;
            this.m_isVulnerable = false;
            entity.SetInvulnerable();
          }
          else
          {
            this.m_isVulnerable = true;
            this.m_vulnerableTime = 5f;
            entity.ChangeVulnerability();
          }
        }
        GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
        if (currentWorld.GetHero().ManaPoints <= 0)
          return;
        if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
          this.m_manaToDrain += 15f * gameTime;
        else
          this.m_manaToDrain += 10f * gameTime;
        if ((double) this.m_manaToDrain < 1.0)
          return;
        --currentWorld.GetHero().ManaPoints;
        --this.m_manaToDrain;
      }
    }

    public void Exit(IFiniteStateMachine stateMachine)
    {
    }
  }
}
