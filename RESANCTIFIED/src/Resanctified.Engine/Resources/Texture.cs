// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Resources.Texture
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework.Graphics;

namespace Resanctified.Engine.Resources
{
  public sealed class Texture : Resource
  {
    internal Texture2D m_xnaTexture;

    public Texture()
      : base(Resource.ResourceType.Texture)
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
        this.m_xnaTexture = (Texture2D) null;
      this.m_isLoaded = false;
    }
  }
}
