using Microsoft.Xna.Framework;

namespace Resanctified.Engine.Maths
{
    public sealed class Transform
    {
        private Vector3 m_position;
        private float m_rotationZ;
        private Vector2 m_scale;
        private Matrix m_local;
        private Matrix m_world;
        private Transform m_parent;
        private bool m_localUpdated;
        private bool m_worldLocked;
        private bool m_identity;

        public Transform() => this.Clear();

        public void Clear()
        {
            this.m_worldLocked = false;
            this.m_parent = null;
            this.SetIdentity();
        }

        public void SetIdentity()
        {
            this.m_position.X = 0.0f;
            this.m_position.Y = 0.0f;
            this.m_position.Z = 0.0f;
            this.m_scale.X = 1f;
            this.m_scale.Y = 1f;
            this.m_rotationZ = 0.0f;
            this.m_local = Matrix.Identity;
            this.m_localUpdated = true;
            this.m_identity = true;
        }

        public Matrix GetLocalTransform()
        {
            if (!this.m_localUpdated)
            {
                Matrix.CreateScale(this.m_scale.X, this.m_scale.Y, 1f, out this.m_local);
                if (this.m_rotationZ != 0.0)
                {
                    Matrix result;
                    Matrix.CreateRotationZ(this.m_rotationZ, out result);
                    this.m_local *= result;
                }
                this.m_local.M41 = this.m_position.X;
                this.m_local.M42 = this.m_position.Y;
                this.m_local.M43 = this.m_position.Z;
                this.m_localUpdated = true;
            }
            return this.m_local;
        }

        public Matrix GetWorldTransform()
        {
            if (this.m_worldLocked)
                return this.m_world;
            if (this.m_parent == null || this.m_parent.m_identity)
                return this.GetLocalTransform();
            this.m_world = this.GetLocalTransform() * this.m_parent.GetWorldTransform();
            return this.m_world;
        }

        public void PreRender()
        {
            if (this.m_worldLocked)
                return;
            this.m_world = this.m_parent == null ? this.GetLocalTransform() : this.GetLocalTransform() * this.m_parent.GetWorldTransform();
            this.m_worldLocked = true;
        }

        public void PostRender() => this.m_worldLocked = false;

        public Vector3 GetPosition() => this.m_position;

        public float GetRotationZ() => this.m_rotationZ;

        public Vector2 GetScale() => this.m_scale;

        public Transform GetParent() => this.m_parent;

        public Vector3 GetWorldPosition()
        {
            Matrix worldTransform = this.GetWorldTransform();
            return new Vector3(worldTransform.M41, worldTransform.M42, worldTransform.M43);
        }

        public void SetWorldPosition(Vector3 worldPos)
        {
            Matrix worldTransform = this.GetWorldTransform();
            this.SetPosition(worldPos.X - worldTransform.M41, worldPos.Y - worldTransform.M42, worldPos.Z - worldTransform.M43);
        }

        public Vector2 GetWorldPosition2D()
        {
            Matrix worldTransform = this.GetWorldTransform();
            return new Vector2(worldTransform.M41, worldTransform.M42);
        }

        public void SetParent(Transform parent) => this.m_parent = parent;

        public void SetPosition(float x, float y, float z)
        {
            this.m_position.X = x;
            this.m_position.Y = y;
            this.m_position.Z = z;
            this.m_localUpdated = false;
            this.m_identity = false;
        }

        public void SetPosition(Vector3 position) => this.SetPosition(position.X, position.Y, position.Z);

        public void SetPosition(float x, float y) => this.SetPosition(x, y, 0.0f);

        public void SetRotationZ(float rz)
        {
            this.m_rotationZ = rz;
            this.m_localUpdated = false;
            this.m_identity = false;
        }

        public void SetScale(Vector2 scale) => this.SetScale(scale.X, scale.Y);

        public void SetScale(float x, float y)
        {
            this.m_scale.X = x;
            this.m_scale.Y = y;
            this.m_localUpdated = false;
            this.m_identity = false;
        }

        public void SetScale(float s) => this.SetScale(s, s);
    }
}
