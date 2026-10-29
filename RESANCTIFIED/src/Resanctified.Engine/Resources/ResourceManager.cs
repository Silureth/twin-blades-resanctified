using System.Collections.Generic;
using Resanctified.Engine.Tools;

namespace Resanctified.Engine.Resources
{
    public sealed class ResourceManager
    {
        private const int MAX_RESOURCES_TO_LOAD = 256;
        private const int TEXTURE_POOL_SIZE = 32;
        private const int ANIMATION2DMODEL_POOL_SIZE = 256;
        private const int SOUNDMODEL_POOL_SIZE = 64;
        private const int FONT_POOL_SIZE = 4;
        private const int UNUSED_RESOURCES_LIST_SIZE = 1024;
        private static readonly ResourceManager s_instance = new ResourceManager();
        private Resource[] m_resourcesToLoad;
        private int m_numResourcesToLoad;
        private Pool<Texture> m_texturePool;
        private Pool<Animation2DModel> m_animModelPool;
        private Pool<SoundModel> m_soundModelPool;
        private Pool<Font> m_fontPool;
        private Dictionary<string, Resource> m_textureNameIndex;
        private Dictionary<string, Resource> m_animModelNameIndex;
        private Dictionary<string, Resource> m_fontNameIndex;
        private Dictionary<string, Resource> m_soundModelNameIndex;
        private List<KeyValuePair<string, Resource>> m_unusedResourcesList;

        public static ResourceManager Instance => s_instance;

        public static ResourceManager GetInstance() => s_instance;

        /// <summary>
        /// Turns an asset name from the original game ("Textures\Sky_Day") into the form the
        /// content folder uses on every platform ("textures/sky_day"): forward slashes and lowercase,
        /// because Linux and Android file systems are case-sensitive and do not treat '\' as a separator.
        /// Every content load should go through this.
        /// </summary>
        public static string NormalizeContentPath(string assetName) => assetName.Replace('\\', '/').ToLowerInvariant();

        public void Initialize()
        {
            PSSFileReader.RegisterLegacyReaderName();
            this.m_fontNameIndex.Clear();
            this.m_animModelNameIndex.Clear();
            this.m_textureNameIndex.Clear();
            this.m_soundModelNameIndex.Clear();
            this.m_unusedResourcesList.Clear();
        }

        public void Cleanup()
        {
            this.Cleanup(this.m_fontNameIndex);
            this.Cleanup(this.m_animModelNameIndex);
            this.Cleanup(this.m_textureNameIndex);
            this.Cleanup(this.m_soundModelNameIndex);
            this.m_unusedResourcesList.Clear();
            Application.Instance.Content.Unload();
        }

        public Texture LoadTexture(string filename)
        {
            filename = NormalizeContentPath(filename);
            Resource resource = null;
            if (!this.m_textureNameIndex.TryGetValue(filename, out resource))
            {
                resource = this.m_texturePool.Add();
                resource.m_filename = filename;
                resource.m_refCount = 1;
                resource.m_isLoaded = false;
                this.m_textureNameIndex.Add(filename, resource);
            }
            else
                resource.AddRef();
            return (Texture)resource;
        }

        public SoundModel LoadSoundModel(string filename)
        {
            filename = NormalizeContentPath(filename);
            Resource resource = null;
            if (!this.m_soundModelNameIndex.TryGetValue(filename, out resource))
            {
                resource = this.m_soundModelPool.Add();
                resource.m_filename = filename;
                resource.m_refCount = 1;
                resource.m_isLoaded = false;
                this.m_soundModelNameIndex.Add(filename, resource);
            }
            else
                resource.AddRef();
            return (SoundModel)resource;
        }

        public Font LoadFont(string filename)
        {
            filename = NormalizeContentPath(filename);
            Resource resource = null;
            if (!this.m_fontNameIndex.TryGetValue(filename, out resource))
            {
                resource = this.m_fontPool.Add();
                resource.m_filename = filename;
                resource.m_refCount = 1;
                resource.m_isLoaded = false;
                this.m_fontNameIndex.Add(filename, resource);
            }
            else
                resource.AddRef();
            return (Font)resource;
        }

        public Animation2DModel LoadAnimation2DModel(string filename)
        {
            filename = NormalizeContentPath(filename);
            Resource resource = null;
            if (!this.m_animModelNameIndex.TryGetValue(filename, out resource))
            {
                resource = this.m_animModelPool.Add();
                resource.m_filename = filename;
                resource.m_refCount = 1;
                resource.m_isLoaded = false;
                this.m_animModelNameIndex.Add(filename, resource);
            }
            else
                resource.AddRef();
            return (Animation2DModel)resource;
        }

        public void LoadAllResources()
        {
            int num1 = this.BeginLoadingResources(Resource.ResourceType.Font);
            for (int resourceIndex = 0; resourceIndex < num1; ++resourceIndex)
                this.LoadResource(resourceIndex);
            this.EndLoadingResources();
            int num2 = this.BeginLoadingResources(Resource.ResourceType.Animation2DModel);
            for (int resourceIndex = 0; resourceIndex < num2; ++resourceIndex)
                this.LoadResource(resourceIndex);
            this.EndLoadingResources();
            int num3 = this.BeginLoadingResources(Resource.ResourceType.Texture);
            for (int resourceIndex = 0; resourceIndex < num3; ++resourceIndex)
                this.LoadResource(resourceIndex);
            this.EndLoadingResources();
            int num4 = this.BeginLoadingResources(Resource.ResourceType.SoundModel);
            for (int resourceIndex = 0; resourceIndex < num4; ++resourceIndex)
                this.LoadResource(resourceIndex);
            this.EndLoadingResources();
        }

