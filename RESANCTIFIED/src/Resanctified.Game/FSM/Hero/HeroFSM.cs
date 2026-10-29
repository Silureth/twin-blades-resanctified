// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.FSM.Hero.HeroFSM
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Game.FSM.Hero.HeroState;
using Resanctified.Game.World.Entities;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.FSM.Hero
{
  public class HeroFSM : EntityFiniteStateMachine
  {
    public HeroFSM.HeroActionId NextAction;
    public HeroStateStand StateStand;
    public HeroStateWalk StateWalk;
    public HeroStateSlide StateSlide;
    public HeroStateJump StateJump;
    public HeroStateAir StateAir;
    public HeroStateLand StateLand;
    public HeroStateStandFire StateStandFire;
    public HeroStateWalkFire StateWalkFire;
    public HeroStateSlash StateSlash;
    public HeroStateHit StateHit;
    public HeroStateGrabbed StateGrabbed;
    public HeroStatePushBack StatePushBack;
    public HeroStatePushed StatePushed;
    public HeroStateDying StateDying;
    public HeroStateDead StateDead;
    public HeroStateScript StateScript;
    private GameAction[] m_gameActionArray;

    public HeroFSM(IEntity entity)
      : base(entity)
    {
      this.m_gameActionArray = new GameAction[9];
      this.m_gameActionArray[0] = new GameAction(PSSButton.ButtonDPadLeft | PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStateDown);
      this.m_gameActionArray[1] = new GameAction(PSSButton.ButtonDPadRight | PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStateDown);
      this.m_gameActionArray[2] = new GameAction(PSSButton.ButtonDPadUp | PSSButton.ButtonLeftThumbstickUp, PSSButtonState.ButtonStateDown);
      this.m_gameActionArray[3] = new GameAction(PSSButton.ButtonB, PSSButtonState.ButtonStatePressed);
      this.m_gameActionArray[4] = new GameAction(PSSButton.ButtonX, PSSButtonState.ButtonStatePressed);
      this.m_gameActionArray[5] = new GameAction(PSSButton.ButtonX, PSSButtonState.ButtonStateDown);
      this.m_gameActionArray[6] = new GameAction(PSSButton.ButtonRightShoulder, PSSButtonState.ButtonStatePressed);
      this.m_gameActionArray[7] = new GameAction(PSSButton.ButtonLeftShoulder, PSSButtonState.ButtonStatePressed);
      this.m_gameActionArray[8] = new GameAction(PSSButton.ButtonNone, PSSButtonState.ButtonStateNone, 0.0f);
      this.NextAction = HeroFSM.HeroActionId.None;
      this.StateStand = new HeroStateStand();
      this.StateWalk = new HeroStateWalk();
      this.StateSlide = new HeroStateSlide();
      this.StateJump = new HeroStateJump();
      this.StateAir = new HeroStateAir();
      this.StateLand = new HeroStateLand();
      this.StateStandFire = new HeroStateStandFire();
      this.StateWalkFire = new HeroStateWalkFire();
      this.StateSlash = new HeroStateSlash();
      this.StateHit = new HeroStateHit();
      this.StateGrabbed = new HeroStateGrabbed();
      this.StatePushBack = new HeroStatePushBack();
      this.StatePushed = new HeroStatePushed();
      this.StateDying = new HeroStateDying();
      this.StateDead = new HeroStateDead();
      this.StateScript = new HeroStateScript();
    }

    public override void Initialize()
    {
      for (int index = 0; index < 9; ++index)
        InputManager.GetInstance().AddGameAction(this.m_gameActionArray[index]);
      this.SetState((IState) this.StateStand);
    }

    public override void Cleanup()
    {
      for (int index = 0; index < 9; ++index)
        InputManager.GetInstance().RemoveGameAction(this.m_gameActionArray[index]);
    }

    public void UpdateNextAction()
    {
      if (this.NextAction != HeroFSM.HeroActionId.None)
        return;
      if (this.m_gameActionArray[3].IsActive())
        this.NextAction = HeroFSM.HeroActionId.Slash;
      else if (this.m_gameActionArray[2].IsActive())
        this.NextAction = HeroFSM.HeroActionId.Jump;
      else if (this.m_gameActionArray[4].IsActive())
      {
        this.NextAction = HeroFSM.HeroActionId.Fire;
      }
      else
      {
        if (!((PlayableCharacter) this.GetEntity()).GetGun().IsAutofireAvailable || !this.m_gameActionArray[5].IsActive())
          return;
        this.NextAction = HeroFSM.HeroActionId.Fire;
      }
    }

    public GameAction GetGameActionFromId(HeroFSM.HeroGameActionId index) => this.m_gameActionArray[(int) index];

    public void SetGameActionsDisabled(bool flag)
    {
      for (int index = 0; index < 9; ++index)
        this.m_gameActionArray[index].IsDisabled = flag;
      this.NextAction = HeroFSM.HeroActionId.None;
    }

    public enum HeroActionId
    {
      None,
      Slash,
      Fire,
      Jump,
    }

    public enum HeroGameActionId
    {
      Left,
      Right,
      Jump,
      Slash,
      Fire,
      AutoFire,
      NextWeapon,
      PrevWeapon,
      PushBack,
      ArraySize,
    }
  }
}
