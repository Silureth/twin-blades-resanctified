using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.FSM.NPC.Zombie;
using Resanctified.Game.FSM.NPC.Zombie.State;

namespace Resanctified.Game.World.Entities
{
    public class NPCZombie : Entity
    {
        public bool IsHit;
        public bool IsHitReset;
        public bool IsAlive;
        public bool IsBurning;
        public int HitPoints;
        public float FreezeTime;
        public float FreezeRatio;
        public bool BreakingIce;
        public bool NoKillCount;
        public float Timer;
        private float m_hitFxTimer;
        private Animation2DView m_view;
        private Animation2DView m_iceView;
        private Animation2DView m_shadowView;
        private NPCZombieFSM m_stateMachine;
        private Physics2D m_physics;
        private HitType m_currentHitType;
        private int m_hitPointsMax;
        private int m_slashDamage;
        private int m_grabDamage;
        private ZombieType m_zombieType;
        private Sound[] m_soundFxArray;

        public NPCZombie()
        {
            this.m_stateMachine = new NPCZombieFSM(this);
            this.m_physics = new Physics2D(this, 0.0f);
            this.IsHit = false;
            this.IsHitReset = false;
            this.IsAlive = false;
            this.IsBurning = false;
            this.HitPoints = 10;
            this.m_hitPointsMax = 10;
            this.m_slashDamage = 10;
            this.m_grabDamage = 8;
            this.m_currentHitType = HitType.None;
            this.m_hitFxTimer = 0.0f;
            this.FreezeTime = 0.0f;
            this.FreezeRatio = 0.0f;
            this.BreakingIce = false;
            this.NoKillCount = false;
            this.Timer = 0.0f;
            this.m_zombieType = ZombieType.Normal;
            this.m_soundFxArray = new Sound[17];
            for (int index = 0; index < 17; ++index)
                this.m_soundFxArray[index] = new Sound();
            this.m_view = new Animation2DView();
            this.m_iceView = new Animation2DView();
            this.m_shadowView = new Animation2DView();
            this.m_type = 3;
        }

        public static int GetHitColor(float time)
        {
            double num = (double)time / 0.5;
            int hitColor = (int)(205.0 * (1.0 - (double)time)) + 50;
            if (hitColor < 50)
                hitColor = 50;
            if (hitColor > byte.MaxValue)
                hitColor = byte.MaxValue;
            return hitColor;
        }

        public override void Initialize() => this.Initialize(1, ZombieType.Normal);

        public void Initialize(int level) => this.Initialize(level, ZombieType.Normal);

        public void Initialize(int level, ZombieType type)
        {
            this.m_zombieType = type;
            this.m_hitFxTimer = 0.0f;
            this.LoadContent();
            this.m_stateMachine.Initialize();
            this.m_view.Initialize();
            this.m_view.GetTransform().SetPosition(0.0f, 0.0f, -1f);
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_iceView.Initialize();
            this.m_iceView.GetTransform().SetPosition(0.0f, 0.0f, 0.1f);
            this.m_iceView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.InitLevelParameters(level);
            this.m_shadowView.Initialize();
            this.m_shadowView.SetAnimation2D(((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetMiscAnimation(GameWorld.MiscAnimId.ZombieShadow));
            this.m_shadowView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shadowView.GetTransform().Clear();
            this.m_shadowView.GetTransform().SetPosition(0.0f, 0.0f, -1.2f);
        }

        public void InitLevelParameters(int level)
        {
            this.m_hitPointsMax = this.GetHitPointsMax();
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                level += this.StoryLevelAdd();
            if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                level += 10;
            if (this.GetZombieType() == ZombieType.ArmoredNormal)
            {
                this.m_slashDamage = 15 + (level - 1) * 4;
                if (this.m_slashDamage > 150)
                    this.m_slashDamage = 150;
            }
            else
            {
                this.m_slashDamage = 10 + (level - 1) * 3;
                if (this.m_slashDamage > 100)
                    this.m_slashDamage = 100;
            }
            this.m_grabDamage = (int)(8.0 + (level - 1) * 1.5);
            if (this.m_grabDamage <= 50)
                return;
            this.m_grabDamage = 50;
        }

        private int StoryLevelAdd()
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            int currentMissionIndex = Scenario.GetInstance().GetCurrentMissionIndex();
            int num = currentWorld.GetCurrentLevel().GetInfestationLevel() - 1;
            if (num < 0)
                num = 0;
            return currentMissionIndex + num * 4;
        }

        public override void Update(float elapsedTime)
        {
            this.m_stateMachine.Update(elapsedTime);
            this.FreezingUpdate(elapsedTime);
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
                    int hitColor = GetHitColor(this.m_hitFxTimer);
                    this.GetView().SetCustomColor((byte)hitColor, byte.MaxValue, (byte)hitColor, byte.MaxValue);
                    this.GetView().SetCustomColorEnabled(true);
                }
            }
            this.m_view.Update(elapsedTime);
            this.m_iceView.Update(elapsedTime);
            this.m_shadowView.Update(elapsedTime);
            this.m_physics.Update(elapsedTime);
            Vector3 position = this.GetTransform().GetPosition();
            float y = (float)(1.0 + (double)(MathHelper.Clamp(position.Y, 0.0f, 1.4f) / 1.4f) * -0.600000023841858);
            this.m_shadowView.GetTransform().SetScale((this.GetTransform().GetScale().X < 0.0 ? -1f : 1f) * y, y);
            this.m_shadowView.GetTransform().SetPosition(position.X, 0.0f, -1.2f);
            this.UpdateCollisionBoxes(this.GetView().GetAnimation2D(), "hit");
            base.Update(elapsedTime);
        }

