using System;
using CodeBase.GameplayLogic.BattleUnitLogic;
using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.TurnLogic
{
    public interface ITurnManager : IService
    {
        ITurnManagerMediator TurnManagerMediator { get; }
        TeamType TeamOfTurn { get; }
        BattleUnit SelectedUnit  { get; }
        IUnitPath SelectedUnitPath { get; }
        void SwitchTeamOfTurn();
        void Prepare();
        void SelectUnit(BattleUnit unit, IUnitPath selectedUnitPath);
        void UnselectUnit();
    }
}