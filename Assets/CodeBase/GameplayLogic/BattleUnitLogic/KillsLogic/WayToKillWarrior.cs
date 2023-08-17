
using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BoardLogic;
using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public class WayToKillWarrior : WayToKill
    {
        public WayToKillWarrior(IBoardTilesContainer boardTilesContainer, IUnitsStateContainer unitsStateContainer) : base(boardTilesContainer,unitsStateContainer)
        {
        }

        public override void TryToKill(Vector2Int caughtUnitIndex, TeamType currentUnitTeamType, UnitType caughtUnitType, Action killActon, Vector2Int direction = new Vector2Int())
        {
            Vector2Int nextIndex = caughtUnitIndex + direction;
            if (IsCaughtUnitSurroundedOnThisSide(nextIndex, currentUnitTeamType, caughtUnitType)) killActon.Invoke();
        }

        public override bool IsCaughtUnitSurroundedOnThisSide(Vector2Int index, TeamType currentUnitTeam, UnitType caughtUnitType)
        {
            if(_boardTilesContainer.IsIndexOnBoard(index))
            {
                BattleUnit neighboringUnit = _unitsStateContainer.GetUnitByIndex(index);

                if (neighboringUnit != null)
                {
                    if (neighboringUnit.TeamType == currentUnitTeam) return true;
                    else return false;
                }
                else
                {
                    TileType tileType = _boardTilesContainer.GetTileTypeByIndex(index);

                    if (tileType == TileType.Thron || tileType == TileType.Shelter) return true;
                    else return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}