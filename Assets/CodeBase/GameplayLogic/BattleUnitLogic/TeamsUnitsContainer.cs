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

        public bool IsThisUnitTypeIsAlive(TeamType teamType, UnitType unitType)
        {
            bool status = false;
            
            if (teamType == TeamType.White)
            {
                for (int i = 0; i < _allWhiteUnits.Count; i++)
                {
                    if (_allWhiteUnits[i].UnitType == unitType && !_allWhiteUnits[i].IsKilled)
                    {
                        status = true;
                        break;
                    }
                }
            }
            else if (teamType == TeamType.Black)
            {
                for (int i = 0; i < _allBlackUnits.Count; i++)
                {
                    if (_allBlackUnits[i].UnitType == unitType && !_allBlackUnits[i].IsKilled)
                    {
                        status = true;
                        break;
                    }
                }
            }

            return status;
        }
    }
}