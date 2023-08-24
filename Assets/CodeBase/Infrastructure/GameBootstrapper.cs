using UnityEngine;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic;
using CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic;
using CodeBase.Infrastructure.Services.AssetManagement;
using CodeBase.Infrastructure.Services.Input;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private IRuleManager _ruleManager;
        
        string _classicModeStaticDataAddress = "ClassicModeStaticData";
        string _boardAddress = "Board";
        string _boardHighlightAddress = "BoardHighlight";
        string _gameplayCanvasAddress = "GameplayCanvas";
        string _debriefingCanvasAddress = "DebriefingCanvas";

        private void Awake()
        {
            GameInitialization();
        }

        async void GameInitialization()
        {
            IAssetsProvider assetsProvider = new AssetsProvider();
            assetsProvider.Initialize();
            
            //Classic mode
            GameModeStaticData modeStaticData = await assetsProvider.Load<GameModeStaticData>(_classicModeStaticDataAddress);

            IRuleManagerMediator ruleManagerMediator = new RuleManagerMediator(); 
            _ruleManager = new RuleManager(ruleManagerMediator);

            IInputServiceMediator inputServiceMediator = new InputServiceMediator();
            IInputService inputService = new InputService(inputServiceMediator,ruleManagerMediator);
            
            IBoardTilesContainer boardTilesContainer = new BoardTilesContainer();
            boardTilesContainer.GenerateBoard(modeStaticData.BoardSize);

            IUnitsStateContainer unitsStateContainer = new UnitsStateContainer(_ruleManager,modeStaticData.BoardSize);

            ITeamsUnitsContainer teamsUnitsContainer = new TeamsUnitsContainer();
            
            IKillsHandler killsHandler = new KillsHandler(boardTilesContainer,unitsStateContainer,
                new WayToKillKing(boardTilesContainer,unitsStateContainer),
                new WayToKillWarrior(boardTilesContainer,unitsStateContainer));

            IUnitsPathCalculatorsManagerMediator unitsPathCalculatorsManagerMediator = new UnitsPathCalculatorsManagerMediator();
            
            IUnitsPathCalculatorsManager unitsPathCalculatorsManager= new UnitsPathCalculatorsManager(unitsPathCalculatorsManagerMediator);
            unitsPathCalculatorsManager.AddUnitPathCalculator(UnitType.King, new KingPathCalculator(boardTilesContainer,unitsStateContainer,modeStaticData.BoardSize));
            unitsPathCalculatorsManager.AddUnitPathCalculator(UnitType.Warrior, new WarriorPathCalculator(boardTilesContainer,unitsStateContainer,modeStaticData.BoardSize));
            
            ITeamMoveValidator teamMoveValidator = new TeamMoveValidator(teamsUnitsContainer,unitsPathCalculatorsManager);

            ITurnManagerMediator turnManagerMediator = new TurnManagerMediator();
            
            ITurnManager turnManager = new TurnManager(turnManagerMediator,ruleManagerMediator);
            turnManager.Prepare();

            IUnitsComanderMediator unitsComanderMediator = new UnitsComanderMediator();
            
            IUnitsComander  unitsComander = new UnitsComander(unitsComanderMediator,
                turnManager,
                unitsStateContainer,
                unitsPathCalculatorsManager,
                new UnitMoveValidator(unitsStateContainer),
                new UnitSelectValidator(unitsStateContainer),
                new UnitsPlacementHandler(_ruleManager, killsHandler, teamMoveValidator, teamsUnitsContainer),
                boardTilesContainer);
            
            IUnitsSpawner  unitsSpawner = new UnitsSpawner(
                ruleManagerMediator,
                new UnitsFactory(teamsUnitsContainer,assetsProvider,modeStaticData),
                unitsStateContainer,
                teamsUnitsContainer,
                modeStaticData.BoardSize);
            await unitsSpawner.Initialize();
            unitsSpawner.PrepareUnits();
            
            IInputHandler inputHandler = new InputHandler(
                inputServiceMediator,
                turnManager,
                unitsComander,
                boardTilesContainer);
            
            GameObject boardPrefab = await assetsProvider.Load<GameObject>(_boardAddress);
            IBoard board = Instantiate(boardPrefab).GetComponent<Board>();
            await board.GenerateBoard(modeStaticData.BoardSize,boardTilesContainer, assetsProvider);
            
            GameObject boardHighlightPrefab = await assetsProvider.Load<GameObject>(_boardHighlightAddress);
            IBoardHighlight boardHighlight = Instantiate(boardHighlightPrefab).GetComponent<BoardHighlight>();
            boardHighlight.Initialize(ruleManagerMediator,
                assetsProvider,
                unitsPathCalculatorsManagerMediator,
                unitsComanderMediator);
            await boardHighlight.GenerateBoardHighlight(modeStaticData.BoardSize);
            
            GameObject gameplayCanvasPrefab = await assetsProvider.Load<GameObject>(_gameplayCanvasAddress); 
            IGameplayCanvas gameplayCanvas = Instantiate(gameplayCanvasPrefab).GetComponent<GameplayCanvas>();
            gameplayCanvas.Initialize(ruleManagerMediator,turnManagerMediator);
            
            GameObject debriefingCanvasPrefab = await assetsProvider.Load<GameObject>(_debriefingCanvasAddress);
            IDebriefingCanvas debriefingCanvas = Instantiate(debriefingCanvasPrefab).GetComponent<DebriefingCanvas>();
            debriefingCanvas.Initialize(_ruleManager,ruleManagerMediator);

            StartGame();
            
            assetsProvider.CleanUp();
        }

        void StartGame()
        {
            _ruleManager.StartGame();
        }
    }
}