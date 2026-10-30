using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Game.FSM;
using Resanctified.Game.FSM.GameState;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.FSM.GameState.MainMenu;

namespace Resanctified.Game.UI
{
    /// <summary>
    /// Help &amp; Options &gt; Keyboard: the desktop controls drawn as key caps, each row followed by what it does.
    /// Gameplay keys on the left, menu and window keys on the right.
    /// </summary>
    public class KeyboardMenuViewController : ViewController
    {
        private const float TITLE_X = 240f;
        private const float TITLE_TEXTURED_Y = 255f;
        private const float TITLE_Y = 250f;
        private const float ROWS_TOP_Y = 212f;
        private const float ROW_STEP_Y = 26f;
        private const float GAME_KEYS_X = 51f;
        private const float GAME_ACTION_X = 163f;
        private const float MENU_KEYS_X = 291f;
        private const float MENU_ACTION_X = 345f;
        private const float KEY_HEIGHT = 20f;
        private const float KEY_MIN_WIDTH = 20f;
        private const float KEY_PADDING_X = 5f;
        private const float KEY_SPACING = 3f;
        private const float KEY_BORDER = 1f;
        private const float KEY_TEXT_SCALE = 0.75f;
        private const float ACTION_TEXT_SCALE = 0.9f;
        private const float GROUP_GAP = 16f;
        private const float ARROW_HALF_SIZE = 4f;
        private static readonly Color KEY_BORDER_COLOR = new Color(210, 210, 210, 255);
        private static readonly Color KEY_FILL_COLOR = new Color(25, 25, 25, 230);

        private sealed class Row
        {
            public Row(GameLocalisation.TextId action, params Keys[][] keyGroups)
            {
                this.Action = action;
                this.KeyGroups = keyGroups;
            }

            public GameLocalisation.TextId Action { get; }

            /// <summary>Alternative ways to do the action, drawn with a "/" between them.</summary>
            public Keys[][] KeyGroups { get; }
        }

        // Mirrors KeyBindings.
        private static readonly Row[] GAME_ROWS =
        {
            new Row(GameLocalisation.TextId.Keyboard_Move, new[] { Keys.A, Keys.D }, new[] { Keys.Left, Keys.Right }),
            new Row(GameLocalisation.TextId.Keyboard_Jump, new[] { Keys.W }, new[] { Keys.Up }),
            new Row(GameLocalisation.TextId.Help_Slash, new[] { Keys.LeftControl }),
            new Row(GameLocalisation.TextId.Help_Fire, new[] { Keys.LeftShift }),
            new Row(GameLocalisation.TextId.Keyboard_ChangeGun, new[] { Keys.Q, Keys.E }, new[] { Keys.Tab }),
            new Row(GameLocalisation.TextId.Keyboard_BreakFree, new[] { KeyBindings.Shake }),
        };

        private static readonly Row[] MENU_ROWS =
        {
            new Row(GameLocalisation.TextId.Ok, new[] { KeyBindings.Validate }),
            new Row(GameLocalisation.TextId.Back, new[] { KeyBindings.Back }),
            new Row(GameLocalisation.TextId.PauseGame, new[] { KeyBindings.Pause }),
            new Row(GameLocalisation.TextId.Keyboard_FullScreen, new[] { KeyBindings.FullScreen }),
            new Row(GameLocalisation.TextId.Keyboard_Screenshot, new[] { KeyBindings.Screenshot }),
            new Row(GameLocalisation.TextId.Keyboard_HDTextures, new[] { KeyBindings.ToggleHdTextures }),
            new Row(GameLocalisation.TextId.Keyboard_Resolution, new[] { KeyBindings.ToggleResolution }),
        };

        private VertexPositionColor[] m_quadBackgroundBlack = new VertexPositionColor[4];
        private VertexPositionTexture[] m_quadBackground = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadBack = new VertexPositionTexture[4];
        // Refilled for every key cap and arrow: DrawQuad copies the vertices when it draws.
        private VertexPositionColor[] m_quadShape = new VertexPositionColor[4];
        private Engine.Resources.Texture m_menuTexture;
        private Button m_buttonBack;
        private Transform m_t = new Transform();
        private Font m_font;
        private bool m_texturedBackground;

