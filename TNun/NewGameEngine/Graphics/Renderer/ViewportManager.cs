// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Graphics.Renderer.ViewportManager
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PSS.GameEngine.Graphics.Renderer
{
  public class ViewportManager
  {
    private static readonly ViewportManager s_instance = new ViewportManager();
    private Viewport[] m_viewports;
    private ViewportID m_activeViewportID;
    private Matrix[] m_spriteBatchTransforms;

    public static ViewportManager Instance => ViewportManager.s_instance;

    private void GraphicsDevice_DeviceReset(object sender, EventArgs e) => this.Initialize();

    private ViewportManager()
    {
      this.m_viewports = new Viewport[9];
      this.m_spriteBatchTransforms = new Matrix[9];
      this.m_activeViewportID = ViewportID.Default;
    }

    public void Initialize()
    {
      Viewport viewport1 = Application.Instance.GraphicsDevice.Viewport;
      this.m_viewports[0] = viewport1;
      Viewport viewport2 = viewport1;
      viewport2.Height /= 2;
      this.m_viewports[1] = viewport2;
      this.m_viewports[2] = viewport2;
      this.m_viewports[2].Y = viewport2.Height;
      Viewport viewport3 = viewport1;
      viewport3.Width /= 2;
      this.m_viewports[3] = viewport3;
      this.m_viewports[4] = viewport3;
      this.m_viewports[4].X = viewport3.Width;
      Viewport viewport4 = viewport3;
      viewport4.Height /= 2;
      this.m_viewports[5] = viewport4;
      this.m_viewports[6] = viewport4;
      this.m_viewports[6].X = viewport4.Width;
      this.m_viewports[7] = viewport4;
      this.m_viewports[7].Y = viewport4.Height;
      this.m_viewports[8] = viewport4;
      this.m_viewports[8].X = viewport4.Width;
      this.m_viewports[8].Y = viewport4.Height;
      for (int index = 0; index < this.m_spriteBatchTransforms.Length; ++index)
      {
        Matrix result1;
        Matrix.CreateScale((float) this.m_viewports[index].Height / (float) viewport1.Height, (float) this.m_viewports[index].Width / (float) viewport1.Width, 1f, out result1);
        Matrix result2;
        Matrix.CreateTranslation((float) this.m_viewports[index].X, 0.0f, 0.0f, out result2);
        Matrix.Multiply(ref result1, ref result2, out this.m_spriteBatchTransforms[index]);
      }
      this.SetActiveViewport(this.m_activeViewportID);
    }

    public void SetActiveViewport(ViewportID viewportID) => Application.Instance.GraphicsDevice.Viewport = this.m_viewports[(int) viewportID];

    public Viewport GetViewport(ViewportID viewportID) => this.m_viewports[(int) viewportID];

    public void GetViewport(ViewportID viewportID, out Viewport viewport) => viewport = this.m_viewports[(int) viewportID];

    public Matrix GetSpriteBatchTransform(ViewportID viewportID) => this.m_spriteBatchTransforms[(int) viewportID];

    public void GetSpriteBatchTransform(ViewportID viewportID, out Matrix transform) => transform = this.m_spriteBatchTransforms[(int) viewportID];
  }
}
