using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface IUnitsComanderMediator
    {
        event Action OnUnitSelected;
        event Action OnUnitUnselected;
        void NotifyAboutSelectedUnit();
        void NotifyAboutUnselectedUnit();

    }
}