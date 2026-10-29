using Microsoft.Xna.Framework;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.World.Zone
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
            if (position.X + (double)left < this.Position.X)
                position.X = this.Position.X - left;
            if (position.X + (double)right > this.Position.X + (double)this.Width)
                position.X = this.Position.X + this.Width - right;
            if (position.Y + (double)bottom < this.Position.Y)
                position.Y = this.Position.Y - bottom;
            if (position.Y + (double)top <= this.Position.Y + (double)this.Height)
                return;
            position.Y = this.Position.Y + this.Height - top;
        }
    }
}
