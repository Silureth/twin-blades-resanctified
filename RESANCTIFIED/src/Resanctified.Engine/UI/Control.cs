using System.Collections.Generic;

namespace Resanctified.Engine.UI
{
    public abstract class Control : View, Responder
    {
        protected ViewController m_viewController;
        protected int m_tag;
        protected List<Responder> m_responders;

        public Control()
        {
            this.m_viewController = null;
            this.m_tag = 0;
            this.m_responders = new List<Responder>(64);
        }

        public virtual void Initialize(ViewController vc)
        {
            this.Initialize();
            this.SetViewController(vc);
            this.m_tag = 0;
            this.m_responders.Clear();
        }

        public override void Cleanup()
        {
            this.SetViewController(null);
            this.m_responders.Clear();
            base.Cleanup();
        }

        public int GetTag() => this.m_tag;

        public void SetTag(int tag) => this.m_tag = tag;

        private void SetViewController(ViewController vc)
        {
            if (vc == null && this.m_viewController != null)
                this.m_viewController.RemoveResponder(this);
            else if (vc != null && this.m_viewController != null)
            {
                this.m_viewController.RemoveResponder(this);
                vc.AddResponder(this);
            }
            else if (vc != null && this.m_viewController == null)
                vc.AddResponder(this);
            this.m_viewController = vc;
        }

        public void AddResponder(Responder responder) => this.m_responders.Add(responder);

        public void RemoveResponder(Responder responder) => this.m_responders.Remove(responder);

        public virtual void TouchesBegan(Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesBegan(touchArray, touchCount);
        }

        public virtual void TouchesMoved(Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesMoved(touchArray, touchCount);
        }

        public virtual void TouchesEnded(Touch[] touchArray, int touchCount)
        {
            for (int index = 0; index < this.m_responders.Count; ++index)
                this.m_responders[index].TouchesEnded(touchArray, touchCount);
        }

        public virtual void TouchesCancelled(Touch[] touchArray, int touchCount)
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
