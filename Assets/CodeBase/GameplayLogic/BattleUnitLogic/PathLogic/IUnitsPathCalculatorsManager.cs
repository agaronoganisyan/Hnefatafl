using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitsPathCalculatorsManager
    {
        event Action<IUnitPath> OnPathCalculated;
        
        public void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator);

        public IUnitPath CalculatePath(BattleUnit battleUnit);

        public bool IsUnitHasAvailableMoves(BattleUnit battleUnit);
    }
}