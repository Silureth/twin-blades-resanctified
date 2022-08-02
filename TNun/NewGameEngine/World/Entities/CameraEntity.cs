// Decompiled with JetBrains decompiler
// Type: PSS.GameEngine.World.Entities.CameraEntity
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\GameEngine.dll

using Microsoft.Xna.Framework;
using PSS.GameEngine.Maths;
using System;

namespace PSS.GameEngine.World.Entities
{
  public class CameraEntity : Entity
  {
    private const float CAMERA_DEFAULT_FOLLOW_SMOOTH_FACTOR_X = 1.5f;
    private const float CAMERA_DEFAULT_FOLLOW_SMOOTH_FACTOR_Y = 2.5f;
    private const float CAMERA_DEFAULT_FOLLOW_MAX_SPEED_X = 8.5f;
    private const float CAMERA_DEFAULT_FOLLOW_MAX_SPEED_Y = 8.5f;
    private const float CAMERA_DEFAULT_FOLLOW_OFFSET_X = 0.0f;
    private const float CAMERA_DEFAULT_FOLLOW_OFFSET_Y = 0.3f;
    private const float CAMERA_STOP_SHAKE_THRESHOLD = 0.0001f;
    protected float m_zNear;
    protected float m_zFar;
    protected float m_orthoWidth;
    protected float m_orthoHeight;
    protected Matrix m_view;
    protected Matrix m_projection;
    protected bool m_projectionUpdated;
    protected InterfaceOrientation m_prevInterfaceOrientation;
    private Vector2 m_disp;
    protected AABB m_boundingBox;
    protected Entity m_followEntity;
    protected float m_followSmoothFactorX;
    protected float m_followSmoothFactorY;
    protected float m_followMaxSpeedX;
    protected float m_followMaxSpeedY;
    protected float m_followOffsetX;
    protected float m_followOffsetY;
    protected bool m_lockedX;
    protected bool m_lockedY;
    protected float m_shakeCurTime;
    protected float m_shakeMaxTime;
    protected float m_shakeAngularSpeed;
    protected float m_shakeDecay;
    protected Vector2 m_shakeVectorStart;
    protected Vector2 m_shakeVectorEnd;
    protected bool m_isShaking;

    public CameraEntity()
    {
      this.m_projectionUpdated = false;
      this.m_followEntity = (Entity) null;
      this.m_type = 2;
      this.m_lockedX = false;
      this.m_lockedY = false;
    }

    public override void Initialize()
    {
      base.Initialize();
      this.m_projectionUpdated = false;
      this.m_followEntity = (Entity) null;
      this.m_disp.Y = 0.0f;
      this.m_disp.Y = 0.0f;
      this.m_isShaking = false;
      this.m_lockedX = false;
      this.m_lockedY = false;
    }

    public override void Cleanup()
    {
      this.m_followEntity = (Entity) null;
      base.Cleanup();
    }

    public override void Update(float elapsedTime)
    {
      if (this.m_followEntity != null)
      {
        Vector3 worldPosition1 = this.m_followEntity.GetTransform().GetWorldPosition();
        if ((double) this.m_followEntity.GetTransform().GetScale().X < 0.0)
          worldPosition1.X -= this.m_followOffsetX;
        else
          worldPosition1.X += this.m_followOffsetX;
        worldPosition1.Y += this.m_followOffsetY;
        if (this.m_lockedX)
          worldPosition1.X = this.m_followOffsetX;
        if (this.m_lockedY)
          worldPosition1.Y = this.m_followOffsetY;
        Vector3 worldPosition2 = this.m_transform.GetWorldPosition();
        Vector2 vector2 = new Vector2(worldPosition1.X - worldPosition2.X, worldPosition1.Y - worldPosition2.Y);
        float num = vector2.LengthSquared();
        vector2.Normalize();
        vector2.X *= num * this.m_followSmoothFactorX;
        vector2.Y *= num * this.m_followSmoothFactorY;
        if ((double) Math.Abs(vector2.X) > (double) this.m_followMaxSpeedX)
          vector2.X = (float) Math.Sign(vector2.X) * this.m_followMaxSpeedX;
        if ((double) Math.Abs(vector2.Y) > (double) this.m_followMaxSpeedY)
          vector2.Y = (float) Math.Sign(vector2.Y) * this.m_followMaxSpeedY;
        this.m_disp = vector2 * elapsedTime;
        this.m_transform.SetPosition(worldPosition2.X + this.m_disp.X, worldPosition2.Y + this.m_disp.Y, worldPosition2.Z);
      }
      if (this.m_isShaking)
      {
        Vector2 vector2 = Vector2.Lerp(this.m_shakeVectorStart, this.m_shakeVectorEnd, this.m_shakeCurTime / this.m_shakeMaxTime) * (float) Math.Cos((double) (this.m_shakeAngularSpeed * this.m_shakeCurTime));
        Vector3 position = this.m_transform.GetPosition();
        position.X += vector2.X;
        position.Y += vector2.Y;
        this.m_transform.SetPosition(position);
        this.m_shakeCurTime += elapsedTime;
        if ((double) this.m_shakeCurTime >= (double) this.m_shakeMaxTime)
          this.m_isShaking = false;
      }
      base.Update(elapsedTime);
    }