        public override void UpdateCollisions()
        {
            if (this.IsHit || this.m_stateMachine.GetCurrentState() != NPCZombieStateGrab.GetInstance() && this.m_stateMachine.GetCurrentState() != NPCZombieStateSlash.GetInstance())
                return;
            AABB? box = this.GetView().GetAnimation2D().GetBox("slash1");
            if (!box.HasValue)
                return;
            PlayableCharacter hero = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero();
            if (hero.IsHit || hero.GetIsInvulnerable() || hero.IsInAir)
                return;
            AABB transformedBox;
            MathHelperEx.TransformBox(out transformedBox, box.Value, this.m_transform.GetWorldTransform());
            CollisionResult[] results = new CollisionResult[1];
            Entity[] entities = new Entity[1]
            {
         hero
            };
            if (CollisionHelper.CollideEntities(transformedBox, entities, 1, ref results, 1, false) <= 0)
                return;
            if (this.m_stateMachine.GetCurrentState() == NPCZombieStateGrab.GetInstance())
                hero.Hit(this.GetGrabDamage(), true);
            else
                hero.Hit(this.GetSlashDamage(), false);
        }

        public override void Cleanup()
        {
            this.m_stateMachine.Cleanup();
            for (int index = 0; index < 17; ++index)
                this.m_soundFxArray[index].Cleanup();
            this.m_view.Cleanup();
            this.m_iceView.Cleanup();
            this.m_shadowView.Cleanup();
            base.Cleanup();
        }

        public void Reset()
        {
            this.m_hitPointsMax = this.GetHitPointsMax();
            this.HitPoints = this.m_hitPointsMax;
            this.IsHit = false;
            this.IsHitReset = false;
            this.IsAlive = false;
            this.IsBurning = false;
            this.FreezeTime = 0.0f;
            this.FreezeRatio = 0.0f;
            this.BreakingIce = false;
            this.Timer = 0.0f;
            this.m_currentHitType = HitType.None;
            this.m_physics.Acceleration.X = 0.0f;
            this.m_physics.Acceleration.Y = 0.0f;
            this.m_physics.Velocity.X = 0.0f;
            this.m_physics.Velocity.Y = 0.0f;
            this.GetView().RemoveSubview(this.GetIceView());
            Renderer.GetInstance().RemoveTopLevelView(this.m_shadowView);
        }

