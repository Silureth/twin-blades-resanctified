// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Resources.Animation2DModel
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Maths;
using System.Text;

namespace Resanctified.Engine.Resources
{
  public sealed class Animation2DModel : Resource
  {
    internal float m_fps;
    internal bool m_hasVertexColors;
    internal bool m_hasMaterials;
    internal bool m_hasTextureCoords;
    internal AABB m_boundingBox;
    internal int m_numStrings;
    internal int m_numMaterials;
    internal int m_numMeshes;
    internal int m_numBoxes;
    internal int m_numFrames;
    internal AABB[] m_boxes;
    internal string[] m_strings;
    internal Animation2DModel.Material[] m_materials;
    internal Animation2DModel.Mesh[] m_meshes;
    internal Animation2DModel.Frame[] m_frames;
    internal Texture[] m_textures;

    public Animation2DModel()
      : base(Resource.ResourceType.Animation2DModel)
    {
    }

    public bool HasVertexColors() => this.m_hasVertexColors;

    public bool HasTextureCoords() => this.m_hasTextureCoords;

    public bool HasMaterials() => this.m_hasMaterials;

    public float GetFPS() => this.m_fps;

    public float GetDuration() => (float) this.m_numFrames / this.m_fps;

    public int GetFrameCount() => this.m_numFrames;

    public AABB GetBoundingBox() => this.m_boundingBox;

    public AABB? GetBox(string name, int frameIndex)
    {
      if (this.m_numBoxes == 0)
        return new AABB?();
      for (int index = 0; index < this.m_frames[frameIndex].numBoxes; ++index)
      {
        if (this.m_strings[this.m_frames[frameIndex].frameBoxes[index].boxNameIndex].Equals(name))
          return new AABB?(this.m_boxes[this.m_frames[frameIndex].frameBoxes[index].boxIndex]);
      }
      return new AABB?();
    }

    public AABB? GetBoxAndColor(string name, int frameIndex, out Color color)
    {
      color = Color.White;
      if (this.m_numBoxes == 0)
        return new AABB?();
      for (int index = 0; index < this.m_frames[frameIndex].numBoxes; ++index)
      {
        if (this.m_strings[this.m_frames[frameIndex].frameBoxes[index].boxNameIndex].Equals(name))
        {
          color = !this.m_hasMaterials ? Color.White : this.m_materials[this.m_frames[frameIndex].frameBoxes[index].boxMaterialIndex].diffuseColor;
          return new AABB?(this.m_boxes[this.m_frames[frameIndex].frameBoxes[index].boxIndex]);
        }
      }
      return new AABB?();
    }

    public int GetBoxes(AABB[] buffer, int length, string name, int frameIndex)
    {
      if (this.m_numBoxes == 0)
        return 0;
      int boxes = 0;
      for (int index = 0; index < this.m_frames[frameIndex].numBoxes && boxes < length; ++index)
      {
        if (this.m_strings[this.m_frames[frameIndex].frameBoxes[index].boxNameIndex].Equals(name))
        {
          buffer[boxes] = this.m_boxes[this.m_frames[frameIndex].frameBoxes[index].boxIndex];
          ++boxes;
        }
      }
      return boxes;
    }

    public Texture[] GetTextures() => this.m_textures;

