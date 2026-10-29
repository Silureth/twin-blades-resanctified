namespace Resanctified.Engine
{
    public interface IGameComponent
    {
        void Initialize();

        void Cleanup();

        void Update(float gameTime);
    }
}
