using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
    public class BulletImpact : Entity
    {
        private ImpactType m_impactType;
        private Animation2DView m_view;
        private Animation2D[] m_animArray;
        private Sound[] m_soundArray;

        public BulletImpact()
        {
            this.m_impactType = ImpactType.Blood;
            this.IsActive = false;
            this.m_view = new Animation2DView();
            this.m_animArray = new Animation2D[5];
            for (int index = 0; index < 5; ++index)
                this.m_animArray[index] = new Animation2D();
            this.m_soundArray = new Sound[4];
            for (int index = 0; index < 4; ++index)
                this.m_soundArray[index] = new Sound();
        }

        public override void Initialize()
        {
            this.LoadContent();
            this.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
            this.m_view.GetTransform().SetPosition(0.0f, 0.0f, 3.5f);
            this.m_view.SetAnimation2D(this.m_animArray[0]);
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.IsActive = false;
        }

        public override void Update(float gameTime) => this.m_view.Update(gameTime);

        public override void Cleanup()
        {
            for (int index = 0; index < 5; ++index)
                this.m_animArray[index].Cleanup();
            this.m_view.Cleanup();
            for (int index = 0; index < 4; ++index)
                this.m_soundArray[index].Cleanup();
        }

        public void SetImpactType(ImpactType type)
        {
            this.m_impactType = type;
            switch (type)
            {
                case ImpactType.Blood:
                    this.m_view.SetAnimation2D(this.m_animArray[0]);
                    break;
                case ImpactType.Armor:
                    this.m_view.SetAnimation2D(this.m_animArray[1]);
                    break;
                case ImpactType.Burn:
                    this.m_view.SetAnimation2D(this.m_animArray[2]);
                    break;
                case ImpactType.Ice:
                    this.m_view.SetAnimation2D(this.m_animArray[3]);
                    break;
                case ImpactType.Holy:
                    this.m_view.SetAnimation2D(this.m_animArray[4]);
                    break;
            }
        }

        public Animation2DView GetView() => this.m_view;

        public Sound GetSound()
        {
            switch (this.m_impactType)
            {
                case ImpactType.Blood:
                    return this.m_soundArray[0];
                case ImpactType.Armor:
                    return this.m_soundArray[1];
                case ImpactType.Burn:
                case ImpactType.Holy:
                    return null;
                case ImpactType.Ice:
                    return this.m_soundArray[3];
                default:
                    return null;
            }
        }

        private void LoadContent()
        {
            this.m_animArray[0].Initialize("Anims\\fx_impact_blood");
            this.m_animArray[1].Initialize("Anims\\fx_impact_armor");
            this.m_animArray[2].Initialize("Anims\\fx_impact_burn");
            this.m_animArray[3].Initialize("Anims\\fx_impact_ice");
            this.m_animArray[4].Initialize("Anims\\fx_impact_holy");
            this.m_soundArray[0].Initialize("Sounds\\zombie_hit01", true);
            this.m_soundArray[1].Initialize("Sounds\\zombie_hit_armor", true);
            this.m_soundArray[2].Initialize("Sounds\\hero_weapon_flame_start", true);
            this.m_soundArray[3].Initialize("Sounds\\zombie_iced", true);
        }

        public enum ImpactType
        {
            Blood,
            Armor,
            Burn,
            Ice,
            Holy,
        }

        public enum ImpactAnimationId
        {
            Blood,
            Armor,
            Burn,
            Ice,
            Holy,
            ArraySize,
        }

        public enum SoundFxId
        {
            SoundFx_Blood,
            SoundFx_Armor,
            SoundFx_Burn,
            SoundFx_Ice,
            SoundFx_ArraySize,
        }
    }
}
