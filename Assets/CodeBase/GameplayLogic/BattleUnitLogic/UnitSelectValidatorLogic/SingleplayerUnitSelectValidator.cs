using CodeBase.GameplayLogic.TurnLogic;
using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitSelectValidatorLogic
{
    public class SingleplayerUnitSelectValidator : UnitSelectValidator
    {
        public override BattleUnit TryToSelectUnit(ITurnManager turnManager,Vector2Int index)
        {
            BattleUnit selectedUnit = _unitsStateContainer.GetUnitByIndex(index);

            if (selectedUnit == null || selectedUnit.TeamType != turnManager.TeamOfTurn) return null;
            else return selectedUnit;
        }
    }
}