// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Helpers.Maths.Range
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System;

namespace PSS.GameEngine.Helpers.Maths
{
  public struct Range
  {
    public float Min;
    public float Max;

    public Range(float min, float max)
    {
      this.Min = min;
      this.Max = max;
    }

    public float? SmallestOverlap(Range other)
    {
      float val1 = this.Max - other.Min;
      if ((double) val1 < 0.0)
        return new float?();
      float val2 = this.Min - other.Max;
      return (double) val2 < 0.0 ? new float?() : new float?(Math.Min(val1, val2));
    }
  }
}
