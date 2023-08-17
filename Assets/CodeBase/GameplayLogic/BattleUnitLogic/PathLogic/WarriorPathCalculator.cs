using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class WarriorPathCalculator: UnitPathCalculator
    {
        public WarriorPathCalculator(IBoardTilesContainer boardTilesContainer, IUnitsStateContainer unitsStateContainer, int boardSize) : base(boardTilesContainer,unitsStateContainer, boardSize)
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