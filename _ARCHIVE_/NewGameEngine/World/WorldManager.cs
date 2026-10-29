// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.WorldManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System.Collections.Generic;

namespace PSS.GameEngine.World
{
  public class WorldManager : IGameComponent
  {
    protected static WorldManager m_instance;
    protected PSS.GameEngine.World.World m_currentWorld;
    protected List<PSS.GameEngine.World.World> m_worldList;

    public static WorldManager Instance
    {
      get
      {
        if (WorldManager.m_instance == null)
          WorldManager.m_instance = new WorldManager();
        return WorldManager.m_instance;
      }
    }

    public static WorldManager GetInstance() => WorldManager.Instance;

    public WorldManager()
    {
      this.m_worldList = new List<PSS.GameEngine.World.World>();
      this.m_currentWorld = (PSS.GameEngine.World.World) null;
    }

    public virtual void Initialize() => this.m_currentWorld = (PSS.GameEngine.World.World) null;

    public virtual void Update(float gameTime)
    {
      for (int index = 0; index < this.m_worldList.Count; ++index)
        this.m_worldList[index].Update(gameTime);
    }

    public virtual void Cleanup() => this.Cleanup(false, true);

    public virtual void Cleanup(bool cleanupWorlds, bool deleteWorlds)
    {
      if (cleanupWorlds || deleteWorlds)
      {
        for (int index = 0; index < this.m_worldList.Count; ++index)
          this.m_worldList[index].Cleanup();
      }
      this.m_worldList.Clear();
      this.m_currentWorld = (PSS.GameEngine.World.World) null;
    }

    public int GetNumWorlds() => this.m_worldList.Count;

    public PSS.GameEngine.World.World GetCurrentWorld() => this.m_currentWorld;

    public void SetCurrentWorld(PSS.GameEngine.World.World world) => this.m_currentWorld = world;

    public PSS.GameEngine.World.World GetWorldAtIndex(int index) => this.m_worldList[index];

    public void AddWorld(PSS.GameEngine.World.World world) => this.AddWorld(world, false);

    public void AddWorld(PSS.GameEngine.World.World world, bool initializeWhenAdded)
    {
      this.m_worldList.Add(world);
      if (!initializeWhenAdded)
        return;
      world.Initialize();
    }

    private void RemoveWorld(PSS.GameEngine.World.World world) => this.RemoveWorld(world, false, true);

    private void RemoveWorld(PSS.GameEngine.World.World world, bool cleanupWhenRemoved, bool deleteWhenRemoved)
    {
      if (cleanupWhenRemoved || deleteWhenRemoved)
        world.Cleanup();
      this.m_worldList.Remove(world);
      if (!object.ReferenceEquals((object) world, (object) this.m_currentWorld))
        return;
      this.m_currentWorld = (PSS.GameEngine.World.World) null;
    }
  }
}
