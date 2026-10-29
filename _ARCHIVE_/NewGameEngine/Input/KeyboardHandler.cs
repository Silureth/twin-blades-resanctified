// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Input.KeyboardHandler
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework.Input;

namespace PSS.GameEngine.Input
{
  public class KeyboardHandler
  {
    private KeyboardState m_oldState;
    private KeyboardState m_curState;

    public void Initialize()
    {
      this.m_oldState = Keyboard.GetState();
      this.m_curState = this.m_oldState;
    }

    public void Cleanup()
    {
    }

    public void Update(float gameTime)
    {
      this.m_oldState = this.m_curState;
      this.m_curState = Keyboard.GetState();
    }

    public bool IsKeyPressed(Keys key) => this.m_curState.IsKeyDown(key) && !this.m_oldState.IsKeyDown(key);

    public bool IsKeyReleased(Keys key) => this.m_oldState.IsKeyDown(key) && !this.m_curState.IsKeyDown(key);

    public bool IsKeyDown(Keys key) => this.m_curState.IsKeyDown(key);

    public bool IsKeyUp(Keys key) => this.m_curState.IsKeyUp(key);
  }
}
