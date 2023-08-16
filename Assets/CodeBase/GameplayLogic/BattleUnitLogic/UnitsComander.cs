using UnityEngine;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsComander : IUnitsComander
    {
        private UnitPathCalculator _unitPathCalculator;

        public UnitsComander(UnitPathCalculator unitPathCalculator)
        {
            _unitPathCalculator = unitPathCalculator;
        }

        public void SelectUnit(BattleUnit unit)
        {
            
        }

        public void MoveUnit(BattleUnit unit, Vector2Int newIndex)
        {
            
        }
    }
}