using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.FSM.NPC.Butcher;
using Resanctified.Game.FSM.NPC.Butcher.State;

namespace Resanctified.Game.World.Entities
{
    public class NPCButcher : NPCBoss
    {
        private Animation2DView m_view;
        private Animation2DView m_shadowView;
        private Animation2DView m_ballView;
        private NPCButcherFSM m_stateMachine;
        private int m_slashDamage;
        private int m_damageTaken;
        private Sound[] m_soundFxArray;
        public float ThrowTimer;
        public bool IsBallBrocken;
        public bool BallCollisionOff;
        public bool IsAngry;
        private float m_hitTimer;
        private float m_hitFxTimer;

        public NPCButcher()
        {
            this.m_stateMachine = new NPCButcherFSM(this);
            this.HitPoints = 3000;
            this.m_slashDamage = 70;
            this.m_bossType = BossType.ButcherForm1;
            this.m_soundFxArray = new Sound[4];
            for (int index = 0; index < 4; ++index)
                this.m_soundFxArray[index] = new Sound();
            this.m_view = new Animation2DView();
            this.m_ballView = new Animation2DView();
            this.m_shadowView = new Animation2DView();
            this.m_type = 5;
            this.ThrowTimer = 0.0f;
            this.IsAngry = false;
            this.IsBallBrocken = false;
            this.m_damageTaken = 0;
            this.m_hitTimer = 0.0f;
            this.m_hitFxTimer = 0.0f;
            this.BallCollisionOff = false;
        }

        public override void Initialize() => this.Initialize(BossType.ButcherForm1);

