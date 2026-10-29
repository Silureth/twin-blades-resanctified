using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.UI;

namespace Resanctified.Engine
{
    public class Application : Game, Responder
    {
        private const int NATIVE_TOUCH_ID_NULL = -1;
        // Marks the touch slot driven by the mouse (or an injected tap) so native touches never reuse it.
        private const int NATIVE_TOUCH_ID_POINTER = -2;
        private const int POINTER_TOUCH_INDEX = 0;
        private static Application s_instance;
        private InterfaceOrientation m_interfaceOrientation;
        private List<Responder> m_responders;
        private Touch[] m_touchArray;
        private Touch[] m_dispatchedTouchArray;
        private float m_elapsedTimeSinceLastFrame;
        private bool m_pointerDown;
        private Queue<Vector2> m_pendingTaps;
        private bool m_injectedTapDown;
        private bool m_shakeInputDown;

        public static Application Instance => s_instance;

        public static Application GetInstance() => s_instance;

        public Application()
        {
            s_instance = this;
            this.m_responders = new List<Responder>(64);
            this.m_touchArray = new Touch[5];
            this.m_dispatchedTouchArray = new Touch[5];
            this.m_elapsedTimeSinceLastFrame = 0.0f;
            this.m_pendingTaps = new Queue<Vector2>();
        }

        protected override void Initialize()
        {
            this.m_responders.Clear();
            for (int index = 0; index < 5; ++index)
            {
                this.m_touchArray[index] = new Touch();
                this.m_touchArray[index].nativeTouchId = -1;
                this.m_dispatchedTouchArray[index] = null;
            }
            this.SetInterfaceOrientation(InterfaceOrientation.LandscapeLeft);
            this.m_pointerDown = false;
            this.m_injectedTapDown = false;
            this.m_shakeInputDown = false;
            this.m_pendingTaps.Clear();
            VirtualScreen.Instance.Initialize(this.GraphicsDevice);
            ResourceManager.Instance.Initialize();
            InputManager.Instance.Initialize();
            Graphics.Renderer.Renderer.GetInstance().Initialize();
            AudioManager.Instance.Initialize();
            base.Initialize();
        }


        protected virtual void Cleanup()
        {
            this.m_responders.Clear();
            AudioManager.Instance.Cleanup();
            ResourceManager.Instance.Cleanup();
            InputManager.Instance.Cleanup();
            Graphics.Renderer.Renderer.GetInstance().Cleanup();
            VirtualScreen.Instance.Cleanup();
        }

        protected override void OnExiting(object sender, ExitingEventArgs args)
        {
            this.Cleanup();
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            this.m_elapsedTimeSinceLastFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;
            // Before InputManager.Update, so a shake is seen by the game actions in this same frame.
            this.UpdateShakeEvents();
            InputManager.Instance.Update(this.m_elapsedTimeSinceLastFrame);
            AudioManager.Instance.Update();
            this.UpdateTouchEvents();
            base.Update(gameTime);
        }

        protected override bool BeginDraw()
        {
            if (!base.BeginDraw())
                return false;
            VirtualScreen.Instance.BeginFrame();
            return true;
        }

        protected override void EndDraw()
        {
            VirtualScreen.Instance.Present();
            base.EndDraw();
        }

        public float GetElapsedTimeSinceLastFrame() => this.m_elapsedTimeSinceLastFrame;

        public void SetInterfaceOrientation(InterfaceOrientation orientation) => this.m_interfaceOrientation = orientation;

        public InterfaceOrientation GetInterfaceOrientation() => this.m_interfaceOrientation;

        /// <summary>
        /// Queues a tap (Began, then Ended on the next frame) at a virtual screen position, Y up.
        /// It goes through the same path as the mouse, so it behaves exactly like a click.
        /// </summary>
        public void InjectTap(Vector2 virtualPos) => this.m_pendingTaps.Enqueue(virtualPos);

        private int GetTouchIndexFromNativeTouchId(int nativeTouchId)
        {
            for (int index = 0; index < 5; ++index)
            {
                if (this.m_touchArray[index].nativeTouchId == nativeTouchId)
                    return index;
            }
            return -1;
        }

        private void UpdateTouchEvents()
        {
            if (TouchPanel.GetCapabilities().IsConnected)
                this.UpdateNativeTouchEvents();
            else
                this.UpdatePointerTouchEvents();
        }

        /// <summary>
        /// The phone game used an accelerometer shake (e.g. to break free when grabbed). On desktop the right
        /// mouse button or the shake key (Space) raises the same one-shot shake the accelerometer callback did.
        /// </summary>
        private void UpdateShakeEvents()
        {
            // The key state is last frame's (InputManager updates next), which also includes injected keys.
            bool down = (this.IsActive && Mouse.GetState().RightButton == ButtonState.Pressed) || InputManager.Instance.IsKeyDown(KeyBindings.Shake);
            if (down && !this.m_shakeInputDown)
            {
                this.ShakeBegin();
                this.ShakeEnded();
            }
            this.m_shakeInputDown = down;
        }

