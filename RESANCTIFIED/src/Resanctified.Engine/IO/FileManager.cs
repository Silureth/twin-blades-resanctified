using System;
using System.IO;

namespace Resanctified.Engine.IO
{
    /// <summary>
    /// Reads and writes the small save files (settings, progress, highscores) in one per-user folder.
    /// Replaces the Windows Phone IsolatedStorage, which modern .NET desktop does not support.
    /// </summary>
    public class FileManager
    {
        private const string TEMP_FILE_SUFFIX = ".tmp";
        private static readonly FileManager s_instance = new FileManager();
        private string m_baseDirectory;

        public static FileManager Instance => s_instance;

        /// <summary>
        /// Folder that holds the save files. Defaults to a folder named by the game in the per-user
        /// local app data folder (e.g. ~/.local/share/&lt;name&gt;, %LOCALAPPDATA%\&lt;name&gt;). A platform head
        /// such as Android can set its own folder before Initialize is called.
        /// </summary>
        public string BaseDirectory
        {
            get => this.m_baseDirectory;
            set => this.m_baseDirectory = value;
        }

        private FileManager()
        {
        }

        /// <param name="appFolderName">Name of the default save folder; the game owns its identity, not the engine.</param>
        public void Initialize(string appFolderName)
        {
            if (string.IsNullOrEmpty(this.m_baseDirectory))
                this.m_baseDirectory = GetDefaultBaseDirectory(appFolderName);
            try
            {
                Directory.CreateDirectory(this.m_baseDirectory);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FileManager] WARNING: cannot create save folder '" + this.m_baseDirectory + "': " + ex.Message);
            }
        }

        public void Cleanup()
        {
        }

        public bool FileExists(string path) => File.Exists(this.GetFullPath(path));

        /// <summary>Returns the whole file, or null if it does not exist or cannot be read (logged).</summary>
        public byte[] ReadFile(string path)
        {
            string fullPath = this.GetFullPath(path);
            if (!File.Exists(fullPath))
                return null;
            try
            {
                return File.ReadAllBytes(fullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FileManager] WARNING: cannot read '" + fullPath + "': " + ex.Message);
                return null;
            }
        }

        public bool ReadFile(string path, byte[] buffer) => this.ReadFile(path, buffer, 0, buffer.Length);

        public bool ReadFile(string path, byte[] buffer, int offset, int count)
        {
            byte[] data = this.ReadFile(path);
            if (data == null || data.Length < count)
                return false;
            Array.Copy(data, 0, buffer, offset, count);
            return true;
        }

        public bool WriteFile(string path, byte[] buffer) => this.WriteFile(path, buffer, 0, buffer.Length);

        /// <summary>
        /// Writes to a temporary file first and then renames it over the target, so a crash or
        /// power loss mid-write leaves the previous save intact instead of a truncated one.
        /// </summary>
        public bool WriteFile(string path, byte[] buffer, int offset, int count)
        {
            string fullPath = this.GetFullPath(path);
            string tempPath = fullPath + TEMP_FILE_SUFFIX;
            try
            {
                Directory.CreateDirectory(this.m_baseDirectory);
                using (FileStream stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    stream.Write(buffer, offset, count);
                    stream.Flush(true);
                }
                File.Move(tempPath, fullPath, true);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FileManager] WARNING: cannot write '" + fullPath + "': " + ex.Message);
                TryDelete(tempPath);
                return false;
            }
        }

        public bool DeleteFile(string path)
        {
            string fullPath = this.GetFullPath(path);
            if (!File.Exists(fullPath))
                return true;
            return TryDelete(fullPath);
        }

        private string GetFullPath(string path)
        {
            if (string.IsNullOrEmpty(this.m_baseDirectory))
                throw new InvalidOperationException("FileManager.Initialize must be called before accessing files.");
            return Path.Combine(this.m_baseDirectory, path);
        }

        private static string GetDefaultBaseDirectory(string appFolderName)
        {
            string root = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            // Can be empty on minimal Linux setups without HOME; keep saves next to the game then.
            if (string.IsNullOrEmpty(root))
                root = AppContext.BaseDirectory;
            return Path.Combine(root, appFolderName);
        }

        private static bool TryDelete(string fullPath)
        {
            try
            {
                File.Delete(fullPath);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[FileManager] WARNING: cannot delete '" + fullPath + "': " + ex.Message);
                return false;
            }
        }
    }
}
