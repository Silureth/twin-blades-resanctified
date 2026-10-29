using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Resanctified.Engine.Graphics
{
    /// <summary>
    /// The game was authored for a fixed 480x320 landscape screen (origin bottom-left, Y up).
    /// Every frame is rendered into a render target of exactly that size and then scaled into
    /// the real window with aspect-preserving letterboxing. Input positions are mapped back
    /// through the same transform here, so no other code needs to know the window size.
    /// The shipped WP7 build used a 520x320 back buffer and so stretched the picture about 8% wider;
    /// that is deliberately not reproduced, the art was authored for an undistorted 480x320.
    /// </summary>
    public sealed class VirtualScreen
    {
        public const int WIDTH = 480;
        public const int HEIGHT = 320;

        private static readonly VirtualScreen s_instance = new VirtualScreen();
        private GraphicsDevice m_graphics;
        private RenderTarget2D m_renderTarget;
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
            this.m_renderTarget = new RenderTarget2D(graphics, WIDTH, HEIGHT, false, SurfaceFormat.Color, DepthFormat.None, 0, RenderTargetUsage.PreserveContents);
            this.m_spriteBatch = new SpriteBatch(graphics);
            this.m_pendingScreenshotPath = null;
        }

        public void Cleanup()
        {
            this.m_spriteBatch?.Dispose();
            this.m_spriteBatch = null;
            this.m_renderTarget?.Dispose();
            this.m_renderTarget = null;
            this.m_graphics = null;
        }

        /// <summary>Redirects all game rendering into the virtual screen (viewport becomes 480x320).</summary>
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
                this.DrawToBackBuffer(destination);
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

        private void DrawToBackBuffer(Rectangle destination)
        {
            // The Renderer sets these once and relies on them staying put, so restore them after SpriteBatch.
            BlendState blendState = this.m_graphics.BlendState;
            DepthStencilState depthStencilState = this.m_graphics.DepthStencilState;
            RasterizerState rasterizerState = this.m_graphics.RasterizerState;
            SamplerState samplerState = this.m_graphics.SamplerStates[0];

            this.m_spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Opaque, ChooseSampler(destination), DepthStencilState.None, RasterizerState.CullNone);
            this.m_spriteBatch.Draw(this.m_renderTarget, destination, Color.White);
            this.m_spriteBatch.End();

            this.m_graphics.BlendState = blendState;
            this.m_graphics.DepthStencilState = depthStencilState;
            this.m_graphics.RasterizerState = rasterizerState;
            this.m_graphics.SamplerStates[0] = samplerState;
        }

        /// <summary>
        /// Saves a PNG after the current frame has been rendered: the 480x320 virtual screen, or with
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

        /// <summary>
        /// Exact integer scales (e.g. the default 960x640 window) stay crisp with point sampling.
        /// At fractional scales point sampling makes uneven pixel columns that shimmer while scrolling,
        /// and the art is painted/alpha-blended rather than strict pixel art, so linear looks better there.
        /// </summary>
        public static bool IsIntegerScale(Rectangle destination)
        {
            return destination.Width > 0 && destination.Width % WIDTH == 0 && destination.Height == destination.Width / WIDTH * HEIGHT;
        }

        private static SamplerState ChooseSampler(Rectangle destination)
        {
            return IsIntegerScale(destination) ? SamplerState.PointClamp : SamplerState.LinearClamp;
        }

        private void SaveScreenshot(string path)
        {
            using (FileStream stream = CreateScreenshotFile(path))
                this.m_renderTarget.SaveAsPng(stream, WIDTH, HEIGHT);
        }

        private void SaveWindowScreenshot(string path)
        {
            PresentationParameters pp = this.m_graphics.PresentationParameters;
            Color[] pixels = new Color[pp.BackBufferWidth * pp.BackBufferHeight];
            this.m_graphics.GetBackBufferData(pixels);
            using (Texture2D texture = new Texture2D(this.m_graphics, pp.BackBufferWidth, pp.BackBufferHeight))
            using (FileStream stream = CreateScreenshotFile(path))
            {
                texture.SetData(pixels);
                texture.SaveAsPng(stream, pp.BackBufferWidth, pp.BackBufferHeight);
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
