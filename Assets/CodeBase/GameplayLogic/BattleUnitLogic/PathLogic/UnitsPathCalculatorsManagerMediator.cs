using System;

namespace CodeBase.GameplayLogic.BattleUnitLogic.PathLogic
{
    public class UnitsPathCalculatorsManagerMediator : IUnitsPathCalculatorsManagerMediator
    {
        public event Action<IUnitPath> OnPathCalculated;
        public void Initialize()
        {
            
        }
        public void Notify(IUnitPath unitPath)
        {
            OnPathCalculated?.Invoke(unitPath);
        }
    }
}