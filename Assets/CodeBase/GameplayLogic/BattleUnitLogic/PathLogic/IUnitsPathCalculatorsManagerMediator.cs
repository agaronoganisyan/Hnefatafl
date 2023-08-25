using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitsPathCalculatorsManagerMediator : IService
    {
        event Action<IUnitPath> OnPathCalculated;

        void Notify(IUnitPath unitPath);
    }
}