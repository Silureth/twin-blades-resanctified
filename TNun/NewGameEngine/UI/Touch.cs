// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.UI.Touch
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;

namespace PSS.GameEngine.UI
{
  public class Touch
  {
    public TouchId touchId;
    public TouchPhase touchPhase;
    public Vector2 prevPos;
    public Vector2 curPos;
    public int tapCount;
    public double timestamp;
    internal Vector2 prevNativePos;
    internal Vector2 curNativePos;
    internal int nativeTouchId;
  }
}
