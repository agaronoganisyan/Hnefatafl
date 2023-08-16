using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic;
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
        private GameModeStaticData _modeStaticData;

        private IBoardTilesContainer _boardTilesContainer;
        private IBoard _board;
        private IBoardHighlight _boardHighlight;
        private IUnitsManager _unitsManager;
        private IUnitsStateContainer _unitsStateContainer;
        private IUnitsComander _unitsComander;
        private IUnitsSpawner _unitsSpawner;
        private IUnitsPathCalculatorsManager _unitsPathCalculatorsManager;
        private ITurnManager _turnManager;
        private IUnitsMoveValidator _unitsMoveValidator;

        private IInputService _inputService;
        private IInputHandler _inputHandler;
        
        [SerializeField] GameplayCanvas _gameplayCanvas;
        [SerializeField] DebriefingCanvas _debriefingCanvas;
        
        private void Awake()
        {
            //Classic mode
            _modeStaticData = Resources.Load<GameModeStaticData>(AssetsPath.PathToClassicModeStaticData);
            
            //ServiceLocator.Register(_board);
            //ServiceLocator.Register(_unitsManager);
            //ServiceLocator.Register(_controller);
            //ServiceLocator.Register(_boardHighlight);

            ServiceLocator.Register(_gameplayCanvas);
            ServiceLocator.Register(_debriefingCanvas);

            GameManager gameManager = new GameManager();
            ServiceLocator.Register(gameManager);

            ServiceLocator.Get<GameManager>().InitializeGame();

            _boardTilesContainer = new BoardTilesContainer();
            _boardTilesContainer.GenerateBoard(_modeStaticData.BoardSize);

            _unitsStateContainer = new UnitsStateContainer(_modeStaticData.BoardSize);
            
            _unitsPathCalculatorsManager= new UnitsPathCalculatorsManager();
            _unitsPathCalculatorsManager.AddUnitPathCalculator(UnitType.King, new KingPathCalculator(_boardTilesContainer,_unitsStateContainer));
            _unitsPathCalculatorsManager.AddUnitPathCalculator(UnitType.Warrior, new WarriorPathCalculator(_boardTilesContainer,_unitsStateContainer));
            
            _turnManager = new TurnManager();
            _turnManager.Prepare();

            _unitsMoveValidator = new UnitsMoveValidator();
            
            _unitsComander = new UnitsComander(_turnManager,_unitsStateContainer,_unitsPathCalculatorsManager,_unitsMoveValidator);
            
            _unitsSpawner = new UnitsSpawner(_modeStaticData, new UnitsFactory(_modeStaticData),_unitsStateContainer);
            _unitsSpawner.Initialize();
            _unitsSpawner.PrepareUnits();
            
            _inputService = new InputService();
            _inputService.SetGameplayMode();
            
            _inputHandler = new InputHandler(_inputService,_turnManager,_unitsComander, _boardTilesContainer,_unitsStateContainer);
            
            _board =  Instantiate(AssetsProvider.GetCachedAsset<Board>(AssetsPath.PathToBoard));
            _board.GenerateBoard(_modeStaticData.BoardSize,_boardTilesContainer);
            
            _boardHighlight =  Instantiate(AssetsProvider.GetCachedAsset<BoardHighlight>(AssetsPath.PathToBoardHighlight));
            _boardHighlight.Initialize(_unitsPathCalculatorsManager);
            _boardHighlight.GenerateBoardHighlight(_modeStaticData.BoardSize);

            _unitsManager = new UnitsManager();
        }

        private void Start()
        {
            ServiceLocator.Get<GameManager>().StartGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))_inputService.SetGameplayMode();
            if (Input.GetKeyDown(KeyCode.S))_inputService.SetUIMode();
            
            if (Input.GetKeyDown(KeyCode.W)) _debriefingCanvas.Open();
        }
    }
}