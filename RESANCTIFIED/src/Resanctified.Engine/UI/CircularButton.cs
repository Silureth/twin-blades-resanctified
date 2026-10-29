using Microsoft.Xna.Framework;
using Resanctified.Engine.Input;

namespace Resanctified.Engine.UI
{
    public class CircularButton : Control
    {
        protected Touch m_touch;
        protected float m_radius;

        public virtual void Initialize(ViewController vc, float radius)
        {
            this.Initialize(vc);
            this.m_radius = radius;
            this.m_touch = null;
        }

        public override void Cleanup()
        {
            this.m_touch = null;
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
            return (double)curPos.LengthSquared() <= this.m_radius * (double)this.m_radius;
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
                        this.m_viewController.Action(this, new ButtonActionEvent()
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
                        this.m_viewController.Action(this, new ButtonActionEvent()
                        {
                            buttonState = PSSButtonState.ButtonStateReleased
                        });
                    this.m_touch = null;
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
                        this.m_viewController.Action(this, new ButtonActionEvent()
                        {
                            buttonState = PSSButtonState.ButtonStateReleased
                        });
                    this.m_touch = null;
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
                        this.m_viewController.Action(this, new ButtonActionEvent()
                        {
                            buttonState = PSSButtonState.ButtonStateReleased
                        });
                    this.m_touch = null;
                    break;
                }
            }
            base.TouchesMoved(touchArray, touchCount);
        }
    }
}
