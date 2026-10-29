using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Resanctified.Engine.Graphics
{
    /// <summary>
    /// The game was authored for a fixed 480x320 landscape screen (origin bottom-left, Y up).
    /// All game code works in those virtual units. Every frame is rendered into a render target of
    /// that size times the render scale (e.g. 960x640 at 2x, for sharper art and text) and then scaled
    /// into the real window with aspect-preserving letterboxing. Input positions are mapped back
    /// through the same transform here, so no other code needs to know the window or target size.
    /// The shipped WP7 build used a 520x320 back buffer and so stretched the picture about 8% wider;
    /// that is deliberately not reproduced, the art was authored for an undistorted 480x320.
    /// </summary>
    public sealed class VirtualScreen
    {
        public const int WIDTH = 480;
        public const int HEIGHT = 320;
        public const int MIN_RENDER_SCALE = 1;
        public const int MAX_RENDER_SCALE = 4;

        private static readonly VirtualScreen s_instance = new VirtualScreen();
        private GraphicsDevice m_graphics;
        private RenderTarget2D m_renderTarget;
        private int m_renderScale = MIN_RENDER_SCALE;
        private SpriteBatch m_spriteBatch;
        private string m_pendingScreenshotPath;
        private bool m_pendingScreenshotIsWindow;

        public static VirtualScreen Instance => s_instance;

        public static VirtualScreen GetInstance() => s_instance;

        private VirtualScreen()
        {
        }

        public void Initialize(GraphicsDevice graphics)
        {
            this.Cleanup();
            this.m_graphics = graphics;
            // PreserveContents: the original code does not clear the screen in every state, and on
            // WP7 the back buffer kept its contents in practice. On OpenGL this costs nothing.
            // Created once: on DesktopGL it survives resizes and fullscreen toggles. A head with GL context
            // loss (Android backgrounding) must call Initialize again after the device is reset.
            this.m_renderTarget = this.CreateRenderTarget();
            this.m_spriteBatch = new SpriteBatch(graphics);
            this.m_pendingScreenshotPath = null;
        }

        public int GetRenderScale() => this.m_renderScale;

        public static int ClampRenderScale(int renderScale) => MathHelper.Clamp(renderScale, MIN_RENDER_SCALE, MAX_RENDER_SCALE);

        /// <summary>
        /// Changes the render target to WIDTH x HEIGHT times <paramref name="renderScale"/>. Before
        /// Initialize this only sets the size to create; afterwards it must not be called while drawing.
        /// </summary>
        public void SetRenderScale(int renderScale)
        {
            renderScale = ClampRenderScale(renderScale);
            if (renderScale == this.m_renderScale)
                return;
            this.m_renderScale = renderScale;
            if (this.m_renderTarget == null)
                return;
            RenderTarget2D previous = this.m_renderTarget;
            this.m_renderTarget = this.CreateRenderTarget();
            // Some screens draw over the previous frame instead of clearing it, so carry the picture over.
            this.m_graphics.SetRenderTarget(this.m_renderTarget);
            this.DrawRenderTarget(previous, new Rectangle(0, 0, this.m_renderTarget.Width, this.m_renderTarget.Height), SamplerState.LinearClamp);
            this.m_graphics.SetRenderTarget(null);
            previous.Dispose();
        }

        private RenderTarget2D CreateRenderTarget()
        {
            return new RenderTarget2D(this.m_graphics, WIDTH * this.m_renderScale, HEIGHT * this.m_renderScale, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
        }

        public void Cleanup()
        {
            this.m_spriteBatch?.Dispose();
            this.m_spriteBatch = null;
            this.m_renderTarget?.Dispose();
            this.m_renderTarget = null;
            this.m_graphics = null;
        }

        /// <summary>
        /// Redirects all game rendering into the virtual screen. The viewport becomes the whole render target;
        /// the 2D and camera projections map virtual units onto it, whatever its pixel size.
        /// </summary>
        public void BeginFrame()
        {
            if (this.m_renderTarget != null)
                this.m_graphics.SetRenderTarget(this.m_renderTarget);
        }

        /// <summary>Scales the finished virtual screen into the back buffer, with black bars around it.</summary>
        public void Present()
        {
            if (this.m_renderTarget == null)
                return;
            this.m_graphics.SetRenderTarget(null);
            if (this.m_pendingScreenshotPath != null && !this.m_pendingScreenshotIsWindow)
                this.TrySaveScreenshot(false);
            this.m_graphics.Clear(Color.Black);
            Rectangle destination = this.GetDestinationRectangle();
            if (destination.Width > 0 && destination.Height > 0)
                this.DrawRenderTarget(this.m_renderTarget, destination, this.ChooseSampler(destination));
            if (this.m_pendingScreenshotPath != null)
                this.TrySaveScreenshot(true);
        }

        // Runs inside EndDraw: a failed write (read-only folder, disk full) must not crash the game,
        // and the request is cleared first so a failure is not retried every frame.
        private void TrySaveScreenshot(bool wholeWindow)
        {
            string path = this.m_pendingScreenshotPath;
            this.m_pendingScreenshotPath = null;
            try
            {
                if (wholeWindow)
                    this.SaveWindowScreenshot(path);
                else
                    this.SaveScreenshot(path);
                Console.WriteLine("Screenshot saved: " + Path.GetFullPath(path));
            }
            catch (Exception ex)
            {
                Console.WriteLine("WARNING: could not save screenshot " + path + " (" + ex.GetType().Name + ": " + ex.Message + ")");
            }
        }

        private void DrawRenderTarget(RenderTarget2D source, Rectangle destination, SamplerState sampler)
        {
            // The Renderer sets these once and relies on them staying put, so restore them after SpriteBatch.
            BlendState blendState = this.m_graphics.BlendState;
            DepthStencilState depthStencilState = this.m_graphics.DepthStencilState;
            RasterizerState rasterizerState = this.m_graphics.RasterizerState;
            SamplerState samplerState = this.m_graphics.SamplerStates[0];

            this.m_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, sampler, DepthStencilState.None, RasterizerState.CullNone);
            this.m_spriteBatch.Draw(source, destination, Color.White);
            this.m_spriteBatch.End();

            this.m_graphics.BlendState = blendState;
            this.m_graphics.DepthStencilState = depthStencilState;
            this.m_graphics.RasterizerState = rasterizerState;
            this.m_graphics.SamplerStates[0] = samplerState;
        }

        /// <summary>
        /// Saves a PNG after the current frame has been rendered: the virtual screen at its render resolution, or with
        /// <paramref name="wholeWindow"/> the back buffer as the player sees it, letterbox bars included.
        /// </summary>
        public void RequestScreenshot(string path, bool wholeWindow = false)
        {
            this.m_pendingScreenshotPath = path;
            this.m_pendingScreenshotIsWindow = wholeWindow;
        }

        public bool HasPendingScreenshot() => this.m_pendingScreenshotPath != null;

        /// <summary>Where the virtual screen is drawn in the back buffer, in back buffer pixels.</summary>
        public Rectangle GetDestinationRectangle()
        {
            PresentationParameters pp = this.m_graphics.PresentationParameters;
            return ComputeDestinationRectangle(pp.BackBufferWidth, pp.BackBufferHeight);
        }

        /// <summary>Window (back buffer) pixel position, Y down -> virtual screen position, Y up.</summary>
        public Vector2 WindowToVirtual(Vector2 windowPos) => WindowToVirtual(windowPos, this.GetDestinationRectangle());

        /// <summary>Inverse of <see cref="WindowToVirtual(Vector2)"/>; used to inject synthetic taps.</summary>
        public Vector2 VirtualToWindow(Vector2 virtualPos) => VirtualToWindow(virtualPos, this.GetDestinationRectangle());

        /// <summary>False when the position lies in a letterbox bar (or the window is minimized).</summary>
        public bool IsInside(Vector2 windowPos) => IsInside(windowPos, this.GetDestinationRectangle());

        public static Rectangle ComputeDestinationRectangle(int backBufferWidth, int backBufferHeight)
        {
            if (backBufferWidth <= 0 || backBufferHeight <= 0)
                return Rectangle.Empty;
            int width;
            int height;
            if (backBufferWidth * HEIGHT > backBufferHeight * WIDTH)
            {
                // Window is wider than 3:2: bars left and right.
                height = backBufferHeight;
                width = (int)Math.Round((double)backBufferHeight * WIDTH / HEIGHT);
            }
            else
            {
                // Window is taller than (or exactly) 3:2: bars top and bottom.
                width = backBufferWidth;
                height = (int)Math.Round((double)backBufferWidth * HEIGHT / WIDTH);
            }
            return new Rectangle((backBufferWidth - width) / 2, (backBufferHeight - height) / 2, width, height);
        }

        public static Vector2 WindowToVirtual(Vector2 windowPos, Rectangle destination)
        {
            if (destination.Width <= 0 || destination.Height <= 0)
                return Vector2.Zero;
            float x = (windowPos.X - destination.X) * WIDTH / destination.Width;
            float yDown = (windowPos.Y - destination.Y) * HEIGHT / destination.Height;
            return new Vector2(x, HEIGHT - yDown);
        }

        public static Vector2 VirtualToWindow(Vector2 virtualPos, Rectangle destination)
        {
            float x = destination.X + virtualPos.X * destination.Width / WIDTH;
            float y = destination.Y + (HEIGHT - virtualPos.Y) * destination.Height / HEIGHT;
            return new Vector2(x, y);
        }

        public static bool IsInside(Vector2 windowPos, Rectangle destination)
        {
            return windowPos.X >= destination.Left && windowPos.X < destination.Right
                && windowPos.Y >= destination.Top && windowPos.Y < destination.Bottom;
        }

        public static Vector2 ClampToScreen(Vector2 virtualPos)
        {
            return new Vector2(MathHelper.Clamp(virtualPos.X, 0.0f, WIDTH), MathHelper.Clamp(virtualPos.Y, 0.0f, HEIGHT));
        }

        /// <summary>Whether <paramref name="destination"/> is an exact integer magnification of a 480x320 picture.</summary>
        public static bool IsIntegerScale(Rectangle destination) => IsIntegerScale(destination, WIDTH, HEIGHT);

        /// <summary>
        /// Exact integer magnifications (e.g. a 960x640 target in a 1920x1280 window) stay crisp with point
        /// sampling. At fractional scales point sampling makes uneven pixel columns that shimmer while
        /// scrolling, and the art is painted/alpha-blended rather than strict pixel art, so linear looks
        /// better there. Shrinking (a 2x target in a small window) also needs linear, to average the pixels.
        /// </summary>
        public static bool IsIntegerScale(Rectangle destination, int sourceWidth, int sourceHeight)
        {
            return destination.Width >= sourceWidth && destination.Width % sourceWidth == 0
                && destination.Height == destination.Width / sourceWidth * sourceHeight;
        }

        private SamplerState ChooseSampler(Rectangle destination)
        {
            return IsIntegerScale(destination, this.m_renderTarget.Width, this.m_renderTarget.Height) ? SamplerState.PointClamp : SamplerState.LinearClamp;
        }

        private void SaveScreenshot(string path)
        {
            Color[] pixels = new Color[this.m_renderTarget.Width * this.m_renderTarget.Height];
            this.m_renderTarget.GetData(pixels);
            this.SaveOpaquePng(path, pixels, this.m_renderTarget.Width, this.m_renderTarget.Height);
        }

        private void SaveWindowScreenshot(string path)
        {
            PresentationParameters pp = this.m_graphics.PresentationParameters;
            Color[] pixels = new Color[pp.BackBufferWidth * pp.BackBufferHeight];
            this.m_graphics.GetBackBufferData(pixels);
            this.SaveOpaquePng(path, pixels, pp.BackBufferWidth, pp.BackBufferHeight);
        }

        // The game's non-premultiplied blending leaves alpha below 255 in the target (e.g. under the dark
        // help overlay). The window ignores it, but image viewers would show the picture washed out.
        private void SaveOpaquePng(string path, Color[] pixels, int width, int height)
        {
            for (int index = 0; index < pixels.Length; ++index)
                pixels[index].A = byte.MaxValue;
            using (Texture2D texture = new Texture2D(this.m_graphics, width, height))
            using (FileStream stream = CreateScreenshotFile(path))
            {
                texture.SetData(pixels);
                texture.SaveAsPng(stream, width, height);
            }
        }

        private static FileStream CreateScreenshotFile(string path)
        {
            string directory = Path.GetDirectoryName(Path.GetFullPath(path));
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);
            return File.Create(path);
        }
    }
}
