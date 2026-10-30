using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Resanctified.Engine.UI;

namespace Resanctified.Engine.Input
{
    public class InputManager : IGameComponent, Responder
    {
        private const int GAMEACTION_CAPACITY = 32;
        public const float INPUTMANAGER_DEFAULT_DEADZONE_X = 0.1f;
        public const float INPUTMANAGER_DEFAULT_DEADZONE_Y = 0.1f;
        private static readonly InputManager m_instance = new InputManager();
        private List<GameAction> m_gameActionList;
        private int m_padButtonsPressed;
        private int m_padButtonsReleased;
        private int m_padButtonsDown;
        // Pad buttons held through the keyboard (KeyBindings), kept apart from the touch-driven ones above
        // because they are re-read from the key state every frame instead of being set by events.
        private PSSButton m_keyButtonsDown;
        private PSSButton m_keyButtonsPressed;
        private PSSButton m_keyButtonsReleased;
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

        public static InputManager Instance => m_instance;

        public static InputManager GetInstance() => m_instance;

        public bool IsActive { get; set; }

        /// <summary>
        /// True since the last gameplay key press, false again as soon as the screen is touched (or clicked),
        /// so the on-screen controls can step aside while playing with the keyboard.
        /// </summary>
        public bool KeyboardInUse { get; private set; }

        public bool IsBackPressed() => this.StdBack.IsActive();

        public bool IsPausePressed() => this.StdPause.IsActive();

        // Alt+Enter is the desktop full-screen toggle, not a confirmation.
        public bool IsValidatePressed() => this.StdValidate.IsActive() && !this.m_keyboard.IsKeyDown(Keys.LeftAlt) && !this.m_keyboard.IsKeyDown(Keys.RightAlt);

        public bool IsKeyDown(Keys key) => this.m_keyboard.IsKeyDown(key);

        /// <summary>Holds these keys down on top of the real keyboard from the next update on (automated checks).</summary>
        public void SetInjectedKeys(IEnumerable<Keys> keys) => this.m_keyboard.SetInjectedKeys(keys);

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
            Application.GetInstance().AddResponder(this);
            this.m_deadZoneX = 0.1f;
            this.m_deadZoneY = 0.1f;
            this.m_shaking = false;
            this.m_shakeTime = -1f;
            this.m_keyButtonsDown = PSSButton.ButtonNone;
            this.m_keyButtonsPressed = PSSButton.ButtonNone;
            this.m_keyButtonsReleased = PSSButton.ButtonNone;
            this.KeyboardInUse = false;
            this.m_responders.Clear();
        }

        public void Cleanup()
        {
            this.m_pad.Cleanup();
            this.m_keyboard.Cleanup();
            this.m_gameActionList.Clear();
            Application.GetInstance().RemoveResponder(this);
            this.m_responders.Clear();
        }

        public void Update(float gameTime)
        {
            if (!this.IsActive)
                return;
            this.m_pad.Update(gameTime);
            this.m_keyboard.Update(gameTime);
            this.UpdateKeyButtons();
            for (int index = 0; index < this.m_gameActionList.Count; ++index)
                this.UpdateGameAction(this.m_gameActionList[index]);
            if (this.m_standardNavigationGameActionsEnabled)
            {
                this.UpdateStdGameAction(this.StdBack);
                this.UpdateStdGameAction(this.StdPause);
                this.UpdateStdGameAction(this.StdValidate);
            }
            this.m_padButtonsPressed = 0;
            this.m_padButtonsReleased = 0;
            if (this.m_shaking)
                this.m_shakeTime += gameTime;
            else
                this.m_shakeTime = -1f;
        }

        public void EnableStandardNavigationGameActions(PlayerIndex id) => this.m_standardNavigationGameActionsEnabled = true;

        public void DisableStandardNavigationGameActions() => this.m_standardNavigationGameActionsEnabled = false;

        private void UpdateKeyButtons()
        {
            PSSButton down = KeyBindings.GetGameplayButtons(this.m_keyboard.GetState());
            this.m_keyButtonsPressed = down & ~this.m_keyButtonsDown;
            this.m_keyButtonsReleased = this.m_keyButtonsDown & ~down;
            this.m_keyButtonsDown = down;
            if (this.m_keyButtonsPressed != PSSButton.ButtonNone || this.m_keyboard.IsKeyPressed(KeyBindings.Shake))
                this.KeyboardInUse = true;
        }

