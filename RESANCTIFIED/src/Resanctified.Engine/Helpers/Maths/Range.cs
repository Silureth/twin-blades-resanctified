using System;

namespace Resanctified.Engine.Helpers.Maths
{
    public struct Range
    {
        public float Min;
        public float Max;

        public Range(float min, float max)
        {
            this.Min = min;
            this.Max = max;
        }

        public float? SmallestOverlap(Range other)
        {
            float val1 = this.Max - other.Min;
            if ((double)val1 < 0.0)
                return new float?();
            float val2 = this.Min - other.Max;
            return (double)val2 < 0.0 ? new float?() : new float?(Math.Min(val1, val2));
        }
    }
}
