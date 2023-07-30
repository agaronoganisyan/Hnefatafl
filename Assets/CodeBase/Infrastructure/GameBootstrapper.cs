using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] Board _board;
        [SerializeField] BoardHighlight _boardHighlight;
        [SerializeField] UnitsManager _unitsManager;
        [SerializeField] Controller _controller;

        private void Awake()
        {
            ServiceLocator.Register<Board>(_board);
            ServiceLocator.Register<UnitsManager>(_unitsManager);
            ServiceLocator.Register<Controller>(_controller);
            ServiceLocator.Register<BoardHighlight>(_boardHighlight);

            GameManager gameManager = new GameManager();
            ServiceLocator.Register<GameManager>(gameManager);

            ServiceLocator.Get<GameManager>().InitializeGame();
        }

        private void Start()
        {
            ServiceLocator.Get<GameManager>().StartGame();
        }
    }
}