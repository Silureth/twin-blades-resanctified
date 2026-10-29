using Microsoft.Xna.Framework.Audio;
using Resanctified.Engine.Sounds;

namespace Resanctified.Engine.Resources
{
    public sealed class SoundModel : Resource
    {
        private SoundEffect m_soundEffect;

        public SoundModel()
          : base(ResourceType.SoundModel)
        {
        }

        internal override void Load()
        {
            if (this.m_isLoaded)
                return;
            // Without an audio device the effect stays null and Sound.Play() does nothing.
            if (AudioManager.Instance.IsAvailable)
                this.m_soundEffect = Application.Instance.Content.Load<SoundEffect>(this.m_filename);
            this.m_isLoaded = true;
        }

        internal override void Unload()
        {
            if (!this.m_isLoaded)
                return;
            if (this.m_soundEffect != null)
                this.m_soundEffect = null;
            this.m_isLoaded = false;
        }

        internal SoundEffect GetSoundEffect() => this.m_soundEffect;
    }
}
