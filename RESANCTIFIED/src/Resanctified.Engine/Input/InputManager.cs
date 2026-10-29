// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Input.InputManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Resanctified.Engine.UI;
using System.Collections.Generic;

namespace Resanctified.Engine.Input
{
  public class InputManager : Resanctified.Engine.IGameComponent, Responder
  {
    private const int GAMEACTION_CAPACITY = 32;
    public const float INPUTMANAGER_DEFAULT_DEADZONE_X = 0.1f;
    public const float INPUTMANAGER_DEFAULT_DEADZONE_Y = 0.1f;
    private static readonly InputManager m_instance = new InputManager();
    private List<GameAction> m_gameActionList;
    private int m_padButtonsPressed;
    private int m_padButtonsReleased;
    private int m_padButtonsDown;
    private Vector2[] m_thumbstickPositions;
    private float m_deadZoneX;
    private float m_deadZoneY;
    private float m_shakeTime;
    private bool m_shaking;
    public GameAction StdStart;
    public GameAction StdPause;
    public GameAction StdUp;
    public GameAction StdDown;
    public GameAction StdLeft;
    public GameAction StdRight;
    public GameAction StdValidate;
    public GameAction StdCancel;
    public GameAction StdBack;
    protected List<Responder> m_responders;
    private PadHandler m_pad;
    private KeyboardHandler m_keyboard;
    private bool m_standardNavigationGameActionsEnabled;

    private InputManager()
    {
      this.m_pad = new PadHandler(PlayerIndex.One);
      this.m_keyboard = new KeyboardHandler();
      this.m_gameActionList = new List<GameAction>();
      this.m_thumbstickPositions = new Vector2[2];
      this.m_deadZoneX = 0.0f;
      this.m_deadZoneY = 0.0f;
      this.IsActive = true;
      this.StdStart = new GameAction(PSSButton.ButtonStart, PSSButtonState.ButtonStatePressed);
      this.StdPause = new GameAction(PSSButton.ButtonStart, PSSButtonState.ButtonStatePressed);
      this.StdUp = new GameAction(PSSButton.ButtonDPadUp | PSSButton.ButtonLeftThumbstickUp, PSSButtonState.ButtonStatePressed);
      this.StdDown = new GameAction(PSSButton.ButtonDPadDown | PSSButton.ButtonLeftThumbstickDown, PSSButtonState.ButtonStatePressed);
      this.StdLeft = new GameAction(PSSButton.ButtonDPadLeft | PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStatePressed);
      this.StdRight = new GameAction(PSSButton.ButtonDPadRight | PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStatePressed);
      this.StdValidate = new GameAction(PSSButton.ButtonA, PSSButtonState.ButtonStatePressed);
      this.StdCancel = new GameAction(PSSButton.ButtonB, PSSButtonState.ButtonStatePressed);
      this.StdBack = new GameAction(PSSButton.ButtonB | PSSButton.ButtonBack, PSSButtonState.ButtonStatePressed);
      this.m_responders = new List<Responder>(64);
    }

    public static InputManager Instance => InputManager.m_instance;

    public static InputManager GetInstance() => InputManager.m_instance;

    public bool IsActive { get; set; }

    public bool IsBackPressed() => this.StdBack.IsActive();

    public void AddGameAction(GameAction action) => this.m_gameActionList.Add(action);

    public void RemoveGameAction(GameAction action) => this.m_gameActionList.Remove(action);

    public void ClearGameActionList() => this.m_gameActionList.Clear();

    public void Initialize()
    {
      this.m_pad.Initialize();
      this.m_keyboard.Initialize();
      this.DisableStandardNavigationGameActions();
      this.m_gameActionList.Clear();
      this.ClearPadState();
      Application.GetInstance().AddResponder((Responder) this);
      this.m_deadZoneX = 0.1f;
      this.m_deadZoneY = 0.1f;
      this.m_shaking = false;
      this.m_shakeTime = -1f;
      this.m_responders.Clear();
    }

    public void Cleanup()
    {
      this.m_pad.Cleanup();
      this.m_keyboard.Cleanup();
      this.m_gameActionList.Clear();
      Application.GetInstance().RemoveResponder((Responder) this);
      this.m_responders.Clear();
    }

