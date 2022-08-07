// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Application
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Devices;
using Microsoft.Devices.Sensors;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using PSS.GameEngine.Input;
using PSS.GameEngine.Resources;
using PSS.GameEngine.Sounds;
using PSS.GameEngine.UI;
using System;
using System.Collections.Generic;
using System.Windows;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PSS.GameEngine.Graphics.Renderer;

namespace PSS.GameEngine
{
    public class Application : Game, Responder
    {
        private const int NATIVE_TOUCH_ID_NULL = -1;
        private const float ACCELEROMETER_FILTERING_FACTOR = 0.1f;
        private const float ACCELEROMETER_MIN_SHAKE_INTERVAL = 0.3f;
        private const float ACCELEROMETER_SHAKE_ACCELERATION_THRESHOLD = 1.5f;
        private const float ACCELEROMETER_SHAKE_ACCELERATION_THRESHOLD_SQUARED = 2.25f;
        private static Application s_instance;
        private InterfaceOrientation m_interfaceOrientation;
        private List<Responder> m_responders;
        private PSS.GameEngine.UI.Touch[] m_touchArray;
        private PSS.GameEngine.UI.Touch[] m_dispatchedTouchArray;
        private float m_deviceScreenWidth;
        private float m_deviceScreenHeight;
        private float m_elapsedTimeSinceLastFrame;
        private Accelerometer m_accelerometer;

        private Vector3 m_accelerometerValue;
        private float m_accelerometerTimer;
        //private GamerServicesComponent m_gamerServiceInstance;

        public static Application Instance => Application.s_instance;

        public static Application GetInstance() => Application.s_instance;

        //public GamerServicesComponent GetGamerServiceInstance() => this.m_gamerServiceInstance;

        public Application()
        {
            Application.s_instance = this;
            // this.m_gamerServiceInstance = (GamerServicesComponent) null;
            //if (Environment.DeviceType != null)
            //  ;
            this.m_responders = new List<Responder>(64);
            this.m_touchArray = new PSS.GameEngine.UI.Touch[5];
            this.m_dispatchedTouchArray = new PSS.GameEngine.UI.Touch[5];
            this.m_elapsedTimeSinceLastFrame = 0.0f;
            this.m_accelerometer = new Accelerometer();
            this.m_accelerometerValue = Vector3.Zero;
            this.m_accelerometerTimer = 0.0f;
        }

        protected override void Initialize()
        {
            this.m_responders.Clear();
            for (int index = 0; index < 5; ++index)
            {
                this.m_touchArray[index] = new PSS.GameEngine.UI.Touch();
                this.m_touchArray[index].nativeTouchId = -1;
                this.m_dispatchedTouchArray[index] = (PSS.GameEngine.UI.Touch)null;
            }
            //this.m_accelerometer.CurrentValueChanged += new SensorReadingEventArgs<AccelerometerReading>(this.OnAccelerometerReadingChanged);
            this.m_accelerometer.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<AccelerometerReading>>(this.OnAccelerometerReadingChanged);
            try
            {
                this.m_accelerometerTimer = 0.0f;
                this.m_accelerometer.Start();
            }
            catch (AccelerometerFailedException ex)
            {
            }
            this.SetInterfaceOrientation(InterfaceOrientation.LandscapeLeft);
            this.m_deviceScreenWidth = (float)this.GraphicsDevice.Viewport.Width;
            this.m_deviceScreenHeight = (float)this.GraphicsDevice.Viewport.Height;
            ResourceManager.Instance.Initialize();
            InputManager.Instance.Initialize();
            PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().Initialize();
            AudioManager.Instance.Initialize();
            base.Initialize();
        }


        protected virtual void Cleanup()
        {
            this.m_responders.Clear();
            try
            {
                this.m_accelerometer.Stop();
            }
            catch (AccelerometerFailedException ex)
            {
            }
            AudioManager.Instance.Cleanup();
            ResourceManager.Instance.Cleanup();
            InputManager.Instance.Cleanup();
            PSS.GameEngine.Graphics.Renderer.Renderer.GetInstance().Cleanup();
        }

        private void OnAccelerometerReadingChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e) => this.AccelerometerShakeReadingChanged(e);
        //private void OnAccelerometerReadingChanged(object sender, SensorReadingEventArgs<AccelerometerReading> e) => ((DependencyObject)Deployment.Current).Dispatcher.BeginInvoke((Action)(() => this.AccelerometerShakeReadingChanged(e)));

