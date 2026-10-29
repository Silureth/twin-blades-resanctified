// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Resources.PSSFileReader
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework.Content;

namespace PSS.GameEngine.Resources
{
  public sealed class PSSFileReader : ContentTypeReader<PSSFile>
  {
    public static int MakeVersion(ushort major, ushort minor) => ((int) major << 16) + (int) minor;

    protected override PSSFile Read(ContentReader input, PSSFile existingInstance)
    {
      if (existingInstance != null)
        return existingInstance;
      int num = input.ReadInt32();
      PSSFile pssFile = new PSSFile();
      switch (new string(input.ReadChars(8)))
      {
        case "PSSANIM ":
          pssFile.m_fileType = PSSFile.FileType.Anim;
          break;
        case "PSSLEVEL":
          pssFile.m_fileType = PSSFile.FileType.Level;
          break;
        case "PSSPLINE":
          pssFile.m_fileType = PSSFile.FileType.Spline;
          break;
      }
      ushort major = input.ReadUInt16();
      ushort minor = input.ReadUInt16();
      pssFile.m_fileVersion = PSSFileReader.MakeVersion(major, minor);
      pssFile.m_fileData = input.ReadBytes(num - 12);
      input.Close();
      return pssFile;
    }
  }
}
