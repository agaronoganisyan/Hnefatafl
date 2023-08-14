using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;
using CodeBase.GameplayLogic.BoardLogic;
using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public class WayToKillKing : WayToKill
    {
        public WayToKillKing(Board board, UnitsManager unitsManager) : base(board, unitsManager)
        {
        }

        public override void TryToKill(Vector2Int caughtUnitIndex, TeamType currentUnitTeamType, UnitType caughtUnitType, Action killActon, Vector2Int direction = new Vector2Int())
        {
            Vector2Int nextDownIndex = caughtUnitIndex + new Vector2Int(0, -1);
            Vector2Int nextUpIndex = caughtUnitIndex + new Vector2Int(0, 1);
            Vector2Int nextLeftIndex = caughtUnitIndex + new Vector2Int(-1, 0);
            Vector2Int nextRightIndex = caughtUnitIndex + new Vector2Int(1, 0);

            if (IsCaughtUnitSurroundedOnThisSide(nextDownIndex, currentUnitTeamType, caughtUnitType) &&
                IsCaughtUnitSurroundedOnThisSide(nextUpIndex, currentUnitTeamType, caughtUnitType) &&
                IsCaughtUnitSurroundedOnThisSide(nextLeftIndex, currentUnitTeamType, caughtUnitType) &&
                IsCaughtUnitSurroundedOnThisSide(nextRightIndex, currentUnitTeamType, caughtUnitType)) killActon.Invoke();
        }

        public override bool IsCaughtUnitSurroundedOnThisSide(Vector2Int index, TeamType currentUnitTeam, UnitType caughtUnitType)
        {
            if (_board.IsIndexOnBoard(index))
            {
                BattleUnit neighboringUnit = _unitsStateContainer.GetUnitByIndex(index);

                if (neighboringUnit != null)
                {
                    if (neighboringUnit.TeamType == currentUnitTeam) return true;
                    else return false;
                }
                else
                {
                    TileType tileType = _board.GetTileTypeByIndex(index);

                    if (tileType == TileType.Thron) return true;
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