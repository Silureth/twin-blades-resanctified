using Resanctified.Engine.FSM;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.FSM.NPC.Orb.State
{
    public class MagicOrbStateRestoring : IState
    {
        private const float ORB_SHIELD_SCALE_START_TIME = 0.5f;
        private const float ORB_SHIELD_SCALE_TIME = 1f;
        private static MagicOrbStateRestoring m_instance = new MagicOrbStateRestoring();

        public static MagicOrbStateRestoring Instance => m_instance;

        public static MagicOrbStateRestoring GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            MagicOrb entity = (MagicOrb)((EntityFiniteStateMachine)stateMachine).GetEntity();
            entity.GetView().SetAnimation2D(entity.AnimationArray[2]);
            entity.GetView().GetAnimation2D().SetTime(0.0f);
            entity.GetView().GetAnimation2D().SetSpeed(1f);
            entity.GetShieldView().SetAnimation2D(entity.AnimationArray[3], true);
            entity.GetShieldView().GetAnimation2D().SetTime(0.0f);
            entity.GetShieldView().GetAnimation2D().SetSpeed(1f);
            entity.GetShieldView().SetCustomColor(GameConstant.SHIELD_INVULNERABLE_COLOR);
            entity.GetShieldView().SetCustomColorEnabled(true);
            entity.GetShieldView().GetTransform().SetScale(0.0f);
            entity.GetView().AddSubview(entity.GetShieldView());
            entity.SetInvulnerable();
            entity.GetSound(MagicOrb.SoundFxId.SoundFx_Restore).Play();
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (stateMachine.CurrentStateTime <= 0.5)
                return;
            MagicOrb entity = (MagicOrb)((EntityFiniteStateMachine)stateMachine).GetEntity();
            float s = (float)(0.800000011920929 * (double)((float)stateMachine.CurrentStateTime - 0.5f) / 1.0);
            if ((double)s >= 0.800000011920929)
            {
                entity.GetShieldView().GetTransform().SetScale(0.8f);
                entity.HitPoints = GameApplication.GetInstance().GetDifficulty() != GameApplication.GameDifficulty.Hard ? 500 : 800;
                stateMachine.SetState(MagicOrbStateStand.GetInstance());
            }
            else
                entity.GetShieldView().GetTransform().SetScale(s);
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
        }
    }
}
