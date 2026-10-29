// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.Script.ScriptFunctionMoveEntity
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.Collections;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.Script
{
  public class ScriptFunctionMoveEntity : IScriptFunction
  {
    private Entity m_entity;
    private Vector3 m_velocity;
    private Vector3 m_initialPos;
    private Vector3 m_finalPos;
    private float m_distance;
    private float m_speed;
    private bool m_scriptBlocking;
    private PList<Animation2D> m_animationList;
    private bool m_isAnimationSet;

    public ScriptFunctionMoveEntity(
      Entity entity,
      float distance,
      Vector2 velocity,
      bool scriptBlocking,
      PList<Animation2D> animationList)
    {
      this.m_entity = entity;
      this.m_finalPos = this.m_initialPos;
      this.m_speed = 0.0f;
      this.m_distance = distance;
      this.m_velocity.X = velocity.X;
      this.m_velocity.Y = velocity.Y;
      this.m_velocity.Z = 0.0f;
      this.m_scriptBlocking = scriptBlocking;
      this.m_animationList = animationList;
      this.m_isAnimationSet = false;
    }

    public ScriptFunctionMoveEntity(
      Entity entity,
      Vector2 finalPos,
      float speed,
      bool scriptBlocking,
      PList<Animation2D> animationList)
    {
      this.m_entity = entity;
      this.m_finalPos = this.m_initialPos;
      this.m_finalPos.X = finalPos.X;
      this.m_finalPos.Y = finalPos.Y;
      this.m_distance = 0.0f;
      this.m_speed = speed;
      this.m_velocity = Vector3.Zero;
      this.m_scriptBlocking = scriptBlocking;
      this.m_animationList = animationList;
      this.m_isAnimationSet = false;
    }

    public ScriptFunctionMoveEntity(ScriptFunctionMoveEntity function)
    {
      this.m_entity = function.m_entity;
      this.m_initialPos = function.m_initialPos;
      this.m_finalPos = function.m_finalPos;
      this.m_distance = function.m_distance;
      this.m_velocity = function.m_velocity;
      this.m_speed = function.m_speed;
      this.m_scriptBlocking = function.m_scriptBlocking;
      if (function.m_animationList == null)
        this.m_animationList = (PList<Animation2D>) null;
      this.m_isAnimationSet = function.m_isAnimationSet;
    }

    public bool Update(float time) => this.m_scriptBlocking || true;

    public void Initialize()
    {
      if ((double) this.m_distance > 0.0)
      {
        Vector3 vector3 = new Vector3(this.m_velocity.X, this.m_velocity.Y, this.m_velocity.Z);
        vector3.Normalize();
        this.m_finalPos = this.m_initialPos + vector3 * this.m_distance;
      }
      else
      {
        this.m_velocity.X = this.m_finalPos.X - this.m_initialPos.X;
        this.m_velocity.Y = this.m_finalPos.Y - this.m_initialPos.Y;
        this.m_velocity.Normalize();
        this.m_velocity *= this.m_speed;
      }
      if (this.m_scriptBlocking)
        return;
      ScriptManager.Instance.AddScriptFunctionSecondary((IScriptFunction) new ScriptFunctionMoveEntity(this)
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
