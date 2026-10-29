using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionMusicStop : IScriptFunction
    {
        public bool Update(float gameTime)
        {
            AudioManager.Instance.StopMusic();
            return true;
        }

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }
    }
}
