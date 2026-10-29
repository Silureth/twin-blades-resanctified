// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Graphics.Renderer.Renderer
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Tools;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;
using PSS.GameEngine.World.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSS.GameEngine.Graphics.Renderer
{
    public sealed class Renderer
    {
        public const float OLD_SCREEN_WIDTH = 480f;
        public const float OLD_SCREEN_HEIGHT = 320f;

        public const float SCREEN_WIDTH_LANDSCAPE = OLD_SCREEN_WIDTH;
        public const float SCREEN_HEIGHT_LANDSCAPE = OLD_SCREEN_HEIGHT;

        public const float SCREEN_WIDTH_PORTRAIT = OLD_SCREEN_HEIGHT;
        public const float SCREEN_HEIGHT_PORTRAIT = OLD_SCREEN_WIDTH;



        private const int TOPLEVELVIEWLIST_CAPACITY = 256;
        private const int RENDERVIEWLIST_CAPACITY = 256;
        private const int DRAWSTRING_MAX_CHARS = 2048;
        private static readonly PSS.GameEngine.Graphics.Renderer.Renderer s_instance = new PSS.GameEngine.Graphics.Renderer.Renderer();
        public Color ClearColor;
        private bool m_stringShadow;
        private VertexPositionTexture[] m_stringVertices;
        private short[] m_stringIndices;
        private StringBuilder m_stringBuilder;
        private static readonly Color STRING_SHADOW_COLOR = new Color(0, 0, 0, 150);
        private GraphicsDevice m_graphics;
        private BasicEffect m_effect;
        private Matrix m_viewMatrix;
        private Matrix m_projectionMatrix;
        private List<View> m_topLevelViewList;
        private List<View> m_renderViewList;

        public static PSS.GameEngine.Graphics.Renderer.Renderer Instance => PSS.GameEngine.Graphics.Renderer.Renderer.s_instance;

        public static PSS.GameEngine.Graphics.Renderer.Renderer GetInstance() => PSS.GameEngine.Graphics.Renderer.Renderer.s_instance;

        public void Initialize()
        {
            this.m_viewMatrix = Matrix.Identity;
            this.m_projectionMatrix = Matrix.Identity;
            this.m_graphics = Application.Instance.GraphicsDevice;
            this.m_graphics.BlendFactor = Color.White;
            this.m_graphics.BlendState = BlendState.Opaque;
            this.m_graphics.DepthStencilState = DepthStencilState.None;
            this.m_graphics.RasterizerState = new RasterizerState()
            {
                CullMode = CullMode.None,
                FillMode = FillMode.Solid,
                MultiSampleAntiAlias = false,
                ScissorTestEnable = false
            };
            this.SetStringShadowEnabled(false);
            this.m_stringVertices = new VertexPositionTexture[8192];
            this.m_stringIndices = new short[12288];
            this.m_stringBuilder = new StringBuilder(2048);
            this.m_effect = new BasicEffect(this.m_graphics);
            this.m_effect.Alpha = 1f;
            this.m_effect.DiffuseColor = Vector3.One;
            this.m_effect.FogEnabled = false;
            this.m_effect.LightingEnabled = false;
            this.m_effect.PreferPerPixelLighting = false;
            this.m_effect.TextureEnabled = false;
            this.m_effect.VertexColorEnabled = false;
            this.ClearTopLevelViewList();
        }

        public void Cleanup()
        {
            if (this.m_effect != null)
            {
                this.m_effect.Dispose();
                this.m_effect = (BasicEffect)null;
            }
            this.m_graphics = (GraphicsDevice)null;
            this.m_stringVertices = (VertexPositionTexture[])null;
            this.m_stringIndices = (short[])null;
        }

        public void Draw(bool clearColor, AABB cameraBoundingBox)
        {
            if (clearColor)
                this.m_graphics.Clear(this.ClearColor);
            this.m_effect.View = this.m_viewMatrix;
            this.m_effect.Projection = this.m_projectionMatrix;
            this.m_renderViewList.Clear();
            for (int index = 0; index < this.m_topLevelViewList.Count; ++index)
                this.AddViewToRenderList(this.m_topLevelViewList[index], cameraBoundingBox);
            this.m_renderViewList.Sort();
            for (int index = 0; index < this.m_renderViewList.Count; ++index)
                this.m_renderViewList[index].Draw();
        }

        public void Draw(bool clearColor)
        {
            if (clearColor)
                this.m_graphics.Clear(this.ClearColor);
            this.m_effect.View = this.m_viewMatrix;
            this.m_effect.Projection = this.m_projectionMatrix;
            this.m_renderViewList.Clear();
            for (int index = 0; index < this.m_topLevelViewList.Count; ++index)
                this.AddViewToRenderList(this.m_topLevelViewList[index]);
            this.m_renderViewList.Sort();
            for (int index = 0; index < this.m_renderViewList.Count; ++index)
                this.m_renderViewList[index].Draw();
        }

        public void SetViewMatrix(Matrix viewMatrix) => this.m_viewMatrix = viewMatrix;

        public Matrix GetViewMatrix() => this.m_viewMatrix;

        public void SetProjectionMatrix(Matrix projectionMatrix) => this.m_projectionMatrix = projectionMatrix;

        public Matrix GetProjectionMatrix() => this.m_projectionMatrix;

        public void Begin2DMode(bool clearColor)
        {
            this.m_effect.View = Matrix.Identity;
            this.m_effect.Projection = Matrix.CreateOrthographicOffCenter(1f, 480f, 0.0f, 319f, 0.0f, 1f);
            if (!clearColor)
                return;
            this.m_graphics.Clear(ClearOptions.Target, this.ClearColor, 1f, 0);
        }

        public void End2DMode()
        {
            this.m_effect.View = this.m_viewMatrix;
            this.m_effect.Projection = this.m_projectionMatrix;
        }

        private void SetStringShadowEnabled(bool enabled) => this.m_stringShadow = enabled;

        private bool IsStringShadowEnabled() => this.m_stringShadow;

        public void DrawString(Font font, Color color, string s, float scaleY)
        {
            this.m_stringBuilder.Length = 0;
            this.m_stringBuilder.Append(s);
            this.DrawString(font, color, this.m_stringBuilder, scaleY);
        }

        public void DrawString(Font font, Color color, StringBuilder sb, float scaleY)
        {
            int length = sb.Length;
            float scaleW = font.GetFontData().scaleW;
            float scaleH = font.GetFontData().scaleH;
            float num1 = 0.0f;
            float num2 = 0.0f;
            int num3 = length;
            int numVertices = 0;
            for (int index1 = 0; index1 < length; ++index1)
            {
                if (sb[index1] == '\n')
                {
                    num2 -= font.GetLineHeight() * scaleY;
                    num1 = 0.0f;
                    --num3;
                }
                else
                {
                    FontCharData? charData = font.GetCharData(sb[index1]);
                    if (charData.HasValue)
                    {
                        float x = charData.Value.x;
                        float y = charData.Value.y;
                        float width = charData.Value.width;
                        float height = charData.Value.height;
                        float xoffset = charData.Value.xoffset;
                        float yoffset = charData.Value.yoffset;
                        this.m_stringVertices[numVertices].TextureCoordinate.X = x / scaleW;
                        this.m_stringVertices[numVertices].TextureCoordinate.Y = y / scaleH;
                        this.m_stringVertices[numVertices].Position.X = num1 + xoffset;
                        this.m_stringVertices[numVertices].Position.Y = num2 - yoffset;
                        int index2 = numVertices + 1;
                        this.m_stringVertices[index2].TextureCoordinate.X = x / scaleW;
                        this.m_stringVertices[index2].TextureCoordinate.Y = (y + height) / scaleH;
                        this.m_stringVertices[index2].Position.X = num1 + xoffset;
                        this.m_stringVertices[index2].Position.Y = num2 - height - yoffset;
                        int index3 = index2 + 1;
                        this.m_stringVertices[index3].TextureCoordinate.X = (x + width) / scaleW;
                        this.m_stringVertices[index3].TextureCoordinate.Y = y / scaleH;
                        this.m_stringVertices[index3].Position.X = width + num1 + xoffset;
                        this.m_stringVertices[index3].Position.Y = num2 - yoffset;
                        int index4 = index3 + 1;
                        this.m_stringVertices[index4].TextureCoordinate.X = (x + width) / scaleW;
                        this.m_stringVertices[index4].TextureCoordinate.Y = (y + height) / scaleH;
                        this.m_stringVertices[index4].Position.X = width + num1 + xoffset;
                        this.m_stringVertices[index4].Position.Y = num2 - height - yoffset;
                        numVertices = index4 + 1;
                        num1 += charData.Value.xadvance;
                    }
                }
            }
            int num4 = 0;
            int num5 = 0;
            for (int index5 = 0; index5 < num3; ++index5)
            {
                short[] stringIndices1 = this.m_stringIndices;
                int index6 = num5;
                int num6 = index6 + 1;
                int num7 = (int)(short)num4;
                stringIndices1[index6] = (short)num7;
                short[] stringIndices2 = this.m_stringIndices;
                int index7 = num6;
                int num8 = index7 + 1;
                int num9 = (int)(short)(num4 + 2);
                stringIndices2[index7] = (short)num9;
                short[] stringIndices3 = this.m_stringIndices;
                int index8 = num8;
                int num10 = index8 + 1;
                int num11 = (int)(short)(num4 + 1);
                stringIndices3[index8] = (short)num11;
                short[] stringIndices4 = this.m_stringIndices;
                int index9 = num10;
                int num12 = index9 + 1;
                int num13 = (int)(short)(num4 + 2);
                stringIndices4[index9] = (short)num13;
                short[] stringIndices5 = this.m_stringIndices;
                int index10 = num12;
                int num14 = index10 + 1;
                int num15 = (int)(short)(num4 + 3);
                stringIndices5[index10] = (short)num15;
                short[] stringIndices6 = this.m_stringIndices;
                int index11 = num14;
                num5 = index11 + 1;
                int num16 = (int)(short)(num4 + 1);
                stringIndices6[index11] = (short)num16;
                num4 += 4;
            }
            this.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.SetTexture(font.GetTexture());
            this.EnableClientStates(VertexFormat.Position3Texture);
            EffectPass pass = this.m_effect.CurrentTechnique.Passes[0];
            if (pass == null)
                return;
            if (this.m_stringShadow)
            {
                Matrix world = this.m_effect.World;
                Matrix matrix = world;
                --matrix.M41;
                ++matrix.M42;
                this.m_effect.World = matrix;
                this.SetColor(PSS.GameEngine.Graphics.Renderer.Renderer.STRING_SHADOW_COLOR);
                pass.Apply();
                this.m_graphics.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, this.m_stringVertices, 0, numVertices, this.m_stringIndices, 0, num5 / 3);
                this.m_effect.World = world;
            }
            this.SetColor(color);
            pass.Apply();
            this.m_graphics.DrawUserIndexedPrimitives<VertexPositionTexture>(PrimitiveType.TriangleList, this.m_stringVertices, 0, numVertices, this.m_stringIndices, 0, num5 / 3);
        }

        public void DrawString(Font font, Transform transform, int anchor, Color color, string s) => this.DrawString(font, transform, anchor, color, s, true);

        public void DrawString(
          Font font,
          Transform transform,
          int anchor,
          Color color,
          StringBuilder sb)
        {
            this.DrawString(font, transform, anchor, color, sb, true);
        }

        public void DrawString(
          Font font,
          Transform transform,
          int anchor,
          Color color,
          string s,
          bool intCoords)
        {
            this.m_stringBuilder.Length = 0;
            this.m_stringBuilder.Append(s);
            this.DrawString(font, transform, anchor, color, this.m_stringBuilder, intCoords);
        }

        public void DrawString(
          Font font,
          Transform transform,
          int anchor,
          Color color,
          StringBuilder sb,
          bool intCoords)
        {
            float d1 = 0.0f;
            float d2 = 0.0f;
            if ((anchor & 4) != 0)
                d1 = (float)(-(double)font.GetStringWidth(sb) * 0.5);
            else if ((anchor & 1) != 0)
                d1 = -font.GetStringWidth(sb);
            if ((anchor & 8) != 0)
                d2 = font.GetLineHeight() * 0.5f;
            else if ((anchor & 2) != 0)
                d2 = font.GetLineHeight();
            else if ((anchor & 2) != 0)
                d2 = font.GetBaselinePosition();
            if (intCoords)
            {
                d1 = (float)Math.Floor((double)d1);
                d2 = (float)Math.Floor((double)d2);
            }
            Matrix matrix = transform == null ? Matrix.Identity : transform.GetWorldTransform();
            matrix.M41 += d1;
            matrix.M42 += d2;
            this.m_effect.World = matrix;
            float scaleY = transform == null ? 1f : transform.GetScale().Y;
            this.DrawString(font, color, sb, scaleY);
        }

        public void DrawString(
          Font font,
          Transform transform,
          int anchor,
          Color color,
          string s,
          List<OLPair> pairArray,
          int pairStartOffset,
          int pairLength,
          bool intCoords)
        {
            for (int index = 0; index < pairLength; ++index)
            {
                OLPair pair = pairArray[pairStartOffset + index];
                if (pair.length > 0)
                    this.DrawString(font, transform, anchor, color, s.Substring(pair.offset, pair.length), intCoords);
                Vector3 position = transform.GetPosition();
                position.Y -= font.GetLineHeight() * transform.GetScale().Y;
                transform.SetPosition(position);
            }
        }

        public void DrawQuad(
          VertexPositionColor[] vertexBuffer,
          Transform transform,
          BlendFunc blendFunc)
        {
            if (blendFunc == BlendFunc.Opaque)
                this.m_graphics.BlendState = BlendState.Opaque;
            else
                this.SetBlendFunc(blendFunc);
            this.SetTexture((Texture2D)null);
            this.EnableClientStates(VertexFormat.Position3Color);
            this.SetColor(Color.White);
            this.m_effect.World = transform == null ? Matrix.Identity : transform.GetWorldTransform();
            EffectPass pass = this.m_effect.CurrentTechnique.Passes[0];
            if (pass == null)
                return;
            pass.Apply();
            this.m_graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertexBuffer, 0, 2);
        }

        public void DrawQuad<T>(
          T[] vertexBuffer,
          VertexFormat vertexFormat,
          Transform transform,
          Texture2D texture,
          Color color,
          int anchor,
          bool intCoords,
          BlendFunc blendFunc,
          bool opaque)
          where T : IVertexType
        {
            if (opaque)
                this.m_graphics.BlendState = BlendState.Opaque;
            else
                this.SetBlendFunc(blendFunc);
            this.SetTexture(texture);
            this.EnableClientStates(vertexFormat);
            this.SetColor(color);
            Matrix matrix = transform == null ? Matrix.Identity : transform.GetWorldTransform();
            float d1 = 0.0f;
            float d2 = 0.0f;
            if (anchor != 0)
            {
                float num1 = VertexFormatHelper.GetVertexPosition<T>(vertexBuffer[1], vertexFormat).X - VertexFormatHelper.GetVertexPosition<T>(vertexBuffer[0], vertexFormat).X;
                float num2 = VertexFormatHelper.GetVertexPosition<T>(vertexBuffer[0], vertexFormat).Y - VertexFormatHelper.GetVertexPosition<T>(vertexBuffer[2], vertexFormat).Y;
                if ((anchor & 4) != 0)
                    d1 = (float)(-(double)num1 * 0.5);
                else if ((anchor & 1) != 0)
                    d1 = -num1;
                if ((anchor & 8) != 0)
                    d2 = num2 * 0.5f;
                else if ((anchor & 2) != 0)
                    d2 = num2;
                if (intCoords)
                {
                    d1 = (float)Math.Floor((double)d1);
                    d2 = (float)Math.Floor((double)d2);
                }
            }
            matrix.M41 += d1;
            matrix.M42 += d2;
            this.m_effect.World = matrix;
            EffectPass pass = this.m_effect.CurrentTechnique.Passes[0];
            if (pass == null)
                return;
            pass.Apply();
            switch (vertexFormat)
            {
                case VertexFormat.Position3Color:
                    this.m_graphics.DrawUserPrimitives<VertexPositionColor>(PrimitiveType.TriangleStrip, vertexBuffer as VertexPositionColor[], 0, 2);
                    break;
                case VertexFormat.Position3ColorTexture:
                    this.m_graphics.DrawUserPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleStrip, vertexBuffer as VertexPositionColorTexture[], 0, 2);
                    break;
                case VertexFormat.Position3Texture:
                    this.m_graphics.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, vertexBuffer as VertexPositionTexture[], 0, 2);
                    break;
            }
        }

        public void DrawQuad<T>(
          T[] vertexBuffer,
          VertexFormat vertexFormat,
          Transform transform,
          PSS.GameEngine.Resources.Texture texture,
          Color color,
          int anchor,
          bool intCoords,
          BlendFunc blendFunc,
          bool opaque)
          where T : IVertexType
        {
            this.DrawQuad<T>(vertexBuffer, vertexFormat, transform, texture == null ? (Texture2D)null : texture.m_xnaTexture, color, anchor, intCoords, blendFunc, opaque);
        }

        public void DrawQuad<T>(
          T[] vertexBuffer,
          VertexFormat vertexFormat,
          Transform transform,
          PSS.GameEngine.Resources.Texture texture,
          Color color,
          int anchor,
          bool intCoords,
          BlendFunc blendFunc)
          where T : IVertexType
        {
            this.DrawQuad<T>(vertexBuffer, vertexFormat, transform, texture == null ? (Texture2D)null : texture.m_xnaTexture, color, anchor, intCoords, blendFunc, false);
        }

        public void DrawQuad<T>(
          T[] vertexBuffer,
          VertexFormat vertexFormat,
          Transform transform,
          PSS.GameEngine.Resources.Texture texture,
          Color color,
          int anchor,
          bool intCoords)
          where T : IVertexType
        {
            this.DrawQuad<T>(vertexBuffer, vertexFormat, transform, texture == null ? (Texture2D)null : texture.m_xnaTexture, color, anchor, intCoords, BlendFunc.SrcAlpha_InvSrcAlpha, false);
        }

        public void DrawQuad<T>(
          T[] vertexBuffer,
          VertexFormat vertexFormat,
          Transform transform,
          PSS.GameEngine.Resources.Texture texture,
          Color color,
          int anchor)
          where T : IVertexType
        {
            this.DrawQuad<T>(vertexBuffer, vertexFormat, transform, texture == null ? (Texture2D)null : texture.m_xnaTexture, color, anchor, true, BlendFunc.SrcAlpha_InvSrcAlpha, false);
        }

        public void DrawQuad<T>(
          T[] vertexBuffer,
          VertexFormat vertexFormat,
          Transform transform,
          PSS.GameEngine.Resources.Texture texture,
          Color color)
          where T : IVertexType
        {
            this.DrawQuad<T>(vertexBuffer, vertexFormat, transform, texture == null ? (Texture2D)null : texture.m_xnaTexture, color, 0, true, BlendFunc.SrcAlpha_InvSrcAlpha, false);
        }

        public void DrawQuad<T>(
          T[] vertexBuffer,
          VertexFormat vertexFormat,
          Transform transform,
          PSS.GameEngine.Resources.Texture texture)
          where T : IVertexType
        {
            this.DrawQuad<T>(vertexBuffer, vertexFormat, transform, texture == null ? (Texture2D)null : texture.m_xnaTexture, Color.White, 0, true, BlendFunc.SrcAlpha_InvSrcAlpha, false);
        }

        public void DrawAnimation2DModel(
          Animation2DModel model,
          int frame,
          Transform transform,
          BlendFunc blendFunc,
          bool useCustomColor,
          Color customColor,
          PSS.GameEngine.Resources.Texture srcTexture,
          PSS.GameEngine.Resources.Texture dstTexture,
          bool opaque)
        {
            if (transform != null)
                this.m_effect.World = transform.GetWorldTransform() with
                {
                    M43 = 0.0f
                };
            else
                this.m_effect.World = Matrix.Identity;
            if (!opaque)
                this.SetBlendFunc(blendFunc);
            else
                this.m_graphics.BlendState = BlendState.Opaque;
            this.EnableClientStates(VertexFormat.Position3ColorTexture);
            if (useCustomColor)
                this.SetColor(customColor);
            else
                this.SetColor(Color.White);
            EffectPass pass = this.m_effect.CurrentTechnique.Passes[0];
            if (pass == null)
                return;
            for (int index = 0; index < model.m_frames[frame].numMeshes; ++index)
            {
                int meshIndex = model.m_frames[frame].frameMeshes[index].meshIndex;
                short[] indexBuffer = model.m_meshes[meshIndex].indexBuffer;
                VertexPositionColorTexture[] vertexBuffer = model.m_meshes[meshIndex].vertexBuffer;
                if (model.m_hasMaterials)
                {
                    int meshMaterialIndex = model.m_frames[frame].frameMeshes[index].meshMaterialIndex;
                    Color diffuseColor = model.m_materials[meshMaterialIndex].diffuseColor;
                    if (useCustomColor)
                    {
                        diffuseColor.R = customColor.R;
                        diffuseColor.G = customColor.G;
                        diffuseColor.B = customColor.B;
                        diffuseColor.A = (byte)((int)diffuseColor.A * (int)customColor.A >> 8);
                    }
                    this.SetColor(diffuseColor);
                    PSS.GameEngine.Resources.Texture texture = model.m_textures[model.m_materials[meshMaterialIndex].diffuseTextureIndex];
                    if (texture != null)
                    {
                        if (object.ReferenceEquals((object)texture, (object)srcTexture))
                            texture = dstTexture;
                        this.SetTexture(texture);
                    }
                    else
                        this.SetTexture((Texture2D)null);
                }
                pass.Apply();
                this.m_graphics.DrawUserIndexedPrimitives<VertexPositionColorTexture>(PrimitiveType.TriangleList, vertexBuffer, 0, vertexBuffer.Length, indexBuffer, 0, indexBuffer.Length / 3);
            }
        }

        public void DrawAnimation2DModel(
          Animation2DModel model,
          int frame,
          Transform transform,
          BlendFunc blendFunc,
          bool useCustomColor,
          Color customColor,
          PSS.GameEngine.Resources.Texture srcTexture,
          PSS.GameEngine.Resources.Texture dstTexture)
        {
            this.DrawAnimation2DModel(model, frame, transform, blendFunc, useCustomColor, customColor, srcTexture, dstTexture, false);
        }

        public void DrawAnimation2DModel(
          Animation2DModel model,
          int frame,
          Transform transform,
          BlendFunc blendFunc,
          bool useCustomColor,
          Color customColor)
        {
            this.DrawAnimation2DModel(model, frame, transform, blendFunc, useCustomColor, customColor, (PSS.GameEngine.Resources.Texture)null, (PSS.GameEngine.Resources.Texture)null, false);
        }

        public void DrawAnimation2DModel(
          Animation2DModel model,
          int frame,
          Transform transform,
          BlendFunc blendFunc,
          bool useCustomColor,
          Color customColor,
          bool opaque)
        {
            this.DrawAnimation2DModel(model, frame, transform, blendFunc, useCustomColor, customColor, (PSS.GameEngine.Resources.Texture)null, (PSS.GameEngine.Resources.Texture)null, opaque);
        }

        public void DrawAnimation2DModel(
          Animation2DModel model,
          int frame,
          Transform transform,
          BlendFunc blendFunc)
        {
            this.DrawAnimation2DModel(model, frame, transform, blendFunc, false, Color.White, (PSS.GameEngine.Resources.Texture)null, (PSS.GameEngine.Resources.Texture)null, false);
        }

        public void DrawAnimation2DModel(
          Animation2DModel model,
          int frame,
          Transform transform,
          BlendFunc blendFunc,
          bool opaque)
        {
            this.DrawAnimation2DModel(model, frame, transform, blendFunc, false, Color.White, (PSS.GameEngine.Resources.Texture)null, (PSS.GameEngine.Resources.Texture)null, opaque);
        }

        public void DrawAABB(AABB box, Transform transform, Color color)
        {
        }

        public void DrawAABB(AABB box, Transform transform) => this.DrawAABB(box, transform, Color.White);

        public void DrawEntityCollisionBoxes(Entity entity)
        {
            foreach (CollisionBox collisionBox in entity.GetCollisionBoxes())
                this.DrawAABB(collisionBox.box, (Transform)null);
        }

        public void DrawEntityBox(Entity entity, Animation2D anim, string boxname)
        {
            AABB? box = anim.GetBox(boxname);
            if (!box.HasValue)
                return;
            Matrix worldTransform = entity.GetTransform().GetWorldTransform();
            AABB transformedBox;
            MathHelperEx.TransformBox(out transformedBox, box.Value, worldTransform);
            this.DrawAABB(transformedBox, (Transform)null);
        }

        public void AddTopLevelView(View view, Transform parentTransform)
        {
            view.GetTransform().SetParent(parentTransform);
            this.m_topLevelViewList.Add(view);
        }

        public void RemoveTopLevelView(View view) => this.m_topLevelViewList.Remove(view);

        public void ClearTopLevelViewList() => this.m_topLevelViewList.Clear();

        private Renderer()
        {
            this.m_topLevelViewList = new List<View>(256);
            this.m_renderViewList = new List<View>(256);
            this.ClearColor = Color.Black;
        }

        private void AddViewToRenderList(View view, AABB cameraBoundingBox)
        {
            if (view.IsInsideCamera(cameraBoundingBox))
                this.m_renderViewList.Add(view);
            for (int index = 0; index < view.GetNumSubviews(); ++index)
                this.AddViewToRenderList(view.GetSubviewAtIndex(index), cameraBoundingBox);
        }

        private void SetBlendFunc(BlendFunc blendFunc)
        {
            switch (blendFunc)
            {
                case BlendFunc.One_InvSrcAlpha:
                    this.m_graphics.BlendState = BlendState.AlphaBlend;
                    break;
                case BlendFunc.SrcAlpha_InvSrcAlpha:
                    this.m_graphics.BlendState = BlendState.NonPremultiplied;
                    break;
                case BlendFunc.SrcAlpha_One:
                    this.m_graphics.BlendState = BlendState.Additive;
                    break;
                case BlendFunc.One_Zero:
                    this.m_graphics.BlendState = BlendState.Opaque;
                    break;
            }
        }

        private void EnableClientStates(VertexFormat format)
        {
            switch (format)
            {
                case VertexFormat.Position3Color:
                    this.m_effect.VertexColorEnabled = true;
                    this.m_effect.TextureEnabled = false;
                    break;
                case VertexFormat.Position3ColorTexture:
                    this.m_effect.VertexColorEnabled = true;
                    this.m_effect.TextureEnabled = true;
                    break;
                case VertexFormat.Position3Texture:
                    this.m_effect.VertexColorEnabled = false;
                    this.m_effect.TextureEnabled = true;
                    break;
            }
        }

        private void SetColor(Color color)
        {
            Vector4 vector4 = color.ToVector4();
            this.m_effect.DiffuseColor = new Vector3(vector4.X, vector4.Y, vector4.Z);
            this.m_effect.Alpha = vector4.W;
        }

        private void SetTexture(PSS.GameEngine.Resources.Texture texture) => this.SetTexture(texture.m_xnaTexture);

        private void SetTexture(Texture2D texture) => this.m_effect.Texture = texture;

        private void AddViewToRenderList(View view)
        {
            this.m_renderViewList.Add(view);
            for (int index = 0; index < view.GetNumSubviews(); ++index)
                this.AddViewToRenderList(view.GetSubviewAtIndex(index));
        }
    }
}
