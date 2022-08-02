// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.UI.TextBoxViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PSS.Game.NunVsZombies.FSM.GameState;
using PSS.GameEngine.Graphics;
using PSS.GameEngine.Input;
using PSS.GameEngine.Maths;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Tools;
using PSS.GameEngine.UI;
using System.Collections.Generic;

namespace PSS.Game.NunVsZombies.UI
{
  public class TextBoxViewController : ViewController
  {
    private const float TEXTBOX_TEXTURE_WIDTH = 512f;
    private const float TEXTBOX_TEXTURE_HEIGHT = 512f;
    private const float TEXTBOX_TEXTZONE_TOP_X = 2f;
    private const float TEXTBOX_TEXTZONE_TOP_Y = 0.0f;
    private const float TEXTBOX_TEXTZONE_WIDTH = 422f;
    private const float TEXTBOX_TEXTZONE_HEIGHT = 102f;
    private const float TEXTBOX_FACE_NUN1_TOP_X = 0.0f;
    private const float TEXTBOX_FACE_NUN1_TOP_Y = 102f;
    private const float TEXTBOX_FACE_NUN1_WIDTH = 94f;
    private const float TEXTBOX_FACE_NUN1_HEIGHT = 120f;
    private const float TEXTBOX_FACE_NECRO1_TOP_X = 155f;
    private const float TEXTBOX_FACE_NECRO1_TOP_Y = 262f;
    private const float TEXTBOX_FACE_NECRO1_WIDTH = 143f;
    private const float TEXTBOX_FACE_NECRO1_HEIGHT = 150f;
    private const float TEXTBOX_FACE_BUTCHER_TOP_X = 0.0f;
    private const float TEXTBOX_FACE_BUTCHER_TOP_Y = 226f;
    private const float TEXTBOX_FACE_BUTCHER_WIDTH = 152f;
    private const float TEXTBOX_FACE_BUTCHER_HEIGHT = 141f;
    private const float TEXTBOX_FACE_PRIEST_TOP_X = 190f;
    private const float TEXTBOX_FACE_PRIEST_TOP_Y = 102f;
    private const float TEXTBOX_FACE_PRIEST_WIDTH = 172f;
    private const float TEXTBOX_FACE_PRIEST_HEIGHT = 154f;
    private const float TEXTBOX_FACE_NUN2_TOP_X = 96f;
    private const float TEXTBOX_FACE_NUN2_TOP_Y = 102f;
    private const float TEXTBOX_FACE_NUN2_WIDTH = 92f;
    private const float TEXTBOX_FACE_NUN2_HEIGHT = 120f;
    private const float TEXTBOX_FACE_NECRO2_TOP_X = 300f;
    private const float TEXTBOX_FACE_NECRO2_TOP_Y = 266f;
    private const float TEXTBOX_FACE_NECRO2_WIDTH = 150f;
    private const float TEXTBOX_FACE_NECRO2_HEIGHT = 186f;
    private const float TEXTBOX_LIVE_TOP_X = 470f;
    private const float TEXTBOX_LIVE_TOP_Y = 5f;
    private const float TEXTBOX_LIVE_WIDTH = 36f;
    private const float TEXTBOX_LIVE_HEIGHT = 36f;
    private const float TEXTBOX_DIE_TOP_X = 470f;
    private const float TEXTBOX_DIE_TOP_Y = 45f;
    private const float TEXTBOX_DIE_WIDTH = 36f;
    private const float TEXTBOX_DIE_HEIGHT = 36f;
    private const float TEXTBOX_FACE_LEFT_POS_X = -5f;
    private const float TEXTBOX_FACE_LEFT_POS_Y = 0.0f;
    private const float TEXTBOX_FACE_RIGHT_POS_X = 480f;
    private const float TEXTBOX_FACE_RIGHT_POS_X_NUN2 = 485f;
    private const float TEXTBOX_FACE_RIGHT_POS_X_PRIEST = 505f;
    private const float TEXTBOX_FACE_RIGHT_POS_X_BUTCHER = 515f;
    private const float TEXTBOX_FACE_RIGHT_POS_X_NECRO = 495f;
    private const float TEXTBOX_FACE_RIGHT_POS_Y = 0.0f;
    private const float TEXTBOX_TEXT_NAME_POS_X_LEFTFACE = 100f;
    private const float TEXTBOX_TEXT_NAME_POS_X_RIGHTFACE = 20f;
    private const float TEXTBOX_TEXT_NAME_POS_Y_OFFSET = -8f;
    private const float TEXTBOX_TEXT_BODY_POS_X_LEFTFACE = 100f;
    private const float TEXTBOX_TEXT_BODY_POS_X_RIGHTFACE = 20f;
    private const float TEXTBOX_TEXT_BODY_POS_Y_OFFSET = -25f;
    private const float TEXTBOX_CHOICE_BUTTON_OFFSET_FROM_TEXT_BODY_POS_X = 350f;
    private const float TEXTBOX_CHOICE_BUTTON_SIZE = 20f;
    private const float TEXTBOX_TEXT_NAME_SCALE = 0.8f;
    private const float TEXTBOX_TEXT_BODY_SCALE = 0.75f;
    private const float TEXTBOX_TEXTZONE_POS_X_LEFTFACE = 480f;
    private const float TEXTBOX_TEXTZONE_POS_X_RIGHTFACE = 0.0f;
    private const float TEXTBOX_TEXTZONE_POS_Y = 102f;
    private const float TEXTBOX_TEXT_BODY_WIDTH = 480f;
    private const int TEXTBOX_TEXT_BODY_LINES_PER_PAGE = 4;
    private static int[] TEXTBOX_FACE_ORIENTATION = new int[6]
    {
      -1,
      1,
      1,
      1,
      1,
      1
    };
    private static Color[] TEXTBOX_NAME_COLOR = new Color[6]
    {
      GameConstant.TEXTBOX_NUN1_NAME_COLOR,
      GameConstant.TEXTBOX_NUN2_NAME_COLOR,
      GameConstant.TEXTBOX_PRIEST_NAME_COLOR,
      GameConstant.TEXTBOX_BOSS_NAME_COLOR,
      GameConstant.TEXTBOX_BOSS_NAME_COLOR,
      GameConstant.TEXTBOX_BOSS_NAME_COLOR
    };
    private static float[] TEXTBOX_FACE_POS_X = new float[6]
    {
      -5f,
      485f,
      505f,
      515f,
      495f,
      495f
    };
    private VertexPositionTexture[] m_quadTextzone = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadFaceNun1 = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadFaceNun2 = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadFaceNecro1 = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadFaceNecro2 = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadFaceButcher = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadFacePriest = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadLive = new VertexPositionTexture[4];
    private VertexPositionTexture[] m_quadDie = new VertexPositionTexture[4];
    private PSS.GameEngine.Resources.Texture m_textureFaces;
    private Transform m_t = new Transform();
    private TextBoxViewController.FaceId m_face;
    private string m_text;
    private string m_textChoiceLive;
    private string m_textChoiceDie;
    private bool m_choiceModeEnabled;
    private int m_currentPage;
    private int m_numPages;
    private Button m_buttonTextzone;
    private Button m_buttonLive;
    private Button m_buttonDie;
    private List<OLPair> m_textPairs;

