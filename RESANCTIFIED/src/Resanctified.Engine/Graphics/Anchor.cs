// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Graphics.Anchor
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

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
