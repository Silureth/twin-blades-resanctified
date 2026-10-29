// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Sounds.AudioManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

namespace Resanctified.Engine.Sounds
{
  public sealed class AudioManager
  {
    private const int AUDIOMANAGER_SOUNDLIST_CAPACITY_DEFAULT = 1024;
    public const float AUDIOMANAGER_LISTENER_GAIN_DEFAULT = 1f;
    public const float AUDIOMANAGER_MUSIC_GAIN_DEFAULT = 1f;
    private static readonly AudioManager s_instance = new AudioManager();
    private List<Sound> m_soundList;
    private Song m_music;
    private string m_musicFilename;
    private int m_musicLoopCount;
    private bool m_gameHasControl;
    private AudioManager.MusicState m_musicState;

    public static AudioManager Instance => AudioManager.s_instance;

    public static AudioManager GetInstance() => AudioManager.s_instance;

    private AudioManager() => this.m_soundList = new List<Sound>(1024);

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
      this.m_gameHasControl = MediaPlayer.GameHasControl;
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

    public void SetListenerGain(float gain) => SoundEffect.MasterVolume = gain;

    public float GetListenerGain() => SoundEffect.MasterVolume;

    public void RegisterSound(Sound sound) => this.m_soundList.Add(sound);

    public void UnregisterSound(Sound sound) => this.m_soundList.Remove(sound);

    public void LoadMusic(string filename) => this.LoadMusic(filename, true);

    public void LoadMusic(string filename, bool streaming)
    {
      this.UnloadMusic();
      if (this.m_gameHasControl)
      {
        try
        {
          this.m_music = Application.Instance.Content.Load<Song>(filename);
          this.m_musicFilename = filename;
          this.m_musicState = AudioManager.MusicState.Stopped;
          this.SetMusicLoopCount(-1);
        }
        catch (Exception ex)
        {
          this.m_music = (Song) null;
          this.m_musicFilename = (string) null;
          this.m_musicState = AudioManager.MusicState.Stopped;
        }
      }
      else
      {
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
        {
          try
          {
            MediaPlayer.Stop();
          }
          catch (Exception ex)
          {
          }
        }
      }
      this.m_music = (Song) null;
      this.m_musicFilename = (string) null;
      this.m_musicState = AudioManager.MusicState.Stopped;
    }

    public string GetMusicFilename() => this.m_musicFilename;

    public void SetMusicGain(float gain)
    {
      try
      {
        MediaPlayer.Volume = gain;
      }
      catch (Exception ex)
      {
      }
    }

    public float GetMusicGain()
    {
      float musicGain = 1f;
      try
      {
        musicGain = MediaPlayer.Volume;
      }
      catch (Exception ex)
      {
      }
      return musicGain;
    }

    public void SetMusicLoopCount(int loopCount)
    {
      if (this.m_gameHasControl)
      {
        try
        {
          MediaPlayer.IsRepeating = loopCount < 0;
        }
        catch (Exception ex)
        {
        }
      }
      this.m_musicLoopCount = loopCount;
    }

    public void PlayMusic()
    {
      if (this.m_gameHasControl)
      {
        try
        {
          if (!(this.m_music != (Song) null))
            return;
          MediaPlayer.Play(this.m_music);
          this.m_musicState = AudioManager.MusicState.Playing;
        }
        catch (Exception ex)
        {
        }
      }
      else
        this.m_musicState = AudioManager.MusicState.Playing;
    }

    public void PauseMusic()
    {
      if (this.m_gameHasControl)
      {
        try
        {
          MediaPlayer.Pause();
          this.m_musicState = AudioManager.MusicState.Paused;
        }
        catch (Exception ex)
        {
        }
      }
      else
        this.m_musicState = AudioManager.MusicState.Paused;
    }

    public void StopMusic()
    {
      if (this.m_gameHasControl)
      {
        try
        {
          MediaPlayer.Stop();
          this.m_musicState = AudioManager.MusicState.Stopped;
        }
        catch (Exception ex)
        {
        }
      }
      else
        this.m_musicState = AudioManager.MusicState.Stopped;
    }

    public void ResumeMusic()
    {
      if (this.m_gameHasControl)
      {
        try
        {
          MediaPlayer.Resume();
          this.m_musicState = AudioManager.MusicState.Playing;
        }
        catch (Exception ex)
        {
        }
      }
      else
        this.m_musicState = AudioManager.MusicState.Playing;
    }

    public bool IsOtherAudioPlaying() => !this.m_gameHasControl;

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

    private enum MusicState
    {
      Stopped,
      Playing,
      Paused,
    }
  }
}
