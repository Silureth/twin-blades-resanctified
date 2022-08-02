// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Input.MouseHandler
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework.Input;

namespace PSS.GameEngine.Input
{
  public class MouseHandler
  {
    private MouseState m_previousMouseState;
    private int m_deltaX;
    private int m_deltaY;
    private int m_deltaZ;

    public void Initialize() => this.m_previousMouseState = Mouse.GetState();

    public void Cleanup()
    {
    }

    public void Update(float gameTime)
    {
      MouseState state = Mouse.GetState();
      this.m_deltaX = state.X - this.m_previousMouseState.X;
      this.m_deltaY = state.Y - this.m_previousMouseState.Y;
      this.m_deltaZ = state.ScrollWheelValue - this.m_previousMouseState.ScrollWheelValue;
      this.m_previousMouseState = state;
    }
  }
}
