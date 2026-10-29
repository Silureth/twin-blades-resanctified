using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSoundStopAll : IScriptFunction
    {
        public bool Update(float gameTime)
        {
            foreach (Sound scriptSound in ScriptManager.Instance.ScriptSoundList)
                scriptSound.Stop();
            ScriptManager.Instance.ScriptSoundList.Clear();
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
