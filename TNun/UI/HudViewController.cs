// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.HudViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM.GameState.InGame;
using PSS.Game.NunVsZombies.World;
using PSS.Game.NunVsZombies.World.Entities;
using PSS.GameEngine;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Graphics.Renderer;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Tools;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;
using System.Text;

namespace PSS.Game.NunVsZombies.UI
{
  public class HudViewController : ViewController
  {
    private const float FX2_TEXTURE_WIDTH = 512f;
    private const float FX2_TEXTURE_HEIGHT = 512f;
    private const float FX2_LIFEBAR_LEFT = 362f;
    private const float FX2_LIFEBAR_TOP = 192f;
    private const float FX2_LIFEBAR_WIDTH = 16f;
    private const float FX2_LIFEBAR_HEIGHT = 303f;
    private const float FX2_CONTAINER_LEFT = 335f;
    private const float FX2_CONTAINER_TOP = 188f;
    private const float FX2_CONTAINER_WIDTH = 18f;
    private const float FX2_CONTAINER_HEIGHT = 309f;
    private const float HUD64_TEXTURE_WIDTH = 64f;
    private const float HUD64_TEXTURE_HEIGHT = 64f;
    private const float HUD64_ZOMBIE_LEFT = 36f;
    private const float HUD64_ZOMBIE_TOP = 0.0f;
    private const float HUD64_ZOMBIE_WIDTH = 27f;
    private const float HUD64_ZOMBIE_HEIGHT = 27f;
    private const float HUD_TEXTURE_WIDTH = 256f;
    private const float HUD_TEXTURE_HEIGHT = 256f;
    private const float HUD_LIFEBAR_LEFT = 1f;
    private const float HUD_LIFEBAR_TOP = 1f;
    private const float HUD_LIFEBAR_WIDTH = 72f;
    private const float HUD_LIFEBAR_HEIGHT = 16f;
    private const float HUD_CONTAINER_LEFT = 0.0f;
    private const float HUD_CONTAINER_TOP = 18f;
    private const float HUD_CONTAINER_WIDTH = 143f;
    private const float HUD_CONTAINER_HEIGHT = 80f;
    private const float HUD_SOCKET_LEFT = 165f;
    private const float HUD_SOCKET_TOP = 1f;
    private const float HUD_SOCKET_WIDTH = 90f;
    private const float HUD_SOCKET_HEIGHT = 90f;
    private const float HUD_MANA_LEFT = 115f;
    private const float HUD_MANA_TOP = 101f;
    private const float HUD_MANA_WIDTH = 38f;
    private const float HUD_MANA_HEIGHT = 38f;
    private const float HUD_THUMBSTICK_LEFT = 155f;
    private const float HUD_THUMBSTICK_TOP = 95f;
    private const float HUD_THUMBSTICK_WIDTH = 53f;
    private const float HUD_THUMBSTICK_HEIGHT = 44f;
    private const float HUD_SELECTOR_LEFT = 210f;
    private const float HUD_SELECTOR_TOP = 96f;
    private const float HUD_SELECTOR_WIDTH = 44f;
    private const float HUD_SELECTOR_HEIGHT = 44f;
    private const float HUD_ICEGUN_LEFT = 2f;
    private const float HUD_ICEGUN_TOP = 136f;
    private const float HUD_ICEGUN_WIDTH = 56f;
    private const float HUD_ICEGUN_HEIGHT = 56f;
    private const float HUD_MACHINEGUN_LEFT = 66f;
    private const float HUD_MACHINEGUN_TOP = 136f;
    private const float HUD_MACHINEGUN_WIDTH = 56f;
    private const float HUD_MACHINEGUN_HEIGHT = 56f;
    private const float HUD_FLAMETHROWER_LEFT = 142f;
    private const float HUD_FLAMETHROWER_TOP = 141f;
    private const float HUD_FLAMETHROWER_WIDTH = 56f;
    private const float HUD_FLAMETHROWER_HEIGHT = 56f;
    private const float HUD_SIMPLEGUN_LEFT = 199f;
    private const float HUD_SIMPLEGUN_TOP = 141f;
    private const float HUD_SIMPLEGUN_WIDTH = 56f;
    private const float HUD_SIMPLEGUN_HEIGHT = 56f;
    private const float HUD_SCYTHEBUTTON_LEFT = 0.0f;
    private const float HUD_SCYTHEBUTTON_TOP = 193f;
    private const float HUD_SCYTHEBUTTON_WIDTH = 62f;
    private const float HUD_SCYTHEBUTTON_HEIGHT = 62f;
    private const float HUD_GUNBUTTON_LEFT = 63f;
    private const float HUD_GUNBUTTON_TOP = 193f;
    private const float HUD_GUNBUTTON_WIDTH = 62f;
    private const float HUD_GUNBUTTON_HEIGHT = 62f;
    private const float HUD_EXPLOSIVEGUN_LEFT = 141f;
    private const float HUD_EXPLOSIVEGUN_TOP = 198f;
    private const float HUD_EXPLOSIVEGUN_WIDTH = 56f;
    private const float HUD_EXPLOSIVEGUN_HEIGHT = 56f;
    private const float HUD_HOLYBEAM_LEFT = 199f;
    private const float HUD_HOLYBEAM_TOP = 198f;
    private const float HUD_HOLYBEAM_WIDTH = 56f;
    private const float HUD_HOLYBEAM_HEIGHT = 56f;
    private const float HUD_PAUSE_LEFT = 6f;
    private const float HUD_PAUSE_TOP = 107f;
    private const float HUD_PAUSE_WIDTH = 14f;
    private const float HUD_PAUSE_HEIGHT = 26f;
    private const float HUD_HEART_LEFT = 27f;
    private const float HUD_HEART_TOP = 101f;
    private const float HUD_HEART_WIDTH = 50f;
    private const float HUD_HEART_HEIGHT = 35f;
    private const float HUD_NUN_LEFT = 78f;
    private const float HUD_NUN_TOP = 99f;
    private const float HUD_NUN_WIDTH = 35f;
    private const float HUD_NUN_HEIGHT = 34f;
    private const float HUD_TOUCH_BUTTON_DETECTION_RANGE = 40f;
    private const float HUD_SELECTOR_BUTTON_SCALE = 1.8f;
    private const float HUD_TOUCH_THUMBSTICK_DETECTION_RANGE = 150f;
    private const float HUD_SELECTOR_THUMBSTICK_SCALE = 2.2f;
    private const float HUD_THUMBSTICK_RANGE = 19f;
    private const float HUD_SOCKET_SCALE = 0.85f;
    private const float HUD_THUMBSTICK_SCALE = 0.85f;
    private const float HUD_TOUCH_WEAPONSWITCH_DETECTION_RANGE = 50f;
    private const float HUD_WEAPONSWITCH_MIN_SPEED_X = 40000f;
    private const float HUD_WEAPONSWITCH_MIN_SPEED_Y = 300f;
    private const float HUD_SCYTHEBUTTON_BUTTON_CX = 344f;
    private const float HUD_SCYTHEBUTTON_BUTTON_CY = 40f;
    private const float HUD_SCYTHEBUTTON_SCALE = 1f;
    private const float HUD_GUNBUTTON_BUTTON_CX = 424f;
    private const float HUD_GUNBUTTON_BUTTON_CY = 40f;
    private const float HUD_GUNBUTTON_SCALE = 1f;
    private const float HUD_THUMBSTICK_CX = 70f;
    private const float HUD_THUMBSTICK_CY = 40f;
    private const float HUD_WEAPONSWITCH_CX = 442f;
    private const float HUD_WEAPONSWITCH_CY = 240f;
    private const float HUD_WEAPONSWITCH_SCALE = 1f;
    private const float HUD_OTHERWEAPON_CX = 460f;
    private const float HUD_OTHERWEAPON_CY = 240f;
    private const float HUD_OTHERWEAPON_OFFSET_Y = 31f;
    private const float HUD_OTHERWEAPON_SCALE = 0.6f;
    private const float HUD_PAUSE_BUTTON_CX = 240f;
    private const float HUD_PAUSE_BUTTON_CY_STORY = 268f;
    private const float HUD_PAUSE_BUTTON_CY_SURVIVAL = 298f;
    private const float HUD_PAUSE_BUTTON_WIDTH = 40f;
    private const float HUD_PAUSE_BUTTON_HEIGHT = 40f;
    private const float HUD_HEART_CX = 442f;
    private const float HUD_HEART_CY = 296f;
    private const float HUD_HEARTSTR_X = 410f;
    private const float HUD_HEARTSTR_Y = 283f;
    private const float HUD_SCORE_X = 150f;
    private const float HUD_SCORE_Y = 283f;
    private const float HUD_ZOMBIENB_X = 245f;
    private const float HUD_ZOMBIENB_Y = 283f;
    private const float HUD_ZOMBIE_X = 240f;
    private const float HUD_ZOMBIE_Y = 278f;
    private const float HUD_CONTAINER_X = 10f;
    private const float HUD_CONTAINER_Y = 310f;
    private const float HUD_BOSS_CONTAINER_X = 85.5f;
    private const float HUD_BOSS_CONTAINER_Y = 321f;
    private const float HUD_MANA_X = 35f;
    private const float HUD_MANA_Y = -41f;
    private const float HUD_MANA_SCALE = 1.15f;
    private const float HUD_LIFEBAR_X = 67f;
    private const float HUD_LIFEBAR_Y = -32f;
    private const float HUD_BOSS_LIFEBAR_X = 0.0f;
    private const float HUD_BOSS_LIFEBAR_Y = 2f;
    private const float HUD_MANA_LERP_FACTOR = 0.9f;
    private const float HUD_LIFEBAR_LERP_FACTOR = 0.9f;
    private const float HUD_LEFTARROW_X = 50f;
    private const float HUD_LEFTARROW_Y = 170f;
    private const float HUD_RIGHTARROW_X = 430f;
    private const float HUD_RIGHTARROW_Y = 170f;
    private static Color HUD_OTHERWEAPON_COLOR = new Color(80, 80, 80, (int) byte.MaxValue);
    public bool HelpScreenEnabled;
    private VertexPositionColor[] m_quadBackground = new VertexPositionColor[4];
    private VertexPositionTexture[] m_quadLifebar = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadContainer = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadSocket = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadMana = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadThumbstick = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadSelectorButton = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadSelectorThumbstick = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadIceGun = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadMachineGun = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadFlameThrower = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadSimpleGun = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadExplosiveGun = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadHolyBeam = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadScytheButton = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadGunButton = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadPause = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadHeart = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBossLifebar = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadBossContainer = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadZombie = new VertexPositionTexture[4];
    private TexturedRoundThumbstick m_leftThumbstick;
    private TexturedCircularButton m_buttonScythe;
    private TexturedCircularButton m_buttonGun;
    private TexturedSwipeCircularButton m_weaponSwitch;
    private Button m_pauseButton;
    private PSS.GameEngine.Resources.Texture m_hudTexture;
    private PSS.GameEngine.Resources.Texture m_hud64Texture;
    private PSS.GameEngine.Resources.Texture m_fx2Texture;
    private int m_heroHearts;
    private VertexPositionTexture[][] m_weaponQuads = new VertexPositionTexture[6][];
    private int m_weaponIndex;
    private int m_prevWeaponIndex;
    private int m_nextWeaponIndex;
    private Transform m_containerTransform = new Transform();
    private Transform m_manaTransform = new Transform();
    private Transform m_lifebarTransform = new Transform();
    private Transform m_heartTransform = new Transform();
    private Transform m_heartStrTransform = new Transform();
    private Transform m_scoreTransform = new Transform();
    private Transform m_prevWeaponTransform = new Transform();
    private Transform m_nextWeaponTransform = new Transform();
    private Transform m_zombiesAliveTransform = new Transform();
    private Transform m_bossContainerTransform = new Transform();
    private Transform m_bossLifebarTransform = new Transform();
    private Transform m_arrowTransform = new Transform();
    private float m_manaScale;
    private Animation2D m_bloodAnim = new Animation2D();
    private bool m_bloodAnimPlaying;
    private Animation2D m_shakeAnimEn = new Animation2D();
    private Animation2D m_shakeAnimFr = new Animation2D();
    private Animation2D m_shakeAnimIt = new Animation2D();
    private Animation2D m_shakeAnimGe = new Animation2D();
    private Animation2D m_shakeAnimSp = new Animation2D();
    private bool m_shakeAnimPlaying;
    private Animation2D m_arrowAnim = new Animation2D();
    private bool m_leftArrowAnimPlaying;
    private bool m_rightArrowAnimPlaying;
    private StringBuilder m_stringBuilder = new StringBuilder(16);
    private NPCBoss m_boss;
    private int m_bossHitPointsMax;

