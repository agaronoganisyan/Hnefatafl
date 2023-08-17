using CodeBase.GameplayLogic.BattleUnitLogic.PathLogic;

namespace CodeBase.GameplayLogic.BattleUnitLogic.MoveLogic
{
    public class TeamMoveValidator :ITeamMoveValidator
    {
        private readonly ITeamsUnitsContainer _teamsUnitsContainer;
        private readonly IUnitsPathCalculatorsManager _unitsPathCalculatorsManager;

        public TeamMoveValidator(ITeamsUnitsContainer teamsUnitsContainer,
            IUnitsPathCalculatorsManager unitsPathCalculatorsManager)
        {
            _teamsUnitsContainer = teamsUnitsContainer;
            _unitsPathCalculatorsManager = unitsPathCalculatorsManager;
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