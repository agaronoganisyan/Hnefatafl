using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public class UnitMoveValidator : IUnitMoveValidator
    {
        private readonly IUnitsStateContainer _unitsStateContainer;
        
        public UnitMoveValidator(IUnitsStateContainer unitsStateContainer)
        {
            _unitsStateContainer = unitsStateContainer;
        }

        public bool IsUnitCanMove(IUnitPath unitPath, Vector2Int newIndex)
        {
            return !_unitsStateContainer.IsThereUnit(newIndex) && unitPath.IsPathHasIndex(newIndex);
        }
    }
}