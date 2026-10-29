using Microsoft.Xna.Framework.Graphics;

namespace Resanctified.Engine.Resources
{
    public sealed class Texture : Resource
    {
        internal Texture2D m_xnaTexture;

        public Texture()
          : base(ResourceType.Texture)
        {
        }

        public int GetWidth() => this.m_xnaTexture.Width;

        public int GetHeight() => this.m_xnaTexture.Height;

        internal override void Load()
        {
            if (this.m_isLoaded)
                return;
            this.m_xnaTexture = Application.Instance.Content.Load<Texture2D>(this.m_filename);
            this.m_isLoaded = true;
        }

        internal override void Unload()
        {
            if (!this.m_isLoaded)
                return;
            if (this.m_xnaTexture != null)
                this.m_xnaTexture = null;
            this.m_isLoaded = false;
        }
    }
}
