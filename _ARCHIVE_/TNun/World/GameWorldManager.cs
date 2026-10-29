// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.World.GameWorldManager
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.GameEngine.World;

namespace PSS.Game.NunVsZombies.World
{
  public class GameWorldManager : WorldManager
  {
    public static GameWorldManager Instance
    {
      get
      {
        if (WorldManager.m_instance == null)
          WorldManager.m_instance = (WorldManager) new GameWorldManager();
        return (GameWorldManager) WorldManager.m_instance;
      }
    }

    public static GameWorldManager GetInstance()
    {
      if (WorldManager.m_instance == null)
        WorldManager.m_instance = (WorldManager) new GameWorldManager();
      return (GameWorldManager) WorldManager.m_instance;
    }

    public override void Initialize() => base.Initialize();

    public override void Cleanup() => base.Cleanup();

    public override void Cleanup(bool cleanupWorlds, bool deleteWorlds) => base.Cleanup(cleanupWorlds, deleteWorlds);

    public override void Update(float gameTime) => base.Update(gameTime);
  }
}
