using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.FSM.NPC.Necromancer;
using Resanctified.Game.FSM.NPC.Necromancer.State;

namespace Resanctified.Game.World.Entities
{
    public class NPCNecromancer : NPCBoss
    {
        private Animation2DView m_view;
        private Animation2DView m_shadowView;
        private Animation2DView m_shieldView;
        private Animation2DView m_deadNunView;
        private Animation2D m_deadNunAnim;
        private NPCNecromancerFSM m_stateMachine;
        private Sound[] m_soundFxArray;
        public float SpellTimer;
        private float m_hitFxTimer;
        public bool IsInvoqueDone;
        public float VulnerableTimer;
        private VulnerabilityType m_currentVulnerabilityType;
        private VulnerabilityType m_previousVulnerabilityType;
        private bool m_shieldBounce;
        private bool m_showDeadNun;
        private bool m_shieldTransition;
        private float m_shieldTransitionTimer;

        public VulnerabilityType GetVulnerabilityType() => this.m_currentVulnerabilityType;

        public void SetInvulnerable()
        {
            this.m_currentVulnerabilityType = VulnerabilityType.None;
            this.m_shieldTransition = true;
        }

        public bool IsInvulnerable() => this.m_currentVulnerabilityType == VulnerabilityType.None;

        public void ChangeVulnerability()
        {
            if (this.m_bossType == BossType.NecromancerForm1)
            {
                this.m_currentVulnerabilityType = this.m_previousVulnerabilityType != VulnerabilityType.Ice ? this.m_previousVulnerabilityType + 1 : VulnerabilityType.Bullet;
                this.m_previousVulnerabilityType = this.m_currentVulnerabilityType;
            }
            else if (this.m_bossType == BossType.NecromancerForm2)
                this.m_currentVulnerabilityType = VulnerabilityType.Holy;
            this.m_shieldTransition = true;
        }

        public bool IsInvoquing() => this.m_stateMachine.GetCurrentState() == NPCNecromancerStateInvoque.GetInstance();

        public bool IsAlive() => this.m_stateMachine.CurrentState != NPCNecromancerStateDying.GetInstance() && this.m_stateMachine.CurrentState != NPCNecromancerStateDead.GetInstance();

        public override bool IsDead() => this.m_stateMachine.GetCurrentState() == NPCNecromancerStateDead.GetInstance();

        public void ShowDeadNun()
        {
            if (this.m_bossType != BossType.NecromancerForm1 || this.m_showDeadNun)
                return;
            Renderer.GetInstance().AddTopLevelView(this.m_deadNunView, this.GetTransform());
            this.m_showDeadNun = true;
        }

        public NPCNecromancer()
        {
            this.m_stateMachine = new NPCNecromancerFSM(this);
            this.HitPoints = 1500;
            this.SpellTimer = 0.0f;
            this.VulnerableTimer = 0.0f;
            this.m_hitFxTimer = 0.0f;
            this.m_currentVulnerabilityType = VulnerabilityType.None;
            this.m_previousVulnerabilityType = this.m_currentVulnerabilityType;
            this.IsInvoqueDone = false;
            this.m_shieldBounce = false;
            this.m_showDeadNun = false;
            this.m_bossType = BossType.NecromancerForm1;
            this.m_soundFxArray = new Sound[7];
            for (int index = 0; index < 7; ++index)
                this.m_soundFxArray[index] = new Sound();
            this.m_view = new Animation2DView();
            this.m_shadowView = new Animation2DView();
            this.m_shieldView = new Animation2DView();
            this.m_deadNunView = new Animation2DView();
            this.m_deadNunAnim = new Animation2D();
            this.m_type = 6;
            this.m_shieldTransition = false;
            this.m_shieldTransitionTimer = 0.0f;
        }

        public override void Initialize() => this.Initialize(BossType.NecromancerForm1);

