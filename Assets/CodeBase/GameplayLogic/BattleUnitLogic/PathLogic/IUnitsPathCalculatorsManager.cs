using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitsPathCalculatorsManager : IService
    {
        public void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator);

        public IUnitPath CalculatePath(BattleUnit battleUnit);

        public bool IsUnitHasAvailableMoves(BattleUnit battleUnit);
    }
}