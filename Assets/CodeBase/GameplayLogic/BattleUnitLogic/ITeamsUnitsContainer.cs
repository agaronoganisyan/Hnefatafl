using System.Collections.Generic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface ITeamsUnitsContainer : IService
    {
        IReadOnlyList<BattleUnit> AllWhiteUnits { get; }
        IReadOnlyList<BattleUnit> AllBlackUnits{ get; }
        void AddWhiteUnit(BattleUnit newUnit);
        void AddBlackUnit(BattleUnit newUnit);
        bool IsThisUnitTypeIsAlive(TeamType teamType, UnitType unitType);
    }
}