        public KeyboardMenuViewController()
        {
            this.m_texturedBackground = false;
            this.m_menuTexture = null;
            this.m_buttonBack = new Button();
        }

        public override void Initialize() => this.Initialize(false);

        public void Initialize(bool texturedBackground)
        {
            this.m_texturedBackground = texturedBackground;
            base.Initialize();
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
            if (this.m_texturedBackground)
            {
                this.m_menuTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
                this.m_font = ResourceManager.Instance.LoadFont("Fonts/charlemagnestd50.xml");
                QuadFactory.CreateAnchoredQuad(this.m_quadBackground, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Bottom);
                QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
                this.m_buttonBack.Initialize(this, 56f, 56f);
                this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
            }
            else
            {
                this.m_buttonBack.Initialize(this, 100f, 56f);
                this.m_buttonBack.GetTransform().SetPosition(30f, 40f);
            }
            QuadFactory.CreateQuad(this.m_quadBackgroundBlack, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_menuTexture);
            Resource.SafeRelease(ref this.m_font);
            this.m_buttonBack.Cleanup();
            base.Cleanup();
        }

        public override void Update(float elapsedTime)
        {
            if (!this.m_active)
                return;
            base.Update(elapsedTime);
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.GetInstance();
            instance.Begin2DMode(false);
            Font font = GameApplication.GetInstance().GetFont();
            this.m_t.SetScale(1f);
            if (this.m_texturedBackground)
            {
                instance.DrawQuad(this.m_quadBackground, VertexFormat.Position3Texture, null, this.m_menuTexture);
                this.m_t.SetPosition(TITLE_X, TITLE_TEXTURED_Y);
                instance.DrawString(this.m_font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.KeyboardBig));
                this.m_t.SetPosition(36f, 40f, 0.0f);
                instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, this.m_t, this.m_menuTexture);
            }
            else
            {
                instance.DrawQuad(this.m_quadBackgroundBlack, VertexFormat.Position3Color, null, null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
                this.m_t.SetPosition(TITLE_X, TITLE_Y);
                instance.DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_SELECTED_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Keyboard));
                this.m_t.SetPosition(30f, 40f, 0.0f);
                instance.DrawString(font, this.m_t, 8, GameConstant.MENU_TEXT_COLOR, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Back));
            }
            this.DrawRows(font, GAME_ROWS, GAME_KEYS_X, GAME_ACTION_X);
            this.DrawRows(font, MENU_ROWS, MENU_KEYS_X, MENU_ACTION_X);
            this.m_t.SetScale(1f);
            instance.End2DMode();
        }

        private void DrawRows(Font font, Row[] rows, float keysX, float actionX)
        {
            for (int rowIndex = 0; rowIndex < rows.Length; ++rowIndex)
            {
                float centerY = ROWS_TOP_Y - rowIndex * ROW_STEP_Y;
                float x = keysX;
                Keys[][] groups = rows[rowIndex].KeyGroups;
                for (int groupIndex = 0; groupIndex < groups.Length; ++groupIndex)
                {
                    if (groupIndex > 0)
                    {
                        this.DrawText(font, "/", x + (GROUP_GAP - font.GetStringWidth("/") * ACTION_TEXT_SCALE) * 0.5f, centerY, ACTION_TEXT_SCALE, GameConstant.MENU_TEXT_COLOR);
                        x += GROUP_GAP;
                    }
                    for (int keyIndex = 0; keyIndex < groups[groupIndex].Length; ++keyIndex)
                    {
                        if (keyIndex > 0)
                            x += KEY_SPACING;
                        x += this.DrawKey(font, groups[groupIndex][keyIndex], x, centerY);
                    }
                }
                this.DrawText(font, GameLocalisation.GetInstance().GetString(rows[rowIndex].Action), actionX, centerY, ACTION_TEXT_SCALE, GameConstant.MENU_TEXT_COLOR);
            }
        }

        /// <summary>Draws one key cap with its left edge at x, centered on centerY, and returns its width.</summary>
        private float DrawKey(Font font, Keys key, float left, float centerY)
        {
            string label = GetKeyLabel(key);
            float width = label == null ? KEY_MIN_WIDTH : Math.Max(KEY_MIN_WIDTH, (float)Math.Round(font.GetStringWidth(label) * KEY_TEXT_SCALE + KEY_PADDING_X * 2.0f));
            float top = (float)Math.Floor(centerY + KEY_HEIGHT * 0.5f);
            this.DrawRectangle(left, top, width, KEY_HEIGHT, KEY_BORDER_COLOR);
            this.DrawRectangle(left + KEY_BORDER, top - KEY_BORDER, width - KEY_BORDER * 2.0f, KEY_HEIGHT - KEY_BORDER * 2.0f, KEY_FILL_COLOR);
            if (label == null)
                this.DrawArrow(key, left + width * 0.5f, top - KEY_HEIGHT * 0.5f);
            else
                this.DrawText(font, label, left + (width - font.GetStringWidth(label) * KEY_TEXT_SCALE) * 0.5f, centerY, KEY_TEXT_SCALE, GameConstant.MENU_TEXT_COLOR);
            return width;
        }

        /// <summary>The text printed on a key cap, or null for the arrow keys, which are drawn as triangles.</summary>
        private static string GetKeyLabel(Keys key)
        {
            GameLocalisation localisation = GameLocalisation.GetInstance();
            switch (key)
            {
                case Keys.Left:
                case Keys.Right:
                case Keys.Up:
                case Keys.Down:
                    return null;
                case Keys.LeftControl:
                case Keys.RightControl:
                    return localisation.GetString(GameLocalisation.TextId.Key_Ctrl);
                case Keys.LeftShift:
                case Keys.RightShift:
                    return localisation.GetString(GameLocalisation.TextId.Key_Shift);
                case Keys.Space:
                    return localisation.GetString(GameLocalisation.TextId.Key_Space);
                case Keys.Enter:
                    return localisation.GetString(GameLocalisation.TextId.Key_Enter);
                case Keys.Escape:
                    return localisation.GetString(GameLocalisation.TextId.Key_Esc);
                default:
                    // Letters and function keys: the enum name is what the key shows (A, Q, F11...).
                    return key.ToString();
            }
        }

        private void DrawRectangle(float left, float top, float width, float height, Color color)
        {
            QuadFactory.CreateQuad(this.m_quadShape, left, top, width, height, color);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quadShape, VertexFormat.Position3Color, null, null, Color.White);
        }

        // The font has no arrow glyphs: a triangle drawn as a quad whose last two corners coincide.
        private void DrawArrow(Keys key, float centerX, float centerY)
        {
            Vector2 direction = key == Keys.Left ? -Vector2.UnitX : key == Keys.Right ? Vector2.UnitX : key == Keys.Up ? Vector2.UnitY : -Vector2.UnitY;
            Vector2 side = new Vector2(direction.Y, -direction.X);
            Vector2 center = new Vector2(centerX, centerY);
            Vector2 tip = center + direction * ARROW_HALF_SIZE;
            Vector2 baseCenter = center - direction * ARROW_HALF_SIZE;
            this.SetShapeVertex(0, tip);
            this.SetShapeVertex(1, baseCenter + side * ARROW_HALF_SIZE);
            this.SetShapeVertex(2, baseCenter - side * ARROW_HALF_SIZE);
            this.SetShapeVertex(3, baseCenter - side * ARROW_HALF_SIZE);
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawQuad(this.m_quadShape, VertexFormat.Position3Color, null, null, Color.White);
        }

        private void SetShapeVertex(int index, Vector2 position)
        {
            this.m_quadShape[index].Position = new Vector3(position, 0.0f);
            this.m_quadShape[index].Color = GameConstant.MENU_TEXT_COLOR;
        }

        // DrawString's anchors measure the unscaled string, so scaled text is placed from its top-left corner here.
        private void DrawText(Font font, string text, float left, float centerY, float scale, Color color)
        {
            this.m_t.SetScale(scale);
            this.m_t.SetPosition((float)Math.Floor(left), (float)Math.Floor(centerY + font.GetLineHeight() * scale * 0.5f));
            Engine.Graphics.Renderer.Renderer.GetInstance().DrawString(font, this.m_t, 0, color, text);
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || sender != this.m_buttonBack || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            if (GameFSM.GetInstance().GetCurrentState() == GameStateMainMenu.GetInstance())
                MainMenuStateKeyboard.GetInstance().BackRequest = true;
            else
                InGameMenuStateKeyboard.GetInstance().BackRequest = true;
        }
    }
}
