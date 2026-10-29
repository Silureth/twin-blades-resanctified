namespace Resanctified.Engine.UI
{
    public interface Responder
    {
        void AddResponder(Responder responder);

        void RemoveResponder(Responder responder);

        void TouchesBegan(Touch[] touchArray, int touchCount);

        void TouchesMoved(Touch[] touchArray, int touchCount);

        void TouchesEnded(Touch[] touchArray, int touchCount);

        void TouchesCancelled(Touch[] touchArray, int touchCount);

        void ShakeBegin();

        void ShakeEnded();

        void ShakeCancelled();
    }
}
