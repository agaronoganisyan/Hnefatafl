using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class UnitsComanderMediator : IUnitsComanderMediator
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