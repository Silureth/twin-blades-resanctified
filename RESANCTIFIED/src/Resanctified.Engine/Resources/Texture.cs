using Microsoft.Xna.Framework.Graphics;

namespace Resanctified.Engine.Resources
{
    public sealed class Texture : Resource
    {
        internal Texture2D m_xnaTexture;
        // HD textures are created here and must be disposed here; originals belong to the ContentManager.
        private bool m_ownsXnaTexture;
        private int m_width;
        private int m_height;

        public Texture()
          : base(ResourceType.Texture)
        {
        }

        /// <summary>Width of the original texture, also when the HD version is loaded: UVs and layout depend on it.</summary>
        public int GetWidth() => this.m_width;

        /// <summary>Height of the original texture, also when the HD version is loaded.</summary>
        public int GetHeight() => this.m_height;

        internal override void Load()
        {
            if (this.m_isLoaded)
                return;
            this.ReleaseXnaTexture();
            Texture2D hdTexture = null;
            if (ResourceManager.Instance.GetTextureSet() == TextureSet.HD)
                hdTexture = HdTexturePack.Instance.TryLoadTexture(Application.Instance.GraphicsDevice, this.m_filename);
            if (hdTexture != null)
            {
                this.m_xnaTexture = hdTexture;
                this.m_ownsXnaTexture = true;
                this.m_width = hdTexture.Width / HdTexturePack.Instance.Scale;
                this.m_height = hdTexture.Height / HdTexturePack.Instance.Scale;
            }
            else
            {
                this.m_xnaTexture = Application.Instance.Content.Load<Texture2D>(this.m_filename);
                this.m_width = this.m_xnaTexture.Width;
                this.m_height = this.m_xnaTexture.Height;
            }
            this.m_isLoaded = true;
        }

        /// <summary>
        /// Loads the image again from the current texture set. Models and fonts share this wrapper object,
        /// so they show the new image from the next draw without being reloaded themselves.
        /// </summary>
        internal void Reload()
        {
            if (!this.m_isLoaded)
                return;
            this.m_isLoaded = false;
            this.Load();
        }

        internal override void Unload()
        {
            if (!this.m_isLoaded)
                return;
            this.ReleaseXnaTexture();
            this.m_isLoaded = false;
        }

        private void ReleaseXnaTexture()
        {
            if (this.m_ownsXnaTexture)
                this.m_xnaTexture?.Dispose();
            this.m_xnaTexture = null;
            this.m_ownsXnaTexture = false;
            this.m_width = 0;
            this.m_height = 0;
        }
    }
}
