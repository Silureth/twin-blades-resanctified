// Decompiled with JetBrains decompiler
// Type: Resanctified.Engine.World.Zone.MobileGameZone
// Assembly: GameEngine, Version=1.3.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 094D4C29-E313-46E6-B898-5E30B122B0C6
// Assembly location: X:\_homebrew\TwinBlades\XAP\TwinBlades\Resanctified.Engine.dll

using Microsoft.Xna.Framework;
using Resanctified.Engine.World.Entities;
using System.Collections.Generic;

namespace Resanctified.Engine.World.Zone
{
  public class MobileGameZone : GameZone
  {
    private List<Entity> m_driverList;
    private Vector3 m_driverOffset;
    public MobileGameZone.MovementAxis MovementAxisAllowed;
    public bool UseMinYDriver;

    public MobileGameZone(
      Vector3 position,
      float width,
      float height,
      Entity driver,
      Vector3 driverOffset,
      MobileGameZone.MovementAxis movementAxisAllowed)
      : base("MobileGameZone_" + driver.GetName(), position, width, height)
    {
      this.m_driverList = new List<Entity>();
      this.m_driverList.Add(driver);
      this.m_driverOffset = driverOffset;
      this.MovementAxisAllowed = movementAxisAllowed;
      this.UseMinYDriver = true;
    }

    public void AddDriver(Entity driver) => this.m_driverList.Add(driver);

    public void ClearDriverList() => this.m_driverList.Clear();

    public void Update(float gameTime)
    {
      if (this.m_driverList.Count == 0)
        return;
      Vector3 zero = Vector3.Zero;
      for (int index = 0; index < this.m_driverList.Count; ++index)
        zero += this.m_driverList[index].GetTransform().GetPosition();
      Vector3 vector3 = zero / (float) this.m_driverList.Count;
      if (this.UseMinYDriver)
      {
        for (int index = 0; index < this.m_driverList.Count; ++index)
        {
          Vector3 position = this.m_driverList[index].GetTransform().GetPosition();
          if ((double) position.Y < (double) vector3.Y)
            vector3.Y = position.Y;
        }
      }
      vector3 -= this.m_driverOffset;
      if ((this.MovementAxisAllowed & MobileGameZone.MovementAxis.XPositive) != (MobileGameZone.MovementAxis) 0 && (double) vector3.X >= (double) this.Position.X)
        this.Position.X = vector3.X;
      if ((this.MovementAxisAllowed & MobileGameZone.MovementAxis.XNegative) != (MobileGameZone.MovementAxis) 0 && (double) vector3.X <= (double) this.Position.X)
        this.Position.X = vector3.X;
      if ((this.MovementAxisAllowed & MobileGameZone.MovementAxis.YPositive) != (MobileGameZone.MovementAxis) 0 && (double) vector3.Y >= (double) this.Position.Y)
        this.Position.Y = vector3.Y;
      if ((this.MovementAxisAllowed & MobileGameZone.MovementAxis.YNegative) == (MobileGameZone.MovementAxis) 0 || (double) vector3.Y > (double) this.Position.Y)
        return;
      this.Position.Y = vector3.Y;
    }

    public enum MovementAxis
    {
      XPositive = 1,
      XNegative = 2,
      YPositive = 16, // 0x00000010
      YNegative = 32, // 0x00000020
      All = 51, // 0x00000033
    }
  }
}
