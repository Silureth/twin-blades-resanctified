// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.Control
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using System.Collections.Generic;

namespace PSS.GameEngine.UI
{
  public abstract class Control : View, Responder
  {
    protected ViewController m_viewController;
    protected int m_tag;
    protected List<Responder> m_responders;

    public Control()
    {
      this.m_viewController = (ViewController) null;
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
      this.SetViewController((ViewController) null);
      this.m_responders.Clear();
      base.Cleanup();
    }

    public int GetTag() => this.m_tag;

    public void SetTag(int tag) => this.m_tag = tag;

    private void SetViewController(ViewController vc)
    {
      if (vc == null && this.m_viewController != null)
        this.m_viewController.RemoveResponder((Responder) this);
      else if (vc != null && this.m_viewController != null)
      {
        this.m_viewController.RemoveResponder((Responder) this);
        vc.AddResponder((Responder) this);
      }
      else if (vc != null && this.m_viewController == null)
        vc.AddResponder((Responder) this);
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