    public void Update(float gameTime)
    {
      if (!this.IsActive)
        return;
      this.m_pad.Update(gameTime);
      this.m_keyboard.Update(gameTime);
      for (int index = 0; index < this.m_gameActionList.Count; ++index)
        this.UpdateGameAction(this.m_gameActionList[index]);
      if (this.m_standardNavigationGameActionsEnabled)
        this.UpdateStdGameAction(this.StdBack);
      this.m_padButtonsPressed = 0;
      this.m_padButtonsReleased = 0;
      if (this.m_shaking)
        this.m_shakeTime += gameTime;
      else
        this.m_shakeTime = -1f;
    }

    public void EnableStandardNavigationGameActions(PlayerIndex id) => this.m_standardNavigationGameActionsEnabled = true;

    public void DisableStandardNavigationGameActions() => this.m_standardNavigationGameActionsEnabled = false;

    private void UpdateGameAction(GameAction ga)
    {
      if (ga.IsDisabled)
      {
        ga.m_isActive = false;
      }
      else
      {
        switch (ga.m_buttonState)
        {
          case PSSButtonState.ButtonStatePressed:
            ga.m_isActive = ((PSSButton) this.m_padButtonsPressed & ga.m_buttonBits) != PSSButton.ButtonNone;
            break;
          case PSSButtonState.ButtonStateDown:
            ga.m_isActive = ((PSSButton) this.m_padButtonsDown & ga.m_buttonBits) != PSSButton.ButtonNone;
            break;
          case PSSButtonState.ButtonStateReleased:
            ga.m_isActive = ((PSSButton) this.m_padButtonsReleased & ga.m_buttonBits) != PSSButton.ButtonNone;
            break;
          case PSSButtonState.ButtonStateUp:
            ga.m_isActive = ((PSSButton) this.m_padButtonsDown & ga.m_buttonBits) == PSSButton.ButtonNone;
            break;
          default:
            ga.m_isActive = false;
            break;
        }
        GameAction gameAction = ga;
        gameAction.m_isActive = ((gameAction.m_isActive ? 1 : 0) | ((double) this.m_shakeTime < 0.0 || (double) ga.m_minShakeTime < 0.0 ? 0 : ((double) this.m_shakeTime >= (double) ga.m_minShakeTime ? 1 : 0))) != 0;
      }
    }

    private void UpdateStdGameAction(GameAction ga)
    {
      ga.m_isActive = false;
      if (ga.IsDisabled)
        return;
      for (int index = 0; index < ga.GetButtonList().Count; ++index)
      {
        switch (ga.m_buttonState)
        {
          case PSSButtonState.ButtonStatePressed:
            if (this.IsButtonPressed(ga.GetButtonList()[index]))
            {
              ga.m_isActive = true;
              break;
            }
            break;
          case PSSButtonState.ButtonStateDown:
            if (this.IsButtonDown(ga.GetButtonList()[index]))
            {
              ga.m_isActive = true;
              break;
            }
            break;
          case PSSButtonState.ButtonStateReleased:
            if (this.IsButtonReleased(ga.GetButtonList()[index]))
            {
              ga.m_isActive = true;
              break;
            }
            break;
          case PSSButtonState.ButtonStateUp:
            if (this.IsButtonUp(ga.GetButtonList()[index]))
            {
              ga.m_isActive = true;
              break;
            }
            break;
        }
      }
    }

    // Desktop keyboard stand-ins for the phone's hardware buttons: Escape is the Windows Phone "Back" key.
    private static Keys GetKeyForButton(Buttons button) => button == Buttons.Back ? Keys.Escape : Keys.None;

    private bool IsButtonPressed(Buttons button)
    {
      Keys key = InputManager.GetKeyForButton(button);
      return this.m_pad.IsButtonPressed(button) || key != Keys.None && this.m_keyboard.IsKeyPressed(key);
    }

    private bool IsButtonDown(Buttons button)
    {
      Keys key = InputManager.GetKeyForButton(button);
      return this.m_pad.IsButtonDown(button) || key != Keys.None && this.m_keyboard.IsKeyDown(key);
    }

    private bool IsButtonReleased(Buttons button)
    {
      Keys key = InputManager.GetKeyForButton(button);
      return this.m_pad.IsButtonReleased(button) || key != Keys.None && this.m_keyboard.IsKeyReleased(key);
    }

    private bool IsButtonUp(Buttons button)
    {
      Keys key = InputManager.GetKeyForButton(button);
      return this.m_pad.IsButtonUp(button) && (key == Keys.None || this.m_keyboard.IsKeyUp(key));
    }

    public Vector2 GetThumbstickPosition(PSSThumbstick thumbstick) => this.m_thumbstickPositions[(int) thumbstick];

