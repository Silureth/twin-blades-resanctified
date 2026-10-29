// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.IO.PSSBinaryReader
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.Maths;
using System.IO;
using System.Text;

namespace Resanctified.Engine.IO
{
  public sealed class PSSBinaryReader : BinaryReader
  {
    public PSSBinaryReader(Stream s)
      : base(s)
    {
    }

    public PSSBinaryReader(Stream s, Encoding e)
      : base(s, e)
    {
    }

    public string ReadCString()
    {
      StringBuilder stringBuilder = new StringBuilder(256);
      for (char ch = this.ReadChar(); ch != char.MinValue; ch = this.ReadChar())
        stringBuilder.Append(ch);
      return stringBuilder.ToString();
    }

    public string ReadLPString()
    {
      int count = (int) this.ReadUInt16();
      return count == 0 ? string.Empty : new string(this.ReadChars(count));
    }

    public Vector3 ReadVector3() => new Vector3(this.ReadSingle(), this.ReadSingle(), this.ReadSingle());

    public Vector4 ReadVector4() => new Vector4(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());

    public Vector2 ReadVector2() => new Vector2(this.ReadSingle(), this.ReadSingle());

    public Color ReadColor() => Color.White with
    {
      R = this.ReadByte(),
      G = this.ReadByte(),
      B = this.ReadByte(),
      A = this.ReadByte()
    };

    public Matrix ReadMatrix() => new Matrix(this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle(), this.ReadSingle());

    public AABB ReadAABB()
    {
      Vector3 min = this.ReadVector3();
      Vector3 max = this.ReadVector3();
      AABB aabb = new AABB();
      aabb.InitializeFromBounds(min, max);
      return aabb;
    }
  }
}
