// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Graphics.VertexFormatHelper
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PSS.GameEngine.Graphics
{
  public static class VertexFormatHelper
  {
    public static int GetVertexSize(VertexFormat format)
    {
      switch (format)
      {
        case VertexFormat.Position3Color:
          return VertexPositionColor.VertexDeclaration.VertexStride;
        case VertexFormat.Position3ColorTexture:
          return VertexPositionColorTexture.VertexDeclaration.VertexStride;
        case VertexFormat.Position3Texture:
          return VertexPositionTexture.VertexDeclaration.VertexStride;
        default:
          return 0;
      }
    }

    public static VertexElement[] GetVertexElements(VertexFormat format)
    {
      switch (format)
      {
        case VertexFormat.Position3Color:
          return VertexPositionColor.VertexDeclaration.GetVertexElements();
        case VertexFormat.Position3ColorTexture:
          return VertexPositionColorTexture.VertexDeclaration.GetVertexElements();
        case VertexFormat.Position3Texture:
          return VertexPositionTexture.VertexDeclaration.GetVertexElements();
        default:
          return (VertexElement[]) null;
      }
    }

    public static Vector3 GetVertexPosition<T>(T vertex, VertexFormat format) where T : IVertexType
    {
      switch (format)
      {
        case VertexFormat.Position3Color:
          VertexPositionColor? nullable1 = (object) vertex as VertexPositionColor?;
          if (nullable1.HasValue)
            return nullable1.Value.Position;
          break;
        case VertexFormat.Position3ColorTexture:
          VertexPositionColorTexture? nullable2 = (object) vertex as VertexPositionColorTexture?;
          if (nullable2.HasValue)
            return nullable2.Value.Position;
          break;
        case VertexFormat.Position3Texture:
          VertexPositionTexture? nullable3 = (object) vertex as VertexPositionTexture?;
          if (nullable3.HasValue)
            return nullable3.Value.Position;
          break;
      }
      return Vector3.Zero;
    }
  }
}
