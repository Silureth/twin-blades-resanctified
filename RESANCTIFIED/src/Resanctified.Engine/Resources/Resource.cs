namespace Resanctified.Engine.Resources
{
    public abstract class Resource
    {
        internal string m_filename;
        internal int m_refCount;
        internal ResourceType m_type;
        internal bool m_isLoaded;

        internal Resource(ResourceType type)
        {
            this.m_type = type;
            this.m_refCount = 0;
            this.m_isLoaded = false;
            this.m_filename = "";
        }

        internal ResourceType GetResourceType() => this.m_type;

        public string GetResourceFilename() => this.m_filename;

        public bool IsLoaded() => this.m_isLoaded;

        public int GetRefCount() => this.m_refCount;

        public void AddRef() => ++this.m_refCount;

        public void Release() => --this.m_refCount;

        public static void SafeRelease<T>(ref T r) where T : Resource
        {
            if (r == null)
                return;
            r.Release();
            r = default(T);
        }

        internal abstract void Load();

        internal abstract void Unload();

        public enum ResourceType
        {
            Texture,
            Animation2DModel,
            Font,
            SoundModel,
        }
    }
}
