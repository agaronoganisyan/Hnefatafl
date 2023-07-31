using System.Collections;
using System.Collections.Generic;
using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class King : BattleUnit
    {
        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_board.IsIndexAvailableToMove(index) || _unitsManager.IsThereUnit(index) ? true : false;
        }
    }
}
