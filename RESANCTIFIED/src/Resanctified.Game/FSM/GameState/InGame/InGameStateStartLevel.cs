using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Graphics;
using Resanctified.Engine.Graphics.Renderer;
using Resanctified.Engine.Input;
using Resanctified.Engine.Maths;
using Resanctified.Engine.World;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStateStartLevel : IDrawableState, IState
    {
        private const float TEXT_NIGHT_0_OFFSET_X = 50f;
        private const float TEXT_DIGIT_0_OFFSET_X = 170f;
        public bool DayDisplayRequest;
        public bool InGameMenuRequest;
        private VertexPositionColor[] m_quadFade;
        private Color m_fadeColor;
        private Transform m_t = new Transform();
        private static InGameStateStartLevel m_instance = new InGameStateStartLevel();

        public static InGameStateStartLevel Instance => m_instance;

        public static InGameStateStartLevel GetInstance() => m_instance;

        public InGameStateStartLevel()
        {
            this.DayDisplayRequest = false;
            this.m_quadFade = new VertexPositionColor[4];
            this.m_fadeColor = new Color(0, 0, 0, (int)byte.MaxValue);
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
            {
                GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
                currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).SetTime(0.0f);
                currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).SetTime(0.0f);
                currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).SetTime(0.0f);
            }
            QuadFactory.CreateQuad(this.m_quadFade, 0.0f, 320f, 480f, 320f, Color.White);
            InputManager.GetInstance().ClearPadState();
            this.InGameMenuRequest = false;
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            currentWorld.GetCamera().Update(gameTime);
            currentWorld.GetEntityManager().Update(gameTime);
            currentWorld.ConstraintToZone();
            if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
            {
                if (stateMachine.GetCurrentStateTime() < 2.0)
                    return;
                if (Scenario.GetInstance().DisplayHelp)
                {
                    stateMachine.SetState(InGameStateHelp.GetInstance());
                }
                else
                {
                    bool inGameMenuRequest = this.InGameMenuRequest;
                    stateMachine.SetState(InGameStatePlay.GetInstance());
                    if (!inGameMenuRequest)
                        return;
                    InGameStatePlay.GetInstance().InGameMenuRequest = true;
                }
            }
            else if ((currentWorld.LevelIndex == GameWorld.LevelId.Level_Survival_1 || this.DayDisplayRequest) && GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
            {
                currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).Update(gameTime);
                currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).Update(gameTime);
                currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).Update(gameTime);
                if (!currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).IsEndOfAnimationReached())
                    return;
                if (currentWorld.DayIndex == 1 && currentWorld.LevelIndex == GameWorld.LevelId.Level_Survival_1)
                {
                    stateMachine.SetState(InGameStateHelp.GetInstance());
                }
                else
                {
                    bool inGameMenuRequest = this.InGameMenuRequest;
                    stateMachine.SetState(InGameStatePlay.GetInstance());
                    if (!inGameMenuRequest)
                        return;
                    InGameStatePlay.GetInstance().InGameMenuRequest = true;
                }
            }
            else
            {
                if (stateMachine.GetCurrentStateTime() < 2.0)
                    return;
                bool inGameMenuRequest = this.InGameMenuRequest;
                stateMachine.SetState(InGameStatePlay.GetInstance());
                if (!inGameMenuRequest)
                    return;
                InGameStatePlay.GetInstance().InGameMenuRequest = true;
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            this.DayDisplayRequest = false;
            this.InGameMenuRequest = false;
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            float num = 0.0f;
            if (stateMachine.GetCurrentStateTime() < 2.0)
                num = (float)(1.0 - stateMachine.GetCurrentStateTime() / 2.0) * byte.MaxValue;
            Renderer instance = Renderer.GetInstance();
            instance.Begin2DMode(false);
            this.m_fadeColor.A = (byte)num;
            instance.DrawQuad(this.m_quadFade, VertexFormat.Position3Color, null, null, this.m_fadeColor);
            if ((currentWorld.LevelIndex == GameWorld.LevelId.Level_Survival_1 || this.DayDisplayRequest) && GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
            {
                float x = 0.0f;
                if (currentWorld.DayIndex < 10)
                    x = 50f;
                this.m_t.SetPosition(x, 0.0f, 0.0f);
                instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Night).GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha);
                if (currentWorld.DayIndex >= 10)
                {
                    instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_1).GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha);
                    x += 170f;
                }
                this.m_t.SetPosition(x, 0.0f, 0.0f);
                instance.DrawAnimation2DModel(currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).GetModel(), currentWorld.GetMiscAnimation(GameWorld.MiscAnimId.Digit_0).GetFrame(), this.m_t, BlendFunc.SrcAlpha_InvSrcAlpha);
            }
            instance.End2DMode();
        }
    }
}
