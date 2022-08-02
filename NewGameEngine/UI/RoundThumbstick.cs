// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.RoundThumbstick
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using PSS.GameEngine.Input;

namespace PSS.GameEngine.UI
{
  public class RoundThumbstick : Control
  {
    protected Touch m_touch;
    protected Vector2 m_thumbstickPosition;
    protected float m_detectionRange;
    protected float m_thumbstickRange;
    protected PSSThumbstick m_thumbstick;

    public RoundThumbstick() => this.m_touch = (Touch) null;

    public virtual void Initialize(
      ViewController vc,
      PSSThumbstick thumbstick,
      float detectionRange,
      float thumbstickRange)
    {
      this.Initialize(vc);
      this.m_thumbstick = thumbstick;
      this.m_thumbstickPosition = Vector2.Zero;
      this.m_detectionRange = detectionRange;
      this.m_thumbstickRange = thumbstickRange;
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

    protected bool HitTest(Touch touch, out Vector2 pos)
    {
      Vector2 worldPosition2D = this.m_transform.GetWorldPosition2D();
      pos = touch.curPos - worldPosition2D;
      if ((double) pos.LengthSquared() > (double) this.m_detectionRange * (double) this.m_detectionRange)
        return false;
      float num1 = pos.Length();
      pos *= 1f / num1;
      if ((double) num1 <= (double) this.m_thumbstickRange)
      {
        float num2 = num1 / this.m_thumbstickRange;
        pos *= num2;
      }
      return true;
    }

    protected void SendThumbstickPositionToController() => this.m_viewController.Action((View) this, (ActionEvent) new RoundThumbstickActionEvent()
    {
      thumbstick = this.m_thumbstick,
      position = this.m_thumbstickPosition
    });

    public override void TouchesBegan(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active || this.m_touch != null)
        return;
      for (int index = 0; index < touchCount; ++index)
      {
        Touch touch = touchArray[index];
        Vector2 pos;
        if (this.HitTest(touch, out pos))
        {
          this.m_thumbstickPosition = pos;
          this.SendThumbstickPositionToController();
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
          this.m_thumbstickPosition = Vector2.Zero;
          this.SendThumbstickPositionToController();
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
          this.m_thumbstickPosition = Vector2.Zero;
          this.SendThumbstickPositionToController();
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
        Vector2 pos;
        if (this.m_touch == touch)
        {
          if (this.HitTest(touch, out pos))
          {
            this.m_thumbstickPosition = pos;
            this.SendThumbstickPositionToController();
            break;
          }
          this.m_thumbstickPosition = Vector2.Zero;
          this.SendThumbstickPositionToController();
          this.m_touch = (Touch) null;
          break;
        }
        if (this.m_touch == null && this.HitTest(touch, out pos))
        {
          this.m_thumbstickPosition = pos;
          this.SendThumbstickPositionToController();
          this.m_touch = touch;
          break;
        }
      }
      base.TouchesMoved(touchArray, touchCount);
    }
  }
}
