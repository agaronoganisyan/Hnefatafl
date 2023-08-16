using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitsPathCalculatorsManager
    {
        Action<IUnitPath> OnPathCalculated { get;  set; }
        
        public void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator);

        public IUnitPath CalculatePath(BattleUnit battleUnit);
    }
}