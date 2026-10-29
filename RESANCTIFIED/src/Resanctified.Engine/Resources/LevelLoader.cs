using System.Collections.Generic;
using System.Text;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Resources
{
    public class LevelLoader
    {
        private string[] m_stringTable;
        private string m_levelFilename;

        public void LoadLevel(string filename, EntityFactory factory, EntityManager manager)
        {
            this.m_levelFilename = ResourceManager.NormalizeContentPath(filename);
            PSSBinaryReader fileDataReader = Application.Instance.Content.Load<PSSFile>(this.m_levelFilename).GetFileDataReader();
            int length = fileDataReader.ReadUInt16();
            this.m_stringTable = new string[length];
            for (int index = 0; index < length; ++index)
                this.m_stringTable[index] = fileDataReader.ReadLPString();
            int num = fileDataReader.ReadUInt16();
            LevelNode reusableNode = new LevelNode();
            reusableNode.properties = new List<LevelNodeProperty>(32);
            for (int index = 0; index < num; ++index)
                this.ProcessNode(fileDataReader, ref reusableNode, null, factory, manager);
            fileDataReader.Close();
        }

        public virtual Entity CreateEntity(
          ref LevelNode node,
          Entity parent,
          EntityFactory factory)
        {
            LevelNodeProperty? nullable1 = node.Find("type");
            if (!nullable1.HasValue || !nullable1.Value.stringValue.Equals("parallax"))
                return null;
            ParallaxEntity entity = (ParallaxEntity)factory.CreateEntity("parallax");
            StringBuilder stringBuilder = new StringBuilder(256);
            stringBuilder.Append(this.m_levelFilename).Append('_').Append(node.name);
            string animFilename = stringBuilder.ToString();
            bool flag = false;
            LevelNodeProperty? nullable2 = node.Find("opaque");
            if (nullable2.HasValue)
                flag = nullable2.Value.intValue != 0;
            BlendFunc blendFunc = BlendFunc.SrcAlpha_InvSrcAlpha;
            LevelNodeProperty? nullable3 = node.Find("blendfunc");
            if (nullable3.HasValue)
                blendFunc = (BlendFunc)nullable3.Value.intValue;
            float num = 1f;
            nullable3 = node.Find("factorx");
            if (nullable3.HasValue)
                num = nullable3.Value.floatValue;
            bool sky = node.name.Equals("sky");
            entity.Initialize(animFilename, 1f - num, 0.0f, sky | flag, blendFunc, sky);
            entity.SetName(node.name);
            entity.GetTransform().SetPosition(node.position);
            entity.GetTransform().SetScale(node.scale);
            entity.GetTransform().SetRotationZ(node.rotationZ);
            return entity;
        }

        private void ProcessNode(
          PSSBinaryReader input,
          ref LevelNode reusableNode,
          Entity parent,
          EntityFactory factory,
          EntityManager manager)
        {
            int index1 = input.ReadUInt16();
            reusableNode.name = this.m_stringTable[index1];
            int num1 = input.ReadUInt16();
            reusableNode.position = input.ReadVector3();
            reusableNode.scale = input.ReadVector2();
            reusableNode.rotationZ = input.ReadSingle();
            int num2 = input.ReadUInt16();
            reusableNode.properties.Clear();
            for (int index2 = 0; index2 < num2; ++index2)
            {
                LevelNodeProperty levelNodeProperty = new LevelNodeProperty();
                int index3 = input.ReadUInt16();
                levelNodeProperty.name = this.m_stringTable[index3];
                byte num3 = input.ReadByte();
                levelNodeProperty.type = (LevelNodePropertyType)num3;
                switch (levelNodeProperty.type)
                {
                    case LevelNodePropertyType.String:
                        int index4 = input.ReadUInt16();
                        levelNodeProperty.stringValue = this.m_stringTable[index4];
                        break;
                    case LevelNodePropertyType.Int:
                        levelNodeProperty.intValue = input.ReadInt32();
                        break;
                    case LevelNodePropertyType.Float:
                        levelNodeProperty.floatValue = input.ReadSingle();
                        break;
                }
                reusableNode.properties.Add(levelNodeProperty);
            }
            Entity entity = this.CreateEntity(ref reusableNode, parent, factory);
            if (entity != null)
                manager.AddEntity(entity);
            for (int index5 = 0; index5 < num1; ++index5)
                this.ProcessNode(input, ref reusableNode, parent, factory, manager);
        }
    }
}
