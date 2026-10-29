using Resanctified.Engine.Resources;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;
using Resanctified.Game.World;

namespace Resanctified.Game.Resources
{
    public class GameLevelLoader : LevelLoader
    {
        public override Entity CreateEntity(
          ref LevelNode node,
          Entity parent,
          EntityFactory factory)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            if (node.name.Equals("world_min"))
            {
                currentWorld.WorldMin = node.position;
                return null;
            }
            if (node.name.Equals("world_max"))
            {
                currentWorld.WorldMax = node.position;
                return null;
            }
            if (!node.name.Equals("level_end"))
                return base.CreateEntity(ref node, parent, factory);
            currentWorld.EndOfLevelPosX = node.position.X;
            return null;
        }
    }
}
