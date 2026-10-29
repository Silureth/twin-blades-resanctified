using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Game.FSM.GameState;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.World;

namespace Resanctified.Game.UI
{
    public class MapViewController : ViewController
    {
        private const float SCREEN_WIDTH = 480f;
        private const float SCREEN_HEIGHT = 320f;
        private const float MAP_TEXTURE_SIZE = 512f;
        private const float MAP_BUTTON_ENTER_LEFT = 1f;
        private const float MAP_BUTTON_ENTER_TOP = 450f;
        private const float MAP_BUTTON_ENTER_WIDTH = 60f;
        private const float MAP_BUTTON_ENTER_HEIGHT = 60f;
        private const float MAP_BUTTON_OBJECTIVE_LEFT = 131f;
        private const float MAP_BUTTON_OBJECTIVE_TOP = 450f;
        private const float MAP_BUTTON_OBJECTIVE_WIDTH = 60f;
        private const float MAP_BUTTON_OBJECTIVE_HEIGHT = 60f;
        private const float MAP_BUTTON_PAUSE_LEFT = 8f;
        private const float MAP_BUTTON_PAUSE_TOP = 416f;
        private const float MAP_BUTTON_PAUSE_WIDTH = 26f;
        private const float MAP_BUTTON_PAUSE_HEIGHT = 26f;
        private const float MAP_SHOP_POS_X = 222f;
        private const float MAP_SHOP_POS_Y = 193f;
        private const float MAP_TOWN_1_POS_X = 135f;
        private const float MAP_TOWN_1_POS_Y = 130f;
        private const float MAP_TOWN_2_POS_X = 195f;
        private const float MAP_TOWN_2_POS_Y = 79f;
        private const float MAP_TOWN_BOSS_POS_X = 296f;
        private const float MAP_TOWN_BOSS_POS_Y = 104f;
        private const float MAP_FOREST_1_POS_X = 351f;
        private const float MAP_FOREST_1_POS_Y = 185f;
        private const float MAP_FOREST_2_POS_X = 398f;
        private const float MAP_FOREST_2_POS_Y = 221f;
        private const float MAP_FOREST_BOSS_POS_X = 308f;
        private const float MAP_FOREST_BOSS_POS_Y = 274f;
        private const float MAP_GRAVEYARD_1_POS_X = 215f;
        private const float MAP_GRAVEYARD_1_POS_Y = 256f;
        private const float MAP_CHAPEL_1_POS_X = 156f;
        private const float MAP_CHAPEL_1_POS_Y = 218f;
        private const float MAP_CHAPEL_BOSS_POS_X = 102f;
        private const float MAP_CHAPEL_BOSS_POS_Y = 195f;
        private const float MAP_INFESTATION_ICON_OFFSET_X = -5f;
        private const float MAP_INFESTATION_ICON_OFFSET_Y = 15f;
        private const float MAP_SHOPWARNING_ICON_OFFSET_X = 0.0f;
        private const float MAP_SHOPWARNING_ICON_OFFSET_Y = 15f;
        private const float MAP_COMPASS_POS_X = 50f;
        private const float MAP_COMPASS_POS_Y = 255f;
        private const float MAP_COMPASS_DAY_NUMBER_POS_X = 18f;
        private const float MAP_COMPASS_DAY_NUMBER_POS_Y = 284f;
        private const float MAP_MISSION_TEXT_POS_X = 30f;
        private const float MAP_MISSION_TEXT_POS_Y = 60f;
        private const float MAP_BUTTON_PAUSE_POS_X = 240f;
        private const float MAP_BUTTON_PAUSE_POS_Y = 300f;
        private const int MAP_NUM_LEVELS = 10;
        private static float[] MAP_POS_ARRAY = new float[22]
        {
      222f,
      193f,
      135f,
      130f,
      195f,
      79f,
      296f,
      104f,
      351f,
      185f,
      398f,
      221f,
      308f,
      274f,
      215f,
      256f,
      156f,
      218f,
      102f,
      195f,
      215f,
      256f
        };
        private VertexPositionTexture[] m_quadOk = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadObjective = new VertexPositionTexture[4];
        private VertexPositionTexture[] m_quadPause = new VertexPositionTexture[4];
        private Engine.Resources.Texture m_mapTexture;
        private Button m_buttonOk;
        private Button m_buttonObjective;
        private Button m_buttonPause;
        private Button[] m_buttonLevel = new Button[10];
        private Animation2D m_map;
        private Animation2D m_shopWarning;
        private Animation2D m_infestation1;
        private Animation2D m_infestation2;
        private Animation2D m_infestation3;
        private Animation2D m_unlocked;
        private Animation2D m_unlockedBoss;
        private Animation2D m_hero;
        private Animation2D m_compass;
        private Transform m_t = new Transform();
        private int m_selectedItem;
        public bool ShopWarningEnabled;
        public bool ButtonsEnabled;

