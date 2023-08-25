using CodeBase.GameplayLogic.BoardLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class KingPathCalculator: UnitPathCalculator
    {
        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_boardTilesContainer.IsIndexOnBoard(index) ||
                   _unitsStateContainer.IsThereUnit(index);

        }
    }
}