using System;
using System.Collections.Generic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class UnitsPathCalculatorsManager : IUnitsPathCalculatorsManager
    {
        private static readonly Dictionary<UnitType, UnitPathCalculator> Calculators = new Dictionary<UnitType, UnitPathCalculator>();

        public event Action<IUnitPath> OnPathCalculated;

        public void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator)
        {
            Calculators.Add(unitType,calculator);
        }

        public IUnitPath CalculatePath(BattleUnit battleUnit)
        {
            if (Calculators.TryGetValue(battleUnit.UnitType, out var calculator))
            {
                IUnitPath unitPath = calculator.CalculatePaths(battleUnit.Index);
                
                OnPathCalculated?.Invoke(unitPath);
                
                return unitPath;
            }

            return null;
        }
        
        public bool IsUnitHasAvailableMoves(BattleUnit battleUnit)
        {
            if (Calculators.TryGetValue(battleUnit.UnitType, out var calculator))
            {
                return calculator.IsThereAvailableMoves(battleUnit.Index);
            }

            return false;
        }
    }
}