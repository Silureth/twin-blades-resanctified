// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.IO.FileManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace PSS.GameEngine.IO
{
  public class FileManager
  {
    private static readonly FileManager s_instance = new FileManager();
    private IsolatedStorageFile m_isoFile;

    public static FileManager Instance => FileManager.s_instance;

    private FileManager()
    {
    }

    public void Initialize()
    {
      try
      {
        this.m_isoFile = IsolatedStorageFile.GetUserStoreForApplication();
      }
      catch (Exception ex)
      {
      }
    }

    public void Cleanup()
    {
      if (this.m_isoFile == null)
        return;
      try
      {
        this.m_isoFile.Dispose();
      }
      catch (Exception ex)
      {
      }
    }

    public bool ReadFile(string path, byte[] buffer) => this.ReadFile(path, buffer, 0, buffer.Length);

    public bool ReadFile(string path, byte[] buffer, int offset, int count)
    {
      IsolatedStorageFileStream storageFileStream = (IsolatedStorageFileStream) null;
      try
      {
        storageFileStream = this.m_isoFile.OpenFile(path, FileMode.Open);
        return storageFileStream.Read(buffer, offset, count) == count;
      }
      catch (Exception ex)
      {
        return false;
      }
      finally
      {
        storageFileStream?.Close();
      }
    }

    public bool WriteFile(string path, byte[] buffer) => this.WriteFile(path, buffer, 0, buffer.Length);

    public bool WriteFile(string path, byte[] buffer, int offset, int count)
    {
      IsolatedStorageFileStream storageFileStream = (IsolatedStorageFileStream) null;
      try
      {
        storageFileStream = this.m_isoFile.OpenFile(path, FileMode.Create);
        storageFileStream.Write(buffer, offset, count);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
      finally
      {
        storageFileStream?.Close();
      }
    }
  }
}
