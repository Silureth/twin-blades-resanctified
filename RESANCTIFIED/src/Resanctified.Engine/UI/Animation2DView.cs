using Resanctified.Engine.Graphics;
using Resanctified.Engine.Maths;
using Resanctified.Engine.Resources;

namespace Resanctified.Engine.UI
{
    public sealed class Animation2DView : View
    {
        private Animation2D m_anim;
        private Texture m_srcTexture;
        private Texture m_dstTexture;

        public Animation2DView()
        {
            this.m_anim = null;
            this.m_srcTexture = null;
            this.m_dstTexture = null;
        }

        public void SetAnimation2D(Animation2D anim)
        {
            this.m_anim = anim;
            this.m_useCustomColor = false;
        }

        public void SetAnimation2D(Animation2D anim, bool preserveCustomColor)
        {
            this.m_anim = anim;
            if (preserveCustomColor)
                return;
            this.m_useCustomColor = false;
        }

        public Animation2D GetAnimation2D() => this.m_anim;

        public void RemapTexture(Texture src, Texture dst)
        {
            Resource.SafeRelease(ref this.m_srcTexture);
            Resource.SafeRelease(ref this.m_dstTexture);
            if (src != null)
            {
                this.m_srcTexture = src;
                this.m_srcTexture.AddRef();
            }
            if (dst == null)
                return;
            this.m_dstTexture = dst;
            this.m_dstTexture.AddRef();
        }

        public override void Initialize()
        {
            base.Initialize();
            this.m_anim = null;
            Resource.SafeRelease(ref this.m_srcTexture);
            Resource.SafeRelease(ref this.m_dstTexture);
        }

        public override void Cleanup()
        {
            this.m_anim = null;
            Resource.SafeRelease(ref this.m_srcTexture);
            Resource.SafeRelease(ref this.m_dstTexture);
            base.Cleanup();
        }

        public override void Draw()
        {
            if (!this.m_visible)
                return;
            Graphics.Renderer.Renderer.GetInstance().DrawAnimation2DModel(this.m_anim.GetModel(), this.m_anim.GetFrame(), this.m_transform, this.m_blendFunc, this.m_useCustomColor, this.m_customColor, this.m_srcTexture, this.m_dstTexture, this.m_opaque);
        }

        public override void Update(float gameTime)
        {
            if (this.m_anim == null)
                return;
            this.m_anim.Update(gameTime);
        }

        public override bool IsInsideCamera(AABB cameraBoundingBox)
        {
            if (!this.m_visible)
                return false;
            AABB transformedBox;
            MathHelperEx.TransformBox(out transformedBox, this.m_anim.GetModel().GetBoundingBox(), this.m_transform.GetWorldTransform());
            return MathHelperEx.BoxIntersectsBox2D(transformedBox, cameraBoundingBox);
        }
    }
}
