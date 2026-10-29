using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Resanctified.Engine.IO;
using Resanctified.Engine.Resources;

namespace Resanctified.Engine.Graphics
{
    /// <summary>
    /// The player's graphics choices, saved as graphics.json next to the save files. The setters apply a
    /// change immediately (textures reload, the render target is recreated), so an options menu only has
    /// to call a setter and then <see cref="Save"/>.
    /// </summary>
    public sealed class GraphicsSettings
    {
        public const string FILE_NAME = "graphics.json";
        // HD falls back to the original art by itself when the HD pack is not installed.
        public const TextureSet DEFAULT_TEXTURE_SET = TextureSet.HD;
        // 2x even with the original art: fonts, edges and scaled sprites come out noticeably sharper.
        public const int DEFAULT_RENDER_SCALE = 2;
        private const string TEXTURE_SET_KEY = "textureSet";
        private const string RENDER_SCALE_KEY = "renderScale";
        private static readonly GraphicsSettings s_instance = new GraphicsSettings();
        private TextureSet m_textureSet;
        private int m_renderScale;

        public static GraphicsSettings Instance => s_instance;

        public static GraphicsSettings GetInstance() => s_instance;

        private GraphicsSettings()
        {
            this.m_textureSet = DEFAULT_TEXTURE_SET;
            this.m_renderScale = DEFAULT_RENDER_SCALE;
        }

        public TextureSet TextureSet => this.m_textureSet;

        /// <summary>Internal render resolution as a multiple of the 480x320 virtual screen.</summary>
        public int RenderScale => this.m_renderScale;

        public void SetTextureSet(TextureSet textureSet)
        {
            this.m_textureSet = textureSet;
            ResourceManager.Instance.SetTextureSet(textureSet);
        }

        public void SetRenderScale(int renderScale)
        {
            this.m_renderScale = VirtualScreen.ClampRenderScale(renderScale);
            VirtualScreen.Instance.SetRenderScale(this.m_renderScale);
        }

        /// <summary>
        /// Reads graphics.json and applies it; call after FileManager.Initialize and before the graphics
        /// are initialized, so nothing has to be created twice. A missing or corrupt file gives the defaults.
        /// </summary>
        public void Load()
        {
            byte[] data = FileManager.Instance.ReadFile(FILE_NAME);
            TextureSet textureSet = DEFAULT_TEXTURE_SET;
            int renderScale = DEFAULT_RENDER_SCALE;
            if (data != null)
                ParseJson(Encoding.UTF8.GetString(data), out textureSet, out renderScale);
            this.SetTextureSet(textureSet);
            this.SetRenderScale(renderScale);
        }

        public bool Save() => FileManager.Instance.WriteFile(FILE_NAME, Encoding.UTF8.GetBytes(ToJson(this.m_textureSet, this.m_renderScale)));

        public static string ToJson(TextureSet textureSet, int renderScale)
        {
            JsonObject json = new JsonObject
            {
                [TEXTURE_SET_KEY] = textureSet.ToString(),
                [RENDER_SCALE_KEY] = renderScale,
            };
            return json.ToJsonString(new JsonSerializerOptions { WriteIndented = true });
        }

        /// <summary>Every value that is missing or invalid (or the whole file, if corrupt) gets its default.</summary>
        public static void ParseJson(string json, out TextureSet textureSet, out int renderScale)
        {
            textureSet = DEFAULT_TEXTURE_SET;
            renderScale = DEFAULT_RENDER_SCALE;
            try
            {
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;
                    if (root.ValueKind != JsonValueKind.Object)
                        return;
                    if (root.TryGetProperty(TEXTURE_SET_KEY, out JsonElement textureSetElement)
                        && textureSetElement.ValueKind == JsonValueKind.String
                        && Enum.TryParse(textureSetElement.GetString(), true, out TextureSet parsedTextureSet)
                        && Enum.IsDefined(parsedTextureSet))
                        textureSet = parsedTextureSet;
                    if (root.TryGetProperty(RENDER_SCALE_KEY, out JsonElement renderScaleElement)
                        && renderScaleElement.ValueKind == JsonValueKind.Number
                        && renderScaleElement.TryGetInt32(out int parsedRenderScale))
                        renderScale = VirtualScreen.ClampRenderScale(parsedRenderScale);
                }
            }
            catch (JsonException)
            {
                Console.WriteLine("[GraphicsSettings] WARNING: " + FILE_NAME + " is corrupt, using the default graphics settings.");
            }
        }
    }
}
