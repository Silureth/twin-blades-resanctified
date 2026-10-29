// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Graphics.QuadFactory
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PSS.GameEngine.Graphics
{
  public static class QuadFactory
  {
    public static void CreateQuad(
      VertexPositionColor[] vb,
      float left,
      float top,
      float width,
      float height,
      Color color)
    {
      vb[0].Position.X = left;
      vb[0].Position.Y = top;
      vb[0].Position.Z = 0.0f;
      vb[0].Color = color;
      vb[1].Position.X = left + width;
      vb[1].Position.Y = top;
      vb[1].Position.Z = 0.0f;
      vb[1].Color = color;
      vb[2].Position.X = left;
      vb[2].Position.Y = top - height;
      vb[2].Position.Z = 0.0f;
      vb[2].Color = color;
      vb[3].Position.X = left + width;
      vb[3].Position.Y = top - height;
      vb[3].Position.Z = 0.0f;
      vb[3].Color = color;
    }

    public static void CreateQuad(
      VertexPositionColor[] vb,
      float width,
      float height,
      Color color,
      Anchor anchor)
    {
      float left = 0.0f;
      if ((anchor & Anchor.HCenter) != Anchor.Left)
        left = (float) (-(double) width * 0.5);
      else if ((anchor & Anchor.Right) != Anchor.Left)
        left = -width;
      float top = 0.0f;
      if ((anchor & Anchor.VCenter) != Anchor.Left)
        top = height * 0.5f;
      else if ((anchor & Anchor.Bottom) != Anchor.Left)
        top = height;
      QuadFactory.CreateQuad(vb, left, top, width, height, color);
    }

    public static void CreateQuad(
      VertexPositionColorTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      Color color,
      float u0,
      float v0,
      float u1,
      float v1)
    {
      vb[0].Position.X = left;
      vb[0].Position.Y = top;
      vb[0].Position.Z = 0.0f;
      vb[0].Color = color;
      vb[0].TextureCoordinate.X = u0;
      vb[0].TextureCoordinate.Y = v0;
      vb[1].Position.X = left + width;
      vb[1].Position.Y = top;
      vb[1].Position.Z = 0.0f;
      vb[1].Color = color;
      vb[1].TextureCoordinate.X = u1;
      vb[1].TextureCoordinate.Y = v0;
      vb[2].Position.X = left;
      vb[2].Position.Y = top - height;
      vb[2].Position.Z = 0.0f;
      vb[2].Color = color;
      vb[2].TextureCoordinate.X = u0;
      vb[2].TextureCoordinate.Y = v1;
      vb[3].Position.X = left + width;
      vb[3].Position.Y = top - height;
      vb[3].Position.Z = 0.0f;
      vb[3].Color = color;
      vb[3].TextureCoordinate.X = u1;
      vb[3].TextureCoordinate.Y = v1;
    }

    public static void CreateQuad(
      VertexPositionColorTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      Color color,
      float textureWidth,
      float textureHeight)
    {
      float u0 = left / textureWidth;
      float v0 = top / textureHeight;
      float u1 = (left + width) / textureWidth;
      float v1 = (top + height) / textureHeight;
      QuadFactory.CreateQuad(vb, left, top, width, height, color, u0, v0, u1, v1);
    }

    public static void CreateQuad(
      VertexPositionTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      float u0,
      float v0,
      float u1,
      float v1)
    {
      vb[0].Position.X = left;
      vb[0].Position.Y = top;
      vb[0].Position.Z = 0.0f;
      vb[0].TextureCoordinate.X = u0;
      vb[0].TextureCoordinate.Y = v0;
      vb[1].Position.X = left + width;
      vb[1].Position.Y = top;
      vb[1].Position.Z = 0.0f;
      vb[1].TextureCoordinate.X = u1;
      vb[1].TextureCoordinate.Y = v0;
      vb[2].Position.X = left;
      vb[2].Position.Y = top - height;
      vb[2].Position.Z = 0.0f;
      vb[2].TextureCoordinate.X = u0;
      vb[2].TextureCoordinate.Y = v1;
      vb[3].Position.X = left + width;
      vb[3].Position.Y = top - height;
      vb[3].Position.Z = 0.0f;
      vb[3].TextureCoordinate.X = u1;
      vb[3].TextureCoordinate.Y = v1;
    }

    public static void CreateQuad(
      VertexPositionTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      float textureWidth,
      float textureHeight)
    {
      float u0 = left / textureWidth;
      float v0 = top / textureHeight;
      float u1 = (left + width) / textureWidth;
      float v1 = (top + height) / textureHeight;
      QuadFactory.CreateQuad(vb, left, top, width, height, u0, v0, u1, v1);
    }

    public static void CreateCenteredQuad(
      VertexPositionTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      float textureWidth,
      float textureHeight)
    {
      float u0 = left / textureWidth;
      float v0 = top / textureHeight;
      float u1 = (left + width) / textureWidth;
      float v1 = (top + height) / textureHeight;
      QuadFactory.CreateQuad(vb, (float) (-(double) width * 0.5), height * 0.5f, width, height, u0, v0, u1, v1);
    }

    public static void CreateCenteredScaledQuad(
      VertexPositionTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      float textureWidth,
      float textureHeight,
      float scale)
    {
      float u0 = left / textureWidth;
      float v0 = top / textureHeight;
      float u1 = (left + width) / textureWidth;
      float v1 = (top + height) / textureHeight;
      QuadFactory.CreateQuad(vb, (float) (-(double) width * 0.5), height * 0.5f, width, height, u0, v0, u1, v1);
      vb[0].Position *= scale;
      vb[1].Position *= scale;
      vb[2].Position *= scale;
      vb[3].Position *= scale;
    }

    public static void CreateAnchoredQuad(
      VertexPositionTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      float textureWidth,
      float textureHeight,
      Anchor anchor)
    {
      float u0 = left / textureWidth;
      float v0 = top / textureHeight;
      float u1 = (left + width) / textureWidth;
      float v1 = (top + height) / textureHeight;
      float left1 = 0.0f;
      if ((anchor & Anchor.HCenter) != Anchor.Left)
        left1 = (float) (-(double) width * 0.5);
      else if ((anchor & Anchor.Right) != Anchor.Left)
        left1 = -width;
      float top1 = 0.0f;
      if ((anchor & Anchor.VCenter) != Anchor.Left)
        top1 = height * 0.5f;
      else if ((anchor & Anchor.Bottom) != Anchor.Left)
        top1 = height;
      QuadFactory.CreateQuad(vb, left1, top1, width, height, u0, v0, u1, v1);
    }

    public static void CreateAnchoredQuad(
      VertexPositionColorTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      float textureWidth,
      float textureHeight,
      Anchor anchor)
    {
      float u0 = left / textureWidth;
      float v0 = top / textureHeight;
      float u1 = (left + width) / textureWidth;
      float v1 = (top + height) / textureHeight;
      float left1 = 0.0f;
      if ((anchor & Anchor.HCenter) != Anchor.Left)
        left1 = (float) (-(double) width * 0.5);
      else if ((anchor & Anchor.Right) != Anchor.Left)
        left1 = -width;
      float top1 = 0.0f;
      if ((anchor & Anchor.VCenter) != Anchor.Left)
        top1 = height * 0.5f;
      else if ((anchor & Anchor.Bottom) != Anchor.Left)
        top1 = height;
      QuadFactory.CreateQuad(vb, left1, top1, width, height, Color.White, u0, v0, u1, v1);
    }

    public static void CreateCenteredQuad(
      VertexPositionColorTexture[] vb,
      float left,
      float top,
      float width,
      float height,
      float textureWidth,
      float textureHeight)
    {
      float u0 = left / textureWidth;
      float v0 = top / textureHeight;
      float u1 = (left + width) / textureWidth;
      float v1 = (top + height) / textureHeight;
      QuadFactory.CreateQuad(vb, (float) (-(double) width * 0.5), height * 0.5f, width, height, Color.White, u0, v0, u1, v1);
    }
  }
}