        public int Hit(HitType hitType, int damage)
        {
            this.IsHit = true;
            this.IsHitReset = true;
            this.m_currentHitType = hitType;
            if (hitType == HitType.Ice)
            {
                this.IsHit = false;
                float num = 1f;
                if (this.HitPoints > this.m_hitPointsMax * 0.25)
                    num = (float)(1.0 - this.HitPoints / (double)this.m_hitPointsMax);
                this.FreezeRatio += 0.3f * num;
                if (this.FreezeRatio >= 1.0)
                {
                    this.IsHit = true;
                    this.FreezeRatio = 1f;
                    return 0;
                }
            }
            if (this.m_stateMachine.GetCurrentState() != NPCZombieStateFreeze.GetInstance())
            {
                switch (hitType)
                {
                    case HitType.Slash:
                    case HitType.AirSlash:
                    case HitType.Bullet:
                    case HitType.Explosion:
                        this.m_hitFxTimer = 0.5f;
                        break;
                }
            }
            if (this.GetZombieType() == ZombieType.ArmoredNormal || this.GetZombieType() == ZombieType.ArmoredGrabber)
            {
                switch (hitType)
                {
                    case HitType.Slash:
                    case HitType.AirSlash:
                        damage = (int)(damage * 0.600000023841858);
                        break;
                    case HitType.Bullet:
                        damage = (int)(damage * 0.600000023841858);
                        break;
                    case HitType.Explosion:
                        damage = (int)(damage * 0.5);
                        break;
                    case HitType.Burn:
                        damage = (int)(damage * 0.800000011920929);
                        break;
                    case HitType.Ice:
                        damage = (int)(damage * 0.899999976158142);
                        break;
                    case HitType.Holy:
                        damage = (int)(damage * 0.800000011920929);
                        break;
                }
            }
            int num1 = damage;
            this.HitPoints -= damage;
            if (this.HitPoints <= 0)
            {
                num1 += this.HitPoints;
                this.HitPoints = 0;
            }
            return num1;
        }

        public Animation2DView GetShadowView() => this.m_shadowView;

        public Animation2DView GetIceView() => this.m_iceView;

        public Animation2DView GetView() => this.m_view;

        public NPCZombieFSM GetStateMachine() => this.m_stateMachine;

        public Physics2D GetPhysics() => this.m_physics;

        public HitType GetHitType() => this.m_currentHitType;

        public void SetHitType(HitType type) => this.m_currentHitType = type;

        public Sound GetSound(SoundFxId soundId) => this.m_soundFxArray[(int)soundId];

        public void SetZombieType(ZombieType type) => this.m_zombieType = type;

        public ZombieType GetZombieType() => this.m_zombieType;

        public int GetSlashDamage() => this.m_slashDamage;

        public int GetGrabDamage() => this.m_grabDamage;

