// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.Physics2D
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using PSS.GameEngine.World.Entities;

namespace PSS.GameEngine.World
{
  public class Physics2D
  {
    public const float DEFAULT_GRAVITY = 9.8f;
    private Entity m_entity;
    public Vector2 Acceleration;
    public Vector2 Velocity;
    public Vector3 OldPos;
    public float Gravity;

    public bool IsActive { get; set; }

    public Physics2D(Entity entity)
      : this(entity, 9.8f)
    {
    }

    public Physics2D(Entity entity, float gravity)
    {
      this.m_entity = entity;
      this.Gravity = gravity;
      this.IsActive = true;
      this.Acceleration = Vector2.Zero;
      this.Velocity = Vector2.Zero;
      this.OldPos = Vector3.Zero;
    }

    public void Update(float gameTime)
    {
      if (!this.IsActive)
        return;
      this.OldPos = this.m_entity.GetTransform().GetPosition();
      this.Velocity.X += this.Acceleration.X * gameTime;
      this.Velocity.Y += this.Acceleration.Y * gameTime;
      this.Velocity.Y -= this.Gravity * gameTime;
      Vector3 position = new Vector3(this.OldPos.X, this.OldPos.Y, this.OldPos.Z);
      position.X += this.Velocity.X * gameTime;
      position.Y += this.Velocity.Y * gameTime;
      this.m_entity.GetTransform().SetPosition(position);
    }

    public void Initialize()
    {
      this.Acceleration = Vector2.Zero;
      this.Velocity = Vector2.Zero;
      this.OldPos.X = this.m_entity.GetTransform().GetPosition().X;
      this.OldPos.Y = this.m_entity.GetTransform().GetPosition().Y;
      this.OldPos.Z = this.m_entity.GetTransform().GetPosition().Z;
    }

    public void Cleanup()
    {
    }
  }
}
