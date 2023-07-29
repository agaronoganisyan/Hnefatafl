using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] Board _board;
        [SerializeField] UnitsManager _unitsManager;

        private void Awake()
        {
            ServiceLocator.Register<Board>(_board);
            ServiceLocator.Register<UnitsManager>(_unitsManager);

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