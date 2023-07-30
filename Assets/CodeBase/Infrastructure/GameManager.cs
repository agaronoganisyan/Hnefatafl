using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BoardLogic;

namespace CodeBase.Infrastructure
{
    public class GameManager : IService
    {
        public void InitializeGame()
        {
            ServiceLocator.Get<Board>().Initialize();
            ServiceLocator.Get<BoardHighlight>().Initialize();
            ServiceLocator.Get<UnitsManager>().Initialize(ServiceLocator.Get<Board>());
            ServiceLocator.Get<Controller>().Initialize(ServiceLocator.Get<Board>(),ServiceLocator.Get<UnitsManager>());
        }

        public void StartGame()
        {
            ServiceLocator.Get<UnitsManager>().SpawnUnits();
        }
    }
}