using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Resanctified.Engine.Event;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Engine.World.Zone
{
    public class Trigger
    {
        private const int ENTITY_STATUS_NONE = 0;
        private const int ENTITY_STATUS_LEFT = 1;
        private const int ENTITY_STATUS_RIGHT = 2;
        private const int ENTITY_STATUS_TOP = 16;
        private const int ENTITY_STATUS_BOTTOM = 32;
        private const int ENTITY_STATUS_INSIDE = 256;
        private List<Entity> m_enablingEntityList;
        private List<string> m_enablingEntityNameList;
        private List<int> m_enablingEntityCurrentStatus;
        private List<int> m_enablingEntityPreviousStatus;
        private List<bool> m_enablingEntityEntered;
        private bool m_activated;
        public GameEvent OnEnterEvent;
        public Vector3 Position;

        public float Width { get; protected set; }

        public float Height { get; protected set; }

        public string Name { get; protected set; }

        public bool IsActive { get; set; }

        public bool ActivateOnlyOnce { get; set; }

        public bool ActivateOnAnyEntity { get; set; }

        public List<Entity> EnablingEntityList => this.m_enablingEntityList;

        public List<string> EnablingEntityNameList => this.m_enablingEntityNameList;

        public Trigger(string name, Vector3 position, float width, float height)
          : this(name, position, width, height, null, null, false, false, true)
        {
        }

        public Trigger(
          string name,
          Vector3 position,
          float width,
          float height,
          Entity enablingEntity)
          : this(name, position, width, height, enablingEntity, null, false, false, true)
        {
        }

        public Trigger(
          string name,
          Vector3 position,
          float width,
          float height,
          Entity enablingEntity,
          GameEvent onEnterEvent,
          bool activateOnlyOnce,
          bool activateOnAnyEntity,
          bool isActive)
        {
            this.Name = name;
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.OnEnterEvent = onEnterEvent;
            this.m_enablingEntityList = new List<Entity>();
            this.m_enablingEntityNameList = new List<string>();
            if (enablingEntity != null)
                this.m_enablingEntityList.Add(enablingEntity);
            this.IsActive = isActive;
            this.ActivateOnlyOnce = activateOnlyOnce;
            this.ActivateOnAnyEntity = activateOnAnyEntity;
            this.m_activated = false;
            this.m_enablingEntityCurrentStatus = new List<int>();
            this.m_enablingEntityPreviousStatus = new List<int>();
            this.m_enablingEntityEntered = new List<bool>();
        }

        public void Initialize()
        {
            if (this.m_enablingEntityNameList.Count == 0)
                return;
            this.m_enablingEntityList.Clear();
            for (int index = 0; index < this.m_enablingEntityNameList.Count; ++index)
            {
                int status = this.GetStatus(index);
                this.m_enablingEntityCurrentStatus.Add(status);
                this.m_enablingEntityPreviousStatus.Add(status);
                this.m_enablingEntityEntered.Add(false);
            }
            this.m_enablingEntityNameList.Clear();
        }

        private int GetStatus(int index)
        {
            Vector3 position = this.EnablingEntityList[index].GetTransform().GetPosition();
            int status = 0;
            if (position.X < (double)this.Position.X)
                status |= 1;
            if (position.X > this.Position.X + (double)this.Width)
                status |= 2;
            if (position.Y < (double)this.Position.Y)
                status |= 32;
            if (position.Y > this.Position.Y + (double)this.Height)
                status |= 16;
            if (position.X >= (double)this.Position.X && position.X <= this.Position.X + (double)this.Width && position.Y >= (double)this.Position.Y && position.Y <= this.Position.Y + (double)this.Height)
                status = 256;
            return status;
        }

        public void Update(float gameTime)
        {
            if (!this.IsActive)
                return;
            for (int index = 0; index < this.EnablingEntityList.Count; ++index)
            {
                int status = this.GetStatus(index);
                if (status == 256)
                {
                    if (this.m_enablingEntityCurrentStatus[index] != 256 && !this.m_enablingEntityEntered[index])
                        this.m_enablingEntityPreviousStatus[index] = this.m_enablingEntityCurrentStatus[index];
                    this.m_enablingEntityEntered[index] = true;
                }
                else if (this.m_enablingEntityEntered[index] && status == this.m_enablingEntityPreviousStatus[index])
                    this.m_enablingEntityEntered[index] = false;
                this.m_enablingEntityCurrentStatus[index] = status;
            }
            int num = 0;
            for (int index = 0; index < this.m_enablingEntityEntered.Count; ++index)
            {
                if (this.m_enablingEntityEntered[index])
                    ++num;
            }
            if (this.ActivateOnAnyEntity && num > 0)
                num = this.m_enablingEntityEntered.Count;
            if (num == this.m_enablingEntityEntered.Count)
            {
                if (this.m_activated)
                    return;
                if (this.OnEnterEvent != null)
                    this.OnEnterEvent.RaiseEvent();
                this.m_activated = true;
                if (!this.ActivateOnlyOnce)
                    return;
                this.IsActive = false;
            }
            else
                this.m_activated = false;
        }
    }
}
