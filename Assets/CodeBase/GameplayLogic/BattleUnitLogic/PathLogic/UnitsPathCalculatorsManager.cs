using System;
using System.Collections.Generic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class UnitsPathCalculatorsManager : IUnitsPathCalculatorsManager
    {
        private IUnitsPathCalculatorsManagerMediator _unitsPathCalculatorsManagerMediator;
        
        private readonly Dictionary<UnitType, UnitPathCalculator> _calculators = new Dictionary<UnitType, UnitPathCalculator>();
        
        public void Initialize()
        {
            _unitsPathCalculatorsManagerMediator = ServiceLocator.Get<IUnitsPathCalculatorsManagerMediator>();
        }
        
        public void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator)
        {
            _calculators.Add(unitType,calculator);
        }

        public IUnitPath CalculatePath(BattleUnit battleUnit)
        {
            if (_calculators.TryGetValue(battleUnit.UnitType, out var calculator))
            {
                IUnitPath unitPath = calculator.CalculatePaths(battleUnit.Index);
                
                _unitsPathCalculatorsManagerMediator.Notify(unitPath);
                
                return unitPath;
            }

            return null;
        }
        
        public bool IsUnitHasAvailableMoves(BattleUnit battleUnit)
        {
            if (_calculators.TryGetValue(battleUnit.UnitType, out var calculator))
            {
                return calculator.IsThereAvailableMoves(battleUnit.Index);
            }

            return false;
        }
    }
}