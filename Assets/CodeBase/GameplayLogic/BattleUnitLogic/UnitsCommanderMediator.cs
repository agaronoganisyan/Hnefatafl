using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsCommanderMediator : IUnitsCommanderMediator
    {
        public event Action OnUnitSelected;
        public event Action OnUnitUnselected;
        public void NotifyAboutSelectedUnit()
        {
            OnUnitSelected?.Invoke();
        }

        public void NotifyAboutUnselectedUnit()
        {
            OnUnitUnselected?.Invoke();
        }
    }
}