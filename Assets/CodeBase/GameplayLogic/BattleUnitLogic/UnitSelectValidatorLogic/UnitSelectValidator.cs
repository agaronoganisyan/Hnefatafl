using CodeBase.GameplayLogic.TurnLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitSelectValidatorLogic
{
    public class UnitSelectValidator : IUnitSelectValidator
    {
        protected IUnitsStateContainer _unitsStateContainer;
        public virtual void Initialize()
        {
            _unitsStateContainer = ServiceLocator.Get<IUnitsStateContainer>();

        }
        public BattleUnit TryToSelectUnit(ITurnManager turnManager,Vector2Int index)
        {
            BattleUnit selectedUnit = _unitsStateContainer.GetUnitByIndex(index);

            if (selectedUnit == null || selectedUnit.TeamType != turnManager.TeamOfTurn) return null;
            else return selectedUnit;
            
            //turnManager.TeamOfTurn != _networkManager.GetPlayerTeam()

        }
    }
}