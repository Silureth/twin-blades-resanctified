using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Maths
{
    public struct Ray
    {
        public Vector3 origin;
        public Vector3 direction;
        public Vector3 inv_direction;
        public int signX;
        public int signY;
        public int signZ;

        public Ray(Vector3 o, Vector3 d)
        {
            this.origin = o;
            this.direction = d;
            this.inv_direction = Vector3.One;
            this.inv_direction /= d;
            this.signX = this.inv_direction.X < 0.0 ? 1 : 0;
            this.signY = this.inv_direction.Y < 0.0 ? 1 : 0;
            this.signZ = this.inv_direction.Z < 0.0 ? 1 : 0;
        }

        public void Initialize(Vector3 o, Vector3 d)
        {
            this.origin = o;
            this.direction = d;
            this.inv_direction.X = 1f / d.X;
            this.inv_direction.Y = 1f / d.Y;
            this.inv_direction.Z = 1f / d.Z;
            this.signX = this.inv_direction.X < 0.0 ? 1 : 0;
            this.signY = this.inv_direction.Y < 0.0 ? 1 : 0;
            this.signZ = this.inv_direction.Z < 0.0 ? 1 : 0;
        }

        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return this.signX;
                return index == 1 ? this.signY : this.signZ;
            }
            set
            {
                if (index == 0)
                    this.signX = value;
                else if (index == 1)
                    this.signY = value;
                else
                    this.signZ = value;
            }
        }
    }
}
