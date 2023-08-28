using System.Threading.Tasks;
using UnityEngine;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.GameFactoryLogic;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.Infrastructure.Services.StaticData;
using CodeBase.NetworkLogic;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Awake()
        {
            GameInitialization();
        }

        async void GameInitialization()
        {
            RegisterAndInitializeAssetsProvider();
            
            await RegisterServices();
            await InitializeServices();
            
            StartGame();
            
            ServiceLocator.Get<IAssetsProvider>().CleanUp();
        }

        void RegisterAndInitializeAssetsProvider()
        {
            ServiceLocator.Register<IAssetsProvider>( new AssetsProvider());
            ServiceLocator.Register<IGameInfrastructureFactory>(new GameInfrastructureFactory());
            ServiceLocator.Get<IAssetsProvider>().Initialize();
            ServiceLocator.Get<IGameInfrastructureFactory>().Initialize();
        }

        private async Task RegisterServices()
        {
            //ServiceLocator.Register<IAssetsProvider>( new AssetsProvider());
            //ServiceLocator.Register<IGameInfrastructureFactory>(new GameInfrastructureFactory());

            ServiceLocator.Register<INetworkManager>(
                await ServiceLocator.Get<IGameInfrastructureFactory>().CreateNetworkManager());
            
            ServiceLocator.Register<IGameModeStaticDataService>(new GameModeStaticDataService());
            //ServiceLocator.Register<IRuleManagerMediator>(new RuleManagerMediator());
            ServiceLocator.Register<IRuleManager>(new RuleManager());
           // ServiceLocator.Register<IInputServiceMediator>(new InputServiceMediator());
            ServiceLocator.Register<IInputService>(new InputService());
            ServiceLocator.Register<IBoardTilesContainer>(new BoardTilesContainer());
            ServiceLocator.Register<IUnitsStateContainer>(new UnitsStateContainer());
            ServiceLocator.Register<ITeamsUnitsContainer>(new TeamsUnitsContainer());
            ServiceLocator.Register<WayToKillKing>(new WayToKillKing());
            ServiceLocator.Register<WayToKillWarrior>(new WayToKillWarrior());
            ServiceLocator.Register<IKillsHandler>( new KillsHandler());
           // ServiceLocator.Register<IUnitsPathCalculatorsManagerMediator>(new UnitsPathCalculatorsManagerMediator());
            ServiceLocator.Register<IUnitsPathCalculatorsManager>(new UnitsPathCalculatorsManager());
            ServiceLocator.Register<KingPathCalculator>(new KingPathCalculator());
            ServiceLocator.Register<WarriorPathCalculator>(new WarriorPathCalculator());
            ServiceLocator.Register<ITeamMoveValidator>(new TeamMoveValidator());
            //ServiceLocator.Register<ITurnManagerMediator>(new TurnManagerMediator());
            ServiceLocator.Register<ITurnManager>(new TurnManager());
            //ServiceLocator.Register<IUnitsComanderMediator>(new UnitsComanderMediator());
            ServiceLocator.Register<IUnitMoveValidator>(new UnitMoveValidator());
            ServiceLocator.Register<IUnitSelectValidator>(new UnitSelectValidator());
            ServiceLocator.Register<IUnitsPlacementHandler>(new UnitsPlacementHandler());
            ServiceLocator.Register<IUnitsCommander>(new UnitsCommander());
            ServiceLocator.Register<IUnitsFactory>(new UnitsFactory());
            ServiceLocator.Register<IUnitsSpawner>(new UnitsSpawner());
            ServiceLocator.Register<IInputHandler>( new InputHandler());
        }
        
        private async Task InitializeServices()
        {
            // ServiceLocator.Get<IAssetsProvider>().Initialize();
            
            ServiceLocator.Get<IGameModeStaticDataService>().Initialize();
            await ServiceLocator.Get<IGameModeStaticDataService>().LoadModeData(GameModeType.Classic);

            //ServiceLocator.Get<IRuleManagerMediator>().Initialize();
            ServiceLocator.Get<IRuleManager>().Initialize();
            //ServiceLocator.Get<IInputServiceMediator>().Initialize();
            ServiceLocator.Get<IInputService>().Initialize();
            
            ServiceLocator.Get<IBoardTilesContainer>().Initialize();
            ServiceLocator.Get<IBoardTilesContainer>().GenerateBoard();

            ServiceLocator.Get<IUnitsStateContainer>().Initialize();
            ServiceLocator.Get<IUnitsStateContainer>().GenerateContainer();

            ServiceLocator.Get<ITeamsUnitsContainer>().Initialize();
            ServiceLocator.Get<WayToKillKing>().Initialize();
            ServiceLocator.Get<WayToKillWarrior>().Initialize();
            
            ServiceLocator.Get<IKillsHandler>().Initialize();
            ServiceLocator.Get<IKillsHandler>().AddWayToKill(UnitType.King, ServiceLocator.Get<WayToKillKing>());
            ServiceLocator.Get<IKillsHandler>().AddWayToKill(UnitType.Warrior, ServiceLocator.Get<WayToKillWarrior>());
            
            //ServiceLocator.Get<IUnitsPathCalculatorsManagerMediator>().Initialize();
            ServiceLocator.Get<IUnitsPathCalculatorsManager>().Initialize();
            ServiceLocator.Get<KingPathCalculator>().Initialize();
            ServiceLocator.Get<WarriorPathCalculator>().Initialize();
            
            ServiceLocator.Get<IUnitsPathCalculatorsManager>().AddUnitPathCalculator(UnitType.King,ServiceLocator.Get<KingPathCalculator>());
            ServiceLocator.Get<IUnitsPathCalculatorsManager>().AddUnitPathCalculator(UnitType.Warrior,ServiceLocator.Get<WarriorPathCalculator>());

            ServiceLocator.Get<ITeamMoveValidator>().Initialize();
            //ServiceLocator.Get<ITurnManagerMediator>().Initialize();
            
            ServiceLocator.Get<ITurnManager>().Initialize();
            ServiceLocator.Get<ITurnManager>().Prepare();

            //ServiceLocator.Get<IUnitsComanderMediator>().Initialize();
            ServiceLocator.Get<IUnitMoveValidator>().Initialize();
            ServiceLocator.Get<IUnitSelectValidator>().Initialize();
            ServiceLocator.Get<IUnitsPlacementHandler>().Initialize();
            ServiceLocator.Get<IUnitsCommander>().Initialize();
            ServiceLocator.Get<IUnitsFactory>().Initialize();
            
            ServiceLocator.Get<IUnitsSpawner>().Initialize();
            await ServiceLocator.Get<IUnitsSpawner>().InitializeUnits();
            ServiceLocator.Get<IUnitsSpawner>().PrepareUnits();
            
            ServiceLocator.Get<IInputHandler>().Initialize();
            
            //ServiceLocator.Get<IGameInfrastructureFactory>().Initialize();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateBoard();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateBoardHighlight();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateGameplayCanvas();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateDebriefingCanvas();
            await ServiceLocator.Get<IGameInfrastructureFactory>().CreateLobbyCanvas();

        }
        
        void StartGame()
        {
            ServiceLocator.Get<IRuleManager>().StartGame();
        }
    }
}