        private int GetHitPointsMax()
        {
            int hitPointsMax = 0;
            int num = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).DayIndex - 1;
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                num += this.StoryLevelAdd();
            if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
                num += 10;
            switch (this.GetZombieType())
            {
                case ZombieType.Normal:
                    hitPointsMax = 10 + num * 6;
                    if (hitPointsMax > 180)
                    {
                        hitPointsMax = 180;
                        break;
                    }
                    break;
                case ZombieType.Grabber:
                    hitPointsMax = 20 + num * 8;
                    if (hitPointsMax > 240)
                    {
                        hitPointsMax = 240;
                        break;
                    }
                    break;
                case ZombieType.ArmoredNormal:
                    hitPointsMax = 10 + num * 6;
                    if (hitPointsMax > 180)
                    {
                        hitPointsMax = 180;
                        break;
                    }
                    break;
                case ZombieType.ArmoredGrabber:
                    hitPointsMax = 20 + num * 8;
                    if (hitPointsMax > 240)
                    {
                        hitPointsMax = 240;
                        break;
                    }
                    break;
            }
            return hitPointsMax;
        }

        private void FreezingUpdate(float elapsedTime)
        {
            if (this.m_stateMachine.GetCurrentState() == NPCZombieStateFreeze.GetInstance())
                return;
            if (this.BreakingIce && this.GetIceView().GetAnimation2D().IsEndOfAnimationReached())
            {
                this.GetView().RemoveSubview(this.GetIceView());
                this.BreakingIce = false;
            }
            if (this.FreezeRatio > 0.0)
            {
                this.FreezeRatio -= elapsedTime * 0.2f;
                if (this.FreezeRatio < 0.0)
                    this.FreezeRatio = 0.0f;
                if (this.GetView().GetAnimation2D() == this.m_stateMachine.AnimationArray[17] || this.GetView().GetAnimation2D() == this.m_stateMachine.AnimationArray[15] || this.GetView().GetAnimation2D() == this.m_stateMachine.AnimationArray[16])
                {
                    this.FreezeRatio = 0.0f;
                    this.GetView().SetCustomColorEnabled(false);
                }
                else
                {
                    if (!this.IsAlive)
                        this.m_view.GetAnimation2D().SetSpeed(1f);
                    else
                        this.m_view.GetAnimation2D().SetSpeed(1f - this.FreezeRatio);
                    this.m_physics.Velocity.X *= 1f - this.FreezeRatio;
                    int num = (int)(byte.MaxValue * (1.0 - this.FreezeRatio));
                    if (num < 128)
                        num = 128;
                    this.GetView().SetCustomColor((byte)num, (byte)num, byte.MaxValue, byte.MaxValue);
                    this.GetView().SetCustomColorEnabled(true);
                }
            }
            else
                this.GetView().SetCustomColorEnabled(false);
        }

        private void LoadContent()
        {
            if (this.m_zombieType == ZombieType.ArmoredNormal || this.m_zombieType == ZombieType.ArmoredGrabber)
            {
                this.m_stateMachine.AnimationArray[0].Initialize("Anims\\zombie_arm_stand");
                this.m_stateMachine.AnimationArray[0].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[1].Initialize("Anims\\zombie_arm_spawn");
                this.m_stateMachine.AnimationArray[2].Initialize("Anims\\zombie_arm_walk1");
                this.m_stateMachine.AnimationArray[2].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[3].Initialize("Anims\\zombie_arm_walk2");
                this.m_stateMachine.AnimationArray[3].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[4].Initialize("Anims\\zombie_arm_slash");
                this.m_stateMachine.AnimationArray[5].Initialize("Anims\\zombie_arm_grab");
                this.m_stateMachine.AnimationArray[6].Initialize("Anims\\zombie_arm_grab_eat");
                this.m_stateMachine.AnimationArray[6].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[7].Initialize("Anims\\zombie_arm_grab_back");
                this.m_stateMachine.AnimationArray[8].Initialize("Anims\\zombie_arm_hit");
                this.m_stateMachine.AnimationArray[9].Initialize("Anims\\zombie_arm_hit2");
                this.m_stateMachine.AnimationArray[10].Initialize("Anims\\zombie_arm_hit_burn");
                this.m_stateMachine.AnimationArray[11].Initialize("Anims\\zombie_arm_hit_holy");
                this.m_stateMachine.AnimationArray[12].Initialize("Anims\\zombie_arm_dying_body_shot");
                this.m_stateMachine.AnimationArray[13].Initialize("Anims\\zombie_arm_dying_head_shot");
                this.m_stateMachine.AnimationArray[14].Initialize("Anims\\zombie_arm_dying_body_cut");
                this.m_stateMachine.AnimationArray[15].Initialize("Anims\\zombie_arm_dying_burn");
                this.m_stateMachine.AnimationArray[16].Initialize("Anims\\zombie_arm_dying_holy");
                this.m_stateMachine.AnimationArray[17].Initialize("Anims\\zombie_arm_dying_ice");
                this.m_stateMachine.AnimationArray[18].Initialize("Anims\\zombie_icebloc_appear");
                this.m_stateMachine.AnimationArray[19].Initialize("Anims\\zombie_icebloc_stand");
                this.m_stateMachine.AnimationArray[19].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[20].Initialize("Anims\\zombie_icebloc_melt");
            }
            else
            {
                this.m_stateMachine.AnimationArray[0].Initialize("Anims\\zombie_stand");
                this.m_stateMachine.AnimationArray[0].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[1].Initialize("Anims\\zombie_spawn");
                this.m_stateMachine.AnimationArray[2].Initialize("Anims\\zombie_walk1");
                this.m_stateMachine.AnimationArray[2].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[3].Initialize("Anims\\zombie_walk2");
                this.m_stateMachine.AnimationArray[3].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[4].Initialize("Anims\\zombie_slash");
                this.m_stateMachine.AnimationArray[5].Initialize("Anims\\zombie_grab");
                this.m_stateMachine.AnimationArray[6].Initialize("Anims\\zombie_grab_eat");
                this.m_stateMachine.AnimationArray[6].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[7].Initialize("Anims\\zombie_grab_back");
                this.m_stateMachine.AnimationArray[8].Initialize("Anims\\zombie_hit");
                this.m_stateMachine.AnimationArray[9].Initialize("Anims\\zombie_hit2");
                this.m_stateMachine.AnimationArray[10].Initialize("Anims\\zombie_hit_burn");
                this.m_stateMachine.AnimationArray[11].Initialize("Anims\\zombie_hit_holy");
                this.m_stateMachine.AnimationArray[12].Initialize("Anims\\zombie_dying_body_shot");
                this.m_stateMachine.AnimationArray[13].Initialize("Anims\\zombie_dying_head_shot");
                this.m_stateMachine.AnimationArray[14].Initialize("Anims\\zombie_dying_body_cut");
                this.m_stateMachine.AnimationArray[15].Initialize("Anims\\zombie_dying_burn");
                this.m_stateMachine.AnimationArray[16].Initialize("Anims\\zombie_dying_holy");
                this.m_stateMachine.AnimationArray[17].Initialize("Anims\\zombie_dying_ice");
                this.m_stateMachine.AnimationArray[18].Initialize("Anims\\zombie_icebloc_appear");
                this.m_stateMachine.AnimationArray[19].Initialize("Anims\\zombie_icebloc_stand");
                this.m_stateMachine.AnimationArray[19].SetLoop(Animation2DLoop.Always);
                this.m_stateMachine.AnimationArray[20].Initialize("Anims\\zombie_icebloc_melt");
            }
            this.m_soundFxArray[0].Initialize("Sounds\\zombie_walk01", true);
            this.m_soundFxArray[1].Initialize("Sounds\\zombie_walk02", true);
            this.m_soundFxArray[2].Initialize("Sounds\\zombie_spawn01", true);
            this.m_soundFxArray[3].Initialize("Sounds\\zombie_spawn02", true);
            this.m_soundFxArray[4].Initialize("Sounds\\zombie_attack01", true);
            this.m_soundFxArray[5].Initialize("Sounds\\zombie_attack02", true);
            this.m_soundFxArray[6].Initialize("Sounds\\zombie_grab_eat01", true);
            this.m_soundFxArray[6].SetLooping(true);
            this.m_soundFxArray[7].Initialize("Sounds\\zombie_hit01", true);
            this.m_soundFxArray[8].Initialize("Sounds\\zombie_hit02", true);
            this.m_soundFxArray[9].Initialize("Sounds\\zombie_hit03", true);
            this.m_soundFxArray[10].Initialize("Sounds\\zombie_pushback01", true);
            this.m_soundFxArray[11].Initialize("Sounds\\zombie_iced", true);
            this.m_soundFxArray[12].Initialize("Sounds\\zombie_ice_break", true);
            this.m_soundFxArray[13].Initialize("Sounds\\zombie_burning", true);
            this.m_soundFxArray[13].SetLooping(true);
            this.m_soundFxArray[14].Initialize("Sounds\\zombie_dying_bodyshot", true);
            this.m_soundFxArray[15].Initialize("Sounds\\zombie_dying_headshot", true);
            this.m_soundFxArray[16].Initialize("Sounds\\zombie_dying_burned", true);
            if (((GameWorld)GameWorldManager.GetInstance().GetCurrentWorld()).GetCurrentLevel().GetLevelType() != Level.LevelType.Boss)
                return;
            for (int index = 0; index < 17; ++index)
                this.m_soundFxArray[index].SetGain(0.5f);
        }

        public enum HitType
        {
            Slash,
            AirSlash,
            Bullet,
            Explosion,
            Burn,
            Ice,
            Holy,
            NPC,
            None,
        }

        public enum SoundFxId
        {
            SoundFx_Walk_1,
            SoundFx_Walk_2,
            SoundFx_Spawn_1,
            SoundFx_Spawn_2,
            SoundFx_Attack_1,
            SoundFx_Attack_2,
            SoundFx_GrabEat_1,
            SoundFx_Hit_1,
            SoundFx_Hit_2,
            SoundFx_Hit_3,
            SoundFx_Push_Back,
            SoundFx_Iced,
            SoundFx_IceBreak,
            SoundFx_Burning,
            SoundFx_Dying_BodyShot,
            SoundFx_Dying_HeadShot,
            SoundFx_Dying_Burned,
            SoundFx_ArraySize,
        }
    }
}
