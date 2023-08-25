using System;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsComanderMediator : IService
    {
        event Action OnUnitSelected;
        event Action OnUnitUnselected;
        void NotifyAboutSelectedUnit();
        void NotifyAboutUnselectedUnit();

    }
}