namespace Resanctified.Engine.Script
{
    public class ScriptFunctionMusicPlay : IScriptFunction
    {
        private int m_index;
        private float m_volume;
        private bool m_loop;

        public ScriptFunctionMusicPlay(int index, float volume, bool loop)
        {
            this.m_index = index;
            this.m_volume = volume;
            this.m_loop = loop;
        }

        public bool Update(float gameTime) => true;

        public void Initialize()
        {
        }

        public void Cleanup()
        {
        }
    }
}
