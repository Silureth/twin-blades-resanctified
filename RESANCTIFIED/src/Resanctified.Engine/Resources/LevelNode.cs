using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Resources
{
    public struct LevelNode
    {
        public string name;
        public Vector3 position;
        public Vector2 scale;
        public float rotationZ;
        public List<LevelNodeProperty> properties;

        public LevelNodeProperty? Find(string propertyName)
        {
            for (int index = 0; index < this.properties.Count; ++index)
            {
                if (this.properties[index].name.Equals(propertyName))
                    return new LevelNodeProperty?(this.properties[index]);
            }
            return new LevelNodeProperty?();
        }
    }
}
