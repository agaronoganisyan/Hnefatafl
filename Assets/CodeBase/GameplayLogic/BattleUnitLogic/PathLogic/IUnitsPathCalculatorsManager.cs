using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitsPathCalculatorsManager : IService
    {
        IUnitsPathCalculatorsManagerMediator Mediator { get; }
        void AddUnitPathCalculator(UnitType unitType, UnitPathCalculator calculator);
        IUnitPath CalculatePath(BattleUnit battleUnit);
        bool IsUnitHasAvailableMoves(BattleUnit battleUnit);
    }
}