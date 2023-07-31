using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure;
using UnityEngine;
using CodeBase.GameplayLogic.TileLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class Warrior : BattleUnit
    {
        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_board.IsIndexAvailableToMove(index) || _board.GetTileTypeByIndex(index) == TileType.Shelter || _unitsManager.IsThereUnit(index) ? true : false;
        }
    }
}