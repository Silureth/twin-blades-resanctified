using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.FSM.Hero;
using Resanctified.Game.FSM.Hero.HeroAnimationState;
using Resanctified.Game.FSM.NPC.Zombie.State;

namespace Resanctified.Game.World.Entities
{
    public class PlayableCharacter : Entity, IPlayableCharacter, IEntity
    {
        public bool IsInAir;
        public bool IsHit;
        public bool IsGrabbed;
        public bool HasFired;
        public bool AirSlashAvailable;
        public bool DoGunRaycast;
        public int HitPoints;
        public int ManaPoints;
        public int Hearts;
        public int SlashDamageLevel;
        public int SlashManaDrainLevel;
        public int HitPointsLevel;
        public int ManaPointsLevel;
        public int AirSlashDamageLevel;
        public int GunFireFXTransitionIndex;
        private Animation2DView m_view;
        private Animation2DView m_gunView;
        private Animation2DView m_gunFireView;
        private Animation2DView m_shadowView;
        private HeroFSM m_stateMachine;
        private HeroAnimationFSM m_animationStateMachine;
        private Physics2D m_physics;
        private int m_currentGunIndex;
        private Gun[] m_gunArray;
        private Sound[] m_soundFxArray;
        private bool m_isInvulnerable;
        private float m_invulnerableTimer;
        private float m_blinkTimer;
        private float m_manaDrainFxTimer;
        private int m_manaDrainFxState;

        public PlayableCharacter()
        {
            this.m_stateMachine = new HeroFSM(this);
            this.m_animationStateMachine = new HeroAnimationFSM(this);
            this.m_physics = new Physics2D(this, 20f);
            this.IsInAir = false;
            this.IsHit = false;
            this.IsGrabbed = false;
            this.HasFired = false;
            this.DoGunRaycast = false;
            this.HitPoints = 100;
            this.ManaPoints = 100;
            this.m_isInvulnerable = false;
            this.m_invulnerableTimer = 0.0f;
            this.m_blinkTimer = 0.0f;
            this.m_currentGunIndex = 0;
            this.AirSlashAvailable = false;
            this.AirSlashDamageLevel = 1;
            this.SlashDamageLevel = 1;
            this.SlashManaDrainLevel = 1;
            this.HitPointsLevel = 1;
            this.ManaPointsLevel = 1;
            this.GunFireFXTransitionIndex = 0;
            this.Hearts = 0;
            this.m_manaDrainFxState = 0;
            this.m_manaDrainFxTimer = 0.0f;
            this.m_view = new Animation2DView();
            this.m_gunView = new Animation2DView();
            this.m_gunFireView = new Animation2DView();
            this.m_shadowView = new Animation2DView();
            this.m_soundFxArray = new Sound[21];
            for (int index = 0; index < 21; ++index)
                this.m_soundFxArray[index] = new Sound();
            this.m_gunArray = new Gun[6];
            this.m_gunArray[0] = new Gun();
            this.m_gunArray[1] = new MachineGun();
            this.m_gunArray[4] = new ExplosiveGun();
            this.m_gunArray[2] = new FlameThrower();
            this.m_gunArray[3] = new IceGun();
            this.m_gunArray[5] = new HolyBeamGun();
        }

        public override void Initialize() => this.Initialize(0.0f, 0.0f);

