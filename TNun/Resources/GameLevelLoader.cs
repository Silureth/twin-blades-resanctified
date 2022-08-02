// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.Resources.GameLevelLoader
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using PSS.Game.NunVsZombies.World;
using PSS.GameEngine.Resources;
using PSS.GameEngine.World;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.Resources
{
  public class GameLevelLoader : LevelLoader
  {
    public override Entity CreateEntity(
      ref LevelNode node,
      Entity parent,
      EntityFactory factory)
    {
      GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
      if (node.name.Equals("world_min"))
      {
        currentWorld.WorldMin = node.position;
        return (Entity) null;
      }
      if (node.name.Equals("world_max"))
      {
        currentWorld.WorldMax = node.position;
        return (Entity) null;
      }
      if (!node.name.Equals("level_end"))
        return base.CreateEntity(ref node, parent, factory);
      currentWorld.EndOfLevelPosX = node.position.X;
      return (Entity) null;
    }
  }
}
