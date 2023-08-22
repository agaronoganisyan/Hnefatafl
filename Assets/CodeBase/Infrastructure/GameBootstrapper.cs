using System;
using System.Threading.Tasks;
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
        private IGameManager _gameManager;
        
        string PathToClassicModeStaticData = "StaticData/ClassicModeStaticData";
        string BoardAddress = "Board";
        string BoardHighlightAddress = "BoardHighlight";
        string GameplayCanvasAddress = "GameplayCanvas";
        string DebriefingCanvasAddress = "DebriefingCanvas";

        private void Awake()
        {
            GameInitialization();
        }

        async void GameInitialization()
        {
            //Classic mode
            GameModeStaticData modeStaticData = Resources.Load<GameModeStaticData>(PathToClassicModeStaticData);

            _gameManager = new GameManager();
            
            IInputService inputService = new InputService(_gameManager);

            IAssetsProvider assetsProvider = new AssetsProvider();
            assetsProvider.Initialize();
            
            IBoardTilesContainer boardTilesContainer = new BoardTilesContainer();
            boardTilesContainer.GenerateBoard(modeStaticData.BoardSize);

            IUnitsStateContainer unitsStateContainer = new UnitsStateContainer(_gameManager,modeStaticData.BoardSize);

            ITeamsUnitsContainer teamsUnitsContainer = new TeamsUnitsContainer();
            
            IKillsHandler killsHandler = new KillsHandler(_gameManager,boardTilesContainer,unitsStateContainer,
                new WayToKillKing(boardTilesContainer,unitsStateContainer),
                new WayToKillWarrior(boardTilesContainer,unitsStateContainer));
            
            IUnitsPathCalculatorsManager unitsPathCalculatorsManager= new UnitsPathCalculatorsManager();
            unitsPathCalculatorsManager.AddUnitPathCalculator(UnitType.King, new KingPathCalculator(boardTilesContainer,unitsStateContainer,modeStaticData.BoardSize));
            unitsPathCalculatorsManager.AddUnitPathCalculator(UnitType.Warrior, new WarriorPathCalculator(boardTilesContainer,unitsStateContainer,modeStaticData.BoardSize));
            
            ITeamMoveValidator teamMoveValidator = new TeamMoveValidator(teamsUnitsContainer,unitsPathCalculatorsManager);
            
            ITurnManager turnManager = new TurnManager(_gameManager);
            turnManager.Prepare();
            
            IUnitsComander  unitsComander = new UnitsComander(turnManager,unitsStateContainer,
                unitsPathCalculatorsManager,
                new UnitMoveValidator(unitsStateContainer),
                new UnitSelectValidator(unitsStateContainer),
                new UnitsPlacementHandler(_gameManager, killsHandler, teamMoveValidator),
                boardTilesContainer);
            
            IUnitsSpawner  unitsSpawner = new UnitsSpawner(
                _gameManager,
                new UnitsFactory(teamsUnitsContainer,assetsProvider,modeStaticData),
                unitsStateContainer,
                teamsUnitsContainer,
                modeStaticData.BoardSize);
            await unitsSpawner.Initialize();
            unitsSpawner.PrepareUnits();
            
            IInputHandler inputHandler = new InputHandler(inputService,turnManager,unitsComander, boardTilesContainer);
            
            GameObject boardPrefab = await assetsProvider.Load<GameObject>(BoardAddress);
            IBoard board = Instantiate(boardPrefab).GetComponent<Board>();
            await board.GenerateBoard(modeStaticData.BoardSize,boardTilesContainer, assetsProvider);
            
            GameObject boardHighlightPrefab = await assetsProvider.Load<GameObject>(BoardHighlightAddress);
            IBoardHighlight boardHighlight = Instantiate(boardHighlightPrefab).GetComponent<BoardHighlight>();
            boardHighlight.Initialize(_gameManager,
                assetsProvider,
                unitsPathCalculatorsManager,
                unitsComander);
            await boardHighlight.GenerateBoardHighlight(modeStaticData.BoardSize);
            
            GameObject gameplayCanvasPrefab = await assetsProvider.Load<GameObject>(GameplayCanvasAddress); 
            IGameplayCanvas gameplayCanvas = Instantiate(gameplayCanvasPrefab).GetComponent<GameplayCanvas>();
            gameplayCanvas.Initialize(_gameManager,turnManager);
            
            GameObject debriefingCanvasPrefab = await assetsProvider.Load<GameObject>(DebriefingCanvasAddress);
            IDebriefingCanvas debriefingCanvas = Instantiate(debriefingCanvasPrefab).GetComponent<DebriefingCanvas>();
            debriefingCanvas.Initialize(_gameManager);

            StartGame();
            
            assetsProvider.CleanUp();
        }

        void StartGame()
        {
            _gameManager.StartGame();
        }
    }
}