    public TextBoxViewController()
    {
      this.m_face = TextBoxViewController.FaceId.Nun1;
      this.m_text = string.Empty;
      this.m_textChoiceLive = string.Empty;
      this.m_textChoiceDie = string.Empty;
      this.m_choiceModeEnabled = false;
      this.m_currentPage = 0;
      this.m_numPages = 0;
      this.m_buttonTextzone = new Button();
      this.m_buttonLive = new Button();
      this.m_buttonDie = new Button();
      this.m_textPairs = new List<OLPair>(8);
    }

    public override void Initialize()
    {
      base.Initialize();
      this.m_textPairs.Clear();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_textureFaces);
      this.m_textureFaces = ResourceManager.GetInstance().LoadTexture("Textures\\texture_faces");
      QuadFactory.CreateAnchoredQuad(this.m_quadTextzone, 2f, 0.0f, 422f, 102f, 512f, 512f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadFaceNun1, 0.0f, 102f, 94f, 120f, 512f, 512f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadFaceNun2, 96f, 102f, 92f, 120f, 512f, 512f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadFaceNecro1, 155f, 262f, 143f, 150f, 512f, 512f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadFaceNecro2, 300f, 266f, 150f, 186f, 512f, 512f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadFaceButcher, 0.0f, 226f, 152f, 141f, 512f, 512f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadFacePriest, 190f, 102f, 172f, 154f, 512f, 512f, Anchor.Left);
      QuadFactory.CreateAnchoredQuad(this.m_quadLive, 470f, 5f, 36f, 36f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      QuadFactory.CreateAnchoredQuad(this.m_quadDie, 470f, 45f, 36f, 36f, 512f, 512f, Anchor.HCenter | Anchor.VCenter);
      this.m_buttonTextzone.Initialize((ViewController) this, 422f, 102f);
      this.m_buttonTextzone.GetTransform().SetPosition(240f, 51f);
      this.m_face = TextBoxViewController.FaceId.Nun1;
      this.m_text = string.Empty;
      this.SetActive(false);
      this.m_buttonLive.Initialize((ViewController) this, 20f, 20f);
      this.m_buttonDie.Initialize((ViewController) this, 20f, 20f);
      this.DisableChoiceMode();
    }

