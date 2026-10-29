using Microsoft.Xna.Framework;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.FSM.NPC.Orb.State;

namespace Resanctified.Game.World.Entities
{
    public class MagicOrb : Entity
    {
        private const int FXMANADRAIN_NB_MAX = 8;
        private const float FXMANADRAIN_EMIT_TIME = 0.2f;
        private Animation2DView m_view;
        private Animation2DView m_shieldView;
        private Sound[] m_soundFxArray;
        public bool IsHit;
        public int HitPoints;
        public bool IsBroken;
        public bool IsDestroyed;
        private NPCZombie.HitType m_currentHitType;
        private NPCNecromancer.VulnerabilityType m_currentVulnerabilityType;
        private NPCNecromancer.VulnerabilityType m_previousVulnerabilityType;
        private bool m_shieldBounce;
        private EntityFiniteStateMachine m_stateMachine;
        public Animation2D[] AnimationArray;
        private FxManaDrain[] m_manaDrainArray;
        private float m_timer;
        private bool m_shieldTransition;
        private float m_shieldTransitionTimer;

        public NPCNecromancer.VulnerabilityType GetVulnerabilityType() => this.m_currentVulnerabilityType;

        public void SetInvulnerable()
        {
            this.m_currentVulnerabilityType = NPCNecromancer.VulnerabilityType.None;
            this.m_shieldTransition = true;
        }

        public bool IsInvulnerable() => this.m_currentVulnerabilityType == NPCNecromancer.VulnerabilityType.None;

        public void ChangeVulnerability()
        {
            this.m_currentVulnerabilityType = this.m_previousVulnerabilityType != NPCNecromancer.VulnerabilityType.Ice ? this.m_previousVulnerabilityType + 1 : NPCNecromancer.VulnerabilityType.Bullet;
            this.m_previousVulnerabilityType = this.m_currentVulnerabilityType;
            this.m_shieldTransition = true;
        }

        public MagicOrb()
        {
            this.m_stateMachine = new EntityFiniteStateMachine(this);
            this.m_view = new Animation2DView();
            this.m_shieldView = new Animation2DView();
            this.m_soundFxArray = new Sound[4];
            for (int index = 0; index < 4; ++index)
                this.m_soundFxArray[index] = new Sound();
            this.IsDestroyed = false;
            this.IsBroken = false;
            this.m_currentVulnerabilityType = NPCNecromancer.VulnerabilityType.None;
            this.m_previousVulnerabilityType = this.m_currentVulnerabilityType;
            this.IsHit = false;
            this.HitPoints = 500;
            this.AnimationArray = new Animation2D[6];
            for (int index = 0; index < 6; ++index)
                this.AnimationArray[index] = new Animation2D();
            this.m_manaDrainArray = new FxManaDrain[8];
            for (int index = 0; index < 8; ++index)
                this.m_manaDrainArray[index] = new FxManaDrain();
            this.m_timer = 0.0f;
            this.m_type = 7;
            this.m_shieldTransition = false;
            this.m_shieldTransitionTimer = 0.0f;
        }

        public override void Initialize()
        {
            this.IsHit = false;
            this.HitPoints = GameApplication.GetInstance().GetDifficulty() != GameApplication.GameDifficulty.Hard ? 500 : 800;
            this.IsDestroyed = false;
            this.IsBroken = false;
            this.m_timer = 0.0f;
            this.m_currentVulnerabilityType = NPCNecromancer.VulnerabilityType.None;
            this.m_previousVulnerabilityType = this.m_currentVulnerabilityType;
            this.LoadContent();
            this.m_stateMachine.Initialize();
            this.m_view.Initialize();
            this.m_view.GetTransform().SetPosition(0.0f, 0.0f, -2f);
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shieldView.Initialize();
            this.m_shieldView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shieldView.GetTransform().SetPosition(0.0f, 0.9f, 1f);
            this.m_shieldView.GetTransform().SetScale(0.8f);
            this.m_shieldView.SetAnimation2D(this.AnimationArray[3]);
            this.m_shieldView.GetAnimation2D().SetTime(0.0f);
            this.m_shieldView.GetAnimation2D().SetSpeed(1f);
            this.m_shieldView.SetCustomColor(GameConstant.SHIELD_INVULNERABLE_COLOR);
            this.m_shieldView.SetCustomColorEnabled(true);
            this.m_shieldTransition = false;
            this.m_shieldTransitionTimer = 0.0f;
            for (int index = 0; index < 8; ++index)
                this.m_manaDrainArray[index].Initialize();
            this.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
            this.m_stateMachine.SetState(MagicOrbStateStand.GetInstance());
            Renderer.GetInstance().AddTopLevelView(this.m_view, this.GetTransform());
            Renderer.GetInstance().AddTopLevelView(this.m_shieldView, this.GetTransform());
        }

