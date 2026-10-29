using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.World;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState.InGame
{
    public class InGameStatePlay : IDrawableState, IState
    {
        public bool InGameMenuRequest;
        public bool GameOverRequest;
        private static InGameStatePlay m_instance = new InGameStatePlay();

        public static InGameStatePlay Instance => m_instance;

        public static InGameStatePlay GetInstance() => m_instance;

        public void Enter(IFiniteStateMachine stateMachine)
        {
            this.InGameMenuRequest = false;
            this.GameOverRequest = false;
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            Application.GetInstance().AddResponder(currentWorld.GetHud());
        }

        public void Update(IFiniteStateMachine stateMachine, float gameTime)
        {
            if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
            {
                stateMachine.SetState(InGameStateScript.GetInstance());
            }
            else
            {
                GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
                currentWorld.Update(gameTime);
                if (this.GameOverRequest)
                {
                    this.GameOverRequest = false;
                    stateMachine.SetState(InGameStateGameOver.GetInstance());
                }
                else
                {
                    if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Story)
                    {
                        if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Shop)
                        {
                            if (currentWorld.IsShopEnabled && currentWorld.GetNPCShop().EnterShopRequest)
                            {
                                currentWorld.GetNPCShop().EnterShopRequest = false;
                                GameStateInGame.GetInstance().ShopRequest = true;
                                return;
                            }
                            if (currentWorld.IsShopExitEnabled && currentWorld.GetHero().GetTransform().GetPosition().X <= -0.5)
                            {
                                stateMachine.SetState(InGameStateEndLevel.GetInstance());
                                return;
                            }
                        }
                        else if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Boss)
                        {
                            if (currentWorld.LevelIndex == GameWorld.LevelId.Level_Chapel_Boss)
                            {
                                if (currentWorld.GetBoss().IsDead())
                                    Scenario.GetInstance().RaiseEventEndLevel(currentWorld.LevelIndex);
                            }
                            else if (currentWorld.GetBoss().IsDead() && currentWorld.ZombiesAliveNb <= 0)
                            {
                                stateMachine.SetState(InGameStateEndLevelStory.GetInstance());
                                return;
                            }
                        }
                        else if (currentWorld.GetCurrentLevel().GetLevelType() == Level.LevelType.Normal)
                        {
                            if (currentWorld.GetHero().GetTransform().GetPosition().X < 8.0 || currentWorld.ZombiesLeftToKill <= 0 && currentWorld.ZombiesAliveNb <= 0)
                                currentWorld.GetHud().ShowRightArrow(true);
                            else
                                currentWorld.GetHud().ShowRightArrow(false);
                            if (currentWorld.ZombiesLeftToKill <= 0 && currentWorld.ZombiesAliveNb <= 0 && currentWorld.GetHero().GetTransform().GetPosition().X >= (double)currentWorld.EndOfLevelPosX)
                            {
                                stateMachine.SetState(InGameStateEndLevelStory.GetInstance());
                                return;
                            }
                        }
                    }
                    else if (GameApplication.GetInstance().GetGameMode() == GameApplication.GameMode.Survival)
                    {
                        if (currentWorld.GetHero().GetTransform().GetPosition().X < 8.0 || currentWorld.GetHero().GetTransform().GetPosition().X > currentWorld.EndOfLevelPosX - 8.0)
                            currentWorld.GetHud().ShowRightArrow(true);
                        else
                            currentWorld.GetHud().ShowRightArrow(false);
                        if (currentWorld.GetHero().GetTransform().GetPosition().X >= (double)currentWorld.EndOfLevelPosX)
                        {
                            stateMachine.SetState(InGameStateEndLevel.GetInstance());
                            return;
                        }
                    }
                    if (InputManager.GetInstance().IsBackPressed() || InputManager.GetInstance().IsPausePressed())
                        this.InGameMenuRequest = true;
                    if (!this.InGameMenuRequest)
                        return;
                    this.InGameMenuRequest = false;
                    AudioManager.GetInstance().PauseAll();
                    stateMachine.SetState(InGameStateMenu.GetInstance());
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
            Application.GetInstance().RemoveResponder(currentWorld.GetHud());
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
        }
    }
}