        public void RemapTextures()
        {
            this.m_map.GetModel().GetTextures()[0].Release();
            this.m_map.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_shopWarning.GetModel().GetTextures()[0].Release();
            this.m_shopWarning.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_infestation1.GetModel().GetTextures()[0].Release();
            this.m_infestation1.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_infestation2.GetModel().GetTextures()[0].Release();
            this.m_infestation2.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_infestation3.GetModel().GetTextures()[0].Release();
            this.m_infestation3.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_unlocked.GetModel().GetTextures()[0].Release();
            this.m_unlocked.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_unlockedBoss.GetModel().GetTextures()[0].Release();
            this.m_unlockedBoss.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_hero.GetModel().GetTextures()[0].Release();
            this.m_hero.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
            this.m_compass.GetModel().GetTextures()[0].Release();
            this.m_compass.GetModel().GetTextures()[0] = this.m_mapTexture;
            this.m_mapTexture.AddRef();
        }

        public GameWorld.LevelId GetSelectedLevelId() => (GameWorld.LevelId)(this.m_selectedItem + 3);

        public MapViewController()
        {
            this.m_mapTexture = null;
            this.m_selectedItem = 0;
            this.m_map = new Animation2D();
            this.m_infestation1 = new Animation2D();
            this.m_infestation2 = new Animation2D();
            this.m_infestation3 = new Animation2D();
            this.m_unlocked = new Animation2D();
            this.m_unlockedBoss = new Animation2D();
            this.m_shopWarning = new Animation2D();
            this.m_hero = new Animation2D();
            this.m_compass = new Animation2D();
            this.ShopWarningEnabled = false;
            this.ButtonsEnabled = true;
            this.m_buttonOk = new Button();
            this.m_buttonObjective = new Button();
            this.m_buttonPause = new Button();
            for (int index = 0; index < 10; ++index)
                this.m_buttonLevel[index] = new Button();
        }

        public override void Initialize() => this.Initialize(GameWorld.DayPart.Morning);