        private void UpdateShieldColor(float time)
        {
            Color color1 = GameConstant.SHIELD_INVULNERABLE_COLOR;
            switch (this.GetVulnerabilityType())
            {
                case NPCNecromancer.VulnerabilityType.Bullet:
                    color1 = GameConstant.SHIELD_BULLET_COLOR;
                    break;
                case NPCNecromancer.VulnerabilityType.Fire:
                    color1 = GameConstant.SHIELD_FIRE_COLOR;
                    break;
                case NPCNecromancer.VulnerabilityType.Ice:
                    color1 = GameConstant.SHIELD_ICE_COLOR;
                    break;
            }
            if (!this.m_shieldTransition)
            {
                this.GetShieldView().SetCustomColor(color1);
                this.GetShieldView().SetCustomColorEnabled(true);
                this.m_shieldTransitionTimer = 0.0f;
            }
            else
            {
                Color color2 = GameConstant.SHIELD_INVULNERABLE_COLOR;
                if (this.GetVulnerabilityType() == NPCNecromancer.VulnerabilityType.None)
                {
                    switch (this.m_previousVulnerabilityType)
                    {
                        case NPCNecromancer.VulnerabilityType.Bullet:
                            color2 = GameConstant.SHIELD_BULLET_COLOR;
                            break;
                        case NPCNecromancer.VulnerabilityType.Fire:
                            color2 = GameConstant.SHIELD_FIRE_COLOR;
                            break;
                        case NPCNecromancer.VulnerabilityType.Ice:
                            color2 = GameConstant.SHIELD_ICE_COLOR;
                            break;
                    }
                }
                this.m_shieldTransitionTimer += time;
                if (this.m_shieldTransitionTimer >= (double)GameConstant.SHIELD_COLOR_TRANSITION_TIME)
                    this.m_shieldTransition = false;
                float num1 = this.m_shieldTransitionTimer / GameConstant.SHIELD_COLOR_TRANSITION_TIME;
                int num2 = (int)(color1.R * (double)num1 + color2.R * (1.0 - (double)num1));
                if (num2 > byte.MaxValue)
                    num2 = byte.MaxValue;
                color1.R = (byte)num2;
                int num3 = (int)(color1.G * (double)num1 + color2.G * (1.0 - (double)num1));
                if (num3 > byte.MaxValue)
                    num3 = byte.MaxValue;
                color1.G = (byte)num3;
                int num4 = (int)(color1.B * (double)num1 + color2.B * (1.0 - (double)num1));
                if (num4 > byte.MaxValue)
                    num4 = byte.MaxValue;
                color1.B = (byte)num4;
                this.GetShieldView().SetCustomColor(color1);
                this.GetShieldView().SetCustomColorEnabled(true);
            }
        }

        public override void Update(float gameTime)
        {
            this.m_stateMachine.Update(gameTime);
            this.m_view.Update(gameTime);
            this.m_shieldView.Update(gameTime);
            this.UpdateShieldColor(gameTime);
            if (this.m_shieldBounce && !this.IsBroken && this.m_shieldView.GetAnimation2D().IsEndOfAnimationReached())
            {
                this.m_shieldView.SetAnimation2D(this.AnimationArray[3], true);
                this.m_shieldView.GetAnimation2D().SetTime(0.0f);
                this.m_shieldBounce = false;
            }
            if (this.IsBroken)
            {
                for (int index = 0; index < 8; ++index)
                {
                    if (this.m_manaDrainArray[index].IsActive)
                        this.m_manaDrainArray[index].Destroy();
                }
            }
            else
            {
                this.m_timer += gameTime;
                if (this.m_timer >= 0.200000002980232)
                {
                    for (int index = 0; index < 8; ++index)
                    {
                        if (!this.m_manaDrainArray[index].IsActive)
                        {
                            this.m_manaDrainArray[index].Emit();
                            this.m_timer = 0.0f;
                            break;
                        }
                    }
                }
                for (int index = 0; index < 8; ++index)
                    this.m_manaDrainArray[index].Update(gameTime);
            }
            this.UpdateCollisionBoxes(this.GetView().GetAnimation2D(), "hit");
            base.Update(gameTime);
        }

        public override void Cleanup()
        {
            this.m_stateMachine.Cleanup();
            this.m_view.Cleanup();
            this.m_shieldView.Cleanup();
            for (int index = 0; index < 4; ++index)
                this.m_soundFxArray[index].Cleanup();
            for (int index = 0; index < 6; ++index)
                this.AnimationArray[index].Cleanup();
            for (int index = 0; index < 8; ++index)
                this.m_manaDrainArray[index].Cleanup();
            base.Cleanup();
        }

