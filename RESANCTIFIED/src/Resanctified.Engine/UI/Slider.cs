// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.UI.Slider
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.Maths;

namespace Resanctified.Engine.UI
{
  public class Slider : Control
  {
    protected Touch m_touch;
    protected float m_normalizedPosition;
    protected float m_length;
    protected float m_radius;
    protected SliderOrientation m_orientation;

    public Slider()
    {
      this.m_touch = (Touch) null;
      this.m_normalizedPosition = 0.0f;
      this.m_length = 0.0f;
      this.m_radius = 0.0f;
      this.m_orientation = SliderOrientation.Horizontal;
    }

    public virtual void Initialize(
      ViewController vc,
      float length,
      float radius,
      SliderOrientation orientation)
    {
      this.Initialize(vc);
      this.m_length = length;
      this.m_radius = radius;
      this.m_orientation = orientation;
      this.m_normalizedPosition = 0.0f;
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

    public void SetNormalizedPosition(float t)
    {
      t = MathHelper.Clamp(t, 0.0f, 1f);
      this.m_normalizedPosition = t;
    }

    public float GetNormalizedPosition() => this.m_normalizedPosition;

    protected void SendEvent()
    {
      if (this.m_viewController == null)
        return;
      this.m_viewController.Action((View) this, (ActionEvent) new SliderActionEvent()
      {
        normalizedPosition = this.m_normalizedPosition
      });
    }

    protected bool HitTest(Touch touch, out float t)
    {
      Vector3 worldPosition = this.m_transform.GetWorldPosition() with
      {
        Z = 0.0f
      };
      Vector3 A;
      Vector3 B;
      if (this.m_orientation == SliderOrientation.Vertical)
      {
        A = worldPosition;
        A.Y += this.m_length * 0.5f;
        B = A;
        B.Y -= this.m_length;
      }
      else
      {
        A = worldPosition;
        A.X -= this.m_length * 0.5f;
        B = A;
        B.X += this.m_length;
      }
      Vector3 P = new Vector3(touch.curPos.X, touch.curPos.Y, 0.0f);
      t = 0.0f;
      Vector3 closestPoint = MathHelperEx.GetClosestPoint(A, B, P, ref t, true);
      return (double) (P - closestPoint).LengthSquared() <= (double) this.m_radius * (double) this.m_radius;
    }

    public override void TouchesBegan(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active || this.m_touch != null)
        return;
      for (int index = 0; index < touchCount; ++index)
      {
        Touch touch = touchArray[index];
        float t;
        if (this.HitTest(touch, out t))
        {
          this.m_normalizedPosition = t;
          this.SendEvent();
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
        float t;
        if (this.m_touch == touch)
        {
          if (this.HitTest(touch, out t))
          {
            this.m_normalizedPosition = t;
            this.SendEvent();
            break;
          }
          this.m_touch = (Touch) null;
          break;
        }
        if (this.m_touch == null && this.HitTest(touch, out t))
        {
          this.m_normalizedPosition = t;
          this.SendEvent();
          this.m_touch = touch;
          break;
        }
      }
      base.TouchesMoved(touchArray, touchCount);
    }
  }
}
