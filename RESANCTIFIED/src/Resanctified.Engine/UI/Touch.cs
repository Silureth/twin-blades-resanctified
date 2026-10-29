using Microsoft.Xna.Framework;

namespace Resanctified.Engine.UI
{
    public class Touch
    {
        public TouchId touchId;
        public TouchPhase touchPhase;
        public Vector2 prevPos;
        public Vector2 curPos;
        public int tapCount;
        public double timestamp;
        internal Vector2 prevNativePos;
        internal Vector2 curNativePos;
        internal int nativeTouchId;
    }
}
