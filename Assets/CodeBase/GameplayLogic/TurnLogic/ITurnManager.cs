using System;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.TurnLogic
{
    public interface ITurnManager : IService
    {
        public TeamType TeamOfTurn { get; }
        public BattleUnit SelectedUnit  { get; }
        public IUnitPath SelectedUnitPath { get; }
        public void SwitchTeamOfTurn();
        public void Prepare();
        void SelectUnit(BattleUnit unit, IUnitPath selectedUnitPath);
        public void UnselectUnit();
    }
}