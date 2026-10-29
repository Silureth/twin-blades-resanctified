// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Sounds.AudioManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Resanctified.Engine.Resources;
using System;
using System.Collections.Generic;
using System.IO;

namespace Resanctified.Engine.Sounds
{
  // Owns the registered sounds and the single background music track.
  // All MonoGame audio calls go through here (or SoundModel/Sound, which ask IsAvailable),
  // so the game keeps running on machines without an audio device: audio calls just do nothing.
  public sealed class AudioManager
  {
    private const int AUDIOMANAGER_SOUNDLIST_CAPACITY_DEFAULT = 1024;
    public const float AUDIOMANAGER_LISTENER_GAIN_DEFAULT = 1f;
    public const float AUDIOMANAGER_MUSIC_GAIN_DEFAULT = 1f;
    // DesktopGL can only stream Ogg Vorbis music. The original Song .xnb files pointed at .wma files,
    // so the music was converted to .ogg and is loaded directly from the content folder.
    private const string MUSIC_FILE_EXTENSION = ".ogg";
    private static readonly AudioManager s_instance = new AudioManager();
    private List<Sound> m_soundList;
    private Song m_music;
    private string m_musicFilename;
    private int m_musicLoopCount;
    private bool m_gameHasControl;
    private float m_listenerGain;
    private float m_musicGain;
    private bool? m_isAvailable;
    private AudioManager.MusicState m_musicState;

    public static AudioManager Instance => AudioManager.s_instance;

    public static AudioManager GetInstance() => AudioManager.s_instance;

    private AudioManager() => this.m_soundList = new List<Sound>(1024);

    // True when an audio device could be opened. Checked lazily on first use, so it does not matter
    // whether a sound, the music or Initialize() touches audio first.
    public bool IsAvailable
    {
      get
      {
        if (!this.m_isAvailable.HasValue)
          this.m_isAvailable = AudioManager.OpenAudioDevice();
        return this.m_isAvailable.Value;
      }
    }

    public void Initialize()
    {
      this.m_musicFilename = (string) null;
      this.m_musicLoopCount = -1;
      this.m_musicState = AudioManager.MusicState.Stopped;
      this.Initialize(true);
    }

    public void Initialize(bool mixWithOtherAudio)
    {
      this.SetListenerGain(1f);
      this.SetMusicGain(1f);
      // Without a device nothing else can be playing through us; reporting "no control" would make
      // the main menu show the "your music is playing" prompt.
      this.m_gameHasControl = !this.IsAvailable || MediaPlayer.GameHasControl;
    }

    public void Cleanup()
    {
      for (int index = 0; index < this.m_soundList.Count; ++index)
        this.m_soundList[index].Cleanup();
      this.m_soundList.Clear();
      this.UnloadMusic();
    }

    public void Update()
    {
      if (!this.IsAvailable)
        return;
      bool gameHasControl1 = MediaPlayer.GameHasControl;
      bool gameHasControl2 = this.m_gameHasControl;
      this.m_gameHasControl = gameHasControl1;
      if (!gameHasControl1 || gameHasControl2)
        return;
      if (this.m_music != (Song) null)
      {
        this.SetMusicLoopCount(this.m_musicLoopCount);
        this.PlayMusic();
      }
      else
      {
        if (this.m_musicFilename == null)
          return;
        bool flag = this.m_musicState == AudioManager.MusicState.Playing;
        this.LoadMusic(this.m_musicFilename);
        this.SetMusicLoopCount(this.m_musicLoopCount);
        if (!flag)
          return;
        this.PlayMusic();
      }
    }

    public void SetListenerGain(float gain)
    {
      this.m_listenerGain = gain;
      if (this.IsAvailable)
        SoundEffect.MasterVolume = gain;
    }

    public float GetListenerGain() => this.m_listenerGain;

    public void RegisterSound(Sound sound) => this.m_soundList.Add(sound);

    public void UnregisterSound(Sound sound) => this.m_soundList.Remove(sound);

    public void LoadMusic(string filename) => this.LoadMusic(filename, true);

    // Music is always streamed; 'streaming' is kept for API compatibility with the original game code.
    public void LoadMusic(string filename, bool streaming)
    {
      this.UnloadMusic();
      if (this.IsAvailable && this.m_gameHasControl)
      {
        this.m_music = AudioManager.LoadSong(filename);
        if (this.m_music != (Song) null)
        {
          this.m_musicFilename = filename;
          this.m_musicState = AudioManager.MusicState.Stopped;
          this.SetMusicLoopCount(-1);
        }
      }
      else
      {
        // Only remember the request; Update() loads it if the game gets control of the media player.
        this.m_musicFilename = filename;
        this.m_musicLoopCount = -1;
        this.m_musicState = AudioManager.MusicState.Stopped;
      }
    }

