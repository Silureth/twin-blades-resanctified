using System.IO;

namespace Resanctified.Engine.Resources
{
    public sealed class PSSFile
    {
        public FileType m_fileType;
        public int m_fileVersion;
        public byte[] m_fileData;

        public PSSBinaryReader GetFileDataReader() => new PSSBinaryReader(new MemoryStream(this.m_fileData));

        public enum FileType
        {
            Anim,
            Level,
            Spline,
        }
    }
}
