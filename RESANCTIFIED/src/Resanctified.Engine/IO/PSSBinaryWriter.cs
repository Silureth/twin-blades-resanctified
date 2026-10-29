// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.IO.PSSBinaryWriter
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.Maths;
using System.IO;
using System.Text;

namespace Resanctified.Engine.IO
{
  public sealed class PSSBinaryWriter : BinaryWriter
  {
    public PSSBinaryWriter(Stream output)
      : base(output)
    {
    }

    public PSSBinaryWriter(Stream output, Encoding encoding)
      : base(output, encoding)
    {
    }

    public void WriteCString(string str)
    {
      for (int index = 0; index < str.Length; ++index)
        this.Write(str[index]);
      this.Write(char.MinValue);
    }

    public void WriteLPString(string str)
    {
      this.Write((ushort) str.Length);
      for (int index = 0; index < str.Length; ++index)
        this.Write(str[index]);
    }

    public void Write(Vector2 v)
    {
      this.Write(v.X);
      this.Write(v.Y);
    }

    public void Write(Vector3 v)
    {
      this.Write(v.X);
      this.Write(v.Y);
      this.Write(v.Z);
    }

    public void Write(Vector4 v)
    {
      this.Write(v.X);
      this.Write(v.Y);
      this.Write(v.Z);
      this.Write(v.W);
    }

    public void Write(Color c)
    {
      this.Write(c.R);
      this.Write(c.G);
      this.Write(c.B);
      this.Write(c.A);
    }

    public void Write(Matrix m)
    {
      this.Write(m.M11);
      this.Write(m.M12);
      this.Write(m.M13);
      this.Write(m.M14);
      this.Write(m.M21);
      this.Write(m.M22);
      this.Write(m.M23);
      this.Write(m.M24);
      this.Write(m.M31);
      this.Write(m.M32);
      this.Write(m.M33);
      this.Write(m.M34);
      this.Write(m.M41);
      this.Write(m.M42);
      this.Write(m.M43);
      this.Write(m.M44);
    }

    public void Write(AABB aabb)
    {
      this.Write(aabb.boundMin);
      this.Write(aabb.boundMax);
    }
  }
}
