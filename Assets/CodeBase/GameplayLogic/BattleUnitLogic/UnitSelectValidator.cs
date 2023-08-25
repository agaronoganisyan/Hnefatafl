using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitSelectValidator : IUnitSelectValidator
    {
        private IUnitsStateContainer _unitsStateContainer;
        public void Initialize()
        {
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();

        }
        public BattleUnit TryToSelectUnit(ITurnManager turnManager,Vector2Int index)
        {
            BattleUnit selectedUnit = _unitsStateContainer.GetUnitByIndex(index);

            if (selectedUnit == null || selectedUnit.TeamType != turnManager.TeamOfTurn) return null;
            else return selectedUnit;
        }
    }
}