// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Resources.PSSBinaryReader
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using System.IO;
using System.Text;

namespace PSS.GameEngine.Resources
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

    public Vector2 ReadVector2() => new Vector2(this.ReadSingle(), this.ReadSingle());

    public Color ReadColor() => Color.White with
    {
      PackedValue = this.ReadUInt32()
    };
  }
}
