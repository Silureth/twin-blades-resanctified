using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Script
{
    public class ScriptFunctionSoundPlay : IScriptFunction
    {
        private Sound m_sound;

        public ScriptFunctionSoundPlay(string soundName, string bankName)
        {
        }

        public bool Update(float gameTime) => true;

        public void Initialize()
        {
        }

        public void Cleanup() => this.m_sound = null;
    }
}
