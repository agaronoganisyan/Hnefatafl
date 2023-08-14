using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;
using CodeBase.GameplayLogic.BoardLogic;
using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.KillsLogic
{
    public class WayToKillWarrior : WayToKill
    {
        public WayToKillWarrior(Board board, UnitsManager unitsManager) : base(board, unitsManager)
        {
        }

        public override void TryToKill(Vector2Int caughtUnitIndex, TeamType currentUnitTeamType, UnitType caughtUnitType, Action killActon, Vector2Int direction = new Vector2Int())
        {
            Vector2Int nextIndex = caughtUnitIndex + direction;
            if (IsCaughtUnitSurroundedOnThisSide(nextIndex, currentUnitTeamType, caughtUnitType)) killActon.Invoke();
        }

        public override bool IsCaughtUnitSurroundedOnThisSide(Vector2Int index, TeamType currentUnitTeam, UnitType caughtUnitType)
        {
            if(_board.IsIndexOnBoard(index))
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