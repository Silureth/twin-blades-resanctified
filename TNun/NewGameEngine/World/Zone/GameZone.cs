// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.Zone.GameZone
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using PSS.GameEngine.World.Entities;

namespace PSS.GameEngine.World.Zone
{
  public class GameZone
  {
    public Vector3 Position;

    public float Width { get; protected set; }

    public float Height { get; protected set; }

    public string Name { get; protected set; }

    public GameZone(string name, Vector3 position, float width, float height)
    {
      this.Name = name;
      this.Position = position;
      this.Width = width;
      this.Height = height;
    }

    public void ConstraintEntity(Entity entity, float left, float right, float bottom, float top)
    {
    }

    public void ConstraintCamera(
      CameraEntity entity,
      float left,
      float right,
      float bottom,
      float top)
    {
    }

    public void ConstraintPosition(
      ref Vector3 position,
      float left,
      float right,
      float bottom,
      float top)
    {
      if ((double) position.X + (double) left < (double) this.Position.X)
        position.X = this.Position.X - left;
      if ((double) position.X + (double) right > (double) this.Position.X + (double) this.Width)
        position.X = this.Position.X + this.Width - right;
      if ((double) position.Y + (double) bottom < (double) this.Position.Y)
        position.Y = this.Position.Y - bottom;
      if ((double) position.Y + (double) top <= (double) this.Position.Y + (double) this.Height)
        return;
      position.Y = this.Position.Y + this.Height - top;
    }
  }
}
