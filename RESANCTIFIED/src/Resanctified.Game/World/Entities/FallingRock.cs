// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.World.Entities.FallingRock
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
  public class FallingRock : Entity
  {
    private Animation2DView m_view;
    private Animation2D m_anim;
    private Animation2D m_animStart;
    private Animation2D m_animEnd;
    private Physics2D m_physics;
    private Sound m_sound;
    private int m_state;

    public FallingRock()
    {
      this.IsActive = false;
      this.m_view = new Animation2DView();
      this.m_anim = new Animation2D();
      this.m_animStart = new Animation2D();
      this.m_animEnd = new Animation2D();
      this.m_sound = new Sound();
      this.m_physics = new Physics2D((Entity) this, 9f);
      this.m_state = 0;
    }

    public override void Initialize()
    {
      this.LoadContent();
      this.GetTransform().SetPosition(0.0f, 0.0f, 0.0f);
      this.m_view.GetTransform().SetPosition(0.0f, 0.0f, 3.8f);
      this.m_view.SetAnimation2D(this.m_animStart);
      this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
      this.IsActive = false;
      this.m_state = 0;
      this.m_physics.Initialize();
    }

    public override void Update(float gameTime)
    {
      if (!this.IsActive)
        return;
      switch (this.m_state)
      {
        case 0:
          if (this.m_view.GetAnimation2D().IsEndOfAnimationReached())
          {
            this.m_view.SetAnimation2D(this.m_anim);
            this.m_anim.SetTime(0.0f);
            ++this.m_state;
            break;
          }
          break;
        case 1:
          this.m_physics.Update(gameTime);
          if ((double) this.GetTransform().GetPosition().Y <= 0.0)
          {
            this.m_view.SetAnimation2D(this.m_animEnd);
            this.m_animEnd.SetTime(0.0f);
            this.m_sound.Play();
            ++this.m_state;
            break;
          }
          break;
        case 2:
          if (this.m_view.GetAnimation2D().IsEndOfAnimationReached())
          {
            this.m_view.SetAnimation2D(this.m_animStart);
            this.m_animStart.SetTime(0.0f);
            this.IsActive = false;
            this.m_state = 0;
            Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().RemoveTopLevelView((View) this.m_view);
            break;
          }
          break;
      }
      this.m_view.Update(gameTime);
    }

    public override void UpdateCollisions()
    {
      if (!this.IsActive)
        return;
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      AABB? box = this.GetView().GetAnimation2D().GetBox("attack1");
      if (!box.HasValue)
        return;
      AABB transformedBox;
      MathHelperEx.TransformBox(out transformedBox, box.Value, this.m_transform.GetWorldTransform());
      CollisionResult[] results = new CollisionResult[128];
      PlayableCharacter hero = currentWorld.GetHero();
      if (!hero.IsHit && !hero.GetIsInvulnerable())
      {
        Entity[] entities = new Entity[1]
        {
          (Entity) hero
        };
        if (CollisionHelper.CollideEntities(transformedBox, entities, 1, ref results, 1, false) > 0)
          hero.Hit(100, false);
      }
      NPCZombie[] npcZombieArray = new NPCZombie[55];
      int entitiesByType = currentWorld.GetEntityManager().GetEntitiesByType(3, (Entity[]) npcZombieArray, 55);
      int num = CollisionHelper.CollideEntities(transformedBox, (Entity[]) npcZombieArray, entitiesByType, ref results, 128, true);
      for (int index = 0; index < num; ++index)
      {
        NPCZombie entity = (NPCZombie) results[index].entity;
        entity.NoKillCount = true;
        entity.Hit(NPCZombie.HitType.NPC, 10000);
      }
    }

    public override void Cleanup()
    {
      this.m_anim.Cleanup();
      this.m_animStart.Cleanup();
      this.m_animEnd.Cleanup();
      this.m_view.Cleanup();
      this.m_sound.Cleanup();
    }

    public void Activate()
    {
      float randomFloat = MathHelperEx.GetRandomFloat(1f, 5f);
      this.GetTransform().SetPosition(randomFloat, 3f, 0.0f);
      this.m_view.SetAnimation2D(this.m_animStart);
      this.m_view.GetAnimation2D().SetTime(0.0f);
      this.IsActive = true;
      Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance().AddTopLevelView((View) this.m_view, this.GetTransform());
    }

    public Animation2DView GetView() => this.m_view;

    public Sound GetSound() => this.m_sound;

    private void LoadContent()
    {
      this.m_animStart.Initialize("Anims\\fx_falling_rock_start");
      this.m_animEnd.Initialize("Anims\\fx_falling_rock_end");
      this.m_anim.Initialize("Anims\\fx_falling_rock");
      this.m_anim.SetLoop(Animation2DLoop.Always);
      this.m_sound.Initialize("Sounds\\fx_rock_break", true);
    }
  }
}
