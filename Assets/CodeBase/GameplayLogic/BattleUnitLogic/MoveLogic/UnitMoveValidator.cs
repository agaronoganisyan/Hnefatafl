using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public class UnitMoveValidator : IUnitMoveValidator
    {
        private IUnitsStateContainer _unitsStateContainer;

        public void Initialize()
        {
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();
        }
        
        public bool IsUnitCanMove(IUnitPath unitPath, Vector2Int newIndex)
        {
            return !_unitsStateContainer.IsThereUnit(newIndex) && unitPath.IsPathHasIndex(newIndex);
        }
        
    }
}