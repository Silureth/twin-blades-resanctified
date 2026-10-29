using System;

namespace Resanctified.Engine.Graphics
{
    [Flags]
    public enum Anchor
    {
        Left = 0,
        Top = 0,
        Right = 1,
        Bottom = 2,
        HCenter = 4,
        VCenter = 8,
        Baseline = Bottom, // 0x00000002
    }
}
