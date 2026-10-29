// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Resources.SoundModel
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework.Audio;

namespace Resanctified.Engine.Resources
{
  public sealed class SoundModel : Resource
  {
    private SoundEffect m_soundEffect;

    public SoundModel()
      : base(Resource.ResourceType.SoundModel)
    {
    }

    internal override void Load()
    {
      if (this.m_isLoaded)
        return;
      this.m_soundEffect = Application.Instance.Content.Load<SoundEffect>(this.m_filename);
      this.m_isLoaded = true;
    }

    internal override void Unload()
    {
      if (!this.m_isLoaded)
        return;
      if (this.m_soundEffect != null)
        this.m_soundEffect = (SoundEffect) null;
      this.m_isLoaded = false;
    }

    internal SoundEffect GetSoundEffect() => this.m_soundEffect;
  }
}
