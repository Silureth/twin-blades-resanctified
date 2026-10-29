namespace Resanctified.Engine.Script
{
    public interface IScriptFunction
    {
        bool Update(float time);

        void Initialize();

        void Cleanup();
    }
}
