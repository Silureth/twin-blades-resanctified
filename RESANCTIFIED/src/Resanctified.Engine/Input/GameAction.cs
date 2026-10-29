using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace Resanctified.Engine.Input
{
    public class GameAction
    {
        internal PSSButton m_buttonBits;
        internal PSSButtonState m_buttonState;
        internal bool m_isActive;
        internal float m_minShakeTime;
        private List<Buttons> m_buttonList;

        internal List<Buttons> GetButtonList() => this.m_buttonList;

        /// <param name="minShakeTime">Shake duration that also activates this action; negative means shaking never does.</param>
        public GameAction(PSSButton buttonBits, PSSButtonState state, float minShakeTime)
        {
            this.m_buttonList = new List<Buttons>(8);
            this.m_buttonBits = buttonBits;
            this.m_buttonState = state;
            this.m_minShakeTime = minShakeTime;
            this.m_isActive = false;
            this.IsDisabled = false;
            this.IsDisableAllowed = false;
            this.AddPSSButton(buttonBits);
        }

        public GameAction(PSSButton buttonBits, PSSButtonState state)
          : this(buttonBits, state, -1f)
        {
        }

        public GameAction(PSSButton buttonBits)
          : this(buttonBits, PSSButtonState.ButtonStateDown, -1f)
        {
        }

        public bool IsActive() => this.m_isActive;

        public bool IsDisableAllowed { get; set; }

        public bool IsDisabled { get; set; }

        public void AddPSSButton(PSSButton buttonBits)
        {
            if ((buttonBits & PSSButton.ButtonA) != PSSButton.ButtonNone)
                this.AddButton(Buttons.A);
            if ((buttonBits & PSSButton.ButtonB) != PSSButton.ButtonNone)
                this.AddButton(Buttons.B);
            if ((buttonBits & PSSButton.ButtonBack) != PSSButton.ButtonNone)
                this.AddButton(Buttons.Back);
            if ((buttonBits & PSSButton.ButtonDPadDown) != PSSButton.ButtonNone)
                this.AddButton(Buttons.DPadDown);
            if ((buttonBits & PSSButton.ButtonDPadLeft) != PSSButton.ButtonNone)
                this.AddButton(Buttons.DPadLeft);
            if ((buttonBits & PSSButton.ButtonDPadRight) != PSSButton.ButtonNone)
                this.AddButton(Buttons.DPadRight);
            if ((buttonBits & PSSButton.ButtonDPadUp) != PSSButton.ButtonNone)
                this.AddButton(Buttons.DPadUp);
            if ((buttonBits & PSSButton.ButtonStart) != PSSButton.ButtonNone)
                this.AddButton(Buttons.Start);
            if ((buttonBits & PSSButton.ButtonX) != PSSButton.ButtonNone)
                this.AddButton(Buttons.X);
            if ((buttonBits & PSSButton.ButtonY) != PSSButton.ButtonNone)
                this.AddButton(Buttons.Y);
            if ((buttonBits & PSSButton.ButtonLeftThumbstickDown) != PSSButton.ButtonNone)
                this.AddButton(Buttons.LeftThumbstickDown);
            if ((buttonBits & PSSButton.ButtonLeftThumbstickLeft) != PSSButton.ButtonNone)
                this.AddButton(Buttons.LeftThumbstickLeft);
            if ((buttonBits & PSSButton.ButtonLeftThumbstickRight) != PSSButton.ButtonNone)
                this.AddButton(Buttons.LeftThumbstickRight);
            if ((buttonBits & PSSButton.ButtonLeftThumbstickUp) != PSSButton.ButtonNone)
                this.AddButton(Buttons.LeftThumbstickUp);
            if ((buttonBits & PSSButton.ButtonRightThumbstickDown) != PSSButton.ButtonNone)
                this.AddButton(Buttons.RightThumbstickDown);
            if ((buttonBits & PSSButton.ButtonRightThumbstickLeft) != PSSButton.ButtonNone)
                this.AddButton(Buttons.RightThumbstickLeft);
            if ((buttonBits & PSSButton.ButtonRightThumbstickRight) != PSSButton.ButtonNone)
                this.AddButton(Buttons.RightThumbstickRight);
            if ((buttonBits & PSSButton.ButtonRightThumbstickUp) != PSSButton.ButtonNone)
                this.AddButton(Buttons.RightThumbstickUp);
            if ((buttonBits & PSSButton.ButtonLeftTrigger) != PSSButton.ButtonNone)
                this.AddButton(Buttons.LeftTrigger);
            if ((buttonBits & PSSButton.ButtonRightTrigger) != PSSButton.ButtonNone)
                this.AddButton(Buttons.RightTrigger);
            if ((buttonBits & PSSButton.ButtonLeftShoulder) != PSSButton.ButtonNone)
                this.AddButton(Buttons.LeftShoulder);
            if ((buttonBits & PSSButton.ButtonRightShoulder) == PSSButton.ButtonNone)
                return;
            this.AddButton(Buttons.RightShoulder);
        }

        public void AddButton(Buttons button) => this.m_buttonList.Add(button);
    }
}
