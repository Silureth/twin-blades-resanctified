// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionAnimationPlay
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Resanctified.Engine.Collections;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World.Entities;
using System.Collections.Generic;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionAnimationPlay : IScriptFunction
  {
    private Entity m_entity;
    private bool m_scriptBlocking;
    private PList<Animation2D> m_animationList;
    private bool m_isAnimationSet;
    private float m_currentTime;
    private int m_loopNb;

    public ScriptFunctionAnimationPlay(
      Entity entity,
      bool scriptBlocking,
      PList<Animation2D> animationList,
      int loopNb)
    {
      this.m_entity = entity;
      this.m_scriptBlocking = scriptBlocking;
      this.m_animationList = animationList;
      this.m_isAnimationSet = false;
      this.m_currentTime = 0.0f;
      this.m_loopNb = loopNb;
    }

    public ScriptFunctionAnimationPlay(ScriptFunctionAnimationPlay function)
    {
      this.m_entity = function.m_entity;
      this.m_scriptBlocking = function.m_scriptBlocking;
      this.m_animationList = new PList<Animation2D>((IEnumerable<Animation2D>) function.m_animationList);
      this.m_isAnimationSet = function.m_isAnimationSet;
      this.m_currentTime = function.m_currentTime;
      this.m_loopNb = function.m_loopNb;
    }

    public bool Update(float time) => this.m_scriptBlocking || true;

    public void Initialize()
    {
      if (this.m_scriptBlocking)
        return;
      ScriptManager.Instance.AddScriptFunctionSecondary((IScriptFunction) new ScriptFunctionAnimationPlay(this)
      {
        m_scriptBlocking = true
      });
    }

    public void Cleanup()
    {
      if (this.m_animationList == null)
        return;
      this.m_animationList.Clear();
      this.m_animationList = (PList<Animation2D>) null;
    }
  }
}
