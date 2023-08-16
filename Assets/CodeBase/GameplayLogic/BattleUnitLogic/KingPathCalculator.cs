using CodeBase.GameplayLogic.BoardLogic;
using CodeBase.GameplayLogic.TileLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class KingPathCalculator: UnitPathCalculator
    {
        public KingPathCalculator(IBoardTilesContainer boardTilesContainer, IUnitsStateContainer unitsStateContainer) : base(boardTilesContainer,unitsStateContainer)
        {

        }

        protected override bool IsThereProblemWithIndex(Vector2Int index)
        {
            return !_boardTilesContainer.IsIndexOnBoard(index) ||
                   _unitsStateContainer.IsThereUnit(index);

        }
    }
}