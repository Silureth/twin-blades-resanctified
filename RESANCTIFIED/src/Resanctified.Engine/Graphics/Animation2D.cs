// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Graphics.Animation2D
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;

namespace Resanctified.Engine.Graphics
{
    public sealed class Animation2D
    {
        private Animation2DModel m_model;
        private float m_time;
        private float m_speed;
        private Animation2DLoop m_loop;
        private bool m_isEndOfAnimationReached;

        public Animation2D()
        {
            this.m_model = (Animation2DModel)null;
            this.m_time = 0.0f;
            this.m_speed = 1f;
            this.m_loop = Animation2DLoop.None;
            this.m_isEndOfAnimationReached = false;
        }

        public void Initialize(string modelFilename)
        {
            this.Initialize(ResourceManager.GetInstance().LoadAnimation2DModel(modelFilename));
            this.m_model.Release();
        }

        public void Initialize(Animation2DModel model)
        {
            Resources.Resource.SafeRelease<Animation2DModel>(ref this.m_model);
            this.m_model = model;
            this.m_model.AddRef();
            this.m_time = 0.0f;
            this.m_speed = 1f;
            this.m_loop = Animation2DLoop.None;
            this.m_isEndOfAnimationReached = false;
        }

        public void Cleanup()
        {
            Resources.Resource.SafeRelease<Animation2DModel>(ref this.m_model);
            this.m_time = 0.0f;
            this.m_speed = 1f;
            this.m_loop = Animation2DLoop.None;
            this.m_isEndOfAnimationReached = false;
        }

        public void Update(float elapsedTime)
        {
            elapsedTime *= this.m_speed;
            this.SetTime(this.m_time + elapsedTime);
        }

        public void SetTime(float time)
        {
            if (!this.m_model.IsLoaded())
            {
                this.m_time = time;
                this.m_isEndOfAnimationReached = false;
            }
            else
            {
                float duration = this.GetDuration();
                this.m_isEndOfAnimationReached = false;
                switch (this.m_loop)
                {
                    case Animation2DLoop.None:
                        if ((double)time < 0.0)
                        {
                            time = 0.0f;
                            this.m_isEndOfAnimationReached = true;
                            break;
                        }
                        if ((double)time > (double)duration)
                        {
                            time = duration;
                            this.m_isEndOfAnimationReached = true;
                            break;
                        }
                        break;
                    case Animation2DLoop.Always:
                        if ((double)time < 0.0)
                        {
                            while ((double)time < 0.0)
                                time += duration;
                            this.m_isEndOfAnimationReached = true;
                            break;
                        }
                        if ((double)time > (double)duration)
                        {
                            while ((double)time > (double)duration)
                                time -= duration;
                            this.m_isEndOfAnimationReached = true;
                            break;
                        }
                        break;
                    case Animation2DLoop.PingPong:
                        if ((double)time < 0.0)
                        {
                            time = -time;
                            while ((double)time > (double)duration)
                                time -= duration;
                            this.m_speed = -this.m_speed;
                            this.m_isEndOfAnimationReached = true;
                            break;
                        }
                        if ((double)time > (double)duration)
                        {
                            time = duration - (time - duration);
                            while ((double)time < 0.0)
                                time += duration;
                            this.m_speed = -this.m_speed;
                            this.m_isEndOfAnimationReached = true;
                            break;
                        }
                        break;
                }
                this.m_time = time;
            }
        }

        public float GetTime() => this.m_time;

        public void SetFrame(int frame)
        {
            if (frame < 0)
                frame = 0;
            else if (frame >= this.GetFrameCount())
                frame = this.GetFrameCount() - 1;
            this.SetTime((float)frame / this.GetFPS());
        }

        public int GetFrame()
        {
            int frame = (int)((double)this.GetFPS() * (double)this.m_time);
            if (frame < 0)
                frame = 0;
            else if (frame >= this.GetFrameCount())
                frame = this.GetFrameCount() - 1;
            return frame;
        }

        public void SetSpeed(float speed) => this.m_speed = speed;

        public float GetSpeed() => this.m_speed;

        public void SetLoop(Animation2DLoop loop) => this.m_loop = loop;

        public Animation2DLoop GetLoop() => this.m_loop;

        public float GetFPS() => this.m_model.GetFPS();

        public int GetFrameCount() => this.m_model.GetFrameCount();

        public float GetDuration() => (float)this.GetFrameCount() / this.GetFPS();

        public bool IsEndOfAnimationReached() => this.m_isEndOfAnimationReached;

        public Animation2DModel GetModel() => this.m_model;

        public AABB? GetBox(string name) => this.m_model.GetBox(name, this.GetFrame());

        public AABB? GetBoxAndColor(string name, out Color color) => this.m_model.GetBoxAndColor(name, this.GetFrame(), out color);

        public int GetBoxes(AABB[] buffer, int length, string name) => this.m_model.GetBoxes(buffer, length, name, this.GetFrame());
    }
}
