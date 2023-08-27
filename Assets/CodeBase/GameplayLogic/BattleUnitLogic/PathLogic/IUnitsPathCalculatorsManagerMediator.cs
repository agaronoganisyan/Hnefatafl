using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public interface IUnitsPathCalculatorsManagerMediator
    {
        event Action<IUnitPath> OnPathCalculated;

        void Notify(IUnitPath unitPath);
    }
}