    public void ClearPadState()
    {
      this.m_padButtonsPressed = 0;
      this.m_padButtonsReleased = 0;
      this.m_padButtonsDown = 0;
      this.m_thumbstickPositions[0] = Vector2.Zero;
      this.m_thumbstickPositions[1] = Vector2.Zero;
    }

    public void SetDeadZones(float deadZoneX, float deadZoneY)
    {
      this.m_deadZoneX = deadZoneX;
      this.m_deadZoneY = deadZoneY;
    }

    public void SetPadButtonsState(PSSButton buttonBits, PSSButtonState buttonState)
    {
      switch (buttonState)
      {
        case PSSButtonState.ButtonStatePressed:
          this.m_padButtonsPressed |= (int) buttonBits;
          this.m_padButtonsDown |= (int) buttonBits;
          break;
        case PSSButtonState.ButtonStateDown:
          if (((PSSButton) this.m_padButtonsDown & buttonBits) == PSSButton.ButtonNone)
            this.m_padButtonsPressed |= (int) buttonBits;
          this.m_padButtonsDown |= (int) buttonBits;
          break;
        case PSSButtonState.ButtonStateReleased:
          this.m_padButtonsReleased |= (int) buttonBits;
          this.m_padButtonsDown &= (int) ~buttonBits;
          break;
        case PSSButtonState.ButtonStateUp:
          if (((PSSButton) this.m_padButtonsDown & buttonBits) != PSSButton.ButtonNone)
            this.m_padButtonsReleased |= (int) buttonBits;
          this.m_padButtonsDown &= (int) ~buttonBits;
          break;
      }
    }

    public void SetThumbstickPosition(PSSThumbstick thumbstick, Vector2 position)
    {
      this.m_thumbstickPositions[(int) thumbstick] = position;
      if (thumbstick == PSSThumbstick.LeftThumbstick)
      {
        if ((double) position.X < -(double) this.m_deadZoneX)
        {
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStateDown);
        }
        else if ((double) position.X > (double) this.m_deadZoneX)
        {
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStateDown);
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStateUp);
        }
        else
        {
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStateUp);
        }
        if ((double) position.Y < -(double) this.m_deadZoneY)
        {
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickUp, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickDown, PSSButtonState.ButtonStateDown);
        }
        else if ((double) position.Y > (double) this.m_deadZoneY)
        {
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickUp, PSSButtonState.ButtonStateDown);
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickDown, PSSButtonState.ButtonStateUp);
        }
        else
        {
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickUp, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickDown, PSSButtonState.ButtonStateUp);
        }
      }
      else
      {
        if ((double) position.X < -(double) this.m_deadZoneX)
        {
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickRight, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickLeft, PSSButtonState.ButtonStateDown);
        }
        else if ((double) position.X > (double) this.m_deadZoneX)
        {
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickRight, PSSButtonState.ButtonStateDown);
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickLeft, PSSButtonState.ButtonStateUp);
        }
        else
        {
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickRight, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickLeft, PSSButtonState.ButtonStateUp);
        }
        if ((double) position.Y < -(double) this.m_deadZoneY)
        {
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickUp, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickDown, PSSButtonState.ButtonStateDown);
        }
        else if ((double) position.Y > (double) this.m_deadZoneY)
        {
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickUp, PSSButtonState.ButtonStateDown);
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickDown, PSSButtonState.ButtonStateUp);
        }
        else
        {
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickUp, PSSButtonState.ButtonStateUp);
          this.SetPadButtonsState(PSSButton.ButtonRightThumbstickDown, PSSButtonState.ButtonStateUp);
        }
      }
    }

    public void AddResponder(Responder responder) => this.m_responders.Add(responder);

    public void RemoveResponder(Responder responder) => this.m_responders.Remove(responder);

    public void TouchesBegan(Touch[] touchArray, int touchCount)
    {
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesBegan(touchArray, touchCount);
    }

    public void TouchesMoved(Touch[] touchArray, int touchCount)
    {
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesMoved(touchArray, touchCount);
    }

    public void TouchesEnded(Touch[] touchArray, int touchCount)
    {
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesEnded(touchArray, touchCount);
    }

    public void TouchesCancelled(Touch[] touchArray, int touchCount)
    {
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesCancelled(touchArray, touchCount);
    }

    public void ShakeBegin()
    {
      this.m_shakeTime = 0.0f;
      this.m_shaking = true;
    }

    public void ShakeEnded() => this.m_shaking = false;

    public void ShakeCancelled() => this.m_shaking = false;
  }
}