    internal override void Load()
    {
      if (this.m_isLoaded)
        return;
      PSSBinaryReader fileDataReader = Application.Instance.Content.Load<PSSFile>(this.m_filename).GetFileDataReader();
      this.m_fps = fileDataReader.ReadSingle();
      int num = (int) fileDataReader.ReadUInt16();
      this.m_hasVertexColors = (num & 1) != 0;
      this.m_hasTextureCoords = (num & 2) != 0;
      this.m_hasMaterials = (num & 4) != 0;
      this.m_boundingBox.InitializeFromBounds(fileDataReader.ReadVector3(), fileDataReader.ReadVector3());
      this.m_numStrings = (int) fileDataReader.ReadUInt16();
      this.m_numMaterials = (int) fileDataReader.ReadUInt16();
      this.m_numMeshes = (int) fileDataReader.ReadUInt16();
      this.m_numBoxes = (int) fileDataReader.ReadUInt16();
      this.m_numFrames = (int) fileDataReader.ReadUInt16();
      if (this.m_numStrings > 0)
      {
        this.m_strings = new string[this.m_numStrings];
        for (int index = 0; index < this.m_numStrings; ++index)
          this.m_strings[index] = fileDataReader.ReadCString();
        if (this.m_hasTextureCoords)
          this.m_textures = new Texture[this.m_numStrings];
      }
      if (this.m_numMaterials > 0 && this.m_hasMaterials)
      {
        this.m_materials = new Animation2DModel.Material[this.m_numMaterials];
        StringBuilder stringBuilder = new StringBuilder(256);
        for (int index = 0; index < this.m_numMaterials; ++index)
        {
          this.m_materials[index].diffuseColor = fileDataReader.ReadColor();
          if (this.m_hasTextureCoords)
          {
            this.m_materials[index].diffuseTextureIndex = (int) fileDataReader.ReadUInt16();
            if (!string.IsNullOrEmpty(this.m_strings[this.m_materials[index].diffuseTextureIndex]) && this.m_textures[this.m_materials[index].diffuseTextureIndex] == null)
            {
              stringBuilder.Length = 0;
              stringBuilder.Append("Textures\\");
              stringBuilder.Append(this.m_strings[this.m_materials[index].diffuseTextureIndex]);
              Texture texture = ResourceManager.Instance.LoadTexture(stringBuilder.ToString());
              this.m_textures[this.m_materials[index].diffuseTextureIndex] = texture;
            }
          }
        }
      }
      if (this.m_numMeshes > 0)
      {
        this.m_meshes = new Animation2DModel.Mesh[this.m_numMeshes];
        for (int index1 = 0; index1 < this.m_numMeshes; ++index1)
        {
          this.m_meshes[index1].numIndices = (int) fileDataReader.ReadUInt16();
          this.m_meshes[index1].numVertices = (int) fileDataReader.ReadUInt16();
          this.m_meshes[index1].commonZ = fileDataReader.ReadSingle();
          this.m_meshes[index1].indexBuffer = new short[this.m_meshes[index1].numIndices];
          this.m_meshes[index1].vertexBuffer = new VertexPositionColorTexture[this.m_meshes[index1].numVertices];
          for (int index2 = 0; index2 < this.m_meshes[index1].numIndices; ++index2)
            this.m_meshes[index1].indexBuffer[index2] = fileDataReader.ReadInt16();
          for (int index3 = 0; index3 < this.m_meshes[index1].numVertices; ++index3)
          {
            Vector2 vector2 = fileDataReader.ReadVector2();
            this.m_meshes[index1].vertexBuffer[index3].Position.X = vector2.X;
            this.m_meshes[index1].vertexBuffer[index3].Position.Y = vector2.Y;
            this.m_meshes[index1].vertexBuffer[index3].Position.Z = 0.0f;
            this.m_meshes[index1].vertexBuffer[index3].Color = !this.m_hasVertexColors ? Color.White : fileDataReader.ReadColor();
            this.m_meshes[index1].vertexBuffer[index3].TextureCoordinate = !this.m_hasTextureCoords ? Vector2.Zero : fileDataReader.ReadVector2();
          }
        }
      }
      if (this.m_numBoxes > 0)
      {
        this.m_boxes = new AABB[this.m_numBoxes];
        for (int index = 0; index < this.m_numBoxes; ++index)
          this.m_boxes[index].InitializeFromBounds(fileDataReader.ReadVector3(), fileDataReader.ReadVector3());
      }
      if (this.m_numFrames > 0)
      {
        this.m_frames = new Animation2DModel.Frame[this.m_numFrames];
        for (int index4 = 0; index4 < this.m_numFrames; ++index4)
        {
          if (this.m_numMeshes > 0)
          {
            this.m_frames[index4].numMeshes = (int) fileDataReader.ReadUInt16();
            this.m_frames[index4].frameMeshes = new Animation2DModel.FrameMesh[this.m_frames[index4].numMeshes];
          }
          else
          {
            this.m_frames[index4].numMeshes = 0;
            this.m_frames[index4].frameMeshes = (Animation2DModel.FrameMesh[]) null;
          }
          if (this.m_numBoxes > 0)
          {
            this.m_frames[index4].numBoxes = (int) fileDataReader.ReadUInt16();
            this.m_frames[index4].frameBoxes = new Animation2DModel.FrameBox[this.m_frames[index4].numBoxes];
          }
          else
          {
            this.m_frames[index4].numBoxes = 0;
            this.m_frames[index4].frameBoxes = (Animation2DModel.FrameBox[]) null;
          }
          for (int index5 = 0; index5 < this.m_frames[index4].numMeshes; ++index5)
          {
            this.m_frames[index4].frameMeshes[index5].meshIndex = (int) fileDataReader.ReadUInt16();
            if (this.m_hasMaterials)
              this.m_frames[index4].frameMeshes[index5].meshMaterialIndex = (int) fileDataReader.ReadUInt16();
          }
          for (int index6 = 0; index6 < this.m_frames[index4].numBoxes; ++index6)
          {
            this.m_frames[index4].frameBoxes[index6].boxIndex = (int) fileDataReader.ReadUInt16();
            this.m_frames[index4].frameBoxes[index6].boxNameIndex = (int) fileDataReader.ReadUInt16();
            if (this.m_hasMaterials)
              this.m_frames[index4].frameBoxes[index6].boxMaterialIndex = (int) fileDataReader.ReadUInt16();
          }
        }
      }
      fileDataReader.Close();
      this.m_isLoaded = true;
    }

    internal override void Unload()
    {
      if (!this.m_isLoaded)
        return;
      this.m_strings = (string[]) null;
      this.m_materials = (Animation2DModel.Material[]) null;
      this.m_meshes = (Animation2DModel.Mesh[]) null;
      this.m_frames = (Animation2DModel.Frame[]) null;
      if (this.m_textures != null)
      {
        for (int index = 0; index < this.m_textures.Length; ++index)
          Resource.SafeRelease<Texture>(ref this.m_textures[index]);
        this.m_textures = (Texture[]) null;
      }
      this.m_boxes = (AABB[]) null;
      this.m_isLoaded = false;
    }

    internal struct Frame
    {
      internal int numMeshes;
      internal int numBoxes;
      internal Animation2DModel.FrameMesh[] frameMeshes;
      internal Animation2DModel.FrameBox[] frameBoxes;
    }

    internal struct FrameMesh
    {
      internal int meshIndex;
      internal int meshMaterialIndex;
    }

    internal struct Mesh
    {
      internal int numIndices;
      internal int numVertices;
      internal float commonZ;
      internal VertexPositionColorTexture[] vertexBuffer;
      internal short[] indexBuffer;
    }

    internal struct Material
    {
      internal Color diffuseColor;
      internal int diffuseTextureIndex;
    }

    internal struct FrameBox
    {
      internal int boxIndex;
      internal int boxNameIndex;
      internal int boxMaterialIndex;
    }
  }
}
