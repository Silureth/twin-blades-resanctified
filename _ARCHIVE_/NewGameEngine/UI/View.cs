// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.View
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using PSS.GameEngine.Graphics.Renderer;
using PSS.GameEngine.Maths;
using System;
using System.Collections.Generic;

namespace PSS.GameEngine.UI
{
  public abstract class View : IComparable<View>
  {
    private const BlendFunc VIEW_BLENDFUNC_DEFAULT = BlendFunc.SrcAlpha_InvSrcAlpha;
    private const bool VIEW_OPAQUE_DEFAULT = false;
    private const bool VIEW_ACTIVE_DEFAULT = true;
    private const bool VIEW_VISIBLE_DEFAULT = true;
    private const bool VIEW_USE_CUSTOM_COLOR_DEFAULT = true;
    private const int VIEW_SUBVIEWLIST_CAPACITY_DEFAULT = 16;
    private static readonly Color VIEW_CUSTOM_COLOR_DEFAULT = Color.White;
    protected View m_parent;
    protected List<View> m_subviews;
    protected bool m_active;
    protected bool m_visible;
    protected bool m_opaque;
    protected BlendFunc m_blendFunc;
    protected bool m_useCustomColor;
    protected Color m_customColor;
    protected Transform m_transform;
    protected float m_renderWorldZ;

    public View()
    {
      this.m_parent = (View) null;
      this.m_subviews = new List<View>(16);
      this.m_visible = true;
      this.m_active = true;
      this.m_opaque = false;
      this.m_blendFunc = BlendFunc.SrcAlpha_InvSrcAlpha;
      this.m_customColor = View.VIEW_CUSTOM_COLOR_DEFAULT;
      this.m_useCustomColor = true;
      this.m_transform = new Transform();
    }

    public virtual void Initialize()
    {
      this.DetachFromParent();
      this.m_parent = (View) null;
      this.m_transform.Clear();
      this.m_subviews.Clear();
      this.m_active = true;
      this.m_visible = true;
      this.m_opaque = false;
      this.m_blendFunc = BlendFunc.SrcAlpha_InvSrcAlpha;
      this.m_customColor = View.VIEW_CUSTOM_COLOR_DEFAULT;
      this.m_useCustomColor = true;
      this.m_renderWorldZ = 0.0f;
    }

    public virtual void Cleanup()
    {
      this.DetachFromParent();
      this.m_parent = (View) null;
    }

    public void AddSubview(View subview)
    {
      subview.m_parent = this;
      subview.m_transform.SetParent(this.m_transform);
      this.m_subviews.Add(subview);
    }

    public void RemoveSubview(View subview)
    {
      if (!this.m_subviews.Remove(subview))
        return;
      subview.m_parent = (View) null;
      subview.m_transform.SetParent((Transform) null);
    }

    public void DetachFromParent()
    {
      if (this.m_parent == null)
        return;
      this.m_parent.RemoveSubview(this);
    }

    public int GetNumSubviews() => this.m_subviews.Count;

    public View GetSubviewAtIndex(int index) => this.m_subviews[index];

    public abstract void Draw();

    public abstract void Update(float gameTime);

    public bool IsActive() => this.m_active;

    public virtual void SetActive(bool active) => this.m_active = active;

    public bool IsVisible() => this.m_visible;

    public void SetVisible(bool visible) => this.m_visible = visible;

    public virtual bool IsInsideCamera(AABB cameraBoundingBox) => this.m_visible;

    public Transform GetTransform() => this.m_transform;

    public void SetBlendFunc(BlendFunc func) => this.m_blendFunc = func;

    public BlendFunc GetBlendFunc() => this.m_blendFunc;

    public bool IsOpaque() => this.m_opaque;

    public void SetOpaque(bool opaque) => this.m_opaque = opaque;

    public void SetCustomColor(Color color) => this.m_customColor = color;

    public void SetCustomColor(byte r, byte g, byte b, byte a)
    {
      this.m_customColor.R = r;
      this.m_customColor.G = b;
      this.m_customColor.B = b;
      this.m_customColor.A = a;
    }

    public void SetCustomColorEnabled(bool enabled) => this.m_useCustomColor = enabled;

    public virtual void PreRender()
    {
      this.m_transform.PreRender();
      this.m_renderWorldZ = this.m_transform.GetWorldTransform().M43;
    }

    public virtual void PostRender() => this.m_transform.PostRender();

    public int CompareTo(View other)
    {
      float num = this.GetTransform().GetWorldPosition().Z - other.GetTransform().GetWorldPosition().Z;
      if ((double) num < 0.0)
        return -1;
      return (double) num > 0.0 ? 1 : 0;
    }
  }
}
