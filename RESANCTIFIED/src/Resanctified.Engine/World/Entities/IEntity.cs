namespace Resanctified.Engine.World.Entities
{
    public interface IEntity
    {
        void Initialize();

        void Cleanup();

        void Update(float elapsedTime);
    }
}