    private void HudViewController_1()
    {
      QuadFactory.CreateAnchoredQuad(this.m_quadContainer, 0.0f, 18f, 143f, 80f, 256f, 256f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadBossContainer, 335f, 188f, 18f, 309f, 512f, 512f, Anchor.Bottom);
      QuadFactory.CreateCenteredScaledQuad(this.m_quadSocket, 165f, 1f, 90f, 90f, 256f, 256f, 0.85f);
      QuadFactory.CreateCenteredScaledQuad(this.m_quadThumbstick, 155f, 95f, 53f, 44f, 256f, 256f, 0.85f);
      QuadFactory.CreateCenteredScaledQuad(this.m_quadSelectorButton, 210f, 96f, 44f, 44f, 256f, 256f, 1.8f);
      QuadFactory.CreateCenteredScaledQuad(this.m_quadSelectorThumbstick, 210f, 96f, 44f, 44f, 256f, 256f, 2.2f);
    }

    public HudViewController()
    {
      this.m_leftThumbstick = new TexturedRoundThumbstick();
      this.m_buttonScythe = new TexturedCircularButton();
      this.m_buttonGun = new TexturedCircularButton();
      this.m_weaponSwitch = new TexturedSwipeCircularButton();
      this.m_pauseButton = new Button();
      this.m_hudTexture = (PSS.GameEngine.Resources.Texture) null;
      this.m_hud64Texture = (PSS.GameEngine.Resources.Texture) null;
      this.HelpScreenEnabled = false;
      this.HudViewController_1();
      QuadFactory.CreateCenteredQuad(this.m_quadIceGun, 2f, 136f, 56f, 56f, 256f, 256f);
      QuadFactory.CreateCenteredQuad(this.m_quadMachineGun, 66f, 136f, 56f, 56f, 256f, 256f);
      QuadFactory.CreateCenteredQuad(this.m_quadFlameThrower, 142f, 141f, 56f, 56f, 256f, 256f);
      QuadFactory.CreateCenteredQuad(this.m_quadSimpleGun, 199f, 141f, 56f, 56f, 256f, 256f);
      QuadFactory.CreateCenteredQuad(this.m_quadExplosiveGun, 141f, 198f, 56f, 56f, 256f, 256f);
      QuadFactory.CreateCenteredQuad(this.m_quadHolyBeam, 199f, 198f, 56f, 56f, 256f, 256f);
      QuadFactory.CreateCenteredScaledQuad(this.m_quadScytheButton, 0.0f, 193f, 62f, 62f, 256f, 256f, 1f);
      QuadFactory.CreateCenteredScaledQuad(this.m_quadGunButton, 63f, 193f, 62f, 62f, 256f, 256f, 1f);
      QuadFactory.CreateQuad(this.m_quadBackground, 0.0f, 320f, 480f, 320f, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      QuadFactory.CreateAnchoredQuad(this.m_quadPause, 6f, 107f, 14f, 26f, 256f, 256f, Anchor.Left);
      QuadFactory.CreateCenteredQuad(this.m_quadHeart, 27f, 101f, 50f, 35f, 256f, 256f);
      QuadFactory.CreateAnchoredQuad(this.m_quadZombie, 36f, 0.0f, 27f, 27f, 64f, 64f, Anchor.Right | Anchor.Bottom);
      this.m_weaponQuads[0] = this.m_quadSimpleGun;
      this.m_weaponQuads[4] = this.m_quadExplosiveGun;
      this.m_weaponQuads[1] = this.m_quadMachineGun;
      this.m_weaponQuads[3] = this.m_quadIceGun;
      this.m_weaponQuads[2] = this.m_quadFlameThrower;
      this.m_weaponQuads[5] = this.m_quadHolyBeam;
    }

    public void Initialize(PlayableCharacter hero, NPCBoss boss)
    {
      this.Initialize();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_hudTexture);
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_hud64Texture);
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_fx2Texture);
      this.m_hudTexture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_hud");
      this.m_hud64Texture = ResourceManager.GetInstance().LoadTexture("Textures\\texture_hud_64");
      this.m_fx2Texture = ResourceManager.GetInstance().LoadTexture("Textures\\fx2");
      this.m_manaScale = (float) hero.ManaPoints / (float) hero.GetManaPointsMax();
      this.CreateManaQuad();
      QuadFactory.CreateAnchoredQuad(this.m_quadLifebar, 1f, 1f, 72f, 16f, 256f, 256f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadBossLifebar, 362f, 192f, 16f, 303f, 512f, 512f, Anchor.Bottom);
      this.m_buttonScythe.Initialize((ViewController) this, 40f, this.m_quadSelectorButton, this.m_quadScytheButton, this.m_hudTexture, this.m_hudTexture);
      this.m_buttonScythe.GetTransform().SetPosition(344f, 40f);
      this.m_buttonGun.Initialize((ViewController) this, 40f, this.m_quadSelectorButton, this.m_quadGunButton, this.m_hudTexture, this.m_hudTexture);
      this.m_buttonGun.GetTransform().SetPosition(424f, 40f);
      this.m_leftThumbstick.Initialize((ViewController) this, PSSThumbstick.LeftThumbstick, 150f, 19f, this.m_quadSelectorThumbstick, this.m_quadSocket, this.m_quadThumbstick, this.m_hudTexture, this.m_hudTexture, this.m_hudTexture);
      this.m_leftThumbstick.GetTransform().SetPosition(70f, 40f);
      this.m_weaponSwitch.Initialize((ViewController) this, 50f, 40000f, 300f);
      this.m_weaponSwitch.GetTransform().SetPosition(442f, 240f);
      this.m_weaponSwitch.GetTransform().SetScale(1f);
      this.SetCurrentGunIcon(hero);
      this.m_pauseButton.Initialize((ViewController) this, 40f, 40f);
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
        this.m_pauseButton.GetTransform().SetPosition(240f, 268f);
      else
        this.m_pauseButton.GetTransform().SetPosition(240f, 298f);
      this.m_lifebarTransform.SetParent(this.m_containerTransform);
      this.m_bossLifebarTransform.SetParent(this.m_bossContainerTransform);
      this.m_manaTransform.SetParent(this.m_containerTransform);
      this.m_containerTransform.SetPosition(10f, 310f);
      this.m_bossContainerTransform.SetPosition(85.5f, 321f);
      this.m_bossContainerTransform.SetRotationZ(-1.570796f);
      this.m_manaTransform.SetPosition(35f, -41f);
      this.m_lifebarTransform.SetPosition(67f, -32f);
      this.m_lifebarTransform.SetScale((float) hero.HitPoints / (float) hero.GetHitPointsMax(), 1f);
      this.m_bossLifebarTransform.SetPosition(0.0f, 2f);
      this.m_bossLifebarTransform.SetScale(1f);
      this.m_heartTransform.SetPosition(442f, 296f);
      this.m_heartStrTransform.SetPosition(410f, 283f);
      this.m_scoreTransform.SetPosition(150f, 283f);
      this.m_nextWeaponTransform.SetPosition(460f, 271f);
      this.m_nextWeaponTransform.SetScale(0.6f);
      this.m_prevWeaponTransform.SetPosition(460f, 209f);
      this.m_prevWeaponTransform.SetScale(0.6f);
      this.m_bloodAnim.Initialize("Anims\\fx_blood");
      this.m_bloodAnimPlaying = false;
      this.m_shakeAnimEn.Initialize("Anims\\shake");
      this.m_shakeAnimEn.SetLoop(Animation2DLoop.Always);
      this.m_shakeAnimFr.Initialize("Anims\\shake_fr");
      this.m_shakeAnimFr.SetLoop(Animation2DLoop.Always);
      this.m_shakeAnimIt.Initialize("Anims\\shake_it");
      this.m_shakeAnimIt.SetLoop(Animation2DLoop.Always);
      this.m_shakeAnimGe.Initialize("Anims\\shake_al");
      this.m_shakeAnimGe.SetLoop(Animation2DLoop.Always);
      this.m_shakeAnimSp.Initialize("Anims\\shake_es");
      this.m_shakeAnimSp.SetLoop(Animation2DLoop.Always);
      this.m_shakeAnimPlaying = false;
      this.m_arrowAnim.Initialize("Anims\\fleche");
      this.m_arrowAnim.SetLoop(Animation2DLoop.Always);
      this.m_leftArrowAnimPlaying = false;
      this.m_rightArrowAnimPlaying = false;
      this.m_heroHearts = hero.Hearts;
      this.m_boss = boss;
      if (boss != null)
        this.m_bossHitPointsMax = boss.HitPoints;
      this.AddSubview((View) this.m_buttonScythe);
      this.AddSubview((View) this.m_buttonGun);
      this.AddSubview((View) this.m_leftThumbstick);
      this.AddSubview((View) this.m_weaponSwitch);
    }

    public override void Cleanup()
    {
      Application.GetInstance().RemoveResponder((Responder) this);
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_hudTexture);
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_hud64Texture);
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_fx2Texture);
      this.m_buttonGun.Cleanup();
      this.m_buttonScythe.Cleanup();
      this.m_leftThumbstick.Cleanup();
      this.m_weaponSwitch.Cleanup();
      this.m_pauseButton.Cleanup();
      this.m_bloodAnim.Cleanup();
      this.m_shakeAnimEn.Cleanup();
      this.m_shakeAnimFr.Cleanup();
      this.m_shakeAnimIt.Cleanup();
      this.m_shakeAnimGe.Cleanup();
      this.m_shakeAnimSp.Cleanup();
      this.m_arrowAnim.Cleanup();
      this.m_boss = (NPCBoss) null;
      base.Cleanup();
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      Font font = GameApplication.Instance.GetFont();
      instance.Begin2DMode(false);
      if (this.HelpScreenEnabled)
        instance.DrawQuad<VertexPositionColor>(this.m_quadBackground, VertexFormat.Position3Color, (Transform) null, (PSS.GameEngine.Resources.Texture) null, GameConstant.INGAME_BLACKBACKGROUND_COLOR);
      if (this.m_nextWeaponIndex >= 0)
        instance.DrawQuad<VertexPositionTexture>(this.m_weaponQuads[this.m_nextWeaponIndex], VertexFormat.Position3Texture, this.m_nextWeaponTransform, this.m_hudTexture, HudViewController.HUD_OTHERWEAPON_COLOR);
      if (this.m_prevWeaponIndex >= 0)
        instance.DrawQuad<VertexPositionTexture>(this.m_weaponQuads[this.m_prevWeaponIndex], VertexFormat.Position3Texture, this.m_prevWeaponTransform, this.m_hudTexture, HudViewController.HUD_OTHERWEAPON_COLOR);
      Color white = Color.White with
      {
        A = (byte) ((double) GameApplication.Instance.HudAlpha * (double) byte.MaxValue)
      };
      this.m_buttonGun.SetCustomColor(white);
      this.m_buttonScythe.SetCustomColor(white);
      this.m_leftThumbstick.SetCustomColor(white);
      for (int index = 0; index < this.m_subviews.Count; ++index)
        this.DrawSubview(this.m_subviews[index]);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadMana, VertexFormat.Position3Texture, this.m_manaTransform, this.m_hudTexture);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadLifebar, VertexFormat.Position3Texture, this.m_lifebarTransform, this.m_hudTexture, Color.White);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadContainer, VertexFormat.Position3Texture, this.m_containerTransform, this.m_hudTexture);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadPause, VertexFormat.Position3Texture, this.m_pauseButton.GetTransform(), this.m_hudTexture, Color.White, 12);
      instance.DrawQuad<VertexPositionTexture>(this.m_quadHeart, VertexFormat.Position3Texture, this.m_heartTransform, this.m_hudTexture);
      this.m_stringBuilder.Length = 0;
      StringHelper.Append(this.m_stringBuilder, this.m_heroHearts, 3).Append(" x");
      instance.DrawString(font, this.m_heartStrTransform, 3, Color.White, this.m_stringBuilder);
      this.m_stringBuilder.Length = 0;
      StringHelper.Append(this.m_stringBuilder, GameApplication.GetInstance().Score, 6);
      instance.DrawString(font, this.m_scoreTransform, 3, Color.White, this.m_stringBuilder);
      if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
      {
        GameWorld currentWorld = (GameWorld) WorldManager.GetInstance().GetCurrentWorld();
        if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
        {
          instance.DrawQuad<VertexPositionTexture>(this.m_quadBossLifebar, VertexFormat.Position3Texture, this.m_bossLifebarTransform, this.m_fx2Texture);
          instance.DrawQuad<VertexPositionTexture>(this.m_quadBossContainer, VertexFormat.Position3Texture, this.m_bossContainerTransform, this.m_fx2Texture);
        }
        else
        {
          this.m_stringBuilder.Length = 0;
          StringHelper.Append(this.m_stringBuilder, currentWorld.ZombiesLeftToKill + currentWorld.ZombiesLeftToKillInit * (4 - currentWorld.GetLevelSectorIndex()), 3);
          this.m_zombiesAliveTransform.SetPosition(245f, 283f);
          instance.DrawString(font, this.m_zombiesAliveTransform, 2, Color.White, this.m_stringBuilder);
          this.m_zombiesAliveTransform.SetPosition(240f, 278f);
          instance.DrawQuad<VertexPositionTexture>(this.m_quadZombie, VertexFormat.Position3Texture, this.m_zombiesAliveTransform, this.m_hud64Texture);
        }
      }
      if (this.m_bloodAnimPlaying && !this.HelpScreenEnabled)
        instance.DrawAnimation2DModel(this.m_bloodAnim.GetModel(), this.m_bloodAnim.GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha, false);
      if (this.m_shakeAnimPlaying && !this.HelpScreenEnabled)
      {
        switch (GameApplication.GetInstance().CurrentLanguage)
        {
          case GameLocalisation.LanguageId.English:
            instance.DrawAnimation2DModel(this.m_shakeAnimEn.GetModel(), this.m_shakeAnimEn.GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            break;
          case GameLocalisation.LanguageId.French:
            instance.DrawAnimation2DModel(this.m_shakeAnimFr.GetModel(), this.m_shakeAnimFr.GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            break;
          case GameLocalisation.LanguageId.Italian:
            instance.DrawAnimation2DModel(this.m_shakeAnimIt.GetModel(), this.m_shakeAnimIt.GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            break;
          case GameLocalisation.LanguageId.German:
            instance.DrawAnimation2DModel(this.m_shakeAnimGe.GetModel(), this.m_shakeAnimGe.GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            break;
          case GameLocalisation.LanguageId.Spanish:
            instance.DrawAnimation2DModel(this.m_shakeAnimSp.GetModel(), this.m_shakeAnimSp.GetFrame(), (Transform) null, BlendFunc.SrcAlpha_InvSrcAlpha, false);
            break;
        }
      }
      if (this.m_leftArrowAnimPlaying && !this.HelpScreenEnabled)
      {
        this.m_arrowTransform.SetPosition(50f, 170f);
        this.m_arrowTransform.SetScale(1f);
        instance.DrawAnimation2DModel(this.m_arrowAnim.GetModel(), this.m_arrowAnim.GetFrame(), this.m_arrowTransform, BlendFunc.SrcAlpha_InvSrcAlpha);
      }
      if (this.m_rightArrowAnimPlaying && !this.HelpScreenEnabled)
      {
        this.m_arrowTransform.SetPosition(430f, 170f);
        this.m_arrowTransform.SetScale(-1f, 1f);
        instance.DrawAnimation2DModel(this.m_arrowAnim.GetModel(), this.m_arrowAnim.GetFrame(), this.m_arrowTransform, BlendFunc.SrcAlpha_InvSrcAlpha);
      }
      instance.End2DMode();
    }

    public void Update(float elapsedTime, PlayableCharacter hero)
    {
      if (!this.m_active)
        return;
      this.Update(elapsedTime);
      this.m_heroHearts = hero.Hearts;
      float num1 = MathHelper.Clamp((float) hero.HitPoints / (float) hero.GetHitPointsMax(), 0.0f, 1f);
      Vector2 scale1 = this.m_lifebarTransform.GetScale();
      scale1.X = num1 + (float) (((double) scale1.X - (double) num1) * 0.899999976158142);
      scale1.Y = 1f;
      this.m_lifebarTransform.SetScale(scale1);
      if (this.m_boss != null)
      {
        float num2 = MathHelper.Clamp((float) this.m_boss.HitPoints / (float) this.m_bossHitPointsMax, 0.0f, 1f);
        Vector2 scale2 = this.m_bossLifebarTransform.GetScale();
        scale2.Y = num2 + (float) (((double) scale2.Y - (double) num2) * 0.899999976158142);
        scale2.X = 1f;
        this.m_bossLifebarTransform.SetScale(scale2);
      }
      float num3 = MathHelper.Clamp((float) hero.ManaPoints / (float) hero.GetManaPointsMax(), 0.0f, 1f);
      this.m_manaScale = num3 + (float) (((double) this.m_manaScale - (double) num3) * 0.899999976158142);
      this.CreateManaQuad();
      if (this.m_bloodAnimPlaying)
      {
        this.m_bloodAnim.Update(elapsedTime);
        if (this.m_bloodAnim.IsEndOfAnimationReached())
          this.m_bloodAnimPlaying = false;
      }
      if (this.m_shakeAnimPlaying)
      {
        this.m_shakeAnimEn.Update(elapsedTime);
        this.m_shakeAnimFr.Update(elapsedTime);
        this.m_shakeAnimIt.Update(elapsedTime);
        this.m_shakeAnimGe.Update(elapsedTime);
        this.m_shakeAnimSp.Update(elapsedTime);
      }
      if (!this.m_leftArrowAnimPlaying && !this.m_rightArrowAnimPlaying)
        return;
      this.m_arrowAnim.Update(elapsedTime);
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || this.HelpScreenEnabled)
        return;
      if (sender == this.m_buttonScythe)
        InputManager.GetInstance().SetPadButtonsState(PSSButton.ButtonB, ((ButtonActionEvent) evt).buttonState);
      else if (sender == this.m_buttonGun)
        InputManager.GetInstance().SetPadButtonsState(PSSButton.ButtonX, ((ButtonActionEvent) evt).buttonState);
      else if (sender == this.m_leftThumbstick)
      {
        RoundThumbstickActionEvent thumbstickActionEvent = (RoundThumbstickActionEvent) evt;
        InputManager.GetInstance().SetThumbstickPosition(thumbstickActionEvent.thumbstick, thumbstickActionEvent.position);
      }
      else if (sender == this.m_weaponSwitch)
      {
        switch (((SwipeCircularButtonActionEvent) evt).direction)
        {
          case SwipeDirection.Up:
            InputManager.GetInstance().SetPadButtonsState(PSSButton.ButtonLeftShoulder, PSSButtonState.ButtonStatePressed);
            InputManager.GetInstance().SetPadButtonsState(PSSButton.ButtonLeftShoulder, PSSButtonState.ButtonStateReleased);
            break;
          case SwipeDirection.Down:
            InputManager.GetInstance().SetPadButtonsState(PSSButton.ButtonRightShoulder, PSSButtonState.ButtonStatePressed);
            InputManager.GetInstance().SetPadButtonsState(PSSButton.ButtonRightShoulder, PSSButtonState.ButtonStateReleased);
            break;
        }
      }
      else
      {
        if (sender != this.m_pauseButton || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
          return;
        InGameStatePlay.GetInstance().InGameMenuRequest = true;
      }
    }

    public void SplashBlood()
    {
      if (this.m_bloodAnimPlaying)
        return;
      this.m_bloodAnim.SetTime(0.0f);
      this.m_bloodAnimPlaying = true;
    }

    public void ShowShakeAnim(bool show)
    {
      if (show == this.m_shakeAnimPlaying)
        return;
      this.m_shakeAnimPlaying = show;
      if (!show)
        return;
      this.m_shakeAnimEn.SetTime(0.0f);
      this.m_shakeAnimFr.SetTime(0.0f);
      this.m_shakeAnimIt.SetTime(0.0f);
      this.m_shakeAnimGe.SetTime(0.0f);
      this.m_shakeAnimSp.SetTime(0.0f);
    }

    public void ShowLeftArrow(bool show)
    {
      if (show == this.m_leftArrowAnimPlaying)
        return;
      this.m_leftArrowAnimPlaying = show;
      if (!show || this.m_rightArrowAnimPlaying)
        return;
      this.m_arrowAnim.SetTime(0.0f);
    }

    public void ShowRightArrow(bool show)
    {
      if (show == this.m_rightArrowAnimPlaying)
        return;
      this.m_rightArrowAnimPlaying = show;
      if (!show || this.m_leftArrowAnimPlaying)
        return;
      this.m_arrowAnim.SetTime(0.0f);
    }

    public void SetCurrentGunIcon(PlayableCharacter hero)
    {
      this.m_weaponIndex = (int) hero.GetGun().GetGunType();
      this.m_weaponSwitch.SetQuad(this.m_weaponQuads[this.m_weaponIndex], this.m_hudTexture);
      Gun[] gunArray = hero.GetGunArray();
      this.m_prevWeaponIndex = -1;
      for (int index1 = 0; index1 < 5 && this.m_prevWeaponIndex < 0; ++index1)
      {
        int index2 = this.m_weaponIndex - index1 - 1;
        if (index2 < 0)
          index2 = 6 + index2;
        if (gunArray[index2].IsAvailable)
          this.m_prevWeaponIndex = index2;
      }
      this.m_nextWeaponIndex = -1;
      for (int index3 = 0; index3 < 5 && this.m_nextWeaponIndex < 0; ++index3)
      {
        int index4 = (this.m_weaponIndex + index3 + 1) % 6;
        if (gunArray[index4].IsAvailable)
          this.m_nextWeaponIndex = index4;
      }
      if (this.m_nextWeaponIndex == -1 || this.m_nextWeaponIndex != this.m_prevWeaponIndex)
        return;
      this.m_prevWeaponIndex = -1;
    }

    private void CreateManaQuad()
    {
      QuadFactory.CreateCenteredScaledQuad(this.m_quadMana, 115f, 101f, 38f, 38f, 256f, 256f, 1.15f);
      float num1 = (float) ((1.0 - (double) this.m_manaScale) * 38.0 * 1.14999997615814);
      this.m_quadMana[0].Position.Y -= num1;
      this.m_quadMana[1].Position.Y -= num1;
      float num2 = (float) ((1.0 - (double) this.m_manaScale) * (19.0 / 128.0));
      this.m_quadMana[0].TextureCoordinate.Y += num2;
      this.m_quadMana[1].TextureCoordinate.Y += num2;
    }
  }
}
