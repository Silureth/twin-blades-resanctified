namespace Resanctified.Engine.UI
{
    public enum TouchPhase
    {
        Unknown = -1, // 0xFFFFFFFF
        Began = 0,
        Ended = 1,
        Moved = 2,
        Stationary = 3,
        Cancelled = 4,
        TouchPhaseMax = 5,
    }
}
