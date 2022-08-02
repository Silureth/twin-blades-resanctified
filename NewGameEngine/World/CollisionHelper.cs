// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.CollisionHelper
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using PSS.GameEngine.Maths;
using PSS.GameEngine.World.Entities;
using System;

namespace PSS.GameEngine.World
{
  public class CollisionHelper
  {
    public static int CollideEntities(
      AABB box,
      Entity[] entities,
      int numEntities,
      ref CollisionResult[] results,
      int maxResults,
      bool sortAscendingMinDist)
    {
      int length = 0;
      for (int index1 = 0; index1 < numEntities && length < maxResults; ++index1)
      {
        for (int index2 = 0; index2 < entities[index1].GetCollisionBoxCount() && length < maxResults; ++index2)
        {
          CollisionBox[] collisionBoxes = entities[index1].GetCollisionBoxes();
          Vector2 normal = new Vector2();
          float tmin = 0.0f;
          if (MathHelperEx.BoxIntersectsBox2D(box, collisionBoxes[index2].box, ref normal, ref tmin))
          {
            Vector2 vector2 = new Vector2(collisionBoxes[index2].box.center.X, collisionBoxes[index2].box.center.Y);
            results[length].entity = entities[index1];
            results[length].tag = collisionBoxes[index2].tag;
            results[length].intersection = vector2 + normal * tmin;
            results[length].normal = normal;
            results[length].tmin = tmin;
            ++length;
          }
        }
      }
      if (sortAscendingMinDist && length > 0)
        Array.Sort<CollisionResult>(results, 0, length);
      return length;
    }

    public static int CollideEntities(
      PSS.GameEngine.Maths.Ray ray,
      float t0,
      float t1,
      Entity[] entities,
      int numEntities,
      ref CollisionResult[] results,
      int maxResults,
      bool sortAscendingMinDist)
    {
      int length = 0;
      for (int index1 = 0; index1 < numEntities && length < maxResults; ++index1)
      {
        for (int index2 = 0; index2 < entities[index1].GetCollisionBoxCount() && length < maxResults; ++index2)
        {
          CollisionBox[] collisionBoxes = entities[index1].GetCollisionBoxes();
          float tmin = 0.0f;
          if (MathHelperEx.RayIntersectsBox(ray, collisionBoxes[index2].box, t0, t1, ref tmin))
          {
            Vector2 vector2_1 = new Vector2(ray.origin.X, ray.origin.Y);
            Vector2 vector2_2 = new Vector2(ray.direction.X, ray.direction.Y);
            results[length].entity = entities[index1];
            results[length].tag = collisionBoxes[index2].tag;
            results[length].intersection = vector2_1 + tmin * vector2_2;
            results[length].normal.X = -ray.direction.X;
            results[length].normal.Y = -ray.direction.Y;
            results[length].tmin = tmin;
            ++length;
          }
        }
      }
      if (sortAscendingMinDist && length > 0)
        Array.Sort<CollisionResult>(results, 0, length);
      return length;
    }
  }
}
