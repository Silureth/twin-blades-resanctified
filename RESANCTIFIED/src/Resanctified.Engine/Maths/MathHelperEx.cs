// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Maths.MathHelperEx
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using System;

namespace Resanctified.Engine.Maths
{
  public static class MathHelperEx
  {
    private static Random s_random = new Random();

    public static void SetRandomSeed(int seed) => MathHelperEx.s_random = new Random(seed);

    public static float GetRandomFloat() => (float) MathHelperEx.s_random.NextDouble();

    public static float GetRandomFloat(float min, float max)
    {
      if ((double) min > (double) max)
      {
        float num = min;
        min = max;
        max = num;
      }
      return min + (float) MathHelperEx.s_random.NextDouble() * (max - min);
    }

    public static double GetRandomDouble() => MathHelperEx.s_random.NextDouble();

    public static double GetRandomDouble(double min, double max)
    {
      if (min > max)
      {
        double num = min;
        min = max;
        max = num;
      }
      return min + MathHelperEx.s_random.NextDouble() * (max - min);
    }

    public static int GetRandomInt() => MathHelperEx.s_random.Next();

    public static int GetRandomInt(int min, int max)
    {
      if (min > max)
      {
        int num = min;
        min = max;
        max = num;
      }
      return MathHelperEx.s_random.Next(min, max);
    }

    public static Vector3 GetClosestPoint(
      Vector3 A,
      Vector3 B,
      Vector3 P,
      ref float t,
      bool segmentClamp)
    {
      Vector3 vector3_1 = P - A;
      Vector3 vector3_2 = B - A;
      float num1 = (float) ((double) vector3_2.X * (double) vector3_2.X + (double) vector3_2.Y * (double) vector3_2.Y);
      float num2 = (float) ((double) vector3_1.X * (double) vector3_2.X + (double) vector3_1.Y * (double) vector3_2.Y);
      t = num2 / num1;
      if (segmentClamp)
      {
        if ((double) t < 0.0)
          t = 0.0f;
        else if ((double) t > 1.0)
          t = 1f;
      }
      Vector3 vector3_3 = new Vector3();
      return A + vector3_2 * t;
    }

    public static void TransformBox(out AABB transformedBox, AABB localBox, Matrix transform)
    {
      Vector3.Transform(ref localBox.center, ref transform, out transformedBox.center);
      Matrix matrix = new Matrix(Math.Abs(transform.M11), Math.Abs(transform.M12), Math.Abs(transform.M13), 0.0f, Math.Abs(transform.M21), Math.Abs(transform.M22), Math.Abs(transform.M23), 0.0f, Math.Abs(transform.M31), Math.Abs(transform.M32), Math.Abs(transform.M33), 0.0f, 0.0f, 0.0f, 0.0f, 1f);
      Vector3.TransformNormal(ref localBox.extents, ref matrix, out transformedBox.extents);
      transformedBox.boundMin = transformedBox.center - transformedBox.extents;
      transformedBox.boundMax = transformedBox.center + transformedBox.extents;
    }

    public static bool BoxIntersectsBox3D(AABB a, AABB b)
    {
      Vector3 vector3 = b.center - a.center;
      return (double) Math.Abs(vector3.X) <= (double) a.extents.X + (double) b.extents.X && (double) Math.Abs(vector3.Y) <= (double) a.extents.Y + (double) b.extents.Y && (double) Math.Abs(vector3.Z) <= (double) a.extents.Z + (double) b.extents.Z;
    }

    public static bool BoxIntersectsBox2D(AABB a, AABB b)
    {
      Vector2 vector2;
      vector2.X = b.center.X - a.center.X;
      vector2.Y = b.center.Y - a.center.Y;
      return (double) Math.Abs(vector2.X) <= (double) a.extents.X + (double) b.extents.X && (double) Math.Abs(vector2.Y) <= (double) a.extents.Y + (double) b.extents.Y;
    }

    public static bool PointInBox(Vector2 p, AABB box)
    {
      Vector2 vector2;
      vector2.X = p.X - box.center.X;
      vector2.Y = p.Y - box.center.Y;
      return (double) Math.Abs(vector2.X) <= (double) box.extents.X && (double) Math.Abs(vector2.Y) <= (double) box.extents.Y;
    }

