// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Maths.Ray
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;

namespace PSS.GameEngine.Maths
{
  public struct Ray
  {
    public Vector3 origin;
    public Vector3 direction;
    public Vector3 inv_direction;
    public int signX;
    public int signY;
    public int signZ;

    public Ray(Vector3 o, Vector3 d)
    {
      this.origin = o;
      this.direction = d;
      this.inv_direction = Vector3.One;
      this.inv_direction /= d;
      this.signX = (double) this.inv_direction.X < 0.0 ? 1 : 0;
      this.signY = (double) this.inv_direction.Y < 0.0 ? 1 : 0;
      this.signZ = (double) this.inv_direction.Z < 0.0 ? 1 : 0;
    }

    public void Initialize(Vector3 o, Vector3 d)
    {
      this.origin = o;
      this.direction = d;
      this.inv_direction.X = 1f / d.X;
      this.inv_direction.Y = 1f / d.Y;
      this.inv_direction.Z = 1f / d.Z;
      this.signX = (double) this.inv_direction.X < 0.0 ? 1 : 0;
      this.signY = (double) this.inv_direction.Y < 0.0 ? 1 : 0;
      this.signZ = (double) this.inv_direction.Z < 0.0 ? 1 : 0;
    }

    public int this[int index]
    {
      get
      {
        if (index == 0)
          return this.signX;
        return index == 1 ? this.signY : this.signZ;
      }
      set
      {
        if (index == 0)
          this.signX = value;
        else if (index == 1)
          this.signY = value;
        else
          this.signZ = value;
      }
    }
  }
}
