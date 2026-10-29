using Microsoft.Xna.Framework;
using Resanctified.Engine;
using Resanctified.Engine.FSM;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateInit : IDrawableState, IState
    {
        private static readonly GameStateInit s_instance = new GameStateInit();

        public static GameStateInit Instance => s_instance;

        public static GameStateInit GetInstance() => s_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            GameApplication.Instance.LoadSettings();
            GameApplication.Instance.SetSoundVolume();
            GameLocalisation.GetInstance().Initialize(GameApplication.Instance.CurrentLanguage);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime) => stateMachine.SetState(GameStateLogoMGS.Instance);

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }

        public void Draw(IFiniteStateMachine stateMachine) => Application.Instance.GraphicsDevice.Clear(Color.Black);
    }
}