        private void AccelerometerShakeReadingChanged(SensorReadingEventArgs<AccelerometerReading> e)
        {
            this.m_accelerometerValue.X = (float)(e.SensorReading.Acceleration.X * 0.100000001490116 + (double)this.m_accelerometerValue.X * 0.899999976158142);
            this.m_accelerometerValue.Y = (float)(e.SensorReading.Acceleration.Y * 0.100000001490116 + (double)this.m_accelerometerValue.Y * 0.899999976158142);
            this.m_accelerometerValue.Z = (float)(e.SensorReading.Acceleration.Z * 0.100000001490116 + (double)this.m_accelerometerValue.Z * 0.899999976158142);
            float num1 = (float)e.SensorReading.Acceleration.X - this.m_accelerometerValue.X;
            float num2 = (float)e.SensorReading.Acceleration.Y - this.m_accelerometerValue.Y;
            float num3 = (float)e.SensorReading.Acceleration.Z - this.m_accelerometerValue.Z;
            if ((double)num1 * (double)num1 + (double)num2 * (double)num2 + (double)num3 * (double)num3 < 2.25 || (double)this.m_accelerometerTimer <= 0.300000011920929)
                return;
            this.m_accelerometerTimer = 0.0f;
            this.ShakeBegin();
            this.ShakeEnded();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            this.Cleanup();
            base.OnExiting(sender, args);
        }

        protected override void Update(GameTime gameTime)
        {
            this.m_elapsedTimeSinceLastFrame = (float)gameTime.ElapsedGameTime.TotalSeconds;
            InputManager.Instance.Update(this.m_elapsedTimeSinceLastFrame);
            AudioManager.Instance.Update();
            this.UpdateTouchEvents();
            this.m_accelerometerTimer += this.m_elapsedTimeSinceLastFrame;
            base.Update(gameTime);
        }

        public float GetElapsedTimeSinceLastFrame() => this.m_elapsedTimeSinceLastFrame;

        public void SetInterfaceOrientation(InterfaceOrientation orientation) => this.m_interfaceOrientation = orientation;

        public InterfaceOrientation GetInterfaceOrientation() => this.m_interfaceOrientation;

        private int GetTouchIndexFromNativeTouchId(int nativeTouchId)
        {
            for (int fromNativeTouchId = 0; fromNativeTouchId < 5; ++fromNativeTouchId)
            {
                if (this.m_touchArray[fromNativeTouchId].nativeTouchId == nativeTouchId)
                    return fromNativeTouchId;
            }
            for (int fromNativeTouchId = 0; fromNativeTouchId < 5; ++fromNativeTouchId)
            {
                if (this.m_touchArray[fromNativeTouchId].nativeTouchId == -1)
                    return fromNativeTouchId;
            }
            return -1;
        }

        private void LogNativeTouch(TouchLocation nativeTouch)
        {
        }

        private void LogTouch(PSS.GameEngine.UI.Touch touch)
        {
        }

        private Vector2 ConvertNativeTouchPositionToVirtualScreenPosition(Vector2 nativeTouchPos)
        {
            nativeTouchPos.X *= 480f / this.m_deviceScreenWidth;
            //nativeTouchPos.Y = 320f - nativeTouchPos.Y;//Fixed: for pixel5 original:  400f 

            nativeTouchPos.Y = Math.Abs(nativeTouchPos.Y - this.m_deviceScreenHeight) / (this.m_deviceScreenHeight / 320f);//320f - nativeTouchPos.Y;
            return nativeTouchPos;
        }

        public float ConvertPositionX(float x)
        {
            float oldXPercent = Renderer.OLD_SCREEN_WIDTH / x;

            float newXPercent = this.m_deviceScreenWidth / oldXPercent;

            return newXPercent;
        }
        public float ConvertPositionY(float y)
        {
            float oldYPercent = Renderer.OLD_SCREEN_HEIGHT / y;

            float newYPercent = this.m_deviceScreenHeight / oldYPercent;

            return newYPercent;
        }