    public override void Cleanup()
    {
      this.m_buttonTextzone.Cleanup();
      this.m_buttonLive.Cleanup();
      this.m_buttonDie.Cleanup();
      Resource.SafeRelease<PSS.GameEngine.Resources.Texture>(ref this.m_textureFaces);
      this.m_textPairs.Clear();
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
      if (!this.m_visible || !this.m_active)
        return;
      PSS.GameEngine.Graphics.Renderer.Renderer instance = PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance();
      instance.Begin2DMode(false);
      Font font = GameApplication.GetInstance().GetFont();
      this.m_t.SetScale(1f);
      int anchor;
      if (TextBoxViewController.TEXTBOX_FACE_ORIENTATION[(int) this.m_face] < 0)
      {
        this.m_t.SetPosition(-5f, 0.0f);
        anchor = 2;
      }
      else
      {
        this.m_t.SetPosition(TextBoxViewController.TEXTBOX_FACE_POS_X[(int) this.m_face], 0.0f);
        anchor = 3;
      }
      switch (this.m_face)
      {
        case TextBoxViewController.FaceId.Nun1:
          instance.DrawQuad<VertexPositionTexture>(this.m_quadFaceNun1, VertexFormat.Position3Texture, this.m_t, this.m_textureFaces, Color.White, anchor);
          break;
        case TextBoxViewController.FaceId.Nun2:
          instance.DrawQuad<VertexPositionTexture>(this.m_quadFaceNun2, VertexFormat.Position3Texture, this.m_t, this.m_textureFaces, Color.White, anchor);
          break;
        case TextBoxViewController.FaceId.Priest:
          instance.DrawQuad<VertexPositionTexture>(this.m_quadFacePriest, VertexFormat.Position3Texture, this.m_t, this.m_textureFaces, Color.White, anchor);
          break;
        case TextBoxViewController.FaceId.Butcher:
          instance.DrawQuad<VertexPositionTexture>(this.m_quadFaceButcher, VertexFormat.Position3Texture, this.m_t, this.m_textureFaces, Color.White, anchor);
          break;
        case TextBoxViewController.FaceId.Necro1:
          instance.DrawQuad<VertexPositionTexture>(this.m_quadFaceNecro1, VertexFormat.Position3Texture, this.m_t, this.m_textureFaces, Color.White, anchor);
          break;
        case TextBoxViewController.FaceId.Necro2:
          instance.DrawQuad<VertexPositionTexture>(this.m_quadFaceNecro2, VertexFormat.Position3Texture, this.m_t, this.m_textureFaces, Color.White, anchor);
          break;
      }
      if (TextBoxViewController.TEXTBOX_FACE_ORIENTATION[(int) this.m_face] < 0)
      {
        this.m_t.SetScale(-1f, 1f);
        this.m_t.SetPosition(480f, 102f);
      }
      else
      {
        this.m_t.SetScale(1f);
        this.m_t.SetPosition(0.0f, 102f);
      }
      instance.DrawQuad<VertexPositionTexture>(this.m_quadTextzone, VertexFormat.Position3Texture, this.m_t, this.m_textureFaces);
      this.m_t.SetScale(0.8f, 0.8f);
      string empty = string.Empty;
      switch (this.m_face)
      {
        case TextBoxViewController.FaceId.Nun1:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Name_Nun1);
          break;
        case TextBoxViewController.FaceId.Nun2:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Name_Nun2);
          break;
        case TextBoxViewController.FaceId.Priest:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Name_Priest);
          break;
        case TextBoxViewController.FaceId.Butcher:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Name_Butcher);
          break;
        case TextBoxViewController.FaceId.Necro1:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Name_Necro1);
          break;
        case TextBoxViewController.FaceId.Necro2:
          empty = GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Name_Necro2);
          break;
      }
      if (TextBoxViewController.TEXTBOX_FACE_ORIENTATION[(int) this.m_face] < 0)
        this.m_t.SetPosition(100f, 94f);
      else
        this.m_t.SetPosition(20f, 94f);
      instance.DrawString(font, this.m_t, 0, TextBoxViewController.TEXTBOX_NAME_COLOR[(int) this.m_face], empty, true);
      this.m_t.SetScale(0.75f, 0.75f);
      if (TextBoxViewController.TEXTBOX_FACE_ORIENTATION[(int) this.m_face] < 0)
        this.m_t.SetPosition(100f, 77f);
      else
        this.m_t.SetPosition(20f, 77f);
      int pairStartOffset = this.m_currentPage * 4;
      int pairLength = 4;
      if (pairStartOffset + pairLength > this.m_textPairs.Count)
        pairLength = this.m_textPairs.Count - pairStartOffset;
      instance.DrawString(font, this.m_t, 0, GameConstant.TEXTBOX_TEXT_COLOR, this.m_text, this.m_textPairs, pairStartOffset, pairLength, true);
      if (this.m_choiceModeEnabled)
      {
        Vector3 position = this.m_t.GetPosition();
        this.m_t.SetPosition(position);
        instance.DrawString(font, this.m_t, 0, GameConstant.TEXTBOX_TEXT_COLOR, this.m_textChoiceLive, true);
        position.Y -= (float) ((double) font.GetLineHeight() * 0.75 * 2.0);
        this.m_t.SetPosition(position);
        instance.DrawString(font, this.m_t, 0, GameConstant.TEXTBOX_TEXT_COLOR, this.m_textChoiceDie, true);
        instance.DrawQuad<VertexPositionTexture>(this.m_quadLive, VertexFormat.Position3Texture, this.m_buttonLive.GetTransform(), this.m_textureFaces);
        instance.DrawQuad<VertexPositionTexture>(this.m_quadDie, VertexFormat.Position3Texture, this.m_buttonDie.GetTransform(), this.m_textureFaces);
      }
      instance.End2DMode();
    }

    public void SetText(TextBoxViewController.FaceId face, string text)
    {
      this.m_face = face;
      this.m_text = text;
      this.m_textPairs.Clear();
      TextUtils.WrapText(ref this.m_textPairs, text, GameApplication.GetInstance().GetFont(), 480f);
      this.m_currentPage = 0;
      this.m_numPages = this.m_textPairs.Count / 4;
      if (this.m_textPairs.Count % 4 != 0)
        ++this.m_numPages;
      InputManager.GetInstance().ClearPadState();
    }

    public override void SetActive(bool active)
    {
      this.SetVisible(active);
      if (this.m_choiceModeEnabled)
      {
        this.m_buttonLive.SetActive(true);
        this.m_buttonDie.SetActive(true);
      }
      else
      {
        this.m_buttonLive.SetActive(false);
        this.m_buttonDie.SetActive(false);
      }
      base.SetActive(active);
    }

    public void EnableChoiceMode(string textChoice1, string textChoice2)
    {
      this.m_choiceModeEnabled = true;
      this.m_buttonLive.SetActive(true);
      this.m_buttonDie.SetActive(true);
      this.m_textChoiceLive = textChoice1;
      this.m_textChoiceDie = textChoice2;
      if (TextBoxViewController.TEXTBOX_FACE_ORIENTATION[(int) this.m_face] < 0)
      {
        this.m_buttonLive.GetTransform().SetPosition(450f, 77f);
        this.m_buttonDie.GetTransform().SetPosition(450f, 27f);
      }
      else
      {
        this.m_buttonLive.GetTransform().SetPosition(370f, 77f);
        this.m_buttonDie.GetTransform().SetPosition(370f, 27f);
      }
    }

    public void DisableChoiceMode()
    {
      this.m_choiceModeEnabled = false;
      this.m_buttonLive.SetActive(false);
      this.m_buttonDie.SetActive(false);
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active)
        return;
      ButtonActionEvent buttonActionEvent = (ButtonActionEvent) evt;
      if (!this.m_choiceModeEnabled)
      {
        if (sender != this.m_buttonTextzone || buttonActionEvent.buttonState != PSSButtonState.ButtonStatePressed)
          return;
        if (this.m_currentPage == this.m_numPages - 1)
        {
          Scenario.GetInstance().RaiseEventNextText();
          GameStateMap.GetInstance().HideObjectivesRequest = true;
        }
        else
          ++this.m_currentPage;
      }
      else if (sender == this.m_buttonLive && buttonActionEvent.buttonState == PSSButtonState.ButtonStatePressed)
      {
        Scenario.GetInstance().RaiseEventNextText();
        GameStateMap.GetInstance().HideObjectivesRequest = true;
      }
      else
      {
        if (sender != this.m_buttonDie || buttonActionEvent.buttonState != PSSButtonState.ButtonStatePressed)
          return;
        Scenario.GetInstance().RaiseEventNextText2();
        GameStateMap.GetInstance().HideObjectivesRequest = true;
      }
    }

    public enum FaceId
    {
      Nun1,
      Nun2,
      Priest,
      Butcher,
      Necro1,
      Necro2,
      Number,
    }
  }
}
