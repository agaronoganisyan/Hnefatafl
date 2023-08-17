using CodeBase.GameplayLogic.BoardLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class KingPathCalculator: UnitPathCalculator
    {
        public KingPathCalculator(IBoardTilesContainer boardTilesContainer, IUnitsStateContainer unitsStateContainer, int boardSize) : base(boardTilesContainer,unitsStateContainer, boardSize)
        {

        }

        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_boardTilesContainer.IsIndexOnBoard(index) ||
                   _unitsStateContainer.IsThereUnit(index);

        }
    }
}