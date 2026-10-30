using System.Collections.Generic;
using Resanctified.Engine.Input;
using Resanctified.Engine.UI;
using Xunit;

namespace Resanctified.Tests
{
    public class ButtonClickTests
    {
        private sealed class RecordingViewController : ViewController
        {
            public readonly List<(View Sender, PSSButtonState State)> Actions = new List<(View, PSSButtonState)>();

            public override void Action(View sender, ActionEvent evt) => this.Actions.Add((sender, ((ButtonActionEvent)evt).buttonState));
        }

        [Fact]
        public void ClickActsLikeATap()
        {
            RecordingViewController vc = new RecordingViewController();
            Button button = new Button();
            button.Initialize(vc, 56f, 56f);

            button.Click();

            Assert.Equal(new[] { (button as View, PSSButtonState.ButtonStatePressed), (button, PSSButtonState.ButtonStateReleased) }, vc.Actions);
        }

        [Fact]
        public void InactiveButtonIgnoresClick()
        {
            RecordingViewController vc = new RecordingViewController();
            Button button = new Button();
            button.Initialize(vc, 56f, 56f);
            button.SetActive(false);

            button.Click();

            Assert.Empty(vc.Actions);
        }

        [Fact]
        public void CleanedUpButtonIgnoresClick()
        {
            RecordingViewController vc = new RecordingViewController();
            Button button = new Button();
            button.Initialize(vc, 56f, 56f);
            button.Cleanup();

            button.Click();

            Assert.Empty(vc.Actions);
        }
    }
}
