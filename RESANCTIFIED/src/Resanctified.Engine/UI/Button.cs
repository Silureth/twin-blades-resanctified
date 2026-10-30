using Microsoft.Xna.Framework;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;

namespace Resanctified.Engine.UI
{
    public class Button : Control
    {
        protected Touch m_touch;
        protected float m_width;
        protected float m_height;

        public Button() => this.m_touch = null;

        public virtual void Initialize(ViewController vc, float width, float height)
        {
            this.Initialize(vc);
            this.m_width = width;
            this.m_height = height;
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

        /// <summary>Does what a tap on the button does: a pressed then a released action, unless it is inactive or already held.</summary>
        public void Click()
        {
            if (!this.m_active || this.m_touch != null || this.m_viewController == null)
                return;
            this.m_viewController.Action(this, new ButtonActionEvent()
            {
                buttonState = PSSButtonState.ButtonStatePressed
            });
            this.m_viewController.Action(this, new ButtonActionEvent()
            {
                buttonState = PSSButtonState.ButtonStateReleased
            });
        }

        public float GetWidth() => this.m_width;

        public float GetHeight() => this.m_height;

        protected bool HitTest(Touch touch)
        {
            AABB localBox = new AABB();
            AABB transformedBox = new AABB();
            localBox.InitializeFromBounds(new Vector3((float)(-(double)this.m_width * 0.5), (float)(-(double)this.m_height * 0.5), 0.0f), new Vector3(this.m_width * 0.5f, this.m_height * 0.5f, 0.0f));
            MathHelperEx.TransformBox(out transformedBox, localBox, this.m_transform.GetWorldTransform());
            return MathHelperEx.PointInBox(touch.curPos, transformedBox);
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
