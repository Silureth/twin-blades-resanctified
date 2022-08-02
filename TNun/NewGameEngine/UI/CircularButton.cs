// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.CircularButton
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using PSS.GameEngine.Input;

namespace PSS.GameEngine.UI
{
  public class CircularButton : Control
  {
    protected Touch m_touch;
    protected float m_radius;

    public virtual void Initialize(ViewController vc, float radius)
    {
      this.Initialize(vc);
      this.m_radius = radius;
      this.m_touch = (Touch) null;
    }

    public override void Cleanup()
    {
      this.m_touch = (Touch) null;
      base.Cleanup();
    }

    public override void Draw()
    {
    }

    public override void Update(float elapsedTime)
    {
    }

    protected bool HitTest(Touch touch)
    {
      Matrix worldTransform = this.m_transform.GetWorldTransform();
      Vector2 curPos = touch.curPos;
      curPos.X -= worldTransform.M41;
      curPos.Y -= worldTransform.M42;
      return (double) curPos.LengthSquared() <= (double) this.m_radius * (double) this.m_radius;
    }

    public override void TouchesBegan(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active || this.m_touch != null)
        return;
      for (int index = 0; index < touchCount; ++index)
      {
        Touch touch = touchArray[index];
        if (this.HitTest(touch))
        {
          if (this.m_viewController != null)
            this.m_viewController.Action((View) this, (ActionEvent) new ButtonActionEvent()
            {
              buttonState = PSSButtonState.ButtonStatePressed
            });
          this.m_touch = touch;
          break;
        }
      }
      base.TouchesBegan(touchArray, touchCount);
    }

    public override void TouchesEnded(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active || this.m_touch == null)
        return;
      for (int index = 0; index < touchCount; ++index)
      {
        if (this.m_touch == touchArray[index])
        {
          if (this.m_viewController != null)
            this.m_viewController.Action((View) this, (ActionEvent) new ButtonActionEvent()
            {
              buttonState = PSSButtonState.ButtonStateReleased
            });
          this.m_touch = (Touch) null;
          break;
        }
      }
      base.TouchesEnded(touchArray, touchCount);
    }

    public override void TouchesCancelled(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active || this.m_touch == null)
        return;
      for (int index = 0; index < touchCount; ++index)
      {
        if (this.m_touch == touchArray[index])
        {
          if (this.m_viewController != null)
            this.m_viewController.Action((View) this, (ActionEvent) new ButtonActionEvent()
            {
              buttonState = PSSButtonState.ButtonStateReleased
            });
          this.m_touch = (Touch) null;
          break;
        }
      }
      base.TouchesCancelled(touchArray, touchCount);
    }

    public override void TouchesMoved(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active)
        return;
      for (int index = 0; index < touchCount; ++index)
      {
        Touch touch = touchArray[index];
        if (this.m_touch == touch && !this.HitTest(touch))
        {
          if (this.m_viewController != null)
            this.m_viewController.Action((View) this, (ActionEvent) new ButtonActionEvent()
            {
              buttonState = PSSButtonState.ButtonStateReleased
            });
          this.m_touch = (Touch) null;
          break;
        }
      }
      base.TouchesMoved(touchArray, touchCount);
    }
  }
}
