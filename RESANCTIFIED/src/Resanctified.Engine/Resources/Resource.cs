// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Resources.Resource
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

namespace Resanctified.Engine.Resources
{
  public abstract class Resource
  {
    internal string m_filename;
    internal int m_refCount;
    internal Resource.ResourceType m_type;
    internal bool m_isLoaded;

    internal Resource(Resource.ResourceType type)
    {
      this.m_type = type;
      this.m_refCount = 0;
      this.m_isLoaded = false;
      this.m_filename = "";
    }

    internal Resource.ResourceType GetResourceType() => this.m_type;

    public string GetResourceFilename() => this.m_filename;

    public bool IsLoaded() => this.m_isLoaded;

    public int GetRefCount() => this.m_refCount;

    public void AddRef() => ++this.m_refCount;

    public void Release() => --this.m_refCount;

    public static void SafeRelease<T>(ref T r) where T : Resource
    {
      if ((object) r == null)
        return;
      r.Release();
      r = default (T);
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