        public void Initialize(BossType type)
        {
            this.m_bossType = type;
            this.HitPoints = 3000;
            this.m_slashDamage = 70;
            if (GameApplication.GetInstance().GetDifficulty() == GameApplication.GameDifficulty.Hard)
            {
                this.HitPoints += 1500;
                this.m_slashDamage += 40;
            }
            this.ThrowTimer = 0.0f;
            this.IsAngry = false;
            this.IsBallBrocken = false;
            this.BallCollisionOff = false;
            this.m_damageTaken = 0;
            this.m_hitTimer = 0.0f;
            this.m_hitFxTimer = 0.0f;
            this.m_physics.Initialize();
            this.LoadContent();
            this.m_stateMachine.Initialize();
            this.m_view.Initialize();
            this.m_view.GetTransform().SetPosition(0.0f, 0.0f, -2f);
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_ballView.Initialize();
            this.m_ballView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_ballView.GetTransform().SetPosition(0.0f, 0.0f, 1f);
            this.m_shadowView.Initialize();
            this.m_shadowView.SetAnimation2D(((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetMiscAnimation(GameWorld.MiscAnimId.ZombieShadow));
            this.m_shadowView.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_shadowView.GetTransform().Clear();
            this.m_shadowView.GetTransform().SetPosition(0.1f, 0.0f, -2.2f);
            this.m_shadowView.GetTransform().SetScale(2f, 1.6f);
            this.GetTransform().SetPosition(6.5f, 0.0f, 0.0f);
            this.GetTransform().SetScale(-1f, 1f);
            this.m_stateMachine.SetState(NPCButcherStateStand.GetInstance());
            Renderer.GetInstance().AddTopLevelView(this.m_view, this.GetTransform());
            this.m_view.AddSubview(this.m_shadowView);
            this.m_view.AddSubview(this.m_ballView);
        }

        public override void Update(float elapsedTime)
        {
            this.ThrowTimer += elapsedTime;
            this.m_stateMachine.Update(elapsedTime);
            this.m_view.Update(elapsedTime);
            this.m_ballView.Update(elapsedTime);
            this.m_shadowView.Update(elapsedTime);
            this.m_physics.Update(elapsedTime);
            this.UpdateCollisionBoxes(this.GetView().GetAnimation2D(), "hit");
            if (this.IsHit)
            {
                this.m_hitTimer += elapsedTime;
                if (this.m_hitTimer > 0.100000001490116)
                {
                    this.m_hitTimer = 0.0f;
                    this.IsHit = false;
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
            base.Update(elapsedTime);
        }

        public override void UpdateCollisions()
        {
            AABB? box = this.GetView().GetAnimation2D().GetBox("slash1");
            Color color;
            AABB? boxAndColor = this.GetBallView().GetAnimation2D().GetBoxAndColor("slash1", out color);
            AABB transformedBox;
            if (box.HasValue)
            {
                MathHelperEx.TransformBox(out transformedBox, box.Value, this.m_transform.GetWorldTransform());
                this.UpdateAttackCollision(ref transformedBox);
            }
            if (this.BallCollisionOff || !boxAndColor.HasValue || color.A == 0)
                return;
            MathHelperEx.TransformBox(out transformedBox, boxAndColor.Value, this.m_transform.GetWorldTransform());
            this.UpdateAttackCollision(ref transformedBox);
        }

        private void UpdateAttackCollision(ref AABB attackBox)
        {
            PlayableCharacter hero = ((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetHero();
            CollisionResult[] results = new CollisionResult[128];
            if (!hero.IsHit && !hero.GetIsInvulnerable())
            {
                Entity[] entities = new Entity[1]
                {
           hero
                };
                if (CollisionHelper.CollideEntities(attackBox, entities, 1, ref results, 1, false) > 0)
                    hero.Hit(this.GetSlashDamage(), false);
            }
            NPCZombie[] npcZombieArray = new NPCZombie[55];
            int entitiesByType = WorldManager.GetInstance().GetCurrentWorld().GetEntityManager().GetEntitiesByType(3, npcZombieArray, 55);
            int num = CollisionHelper.CollideEntities(attackBox, npcZombieArray, entitiesByType, ref results, 128, true);
            for (int index = 0; index < num; ++index)
            {
                NPCZombie entity = (NPCZombie)results[index].entity;
                entity.NoKillCount = true;
                entity.Hit(NPCZombie.HitType.NPC, 10000);
            }
        }

        public override void Cleanup()
        {
            this.m_stateMachine.Cleanup();
            for (int index = 0; index < 4; ++index)
                this.m_soundFxArray[index].Cleanup();
            this.m_view.Cleanup();
            this.m_shadowView.Cleanup();
            this.m_ballView.Cleanup();
            base.Cleanup();
        }

        public override int Hit(NPCZombie.HitType hitType, int damage)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float x1 = this.GetTransform().GetScale().X;
            float x2 = this.GetTransform().GetPosition().X + x1 * MathHelperEx.GetRandomFloat(-0.2f, 0.6f);
            float randomFloat = MathHelperEx.GetRandomFloat(0.5f, 1.2f);
            this.IsHit = true;
            this.m_currentHitType = hitType;
            this.m_hitFxTimer = 0.5f;
            switch (hitType)
            {
                case NPCZombie.HitType.Slash:
                case NPCZombie.HitType.AirSlash:
                    currentWorld.AddBulletImpact(x2, randomFloat, x1, BulletImpact.ImpactType.Blood);
                    break;
                case NPCZombie.HitType.Bullet:
                    currentWorld.AddBulletImpact(x2, randomFloat, x1, BulletImpact.ImpactType.Blood);
                    break;
                case NPCZombie.HitType.Explosion:
                    currentWorld.AddBulletImpact(x2, randomFloat, x1, BulletImpact.ImpactType.Blood);
                    break;
                case NPCZombie.HitType.Burn:
                    currentWorld.AddBulletImpact(x2, randomFloat, x1, BulletImpact.ImpactType.Burn);
                    break;
                case NPCZombie.HitType.Ice:
                    currentWorld.AddBulletImpact(x2, randomFloat, x1, BulletImpact.ImpactType.Ice);
                    break;
                case NPCZombie.HitType.Holy:
                    currentWorld.AddBulletImpact(x2, randomFloat, x1, BulletImpact.ImpactType.Holy);
                    break;
            }
            int num = damage;
            this.HitPoints -= damage;
            if (this.HitPoints <= 0)
            {
                num += this.HitPoints;
                this.HitPoints = 0;
            }
            this.m_damageTaken += num;
            if (this.m_damageTaken >= 300)
            {
                this.m_damageTaken = 0;
                this.IsAngry = true;
            }
            if (this.HitPoints <= 1500)
                this.ThrowTimer = 4f;
            return num;
        }

        public override bool IsDead() => this.m_stateMachine.GetCurrentState() == NPCButcherStateDead.GetInstance();

        public Animation2DView GetShadowView() => this.m_shadowView;

        public Animation2DView GetView() => this.m_view;

        public Animation2DView GetBallView() => this.m_ballView;

        public NPCButcherFSM GetStateMachine() => this.m_stateMachine;

        public Sound GetSound(SoundFxId soundId) => this.m_soundFxArray[(int)soundId];

        public int GetSlashDamage() => this.m_slashDamage;

        private void LoadContent()
        {
            this.m_stateMachine.AnimationArray[0].Initialize("Anims\\b_butcher_stand");
            this.m_stateMachine.AnimationArray[0].SetLoop(Animation2DLoop.Always);
            this.m_stateMachine.AnimationArray[1].Initialize("Anims\\b_butcher_walk_slash");
            this.m_stateMachine.AnimationArray[1].SetLoop(Animation2DLoop.Always);
            this.m_stateMachine.AnimationArray[2].Initialize("Anims\\b_butcher_stand_slash");
            this.m_stateMachine.AnimationArray[3].Initialize("Anims\\b_butcher_throw");
            this.m_stateMachine.AnimationArray[4].Initialize("Anims\\b_butcher_angry");
            this.m_stateMachine.AnimationArray[5].Initialize("Anims\\b_butcher_dying");
            this.m_stateMachine.AnimationArray[6].Initialize("Anims\\b_butcher_ball_stand");
            this.m_stateMachine.AnimationArray[6].SetLoop(Animation2DLoop.Always);
            this.m_stateMachine.AnimationArray[7].Initialize("Anims\\b_butcher_ball_walk_slash");
            this.m_stateMachine.AnimationArray[7].SetLoop(Animation2DLoop.Always);
            this.m_stateMachine.AnimationArray[8].Initialize("Anims\\b_butcher_ball_stand_slash");
            this.m_stateMachine.AnimationArray[9].Initialize("Anims\\b_butcher_ball_throw");
            this.m_stateMachine.AnimationArray[10].Initialize("Anims\\b_butcher_ball_throw_break");
            this.m_stateMachine.AnimationArray[11].Initialize("Anims\\b_butcher_ball_angry");
            this.m_stateMachine.AnimationArray[12].Initialize("Anims\\b_butcher_ball_dying");
            this.m_stateMachine.AnimationArray[13].Initialize("Anims\\b_butcher_ball_stand_tr");
            this.m_soundFxArray[0].Initialize("Sounds\\butcher_slash", true);
            this.m_soundFxArray[1].Initialize("Sounds\\butcher_throw", true);
            this.m_soundFxArray[2].Initialize("Sounds\\butcher_angry", true);
            this.m_soundFxArray[3].Initialize("Sounds\\butcher_dying", true);
        }

        public enum SoundFxId
        {
            SoundFx_Slash,
            SoundFx_Throw,
            SoundFx_Angry,
            SoundFx_Dying,
            SoundFx_ArraySize,
        }
    }
}
