using Resanctified.Engine.FSM;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateDead : IState
    {
        public void Enter(IFiniteStateMachine stateMachine) => ((PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity()).GetAnimationStateMachine().SetState(HeroAnimationStateDead.GetInstance());

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (stateMachine.GetCurrentStateTime() < 0.5)
                return;
            InGameStatePlay.GetInstance().GameOverRequest = true;
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
