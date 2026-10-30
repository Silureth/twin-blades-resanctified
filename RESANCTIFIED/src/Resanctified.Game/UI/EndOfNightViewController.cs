using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Tools;
using Resanctified.Engine.UI;
using Resanctified.Game.World.Entities;

namespace Resanctified.Game.UI
{
    internal sealed class EndOfNightViewController : ViewController
    {
        private const float TEX_WIDTH = 512f;
        private const float TEX_HEIGHT = 512f;
        private const float WEAPON_COLOR_TEX_TOP = 10f;
        private const float WEAPON_GRAY_TEX_TOP = 89f;
        private const float MGUN_COLOR_TEX_TOP = 181f;
        private const float MGUN_GRAY_TEX_TOP = 267f;
        private const float WEAPON_TEX_WIDTH = 81f;
        private const float WEAPON_TEX_HEIGHT = 75f;
        private const float HOLY_TEX_LEFT = 12f;
        private const float ICE_TEX_LEFT = 109f;
        private const float FIRE_TEX_LEFT = 203f;
        private const float EXPLODE_TEX_LEFT = 298f;
        private const float BULLET_TEX_LEFT = 396f;
        private const float MGUN_TEX_LEFT = 12f;
        private const float SCYTHE_COLOR_TEX_LEFT = 280f;
        private const float SCYTHE_COLOR_TEX_TOP = 180f;
        private const float SCYTHE_GRAY_TEX_LEFT = 283f;
        private const float SCYTHE_GRAY_TEX_TOP = 260f;
        private const float HITPOINTSMAX_COLOR_TEX_LEFT = 183f;
        private const float HITPOINTSMAX_COLOR_TEX_TOP = 260f;
        private const float HITPOINTSMAX_GRAY_TEX_LEFT = 65f;
        private const float HITPOINTSMAX_GRAY_TEX_TOP = 345f;
        private const float SLASHMANADRAIN_COLOR_TEX_LEFT = 154f;
        private const float SLASHMANADRAIN_COLOR_TEX_TOP = 345f;
        private const float SLASHMANADRAIN_GRAY_TEX_LEFT = 333f;
        private const float SLASHMANADRAIN_GRAY_TEX_TOP = 345f;
        private const float MANAPOINTSMAX_COLOR_TEX_LEFT = 243f;
        private const float MANAPOINTSMAX_COLOR_TEX_TOP = 345f;
        private const float MANAPOINTSMAX_GRAY_TEX_LEFT = 422f;
        private const float MANAPOINTSMAX_GRAY_TEX_TOP = 345f;
        private const float AIRSLASH_COLOR_TEX_LEFT = 56f;
        private const float AIRSLASH_COLOR_TEX_TOP = 345f;
        private const float AIRSLASH_GRAY_TEX_LEFT = 280f;
        private const float AIRSLASH_GRAY_TEX_TOP = 260f;
        private const float BLUEORB_TEX_LEFT = 18f;
        private const float BLUEORB_TEX_TOP = 370f;
        private const float BLUEORB_TEX_WIDTH = 19f;
        private const float BLUEORB_TEX_HEIGHT = 19f;
        private const float BIGHEART_TEX_LEFT = 110f;
        private const float BIGHEART_TEX_TOP = 258f;
        private const float BIGHEART_TEX_WIDTH = 52f;
        private const float BIGHEART_TEX_HEIGHT = 38f;
        private const float SMHEART_TEX_LEFT = 123f;
        private const float SMHEART_TEX_TOP = 308f;
        private const float SMHEART_TEX_WIDTH = 30f;
        private const float SMHEART_TEX_HEIGHT = 22f;
        private const float UPG_COLOR_TEX_LEFT = 204f;
        private const float UPG_COLOR_TEX_TOP = 184f;
        private const float UPG_COLOR_TEX_WIDTH = 56f;
        private const float UPG_COLOR_TEX_HEIGHT = 56f;
        private const float UPG_GRAY_TEX_LEFT = 341f;
        private const float UPG_GRAY_TEX_TOP = 346f;
        private const float UPG_GRAY_TEX_WIDTH = 55f;
        private const float UPG_GRAY_TEX_HEIGHT = 55f;
        private const float BG_TEX_LEFT = 0.0f;
        private const float BG_TEX_TOP = 0.0f;
        private const float BG_TEX_WIDTH = 480f;
        private const float BG_TEX_HEIGHT = 320f;
        private const float NUNTAB_TEX_LEFT = 389f;
        private const float NUNTAB_TEX_TOP = 247f;
        private const float NUNTAB_TEX_WIDTH = 114f;
        private const float NUNTAB_TEX_HEIGHT = 82f;
        private const float GUNTAB_TEX_LEFT = 389f;
        private const float GUNTAB_TEX_TOP = 173f;
        private const float GUNTAB_TEX_WIDTH = 114f;
        private const float GUNTAB_TEX_HEIGHT = 68f;
        private const float MAINTAB_TEX_LEFT = 5f;
        private const float MAINTAB_TEX_TOP = 426f;
        private const float MAINTAB_TEX_WIDTH = 455f;
        private const float MAINTAB_TEX_HEIGHT = 82f;
        private const float SEL_TEX_LEFT = 389f;
        private const float SEL_TEX_TOP = 93f;
        private const float SEL_TEX_WIDTH = 58f;
        private const float SEL_TEX_HEIGHT = 58f;
        private const float SEL_OFFSET_X_FROM_WEAPON_CENTER = -2f;
        private const float SEL_OFFSET_Y_FROM_WEAPON_CENTER = 7f;
        private const float SEL_OFFSET_X_FROM_NUNUPGRADE_CENTER = -2f;
        private const float SEL_OFFSET_Y_FROM_NUNUPGRADE_CENTER = 7f;
        private const float UPG_BTN_RADIUS = 30f;
        private const float VAL_BTN_RADIUS = 30f;
        private const float WEAPON_BTN_RADIUS = 40f;
        private const float NUNUPGRADE_BTN_RADIUS = 40f;
        private const float TAB_BTN_RADIUS = 30f;
        private const float SCREEN_WIDTH = 480f;
        private const float SCREEN_HEIGHT = 320f;
        private const float WEAPON_BTN_CY1 = 187f;
        private const float WEAPON_BTN_CY2 = 84f;
        private const float WEAPON_BTN_CX1 = 87f;
        private const float WEAPON_BTN_CX2 = 238f;
        private const float WEAPON_BTN_CX3 = 387f;
        private const float BULLET_BTN_CX = 87f;
        private const float BULLET_BTN_CY = 187f;
        private const float FIRE_BTN_CX = 238f;
        private const float FIRE_BTN_CY = 187f;
        private const float EXPLODE_BTN_CX = 387f;
        private const float EXPLODE_BTN_CY = 187f;
        private const float MGUN_BTN_CX = 87f;
        private const float MGUN_BTN_CY = 84f;
        private const float ICE_BTN_CX = 238f;
        private const float ICE_BTN_CY = 84f;
        private const float HOLY_BTN_CX = 387f;
        private const float HOLY_BTN_CY = 84f;
        private const float UPG_BTN_CX = 443f;
        private const float UPG_BTN_CY = 274f;
        private const float UPG_BTN_MIN_SCALE = 1f;
        private const float UPG_BTN_MAX_SCALE = 1.3f;
        private const float UPG_BTN_SCALE_DURATION = 0.15f;
        private const float GUNTAB_BTN_CX = 70f;
        private const float GUNTAB_BTN_CY = 266f;
        private const float NUNTAB_BTN_CX = 178f;
        private const float NUNTAB_BTN_CY = 266f;
        private const float BIGHEART_CX = 328f;
        private const float BIGHEART_CY = 262f;
        private const float GUNTAB_X = 13f;
        private const float GUNTAB_Y = 299f;
        private const float NUNTAB_X = 121f;
        private const float NUNTAB_Y = 313f;
        private const float MAINTAB_X = 13f;
        private const float MAINTAB_Y = 313f;
        private const float HEARTS_X = 355f;
        private const float HEARTS_Y = 255f;
        private const float WEAPON_SMALL_HEART_OFFSET_X_FROM_CENTER = 20f;
        private const float WEAPON_SMALL_HEART_OFFSET_Y_FROM_CENTER = -48f;
        private const float WEAPON_COST_OFFSET_X_FROM_CENTER = -8f;
        private const float WEAPON_COST_OFFSET_Y_FROM_CENTER = -56f;
        private const float NUNUPGRADE_BTN_CX1 = 87f;
        private const float NUNUPGRADE_BTN_CX2 = 238f;
        private const float NUNUPGRADE_BTN_CX3 = 387f;
        private const float NUNUPGRADE_BTN_CY1 = 187f;
        private const float NUNUPGRADE_BTN_CY2 = 136f;
        private const float NUNUPGRADE_BTN_CY3 = 84f;
        private const float NUNUPGRADE_SMALL_HEART_OFFSET_X_FROM_CENTER = 20f;
        private const float NUNUPGRADE_SMALL_HEART_OFFSET_Y_FROM_CENTER = -48f;
        private const float NUNUPGRADE_COST_OFFSET_X_FROM_CENTER = -8f;
        private const float NUNUPGRADE_COST_OFFSET_Y_FROM_CENTER = -56f;
        private static readonly int[,] NUNUPGRADE_TEXTID = new int[5, 2]
        {
      {
        -1,
        62
      },
      {
        -1,
        63
      },
      {
        -1,
        64
      },
      {
        -1,
        65
      },
      {
        60,
        61
      }
        };
        private static readonly float[,] NUNUPGRADE_CENTERS = new float[5, 2]
        {
      {
        87f,
        187f
      },
      {
        387f,
        187f
      },
      {
        87f,
        84f
      },
      {
        387f,
        84f
      },
      {
        238f,
        136f
      }
        };
        private static readonly int[,] NUNUPGRADE_COSTS = new int[5, 5]
        {
      {
        10,
        25,
        50,
        75,
        100
      },
      {
        10,
        25,
        50,
        75,
        100
      },
      {
        10,
        20,
        40,
        60,
        80
      },
      {
        10,
        20,
        40,
        60,
        80
      },
      {
        40,
        60,
        80,
        100,
        120
      }
        };
        private static readonly int[,] WEAPON_COSTS = new int[6, 5]
        {
      {
        1,
        20,
        30,
        50,
        80
      },
      {
        30,
        50,
        80,
        100,
        120
      },
      {
        60,
        80,
        100,
        120,
        150
      },
      {
        60,
        80,
        100,
        120,
        150
      },
      {
        75,
        100,
        125,
        150,
        200
      },
      {
        120,
        160,
        200,
        250,
        300
      }
        };
        private static readonly float[,] WEAPON_CENTERS = new float[6, 2]
        {
      {
        87f,
        187f
      },
      {
        238f,
        187f
      },
      {
        387f,
        187f
      },
      {
        87f,
        84f
      },
      {
        238f,
        84f
      },
      {
        387f,
        84f
      }
        };
        private static readonly int[,] WEAPON_TEXTID = new int[6, 2]
        {
      {
        -1,
        54
      },
      {
        49,
        55
      },
      {
        50,
        56
      },
      {
        51,
        57
      },
      {
        52,
        58
      },
      {
        53,
        59
      }
        };
        private static readonly float[,] BLUEORB_CENTERS = new float[5, 2]
        {
      {
        -25f,
        30f
      },
      {
        -33f,
        -4f
      },
      {
        -1f,
        -27f
      },
      {
        32f,
        -4f
      },
      {
        23f,
        30f
      }
        };
        private VertexPositionTexture[] m_backgroundQuad;
        private VertexPositionTexture[,][] m_weaponQuads;
        private VertexPositionTexture[] m_blueOrbQuad;
        private VertexPositionTexture[] m_bigHeartQuad;
        private VertexPositionTexture[] m_smallHeartQuad;
        private VertexPositionTexture[][] m_upgradeQuads;
        private VertexPositionTexture[] m_selQuad;
        private VertexPositionTexture[] m_nunTabQuad;
        private VertexPositionTexture[] m_gunTabQuad;
        private VertexPositionTexture[] m_mainTabQuad;
        private VertexPositionTexture[,][] m_nunUpgradeQuads;
        private VertexPositionTexture[] m_quadBack;
        private VertexPositionTexture[] m_quadNext;
        private CircularButton[] m_weaponButtons;
        private CircularButton m_upgradeButton;
        private CircularButton m_gunTabButton;
        private CircularButton m_nunTabButton;
        private CircularButton[] m_nunUpgradeButtons;
        private Button m_buttonBack;
        private Button m_buttonNext;
        private Engine.Resources.Texture m_textureEon;
        private Engine.Resources.Texture m_textureMenu1;
        private int[] m_weaponLevels;
        private int[] m_nunUpgradeLevels;
        private PlayableCharacter m_hero;
        private int m_activeTab;
        private bool m_done;
        private bool m_upgradeButtonActive;
        private float m_upgradeButtonScaleTime;
        private int m_selectedWeaponIndex;
        private int m_selectedNunUpgradeIndex;
        public bool ButtonsEnabled;
        private StringBuilder m_stringBuilder;
        private Transform m_trans;