    public void UnloadMusic()
    {
      if (this.m_music != (Song) null)
      {
        if (this.m_gameHasControl)
          MediaPlayer.Stop();
        // Songs from Song.FromUri are not owned by the ContentManager; dispose to free the OpenAL stream.
        this.m_music.Dispose();
        this.m_music = (Song) null;
      }
      this.m_musicFilename = (string) null;
      this.m_musicState = AudioManager.MusicState.Stopped;
    }

    public string GetMusicFilename() => this.m_musicFilename;

    public void SetMusicGain(float gain)
    {
      this.m_musicGain = gain;
      if (this.IsAvailable)
        MediaPlayer.Volume = gain;
    }

    public float GetMusicGain() => this.m_musicGain;

    public void SetMusicLoopCount(int loopCount)
    {
      if (this.IsAvailable && this.m_gameHasControl)
        MediaPlayer.IsRepeating = loopCount < 0;
      this.m_musicLoopCount = loopCount;
    }

    public void PlayMusic()
    {
      if (this.IsAvailable && this.m_gameHasControl)
      {
        if (this.m_music == (Song) null)
          return;
        MediaPlayer.Play(this.m_music);
      }
      this.m_musicState = AudioManager.MusicState.Playing;
    }

    public void PauseMusic()
    {
      if (this.IsAvailable && this.m_gameHasControl)
        MediaPlayer.Pause();
      this.m_musicState = AudioManager.MusicState.Paused;
    }

    public void StopMusic()
    {
      if (this.IsAvailable && this.m_gameHasControl)
        MediaPlayer.Stop();
      this.m_musicState = AudioManager.MusicState.Stopped;
    }

    public void ResumeMusic()
    {
      if (this.IsAvailable && this.m_gameHasControl)
        MediaPlayer.Resume();
      this.m_musicState = AudioManager.MusicState.Playing;
    }

    public void PauseAll()
    {
      this.PauseMusic();
      for (int index = 0; index < this.m_soundList.Count; ++index)
        this.m_soundList[index].Pause();
    }

    public void ResumeAll()
    {
      this.ResumeMusic();
      for (int index = 0; index < this.m_soundList.Count; ++index)
        this.m_soundList[index].Resume();
    }

    public void StopAll()
    {
      this.StopMusic();
      for (int index = 0; index < this.m_soundList.Count; ++index)
        this.m_soundList[index].Stop();
    }

    private static bool OpenAudioDevice()
    {
      try
      {
        // MonoGame would otherwise open the device on the first SoundEffect/Song load and throw there
        // (NoAudioHardwareException from OpenAL, DllNotFoundException, ...). Do it once, up front.
        SoundEffect.Initialize();
        return true;
      }
      catch (Exception ex)
      {
        Console.WriteLine("[Audio] WARNING: no audio device available, sound and music are disabled. (" + ex.GetType().Name + ": " + ex.Message + ")");
        return false;
      }
    }

    // Returns null (and logs) if the music file is missing or broken; music is not worth crashing for.
    // TODO(Android): this reads a loose file next to the executable. Inside an APK the content is an
    // asset, so File.Exists fails there; that head will need to load songs through TitleContainer instead.
    private static Song LoadSong(string filename)
    {
      string assetName = ResourceManager.NormalizeContentPath(filename);
      // Song.FromUri hands the path straight to the Ogg reader, which would resolve a relative path against
      // the working directory, so build an absolute path inside the content folder.
      string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, Application.Instance.Content.RootDirectory, assetName + MUSIC_FILE_EXTENSION));
      if (!File.Exists(path))
      {
        Console.WriteLine("[Audio] WARNING: music file not found: " + path);
        return (Song) null;
      }
      try
      {
        return Song.FromUri(assetName, new Uri(path));
      }
      catch (Exception ex)
      {
        Console.WriteLine("[Audio] WARNING: could not load music " + path + " (" + ex.GetType().Name + ": " + ex.Message + ")");
        return (Song) null;
      }
    }

    private enum MusicState
    {
      Stopped,
      Playing,
      Paused,
    }
  }
}
