using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Maths
{
    public struct AABB
    {
        public Vector3 boundMin;
        public Vector3 boundMax;
        public Vector3 center;
        public Vector3 extents;

        public void InitializeFromCenterAndExtents(Vector3 c, Vector3 e)
        {
            this.boundMin = c - e;
            this.boundMax = c + e;
            this.center = c;
            this.extents = e;
        }

        public void InitializeFromBounds(Vector3 min, Vector3 max)
        {
            this.boundMin = min;
            this.boundMax = max;
            this.center = (min + max) * 0.5f;
            this.extents = (max - min) * 0.5f;
        }

        public Vector3 this[int index]
        {
            get => index != 0 ? this.boundMax : this.boundMin;
            set
            {
                if (index == 0)
                    this.boundMin = value;
                else
                    this.boundMax = value;
            }
        }
    }
}