        public EndOfNightViewController()
        {
            this.m_stringBuilder = new StringBuilder(256);
            this.m_trans = new Transform();
            this.m_textureEon = null;
            this.m_textureMenu1 = null;
            this.m_hero = null;
            this.m_activeTab = 0;
            this.m_done = false;
            this.m_selectedWeaponIndex = -1;
            this.m_selectedNunUpgradeIndex = -1;
            this.m_upgradeButtonActive = false;
            this.m_upgradeButtonScaleTime = 0.0f;
            this.m_backgroundQuad = new VertexPositionTexture[4];
            this.m_weaponQuads = new VertexPositionTexture[6, 2][];
            for (int index1 = 0; index1 < 6; ++index1)
            {
                for (int index2 = 0; index2 < 2; ++index2)
                    this.m_weaponQuads[index1, index2] = new VertexPositionTexture[4];
            }
            this.m_blueOrbQuad = new VertexPositionTexture[4];
            this.m_bigHeartQuad = new VertexPositionTexture[4];
            this.m_smallHeartQuad = new VertexPositionTexture[4];
            this.m_upgradeQuads = new VertexPositionTexture[2][];
            for (int index = 0; index < 2; ++index)
                this.m_upgradeQuads[index] = new VertexPositionTexture[4];
            this.m_selQuad = new VertexPositionTexture[4];
            this.m_nunTabQuad = new VertexPositionTexture[4];
            this.m_gunTabQuad = new VertexPositionTexture[4];
            this.m_mainTabQuad = new VertexPositionTexture[4];
            this.m_nunUpgradeQuads = new VertexPositionTexture[5, 2][];
            for (int index3 = 0; index3 < 5; ++index3)
            {
                for (int index4 = 0; index4 < 2; ++index4)
                    this.m_nunUpgradeQuads[index3, index4] = new VertexPositionTexture[4];
            }
            this.m_quadBack = new VertexPositionTexture[4];
            this.m_quadNext = new VertexPositionTexture[4];
            this.m_weaponButtons = new CircularButton[6];
            for (int index = 0; index < 6; ++index)
                this.m_weaponButtons[index] = new CircularButton();
            this.m_upgradeButton = new CircularButton();
            this.m_gunTabButton = new CircularButton();
            this.m_nunTabButton = new CircularButton();
            this.m_nunUpgradeButtons = new CircularButton[5];
            for (int index = 0; index < 5; ++index)
                this.m_nunUpgradeButtons[index] = new CircularButton();
            this.m_buttonBack = new Button();
            this.m_buttonNext = new Button();
            this.DefaultButton = this.m_buttonNext;
            this.m_weaponLevels = new int[6];
            this.m_nunUpgradeLevels = new int[5];
            QuadFactory.CreateAnchoredQuad(this.m_backgroundQuad, 0.0f, 0.0f, 480f, 320f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[5, 1], 12f, 10f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[3, 1], 109f, 10f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[2, 1], 203f, 10f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[4, 1], 298f, 10f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[0, 1], 396f, 10f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[1, 1], 12f, 181f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[5, 0], 12f, 89f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[3, 0], 109f, 89f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[2, 0], 203f, 89f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[4, 0], 298f, 89f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[0, 0], 396f, 89f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_weaponQuads[1, 0], 12f, 267f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_blueOrbQuad, 18f, 370f, 19f, 19f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_bigHeartQuad, 110f, 258f, 52f, 38f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_smallHeartQuad, 123f, 308f, 30f, 22f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_upgradeQuads[1], 204f, 184f, 56f, 56f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_upgradeQuads[0], 341f, 346f, 55f, 55f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_selQuad, 389f, 93f, 58f, 58f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_mainTabQuad, 5f, 426f, 455f, 82f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunTabQuad, 389f, 247f, 114f, 82f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_gunTabQuad, 389f, 173f, 114f, 68f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[0, 0], 283f, 260f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[0, 1], 280f, 180f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[2, 0], 65f, 345f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[2, 1], 183f, 260f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[1, 0], 333f, 345f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[1, 1], 154f, 345f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[3, 0], 422f, 345f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[3, 1], 243f, 345f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[4, 0], 280f, 260f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_nunUpgradeQuads[4, 1], 56f, 345f, 81f, 75f, 512f, 512f, Anchor.Left);
            QuadFactory.CreateAnchoredQuad(this.m_quadBack, 89f, 452f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            QuadFactory.CreateAnchoredQuad(this.m_quadNext, 8f, 331f, 56f, 56f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            this.ButtonsEnabled = true;
        }

        public override void Cleanup()
        {
            Resource.SafeRelease(ref this.m_textureEon);
            Resource.SafeRelease(ref this.m_textureMenu1);
            this.m_hero = null;
            for (int index = 0; index < 6; ++index)
                this.m_weaponButtons[index].Cleanup();
            this.m_buttonNext.Cleanup();
            this.m_buttonBack.Cleanup();
            this.m_upgradeButton.Cleanup();
            this.m_nunTabButton.Cleanup();
            this.m_gunTabButton.Cleanup();
            for (int index = 0; index < 5; ++index)
                this.m_nunUpgradeButtons[index].Cleanup();
            base.Cleanup();
        }

        public void Initialize(PlayableCharacter hero)
        {
            this.Initialize();
            Resource.SafeRelease(ref this.m_textureEon);
            Resource.SafeRelease(ref this.m_textureMenu1);
            this.m_textureEon = ResourceManager.GetInstance().LoadTexture("Textures\\texture_eon");
            this.m_textureMenu1 = ResourceManager.GetInstance().LoadTexture("Textures\\texture_menu_1");
            this.m_hero = hero;
            this.m_done = false;
            Gun[] gunArray = hero.GetGunArray();
            for (int index = 0; index < 6; ++index)
                this.m_weaponLevels[index] = !gunArray[index].IsAvailable ? 0 : gunArray[index].GetLevel();
            this.m_nunUpgradeLevels[0] = hero.SlashDamageLevel;
            this.m_nunUpgradeLevels[1] = hero.SlashManaDrainLevel;
            this.m_nunUpgradeLevels[2] = hero.HitPointsLevel;
            this.m_nunUpgradeLevels[3] = hero.ManaPointsLevel;
            this.m_nunUpgradeLevels[4] = hero.AirSlashAvailable ? hero.AirSlashDamageLevel : 0;
            this.m_upgradeButton.Initialize(this, 30f);
            this.m_upgradeButton.GetTransform().SetPosition(443f, 274f);
            this.m_gunTabButton.Initialize(this, 30f);
            this.m_gunTabButton.GetTransform().SetPosition(70f, 266f);
            this.m_nunTabButton.Initialize(this, 30f);
            this.m_nunTabButton.GetTransform().SetPosition(178f, 266f);
            for (int index = 0; index < 6; ++index)
            {
                this.m_weaponButtons[index].Initialize(this, 40f);
                this.m_weaponButtons[index].GetTransform().SetPosition(WEAPON_CENTERS[index, 0], WEAPON_CENTERS[index, 1]);
            }
            for (int index = 0; index < 5; ++index)
            {
                this.m_nunUpgradeButtons[index].Initialize(this, 40f);
                this.m_nunUpgradeButtons[index].GetTransform().SetPosition(NUNUPGRADE_CENTERS[index, 0], NUNUPGRADE_CENTERS[index, 1]);
            }
            this.m_buttonBack.Initialize(this, 56f, 56f);
            this.m_buttonBack.GetTransform().SetPosition(36f, 40f);
            this.m_buttonNext.Initialize(this, 56f, 56f);
            this.m_buttonNext.GetTransform().SetPosition(440f, 40f);
            this.SelectActiveTab(0);
            this.ButtonsEnabled = true;
        }

        public override void Update(float elapsedTime)
        {
            if (!this.m_active)
                return;
            if (this.m_upgradeButtonActive)
                this.m_upgradeButtonScaleTime += elapsedTime;
            else
                this.m_upgradeButtonScaleTime -= elapsedTime;
            this.m_upgradeButtonScaleTime = MathHelper.Clamp(this.m_upgradeButtonScaleTime, 0.0f, 0.15f);
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Font font = GameApplication.Instance.GetFont();
            Engine.Graphics.Renderer.Renderer instance = Engine.Graphics.Renderer.Renderer.Instance;
            Transform trans = this.m_trans;
            trans.Clear();
            trans.SetParent(this.m_transform);
            instance.Begin2DMode(false);
            trans.SetPosition(0.0f, 320f);
            instance.DrawQuad(this.m_backgroundQuad, VertexFormat.Position3Texture, trans, this.m_textureMenu1);
            trans.SetPosition(13f, 313f);
            instance.DrawQuad(this.m_mainTabQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 0);
            if (this.m_activeTab == 0)
            {
                trans.SetPosition(13f, 299f);
                instance.DrawQuad(this.m_gunTabQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 0);
                for (int index1 = 0; index1 < 6; ++index1)
                {
                    trans.SetPosition(WEAPON_CENTERS[index1, 0], WEAPON_CENTERS[index1, 1]);
                    int index2 = this.m_weaponLevels[index1] > 0 ? 1 : 0;
                    instance.DrawQuad(this.m_weaponQuads[index1, index2], VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                    for (int index3 = 1; index3 <= this.m_weaponLevels[index1]; ++index3)
                    {
                        trans.SetPosition(WEAPON_CENTERS[index1, 0] + BLUEORB_CENTERS[index3 - 1, 0], WEAPON_CENTERS[index1, 1] + BLUEORB_CENTERS[index3 - 1, 1]);
                        instance.DrawQuad(this.m_blueOrbQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                    }
                    if (index1 == this.m_selectedWeaponIndex)
                    {
                        trans.SetPosition(WEAPON_CENTERS[index1, 0] - 2f, WEAPON_CENTERS[index1, 1] + 7f);
                        instance.DrawQuad(this.m_selQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                    }
                }
                for (int index = 0; index < 6; ++index)
                {
                    if (this.m_weaponLevels[index] < 5)
                    {
                        trans.SetPosition(WEAPON_CENTERS[index, 0] + 20f, WEAPON_CENTERS[index, 1] - 48f);
                        instance.DrawQuad(this.m_smallHeartQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                        trans.SetPosition(WEAPON_CENTERS[index, 0] - 8f, WEAPON_CENTERS[index, 1] - 56f);
                        this.m_stringBuilder.Length = 0;
                        StringHelper.Append(this.m_stringBuilder, WEAPON_COSTS[index, this.m_weaponLevels[index]]);
                        instance.DrawString(font, trans, 3, Color.White, this.m_stringBuilder);
                    }
                }
            }
            else
            {
                trans.SetPosition(121f, 313f);
                instance.DrawQuad(this.m_nunTabQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 0);
                for (int index4 = 0; index4 < 5; ++index4)
                {
                    trans.SetPosition(NUNUPGRADE_CENTERS[index4, 0], NUNUPGRADE_CENTERS[index4, 1]);
                    int index5 = this.m_nunUpgradeLevels[index4] > 0 ? 1 : 0;
                    instance.DrawQuad(this.m_nunUpgradeQuads[index4, index5], VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                    for (int index6 = 1; index6 <= this.m_nunUpgradeLevels[index4]; ++index6)
                    {
                        trans.SetPosition(NUNUPGRADE_CENTERS[index4, 0] + BLUEORB_CENTERS[index6 - 1, 0], NUNUPGRADE_CENTERS[index4, 1] + BLUEORB_CENTERS[index6 - 1, 1]);
                        instance.DrawQuad(this.m_blueOrbQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                    }
                    if (index4 == this.m_selectedNunUpgradeIndex)
                    {
                        trans.SetPosition(NUNUPGRADE_CENTERS[index4, 0] - 2f, NUNUPGRADE_CENTERS[index4, 1] + 7f);
                        instance.DrawQuad(this.m_selQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                    }
                }
                for (int index = 0; index < 5; ++index)
                {
                    if (this.m_nunUpgradeLevels[index] < 5)
                    {
                        trans.SetPosition(NUNUPGRADE_CENTERS[index, 0] + 20f, NUNUPGRADE_CENTERS[index, 1] - 48f);
                        instance.DrawQuad(this.m_smallHeartQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                        trans.SetPosition(NUNUPGRADE_CENTERS[index, 0] - 8f, NUNUPGRADE_CENTERS[index, 1] - 56f);
                        this.m_stringBuilder.Length = 0;
                        StringHelper.Append(this.m_stringBuilder, NUNUPGRADE_COSTS[index, this.m_nunUpgradeLevels[index]]);
                        instance.DrawString(font, trans, 3, Color.White, this.m_stringBuilder);
                    }
                }
            }
            trans.SetPosition(328f, 262f);
            instance.DrawQuad(this.m_bigHeartQuad, VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
            trans.SetPosition(355f, byte.MaxValue);
            this.m_stringBuilder.Length = 0;
            StringHelper.Append(this.m_stringBuilder.Append("x "), this.m_hero.Hearts, 3);
            instance.DrawString(font, trans, 2, Color.White, this.m_stringBuilder);
            trans.SetPosition(443f, 274f);
            if (this.m_upgradeButtonActive)
            {
                float num1 = this.m_upgradeButtonScaleTime / 0.15f;
                float num2 = (float)(3.0 * (double)num1 * (double)num1 - 2.0 * (double)num1 * (double)num1 * (double)num1);
                trans.SetScale((float)(1.0 + (double)num2 * 0.299999952316284));
                instance.DrawQuad(this.m_upgradeQuads[1], VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                trans.SetScale(1f);
            }
            else
            {
                float num3 = this.m_upgradeButtonScaleTime / 0.15f;
                float num4 = (float)(3.0 * (double)num3 * (double)num3 - 2.0 * (double)num3 * (double)num3 * (double)num3);
                trans.SetScale((float)(1.0 + (double)num4 * 0.299999952316284));
                instance.DrawQuad(this.m_upgradeQuads[0], VertexFormat.Position3Texture, trans, this.m_textureEon, Color.White, 12);
                trans.SetScale(1f);
            }
            int num = -1;
            Color color = GameConstant.MENU_TEXT_COLOR;
            trans.SetPosition(240f, 0.0f);
            if (this.m_activeTab == 0)
            {
                if (this.m_selectedWeaponIndex < 0)
                {
                    num = 48;
                }
                else
                {
                    num = this.m_weaponLevels[this.m_selectedWeaponIndex] != 0 ? WEAPON_TEXTID[this.m_selectedWeaponIndex, 1] : WEAPON_TEXTID[this.m_selectedWeaponIndex, 0];
                    if (!this.m_upgradeButtonActive)
                        color = GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR;
                }
            }
            else if (this.m_activeTab == 1)
            {
                if (this.m_selectedNunUpgradeIndex < 0)
                {
                    num = 47;
                }
                else
                {
                    num = this.m_nunUpgradeLevels[this.m_selectedNunUpgradeIndex] != 0 ? NUNUPGRADE_TEXTID[this.m_selectedNunUpgradeIndex, 1] : NUNUPGRADE_TEXTID[this.m_selectedNunUpgradeIndex, 0];
                    if (!this.m_upgradeButtonActive)
                        color = GameConstant.MENU_TEXT_NOTAVAILABLE_COLOR;
                }
            }
            if (num >= 0)
                instance.DrawString(font, trans, 6, color, GameLocalisation.GetInstance().GetString((GameLocalisation.TextId)num));
            if (this.m_buttonBack.IsVisible())
            {
                trans.SetPosition(36f, 40f, 0.0f);
                instance.DrawQuad(this.m_quadBack, VertexFormat.Position3Texture, trans, this.m_textureMenu1);
            }
            if (this.m_buttonNext.IsVisible())
            {
                trans.SetPosition(440f, 40f, 0.0f);
                instance.DrawQuad(this.m_quadNext, VertexFormat.Position3Texture, trans, this.m_textureMenu1);
            }
            instance.End2DMode();
        }

        public bool IsDone() => this.m_done;

        public void CancelIsDone() => this.m_done = false;

        public void SetIsDone(bool value) => this.m_done = value;

        private void SelectWeapon(int weaponIndex, bool playSound)
        {
            if (weaponIndex >= 0 && this.m_weaponLevels[weaponIndex] < 5 && this.m_hero.Hearts >= WEAPON_COSTS[weaponIndex, this.m_weaponLevels[weaponIndex]])
            {
                this.m_upgradeButtonActive = true;
                this.m_upgradeButtonScaleTime = 0.0f;
            }
            else
                this.m_upgradeButtonActive = false;
            if (weaponIndex >= 0)
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Select);
            this.m_selectedWeaponIndex = weaponIndex;
        }

        private void SelectNunUpgrade(int upgradeIndex, bool playSound)
        {
            if (upgradeIndex >= 0 && this.m_nunUpgradeLevels[upgradeIndex] < 5 && this.m_hero.Hearts >= NUNUPGRADE_COSTS[upgradeIndex, this.m_nunUpgradeLevels[upgradeIndex]])
            {
                this.m_upgradeButtonActive = true;
                this.m_upgradeButtonScaleTime = 0.0f;
            }
            else
                this.m_upgradeButtonActive = false;
            if (upgradeIndex >= 0)
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Select);
            this.m_selectedNunUpgradeIndex = upgradeIndex;
        }

        private void SelectNunUpgrade(int upgradeIndex) => this.SelectNunUpgrade(upgradeIndex, true);

        private void SelectWeapon(int weaponIndex) => this.SelectWeapon(weaponIndex, true);

        public void SelectActiveTab(int tab)
        {
            this.SelectWeapon(-1);
            this.SelectNunUpgrade(-1);
            this.m_activeTab = tab;
            this.m_buttonBack.SetActive(tab != 0);
            this.m_buttonBack.SetVisible(tab != 0);
        }

        private bool AllWeaponLevelsFull()
        {
            for (int index = 0; index < 6; ++index)
            {
                if (this.m_weaponLevels[index] < 5)
                    return false;
            }
            return true;
        }

        private bool AllNunUpgradeLevelsFull()
        {
            for (int index = 0; index < 5; ++index)
            {
                if (this.m_nunUpgradeLevels[index] < 5)
                    return false;
            }
            return true;
        }

        private void CopyNunUpgradesAndWeaponsToHero()
        {
            Gun[] gunArray = this.m_hero.GetGunArray();
            for (int index = 0; index < 6; ++index)
            {
                if (this.m_weaponLevels[index] == 0)
                {
                    gunArray[index].IsAvailable = false;
                }
                else
                {
                    gunArray[index].IsAvailable = true;
                    gunArray[index].SetLevel(this.m_weaponLevels[index]);
                }
            }
            this.m_hero.SlashDamageLevel = this.m_nunUpgradeLevels[0];
            this.m_hero.SlashManaDrainLevel = this.m_nunUpgradeLevels[1];
            this.m_hero.HitPointsLevel = this.m_nunUpgradeLevels[2];
            this.m_hero.ManaPointsLevel = this.m_nunUpgradeLevels[3];
            if (this.m_nunUpgradeLevels[4] == 0)
            {
                this.m_hero.AirSlashAvailable = false;
            }
            else
            {
                this.m_hero.AirSlashAvailable = true;
                this.m_hero.AirSlashDamageLevel = this.m_nunUpgradeLevels[4];
            }
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active)
                return;
            ButtonActionEvent buttonActionEvent = (ButtonActionEvent)evt;
            if (!this.ButtonsEnabled)
                return;
            if (sender == this.m_buttonNext && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed)
            {
                if (this.m_activeTab == 1)
                {
                    this.CopyNunUpgradesAndWeaponsToHero();
                    this.m_done = true;
                    GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Validate);
                }
                else
                {
                    this.SelectActiveTab(1);
                    GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
                }
            }
            else if (sender == this.m_buttonBack && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed && this.m_activeTab == 1)
            {
                this.SelectActiveTab(0);
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_upgradeButton && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed && this.m_upgradeButtonActive)
            {
                if (this.m_selectedWeaponIndex >= 0)
                {
                    this.m_hero.Hearts -= WEAPON_COSTS[this.m_selectedWeaponIndex, this.m_weaponLevels[this.m_selectedWeaponIndex]];
                    ++this.m_weaponLevels[this.m_selectedWeaponIndex];
                    this.SelectWeapon(this.m_selectedWeaponIndex);
                    this.CopyNunUpgradesAndWeaponsToHero();
                    GameApplication.GetInstance().CheckUpgradesAchievements();
                }
                else if (this.m_selectedNunUpgradeIndex >= 0)
                {
                    this.m_hero.Hearts -= NUNUPGRADE_COSTS[this.m_selectedNunUpgradeIndex, this.m_nunUpgradeLevels[this.m_selectedNunUpgradeIndex]];
                    ++this.m_nunUpgradeLevels[this.m_selectedNunUpgradeIndex];
                    this.SelectNunUpgrade(this.m_selectedNunUpgradeIndex);
                    this.CopyNunUpgradesAndWeaponsToHero();
                    GameApplication.GetInstance().CheckUpgradesAchievements();
                }
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Upgrade);
            }
            else if (sender == this.m_gunTabButton && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed)
            {
                this.SelectActiveTab(0);
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (sender == this.m_nunTabButton && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed)
            {
                this.SelectActiveTab(1);
                GameApplication.GetInstance().PlayOutgameSound(GameApplication.OutgameSound.Next);
            }
            else if (this.m_activeTab == 0)
            {
                for (int weaponIndex = 0; weaponIndex < 6; ++weaponIndex)
                {
                    if (sender == this.m_weaponButtons[weaponIndex] && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed)
                    {
                        this.SelectWeapon(weaponIndex);
                        break;
                    }
                }
            }
            else
            {
                for (int upgradeIndex = 0; upgradeIndex < 5; ++upgradeIndex)
                {
                    if (sender == this.m_nunUpgradeButtons[upgradeIndex] && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed)
                    {
                        this.SelectNunUpgrade(upgradeIndex);
                        break;
                    }
                }
            }
        }
    }
}
