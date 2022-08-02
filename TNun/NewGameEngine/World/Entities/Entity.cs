// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.Entities.Entity
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.Graphics;
using PSS.GameEngine.Maths;

namespace PSS.GameEngine.World.Entities
{
  public class Entity : IEntity
  {
    private const int ENTITY_MAX_COLLISION_BOXES = 8;
    public bool IsActive;
    protected EntityFactory m_factory;
    protected int m_type;
    protected Transform m_transform;
    protected string m_name;
    protected CollisionBox[] m_collisionBoxes;
    protected int m_collisionBoxCount;

    public Entity()
    {
      this.IsActive = true;
      this.m_type = 0;
      this.m_factory = (EntityFactory) null;
      this.m_collisionBoxCount = 0;
      this.m_collisionBoxes = new CollisionBox[8];
      this.m_transform = new Transform();
    }

    public virtual void Initialize()
    {
      this.IsActive = true;
      this.m_transform.Clear();
    }

    public virtual void Cleanup()
    {
      if (this.m_factory == null)
        return;
      this.m_factory.ReleaseEntity(this);
      this.m_factory = (EntityFactory) null;
    }

    public virtual void Update(float elapsedTime)
    {
    }

    public virtual void UpdateCollisions()
    {
    }

    public void SetFactory(EntityFactory factory) => this.m_factory = factory;

    public int GetEntityType() => this.m_type;

    public Transform GetTransform() => this.m_transform;

    public void SetName(string name) => this.m_name = name;

    public string GetName() => this.m_name;

    public CollisionBox[] GetCollisionBoxes() => this.m_collisionBoxes;

    public int GetCollisionBoxCount() => this.m_collisionBoxCount;

    protected virtual void UpdateCollisionBoxes(Animation2D anim, string boxname)
    {
      AABB[] buffer = new AABB[8];
      this.m_collisionBoxCount = anim.GetBoxes(buffer, 8, boxname);
      for (int index = 0; index < this.m_collisionBoxCount; ++index)
      {
        MathHelperEx.TransformBox(out this.m_collisionBoxes[index].box, buffer[index], this.m_transform.GetWorldTransform());
        this.m_collisionBoxes[index].tag = 0;
      }
    }
  }
}
