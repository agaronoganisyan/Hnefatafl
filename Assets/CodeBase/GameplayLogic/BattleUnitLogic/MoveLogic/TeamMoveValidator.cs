using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;
using CodeBase.Infrastructure.Services.ServiceLocatorLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public class TeamMoveValidator :ITeamMoveValidator
    {
        private ITeamsUnitsContainer _teamsUnitsContainer;
        private IUnitsPathCalculatorsManager _unitsPathCalculatorsManager;
        
        public void Initialize()
        {
            _teamsUnitsContainer = ServiceLocator.Get<ITeamsUnitsContainer>();
            _unitsPathCalculatorsManager = ServiceLocator.Get<IUnitsPathCalculatorsManager>();
        }
        
        public bool IsThisTeamHaveAnyAvailableMoves(TeamType teamType)
        {
            bool status = false;

            if (teamType == TeamType.White)
            {
                for (int i = 0; i < _teamsUnitsContainer.AllWhiteUnits.Count; i++)
                {
                    if (!_teamsUnitsContainer.AllWhiteUnits[i].IsKilled && _unitsPathCalculatorsManager.IsUnitHasAvailableMoves(_teamsUnitsContainer.AllWhiteUnits[i]))
                    {
                        status = true;
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < _teamsUnitsContainer.AllBlackUnits.Count; i++)
                {
                    if (!_teamsUnitsContainer.AllBlackUnits[i].IsKilled && _unitsPathCalculatorsManager.IsUnitHasAvailableMoves(_teamsUnitsContainer.AllBlackUnits[i]))
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