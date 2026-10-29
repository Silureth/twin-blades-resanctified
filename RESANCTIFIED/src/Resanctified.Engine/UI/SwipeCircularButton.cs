// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.UI.SwipeCircularButton
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

namespace Resanctified.Engine.UI
{
  public class SwipeCircularButton : CircularButton
  {
    private const float SWIPECIRCULARBUTTON_DEFAULT_MIN_SPEED_X = 4f;
    private const float SWIPECIRCULARBUTTON_DEFAULT_MIN_SPEED_Y = 4f;
    protected float m_minSpeedX;
    protected float m_minSpeedY;
    protected bool m_swiped;

    public override void Initialize(ViewController vc, float radius) => this.Initialize(vc, radius, 4f, 4f);

    public virtual void Initialize(
      ViewController vc,
      float radius,
      float minSpeedX,
      float minSpeedY)
    {
      base.Initialize(vc, radius);
      this.m_minSpeedX = minSpeedX;
      this.m_minSpeedY = minSpeedY;
    }

    public override void Cleanup() => base.Cleanup();

    private void HandleSwipe(Touch touch)
    {
      float num1 = 1f / Application.GetInstance().GetElapsedTimeSinceLastFrame();
      float num2 = (touch.curPos.X - touch.prevPos.X) * num1;
      float num3 = (touch.curPos.Y - touch.prevPos.Y) * num1;
      if ((double) num2 > (double) this.m_minSpeedX)
        this.SendActionToController(SwipeDirection.Right);
      else if ((double) num2 < -(double) this.m_minSpeedX)
        this.SendActionToController(SwipeDirection.Left);
      if ((double) num3 > (double) this.m_minSpeedY)
      {
        this.SendActionToController(SwipeDirection.Up);
      }
      else
      {
        if ((double) num3 >= -(double) this.m_minSpeedY)
          return;
        this.SendActionToController(SwipeDirection.Down);
      }
    }

    private void SendActionToController(SwipeDirection direction)
    {
      this.m_viewController.Action((View) this, (ActionEvent) new SwipeCircularButtonActionEvent()
      {
        direction = direction
      });
      this.m_swiped = true;
    }

    public override void TouchesBegan(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active || this.m_touch != null)
        return;
      this.m_swiped = false;
      for (int index = 0; index < touchCount; ++index)
      {
        Touch touch = touchArray[index];
        if (this.HitTest(touch))
        {
          this.HandleSwipe(touch);
          this.m_touch = touch;
          break;
        }
      }
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesBegan(touchArray, touchCount);
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
          this.m_swiped = false;
          break;
        }
      }
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesEnded(touchArray, touchCount);
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
          this.m_swiped = false;
          break;
        }
      }
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesCancelled(touchArray, touchCount);
    }

    public override void TouchesMoved(Touch[] touchArray, int touchCount)
    {
      if (!this.m_active)
        return;
      for (int index = 0; index < touchCount; ++index)
      {
        Touch touch = touchArray[index];
        if (this.m_touch == null)
        {
          if (this.HitTest(touch))
          {
            this.HandleSwipe(touch);
            this.m_touch = touch;
            break;
          }
        }
        else if (touch == this.m_touch && !this.m_swiped)
          this.HandleSwipe(touch);
      }
      for (int index = 0; index < this.m_responders.Count; ++index)
        this.m_responders[index].TouchesMoved(touchArray, touchCount);
    }
  }
}
