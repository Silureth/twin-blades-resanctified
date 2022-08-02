// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Sounds.Sound
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework.Audio;
using PSS.GameEngine.Resources;

namespace PSS.GameEngine.Sounds
{
  public sealed class Sound
  {
    private const float SOUND_GAIN_DEFAULT = 1f;
    private const bool SOUND_LOOPING_DEFAULT = false;
    private SoundModel m_model;
    private SoundEffectInstance m_instance;
    private bool m_looping;
    private float m_gain;

    public Sound()
    {
      this.m_looping = false;
      this.m_gain = 1f;
    }

    public void Initialize(string filename, bool registerSound)
    {
      this.Initialize(ResourceManager.Instance.LoadSoundModel(filename), registerSound);
      this.m_model.Release();
    }

    public void Initialize(SoundModel model, bool registerSound)
    {
      Resource.SafeRelease<SoundModel>(ref this.m_model);
      this.m_model = model;
      this.m_model.AddRef();
      this.SetLooping(false);
      this.SetGain(1f);
      if (!registerSound)
        return;
      AudioManager.Instance.RegisterSound(this);
    }

    public void Cleanup()
    {
      Resource.SafeRelease<SoundModel>(ref this.m_model);
      if (this.m_instance != null)
      {
        if (!this.m_instance.IsDisposed)
          this.m_instance.Dispose();
        this.m_instance = (SoundEffectInstance) null;
      }
      AudioManager.Instance.UnregisterSound(this);
    }

    public void Play()
    {
      if (this.m_instance != null)
      {
        if (!this.m_instance.IsDisposed)
          this.m_instance.Dispose();
        this.m_instance = (SoundEffectInstance) null;
      }
      this.m_instance = this.m_model.GetSoundEffect().CreateInstance();
      this.m_instance.IsLooped = this.m_looping;
      this.m_instance.Volume = this.m_gain;
      this.m_instance.Play();
    }

    public void Stop()
    {
      if (this.m_instance == null || this.m_instance.IsDisposed)
        return;
      this.m_instance.Stop();
    }

    public void Rewind()
    {
      if (this.m_instance == null || this.m_instance.IsDisposed)
        return;
      SoundState state = this.m_instance.State;
      this.m_instance.Stop();
      if (state != SoundState.Playing)
        return;
      this.Play();
    }

    public void Pause()
    {
      if (this.m_instance == null || this.m_instance.IsDisposed)
        return;
      this.m_instance.Pause();
    }

    public void Resume()
    {
      if (this.m_instance == null || this.m_instance.IsDisposed || this.m_instance.State != SoundState.Paused)
        return;
      this.m_instance.Resume();
    }

    public void SetLooping(bool looping)
    {
      if (this.m_instance != null && !this.m_instance.IsDisposed)
        this.m_instance.IsLooped = looping;
      this.m_looping = looping;
    }

    public bool IsLooping() => this.m_looping;

    public void SetGain(float gain)
    {
      if (this.m_instance != null && !this.m_instance.IsDisposed)
        this.m_instance.Volume = gain;
      this.m_gain = gain;
    }

    public float GetGain() => this.m_gain;
  }
}
