using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitSelectValidator : IUnitSelectValidator
    {
        private IUnitsStateContainer _unitsStateContainer;
        
        public UnitSelectValidator(IUnitsStateContainer unitsStateContainer)
        {
            _unitsStateContainer = unitsStateContainer;
        }

        public BattleUnit TryToSelectUnit(ITurnManager turnManager,Vector2Int index)
        {
            BattleUnit selectedUnit = _unitsStateContainer.GetUnitByIndex(index);

            if (selectedUnit == null || selectedUnit.TeamType != turnManager.TeamOfTurn) return null;
            else return selectedUnit;


        }
    }
}