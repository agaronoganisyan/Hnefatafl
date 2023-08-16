using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class KingPathCalculator: UnitPathCalculator
    {
        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_boardTilesContainer.IsIndexAvailableToMove(index) ||
                   _unitsStateContainer.IsThereUnit(index);

        }
    }
}