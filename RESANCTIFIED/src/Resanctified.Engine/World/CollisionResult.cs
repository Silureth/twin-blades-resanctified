// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.World.CollisionResult
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.World.Entities;
using System;

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

    public static bool operator ==(CollisionResult a, CollisionResult b) => (double) a.tmin == (double) b.tmin;

    public static bool operator !=(CollisionResult a, CollisionResult b) => (double) a.tmin != (double) b.tmin;

    public static bool operator <(CollisionResult a, CollisionResult b) => (double) a.tmin < (double) b.tmin;

    public static bool operator <=(CollisionResult a, CollisionResult b) => (double) a.tmin <= (double) b.tmin;

    public static bool operator >(CollisionResult a, CollisionResult b) => (double) a.tmin > (double) b.tmin;

    public static bool operator >=(CollisionResult a, CollisionResult b) => (double) a.tmin >= (double) b.tmin;

    public override bool Equals(object o)
    {
      try
      {
        return this == (CollisionResult) o;
      }
      catch
      {
        return false;
      }
    }

    public override int GetHashCode() => base.GetHashCode();
  }
}
