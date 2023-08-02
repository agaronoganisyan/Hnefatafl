using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic;
using CodeBase.GameplayLogic.UILogic;
using CodeBase.GameplayLogic.UILogic.DebriefingCanvasLogic;
using CodeBase.GameplayLogic.UILogic.GameplayCanvasLogic;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] Board _board;
        [SerializeField] BoardHighlight _boardHighlight;
        [SerializeField] UnitsManager _unitsManager;
        [SerializeField] Controller _controller;

        [SerializeField] GameplayCanvas _gameplayCanvas;
        [SerializeField] DebriefingCanvas _debriefingCanvas;

        private void Awake()
        {
            ServiceLocator.Register(_board);
            ServiceLocator.Register(_unitsManager);
            ServiceLocator.Register(_controller);
            ServiceLocator.Register(_boardHighlight);

            ServiceLocator.Register(_gameplayCanvas);
            ServiceLocator.Register(_debriefingCanvas);

            GameManager gameManager = new GameManager();
            ServiceLocator.Register(gameManager);

            ServiceLocator.Get<GameManager>().InitializeGame();
        }

        private void Start()
        {
            ServiceLocator.Get<GameManager>().StartGame();
        }
    }
}