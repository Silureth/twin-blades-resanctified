// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Resources.LevelNode
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PSS.GameEngine.Resources
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
