using Resanctified.Engine.FSM;
using Resanctified.Engine.World;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.World;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.Hero.HeroState
{
    public class HeroStateGrabbed : IState
    {
        public void Enter(IFiniteStateMachine stateMachine)
        {
            PlayableCharacter entity = (PlayableCharacter)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetAnimationStateMachine().SetState(HeroAnimationStateGrabbed.GetInstance());
            entity.GetPhysics().Velocity.X = 0.0f;
            entity.GetPhysics().Acceleration.X = 0.0f;
            ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHud().ShowShakeAnim(true);
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            HeroFSM heroFsm = (HeroFSM)stateMachine;
            PlayableCharacter entity = (PlayableCharacter)heroFsm.GetEntity();
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            if (entity.HitPoints <= 0)
            {
                currentWorld.GetHud().ShowShakeAnim(false);
                stateMachine.SetState(heroFsm.StateDying);
            }
            else
            {
                if (!heroFsm.GetGameActionFromId(HeroFSM.HeroGameActionId.PushBack).IsActive() && heroFsm.GetCurrentStateTime() <= 3.0)
                    return;
                entity.IsGrabbed = false;
                currentWorld.GetHud().ShowShakeAnim(false);
                stateMachine.SetState(heroFsm.StatePushBack);
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