        private void UpdateNativeTouchEvents()
        {
            TouchCollection state = TouchPanel.GetState();
            for (int index = 0; index < state.Count; ++index)
            {
                TouchLocation nativeTouch = state[index];
                if (nativeTouch.State == TouchLocationState.Pressed)
                {
                    // A touch that starts in a letterbox bar is not a touch on the game.
                    if (!VirtualScreen.Instance.IsInside(nativeTouch.Position))
                        continue;
                    int freeIndex = this.GetTouchIndexFromNativeTouchId(NATIVE_TOUCH_ID_NULL);
                    if (freeIndex >= 0)
                        this.BeginTouch(freeIndex, nativeTouch.Id, nativeTouch.Position);
                }
                else
                {
                    // Only touches that began on the game screen are tracked.
                    int touchIndex = this.GetTouchIndexFromNativeTouchId(nativeTouch.Id);
                    if (touchIndex < 0)
                        continue;
                    if (nativeTouch.State == TouchLocationState.Moved)
                        this.MoveTouch(touchIndex, nativeTouch.Position);
                    else
                        this.EndTouch(touchIndex, nativeTouch.Position, nativeTouch.State == TouchLocationState.Released);
                }
            }
        }

        /// <summary>Desktop: the left mouse button (or an injected tap) acts as a single finger.</summary>
        private void UpdatePointerTouchEvents()
        {
            bool down;
            bool injected = false;
            Vector2 windowPos;
            if (this.m_injectedTapDown)
            {
                down = false;
                injected = true;
                windowPos = this.m_touchArray[POINTER_TOUCH_INDEX].curNativePos;
                this.m_injectedTapDown = false;
            }
            else if (!this.m_pointerDown && this.m_pendingTaps.Count > 0)
            {
                down = true;
                injected = true;
                windowPos = VirtualScreen.Instance.VirtualToWindow(this.m_pendingTaps.Dequeue());
                this.m_injectedTapDown = true;
            }
            else
            {
                MouseState mouse = Mouse.GetState();
                down = this.IsActive && mouse.LeftButton == ButtonState.Pressed;
                windowPos = new Vector2(mouse.X, mouse.Y);
            }

            if (down && !this.m_pointerDown)
            {
                // Clicks in the letterbox bars are ignored: a touch has to start on the game screen.
                if (injected || VirtualScreen.Instance.IsInside(windowPos))
                {
                    this.BeginTouch(POINTER_TOUCH_INDEX, NATIVE_TOUCH_ID_POINTER, windowPos);
                    this.m_pointerDown = true;
                }
            }
            else if (down)
                this.MoveTouch(POINTER_TOUCH_INDEX, windowPos);
            else if (this.m_pointerDown)
            {
                // Losing focus while the button is held cancels the touch instead of completing it.
                this.EndTouch(POINTER_TOUCH_INDEX, windowPos, injected || this.IsActive);
                this.m_pointerDown = false;
            }
        }

        private void BeginTouch(int touchIndex, int nativeTouchId, Vector2 windowPos)
        {
            Touch touch = this.m_touchArray[touchIndex];
            touch.touchId = (TouchId)touchIndex;
            touch.touchPhase = TouchPhase.Began;
            touch.prevPos = VirtualScreen.Instance.WindowToVirtual(windowPos);
            touch.curPos = touch.prevPos;
            touch.tapCount = 1;
            touch.timestamp = 0.0;
            touch.prevNativePos = windowPos;
            touch.curNativePos = windowPos;
            touch.nativeTouchId = nativeTouchId;
            this.m_dispatchedTouchArray[0] = touch;
            this.TouchesBegan(this.m_dispatchedTouchArray, 1);
        }

        private void MoveTouch(int touchIndex, Vector2 windowPos)
        {
            Touch touch = this.m_touchArray[touchIndex];
            this.UpdateTouchPosition(touch, windowPos);
            touch.touchPhase = touch.curNativePos != touch.prevNativePos ? TouchPhase.Moved : TouchPhase.Stationary;
            this.m_dispatchedTouchArray[0] = touch;
            this.TouchesMoved(this.m_dispatchedTouchArray, 1);
        }

        private void EndTouch(int touchIndex, Vector2 windowPos, bool released)
        {
            Touch touch = this.m_touchArray[touchIndex];
            this.UpdateTouchPosition(touch, windowPos);
            this.m_dispatchedTouchArray[0] = touch;
            if (released)
            {
                touch.touchPhase = TouchPhase.Ended;
                this.TouchesEnded(this.m_dispatchedTouchArray, 1);
            }
            else
            {
                touch.touchPhase = TouchPhase.Cancelled;
                this.TouchesCancelled(this.m_dispatchedTouchArray, 1);
            }
            touch.nativeTouchId = NATIVE_TOUCH_ID_NULL;
        }

        private void UpdateTouchPosition(Touch touch, Vector2 windowPos)
        {
            touch.prevPos = touch.curPos;
            // A finger dragged into the letterbox bars stays on the screen edge so it can still end normally.
            touch.curPos = VirtualScreen.ClampToScreen(VirtualScreen.Instance.WindowToVirtual(windowPos));
            touch.tapCount = 1;
            touch.timestamp = 0.0;
            touch.prevNativePos = touch.curNativePos;
            touch.curNativePos = windowPos;
        }

        public void AddResponder(Responder responder) => this.m_responders.Add(responder);

        public void RemoveResponder(Responder responder) => this.m_responders.Remove(responder);

        public void TouchesBegan(Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesBegan(touchArray, touchCount);
        }

        public void TouchesMoved(Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesMoved(touchArray, touchCount);
        }

        public void TouchesEnded(Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesEnded(touchArray, touchCount);
        }

        public void TouchesCancelled(Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesCancelled(touchArray, touchCount);
        }

        public void ShakeBegin()
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].ShakeBegin();
        }

        public void ShakeEnded()
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].ShakeEnded();
        }

        public void ShakeCancelled()
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].ShakeCancelled();
        }
    }
}
