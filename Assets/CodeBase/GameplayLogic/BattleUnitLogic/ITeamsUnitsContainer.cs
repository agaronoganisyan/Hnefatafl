using System.Collections.Generic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface ITeamsUnitsContainer
    {
        IReadOnlyList<BattleUnit> AllWhiteUnits { get; }
        IReadOnlyList<BattleUnit> AllBlackUnits{ get; }
        void AddWhiteUnit(BattleUnit newUnit);
        void AddBlackUnit(BattleUnit newUnit);
        bool IsThisUnitTypeIsAlive(TeamType teamType, UnitType unitType);
    }
}