        public int Hit(NPCZombie.HitType hitType, int damage)
        {
            if (this.IsBroken)
                return 0;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float x1 = this.GetTransform().GetScale().X;
            float x2 = this.GetTransform().GetPosition().X + x1 * MathHelperEx.GetRandomFloat(-0.1f, 0.1f);
            float randomFloat1 = MathHelperEx.GetRandomFloat(0.5f, 0.8f);
            float x3 = this.GetTransform().GetPosition().X + x1 * MathHelperEx.GetRandomFloat(0.8f, 0.5f);
            float randomFloat2 = MathHelperEx.GetRandomFloat(0.5f, 0.8f);
            this.IsHit = true;
            this.m_currentHitType = hitType;
            switch (hitType)
            {
                case NPCZombie.HitType.Slash:
                case NPCZombie.HitType.AirSlash:
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Bullet:
                    if (this.m_currentVulnerabilityType == NPCNecromancer.VulnerabilityType.Bullet)
                    {
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Armor);
                        break;
                    }
                    currentWorld.AddBulletImpact(x3, randomFloat2, x1, BulletImpact.ImpactType.Armor, false);
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Explosion:
                    if (this.m_currentVulnerabilityType == NPCNecromancer.VulnerabilityType.Bullet)
                    {
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Burn);
                        break;
                    }
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Burn:
                    if (this.m_currentVulnerabilityType == NPCNecromancer.VulnerabilityType.Fire)
                    {
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Burn);
                        break;
                    }
                    currentWorld.AddBulletImpact(x3, randomFloat2, x1, BulletImpact.ImpactType.Burn, false);
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Ice:
                    if (this.m_currentVulnerabilityType == NPCNecromancer.VulnerabilityType.Ice)
                    {
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Ice);
                        break;
                    }
                    currentWorld.AddBulletImpact(x3, randomFloat2, x1, BulletImpact.ImpactType.Ice, false);
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Holy:
                    currentWorld.AddBulletImpact(x3, randomFloat2, x1, BulletImpact.ImpactType.Holy, false);
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
            }
            int num = damage;
            this.HitPoints -= damage;
            if (damage == 0)
                this.IsHit = false;
            if (this.HitPoints <= 0)
            {
                num += this.HitPoints;
                this.HitPoints = 0;
            }
            return num;
        }

        public override void UpdateCollisions()
        {
            if (this.IsBroken)
                return;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float num1 = this.GetTransform().GetPosition().X - currentWorld.GetHero().GetTransform().GetPosition().X;
            float num2 = this.m_shieldView.GetTransform().GetPosition().Y - currentWorld.GetHero().GetTransform().GetPosition().Y;
            if ((double)(num1 * num1 + num2 * num2) >= 1.0)
                return;
            this.m_shieldView.SetAnimation2D(this.AnimationArray[4], true);
            this.m_shieldView.GetAnimation2D().SetTime(0.0f);
            this.m_shieldBounce = true;
            currentWorld.GetHero().GetStateMachine().SetState(currentWorld.GetHero().GetStateMachine().StateHit);
            currentWorld.GetHero().GetPhysics().Velocity.X = 3f;
            currentWorld.GetHero().GetPhysics().Acceleration.X = -8f;
            this.GetSound(SoundFxId.SoundFx_Shield_Bounce).Play();
        }

        public EntityFiniteStateMachine GetStateMachine() => this.m_stateMachine;

        public Animation2DView GetView() => this.m_view;

        public Animation2DView GetShieldView() => this.m_shieldView;

        public Sound GetSound(SoundFxId soundId) => this.m_soundFxArray[(int)soundId];

        private void LoadContent()
        {
            this.AnimationArray[0].Initialize("Anims\\b_orb");
            this.AnimationArray[0].SetLoop(Animation2DLoop.Always);
            this.AnimationArray[1].Initialize("Anims\\b_orb_break");
            this.AnimationArray[2].Initialize("Anims\\b_orb_restore");
            this.AnimationArray[3].Initialize("Anims\\fx_shield");
            this.AnimationArray[3].SetLoop(Animation2DLoop.Always);
            this.AnimationArray[4].Initialize("Anims\\fx_shield_bounce");
            this.AnimationArray[5].Initialize("Anims\\fx_shield_disappear");
            this.m_soundFxArray[0].Initialize("Sounds\\fx_orb_break", true);
            this.m_soundFxArray[1].Initialize("Sounds\\fx_orb_restore", true);
            this.m_soundFxArray[2].Initialize("Sounds\\fx_shield_impact", true);
            this.m_soundFxArray[3].Initialize("Sounds\\fx_shield_bounce", true);
        }

        public enum MagicOrbAnimationId
        {
            Stand,
            Break,
            Restore,
            Shield,
            ShieldBounce,
            ShieldDisappear,
            ArraySize,
        }

        public enum MagicOrbStateId
        {
            Stand,
            Break,
            Broken,
            Restoring,
            Destroyed,
        }

        public enum SoundFxId
        {
            SoundFx_Break,
            SoundFx_Restore,
            SoundFx_Shield_Hit,
            SoundFx_Shield_Bounce,
            SoundFx_ArraySize,
        }
    }
}
