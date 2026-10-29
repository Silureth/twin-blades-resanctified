using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.IO;
using System;
using System.IO;

namespace Resanctified.Desktop
{
    /// <summary>
    /// Desktop-only behaviour: a resizable window, fullscreen toggle (F11 / Alt+Enter),
    /// F12 screenshots, and running the <see cref="DesktopDebugOptions"/> script.
    /// </summary>
    public sealed class DesktopHost : GameComponent
    {
        private const int DEFAULT_WINDOW_SCALE = 2;
        private const string SCREENSHOT_DIRECTORY = "screenshots";
        private readonly GraphicsDeviceManager m_graphics;
        private readonly DesktopDebugOptions m_debugOptions;
        private Point m_windowedSize;
        private KeyboardState m_previousKeyboard;
        private bool m_applyingGraphicsChanges;
        private int m_nextClickIndex;
        private int m_nextCaptureIndex;

        public DesktopHost(Microsoft.Xna.Framework.Game game, DesktopDebugOptions debugOptions)
            : base(game)
        {
            this.m_graphics = (GraphicsDeviceManager)game.Services.GetService(typeof(IGraphicsDeviceManager));
            this.m_debugOptions = debugOptions;
            this.m_windowedSize = debugOptions.WindowSize ?? new Point(VirtualScreen.WIDTH * DEFAULT_WINDOW_SCALE, VirtualScreen.HEIGHT * DEFAULT_WINDOW_SCALE);
            this.m_graphics.PreferredBackBufferWidth = this.m_windowedSize.X;
            this.m_graphics.PreferredBackBufferHeight = this.m_windowedSize.Y;
            this.m_graphics.IsFullScreen = false;
            // Borderless fullscreen: no display mode change, so toggling is instant and safe.
            this.m_graphics.HardwareModeSwitch = false;
            game.IsMouseVisible = true;
            game.Window.Title = "Resanctified";
            game.Window.AllowUserResizing = true;
            game.Window.ClientSizeChanged += this.OnClientSizeChanged;
        }

        public override void Update(GameTime gameTime)
        {
            this.UpdateKeyboard();
            this.UpdateDebugScript(gameTime.TotalGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.Game.Window.ClientSizeChanged -= this.OnClientSizeChanged;
            base.Dispose(disposing);
        }

        private void UpdateKeyboard()
        {
            KeyboardState keyboard = Keyboard.GetState();
            bool alt = keyboard.IsKeyDown(Keys.LeftAlt) || keyboard.IsKeyDown(Keys.RightAlt);
            if (this.IsKeyPressed(keyboard, Keys.F11) || alt && this.IsKeyPressed(keyboard, Keys.Enter))
                this.ToggleFullScreen();
            if (this.IsKeyPressed(keyboard, Keys.F12))
            {
                // Next to the save files: the working directory may be read-only or inside the source tree.
                string path = Path.Combine(FileManager.Instance.BaseDirectory, SCREENSHOT_DIRECTORY, "screenshot_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".png");
                VirtualScreen.Instance.RequestScreenshot(path);
            }
            this.m_previousKeyboard = keyboard;
        }

        private bool IsKeyPressed(KeyboardState keyboard, Keys key) => keyboard.IsKeyDown(key) && this.m_previousKeyboard.IsKeyUp(key);

        private void UpdateDebugScript(double time)
        {
            while (this.m_nextClickIndex < this.m_debugOptions.Clicks.Count && this.m_debugOptions.Clicks[this.m_nextClickIndex].Seconds <= time)
            {
                Vector2 position = this.m_debugOptions.Clicks[this.m_nextClickIndex++].Position;
                Resanctified.Engine.Application.Instance.InjectTap(position);
                Console.WriteLine($"[{time:0.00}s] click {position.X},{position.Y}");
            }

            // Only one screenshot can be pending per frame; later ones wait for the next frame.
            if (this.m_nextCaptureIndex < this.m_debugOptions.Captures.Count
                && this.m_debugOptions.Captures[this.m_nextCaptureIndex].Seconds <= time
                && !VirtualScreen.Instance.HasPendingScreenshot())
            {
                DesktopDebugOptions.Capture capture = this.m_debugOptions.Captures[this.m_nextCaptureIndex++];
                VirtualScreen.Instance.RequestScreenshot(capture.FilePath, capture.WholeWindow);
                Console.WriteLine($"[{time:0.00}s] capture {Path.GetFullPath(capture.FilePath)}");
            }

            bool capturesDone = this.m_nextCaptureIndex >= this.m_debugOptions.Captures.Count && !VirtualScreen.Instance.HasPendingScreenshot();
            if (this.m_debugOptions.ExitAfterSeconds.HasValue && time >= this.m_debugOptions.ExitAfterSeconds.Value && capturesDone)
            {
                Console.WriteLine($"[{time:0.00}s] exit");
                this.Game.Exit();
            }
        }

        private void ToggleFullScreen()
        {
            if (this.m_graphics.IsFullScreen)
            {
                this.m_graphics.IsFullScreen = false;
                this.m_graphics.PreferredBackBufferWidth = this.m_windowedSize.X;
                this.m_graphics.PreferredBackBufferHeight = this.m_windowedSize.Y;
            }
            else
            {
                DisplayMode display = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
                this.m_graphics.IsFullScreen = true;
                this.m_graphics.PreferredBackBufferWidth = display.Width;
                this.m_graphics.PreferredBackBufferHeight = display.Height;
            }
            this.ApplyGraphicsChanges();
        }

        /// <summary>Keeps the back buffer the same size as the window; VirtualScreen letterboxes into it.</summary>
        private void OnClientSizeChanged(object sender, EventArgs e)
        {
            Rectangle bounds = this.Game.Window.ClientBounds;
            if (this.m_applyingGraphicsChanges || this.m_graphics.IsFullScreen || bounds.Width <= 0 || bounds.Height <= 0)
                return;
            this.m_windowedSize = new Point(bounds.Width, bounds.Height);
            if (this.m_graphics.PreferredBackBufferWidth == bounds.Width && this.m_graphics.PreferredBackBufferHeight == bounds.Height)
                return;
            this.m_graphics.PreferredBackBufferWidth = bounds.Width;
            this.m_graphics.PreferredBackBufferHeight = bounds.Height;
            this.ApplyGraphicsChanges();
        }

        // ApplyChanges can resize the window and raise ClientSizeChanged again; the flag stops that loop.
        private void ApplyGraphicsChanges()
        {
            this.m_applyingGraphicsChanges = true;
            try
            {
                this.m_graphics.ApplyChanges();
            }
            finally
            {
                this.m_applyingGraphicsChanges = false;
            }
        }
    }
}
