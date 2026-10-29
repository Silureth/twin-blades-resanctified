using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Resanctified.Engine.Resources
{
    /// <summary>
    /// The optional HD texture pack: upscaled straight-alpha PNGs in Content/hd/&lt;key&gt;.png, listed in
    /// Content/hd/manifest.json as {"scale": 2, "generator": "...", "textures": ["textures/fx", ...]}. A key is
    /// the content path of the original texture without extension (see <see cref="ResourceManager.NormalizeContentPath"/>).
    /// Every PNG is exactly <see cref="Scale"/> times the original size, so the game keeps using the original
    /// (logical) sizes and only the sampled detail changes. Without a manifest the pack is simply unavailable.
    /// </summary>
    public sealed class HdTexturePack
    {
        public const string FOLDER_NAME = "hd";
        public const string MANIFEST_FILE_NAME = "manifest.json";
        private static readonly HdTexturePack s_instance = new HdTexturePack();
        private Manifest m_manifest;
        private string m_folder;

        public static HdTexturePack Instance => s_instance;

        public static HdTexturePack GetInstance() => s_instance;

        private HdTexturePack()
        {
        }

        public bool IsAvailable => this.m_manifest != null;

        /// <summary>How many HD pixels make one original pixel along each axis.</summary>
        public int Scale => this.m_manifest?.Scale ?? 1;

        /// <summary>Reads the manifest once at startup; any problem leaves the pack unavailable.</summary>
        public void Initialize(string contentRootDirectory)
        {
            this.m_manifest = null;
            this.m_folder = Path.Combine(contentRootDirectory, FOLDER_NAME);
            string manifestPath = Path.Combine(this.m_folder, MANIFEST_FILE_NAME);
            string json;
            try
            {
                // TitleContainer rather than File, so this also works for content packed into an Android APK.
                using (Stream stream = TitleContainer.OpenStream(manifestPath))
                using (StreamReader reader = new StreamReader(stream))
                    json = reader.ReadToEnd();
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is DirectoryNotFoundException)
            {
                Console.WriteLine("[HdTexturePack] No HD texture pack (" + manifestPath + " not found), using the original textures.");
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine("[HdTexturePack] WARNING: cannot read '" + manifestPath + "': " + ex.Message);
                return;
            }

            if (!TryParseManifest(json, out Manifest manifest))
            {
                Console.WriteLine("[HdTexturePack] WARNING: '" + manifestPath + "' is not a valid manifest, using the original textures.");
                return;
            }
            this.m_manifest = manifest;
            Console.WriteLine("[HdTexturePack] HD texture pack found: " + manifest.Textures.Count + " textures at " + manifest.Scale + "x.");
        }

        public bool Contains(string key) => this.m_manifest != null && this.m_manifest.Textures.Contains(key);

        /// <summary>
        /// Loads the HD version of a texture as a mipmapped Texture2D owned by the caller. Returns null when
        /// the pack has no such texture or its PNG is unusable (logged), so the caller can fall back to the original.
        /// </summary>
        public Texture2D TryLoadTexture(GraphicsDevice graphics, string key)
        {
            if (!this.Contains(key))
                return null;
            string path = Path.Combine(this.m_folder, key + ".png");
            try
            {
                byte[] pixels = null;
                int width;
                int height;
                // FromStream hands the decoded RGBA bytes to the processor before uploading them; that upload
                // is thrown away because the texture needs mip levels, which FromStream cannot create. A custom
                // processor also stops FromStream from blackening transparent pixels, whose colour matters for
                // straight-alpha filtering at sprite edges.
                using (Stream stream = TitleContainer.OpenStream(path))
                using (Texture2D decoded = Texture2D.FromStream(graphics, stream, data => pixels = data))
                {
                    width = decoded.Width;
                    height = decoded.Height;
                }
                if (pixels == null || pixels.Length != width * height * 4)
                    throw new InvalidDataException("unexpected pixel data size");
                if (!TryGetLogicalSize(width, height, this.m_manifest.Scale, out _, out _))
                    throw new InvalidDataException(width + "x" + height + " is not a multiple of the pack scale " + this.m_manifest.Scale);
                return CreateMipmappedTexture(graphics, width, height, pixels, this.m_manifest.Premultiplied.Contains(key));
            }
            catch (Exception ex)
            {
                Console.WriteLine("[HdTexturePack] WARNING: cannot load '" + path + "', using the original texture: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Parses manifest.json. Keys are normalized like every other content path. A scale below 2 or an
        /// empty texture list is rejected, since such a pack would change nothing. The optional
        /// "premultiplied" list is described at <see cref="Manifest.Premultiplied"/>.
        /// </summary>
        public static bool TryParseManifest(string json, out Manifest manifest)
        {
            manifest = null;
            try
            {
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;
                    if (root.ValueKind != JsonValueKind.Object
                        || !root.TryGetProperty("scale", out JsonElement scaleElement)
                        || scaleElement.ValueKind != JsonValueKind.Number
                        || !scaleElement.TryGetInt32(out int scale)
                        || scale < 2
                        || !root.TryGetProperty("textures", out JsonElement texturesElement)
                        || !TryReadKeys(texturesElement, out HashSet<string> textures)
                        || textures.Count == 0)
                        return false;
                    HashSet<string> premultiplied = new HashSet<string>();
                    if (root.TryGetProperty("premultiplied", out JsonElement premultipliedElement) && !TryReadKeys(premultipliedElement, out premultiplied))
                        return false;
                    manifest = new Manifest(scale, textures, premultiplied);
                    return true;
                }
            }
            catch (JsonException)
            {
                return false;
            }
        }

        /// <summary>The original texture size an HD image stands for; false if it is not an exact multiple.</summary>
        public static bool TryGetLogicalSize(int pixelWidth, int pixelHeight, int scale, out int width, out int height)
        {
            width = pixelWidth / scale;
            height = pixelHeight / scale;
            return width > 0 && height > 0 && pixelWidth % scale == 0 && pixelHeight % scale == 0;
        }

        private static bool TryReadKeys(JsonElement array, out HashSet<string> keys)
        {
            keys = new HashSet<string>();
            if (array.ValueKind != JsonValueKind.Array)
                return false;
            foreach (JsonElement key in array.EnumerateArray())
            {
                if (key.ValueKind != JsonValueKind.String || string.IsNullOrWhiteSpace(key.GetString()))
                    return false;
                keys.Add(ResourceManager.NormalizeContentPath(key.GetString()));
            }
            return true;
        }

        // Mip levels keep the 2x art clean when it is drawn at original size (render scale 1),
        // where plain bilinear sampling of a twice as large texture would shimmer.
        private static Texture2D CreateMipmappedTexture(GraphicsDevice graphics, int width, int height, byte[] pixels, bool premultiply)
        {
            Texture2D texture = new Texture2D(graphics, width, height, true, SurfaceFormat.Color);
            try
            {
                byte[] level = pixels;
                int levelWidth = width;
                int levelHeight = height;
                for (int levelIndex = 0; levelIndex < texture.LevelCount; ++levelIndex)
                {
                    // The next level is filtered from straight alpha, before this one is premultiplied in place.
                    byte[] nextLevel = levelIndex + 1 < texture.LevelCount ? TexturePixels.DownsampleHalf(level, levelWidth, levelHeight) : null;
                    if (premultiply)
                        TexturePixels.PremultiplyAlpha(level);
                    texture.SetData(levelIndex, null, level, 0, level.Length);
                    level = nextLevel;
                    levelWidth = TexturePixels.GetHalfSize(levelWidth);
                    levelHeight = TexturePixels.GetHalfSize(levelHeight);
                }
                return texture;
            }
            catch
            {
                texture.Dispose();
                throw;
            }
        }

        public sealed class Manifest
        {
            public Manifest(int scale, HashSet<string> textures, HashSet<string> premultiplied)
            {
                this.Scale = scale;
                this.Textures = textures;
                this.Premultiplied = premultiplied;
            }

            public int Scale { get; }

            public HashSet<string> Textures { get; }

            /// <summary>
            /// Textures whose original .xnb holds premultiplied colour (the XNA pipeline default, kept by a few
            /// Color-format textures such as fonts/charlemagnestd50_0) although the renderer blends everything
            /// as straight alpha. Their PNGs are straight like all others and are premultiplied again on load,
            /// so they blend exactly like the originals.
            /// </summary>
            public HashSet<string> Premultiplied { get; }
        }
    }
}