        private void UpdateGameAction(GameAction ga)
        {
            if (ga.IsDisabled)
            {
                ga.m_isActive = false;
            }
            else
            {
                PSSButton pressed = (PSSButton)this.m_padButtonsPressed | this.m_keyButtonsPressed;
                PSSButton down = (PSSButton)this.m_padButtonsDown | this.m_keyButtonsDown;
                PSSButton released = (PSSButton)this.m_padButtonsReleased | this.m_keyButtonsReleased;
                switch (ga.m_buttonState)
                {
                    case PSSButtonState.ButtonStatePressed:
                        ga.m_isActive = (pressed & ga.m_buttonBits) != PSSButton.ButtonNone;
                        break;
                    case PSSButtonState.ButtonStateDown:
                        ga.m_isActive = (down & ga.m_buttonBits) != PSSButton.ButtonNone;
                        break;
                    case PSSButtonState.ButtonStateReleased:
                        ga.m_isActive = (released & ga.m_buttonBits) != PSSButton.ButtonNone;
                        break;
                    case PSSButtonState.ButtonStateUp:
                        ga.m_isActive = (down & ga.m_buttonBits) == PSSButton.ButtonNone;
                        break;
                    default:
                        ga.m_isActive = false;
                        break;
                }
                GameAction gameAction = ga;
                gameAction.m_isActive = ((gameAction.m_isActive ? 1 : 0) | (this.m_shakeTime < 0.0 || ga.m_minShakeTime < 0.0 ? 0 : (this.m_shakeTime >= (double)ga.m_minShakeTime ? 1 : 0))) != 0;
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

        private bool IsButtonPressed(Buttons button)
        {
            Keys key = KeyBindings.GetKeyForButton(button);
            return this.m_pad.IsButtonPressed(button) || key != Keys.None && this.m_keyboard.IsKeyPressed(key);
        }

        private bool IsButtonDown(Buttons button)
        {
            Keys key = KeyBindings.GetKeyForButton(button);
            return this.m_pad.IsButtonDown(button) || key != Keys.None && this.m_keyboard.IsKeyDown(key);
        }

        private bool IsButtonReleased(Buttons button)
        {
            Keys key = KeyBindings.GetKeyForButton(button);
            return this.m_pad.IsButtonReleased(button) || key != Keys.None && this.m_keyboard.IsKeyReleased(key);
        }

        private bool IsButtonUp(Buttons button)
        {
            Keys key = KeyBindings.GetKeyForButton(button);
            return this.m_pad.IsButtonUp(button) && (key == Keys.None || this.m_keyboard.IsKeyUp(key));
        }

        public Vector2 GetThumbstickPosition(PSSThumbstick thumbstick) => this.m_thumbstickPositions[(int)thumbstick];

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
                    this.m_padButtonsPressed |= (int)buttonBits;
                    this.m_padButtonsDown |= (int)buttonBits;
                    break;
                case PSSButtonState.ButtonStateDown:
                    if (((PSSButton)this.m_padButtonsDown & buttonBits) == PSSButton.ButtonNone)
                        this.m_padButtonsPressed |= (int)buttonBits;
                    this.m_padButtonsDown |= (int)buttonBits;
                    break;
                case PSSButtonState.ButtonStateReleased:
                    this.m_padButtonsReleased |= (int)buttonBits;
                    this.m_padButtonsDown &= (int)~buttonBits;
                    break;
                case PSSButtonState.ButtonStateUp:
                    if (((PSSButton)this.m_padButtonsDown & buttonBits) != PSSButton.ButtonNone)
                        this.m_padButtonsReleased |= (int)buttonBits;
                    this.m_padButtonsDown &= (int)~buttonBits;
                    break;
            }
        }

        public void SetThumbstickPosition(PSSThumbstick thumbstick, Vector2 position)
        {
            this.m_thumbstickPositions[(int)thumbstick] = position;
            if (thumbstick == PSSThumbstick.LeftThumbstick)
            {
                if (position.X < -(double)this.m_deadZoneX)
                {
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStateUp);
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStateDown);
                }
                else if (position.X > (double)this.m_deadZoneX)
                {
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStateDown);
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStateUp);
                }
                else
                {
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickRight, PSSButtonState.ButtonStateUp);
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickLeft, PSSButtonState.ButtonStateUp);
                }
                if (position.Y < -(double)this.m_deadZoneY)
                {
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickUp, PSSButtonState.ButtonStateUp);
                    this.SetPadButtonsState(PSSButton.ButtonLeftThumbstickDown, PSSButtonState.ButtonStateDown);
                }
                else if (position.Y > (double)this.m_deadZoneY)
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
                if (position.X < -(double)this.m_deadZoneX)
                {
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickRight, PSSButtonState.ButtonStateUp);
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickLeft, PSSButtonState.ButtonStateDown);
                }
                else if (position.X > (double)this.m_deadZoneX)
                {
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickRight, PSSButtonState.ButtonStateDown);
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickLeft, PSSButtonState.ButtonStateUp);
                }
                else
                {
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickRight, PSSButtonState.ButtonStateUp);
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickLeft, PSSButtonState.ButtonStateUp);
                }
                if (position.Y < -(double)this.m_deadZoneY)
                {
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickUp, PSSButtonState.ButtonStateUp);
                    this.SetPadButtonsState(PSSButton.ButtonRightThumbstickDown, PSSButtonState.ButtonStateDown);
                }
                else if (position.Y > (double)this.m_deadZoneY)
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
            this.KeyboardInUse = false;
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