        public int BeginLoadingResources(Resource.ResourceType resourceType)
        {
            this.m_numResourcesToLoad = 0;
            switch (resourceType)
            {
                case Resource.ResourceType.Texture:
                    this.GatherResourcesToLoad(this.m_textureNameIndex);
                    break;
                case Resource.ResourceType.Animation2DModel:
                    this.GatherResourcesToLoad(this.m_animModelNameIndex);
                    break;
                case Resource.ResourceType.Font:
                    this.GatherResourcesToLoad(this.m_fontNameIndex);
                    break;
                case Resource.ResourceType.SoundModel:
                    this.GatherResourcesToLoad(this.m_soundModelNameIndex);
                    break;
            }
            return this.m_numResourcesToLoad;
        }

        public void LoadResource(int resourceIndex) => this.m_resourcesToLoad[resourceIndex].Load();

        public void EndLoadingResources() => this.m_numResourcesToLoad = 0;

        public void DestroyUnusedResources()
        {
            this.DestroyUnusedResources(this.m_fontNameIndex);
            this.DestroyUnusedResources(this.m_animModelNameIndex);
            this.DestroyUnusedResources(this.m_textureNameIndex);
            this.DestroyUnusedResources(this.m_soundModelNameIndex);
        }

        private ResourceManager()
        {
            this.m_textureNameIndex = new Dictionary<string, Resource>(32);
            this.m_animModelNameIndex = new Dictionary<string, Resource>(256);
            this.m_fontNameIndex = new Dictionary<string, Resource>(4);
            this.m_soundModelNameIndex = new Dictionary<string, Resource>(64);
            this.m_resourcesToLoad = new Resource[256];
            this.m_texturePool = new Pool<Texture>(32);
            this.m_animModelPool = new Pool<Animation2DModel>(256);
            this.m_soundModelPool = new Pool<SoundModel>(64);
            this.m_fontPool = new Pool<Font>(4);
            this.m_unusedResourcesList = new List<KeyValuePair<string, Resource>>(1024);
        }

        private void Cleanup(Dictionary<string, Resource> nameIndex)
        {
            Dictionary<string, Resource>.Enumerator enumerator = nameIndex.GetEnumerator();
            while (enumerator.MoveNext())
                this.DestroyResource(enumerator.Current.Value);
            enumerator.Dispose();
            nameIndex.Clear();
        }

        private void DestroyResource(Resource resource)
        {
            Resource.ResourceType resourceType = resource.GetResourceType();
            resource.Unload();
            resource.m_refCount = 0;
            switch (resourceType)
            {
                case Resource.ResourceType.Texture:
                    this.m_texturePool.Remove((Texture)resource);
                    break;
                case Resource.ResourceType.Animation2DModel:
                    this.m_animModelPool.Remove((Animation2DModel)resource);
                    break;
                case Resource.ResourceType.Font:
                    this.m_fontPool.Remove((Font)resource);
                    break;
                case Resource.ResourceType.SoundModel:
                    this.m_soundModelPool.Remove((SoundModel)resource);
                    break;
            }
        }

        public void UnloadContentManagerAndReloadResources()
        {
            Application.Instance.Content.Unload();
            this.ReloadResources(this.m_fontNameIndex);
            this.ReloadResources(this.m_textureNameIndex);
            this.ReloadResources(this.m_animModelNameIndex);
            this.ReloadResources(this.m_soundModelNameIndex);
        }

        private void ReloadResources(Dictionary<string, Resource> nameIndex)
        {
            Dictionary<string, Resource>.Enumerator enumerator = nameIndex.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Resource resource = enumerator.Current.Value;
                if (resource.m_isLoaded)
                {
                    resource.m_isLoaded = false;
                    resource.Load();
                }
            }
            enumerator.Dispose();
        }

        private void DestroyUnusedResources(Dictionary<string, Resource> nameIndex)
        {
            this.m_unusedResourcesList.Clear();
            Dictionary<string, Resource>.Enumerator enumerator = nameIndex.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Value.GetRefCount() == 0)
                    this.m_unusedResourcesList.Add(enumerator.Current);
            }
            enumerator.Dispose();
            for (int index = 0; index < this.m_unusedResourcesList.Count; ++index)
            {
                nameIndex.Remove(this.m_unusedResourcesList[index].Key);
                this.DestroyResource(this.m_unusedResourcesList[index].Value);
            }
            this.m_unusedResourcesList.Clear();
        }

        private void GatherResourcesToLoad(Dictionary<string, Resource> nameIndex)
        {
            Dictionary<string, Resource>.Enumerator enumerator = nameIndex.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Resource resource = enumerator.Current.Value;
                if (!resource.IsLoaded() && resource.GetRefCount() > 0)
                {
                    this.m_resourcesToLoad[this.m_numResourcesToLoad] = resource;
                    ++this.m_numResourcesToLoad;
                }
            }
            enumerator.Dispose();
        }
    }
}
