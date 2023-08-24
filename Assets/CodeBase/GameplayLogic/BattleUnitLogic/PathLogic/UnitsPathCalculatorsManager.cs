using System;
using System.Collections.Generic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class UnitsPathCalculatorsManager : IUnitsPathCalculatorsManager
    {
        private IUnitsPathCalculatorsManagerMediator _unitsPathCalculatorsManagerMediator;
        
        private static readonly Dictionary<UnitType, UnitPathCalculator> Calculators = new Dictionary<UnitType, UnitPathCalculator>();

        public UnitsPathCalculatorsManager(IUnitsPathCalculatorsManagerMediator unitsPathCalculatorsManagerMediator)
        {
            _unitsPathCalculatorsManagerMediator = unitsPathCalculatorsManagerMediator;
        }

        public void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator)
        {
            Calculators.Add(unitType,calculator);
        }

        public IUnitPath CalculatePath(BattleUnit battleUnit)
        {
            if (Calculators.TryGetValue(battleUnit.UnitType, out var calculator))
            {
                IUnitPath unitPath = calculator.CalculatePaths(battleUnit.Index);
                
                _unitsPathCalculatorsManagerMediator.Notify(unitPath);
                
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