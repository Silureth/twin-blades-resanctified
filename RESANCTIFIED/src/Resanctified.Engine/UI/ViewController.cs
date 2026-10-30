using System.Collections.Generic;

namespace Resanctified.Engine.UI
{
    public class ViewController : View, Responder
    {
        private List<Responder> m_responders;

        public ViewController() => this.m_responders = new List<Responder>(64);

        /// <summary>
        /// The OK, Next or full-screen "tap to continue" button, or null. While the view takes touches, the
        /// Validate key clicks it (see Application), so keyboard players never need the mouse to move on.
        /// </summary>
        public Button DefaultButton { get; protected set; }

        public override void Initialize()
        {
            base.Initialize();
            this.m_responders.Clear();
        }

        public override void Cleanup()
        {
            base.Cleanup();
            this.m_responders.Clear();
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Graphics.Renderer.Renderer.GetInstance().Begin2DMode(false);
            for (int index = 0; index < this.m_subviews.Count; ++index)
                this.DrawSubview(this.m_subviews[index]);
            Graphics.Renderer.Renderer.GetInstance().End2DMode();
        }

        public override void Update(float elapsedTime)
        {
            if (!this.m_active)
                return;
            for (int index = 0; index < this.m_subviews.Count; ++index)
                this.UpdateSubview(this.m_subviews[index], elapsedTime);
        }

        public override void SetActive(bool active)
        {
            base.SetActive(active);
            for (int index = 0; index < this.m_subviews.Count; ++index)
                this.SetSubviewActive(this.m_subviews[index], active);
        }

        protected void DrawSubview(View view)
        {
            if (!view.IsVisible())
                return;
            view.PreRender();
            view.Draw();
            for (int index = 0; index < view.GetNumSubviews(); ++index)
                this.DrawSubview(view.GetSubviewAtIndex(index));
            view.PostRender();
        }

        protected void UpdateSubview(View view, float elapsedTime)
        {
            if (!view.IsActive())
                return;
            view.Update(elapsedTime);
            for (int index = 0; index < view.GetNumSubviews(); ++index)
                this.UpdateSubview(view.GetSubviewAtIndex(index), elapsedTime);
        }

        protected void SetSubviewActive(View view, bool active)
        {
            view.SetActive(active);
            for (int index = 0; index < view.GetNumSubviews(); ++index)
                this.SetSubviewActive(view.GetSubviewAtIndex(index), active);
        }

        public virtual void Action(View sender)
        {
        }

        public virtual void Action(View sender, ActionEvent evt)
        {
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