        public void Initialize(float x, float y)
        {
            this.IsInAir = false;
            this.IsHit = false;
            this.IsGrabbed = false;
            this.HasFired = false;
            this.DoGunRaycast = false;
            this.SetIsInvulnerable(false);
            this.GunFireFXTransitionIndex = 0;
            this.m_manaDrainFxState = 0;
            this.m_manaDrainFxTimer = 0.0f;
            this.GetTransform().SetPosition(x, y, 0.0f);
            this.GetTransform().SetScale(1f, 1f);
            this.m_view.Initialize();
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_view.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
            this.m_gunView.Initialize();
            this.m_gunView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_gunView.GetTransform().SetPosition(0.0f, 0.0f, -0.1f);
            this.m_gunFireView.GetTransform().SetPosition(0.0f, 0.0f, 2f);
            this.m_gunFireView.SetVisible(false);
            this.m_view.AddSubview(this.m_gunFireView);
            this.m_shadowView.Initialize();
            this.m_shadowView.SetAnimation2D(((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetMiscAnimation(GameWorld.MiscAnimId.HeroShadow));
            this.m_shadowView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shadowView.GetTransform().Clear();
            this.m_shadowView.GetTransform().SetPosition(x, y, -0.2f);
            Renderer.GetInstance().AddTopLevelView(this.m_view, this.GetTransform());
            Renderer.GetInstance().AddTopLevelView(this.m_shadowView, null);
            this.LoadContent();
            this.m_animationStateMachine.Initialize();
            this.m_stateMachine.Initialize();
            for (int index = 0; index < 6; ++index)
                this.m_gunArray[index].Initialize();
            this.m_currentGunIndex = 0;
        }

        public override void Update(float elapsedTime)
        {
            if (this.m_invulnerableTimer > 0.0)
            {
                this.IsHit = false;
                this.m_blinkTimer -= elapsedTime;
                this.m_invulnerableTimer -= elapsedTime;
                if (this.m_blinkTimer <= 0.0)
                {
                    this.m_blinkTimer = 0.04f;
                    this.m_view.SetVisible(!this.m_view.IsVisible());
                    this.m_gunView.SetVisible(!this.m_gunView.IsVisible());
                    this.m_shadowView.SetVisible(!this.m_shadowView.IsVisible());
                }
                if (this.m_invulnerableTimer <= 0.0)
                    this.SetIsInvulnerable(false);
            }
            if (this.m_manaDrainFxState > 0)
            {
                switch (this.m_manaDrainFxState)
                {
                    case 1:
                        this.m_manaDrainFxTimer -= elapsedTime;
                        if (this.m_manaDrainFxTimer <= 0.0)
                        {
                            this.m_manaDrainFxTimer = 0.5f;
                            this.m_manaDrainFxState = 2;
                            break;
                        }
                        break;
                    case 2:
                        this.m_manaDrainFxTimer += elapsedTime;
                        if (this.m_manaDrainFxTimer >= 1.5)
                        {
                            this.m_manaDrainFxState = 1;
                            this.m_manaDrainFxTimer = 1f;
                            break;
                        }
                        break;
                }
                int num = (int)(byte.MaxValue * (double)this.m_manaDrainFxTimer);
                if (num < 128)
                    num = 128;
                if (num > byte.MaxValue)
                    num = byte.MaxValue;
                this.GetView().SetCustomColor((byte)num, (byte)num, byte.MaxValue, byte.MaxValue);
                this.GetView().SetCustomColorEnabled(true);
            }
            if (!this.m_stateMachine.GetGameActionFromId(HeroFSM.HeroGameActionId.AutoFire).IsActive())
            {
                if (this.m_stateMachine.GetGameActionFromId(HeroFSM.HeroGameActionId.NextWeapon).IsActive())
                {
                    this.NextGun();
                    ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHud().SetCurrentGunIcon(this);
                }
                else if (this.m_stateMachine.GetGameActionFromId(HeroFSM.HeroGameActionId.PrevWeapon).IsActive())
                {
                    this.PreviousGun();
                    ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHud().SetCurrentGunIcon(this);
                }
            }
            this.m_stateMachine.Update(elapsedTime);
            this.m_animationStateMachine.Update(elapsedTime);
            this.GetGun().Update(elapsedTime);
            this.m_view.Update(elapsedTime);
            this.m_gunView.Update(elapsedTime);
            this.m_gunFireView.Update(elapsedTime);
            this.m_shadowView.Update(elapsedTime);
            this.UpdateGunFX();
            this.m_physics.Update(elapsedTime);
            if (this.GetTransform().GetPosition().Y < 0.0)
            {
                this.IsInAir = false;
                Vector3 position = this.GetTransform().GetPosition() with
                {
                    Y = 0.0f
                };
                this.GetTransform().SetPosition(position);
            }
            if (this.GetTransform().GetPosition().Y > 0.0)
                this.IsInAir = true;
            Vector3 position1 = this.GetTransform().GetPosition();
            float s = (float)(1.0 + (double)(MathHelper.Clamp(position1.Y, 0.0f, 1.4f) / 1.4f) * -0.600000023841858);
            this.m_shadowView.GetTransform().SetScale(s);
            this.m_shadowView.GetTransform().SetPosition(position1.X, 0.0f, -0.2f);
            this.UpdateCollisionBoxes(this.GetView().GetAnimation2D(), "hit");
            base.Update(elapsedTime);
        }

        public override void UpdateCollisions()
        {
            if (this.m_stateMachine.GetCurrentState() == this.m_stateMachine.StateSlash)
            {
                this.UpdateCollisionsSlash();
            }
            else
            {
                if (this.m_stateMachine.GetCurrentState() != this.m_stateMachine.StateStandFire && this.m_stateMachine.GetCurrentState() != this.m_stateMachine.StateWalkFire)
                    return;
                this.UpdateCollisionsFire();
            }
        }

        public override void Cleanup()
        {
            this.m_stateMachine.Cleanup();
            this.m_animationStateMachine.Cleanup();
            for (int index = 0; index < 6; ++index)
                this.m_gunArray[index].Cleanup();
            for (int index = 0; index < 21; ++index)
                this.m_soundFxArray[index].Cleanup();
            this.m_view.Cleanup();
            this.m_gunView.Cleanup();
            this.m_gunFireView.Cleanup();
            this.m_shadowView.Cleanup();
            base.Cleanup();
        }

        public void EnableManaDrainFx()
        {
            this.m_manaDrainFxState = 1;
            this.m_manaDrainFxTimer = 1f;
        }

        public void DisableManaDrainFx()
        {
            this.m_manaDrainFxState = 0;
            this.m_manaDrainFxTimer = 0.0f;
            this.GetView().SetCustomColorEnabled(false);
        }

        public bool GetIsInvulnerable() => this.m_isInvulnerable;

        public void SetIsInvulnerable(bool value)
        {
            this.m_isInvulnerable = value;
            if (!this.m_isInvulnerable)
            {
                this.m_view.SetVisible(true);
                this.m_gunView.SetVisible(true);
                this.m_shadowView.SetVisible(true);
                this.m_invulnerableTimer = 0.0f;
            }
            else
                this.m_invulnerableTimer = 2f;
        }

        public void ResetBlink()
        {
            this.m_view.SetVisible(true);
            this.m_gunView.SetVisible(true);
            this.m_shadowView.SetVisible(true);
            this.m_blinkTimer = 0.04f;
        }

        public Animation2DView GetView() => this.m_view;

        public Animation2DView GetGunView() => this.m_gunView;

        public Animation2DView GetGunFireView() => this.m_gunFireView;

        public HeroFSM GetStateMachine() => this.m_stateMachine;

        public HeroAnimationFSM GetAnimationStateMachine() => this.m_animationStateMachine;

        public Physics2D GetPhysics() => this.m_physics;

        public Gun GetGun() => this.m_gunArray[this.m_currentGunIndex];

        public Gun[] GetGunArray() => this.m_gunArray;

        public int GetCurrentGunIndex() => this.m_currentGunIndex;

        public void SetCurrentGunIndex(int index) => this.m_currentGunIndex = index;

        public int GetHitPointsMax() => 100 + (this.HitPointsLevel - 1) * 50;

        public int GetManaPointsMax() => 100 + (this.ManaPointsLevel - 1) * 50;

        public int GetSlashDamage() => 15 + (this.SlashDamageLevel - 1) * 9;

        public int GetAirSlashDamage() => 50 + (this.AirSlashDamageLevel - 1) * 9;

        public int GetSlashManaDrain(int damageInflicted) => (int)(damageInflicted * (0.150000005960464 + 0.0599999986588955 * (this.SlashManaDrainLevel - 1)));

        public void Hit(int damage, bool grabbed)
        {
            this.IsHit = true;
            if (!this.IsGrabbed)
                this.IsGrabbed = grabbed;
            this.HitPoints -= damage;
            if (this.HitPoints >= 0)
                return;
            this.HitPoints = 0;
        }

        public Sound GetSound(SoundFxId soundId) => this.m_soundFxArray[(int)soundId];

        private void NextGun()
        {
            ++this.m_currentGunIndex;
            if (this.m_currentGunIndex >= 6)
                this.m_currentGunIndex = 0;
            if (!this.GetGun().IsAvailable)
            {
                this.NextGun();
            }
            else
            {
                int num = 0;
                for (int index = 0; index < 6; ++index)
                {
                    if (this.m_gunArray[index].IsAvailable)
                        ++num;
                }
                if (num <= 1)
                    return;
                this.GetSound(SoundFxId.SoundFx_ChangeWeapon).Play();
            }
        }

        private void PreviousGun()
        {
            --this.m_currentGunIndex;
            if (this.m_currentGunIndex < 0)
                this.m_currentGunIndex = 5;
            if (!this.GetGun().IsAvailable)
            {
                this.PreviousGun();
            }
            else
            {
                int num = 0;
                for (int index = 0; index < 6; ++index)
                {
                    if (this.m_gunArray[index].IsAvailable)
                        ++num;
                }
                if (num <= 1)
                    return;
                this.GetSound(SoundFxId.SoundFx_ChangeWeapon).Play();
            }
        }

        private void UpdateCollisionsSlash()
        {
            Color color1;
            AABB? boxAndColor1 = this.GetView().GetAnimation2D().GetBoxAndColor("slash_stand", out color1);
            Color color2;
            AABB? boxAndColor2 = this.GetView().GetAnimation2D().GetBoxAndColor("slash_air2", out color2);
            AABB transformedBox;
            if (boxAndColor1.HasValue)
            {
                if (color1.A == 0)
                    return;
                MathHelperEx.TransformBox(out transformedBox, boxAndColor1.Value, this.m_transform.GetWorldTransform());
            }
            else
            {
                if (!boxAndColor2.HasValue || color2.A == 0)
                    return;
                MathHelperEx.TransformBox(out transformedBox, boxAndColor2.Value, this.m_transform.GetWorldTransform());
            }
            NPCZombie[] npcZombieArray = new NPCZombie[55];
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            int entitiesByType = currentWorld.GetEntityManager().GetEntitiesByType(3, npcZombieArray, 55);
            CollisionResult[] results = new CollisionResult[128];
            int num = CollisionHelper.CollideEntities(transformedBox, npcZombieArray, entitiesByType, ref results, 128, true);
            NPCZombie.HitType hitType;
            int damage;
            if (boxAndColor1.HasValue)
            {
                hitType = NPCZombie.HitType.Slash;
                damage = this.GetSlashDamage();
            }
            else
            {
                hitType = NPCZombie.HitType.AirSlash;
                damage = this.GetAirSlashDamage();
            }
            int damageInflicted = 0;
            for (int index = 0; index < num; ++index)
            {
                NPCZombie entity = (NPCZombie)results[index].entity;
                if (!entity.IsHit || entity.GetHitType() != hitType)
                    damageInflicted += entity.Hit(hitType, damage);
            }
            if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss && (!currentWorld.GetBoss().IsHit || currentWorld.GetBoss().GetHitType() != hitType))
            {
                Entity[] entities = new Entity[1]
                {
           currentWorld.GetBoss()
                };
                if (CollisionHelper.CollideEntities(transformedBox, entities, 1, ref results, 1, false) > 0)
                    damageInflicted += currentWorld.GetBoss().Hit(hitType, damage);
            }
            this.ManaPoints += this.GetSlashManaDrain(damageInflicted);
            if (this.ManaPoints <= this.GetManaPointsMax())
                return;
            this.ManaPoints = this.GetManaPointsMax();
        }

        private void UpdateCollisionsFire()
        {
            if (!this.DoGunRaycast)
                return;
            this.DoGunRaycast = false;
            Vector3 zero1 = Vector3.Zero;
            Vector3 zero2 = Vector3.Zero;
            zero1.X = this.GetTransform().GetPosition().X + 0.0f * this.GetTransform().GetScale().X;
            zero1.Y = this.GetTransform().GetPosition().Y + 0.7f;
            zero1.Z = 0.0f;
            zero2.X = this.GetTransform().GetScale().X;
            zero2.Y = zero2.Z = 0.0f;
            Engine.Maths.Ray ray = new Engine.Maths.Ray(zero1, zero2);
            NPCZombie[] npcZombieArray = new NPCZombie[55];
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            int entitiesByType = currentWorld.GetEntityManager().GetEntitiesByType(3, npcZombieArray, 55);
            CollisionResult[] results = new CollisionResult[128];
            Gun.GunType gunType = this.GetGun().GetGunType();
            Entity[] entities1 = new Entity[1]
            {
         currentWorld.GetBoss()
            };
            Entity[] entities2 = new Entity[1]
            {
         currentWorld.GetMagicOrb()
            };
            switch (gunType)
            {
                case Gun.GunType.Gun:
                case Gun.GunType.MachineGun:
                    if (CollisionHelper.CollideEntities(ray, 0.0f, 5f, npcZombieArray, entitiesByType, ref results, 128, true) > 0)
                    {
                        NPCZombie entity = (NPCZombie)results[0].entity;
                        if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss && currentWorld.GetBoss().GetBossType() == NPCBoss.BossType.ButcherForm1 && CollisionHelper.CollideEntities(ray, 0.0f, 5f, entities1, 1, ref results, 1, false) > 0 && (double)Math.Abs(currentWorld.GetBoss().GetTransform().GetPosition().X - currentWorld.GetHero().GetTransform().GetPosition().X) < (double)Math.Abs(entity.GetTransform().GetPosition().X - currentWorld.GetHero().GetTransform().GetPosition().X))
                        {
                            currentWorld.GetBoss().Hit(NPCZombie.HitType.Bullet, this.GetGun().GetDamage());
                            break;
                        }
                        if ((entity.GetZombieType() == ZombieType.ArmoredNormal || entity.GetZombieType() == ZombieType.ArmoredGrabber) && entity.GetStateMachine().CurrentState != NPCZombieStateFreeze.GetInstance() && MathHelperEx.GetRandomInt(0, 100) > 30)
                        {
                            float x1 = entity.GetTransform().GetScale().X;
                            float x2 = entity.GetTransform().GetPosition().X + x1 * 0.1f;
                            float randomFloat = MathHelperEx.GetRandomFloat(0.5f, 0.7f);
                            if (entity.GetStateMachine().CurrentState == NPCZombieStateSpawn.GetInstance())
                                x2 = entity.GetTransform().GetPosition().X;
                            else if (MathHelperEx.GetRandomInt(0, 100) > 70)
                                randomFloat = MathHelperEx.GetRandomFloat(0.9f, 1f);
                            currentWorld.AddBulletImpact(x2, randomFloat, x1, BulletImpact.ImpactType.Armor);
                            break;
                        }
                        entity.Hit(NPCZombie.HitType.Bullet, this.GetGun().GetDamage());
                        break;
                    }
                    if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Boss)
                        break;
                    if (CollisionHelper.CollideEntities(ray, 0.0f, 5f, entities1, 1, ref results, 1, false) > 0)
                        currentWorld.GetBoss().Hit(NPCZombie.HitType.Bullet, this.GetGun().GetDamage());
                    if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Chapel_Boss || CollisionHelper.CollideEntities(ray, 0.0f, 5f, entities2, 1, ref results, 1, false) <= 0)
                        break;
                    currentWorld.GetMagicOrb().Hit(NPCZombie.HitType.Bullet, this.GetGun().GetDamage());
                    break;
                case Gun.GunType.FlameThrower:
                    int num1 = CollisionHelper.CollideEntities(ray, 0.0f, 4f, npcZombieArray, entitiesByType, ref results, 128, true);
                    for (int index = 0; index < num1; ++index)
                        ((NPCZombie)results[index].entity).Hit(NPCZombie.HitType.Burn, this.GetGun().GetDamage());
                    if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Boss)
                        break;
                    if (CollisionHelper.CollideEntities(ray, 0.0f, 4f, entities1, 1, ref results, 1, false) > 0)
                        currentWorld.GetBoss().Hit(NPCZombie.HitType.Burn, this.GetGun().GetDamage());
                    if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Chapel_Boss || CollisionHelper.CollideEntities(ray, 0.0f, 4f, entities2, 1, ref results, 1, false) <= 0)
                        break;
                    currentWorld.GetMagicOrb().Hit(NPCZombie.HitType.Burn, this.GetGun().GetDamage());
                    break;
                case Gun.GunType.Ice:
                    int num2 = CollisionHelper.CollideEntities(ray, 0.0f, 2.5f, npcZombieArray, entitiesByType, ref results, 128, true);
                    for (int index = 0; index < num2; ++index)
                    {
                        ((NPCZombie)results[index].entity).Hit(NPCZombie.HitType.Ice, this.GetGun().GetDamage());
                        ((NPCZombie)results[index].entity).FreezeTime = ((IceGun)this.GetGun()).GetFreezeTime();
                    }
                    if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Boss)
                        break;
                    if (CollisionHelper.CollideEntities(ray, 0.0f, 2.5f, entities1, 1, ref results, 1, false) > 0)
                        currentWorld.GetBoss().Hit(NPCZombie.HitType.Ice, this.GetGun().GetDamage());
                    if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Chapel_Boss || CollisionHelper.CollideEntities(ray, 0.0f, 2.5f, entities2, 1, ref results, 1, false) <= 0)
                        break;
                    currentWorld.GetMagicOrb().Hit(NPCZombie.HitType.Ice, this.GetGun().GetDamage());
                    break;
                case Gun.GunType.Explosive:
                    float x;
                    if (CollisionHelper.CollideEntities(ray, 0.0f, 4f, npcZombieArray, entitiesByType, ref results, 128, true) > 0)
                    {
                        NPCZombie entity = (NPCZombie)results[0].entity;
                        x = entity.GetTransform().GetPosition().X;
                        if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss && currentWorld.GetBoss().GetBossType() == NPCBoss.BossType.ButcherForm1 && CollisionHelper.CollideEntities(ray, 0.0f, 4f, entities1, 1, ref results, 1, false) > 0 && (double)Math.Abs(currentWorld.GetBoss().GetTransform().GetPosition().X - currentWorld.GetHero().GetTransform().GetPosition().X) < (double)Math.Abs(entity.GetTransform().GetPosition().X - currentWorld.GetHero().GetTransform().GetPosition().X))
                            x = currentWorld.GetBoss().GetTransform().GetPosition().X;
                    }
                    else
                    {
                        x = this.GetTransform().GetPosition().X + 4f * zero2.X;
                        if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
                        {
                            if (CollisionHelper.CollideEntities(ray, 0.0f, 4f, entities1, 1, ref results, 1, false) > 0)
                                x = currentWorld.GetBoss().GetBossType() == NPCBoss.BossType.NecromancerForm1 || currentWorld.GetBoss().GetBossType() == NPCBoss.BossType.NecromancerForm2 ? (((NPCNecromancer)currentWorld.GetBoss()).GetVulnerabilityType() == NPCNecromancer.VulnerabilityType.Bullet ? currentWorld.GetBoss().GetTransform().GetPosition().X : currentWorld.GetBoss().GetTransform().GetPosition().X - 1f) : currentWorld.GetBoss().GetTransform().GetPosition().X;
                            if (currentWorld.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss && CollisionHelper.CollideEntities(ray, 0.0f, 4f, entities2, 1, ref results, 1, false) > 0)
                                x = currentWorld.GetMagicOrb().GetVulnerabilityType() == NPCNecromancer.VulnerabilityType.Bullet ? currentWorld.GetMagicOrb().GetTransform().GetPosition().X : currentWorld.GetMagicOrb().GetTransform().GetPosition().X + 0.5f;
                        }
                    }
                    currentWorld.AddExplosion(x, 0.0f);
                    for (int index = 0; index < entitiesByType; ++index)
                    {
                        float num3 = x - npcZombieArray[index].GetTransform().GetPosition().X;
                        float num4 = num3 * num3;
                        if ((double)num4 < 2.25)
                        {
                            float num5 = (float)(1.0 - (double)num4 / 2.25);
                            npcZombieArray[index].Hit(NPCZombie.HitType.Explosion, (int)(this.GetGun().GetDamage() * (double)num5));
                        }
                    }
                    if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Boss)
                        break;
                    float num6 = x - currentWorld.GetBoss().GetTransform().GetPosition().X;
                    float num7 = num6 * num6;
                    if ((double)num7 < 2.25)
                    {
                        float num8 = (float)(1.0 - (double)num7 / 2.25);
                        currentWorld.GetBoss().Hit(NPCZombie.HitType.Explosion, (int)(this.GetGun().GetDamage() * (double)num8));
                    }
                    if (currentWorld.LevelIndex != GameWorld.LevelId.Level_Chapel_Boss)
                        break;
                    float num9 = x - currentWorld.GetMagicOrb().GetTransform().GetPosition().X;
                    float num10 = num9 * num9;
                    if ((double)num10 >= 2.25)
                        break;
                    double num11 = (double)num10 / 2.25;
                    currentWorld.GetMagicOrb().Hit(NPCZombie.HitType.Explosion, this.GetGun().GetDamage());
                    break;
                case Gun.GunType.HolyBeam:
                    int num12 = CollisionHelper.CollideEntities(ray, 0.0f, 6f, npcZombieArray, entitiesByType, ref results, 128, true);
                    for (int index = 0; index < num12; ++index)
                        ((NPCZombie)results[index].entity).Hit(NPCZombie.HitType.Holy, this.GetGun().GetDamage());
                    if (currentWorld.GetCurrentLevel().GetLevelType() != Level.LevelType.Boss || CollisionHelper.CollideEntities(ray, 0.0f, 6f, entities1, 1, ref results, 1, false) <= 0)
                        break;
                    currentWorld.GetBoss().Hit(NPCZombie.HitType.Holy, this.GetGun().GetDamage());
                    break;
            }
        }

        private void UpdateGunFX()
        {
            if (!this.m_gunFireView.IsVisible())
                return;
            if (this.GetAnimationStateMachine().CurrentState != HeroAnimationStateStandFireRight.Instance && this.GetAnimationStateMachine().CurrentState != HeroAnimationStateWalkFireRight.Instance && this.GunFireFXTransitionIndex < 2)
            {
                if (this.GetGun().GetGunType() == Gun.GunType.FlameThrower)
                {
                    this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[3]);
                    this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                    this.GetSound(SoundFxId.SoundFx_FlameThrower).Stop();
                }
                else if (this.GetGun().GetGunType() == Gun.GunType.Ice)
                {
                    this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[6]);
                    this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                    this.GetSound(SoundFxId.SoundFx_IceGun).Stop();
                }
                else if (this.GetGun().GetGunType() == Gun.GunType.HolyBeam)
                {
                    this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[9]);
                    this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                    this.GetSound(SoundFxId.SoundFx_HolyGun).Stop();
                }
                this.GunFireFXTransitionIndex = 2;
            }
            switch (this.GunFireFXTransitionIndex)
            {
                case 0:
                    if (!this.m_gunFireView.GetAnimation2D().IsEndOfAnimationReached())
                        break;
                    if (this.GetGun().GetGunType() == Gun.GunType.FlameThrower)
                    {
                        this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[2]);
                        this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                    }
                    else if (this.GetGun().GetGunType() == Gun.GunType.Ice)
                    {
                        this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[5]);
                        this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                    }
                    else if (this.GetGun().GetGunType() == Gun.GunType.HolyBeam)
                    {
                        this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[8]);
                        this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                    }
                    ++this.GunFireFXTransitionIndex;
                    break;
                case 1:
                    if (!this.GetGun().OutOfMana)
                        break;
                    if (this.GetGun().GetGunType() == Gun.GunType.FlameThrower)
                    {
                        this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[3]);
                        this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                        this.GetSound(SoundFxId.SoundFx_FlameThrower).Stop();
                    }
                    else if (this.GetGun().GetGunType() == Gun.GunType.Ice)
                    {
                        this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[6]);
                        this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                        this.GetSound(SoundFxId.SoundFx_IceGun).Stop();
                    }
                    else if (this.GetGun().GetGunType() == Gun.GunType.HolyBeam)
                    {
                        this.m_gunFireView.SetAnimation2D(this.GetAnimationStateMachine().GunFXAnimationArray[9]);
                        this.m_gunFireView.GetAnimation2D().SetTime(0.0f);
                        this.GetSound(SoundFxId.SoundFx_HolyGun).Stop();
                    }
                    ++this.GunFireFXTransitionIndex;
                    break;
                case 2:
                    if (!this.m_gunFireView.GetAnimation2D().IsEndOfAnimationReached())
                        break;
                    this.m_gunFireView.SetVisible(false);
                    this.GunFireFXTransitionIndex = 0;
                    break;
            }
        }

        private void LoadContent()
        {
            this.m_animationStateMachine.AnimationArray[0].Initialize("Anims\\nun_stand_body");
            this.m_animationStateMachine.AnimationArray[0].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[2].Initialize("Anims\\nun_stand_body_fire");
            this.m_animationStateMachine.AnimationArray[2].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[1].Initialize("Anims\\nun_stand_gun");
            this.m_animationStateMachine.AnimationArray[1].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[3].Initialize("Anims\\nun_stand_gun_fire_right");
            this.m_animationStateMachine.AnimationArray[4].Initialize("Anims\\nun_stand_gun_fire_right_tr");
            this.m_animationStateMachine.AnimationArray[5].Initialize("Anims\\nun_stand_gun_fire_right_tr_b");
            this.m_animationStateMachine.AnimationArray[6].Initialize("Anims\\nun_stand_slash1_body");
            this.m_animationStateMachine.AnimationArray[7].Initialize("Anims\\nun_stand_slash2_body");
            this.m_animationStateMachine.AnimationArray[8].Initialize("Anims\\nun_stand_slash3_body");
            this.m_animationStateMachine.AnimationArray[9].Initialize("Anims\\nun_stand_hit");
            this.m_animationStateMachine.AnimationArray[10].Initialize("Anims\\nun_grab_hit");
            this.m_animationStateMachine.AnimationArray[11].Initialize("Anims\\nun_grab_stand");
            this.m_animationStateMachine.AnimationArray[11].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[12].Initialize("Anims\\nun_grab_back");
            this.m_animationStateMachine.AnimationArray[13].Initialize("Anims\\nun_grab_dying");
            this.m_animationStateMachine.AnimationArray[14].Initialize("Anims\\nun_walk_body");
            this.m_animationStateMachine.AnimationArray[14].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[15].Initialize("Anims\\nun_walk_gun");
            this.m_animationStateMachine.AnimationArray[15].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[16].Initialize("Anims\\nun_walk_gun_fire_right");
            this.m_animationStateMachine.AnimationArray[17].Initialize("Anims\\nun_walk_gun_fire_right_tr");
            this.m_animationStateMachine.AnimationArray[18].Initialize("Anims\\nun_walk_gun_fire_right_tr_b");
            this.m_animationStateMachine.AnimationArray[19].Initialize("Anims\\nun_walk_slide_body");
            this.m_animationStateMachine.AnimationArray[20].Initialize("Anims\\nun_jump_body");
            this.m_animationStateMachine.AnimationArray[21].Initialize("Anims\\nun_jump_air_body");
            this.m_animationStateMachine.AnimationArray[22].Initialize("Anims\\nun_air_body");
            this.m_animationStateMachine.AnimationArray[22].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[23].Initialize("Anims\\nun_fall_body");
            this.m_animationStateMachine.AnimationArray[25].Initialize("Anims\\nun_land_body");
            this.m_animationStateMachine.AnimationArray[26].Initialize("Anims\\nun_air_slash2_finishing_body");
            this.m_animationStateMachine.AnimationArray[24].Initialize("Anims\\nun_air_hit");
            this.m_animationStateMachine.AnimationArray[27].Initialize("Anims\\nun_dying_body");
            this.m_animationStateMachine.AnimationArray[28].Initialize("Anims\\nun_dead_body");
            this.m_animationStateMachine.AnimationArray[28].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.AnimationArray[29].Initialize("Anims\\nun_crouch_body");
            this.m_animationStateMachine.GunFXAnimationArray[0].Initialize("Anims\\fx_gun_fire");
            this.m_animationStateMachine.GunFXAnimationArray[1].Initialize("Anims\\fx_flame_fire");
            this.m_animationStateMachine.GunFXAnimationArray[2].Initialize("Anims\\fx_flame_stand");
            this.m_animationStateMachine.GunFXAnimationArray[2].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.GunFXAnimationArray[3].Initialize("Anims\\fx_flame_end");
            this.m_animationStateMachine.GunFXAnimationArray[4].Initialize("Anims\\fx_ice_fire");
            this.m_animationStateMachine.GunFXAnimationArray[5].Initialize("Anims\\fx_ice_stand");
            this.m_animationStateMachine.GunFXAnimationArray[5].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.GunFXAnimationArray[6].Initialize("Anims\\fx_ice_end");
            this.m_animationStateMachine.GunFXAnimationArray[7].Initialize("Anims\\fx_rail_fire");
            this.m_animationStateMachine.GunFXAnimationArray[8].Initialize("Anims\\fx_rail_stand");
            this.m_animationStateMachine.GunFXAnimationArray[8].SetLoop(Animation2DLoop.Always);
            this.m_animationStateMachine.GunFXAnimationArray[9].Initialize("Anims\\fx_rail_end");
            int randomInt = MathHelperEx.GetRandomInt(0, 100);
            this.m_soundFxArray[0].Initialize("Sounds\\hero_slash", true);
            this.m_soundFxArray[1].Initialize("Sounds\\hero_slash_air", true);
            this.m_soundFxArray[2].Initialize("Sounds\\hero_switch_weapon", true);
            this.m_soundFxArray[3].Initialize("Sounds\\hero_weapon_gun", true);
            this.m_soundFxArray[4].Initialize("Sounds\\hero_weapon_flame", true);
            this.m_soundFxArray[4].SetLooping(true);
            this.m_soundFxArray[5].Initialize("Sounds\\hero_weapon_flame_start", true);
            this.m_soundFxArray[7].Initialize("Sounds\\hero_weapon_ice_start", true);
            this.m_soundFxArray[6].Initialize("Sounds\\hero_weapon_ice", true);
            this.m_soundFxArray[6].SetLooping(true);
            this.m_soundFxArray[9].Initialize("Sounds\\hero_weapon_holy_start", true);
            this.m_soundFxArray[8].Initialize("Sounds\\hero_weapon_holy", true);
            this.m_soundFxArray[8].SetLooping(true);
            this.m_soundFxArray[10].Initialize("Sounds\\hero_weapon_empty", true);
            this.m_soundFxArray[11].Initialize("Sounds\\hero_hit01", true);
            this.m_soundFxArray[12].Initialize("Sounds\\hero_hit02", true);
            if (randomInt < 33)
                this.m_soundFxArray[13].Initialize("Sounds\\hero_hit03", true);
            else if (randomInt < 66)
                this.m_soundFxArray[13].Initialize("Sounds\\hero_hit04", true);
            else
                this.m_soundFxArray[13].Initialize("Sounds\\hero_hit05", true);
            this.m_soundFxArray[14].Initialize("Sounds\\hero_dying01", true);
            if (randomInt < 50)
                this.m_soundFxArray[15].Initialize("Sounds\\hero_dying02", true);
            else
                this.m_soundFxArray[15].Initialize("Sounds\\hero_dying03", true);
            this.m_soundFxArray[16].Initialize("Sounds\\hero_jump01", true);
            this.m_soundFxArray[17].Initialize("Sounds\\hero_jump02", true);
            this.m_soundFxArray[18].Initialize("Sounds\\hero_land01", true);
            this.m_soundFxArray[19].Initialize("Sounds\\hero_pushback", true);
            this.m_soundFxArray[20].Initialize("Sounds\\hero_potion_pickup", true);
        }

        public enum SoundFxId
        {
            SoundFx_Slash_1,
            SoundFx_Slash_2,
            SoundFx_ChangeWeapon,
            SoundFx_Gun,
            SoundFx_FlameThrower,
            SoundFx_FlameThrower_Start,
            SoundFx_IceGun,
            SoundFx_IceGun_Start,
            SoundFx_HolyGun,
            SoundFx_HolyGun_Start,
            SoundFx_Weapon_Empty,
            SoundFx_Hit_1,
            SoundFx_Hit_2,
            SoundFx_Hit_3,
            SoundFx_Dying_1,
            SoundFx_Dying_2,
            SoundFx_Jump_1,
            SoundFx_Jump_2,
            SoundFx_Land,
            SoundFx_PushBack,
            SoundFx_Potion,
            SoundFx_ArraySize,
        }
    }
}