    public static bool PointInBox(Vector3 p, AABB box)
    {
      Vector3 vector3 = box.center - p;
      return (double) Math.Abs(vector3.X) <= (double) box.extents.X && (double) Math.Abs(vector3.Y) <= (double) box.extents.Y && (double) Math.Abs(vector3.Z) <= (double) box.extents.Z;
    }

    public static bool BoxIntersectsBox3D(AABB a, AABB b, ref Vector3 normal, ref float tmin)
    {
      Vector3 vector3 = b.center - a.center;
      float num1 = Math.Abs(vector3.X);
      if ((double) num1 > (double) a.extents.X + (double) b.extents.X)
        return false;
      float num2 = Math.Abs(vector3.Y);
      if ((double) num2 > (double) a.extents.Y + (double) b.extents.Y)
        return false;
      if ((double) num1 < (double) num2)
      {
        tmin = num1;
        normal.X = (double) vector3.X < 0.0 ? -1f : 1f;
        normal.Y = 0.0f;
        normal.Z = 0.0f;
      }
      else
      {
        tmin = num2;
        normal.X = 0.0f;
        normal.Y = (double) vector3.Y < 0.0 ? -1f : 1f;
        normal.Z = 0.0f;
      }
      float num3 = Math.Abs(vector3.Z);
      if ((double) num3 > (double) a.extents.Z + (double) b.extents.Z)
        return false;
      if ((double) num3 < (double) tmin)
      {
        tmin = num3;
        normal.X = 0.0f;
        normal.Y = 0.0f;
        normal.Z = (double) vector3.Z < 0.0 ? -1f : 1f;
      }
      return true;
    }

    public static bool BoxIntersectsBox2D(AABB a, AABB b, ref Vector2 normal, ref float tmin)
    {
      Vector2 vector2;
      vector2.X = b.center.X - a.center.X;
      vector2.Y = b.center.Y - a.center.Y;
      float num1 = Math.Abs(vector2.X);
      if ((double) num1 > (double) a.extents.X + (double) b.extents.X)
        return false;
      float num2 = Math.Abs(vector2.Y);
      if ((double) num2 > (double) a.extents.Y + (double) b.extents.Y)
        return false;
      if ((double) num1 < (double) num2)
      {
        tmin = num1;
        normal.X = (double) vector2.X < 0.0 ? -1f : 1f;
        normal.Y = 0.0f;
      }
      else
      {
        tmin = num2;
        normal.X = 0.0f;
        normal.Y = (double) vector2.Y < 0.0 ? -1f : 1f;
      }
      return true;
    }

    public static bool RayIntersectsBox(Ray r, AABB box, float t0, float t1, ref float tmin)
    {
      tmin = (box[r[0]].X - r.origin.X) * r.inv_direction.X;
      float num1 = (box[1 - r[0]].X - r.origin.X) * r.inv_direction.X;
      float num2 = (box[r[1]].Y - r.origin.Y) * r.inv_direction.Y;
      float num3 = (box[1 - r[1]].Y - r.origin.Y) * r.inv_direction.Y;
      if ((double) tmin > (double) num3 || (double) num2 > (double) num1)
        return false;
      if ((double) num2 > (double) tmin)
        tmin = num2;
      if ((double) num3 < (double) num1)
        num1 = num3;
      float num4 = (box[r[2]].Z - r.origin.Z) * r.inv_direction.Z;
      float num5 = (box[1 - r[2]].Z - r.origin.Z) * r.inv_direction.Z;
      if ((double) tmin > (double) num5 || (double) num4 > (double) num1)
        return false;
      if ((double) num4 > (double) tmin)
        tmin = num4;
      if ((double) num5 < (double) num1)
        num1 = num5;
      return (double) tmin < (double) t1 && (double) num1 > (double) t0;
    }

    public static float RadiansToDegrees(float radians) => (float) ((double) radians * 180.0 * (1.0 / Math.PI));

    public static float DegreesToRadians(float degrees) => (float) ((double) degrees * Math.PI * (1.0 / 180.0));
  }
}
