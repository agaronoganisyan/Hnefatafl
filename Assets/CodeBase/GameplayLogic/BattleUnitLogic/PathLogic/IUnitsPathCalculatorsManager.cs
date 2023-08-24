using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitsPathCalculatorsManager
    {
        public void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator);

        public IUnitPath CalculatePath(BattleUnit battleUnit);

        public bool IsUnitHasAvailableMoves(BattleUnit battleUnit);
    }
}