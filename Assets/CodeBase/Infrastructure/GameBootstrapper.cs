using System;
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
         
        private void Awake()
        {
            //Classic mode
            GameModeStaticData modeStaticData = Resources.Load<GameModeStaticData>(AssetsPath.PathToClassicModeStaticData);

            _gameManager = new GameManager();
            
            IInputService inputService = new InputService(_gameManager);
            
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
            
            IUnitsSpawner  unitsSpawner = new UnitsSpawner(_gameManager,new UnitsFactory(teamsUnitsContainer,modeStaticData),unitsStateContainer,teamsUnitsContainer,modeStaticData.BoardSize);
            unitsSpawner.Initialize();
            unitsSpawner.PrepareUnits();
            
            IInputHandler inputHandler = new InputHandler(inputService,turnManager,unitsComander, boardTilesContainer);
            
            IBoard  board =  Instantiate(AssetsProvider.GetCachedAsset<Board>(AssetsPath.PathToBoard));
            board.GenerateBoard(modeStaticData.BoardSize,boardTilesContainer);
            
            IBoardHighlight boardHighlight =  Instantiate(AssetsProvider.GetCachedAsset<BoardHighlight>(AssetsPath.PathToBoardHighlight));
            boardHighlight.Initialize(_gameManager, unitsPathCalculatorsManager,unitsComander);
            boardHighlight.GenerateBoardHighlight(modeStaticData.BoardSize);
            
            IGameplayCanvas gameplayCanvas = Instantiate(AssetsProvider.GetCachedAsset<GameplayCanvas>(AssetsPath.PathToGameplayCanvas));
            gameplayCanvas.Initialize(_gameManager,turnManager);
            
            IDebriefingCanvas debriefingCanvas = Instantiate(AssetsProvider.GetCachedAsset<DebriefingCanvas>(AssetsPath.PathToDebriefingCanvas));
            debriefingCanvas.Initialize(_gameManager);
        }

        private void Start()
        {
            _gameManager.StartGame();
        }
    }
}