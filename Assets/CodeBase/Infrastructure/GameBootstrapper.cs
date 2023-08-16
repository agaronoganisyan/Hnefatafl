using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic;
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
            
            //добавить менеджер способов расчета путей 
            //_unitsComander = new UnitsComander(new unitpa);

            _unitsSpawner = new UnitsSpawner(_modeStaticData, new UnitsFactory(_modeStaticData),_unitsStateContainer);
            _unitsSpawner.Initialize();
            _unitsSpawner.PrepareUnits();
            
            _inputService = new InputService();
            _inputService.SetGameplayMode();

            _inputHandler = new InputHandler(_inputService,_unitsComander, _boardTilesContainer,_unitsStateContainer);
            
            _board =  Instantiate(AssetsProvider.GetCachedAsset<Board>(AssetsPath.PathToBoard));
            _board.GenerateBoard(_modeStaticData.BoardSize,_boardTilesContainer);
            
            _boardHighlight =  Instantiate(AssetsProvider.GetCachedAsset<BoardHighlight>(AssetsPath.PathToBoardHighlight));
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