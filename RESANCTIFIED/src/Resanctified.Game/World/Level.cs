namespace Resanctified.Game.World
{
    public class Level
    {
        private string m_modelPath;
        private string m_textureDayPath;
        private string m_textureNightPath;
        private string m_textureSkyMorningPath;
        private string m_textureSkyDayPath;
        private string m_textureSkyEveningPath;
        private string m_textureSkyNightPath;
        private LevelType m_levelType;
        private int m_infestationLevel;
        public bool Locked;

        public Level(
          string modelPath,
          string textureDayPath,
          string textureNightPath,
          string textureSkyMorningPath,
          string textureSkyDayPath,
          string textureSkyEveningPath,
          string textureSkyNightPath,
          LevelType type)
        {
            this.m_modelPath = modelPath;
            this.m_textureDayPath = textureDayPath;
            this.m_textureNightPath = textureNightPath;
            this.m_textureSkyMorningPath = textureSkyMorningPath;
            this.m_textureSkyDayPath = textureSkyDayPath;
            this.m_textureSkyEveningPath = textureSkyEveningPath;
            this.m_textureSkyNightPath = textureSkyNightPath;
            this.m_levelType = type;
            this.m_infestationLevel = 0;
            this.Locked = false;
        }

        public int GetInfestationLevel() => this.m_infestationLevel;

        public void SetInfestationLevel(int level) => this.m_infestationLevel = level;

        public void IncreaseInfestationLevel()
        {
            if (this.m_infestationLevel >= 3)
                return;
            ++this.m_infestationLevel;
        }

        public string GetModelPath() => this.m_modelPath;

        public string GetTextureDayPath() => this.m_textureDayPath;

        public string GetTextureNightPath() => this.m_textureNightPath;

        public string GetTextureSkyMorningPath() => this.m_textureSkyMorningPath;

        public string GetTextureSkyDayPath() => this.m_textureSkyDayPath;

        public string GetTextureSkyEveningPath() => this.m_textureSkyEveningPath;

        public string GetTextureSkyNightPath() => this.m_textureSkyNightPath;

        public LevelType GetLevelType() => this.m_levelType;

        public enum LevelType
        {
            Normal,
            Boss,
            Shop,
            Script,
        }
    }
}
