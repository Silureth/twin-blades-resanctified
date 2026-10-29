using System;
using Microsoft.Xna.Framework;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.World
{
    public struct CollisionResult : IComparable<CollisionResult>
    {
        public Entity entity;
        public int tag;
        public Vector2 intersection;
        public Vector2 normal;
        public float tmin;

        public int CompareTo(CollisionResult other)
        {
            if (this < other)
                return -1;
            return this > other ? 1 : 0;
        }

        public static bool operator ==(CollisionResult a, CollisionResult b) => a.tmin == (double)b.tmin;

        public static bool operator !=(CollisionResult a, CollisionResult b) => a.tmin != (double)b.tmin;

        public static bool operator <(CollisionResult a, CollisionResult b) => a.tmin < (double)b.tmin;

        public static bool operator <=(CollisionResult a, CollisionResult b) => a.tmin <= (double)b.tmin;

        public static bool operator >(CollisionResult a, CollisionResult b) => a.tmin > (double)b.tmin;

        public static bool operator >=(CollisionResult a, CollisionResult b) => a.tmin >= (double)b.tmin;

        public override bool Equals(object o)
        {
            try
            {
                return this == (CollisionResult)o;
            }
            catch
            {
                return false;
            }
        }

        public override int GetHashCode() => base.GetHashCode();
    }
}
