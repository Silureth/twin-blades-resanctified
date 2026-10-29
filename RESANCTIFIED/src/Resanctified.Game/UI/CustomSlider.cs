// Decompiled with JetBrains decompiler
// Type: Resanctified.Game.UI.CustomSlider
// Assembly: TwinBlades, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 5F185CF1-B1A3-44DD-BD34-391B69A73148
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\TwinBlades.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.UI;
using System;

namespace Resanctified.Game.UI
{
  public class CustomSlider : Slider
  {
    private const float MARGIN_V = 2f;
    private const float MARGIN_H = 2f;
    private Color m_barDisabledColor;
    private Color m_barEnabledColorMin;
    private Color m_barEnabledColorMax;
    private Color m_containerDisabledColor;
    private Color m_containerEnabledColor;
    private static readonly Color BAR_COLOR_MAX = new Color(200, 0, 0, (int) byte.MaxValue);
    private static readonly Color BAR_COLOR_MIN = new Color(200, 0, 0, 30);
    private static readonly Color BAR_COLOR_DISABLED = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 128);
    private static readonly Color CONTAINER_COLOR = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 50);
    private static readonly Color CONTAINER_COLOR_DISABLED = new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue, 50);
    private VertexPositionColor[] m_quadContainer;
    private VertexPositionColor[] m_quadBar;

    public CustomSlider()
    {
      this.m_quadBar = new VertexPositionColor[4];
      this.m_quadContainer = new VertexPositionColor[4];
    }

    public override void Initialize(
      ViewController vc,
      float length,
      float radius,
      SliderOrientation orientation)
    {
      base.Initialize(vc, length, radius, orientation);
      this.m_containerEnabledColor = CustomSlider.CONTAINER_COLOR;
      this.m_containerDisabledColor = CustomSlider.CONTAINER_COLOR_DISABLED;
      this.m_barDisabledColor = CustomSlider.BAR_COLOR_DISABLED;
      this.m_barEnabledColorMax = CustomSlider.BAR_COLOR_MAX;
      this.m_barEnabledColorMin = CustomSlider.BAR_COLOR_MIN;
    }

    public override void Draw()
    {
      if (!this.m_visible)
        return;
      Color color1 = this.m_active ? this.m_containerEnabledColor : this.m_containerDisabledColor;
      Color color2 = this.m_active ? this.m_barEnabledColorMin : this.m_barDisabledColor;
      Color color3 = this.m_active ? this.m_barEnabledColorMax : this.m_barDisabledColor;
      float num = (float) Math.Floor((double) this.m_length * 0.5);
      float top = (float) Math.Floor((double) this.m_radius * 0.5);
      QuadFactory.CreateQuad(this.m_quadContainer, (float) (-(double) num - 2.0), top + 2f, (float) ((double) this.m_length + 2.0 + 2.0), (float) ((double) this.m_radius + 2.0 + 2.0), color1);
      QuadFactory.CreateQuad(this.m_quadBar, -num, top, this.m_length * this.m_normalizedPosition, this.m_radius, Color.Lerp(color2, color3, this.m_normalizedPosition));
      Resanctified.Engine.Graphics.Renderer.Renderer instance = Resanctified.Engine.Graphics.Renderer.Renderer.GetInstance();
      instance.DrawQuad(this.m_quadContainer, this.m_transform, BlendFunc.SrcAlpha_InvSrcAlpha);
      instance.DrawQuad(this.m_quadBar, this.m_transform, BlendFunc.SrcAlpha_InvSrcAlpha);
    }

    public void SetBarColor(Color disabled, Color enabledMin, Color enabledMax)
    {
      this.m_barDisabledColor = disabled;
      this.m_barEnabledColorMin = enabledMin;
      this.m_barEnabledColorMax = enabledMax;
    }

    public void SetContainerColor(Color disabled, Color enabled)
    {
      this.m_containerDisabledColor = disabled;
      this.m_containerEnabledColor = enabled;
    }
  }
}
