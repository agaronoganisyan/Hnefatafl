using CodeBase.GameplayLogic.BoardLogic;
using UnityEngine;
using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public abstract class WayToKill : IService
    {
        protected IBoardTilesContainer _boardTilesContainer;
        protected IUnitsStateContainer _unitsStateContainer;

        public void Initialize()
        {
            _boardTilesContainer = ServiceLocator.Get<IBoardTilesContainer>();
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();
        }

        public abstract void TryToKill(Vector2Int caughtUnitIndex, TeamType currentUnitTeamType, UnitType caughtUnitType, Action killActon, Vector2Int direction = new Vector2Int());

        public abstract bool IsCaughtUnitSurroundedOnThisSide(Vector2Int index, TeamType currentUnitTeam, UnitType caughtUnitType);
    }
}