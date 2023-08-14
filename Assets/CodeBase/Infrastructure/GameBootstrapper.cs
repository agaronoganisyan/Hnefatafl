using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.UILogic;
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

        private IBoard _board;
        private IBoardHighlight _boardHighlight;
        private IUnitsManager _unitsManager;
        [SerializeField] Controller _controller;

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
            ServiceLocator.Register(_controller);
            //ServiceLocator.Register(_boardHighlight);

            ServiceLocator.Register(_gameplayCanvas);
            ServiceLocator.Register(_debriefingCanvas);

            GameManager gameManager = new GameManager();
            ServiceLocator.Register(gameManager);

            ServiceLocator.Get<GameManager>().InitializeGame();

            _inputService = new InputService();
            _inputService.SetGameplayMode();

            _inputHandler = new InputHandler(_inputService);
            
            _board =  Instantiate(AssetsProvider.GetCachedAsset<Board>(AssetsPath.PathToBoard));
            _board.GenerateBoard(_modeStaticData.BoardSize);
            
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