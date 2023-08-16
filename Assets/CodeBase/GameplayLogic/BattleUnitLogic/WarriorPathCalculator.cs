using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class WarriorPathCalculator: UnitPathCalculator
    {
        public WarriorPathCalculator(IBoardTilesContainer boardTilesContainer, IUnitsStateContainer unitsStateContainer) : base(boardTilesContainer,unitsStateContainer)
        {

        }
        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_boardTilesContainer.IsIndexOnBoard(index) ||
                   _boardTilesContainer.GetTileTypeByIndex(index) == TileType.Shelter ||
                   _unitsStateContainer.IsThereUnit(index);
        }
    }
}