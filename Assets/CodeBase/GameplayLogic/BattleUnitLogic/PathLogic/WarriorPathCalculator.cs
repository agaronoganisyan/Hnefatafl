using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class WarriorPathCalculator: UnitPathCalculator
    {
        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_boardTilesContainer.IsIndexOnBoard(index) ||
                   _boardTilesContainer.GetTileTypeByIndex(index) == TileType.Shelter ||
                   _unitsStateContainer.IsThereUnit(index);
        }
    }
}