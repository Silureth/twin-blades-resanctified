// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Helpers.Maths.Segment
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using System;

namespace Resanctified.Engine.Helpers.Maths
{
  public struct Segment
  {
    private const float SMALL_NUM = 1E-08f;
    public Vector2 P0;
    public Vector2 P1;
    public static readonly Segment Zero = new Segment(0.0f, 0.0f, 0.0f, 0.0f);

    public Segment(Vector2 p0, Vector2 p1)
    {
      this.P0 = p0;
      this.P1 = p1;
    }

    public Segment(float x0, float y0, float x1, float y1)
    {
      this.P0 = new Vector2(x0, y0);
      this.P1 = new Vector2(x1, y1);
    }

    public static float Distance(Segment s1, Segment s2)
    {
      float result;
      Segment.Distance(ref s1, ref s2, out result);
      return result;
    }

    public static void Distance(ref Segment s1, ref Segment s2, out float result)
    {
      float result1;
      Segment.DistanceSquared(ref s1, ref s2, out result1);
      result = (float) Math.Sqrt((double) result1);
    }

    public static float DistanceSquared(Segment s1, Segment s2)
    {
      float result;
      Segment.DistanceSquared(ref s1, ref s2, out result);
      return result;
    }

    public static void DistanceSquared(ref Segment s1, ref Segment s2, out float result)
    {
      Vector2 vector2_1 = s1.P1 - s1.P0;
      Vector2 vector2_2 = s2.P1 - s2.P0;
      Vector2 vector2_3 = s1.P0 - s2.P0;
      float result1;
      Vector2.Dot(ref vector2_1, ref vector2_1, out result1);
      float result2;
      Vector2.Dot(ref vector2_1, ref vector2_2, out result2);
      float result3;
      Vector2.Dot(ref vector2_2, ref vector2_2, out result3);
      float result4;
      Vector2.Dot(ref vector2_1, ref vector2_3, out result4);
      float result5;
      Vector2.Dot(ref vector2_2, ref vector2_3, out result5);
      float num1 = (float) ((double) result1 * (double) result3 - (double) result2 * (double) result2);
      float num2 = num1;
      float num3 = num1;
      float num4;
      float num5;
      if ((double) num1 < 9.99999993922529E-09)
      {
        num4 = 0.0f;
        num2 = 1f;
        num5 = result5;
        num3 = result3;
      }
      else
      {
        num4 = (float) ((double) result2 * (double) result5 - (double) result3 * (double) result4);
        num5 = (float) ((double) result1 * (double) result5 - (double) result2 * (double) result4);
        if ((double) num4 < 0.0)
        {
          num4 = 0.0f;
          num5 = result5;
          num3 = result3;
        }
        else if ((double) num4 > (double) num2)
        {
          num4 = num2;
          num5 = result5 + result2;
          num3 = result3;
        }
      }
      if ((double) num5 < 0.0)
      {
        num5 = 0.0f;
        if (-(double) result4 < 0.0)
          num4 = 0.0f;
        else if (-(double) result4 > (double) result1)
        {
          num4 = num2;
        }
        else
        {
          num4 = -result4;
          num2 = result1;
        }
      }
      else if ((double) num5 > (double) num3)
      {
        num5 = num3;
        if (-(double) result4 + (double) result2 < 0.0)
          num4 = 0.0f;
        else if (-(double) result4 + (double) result2 > (double) result1)
        {
          num4 = num2;
        }
        else
        {
          num4 = -result4 + result2;
          num2 = result1;
        }
      }
      float num6 = (double) Math.Abs(num4) < 9.99999993922529E-09 ? 0.0f : num4 / num2;
      float num7 = (double) Math.Abs(num5) < 9.99999993922529E-09 ? 0.0f : num5 / num3;
      Vector2 vector2_4 = vector2_3 + num6 * vector2_1 - num7 * vector2_2;
      result = vector2_4.LengthSquared();
    }

    public float Length() => (this.P1 - this.P0).Length();

    public float LengthSquared() => (this.P1 - this.P0).LengthSquared();

    public static Segment Transform(Segment segment, Matrix matrix)
    {
      Segment result;
      Segment.Transform(ref segment, ref matrix, out result);
      return result;
    }

    public static void Transform(ref Segment segment, ref Matrix matrix, out Segment result)
    {
      Vector2.Transform(ref segment.P0, ref matrix, out result.P0);
      Vector2.Transform(ref segment.P1, ref matrix, out result.P1);
    }

    public Vector2 Normal()
    {
      Vector2 vector2 = new Vector2(this.P0.Y - this.P1.Y, this.P1.X - this.P0.X);
      vector2.Normalize();
      return vector2;
    }

    public BoundingBox ComputeBoundingBox()
    {
      BoundingBox boundingBox = new BoundingBox();
      if ((double) this.P0.X < (double) this.P1.X)
      {
        boundingBox.Min.X = this.P0.X;
        boundingBox.Max.X = this.P1.X;
      }
      else
      {
        boundingBox.Min.X = this.P1.X;
        boundingBox.Max.X = this.P0.X;
      }
      if ((double) this.P0.Y < (double) this.P1.Y)
      {
        boundingBox.Min.Y = this.P0.Y;
        boundingBox.Max.Y = this.P1.Y;
      }
      else
      {
        boundingBox.Min.Y = this.P1.Y;
        boundingBox.Max.Y = this.P0.Y;
      }
      boundingBox.Min.Z = 0.0f;
      boundingBox.Max.Z = 0.0f;
      return boundingBox;
    }
  }
}
