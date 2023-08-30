using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitSelectValidatorLogic
{
    public abstract class UnitSelectValidator : IUnitSelectValidator
    {
        protected IUnitsStateContainer _unitsStateContainer;
        public virtual void Initialize()
        {
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();

        }
        public abstract BattleUnit TryToSelectUnit(ITurnManager turnManager, Vector2Int index);
    }
}