        public void Initialize(GameWorld.DayPart dayPart)
        {
            base.Initialize();
            Resource.SafeRelease(ref this.m_mapTexture);
            this.m_map.Initialize("Anims\\map");
            this.m_map.SetLoop(Animation2DLoop.Always);
            switch (dayPart)
            {
                case GameWorld.DayPart.Morning:
                    this.m_compass.Initialize("Anims\\map_compas_morning");
                    this.m_mapTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_map");
                    break;
                case GameWorld.DayPart.Day:
                    this.m_compass.Initialize("Anims\\map_compas_day");
                    this.m_mapTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_map");
                    break;
                case GameWorld.DayPart.Evening:
                    this.m_compass.Initialize("Anims\\map_compas_evening");
                    this.m_mapTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_map_night");
                    break;
                case GameWorld.DayPart.Night:
                    this.m_compass.Initialize("Anims\\map_compas_night");
                    this.m_mapTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_map_night");
                    break;
            }
            this.m_compass.SetLoop(Animation2DLoop.Always);
            this.m_infestation1.Initialize("Anims\\map_icon_orange");
            this.m_infestation1.SetLoop(Animation2DLoop.Always);
            this.m_infestation2.Initialize("Anims\\map_icon_red");
            this.m_infestation2.SetLoop(Animation2DLoop.Always);
            this.m_infestation3.Initialize("Anims\\map_icon_black");
            this.m_infestation3.SetLoop(Animation2DLoop.Always);
            this.m_unlocked.Initialize("Anims\\map_icon_lvl");
            this.m_unlocked.SetLoop(Animation2DLoop.Always);
            this.m_unlockedBoss.Initialize("Anims\\map_icon_boss");
            this.m_unlockedBoss.SetLoop(Animation2DLoop.Always);
            this.m_hero.Initialize("Anims\\map_icon_nun");
            this.m_hero.SetLoop(Animation2DLoop.Always);
            this.m_shopWarning.Initialize("Anims\\map_icon_exclamation");
            this.m_shopWarning.SetLoop(Animation2DLoop.Always);
            QuadFactory.CreateAnchoredQuad(this.m_quadObjective, 131f, 450f, 60f, 60f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            QuadFactory.CreateAnchoredQuad(this.m_quadOk, 1f, 450f, 60f, 60f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            QuadFactory.CreateAnchoredQuad(this.m_quadPause, 8f, 416f, 26f, 26f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
            this.m_buttonObjective.Initialize(this, 56f, 56f);
            this.m_buttonObjective.GetTransform().SetPosition(36f, 40f);
            this.m_buttonOk.Initialize(this, 56f, 56f);
            this.m_buttonOk.GetTransform().SetPosition(440f, 40f);
            this.m_buttonPause.Initialize(this, 56f, 56f);
            this.m_buttonPause.GetTransform().SetPosition(240f, 300f);
            for (int tag = 0; tag < 10; ++tag)
            {
                this.m_buttonLevel[tag].Initialize(this, 56f, 56f);
                this.m_buttonLevel[tag].SetTag(tag);
                this.m_buttonLevel[tag].GetTransform().SetPosition(MAP_POS_ARRAY[tag * 2], MAP_POS_ARRAY[tag * 2 + 1]);
            }
            this.m_selectedItem = (int)(((GameWorld)GameWorldManager.Instance.GetCurrentWorld()).LevelIndex - 3);
            this.ShopWarningEnabled = false;
            this.ButtonsEnabled = true;
        }

        public override void Cleanup()
        {
            this.m_buttonOk.Cleanup();
            this.m_buttonObjective.Cleanup();
            this.m_buttonPause.Cleanup();
            for (int index = 0; index < 10; ++index)
                this.m_buttonLevel[index].Cleanup();
            this.m_map.Cleanup();
            this.m_infestation1.Cleanup();
            this.m_infestation2.Cleanup();
            this.m_infestation3.Cleanup();
            this.m_unlocked.Cleanup();
            this.m_unlockedBoss.Cleanup();
            this.m_shopWarning.Cleanup();
            this.m_hero.Cleanup();
            this.m_compass.Cleanup();
            Resource.SafeRelease(ref this.m_mapTexture);
            base.Cleanup();
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Renderer instance = Renderer.GetInstance();
            Font font = GameApplication.GetInstance().GetFont();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            instance.Begin2DMode(false);
            this.m_t.Clear();
            instance.DrawAnimation2DModel(this.m_map.GetModel(), this.m_map.GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            this.m_t.SetPosition(50f, byte.MaxValue);
            instance.DrawAnimation2DModel(this.m_compass.GetModel(), this.m_compass.GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            this.m_t.SetScale(0.9f);
            this.m_t.SetPosition(18f, 284f);
            Renderer.GetInstance().DrawString(font, this.m_t, 12, GameConstant.MENU_TEXT_COLOR, currentWorld.DayIndex.ToString());
            this.m_t.SetScale(1f);
            if (this.ButtonsEnabled)
            {
                instance.DrawQuad(this.m_quadOk, VertexFormat.Position3Texture, this.m_buttonOk.GetTransform(), this.m_mapTexture);
                instance.DrawQuad(this.m_quadObjective, VertexFormat.Position3Texture, this.m_buttonObjective.GetTransform(), this.m_mapTexture);
                instance.DrawQuad(this.m_quadPause, VertexFormat.Position3Texture, this.m_buttonPause.GetTransform(), this.m_mapTexture);
            }
            for (int index = 0; index < 10; ++index)
            {
                Level level = currentWorld.GetLevel((GameWorld.LevelId)(index + 3));
                if (!level.Locked)
                {
                    this.m_t.SetPosition(MAP_POS_ARRAY[index * 2], MAP_POS_ARRAY[index * 2 + 1]);
                    Animation2D animation2D = level.GetLevelType() == Level.LevelType.Boss ? this.m_unlockedBoss : this.m_unlocked;
                    instance.DrawAnimation2DModel(animation2D.GetModel(), animation2D.GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha, false);
                }
            }
            this.m_t.SetPosition(MAP_POS_ARRAY[this.m_selectedItem * 2], MAP_POS_ARRAY[this.m_selectedItem * 2 + 1]);
            instance.DrawAnimation2DModel(this.m_hero.GetModel(), this.m_hero.GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            if (this.ShopWarningEnabled)
            {
                this.m_t.SetPosition(MAP_POS_ARRAY[0] + 0.0f, MAP_POS_ARRAY[1] + 15f);
                instance.DrawAnimation2DModel(this.m_shopWarning.GetModel(), this.m_shopWarning.GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            }
            for (int index = 0; index < 10; ++index)
            {
                Level level = currentWorld.GetLevel((GameWorld.LevelId)(index + 3));
                if (!level.Locked)
                {
                    this.m_t.SetPosition(MAP_POS_ARRAY[index * 2] - 5f, MAP_POS_ARRAY[index * 2 + 1] + 15f);
                    Animation2D animation2D = null;
                    switch (level.GetInfestationLevel())
                    {
                        case 1:
                            animation2D = this.m_infestation1;
                            break;
                        case 2:
                            animation2D = this.m_infestation2;
                            break;
                        case 3:
                            animation2D = this.m_infestation3;
                            break;
                    }
                    if (animation2D != null)
                        instance.DrawAnimation2DModel(animation2D.GetModel(), animation2D.GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha, false);
                }
            }
            instance.End2DMode();
        }

        public override void Update(float elapsedTime)
        {
            if (!this.m_active)
                return;
            this.m_map.Update(elapsedTime);
            this.m_infestation1.Update(elapsedTime);
            this.m_infestation2.Update(elapsedTime);
            this.m_infestation3.Update(elapsedTime);
            this.m_unlocked.Update(elapsedTime);
            this.m_unlockedBoss.Update(elapsedTime);
            this.m_shopWarning.Update(elapsedTime);
            this.m_hero.Update(elapsedTime);
            this.m_compass.Update(elapsedTime);
            base.Update(elapsedTime);
        }

        public override void Action(View sender, ActionEvent evt)
        {
            if (!this.m_active || !this.ButtonsEnabled || ((ButtonActionEvent)evt).buttonState != PSSButtonState.ButtonStatePressed)
                return;
            if (sender == this.m_buttonOk)
                GameStateMap.GetInstance().EnterLevelRequest = true;
            else if (sender == this.m_buttonObjective)
                GameStateMap.GetInstance().ShowObjectivesRequest = true;
            else if (sender == this.m_buttonPause)
            {
                InGameStateMapNavigation.GetInstance().InGameMenuRequest = true;
            }
            else
            {
                Button button = (Button)sender;
                if (((GameWorld)WorldManager.GetInstance().GetCurrentWorld()).GetLevel((GameWorld.LevelId)(button.GetTag() + 3)).Locked)
                    return;
                this.m_selectedItem = button.GetTag();
            }
        }
    }
}