    public void FollowEntity(Entity entity) => this.FollowEntity(entity, 1.5f, 2.5f, 8.5f, 0.0f, 8.5f, 0.3f, false, false);

    public void FollowEntity(
      Entity entity,
      float smoothFactorX,
      float smoothFactorY,
      float maxSpeedX,
      float offsetX,
      float maxSpeedY,
      float offsetY,
      bool lockedX,
      bool lockedY)
    {
      this.m_followEntity = entity;
      this.m_followSmoothFactorX = smoothFactorX;
      this.m_followSmoothFactorY = smoothFactorY;
      this.m_followMaxSpeedX = maxSpeedX;
      this.m_followMaxSpeedY = maxSpeedY;
      this.m_followOffsetX = offsetX;
      this.m_followOffsetY = offsetY;
      this.m_lockedX = lockedX;
      this.m_lockedY = lockedY;
    }

    public bool IsLockedX() => this.m_lockedX;

    public bool IsLockedY() => this.m_lockedY;

    public void StartShaking(
      float duration,
      float angularSpeed,
      Vector2 startAmplitude,
      Vector2 endAmplitude)
    {
      this.m_shakeCurTime = 0.0f;
      this.m_shakeMaxTime = duration;
      this.m_shakeAngularSpeed = angularSpeed;
      this.m_shakeVectorStart = startAmplitude;
      this.m_shakeVectorEnd = endAmplitude;
      this.m_isShaking = true;
    }

    public void StartShaking() => this.StartShaking(1f, 47.12389f, new Vector2(0.0f, 0.2f), Vector2.Zero);

    public void StopShaking() => this.m_isShaking = false;

    public bool IsShaking() => this.m_isShaking;

    public void SetOrthogonalProjection(float width, float height) => this.SetOrthogonalProjection(width, height, 0.0f, 1f);

    public void SetOrthogonalProjection(float width, float height, float zNear, float zFar)
    {
      this.m_zNear = zNear;
      this.m_zFar = zFar;
      this.m_orthoWidth = width;
      this.m_orthoHeight = height;
      this.m_projectionUpdated = false;
    }

    public float GetOrthoWidth() => this.m_orthoWidth;

    public float GetOrthoHeight() => this.m_orthoHeight;

    public float GetZNear() => this.m_zNear;

    public float GetZFar() => this.m_zFar;

    public Matrix GetViewMatrix()
    {
      Vector3 worldPosition = this.m_transform.GetWorldPosition();
      Matrix.CreateTranslation(-worldPosition.X, -worldPosition.Y, -worldPosition.Z, out this.m_view);
      return this.m_view;
    }

    public AABB GetBoundingBox()
    {
      Vector3 worldPosition = this.m_transform.GetWorldPosition();
      this.m_boundingBox.InitializeFromBounds(new Vector3(worldPosition.X - this.m_orthoWidth * 0.5f, worldPosition.Y - this.m_orthoHeight * 0.5f, this.m_zNear), new Vector3(worldPosition.X + this.m_orthoWidth * 0.5f, worldPosition.Y + this.m_orthoHeight * 0.5f, this.m_zFar));
      return this.m_boundingBox;
    }

    public Matrix GetProjectionMatrix()
    {
      if (!this.m_projectionUpdated)
      {
        this.m_projection = Matrix.CreateOrthographic(this.m_orthoWidth, this.m_orthoHeight, this.m_zNear, this.m_zFar);
        this.m_projectionUpdated = true;
      }
      return this.m_projection;
    }

    public Vector2 GetDisplacement() => this.m_disp;
  }
}
