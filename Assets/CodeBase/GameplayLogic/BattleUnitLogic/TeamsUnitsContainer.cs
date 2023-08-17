using System.Collections.Generic;

namespace CodeBase.GameplayLogic.BattleUnitLogic
{
    public class TeamsUnitsContainer : ITeamsUnitsContainer
    {
        public IReadOnlyList<BattleUnit> AllWhiteUnits =>_allWhiteUnits;
        private List<BattleUnit> _allWhiteUnits = new List<BattleUnit>();
        
        public IReadOnlyList<BattleUnit> AllBlackUnits =>_allBlackUnits;
        private List<BattleUnit> _allBlackUnits = new List<BattleUnit>();

        public void AddWhiteUnit(BattleUnit newUnit)
        {
            _allWhiteUnits.Add(newUnit);
        }
        
        public void AddBlackUnit(BattleUnit newUnit)
        {
            _allBlackUnits.Add(newUnit);
        }
    }
}