        public void Initialize(BossType type)
        {
            this.m_bossType = type;
            this.SpellTimer = 0.0f;
            this.VulnerableTimer = 0.0f;
            this.m_hitFxTimer = 0.0f;
            this.IsInvoqueDone = false;
            this.m_shieldBounce = false;
            this.m_showDeadNun = false;
            this.m_currentVulnerabilityType = VulnerabilityType.None;
            this.m_previousVulnerabilityType = this.m_currentVulnerabilityType;
            this.m_physics.Initialize();
            if (this.m_bossType == BossType.NecromancerForm1)
            {
                this.HitPoints = 1500;
                if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                    this.HitPoints += 500;
            }
            else if (this.m_bossType == BossType.NecromancerForm2)
            {
                this.HitPoints = 5000;
                if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                    this.HitPoints += 1500;
            }
            this.LoadContent();
            this.m_stateMachine.Initialize();
            this.m_view.Initialize();
            this.m_view.GetTransform().SetPosition(0.0f, 0.0f, -2f);
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shieldView.Initialize();
            this.m_shieldView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shieldView.GetTransform().SetPosition(0.0f, 1f, 1f);
            this.m_shieldView.SetAnimation2D(this.m_stateMachine.AnimationArray[7]);
            this.m_shieldView.GetAnimation2D().SetTime(0.0f);
            this.m_shieldView.GetAnimation2D().SetSpeed(1f);
            this.m_shieldView.SetCustomColor(GameConstant.SHIELD_INVULNERABLE_COLOR);
            this.m_shieldView.SetCustomColorEnabled(true);
            this.m_shieldTransition = false;
            this.m_shieldTransitionTimer = 0.0f;
            this.m_shadowView.Initialize();
            this.m_shadowView.SetAnimation2D(((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetMiscAnimation(GameWorld.MiscAnimId.ZombieShadow));
            this.m_shadowView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shadowView.GetTransform().Clear();
            this.m_shadowView.GetTransform().SetPosition(0.0f, 0.0f, -2.2f);
            this.GetTransform().SetPosition(6.5f, 0.0f, 0.0f);
            this.GetTransform().SetScale(-1f, 1f);
            this.m_stateMachine.SetState(NPCNecromancerStateStand.GetInstance());
            Renderer.GetInstance().AddTopLevelView(this.m_view, this.GetTransform());
            this.m_view.AddSubview(this.m_shadowView);
            Renderer.GetInstance().AddTopLevelView(this.m_shieldView, this.GetTransform());
            if (this.m_bossType != BossType.NecromancerForm1)
                return;
            this.m_deadNunView.Initialize();
            this.m_deadNunView.SetAnimation2D(this.m_deadNunAnim);
            this.m_deadNunView.GetTransform().SetPosition(2f, 0.5f, -2.5f);
        }

        public override void Update(float elapsedTime)
        {
            this.SpellTimer += elapsedTime;
            if (this.VulnerableTimer > 0.0)
            {
                this.VulnerableTimer -= elapsedTime;
                if (this.VulnerableTimer <= 0.0)
                {
                    this.VulnerableTimer = 0.0f;
                    this.SetInvulnerable();
                }
            }
            if (this.m_hitFxTimer > 0.0)
            {
                this.m_hitFxTimer -= elapsedTime;
                if (this.m_hitFxTimer <= 0.0)
                {
                    this.GetView().SetCustomColorEnabled(false);
                    this.m_hitFxTimer = 0.0f;
                }
                else
                {
                    int hitColor = NPCZombie.GetHitColor(this.m_hitFxTimer);
                    this.GetView().SetCustomColor((byte)hitColor, byte.MaxValue, (byte)hitColor, byte.MaxValue);
                    this.GetView().SetCustomColorEnabled(true);
                }
            }
            this.m_stateMachine.Update(elapsedTime);
            this.m_view.Update(elapsedTime);
            this.m_shadowView.Update(elapsedTime);
            this.m_shieldView.Update(elapsedTime);
            this.m_physics.Update(elapsedTime);
            this.UpdateShieldColor(elapsedTime);
            if (this.m_bossType == BossType.NecromancerForm1 && this.m_showDeadNun)
                this.m_deadNunView.Update(elapsedTime);
            if (this.IsAlive())
            {
                if (this.m_shieldBounce && this.m_shieldView.GetAnimation2D().IsEndOfAnimationReached())
                {
                    this.m_shieldView.SetAnimation2D(this.m_stateMachine.AnimationArray[7], true);
                    this.m_shieldView.GetAnimation2D().SetTime(0.0f);
                    this.m_shieldBounce = false;
                }
                this.UpdateCollisionBoxes(this.GetView().GetAnimation2D(), "hit");
            }
            base.Update(elapsedTime);
        }

        private void UpdateShieldColor(float time)
        {
            Color color1 = GameConstant.SHIELD_INVULNERABLE_COLOR;
            switch (this.GetVulnerabilityType())
            {
                case VulnerabilityType.Bullet:
                    color1 = GameConstant.SHIELD_BULLET_COLOR;
                    break;
                case VulnerabilityType.Fire:
                    color1 = GameConstant.SHIELD_FIRE_COLOR;
                    break;
                case VulnerabilityType.Ice:
                    color1 = GameConstant.SHIELD_ICE_COLOR;
                    break;
                case VulnerabilityType.Holy:
                    color1 = GameConstant.SHIELD_HOLY_COLOR;
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
                if (this.GetVulnerabilityType() == VulnerabilityType.None)
                {
                    switch (this.m_previousVulnerabilityType)
                    {
                        case VulnerabilityType.Bullet:
                            color2 = GameConstant.SHIELD_BULLET_COLOR;
                            break;
                        case VulnerabilityType.Fire:
                            color2 = GameConstant.SHIELD_FIRE_COLOR;
                            break;
                        case VulnerabilityType.Ice:
                            color2 = GameConstant.SHIELD_ICE_COLOR;
                            break;
                        case VulnerabilityType.Holy:
                            color2 = GameConstant.SHIELD_HOLY_COLOR;
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

        public override void UpdateCollisions()
        {
            if (!this.IsAlive())
                return;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float num1 = this.GetTransform().GetPosition().X - currentWorld.GetHero().GetTransform().GetPosition().X;
            float num2 = this.m_shieldView.GetTransform().GetPosition().Y - currentWorld.GetHero().GetTransform().GetPosition().Y;
            float num3 = num1 * num1;
            float num4 = num2 * num2;
            float num5 = num3 + num4;
            bool flag = false;
            if ((double)num5 < 1.44000005722046)
                flag = true;
            else if (currentWorld.GetHero().GetTransform().GetPosition().Y < 1.0 && (double)num3 < 1.44000005722046)
                flag = true;
            if (flag)
            {
                this.m_shieldView.SetAnimation2D(this.m_stateMachine.AnimationArray[8], true);
                this.m_shieldView.GetAnimation2D().SetTime(0.0f);
                this.m_shieldBounce = true;
                currentWorld.GetHero().GetStateMachine().SetState(currentWorld.GetHero().GetStateMachine().StatePushed);
                currentWorld.GetHero().GetPhysics().Velocity.X = -4f;
                currentWorld.GetHero().GetPhysics().Acceleration.X = 8f;
                this.GetSound(SoundFxId.SoundFx_Shield_Bounce).Play();
            }
            Color color;
            AABB? boxAndColor = this.GetView().GetAnimation2D().GetBoxAndColor("attack1", out color);
            if (!boxAndColor.HasValue || color.A == 0)
                return;
            PlayableCharacter hero = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero();
            if (hero.IsHit || hero.GetIsInvulnerable())
                return;
            AABB transformedBox;
            MathHelperEx.TransformBox(out transformedBox, boxAndColor.Value, this.m_transform.GetWorldTransform());
            CollisionResult[] results = new CollisionResult[1];
            Entity[] entities = new Entity[1]
            {
         hero
            };
            if (CollisionHelper.CollideEntities(transformedBox, entities, 1, ref results, 1, false) <= 0)
                return;
            int damage = 0;
            if (this.m_bossType == BossType.NecromancerForm1)
                damage = 80;
            if (this.m_bossType == BossType.NecromancerForm2)
                damage = 100;
            if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                damage += 40;
            hero.Hit(damage, false);
        }

        public override void Cleanup()
        {
            this.m_stateMachine.Cleanup();
            for (int index = 0; index < 7; ++index)
                this.m_soundFxArray[index].Cleanup();
            this.m_view.Cleanup();
            this.m_shadowView.Cleanup();
            this.m_shieldView.Cleanup();
            if (this.m_bossType == BossType.NecromancerForm1)
            {
                this.m_deadNunAnim.Cleanup();
                this.m_deadNunView.Cleanup();
            }
            base.Cleanup();
        }

        public override int Hit(NPCZombie.HitType hitType, int damage)
        {
            if (!this.IsAlive())
                return 0;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float x1 = this.GetTransform().GetScale().X;
            float x2 = this.GetTransform().GetPosition().X + x1 * MathHelperEx.GetRandomFloat(-0.1f, 0.1f);
            float randomFloat1 = MathHelperEx.GetRandomFloat(0.5f, 1f);
            float x3 = this.GetTransform().GetPosition().X + x1 * MathHelperEx.GetRandomFloat(0.85f, 0.65f);
            float randomFloat2 = MathHelperEx.GetRandomFloat(0.5f, 1f);
            this.IsHit = true;
            this.m_currentHitType = hitType;
            switch (hitType)
            {
                case NPCZombie.HitType.Slash:
                case NPCZombie.HitType.AirSlash:
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Bullet:
                    if (this.m_currentVulnerabilityType == VulnerabilityType.Bullet)
                    {
                        if (this.m_bossType == BossType.NecromancerForm1)
                            damage = (int)(damage * 1.0);
                        if (this.m_bossType == BossType.NecromancerForm2)
                            damage = (int)(damage * 0.0);
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Blood);
                        break;
                    }
                    currentWorld.AddBulletImpact(x3, randomFloat2, x1, BulletImpact.ImpactType.Armor, false);
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Explosion:
                    if (this.m_currentVulnerabilityType == VulnerabilityType.Bullet)
                    {
                        if (this.m_bossType == BossType.NecromancerForm1)
                            damage = (int)(damage * 1.0);
                        if (this.m_bossType == BossType.NecromancerForm2)
                            damage = (int)(damage * 0.0);
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Blood);
                        break;
                    }
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Burn:
                    if (this.m_currentVulnerabilityType == VulnerabilityType.Fire)
                    {
                        if (this.m_bossType == BossType.NecromancerForm1)
                            damage = (int)(damage * 1.0);
                        if (this.m_bossType == BossType.NecromancerForm2)
                            damage = (int)(damage * 0.0);
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Burn);
                        break;
                    }
                    currentWorld.AddBulletImpact(x3, randomFloat2, x1, BulletImpact.ImpactType.Burn, false);
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Ice:
                    if (this.m_currentVulnerabilityType == VulnerabilityType.Ice)
                    {
                        if (this.m_bossType == BossType.NecromancerForm1)
                            damage = (int)(damage * 1.0);
                        if (this.m_bossType == BossType.NecromancerForm2)
                            damage = (int)(damage * 0.0);
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Ice);
                        break;
                    }
                    currentWorld.AddBulletImpact(x3, randomFloat2, x1, BulletImpact.ImpactType.Ice, false);
                    this.GetSound(SoundFxId.SoundFx_Shield_Hit).Play();
                    this.IsHit = false;
                    return 0;
                case NPCZombie.HitType.Holy:
                    if (this.m_currentVulnerabilityType == VulnerabilityType.Holy)
                    {
                        if (this.m_bossType == BossType.NecromancerForm1)
                            damage = (int)(damage * 0.0);
                        if (this.m_bossType == BossType.NecromancerForm2)
                            damage = (int)(damage * 1.0);
                        currentWorld.AddBulletImpact(x2, randomFloat1, x1, BulletImpact.ImpactType.Holy);
                        break;
                    }
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
            if (this.IsHit)
                this.m_hitFxTimer = 0.5f;
            return num;
        }

        public Animation2DView GetShadowView() => this.m_shadowView;

        public Animation2DView GetShieldView() => this.m_shieldView;

        public Animation2DView GetView() => this.m_view;

        public NPCNecromancerFSM GetStateMachine() => this.m_stateMachine;

        public int GetHitpointsMax()
        {
            if (this.m_bossType == BossType.NecromancerForm1)
                return 1500;
            return this.m_bossType == BossType.NecromancerForm2 ? 5000 : 0;
        }

        public Sound GetSound(SoundFxId soundId) => this.m_soundFxArray[(int)soundId];

        private void LoadContent()
        {
            if (this.GetBossType() == BossType.NecromancerForm1)
            {
                this.m_stateMachine.AnimationArray[0].Initialize("Anims\\b_necro1_stand");
                this.m_stateMachine.AnimationArray[0].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[1].Initialize("Anims\\b_necro1_hit");
                this.m_stateMachine.AnimationArray[2].Initialize("Anims\\b_necro1_dying");
                this.m_stateMachine.AnimationArray[3].Initialize("Anims\\b_necro1_spell_blast");
                this.m_stateMachine.AnimationArray[4].Initialize("Anims\\b_necro1_spell_invoque_start");
                this.m_stateMachine.AnimationArray[5].Initialize("Anims\\b_necro1_spell_invoquing");
                this.m_stateMachine.AnimationArray[5].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[6].Initialize("Anims\\b_necro1_spell_invoque_end");
                this.m_deadNunAnim.Initialize("Anims\\b_nun2_dead");
                this.m_deadNunAnim.SetLoop(Animation2DLoop.Always);
                this.m_soundFxArray[0].Initialize("Sounds\\necro_blast", true);
                this.m_soundFxArray[1].Initialize("Sounds\\necro1_invoque", true);
                this.m_soundFxArray[2].Initialize("Sounds\\fx_earthquake", true);
                this.m_soundFxArray[2].SetLooping(true);
                this.m_soundFxArray[3].Initialize("Sounds\\necro1_hit", true);
                this.m_soundFxArray[4].Initialize("Sounds\\necro1_dying", true);
            }
            else if (this.GetBossType() == BossType.NecromancerForm2)
            {
                this.m_stateMachine.AnimationArray[0].Initialize("Anims\\b_necro2_stand");
                this.m_stateMachine.AnimationArray[0].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[1].Initialize("Anims\\b_necro2_hit");
                this.m_stateMachine.AnimationArray[2].Initialize("Anims\\b_necro2_dying");
                this.m_stateMachine.AnimationArray[3].Initialize("Anims\\b_necro2_spell_blast");
                this.m_stateMachine.AnimationArray[4].Initialize("Anims\\b_necro2_spell_invoque_start");
                this.m_stateMachine.AnimationArray[5].Initialize("Anims\\b_necro2_spell_invoquing");
                this.m_stateMachine.AnimationArray[5].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[6].Initialize("Anims\\b_necro2_spell_invoque_end");
                this.m_soundFxArray[0].Initialize("Sounds\\necro_blast", true);
                this.m_soundFxArray[1].Initialize("Sounds\\necro2_invoque", true);
                this.m_soundFxArray[2].Initialize("Sounds\\fx_earthquake", true);
                this.m_soundFxArray[2].SetLooping(true);
                this.m_soundFxArray[3].Initialize("Sounds\\necro2_hit", true);
                this.m_soundFxArray[4].Initialize("Sounds\\necro2_dying", true);
            }
            this.m_stateMachine.AnimationArray[7].Initialize("Anims\\fx_shield");
            this.m_stateMachine.AnimationArray[7].SetLoop(Animation2DLoop.Always);
            this.m_stateMachine.AnimationArray[8].Initialize("Anims\\fx_shield_bounce");
            this.m_stateMachine.AnimationArray[9].Initialize("Anims\\fx_shield_disappear");
            this.m_soundFxArray[5].Initialize("Sounds\\fx_shield_impact", true);
            this.m_soundFxArray[6].Initialize("Sounds\\fx_shield_bounce", true);
        }

        public enum VulnerabilityType
        {
            None,
            Bullet,
            Fire,
            Ice,
            Holy,
        }

        public enum SoundFxId
        {
            SoundFx_Blast,
            SoundFx_Invoque,
            SoundFx_Earthquake,
            SoundFx_Hit,
            SoundFx_Dying,
            SoundFx_Shield_Hit,
            SoundFx_Shield_Bounce,
            SoundFx_ArraySize,
        }
    }
}
