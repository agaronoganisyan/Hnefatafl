using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using CodeBase.GameplayLogic;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.Infrastructure
{
    public class GameManager : IService
    {
        public void InitializeGame()
        {
            ServiceLocator.Get<Board>().Initialize();
            ServiceLocator.Get<UnitsManager>().Initialize();
        }

        public void StartGame()
        {
            ServiceLocator.Get<UnitsManager>().SpawnUnits();
        }
    }
}