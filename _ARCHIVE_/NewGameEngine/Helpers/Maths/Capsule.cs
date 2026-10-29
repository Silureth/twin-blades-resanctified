// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Helpers.Maths.Capsule
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;

namespace PSS.GameEngine.Helpers.Maths
{
  public struct Capsule
  {
    public Segment Segment;
    public float Radius;

    public Capsule(Vector2 p0, Vector2 p1, float radius)
    {
      this.Segment.P0 = p0;
      this.Segment.P1 = p1;
      this.Radius = radius;
    }

    public Capsule(Segment segment, float radius)
    {
      this.Segment = segment;
      this.Radius = radius;
    }
  }
}
