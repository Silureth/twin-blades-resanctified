using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.Maths;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.World
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
                Array.Sort(results, 0, length);
            return length;
        }

        public static int CollideEntities(
          Maths.Ray ray,
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
                Array.Sort(results, 0, length);
            return length;
        }
    }
}
