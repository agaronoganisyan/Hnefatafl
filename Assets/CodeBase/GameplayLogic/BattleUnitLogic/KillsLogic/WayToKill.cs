using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using UnityEngine;
using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public abstract class WayToKill 
    {
        protected IBoardTilesContainer _boardTilesContainer;
        protected UnitsManager _unitsManager;
        protected IUnitsStateContainer _unitsStateContainer;

        public WayToKill(IBoardTilesContainer boardTilesContainer, UnitsManager unitsManager)
        {
            _boardTilesContainer = boardTilesContainer;
            _unitsManager = unitsManager;
        }

        public abstract void TryToKill(Vector2Int caughtUnitIndex, TeamType currentUnitTeamType, UnitType caughtUnitType, Action killActon, Vector2Int direction = new Vector2Int());

        public abstract bool IsCaughtUnitSurroundedOnThisSide(Vector2Int index, TeamType currentUnitTeam, UnitType caughtUnitType);
    }
}