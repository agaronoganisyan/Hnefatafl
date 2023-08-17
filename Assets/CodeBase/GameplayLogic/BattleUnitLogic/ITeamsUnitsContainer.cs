using System.Collections.Generic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public interface ITeamsUnitsContainer
    {
        IReadOnlyList<BattleUnit> AllWhiteUnits { get; }
        IReadOnlyList<BattleUnit> AllBlackUnits{ get; }
        public void AddWhiteUnit(BattleUnit newUnit);
        public void AddBlackUnit(BattleUnit newUnit);
    }
}