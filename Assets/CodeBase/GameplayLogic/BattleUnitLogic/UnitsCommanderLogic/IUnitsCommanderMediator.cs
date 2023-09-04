using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.UnitsCommanderLogic
{
    public interface IUnitsCommanderMediator
    {
        event Action OnUnitSelected;
        event Action OnUnitUnselected;
        void NotifyAboutSelectedUnit();
        void NotifyAboutUnselectedUnit();

    }
}