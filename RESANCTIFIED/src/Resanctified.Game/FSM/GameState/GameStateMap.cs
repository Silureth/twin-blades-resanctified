using Resanctified.Engine;
using Resanctified.Engine.FSM;
using Resanctified.Engine.Input;
using Resanctified.Engine.Resources;
using Resanctified.Engine.Sounds;
using Resanctified.Engine.World;
using Resanctified.Game.FSM.GameState.InGame;
using Resanctified.Game.UI;
using Resanctified.Game.World;

namespace Resanctified.Game.FSM.GameState
{
    public class GameStateMap : IDrawableState, IState
    {
        private static readonly GameStateMap s_instance = new GameStateMap();
        private MapViewController m_viewController;
        private bool m_areaClearMessage;
        private bool m_deleteWorld;
        private DrawableFiniteStateMachine m_subStateMachine;
        public bool BackToMainMenuRequest;
        public bool ClearInput;
        public bool ShowObjectivesRequest;
        public bool EnterLevelRequest;
        public bool HideObjectivesRequest;

        public static GameStateMap Instance => s_instance;

        public static GameStateMap GetInstance() => s_instance;

        public MapViewController GetMapView() => this.m_viewController;

        public GameStateMap()
        {
            this.m_viewController = new MapViewController();
            this.m_subStateMachine = new DrawableFiniteStateMachine();
            this.BackToMainMenuRequest = false;
            this.ClearInput = false;
            this.ShowObjectivesRequest = false;
            this.HideObjectivesRequest = false;
            this.EnterLevelRequest = false;
            this.m_areaClearMessage = false;
            this.m_deleteWorld = false;
        }

        public void Enter(IFiniteStateMachine stateMachine)
        {
            ResourceManager.GetInstance().DestroyUnusedResources();
            ResourceManager.GetInstance().UnloadContentManagerAndReloadResources();
            GameWorld currentWorld = (GameWorld)GameWorldManager.Instance.GetCurrentWorld();
            currentWorld.GetHero().HitPoints = currentWorld.GetHero().GetHitPointsMax();
            currentWorld.GetHero().ManaPoints = currentWorld.GetHero().GetManaPointsMax();
            currentWorld.SaveGameData();
            this.m_viewController.Initialize(currentWorld.GetDayPart());
            AudioManager.Instance.LoadMusic("Sounds\\music_splash");
            int num = ResourceManager.Instance.BeginLoadingResources(Resource.ResourceType.Animation2DModel);
            for (int resourceIndex = 0; resourceIndex < num; ++resourceIndex)
                ResourceManager.Instance.LoadResource(resourceIndex);
            ResourceManager.Instance.EndLoadingResources();
            this.m_viewController.RemapTextures();
            ResourceManager.Instance.LoadAllResources();
            AudioManager.Instance.PlayMusic();
            this.BackToMainMenuRequest = false;
            this.ClearInput = false;
            this.ShowObjectivesRequest = false;
            this.HideObjectivesRequest = false;
            this.EnterLevelRequest = false;
            this.m_areaClearMessage = false;
            this.m_deleteWorld = false;
            Application.GetInstance().AddResponder(this.m_viewController);
            this.m_subStateMachine.SetState(InGameStateMapNavigation.GetInstance());
        }

        public void Update(IFiniteStateMachine stateMachine, float elapsedTime)
        {
            this.m_viewController.Update(elapsedTime);
            if (this.BackToMainMenuRequest)
            {
                this.BackToMainMenuRequest = false;
                this.m_deleteWorld = true;
                GameFSM.GetInstance().SetState(GameStateMainMenu.GetInstance());
            }
            else if (GameApplication.GetInstance().GetTextBoxViewController().IsActive())
            {
                this.m_viewController.ButtonsEnabled = false;
                if (!this.m_areaClearMessage)
                    return;
                if (InputManager.GetInstance().IsBackPressed())
                    this.HideObjectivesRequest = true;
                if (!this.HideObjectivesRequest)
                    return;
                this.HideObjectivesRequest = false;
                GameApplication.GetInstance().GetTextBoxViewController().SetActive(false);
                this.m_areaClearMessage = false;
                this.m_viewController.ButtonsEnabled = true;
            }
            else
            {
                this.m_subStateMachine.Update(elapsedTime);
                if (this.m_subStateMachine.GetCurrentState() != InGameStateMapNavigation.GetInstance())
                {
                    this.m_viewController.ButtonsEnabled = false;
                }
                else
                {
                    this.m_viewController.ButtonsEnabled = true;
                    if (this.ClearInput)
                    {
                        InputManager.GetInstance().ClearPadState();
                        this.ClearInput = false;
                    }
                    else if (this.ShowObjectivesRequest)
                    {
                        this.ShowObjectivesRequest = false;
                        this.HideObjectivesRequest = false;
                        GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, Scenario.GetInstance().GetCurrentMissionName());
                        GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                        this.m_areaClearMessage = true;
                    }
                    else
                    {
                        if (!this.EnterLevelRequest)
                            return;
                        this.EnterLevelRequest = false;
                        GameWorld currentWorld = (GameWorld)WorldManager.GetInstance().GetCurrentWorld();
                        Level level = currentWorld.GetLevel(this.m_viewController.GetSelectedLevelId());
                        if (level.GetLevelType() != Level.LevelType.Shop && (level.GetInfestationLevel() == 0 || level.Locked))
                        {
                            GameApplication.GetInstance().GetTextBoxViewController().SetText(TextBoxViewController.FaceId.Nun1, GameLocalisation.GetInstance().GetString(GameLocalisation.TextId.Dialog_Mission_Level_Clear));
                            GameApplication.GetInstance().GetTextBoxViewController().SetActive(true);
                            this.m_areaClearMessage = true;
                        }
                        else
                        {
                            currentWorld.LevelIndex = this.m_viewController.GetSelectedLevelId();
                            Scenario.GetInstance().RaiseEventSelectLevel(currentWorld.LevelIndex);
                            stateMachine.SetState(GameStateLoadLevel.GetInstance());
                        }
                    }
                }
            }
        }

        public void Exit(IFiniteStateMachine stateMachine)
        {
            Application.GetInstance().RemoveResponder(this.m_viewController);
            this.m_subStateMachine.GetCurrentState().Exit(this.m_subStateMachine);
            this.m_viewController.Cleanup();
            AudioManager.GetInstance().StopAll();
            AudioManager.GetInstance().UnloadMusic();
            InputManager.GetInstance().ClearPadState();
            GameWorld currentWorld = (GameWorld)GameWorldManager.GetInstance().GetCurrentWorld();
            currentWorld.GetHero().HitPoints = currentWorld.GetHero().GetHitPointsMax();
            currentWorld.GetHero().ManaPoints = currentWorld.GetHero().GetManaPointsMax();
            currentWorld.SaveGameData();
            if (this.m_deleteWorld)
                WorldManager.GetInstance().Cleanup(false, true);
            ResourceManager.GetInstance().DestroyUnusedResources();
        }

        public void Draw(IFiniteStateMachine stateMachine)
        {
            this.m_viewController.Draw();
            ((IDrawableState)this.m_subStateMachine.GetCurrentState()).Draw(this.m_subStateMachine);
        }
    }
}
