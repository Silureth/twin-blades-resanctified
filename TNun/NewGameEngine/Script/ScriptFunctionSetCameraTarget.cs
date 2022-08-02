// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.Script.ScriptFunctionSetCameraTarget
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using PSS.GameEngine.Collections;
using PSS.GameEngine.World.Entities;

namespace PSS.GameEngine.Script
{
  public class ScriptFunctionSetCameraTarget : IScriptFunction
  {
    private CameraEntity m_camera;
    private PList<Entity> m_entityList;

    public ScriptFunctionSetCameraTarget(CameraEntity camera, PList<Entity> entityList)
    {
      this.m_entityList = entityList;
      this.m_camera = camera;
    }

    public bool Update(float gameTime) => true;

    public void Initialize()
    {
    }

    public void Cleanup()
    {
      if (this.m_entityList != null)
        this.m_entityList.Clear();
      this.m_entityList = (PList<Entity>) null;
    }
  }
}
