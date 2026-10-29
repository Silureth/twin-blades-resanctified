using Microsoft.Xna.Framework;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.UI;
using Resanctified.Engine.World;
using Resanctified.Engine.World.Entities;

namespace Resanctified.Game.World.Entities
{
    public class FxManaDrain : Entity
    {
        private const float FX_EMIT_OFFSET_Y = 1f;
        private const float FX_EMIT_TIME = 0.2f;
        private const float FX_EMIT_SPEED = 1.5f;
        private const float ORB_DISTANCE_MIN = 0.1f;
        private Animation2DView m_view;
        private Animation2D m_anim;
        private int m_state;
        private float m_timer;

        public FxManaDrain()
        {
            this.m_view = new Animation2DView();
            this.m_anim = new Animation2D();
            this.m_type = 8;
            this.m_state = 0;
            this.m_timer = 0.0f;
        }

        public override void Initialize()
        {
            this.m_anim.Initialize("Anims\\fx_manadrain");
            this.m_anim.SetLoop(Animation2DLoop.Always);
            this.m_view.Initialize();
            this.m_view.SetBlendFunc(BlendFunc.SrcAlpha_InvSrcAlpha);
            this.m_view.GetTransform().SetPosition(0.0f, 0.0f, -1f);
            this.m_view.SetAnimation2D(this.m_anim);
            this.m_view.GetAnimation2D().SetTime(0.0f);
            this.m_state = 0;
            this.m_timer = 0.0f;
            this.IsActive = false;
        }

        public override void Update(float gameTime)
        {
            if (!this.IsActive)
                return;
            this.m_view.Update(gameTime);
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            switch (this.m_state)
            {
                case 1:
                    this.m_timer += gameTime;
                    this.GetTransform().SetPosition(currentWorld.GetHero().GetTransform().GetPosition().X, (float)(currentWorld.GetHero().GetTransform().GetPosition().Y + 1.0 + 1.5 * this.m_timer));
                    if (this.m_timer < 0.200000002980232)
                        break;
                    this.m_timer = 0.0f;
                    ++this.m_state;
                    break;
                case 2:
                    this.m_timer += gameTime;
                    if (this.m_timer > 8.0)
                    {
                        this.Destroy();
                        break;
                    }
                    float x1 = this.GetTransform().GetPosition().X;
                    float y1 = this.GetTransform().GetPosition().Y;
                    Vector2 vector2 = new Vector2(x1 - currentWorld.GetMagicOrb().GetTransform().GetPosition().X, y1 - (currentWorld.GetMagicOrb().GetTransform().GetPosition().Y + 0.9f));
                    if ((double)vector2.LengthSquared() <= 0.100000001490116)
                    {
                        this.Destroy();
                        break;
                    }
                    vector2.Normalize();
                    vector2 *= 1.5f * gameTime;
                    float x2 = x1 - vector2.X;
                    float y2 = y1 - vector2.Y;
                    this.GetTransform().SetPosition(x2, y2);
                    break;
            }
        }

        public override void Cleanup()
        {
            this.m_anim.Cleanup();
            this.m_view.Cleanup();
        }

        public void Emit()
        {
            Renderer.GetInstance().AddTopLevelView(this.m_view, this.GetTransform());
            this.m_state = 1;
            this.m_timer = 0.0f;
            this.IsActive = true;
        }

        public void Destroy()
        {
            this.IsActive = false;
            this.m_state = 0;
            Renderer.GetInstance().RemoveTopLevelView(this.m_view);
        }
    }
}