        private void UpdateTouchEvents()
        {
            if (!TouchPanel.GetCapabilities().IsConnected)
                return;
            TouchCollection state = TouchPanel.GetState();
            if (state.Count == 0)
                return;
            for (int index = 0; index < state.Count; ++index)
            {
                int fromNativeTouchId = this.GetTouchIndexFromNativeTouchId(state[index].Id);
                if (fromNativeTouchId >= 0)
                {
                    switch (state[index].State)
                    {
                        case TouchLocationState.Invalid:
                        case TouchLocationState.Released:
                            this.m_touchArray[fromNativeTouchId].touchId = (TouchId)fromNativeTouchId;
                            this.m_touchArray[fromNativeTouchId].prevPos = this.m_touchArray[fromNativeTouchId].curPos;
                            this.m_touchArray[fromNativeTouchId].curPos = this.ConvertNativeTouchPositionToVirtualScreenPosition(state[index].Position);
                            this.m_touchArray[fromNativeTouchId].tapCount = 1;
                            this.m_touchArray[fromNativeTouchId].timestamp = 0.0;
                            this.m_touchArray[fromNativeTouchId].prevNativePos = this.m_touchArray[fromNativeTouchId].curNativePos;
                            this.m_touchArray[fromNativeTouchId].curNativePos = state[index].Position;
                            this.m_dispatchedTouchArray[0] = this.m_touchArray[fromNativeTouchId];
                            if (state[index].State == TouchLocationState.Released)
                            {
                                this.m_touchArray[fromNativeTouchId].touchPhase = TouchPhase.Ended;
                                this.TouchesEnded(this.m_dispatchedTouchArray, 1);
                            }
                            else
                            {
                                this.m_touchArray[fromNativeTouchId].touchPhase = TouchPhase.Cancelled;
                                this.TouchesCancelled(this.m_dispatchedTouchArray, 1);
                            }
                            this.m_touchArray[fromNativeTouchId].nativeTouchId = -1;
                            continue;
                        case TouchLocationState.Pressed:
                            this.m_touchArray[fromNativeTouchId].touchId = (TouchId)fromNativeTouchId;
                            this.m_touchArray[fromNativeTouchId].touchPhase = TouchPhase.Began;
                            this.m_touchArray[fromNativeTouchId].prevPos = this.ConvertNativeTouchPositionToVirtualScreenPosition(state[index].Position);
                            this.m_touchArray[fromNativeTouchId].curPos = this.m_touchArray[fromNativeTouchId].prevPos;
                            this.m_touchArray[fromNativeTouchId].tapCount = 1;
                            this.m_touchArray[fromNativeTouchId].timestamp = 0.0;
                            this.m_touchArray[fromNativeTouchId].prevNativePos = state[index].Position;
                            this.m_touchArray[fromNativeTouchId].curNativePos = this.m_touchArray[fromNativeTouchId].prevNativePos;
                            this.m_touchArray[fromNativeTouchId].nativeTouchId = state[index].Id;
                            this.m_dispatchedTouchArray[0] = this.m_touchArray[fromNativeTouchId];
                            this.TouchesBegan(this.m_dispatchedTouchArray, 1);
                            continue;
                        case TouchLocationState.Moved:
                            this.m_touchArray[fromNativeTouchId].touchId = (TouchId)fromNativeTouchId;
                            this.m_touchArray[fromNativeTouchId].prevPos = this.m_touchArray[fromNativeTouchId].curPos;
                            this.m_touchArray[fromNativeTouchId].curPos = this.ConvertNativeTouchPositionToVirtualScreenPosition(state[index].Position);
                            this.m_touchArray[fromNativeTouchId].tapCount = 1;
                            this.m_touchArray[fromNativeTouchId].timestamp = 0.0;
                            this.m_touchArray[fromNativeTouchId].prevNativePos = this.m_touchArray[fromNativeTouchId].curNativePos;
                            this.m_touchArray[fromNativeTouchId].curNativePos = state[index].Position;
                            this.m_touchArray[fromNativeTouchId].touchPhase = !(this.m_touchArray[fromNativeTouchId].curNativePos == this.m_touchArray[fromNativeTouchId].prevNativePos) ? TouchPhase.Moved : TouchPhase.Stationary;
                            this.m_dispatchedTouchArray[0] = this.m_touchArray[fromNativeTouchId];
                            this.TouchesMoved(this.m_dispatchedTouchArray, 1);
                            continue;
                        default:
                            continue;
                    }
                }
            }
        }

        private void UpdateAccelerometerEvents()
        {
        }

        public void AddResponder(Responder responder) => this.m_responders.Add(responder);

        public void RemoveResponder(Responder responder) => this.m_responders.Remove(responder);

        public void TouchesBegan(PSS.GameEngine.UI.Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesBegan(touchArray, touchCount);
        }

        public void TouchesMoved(PSS.GameEngine.UI.Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesMoved(touchArray, touchCount);
        }

        public void TouchesEnded(PSS.GameEngine.UI.Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesEnded(touchArray, touchCount);
        }

        public void TouchesCancelled(PSS.GameEngine.UI.Touch[] touchArray, int touchCount)
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
