using System;
using CodeBase.GameplayLogic.BattleUnitLogic;

namespace CodeBase.GameplayLogic.TurnLogic
{
    public class TurnManagerMediator : ITurnManagerMediator
    {
        public event Action<TeamType> OnTeamOfTurnChanged;
        
        public void Initialize()
        {
            
        }
        
        public void Notify(TeamType teamType)
        {
            OnTeamOfTurnChanged?.Invoke(teamType);
        }
    }
}