// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Resources.PSSFile
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using System.IO;

namespace Resanctified.Engine.Resources
{
  public sealed class PSSFile
  {
    public PSSFile.FileType m_fileType;
    public int m_fileVersion;
    public byte[] m_fileData;

    public PSSBinaryReader GetFileDataReader() => new PSSBinaryReader((Stream) new MemoryStream(this.m_fileData));

    public enum FileType
    {
      Anim,
      Level,
      Spline,
    }
  }
}
