// Decompiled with JetBrains decompiler
// Type: PSS.Game.NunVsZombies.World.Entities.NPCShopViewController
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using PSS.Game.NunVsZombies.FSM.GameState;
using PSS.GameEngine;
using PSS.GameEngine.Input;
using PSS.GameEngine.UI;
using PSS.GameEngine.World;
using PSS.GameEngine.World.Entities;

namespace PSS.Game.NunVsZombies.World.Entities
{
  public class NPCShopViewController : ViewController
  {
    private Button m_iconButton;
    private NPCShop m_shop;

    public NPCShopViewController()
    {
      this.m_iconButton = new Button();
      this.m_shop = (NPCShop) null;
    }

    public void Initialize(NPCShop shop)
    {
      this.Initialize();
      this.m_iconButton.Initialize((ViewController) this, 120f, 200f);
      this.m_shop = shop;
      Application.GetInstance().AddResponder((Responder) this);
    }

    public override void Cleanup()
    {
      this.m_iconButton.Cleanup();
      this.m_shop = (NPCShop) null;
      Application.GetInstance().RemoveResponder((Responder) this);
      base.Cleanup();
    }

    public override void Action(View sender, ActionEvent evt)
    {
      if (!this.m_active || !this.m_shop.IsIconActive() || GameApplication.GetInstance().GetTextBoxViewController().IsActive())
        return;
      GameStateInGame.GetInstance();
      if (GameStateInGame.GetInstance().IsMenuInGameActive() || ((ButtonActionEvent) evt).buttonState != PSSButtonState.ButtonStatePressed)
        return;
      this.m_shop.EnterShopRequest = true;
    }

    public void SetButtonPosition(CameraEntity camera)
    {
      Vector3 vector3 = Vector3.Transform(new Vector3(6f, 0.85f, 0.0f), camera.GetViewMatrix());
      float num1 = camera.GetOrthoWidth() * 0.5f;
      float num2 = camera.GetOrthoHeight() * 0.5f;
      Vector3 zero = Vector3.Zero with
      {
        X = (float) ((double) vector3.X * 240.0 / (double) num1 + 240.0),
        Y = (float) ((double) vector3.Y * 160.0 / (double) num2 + 160.0)
      };
      this.m_iconButton.GetTransform().SetPosition(zero);
    }

    public override void Update(float elapsedTime) => this.SetButtonPosition(((GameWorld) WorldManager.GetInstance().GetCurrentWorld()).GetCamera());
  }
}
