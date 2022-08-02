// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.Entities.EntityFactory
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.Tools;

namespace PSS.GameEngine.World.Entities
{
  public class EntityFactory
  {
    protected Pool<CameraEntity> m_cameraPool;
    protected Pool<ParallaxEntity> m_parallaxPool;

    public EntityFactory()
    {
      this.m_cameraPool = new Pool<CameraEntity>(8);
      this.m_parallaxPool = new Pool<ParallaxEntity>(96);
    }

    public virtual void Initialize()
    {
      this.m_cameraPool.Clear();
      this.m_parallaxPool.Clear();
    }

    public virtual void Cleanup()
    {
      this.m_cameraPool.Clear();
      this.m_parallaxPool.Clear();
    }

    public virtual Entity CreateEntity(string type)
    {
      Entity entity = (Entity) null;
      if (type == "camera")
      {
        entity = (Entity) this.m_cameraPool.Add();
        entity.SetFactory(this);
      }
      else if (type == "parallax")
      {
        entity = (Entity) this.m_parallaxPool.Add();
        entity.SetFactory(this);
      }
      return entity;
    }

    public virtual void ReleaseEntity(Entity entity)
    {
      switch (entity.GetEntityType())
      {
        case 1:
          this.m_parallaxPool.Remove((ParallaxEntity) entity);
          break;
        case 2:
          this.m_cameraPool.Remove((CameraEntity) entity);
          break;
      }
    }
  }
}
