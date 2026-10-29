// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.Entities.EntityManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System.Collections.Generic;

namespace PSS.GameEngine.World.Entities
{
  public class EntityManager
  {
    protected List<Entity> m_entityList;

    public EntityManager() => this.m_entityList = new List<Entity>(256);

    public void Initialize()
    {
    }

    public void Cleanup()
    {
      for (int index = 0; index < this.m_entityList.Count; ++index)
        this.m_entityList[index].Cleanup();
      this.m_entityList.Clear();
    }

    public void Update(float elapsedTime)
    {
      for (int index = 0; index < this.m_entityList.Count; ++index)
        this.m_entityList[index].Update(elapsedTime);
    }

    public void AddEntity(Entity entity) => this.m_entityList.Add(entity);

    public void RemoveEntity(Entity entity) => this.m_entityList.Remove(entity);

    public int GetEntitiesByName(string name, Entity[] buffer, int length)
    {
      int entitiesByName = 0;
      for (int index = 0; index < this.m_entityList.Count; ++index)
      {
        if (this.m_entityList[index].GetName().Equals(name))
        {
          buffer[entitiesByName] = this.m_entityList[index];
          ++entitiesByName;
        }
      }
      return entitiesByName;
    }

    public int GetEntitiesByType(int type, Entity[] buffer, int length)
    {
      int entitiesByType = 0;
      for (int index = 0; index < this.m_entityList.Count; ++index)
      {
        if (this.m_entityList[index].GetEntityType() == type)
        {
          buffer[entitiesByType] = this.m_entityList[index];
          ++entitiesByType;
        }
      }
      return entitiesByType;
    }

    public int GetNumEntities() => this.m_entityList.Count;

    public Entity GetEntityAtIndex(int index) => this.m_entityList[index];
  }
}
